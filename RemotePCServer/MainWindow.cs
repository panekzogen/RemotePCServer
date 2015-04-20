using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using RemotePCLib;
using System.Threading;
using System.Security.Permissions;

namespace RemotePCServer
{
    public partial class MainWindow : MetroForm
    {
        BluetoothServer servSocket;
        Thread connectThread;
        Thread readingThread;

        delegate void SetStateCallback(bool state, string deviceAddress, string deviceName);
        delegate void SetTestRead(string buf);
        public MainWindow()
        {
            InitializeComponent();
            servSocket = new BluetoothServer();
            connectThread = new Thread(new ThreadStart(waitingConnect));
            connectThread.Start();
        }
        private void waitingConnect()
        {
            while (!metroTabControl1.Enabled)
            {
                if(servSocket.clientConnect())
                    switch (MessageBox.Show("Device: " + servSocket.deviceName + " (" + servSocket.deviceAddress + ")" + " try to connect", "Incoming connection",
                        MessageBoxButtons.YesNo))
                    {
                        case DialogResult.Yes:
                            servSocket.getCommand();
                            this.safeSetState(true, servSocket.deviceAddress, servSocket.deviceName);
                            return;
                        default:
                            servSocket.disconnect();
                            this.safeSetState(false, "none", "none");
                            break;
                    }
            }
        }
        private void safeSetState(bool state, string deviceAddress, string deviceName)
        {
            if (this.metroTabControl1.InvokeRequired)
            {
                SetStateCallback d = new SetStateCallback(safeSetState);
                this.Invoke(d, new object[] { state, deviceAddress, deviceName });
            }
            else
            {
                this.metroTabControl1.Enabled = state;
                this.deviceAddress.Text = deviceAddress;
                this.deviceName.Text = deviceName;
            };
        }
        private void streamReading()
        {
            while (true)
            {
                string s = servSocket.getCommand();
                if (s == null)
                {
                    this.safeSetState(false, "none", "none");
                    break;
                }
                else testRead(s);
            }                
        }
        private void testRead(string buf)
        {
            if (this.metroTabControl1.InvokeRequired)
            {
                SetTestRead d = new SetTestRead(testRead);
                this.Invoke(d, new object[] { buf });
            }
            else
            {
                this.bufLabel.Text = buf;
            };
        }
        private void metroTabControl1_Selected(object sender, TabControlEventArgs e)
        {
            switch (metroTabControl1.SelectedIndex)
            {
                case 0: 
                    break;
                default: break;
            }
        }

        private void trayIcon_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
                if (this.Visible == false)
                    this.Show();
                else this.Hide();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            connectThread.Abort();
            readingThread.Abort();
            servSocket.forceStop();
            Application.Exit();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                e.Cancel = true;
                this.Hide();
            }
                
            // Confirm user wants to close
            /*switch (MessageBox.Show(this, "Are you sure you want to close?", "Closing", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                    break;
            }*/
        }

        private void metroTabControl1_EnabledChanged(object sender, EventArgs e)
        {
            if (!metroTabControl1.Enabled)
            {
                readingThread.Abort();
                StatusBar.Text = "Waiting for connection . . .";
                connectThread = new Thread(new ThreadStart(waitingConnect));
                connectThread.Start();
            }
            else
            {
                connectThread.Abort();
                StatusBar.Text = "Connected";
                readingThread = new Thread(new ThreadStart(streamReading));
                readingThread.Start();
            }
        }
    }
}
