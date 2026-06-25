using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;


namespace WifiUsbConfigurator
{
          public partial class MainForm : Form
        {
        private SerialPort serialPort;
        public MainForm()
            {
                InitializeComponent();
            }
        private void MainForm_Load(object sender, EventArgs e)
            {
                RefreshComPorts();
                txtPcIp.Text = GetLocalIPAddress();
                txtPcPort.Text = "5005";
                rbA.Checked = true;
            }
        private void RefreshComPorts()
            {
                cmbComPorts.Items.Clear();

                string[] ports = SerialPort.GetPortNames()
                                           .OrderBy(p => p)
                                           .ToArray();

                cmbComPorts.Items.AddRange(ports);

                if (cmbComPorts.Items.Count > 0)
                    cmbComPorts.SelectedIndex = 0;
            }
        private void SendLine(string line)
            {
                if (serialPort == null || !serialPort.IsOpen)
                {
                    MessageBox.Show("Nincs megnyitott COM kapcsolat.");
                    return;
                }

                try
                {
                    serialPort.WriteLine(line);
                    Log("> " + line);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Küldési hiba:\n" + ex.Message);
                }
            }
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
            {
                try
                {
                    string line = serialPort.ReadLine().Trim();

                    BeginInvoke(new Action(() =>
                    {
                        Log("< " + line);
                        ProcessEspLine(line);
                    }));
                }
                catch
                {
                    // részleges sor vagy timeout esetén nem baj
                }
            }
        private void ProcessEspLine(string line)
            {
                if (line.StartsWith("CFG;"))
                {
                    txtSsid.Text = GetValue(line, "ssid");
                    txtPcIp.Text = GetValue(line, "pcip");
                    txtPcPort.Text = GetValue(line, "pcport");

                    string id = GetValue(line, "id");
                    rbA.Checked = id == "A";
                    rbB.Checked = id == "B";

                    // jelszót biztonsági okból nem töltjük vissza,
                    // ha az ESP csak ****** értéket küld
                }
            }
        private string GetValue(string data, string key)
            {
                key += "=";

                int start = data.IndexOf(key);
                if (start < 0) return "";

                start += key.Length;

                int end = data.IndexOf(";", start);
                if (end < 0) end = data.Length;

                return data.Substring(start, end - start);
            }
        private void Log(string text)
            {
            txtLog.Text = DateTime.Now.ToString("HH:mm:ss") + "  " + text;
            //txtLog.AppendText(DateTime.Now.ToString("HH:mm:ss") + "  " + text + Environment.NewLine);
        }
        private string GetLocalIPAddress()
            {
                try
                {
                    using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                    {
                        socket.Connect("8.8.8.8", 65530);
                        IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                        return endPoint.Address.ToString();
                    }
                }
                catch
                {
                    return "192.168.1.100";
                }
            }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
            {
                if (serialPort != null && serialPort.IsOpen)
                    serialPort.Close();
            }
        private void btnConnect_Click(object sender, EventArgs e)
        {
          try
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    serialPort.Close();
                    btnConnect.Text = "Kapcsolódás";
                    Log("Kapcsolat bontva.");
                    return;
                }

                if (cmbComPorts.SelectedItem == null)
                {
                    MessageBox.Show("Válassz COM portot!");
                    return;
                }

                serialPort = new SerialPort(cmbComPorts.SelectedItem.ToString(), 115200);
                serialPort.NewLine = "\n";
                serialPort.ReadTimeout = 1500;
                serialPort.WriteTimeout = 1500;
                serialPort.DataReceived += SerialPort_DataReceived;
                serialPort.Open();

                btnConnect.Text = "Bontás";
                Log("Kapcsolódva: " + serialPort.PortName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nem sikerült kapcsolódni:\n" + ex.Message);
            }
        }
        private void btnReboot_Click(object sender, EventArgs e)
        {
            SendLine("REBOOT");
        }
        private void btnInfo_Click(object sender, EventArgs e)
        {
            SendLine("INFO");
        }
        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            string id = rbA.Checked ? "A" : "B";

            string cmd =
                "SETCFG;" +
                "ssid=" + txtSsid.Text.Trim() + ";" +
                "pass=" + txtPassword.Text.Trim() + ";" +
                "pcip=" + txtPcIp.Text.Trim() + ";" +
                "pcport=" + txtPcPort.Text.Trim() + ";" +
                "id=" + id;

            SendLine(cmd);
        }

        private void btnRefreshPorts_Click(object sender, EventArgs e)
        {
            RefreshComPorts();
        }

        private void btn_ReadConfig_Click(object sender, EventArgs e)
        {

            SendLine("GETCFG");
        }
        private string GetVisibleWifiNetworks()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "netsh";
                psi.Arguments = "wlan show networks mode=bssid";
                psi.RedirectStandardOutput = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.StandardOutputEncoding = Encoding.UTF8;

                using (Process p = Process.Start(psi))
                {
                    string output = p.StandardOutput.ReadToEnd();
                    p.WaitForExit();
                    return output;
                }
            }
            catch (Exception ex)
            {
                return "WiFi lista lekérdezési hiba: " + ex.Message;
            }
        }
        private void btnWifiList_Click(object sender, EventArgs e)
        {
            txtLog.Text = GetVisibleWifiNetworks();
        }
    }
}
    


