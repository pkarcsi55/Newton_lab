using ScottPlot.WinForms;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Newton
{
    public partial class MainForm : Form
    {
        public class ForceSample
        {
            public long T_us { get; set; }
            public int Raw { get; set; }
            public double Force_N { get; set; }
        }
        public class ForceRingBuffer
        {
            private readonly ForceSample[] buffer;
            private int index = 0;
            private int count = 0;
            private readonly object lockObj = new object();

            public ForceRingBuffer(int capacity)
            {
                buffer = new ForceSample[capacity];
            }

            public void Add(ForceSample sample)
            {
                lock (lockObj)
                {
                    buffer[index] = sample;
                    index = (index + 1) % buffer.Length;

                    if (count < buffer.Length)
                        count++;
                }
            }

            public ForceSample[] GetSnapshot()
            {
                lock (lockObj)
                {
                    ForceSample[] result = new ForceSample[count];

                    int start = (index - count + buffer.Length) % buffer.Length;

                    for (int i = 0; i < count; i++)
                    {
                        int pos = (start + i) % buffer.Length;
                        result[i] = buffer[pos];
                    }

                    return result;
                }
            }

            public void Clear()
            {
                lock (lockObj)
                {
                    index = 0;
                    count = 0;
                }
            }
        }
        private string settingsFile = Path.Combine(Application.StartupPath, "setting.txt");
        private bool isOpenA = false;
        private bool isOpenB = false;
        private ForceRingBuffer forceBufferA = new ForceRingBuffer(8000);
        private ForceRingBuffer forceBufferB = new ForceRingBuffer(8000);
        private double displayWindowSec = 10.0;
        private double factorA = 1.0;
        private double factorB = 1.0;

        // C# oldali tárázás.
        // A tareOffset... a skálázás és B-csatorna előjelfordítás UTÁNI,
        // de még nullázás ELŐTTI értéket tárolja. Így az ESP módosítása nélkül
        // működik UDP/WiFi üzemmódban.
        private double tareOffsetA = 0.0;
        private double tareOffsetB = 0.0;
        private double latestUntaredForceA = 0.0;
        private double latestUntaredForceB = 0.0;
        private bool hasLatestA = false;
        private bool hasLatestB = false;

        private FormsPlot formsPlot = new FormsPlot();
        private System.Windows.Forms.Timer plotTimer = new System.Windows.Forms.Timer();

        // ===== UDP TESZT MÓD =====
        private const int UDP_PORT = 4210;
        private UdpClient udpClient;
        private Thread udpThread;
        private volatile bool udpRunning = false;

        // ===== UDP FREKVENCIATESZT =====
        private readonly object udpStatLock = new object();
        private long lastUdpTimeUsA = -1;
        private long lastUdpTimeUsB = -1;
        private int lastUdpSeqA = -1;
        private int lastUdpSeqB = -1;
        private int udpLostA = 0;
        private int udpLostB = 0;
        private double udpDtMaxMsA = 0.0;
        private double udpDtMaxMsB = 0.0;
        private string udpStatTextA = "";
        private string udpStatTextB = "";

        // Gördülő frekvenciamérés: nem teljes futás átlaga, hanem az utolsó N mintaköz
        private const int UDP_FREQ_WINDOW = 80;
        private Queue<double> udpDtWindowA = new Queue<double>();
        private Queue<double> udpDtWindowB = new Queue<double>();
        // ===============================
        // =========================
        private string GetLocalIPv4()
        {
            foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }

            return "N/A";
        }
        private void PlotTimer_Tick(object sender, EventArgs e)
        {
            double windowSec = displayWindowSec;
            formsPlot.Plot.Axes.SetLimitsX(0, displayWindowSec);
            formsPlot.Plot.Clear();
            bool hasData = false;
            if (isOpenA)
            {
                ForceSample[] samplesA = forceBufferA.GetSnapshot();

                if (samplesA.Length >= 2)
                {
                    double latestA = samplesA[samplesA.Length - 1].T_us / 1_000_000.0;

                    double[] xsA = samplesA
                        .Select(s =>
                        displayWindowSec + (s.T_us / 1_000_000.0 - latestA))
                        .ToArray();

                    double[] ysA = samplesA
                    .Select(s => s.Force_N)
                    .ToArray();

                    formsPlot.Plot.Add.Scatter(xsA, ysA);
                    hasData = true;
                }
            }

            if (isOpenB)
            {
                ForceSample[] samplesB = forceBufferB.GetSnapshot();

                if (samplesB.Length >= 2)
                {
                    double latestB = samplesB[samplesB.Length - 1].T_us / 1_000_000.0;

                    double[] xsB = samplesB
                        .Select(s => windowSec + (s.T_us / 1_000_000.0 - latestB))
                        .ToArray();

                    double[] ysB = samplesB
                        .Select(s => s.Force_N)
                        .ToArray();

                    formsPlot.Plot.Add.Scatter(xsB, ysB);
                    hasData = true;
                }
            }

            if (!hasData)
                return;
            formsPlot.Plot.Axes.SetLimitsX(0, windowSec);
            formsPlot.Refresh();
        }
        private void TareDevice(string slot)
        {
            try
            {
                if (slot == "A")
                {
                    if (!hasLatestA)
                    {
                        MessageBox.Show("Az A csatornán még nincs beérkezett mérési adat.");
                        return;
                    }

                    tareOffsetA = latestUntaredForceA;
                    forceBufferA.Clear();
                    lblStatusA.Text = "A: tárázva";
                }
                else if (slot == "B")
                {
                    if (!hasLatestB)
                    {
                        MessageBox.Show("A B csatornán még nincs beérkezett mérési adat.");
                        return;
                    }

                    tareOffsetB = latestUntaredForceB;
                    forceBufferB.Clear();
                    lblStatusB.Text = "B: tárázva";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tárázási hiba:\n" + ex.Message);
            }
        }
        private void StartUdpReceiver()
        {
            if (udpRunning)
                return;

            try
            {
                udpClient = new UdpClient(UDP_PORT);
                udpRunning = true;

                udpThread = new Thread(UdpReceiveLoop);
                udpThread.IsBackground = true;
                udpThread.Start();

                lblStatusA.Text = "UDP: port " + UDP_PORT;
            }
            catch (Exception ex)
            {
                MessageBox.Show("UDP vevő indítási hiba:\n" + ex.Message);
            }
        }

        private void StopUdpReceiver()
        {
            udpRunning = false;

            try
            {
                if (udpClient != null)
                    udpClient.Close();
            }
            catch { }

            udpClient = null;
        }

        private bool udpDebugShown = false;


        private void UdpReceiveLoop()
        {
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

            while (udpRunning)
            {
                try
                {
                    byte[] data = udpClient.Receive(ref remoteEP);
                    string line = Encoding.ASCII.GetString(data).Trim();

                    if (TryParseUdpForceLine(line, out string slot, out ForceSample sample))
                    {
                        ProcessIncomingSample(slot, sample, "UDP");
                    }
                }
                catch
                {
                    // StopUdpReceiver() lezárja a socketet, ilyenkor természetes a hiba.
                }
            }
        }

        private bool TryParseUdpForceLine(string line, out string slot, out ForceSample sample)
        {
            slot = "";
            sample = null;

            // Várt ESP formátum:
            // A;csomagszam;ido_us;ero_N
            // példa: A;152;3045821;0.7342
            string[] p = line.Split(';');

            if (p.Length < 4)
                return false;

            slot = p[0].Trim().ToUpperInvariant();

            if (slot != "A" && slot != "B")
                return false;

            if (!long.TryParse(p[2], out long t_us))
                return false;

            if (!double.TryParse(
                    p[3].Replace(",", "."),
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out double force))
                return false;

            int raw = 0;
            int.TryParse(p[1], out raw); // tesztben ide kerülhet a csomagszám

            sample = new ForceSample
            {
                T_us = t_us,
                Raw = raw,
                Force_N = force
            };

            return true;
        }

        private void UpdateUdpFrequencyStats(string slot, ForceSample sample)
        {
            if (sample == null)
                return;

            lock (udpStatLock)
            {
                bool isA = (slot == "A");

                long lastT = isA ? lastUdpTimeUsA : lastUdpTimeUsB;
                int lastSeq = isA ? lastUdpSeqA : lastUdpSeqB;
                int lost = isA ? udpLostA : udpLostB;
                double dtMax = isA ? udpDtMaxMsA : udpDtMaxMsB;
                Queue<double> win = isA ? udpDtWindowA : udpDtWindowB;

                // ESP újraindulás / micros visszaugrás / csomagszám visszaugrás esetén nullázzuk a statisztikát
                if ((lastT > 0 && sample.T_us < lastT) ||
                    (lastSeq >= 0 && sample.Raw > 0 && sample.Raw < lastSeq))
                {
                    win.Clear();
                    lost = 0;
                    dtMax = 0.0;
                }

                double lastDtMs = 0.0;

                if (lastT > 0 && sample.T_us > lastT)
                {
                    double dtMs = (sample.T_us - lastT) / 1000.0;
                    lastDtMs = dtMs;

                    // Ésszerű tartomány: kiszűrjük az indítási/újraindítási nagy ugrásokat
                    if (dtMs > 1.0 && dtMs < 100.0)
                    {
                        win.Enqueue(dtMs);
                        while (win.Count > UDP_FREQ_WINDOW)
                            win.Dequeue();

                        if (dtMs > dtMax)
                            dtMax = dtMs;
                    }
                }

                // UDP formátumban a Raw mezőbe most a csomagszámot tettük.
                if (lastSeq >= 0 && sample.Raw > lastSeq + 1)
                    lost += sample.Raw - lastSeq - 1;

                lastT = sample.T_us;
                lastSeq = sample.Raw;

                double avgDtMs = win.Count > 0 ? win.Average() : 0.0;
                double hz = avgDtMs > 0.0 ? 1000.0 / avgDtMs : 0.0;

                string stat = $"{hz:F1} Hz, lost={lost}";

                if (isA)
                {
                    lastUdpTimeUsA = lastT;
                    lastUdpSeqA = lastSeq;
                    udpLostA = lost;
                    udpDtMaxMsA = dtMax;
                    udpStatTextA = stat;
                }
                else
                {
                    lastUdpTimeUsB = lastT;
                    lastUdpSeqB = lastSeq;
                    udpLostB = lost;
                    udpDtMaxMsB = dtMax;
                    udpStatTextB = stat;
                }
            }
        }

        private void ProcessIncomingSample(string slot, ForceSample sample, string source)
        {
            ForceRingBuffer buffer = (slot == "A") ? forceBufferA : forceBufferB;

            double displayForce = sample.Force_N * (slot == "A" ? factorA : factorB);

            // B csatorna előjelfordítása
            if (slot == "B")
                displayForce = -displayForce;

            if (slot == "A")
            {
                latestUntaredForceA = displayForce;
                hasLatestA = true;
                displayForce -= tareOffsetA;
            }
            else
            {
                latestUntaredForceB = displayForce;
                hasLatestB = true;
                displayForce -= tareOffsetB;
            }

            sample.Force_N = displayForce;

            if (source == "UDP")
                UpdateUdpFrequencyStats(slot, sample);

            buffer.Add(sample);

            if (slot == "A")
                isOpenA = true;
            else
                isOpenB = true;

            try
            {
                BeginInvoke(new Action(() =>
                {
                    if (slot == "A")
                    {
                        string stat = (source == "UDP") ? " | " + udpStatTextA : "";


                        lblStatusA.Text = $"A: {displayForce:F3} N {stat}";
                    }
                    else
                    {
                        string stat = (source == "UDP") ? " | " + udpStatTextB : "";

                        lblStatusB.Text = $"B: {displayForce:F3} N {stat}";
                    }
                }));
            }
            catch { }
        }

        private void LoadSettings()
        {
            if (!File.Exists(settingsFile))
                return;

            string[] lines = File.ReadAllLines(settingsFile);

            foreach (string line in lines)
            {
                string[] parts = line.Split('=');

                if (parts.Length != 2)
                    continue;

                string key = parts[0].Trim();
                string value = parts[1].Trim();

                if (key == "TimeBaseIndex")
                {
                    if (int.TryParse(value, out int idx))
                    {
                        if (idx >= 0 && idx < comboTimeWindow.Items.Count)
                            comboTimeWindow.SelectedIndex = idx;
                    }
                }
                else if (key == "FactorA")
                {
                    if (double.TryParse(
                        value.Replace(",", "."),
                        System.Globalization.NumberStyles.Float,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out double f))
                    {
                        factorA = f;
                        txtFactorA.Text = f.ToString("F3");
                    }
                }
                else if (key == "FactorB")



                {
                    if (double.TryParse(
                        value.Replace(",", "."),
                        System.Globalization.NumberStyles.Float,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out double f))
                    {
                        factorB = f;
                        txtFactorB.Text = f.ToString("F3");
                    }
                }
            }
        }
        private void SaveSettings()
        {

            List<string> lines = new List<string>();

            lines.Add($"TimeBaseIndex={comboTimeWindow.SelectedIndex}");
            lines.Add($"FactorA={factorA.ToString(System.Globalization.CultureInfo.InvariantCulture)}");
            lines.Add($"FactorB={factorB.ToString(System.Globalization.CultureInfo.InvariantCulture)}");

            File.WriteAllLines(settingsFile, lines);
        }
        private void SaveBuffersToCsv(string fileName)
        {
            ForceSample[] a = forceBufferA.GetSnapshot();
            ForceSample[] b = forceBufferB.GetSnapshot();

            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                sw.WriteLine("channel;t_us;t_s;raw;force_N");

                foreach (ForceSample s in a)
                {
                    double t_s = s.T_us / 1_000_000.0;
                    double f = s.Force_N;

                    sw.WriteLine(
                        $"A;{s.T_us};{t_s.ToString(CultureInfo.InvariantCulture)};{s.Raw};{f.ToString(CultureInfo.InvariantCulture)}");
                }

                foreach (ForceSample s in b)
                {
                    double t_s = s.T_us / 1_000_000.0;
                    double f = s.Force_N;

                    sw.WriteLine(
                        $"B;{s.T_us};{t_s.ToString(CultureInfo.InvariantCulture)};{s.Raw};{f.ToString(CultureInfo.InvariantCulture)}");
                }
            }

            MessageBox.Show("CSV mentve.");
        }
        //**************************************
        public MainForm()
        {

            InitializeComponent();
            groupBoxControls.Dock = DockStyle.Top;
            groupBoxControls.Height = 120;

            panelPlot.Dock = DockStyle.Fill;
            panelPlot.BringToFront();

            formsPlot.Dock = DockStyle.Fill;
            panelPlot.Controls.Add(formsPlot);

            formsPlot.Plot.Title("Force–Time Graph");
            formsPlot.Plot.XLabel("Time (s)");
            formsPlot.Plot.YLabel("Force (N)");
            plotTimer.Interval = 50;
            plotTimer.Tick += PlotTimer_Tick;
            plotTimer.Start();
            this.FormClosing += MainForm_FormClosing;

        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            comboTimeWindow.Items.Add("2 s");
            comboTimeWindow.Items.Add("5 s");
            comboTimeWindow.Items.Add("10 s");
            comboTimeWindow.Items.Add("20 s");
            comboTimeWindow.Items.Add("30 s");
            comboTimeWindow.Items.Add("60 s");
            comboTimeWindow.SelectedIndex = 2;//10 sec
            txtFactorA.Text = "1,000";
            txtFactorB.Text = "1,000";
            LoadSettings();
            lblIpAddress.Text =    $"IP: {GetLocalIPv4()} UDP: {UDP_PORT}";

            // UDP tesztvevő automatikus indítása
            StartUdpReceiver();

        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            StopUdpReceiver();
            SaveSettings();


        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            forceBufferA.Clear();
            forceBufferB.Clear();

            lblStatusA.Text = "várakozás...";
            lblStatusB.Text = "várakozás...";

            plotTimer.Start();
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            plotTimer.Stop();

            lblStatusA.Text = "áll";
            lblStatusB.Text = "áll";
        }

        private void btnTareA_Click(object sender, EventArgs e)
        {
            TareDevice("A");
        }
        private void btnTareB_Click(object sender, EventArgs e)
        {
            TareDevice("B");
        }
        private void comboTimeWindow_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboTimeWindow.SelectedIndex)
            {
                case 0:
                    displayWindowSec = 2;
                    break;

                case 1:
                    displayWindowSec = 5;
                    break;

                case 2:
                    displayWindowSec = 10;
                    break;

                case 3:
                    displayWindowSec = 20;
                    break;

                case 4:
                    displayWindowSec = 30;
                    break;

                case 5:
                    displayWindowSec = 60;
                    break;
            }
        }
        private void clearGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            forceBufferA.Clear();
            forceBufferB.Clear();
            PlotTimer_Tick(null, EventArgs.Empty);
            formsPlot.Refresh();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void txtFactorB_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtFactorB.Text.Replace(",", "."),
        System.Globalization.NumberStyles.Float,
        System.Globalization.CultureInfo.InvariantCulture,
        out double f))
            {
                factorB = f;
            }
        }
        private void txtFactorA_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtFactorA.Text.Replace(",", "."),
        System.Globalization.NumberStyles.Float,
        System.Globalization.CultureInfo.InvariantCulture,
        out double f))
            {
                factorA = f;
            }
        }
        private void saveAsCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV fájl (*.csv)|*.csv";
                sfd.FileName = "newton_force_measurement.csv";

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                SaveBuffersToCsv(sfd.FileName);
            }
        }
        private void helpToolStripMenuItem_Click(
object sender,
EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", Path.Combine(Application.StartupPath, "help.txt"));

        }

    }
}

