using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Storage;

namespace NewtonRemote;

public partial class MainPage : ContentPage
{
    private UdpClient? udp;
    private CancellationTokenSource? sendCts;

    private int seq = 0;
    private DateTime startTime;
    private double accValue = 0.0;

    private long sentCount = 0;
    private DateTime lastUiUpdate = DateTime.MinValue;

    private const int SendPeriodMs = 20;
    private const int UiPeriodMs = 500;

    public MainPage()
    {
        InitializeComponent();

        TxtIp.Text = Preferences.Get("pc_ip", "192.168.137.208");
        TxtPort.Text = Preferences.Get("pc_port", "4210");
        AxisPicker.SelectedIndex = 0;

        LblWifi.Text = "Telefon IP: " + GetLocalIPAddress();
    }

    private void OnStartClicked(object? sender, EventArgs e)
    {
        try
        {
            if (sendCts != null)
                return;

            Preferences.Set("pc_ip", TxtIp.Text ?? "");
            Preferences.Set("pc_port", TxtPort.Text ?? "4210");

            udp = new UdpClient();
            seq = 0;
            sentCount = 0;
            accValue = 0.0;
            startTime = DateTime.UtcNow;
            lastUiUpdate = DateTime.MinValue;

            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            Accelerometer.Start(SensorSpeed.Game);

            sendCts = new CancellationTokenSource();
            _ = Task.Run(() => SendLoopAsync(sendCts.Token));

            LblStatus.Text = "Küldés elindult";
        }
        catch (Exception ex)
        {
            LblStatus.Text = "Start hiba: " + ex.Message;
        }
    }

    private void OnStopClicked(object? sender, EventArgs e)
    {
        try
        {
            sendCts?.Cancel();
            sendCts = null;

            if (Accelerometer.IsMonitoring)
                Accelerometer.Stop();

            Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;

            udp?.Dispose();
            udp = null;

            LblStatus.Text = $"Leállítva. Csomagok: {sentCount}";
        }
        catch (Exception ex)
        {
            LblStatus.Text = "Stop hiba: " + ex.Message;
        }
    }

    private void Accelerometer_ReadingChanged(object? sender, AccelerometerChangedEventArgs e)
    {
        string axis = AxisPicker.SelectedItem?.ToString() ?? "AY";

        accValue = axis switch
        {
            "AX" => e.Reading.Acceleration.X * 9.81,
            "AZ" => e.Reading.Acceleration.Z * 9.81,
            _ => e.Reading.Acceleration.Y * 9.81
        };
    }

    private async Task SendLoopAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await SendPacketAsync(accValue);

            try
            {
                await Task.Delay(SendPeriodMs, token);
            }
            catch
            {
                break;
            }
        }
    }

    private async Task SendPacketAsync(double value)
    {
        try
        {
            string ip = TxtIp.Text ?? "";
            int port = int.Parse(TxtPort.Text ?? "4210");

            seq++;
            long t_us = (long)(DateTime.UtcNow - startTime).TotalMilliseconds * 1000;

            string msg = $"A;{seq};{t_us};{value:F3}\r\n";
            byte[] data = Encoding.ASCII.GetBytes(msg);

            udp ??= new UdpClient();
            await udp.SendAsync(data, data.Length, ip, port);

            sentCount++;

            if ((DateTime.UtcNow - lastUiUpdate).TotalMilliseconds >= UiPeriodMs)
            {
                lastUiUpdate = DateTime.UtcNow;

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    string axis = AxisPicker.SelectedItem?.ToString() ?? "AY";
                    LblAy.Text = $"{axis} = {value:F2} m/s²";
                    LblStatus.Text = $"Küldés aktív ({sentCount} csomag)";
                });
            }
        }
        catch (Exception ex)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                LblStatus.Text = "Küldési hiba: " + ex.Message;
            });
        }
    }

    private string GetLocalIPAddress()
    {
        try
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
        }
        catch
        {
        }

        return "?";
    }
}