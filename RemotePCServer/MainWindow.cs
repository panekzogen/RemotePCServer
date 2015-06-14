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

using System.Runtime.InteropServices;
using Microsoft.Office.Core;
using Microsoft.Office.Interop;



namespace RemotePCServer
{
    public partial class MainWindow : MetroForm
    {
        BluetoothServer servSocket;
        Thread connectThread;
        Thread readingThread;

        DataTable historyTable;

        Microsoft.Office.Interop.PowerPoint.Application oPPT;

        MediaPlayer mp;

        delegate void SetStateCallback(bool state, string deviceAddress, string deviceName, string device, string deviceModel, string deviceVersion);
        delegate void SetTestRead(string buf);
        public MainWindow()
        {
            InitializeComponent();
            historyTable = new DataTable();
            historyTable.Columns.Add("Time", typeof(DateTime));
            historyTable.Columns.Add("Operation", typeof(String));
            historyTable.Columns.Add("From", typeof(String));
            this.HistoryGrid.DataSource = historyTable;

            metroTabControl1.SelectTab(0);

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
                            this.safeSetState(true, servSocket.deviceAddress, servSocket.deviceName, servSocket.device, servSocket.deviceModel, servSocket.androidVersion);
                            return;
                        default:
                            servSocket.disconnect();
                            this.safeSetState(false, "none", "none", "none", "none", "none");
                            break;
                    }
            }
        }
        private void safeSetState(bool state, string deviceAddress, string deviceName, string device, string deviceModel, string deviceVersion)
        {
            if (this.metroTabControl1.InvokeRequired)
            {
                SetStateCallback d = new SetStateCallback(safeSetState);
                this.Invoke(d, new object[] { state, deviceAddress, deviceName, device, deviceModel, deviceVersion });
            }
            else
            {
                if (deviceName != "none")
                {
                    trayIcon.BalloonTipText = deviceName + " connected.";
                    trayIcon.BalloonTipTitle = "Incoming connection";
                    trayIcon.ShowBalloonTip(1);
                }
                this.metroTabControl1.Enabled = state;
                this.deviceAddress.Text = deviceAddress;
                this.deviceName.Text = deviceName;
                this.device.Text = device;
                this.model.Text = deviceModel;
                this.version.Text = deviceVersion;
                servSocket.sendCommand("lal");
            };
        }
        private void streamReading()
        {
            string s;
            while (true)
            {
                mut.WaitOne();
                s = servSocket.getCommand();
                if (s == null)
                {
                    this.safeSetState(false, "none", "none", "none", "none", "none");
                    mut.ReleaseMutex();
                    break;
                }
                else bytesTransf(s); 
                if (s == "0109111117115101" || s == "1071011219811197114100")
                {
                    Thread.Sleep(200);
                }
                mut.ReleaseMutex();
            }                
        }
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        const uint KEYEVENTF_KEYUP = 0x0002;
        [DllImport("user32.dll")]
        static extern bool keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        private void bytesTransf(string buf)
        {
            if (this.metroTabControl1.InvokeRequired)
            {
                SetTestRead d = new SetTestRead(bytesTransf);
                this.Invoke(d, new object[] { buf });
            }
            else
            {
                DataRow dt = historyTable.NewRow();
                dt[0] = DateTime.Now;
                dt[1] = buf;
                dt[2] = deviceName.Text;
                historyTable.Rows.InsertAt(dt, 0);
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
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
                this.Activate();
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            connectThread.Abort();
            if(readingThread != null)
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
                mut = new Mutex();
                connectThread = new Thread(new ThreadStart(waitingConnect));
                connectThread.Start();
            }
            else
            {
                historyTable.Rows.Clear();
                connectThread.Abort();
                StatusBar.Text = "Connected";
                readingThread = new Thread(new ThreadStart(streamReading));
                readingThread.Start();
            }
        }


        private void HistoryGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            switch (historyTable.Rows[0].ItemArray[1].ToString())
            {
                case "112114101118105111117115":
                    mp = new MediaPlayer();
                    HistoryGrid.Rows[0].Cells[1].Value = "WMP Previous"; 
                    mp.Prev();
                    break;
                case "01121089712111297117115101":
                    mp = new MediaPlayer();
                    HistoryGrid.Rows[0].Cells[1].Value = "WMP Play/Pause";      
                    mp.PlayPause();
                    break;
                case "110101120116":
                    mp = new MediaPlayer();
                    HistoryGrid.Rows[0].Cells[1].Value = "WMP Next";    
                    mp.Next();
                    break;
                case "8611110868111119110":
                    mp = new MediaPlayer();
                    HistoryGrid.Rows[0].Cells[1].Value = "WMP Volume Down";
                    mp.VolDown();
                    break;
                case "8611110885112":
                    mp = new MediaPlayer();
                    HistoryGrid.Rows[0].Cells[1].Value = "WMP Volume Up";
                    mp.VolUp();
                    break;
                case "8211711067108111115101":
                    mp = new MediaPlayer();
                    HistoryGrid.Rows[0].Cells[1].Value = "WMP Run/Close";
                    if (mp.isRunned) mp.Close();
                    else mp.Run();
                    break;
                case "11511697114116115108105100101115104111119":
                    HistoryGrid.Rows[0].Cells[1].Value = "PP Start slideshow";
                    oPPT = new Microsoft.Office.Interop.PowerPoint.Application();
                    if (oPPT.Visible == MsoTriState.msoTrue)
                        oPPT.ActivePresentation.SlideShowSettings.Run();
                    break;
                case "0110101120116115108105100101":
                    HistoryGrid.Rows[0].Cells[1].Value = "PP Next slide";
                    oPPT = new Microsoft.Office.Interop.PowerPoint.Application();
                    if (oPPT.Visible == MsoTriState.msoTrue)
                        if (oPPT.SlideShowWindows.Count != 0)
                            oPPT.ActivePresentation.SlideShowWindow.View.Next();
                    break;
                case "0112114101118105111117115115108105100101":
                    HistoryGrid.Rows[0].Cells[1].Value = "PP Previous slide";
                    oPPT = new Microsoft.Office.Interop.PowerPoint.Application();
                    if (oPPT.Visible == MsoTriState.msoTrue)
                        if (oPPT.SlideShowWindows.Count != 0)
                            oPPT.ActivePresentation.SlideShowWindow.View.Previous();
                    break;
                case "0115116111112115108105100101115104111119":
                    HistoryGrid.Rows[0].Cells[1].Value = "PP Stop slideshow";
                    oPPT = new Microsoft.Office.Interop.PowerPoint.Application();
                    if (oPPT.Visible == MsoTriState.msoTrue)
                            if (oPPT.SlideShowWindows.Count != 0)
                                oPPT.ActivePresentation.SlideShowWindow.View.Exit();
                    break;
                case "0109111117115101":
                    HistoryGrid.Rows[0].Cells[1].Value = "Touchpad activate";
                    Thread touchpadThread = new Thread(new ThreadStart(touchpadMode));
                    touchpadThread.Start();
                    break;
                case "1071011219811197114100":
                    HistoryGrid.Rows[0].Cells[1].Value = "Keyboar activate";
                    Thread keyboardThread = new Thread(new ThreadStart(keyboardMode));
                    keyboardThread.Start();
                    break;
                default:
                    break;
            }
        }

        private static Mutex mut = new Mutex();
        private void keyboardMode()
        {
            string s;
            int key;
            mut.WaitOne();
            while (true)
            {
                s = servSocket.getCommand();
                if (s == "0101110100109111117115101") break;
                if (s == null)
                {
                    this.safeSetState(false, "none", "none", "none", "none", "none");
                    break;
                }
                if (s.Length >= 4) s = s.Substring(0, 2);
                key = Int32.Parse(s);
                switch (key)
                {
                    case 20:
                        if (!IsKeyLocked(Keys.CapsLock))
                            keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                        else while (IsKeyLocked(Keys.CapsLock))
                            {
                                keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                                keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
                            }
                        break;
                    case 144:
                        if (!IsKeyLocked(Keys.NumLock))
                            keybd_event(144, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                        else while (IsKeyLocked(Keys.NumLock))
                            {
                                keybd_event(144, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                                keybd_event(144, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
                            }
                        break;
                    default:
                        keybd_event((byte)key, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                        break;
                }
            }
            mut.ReleaseMutex();
        }
        private void touchpadMode()
        {
            string s;
            string xstr, ystr;
            double x = 0, y = 0, delta = 0.5;
            mut.WaitOne();
            while (true)
            {
                s = servSocket.getCommand();
                if (s == null || s == "0101110100109111117115101") break;
                if (s == "1081011021169910810599107")
                {
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
                    continue;
                }
                if (s == "01141051031041169910810599107")
                {
                    mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
                    continue;
                }
                xstr = s.Substring(0, s.IndexOf('x'));
                ystr = s.Substring(s.IndexOf('x') + 1, s.IndexOf(';') - s.IndexOf('x') - 1);
                if (xstr[0] == '-')
                {
                    x = Int32.Parse(xstr.Substring(1));
                    x = x * x * -1;
                }
                else { x = Int32.Parse(xstr); x *= x; }
                if (ystr[0] == '-')
                {
                    y = Int32.Parse(ystr.Substring(1));
                    y = y * y * -1;
                }
                else { y = Int32.Parse(ystr); y *= y; }
                while (x != 0 || y != 0)
                {
                    if (x != 0)
                    {
                        if (x > 0)
                        {
                            x -= delta;
                            Cursor.Position = new Point(Cursor.Position.X + 1, Cursor.Position.Y);
                        }
                        else
                        {
                            x += delta;
                            Cursor.Position = new Point(Cursor.Position.X - 1, Cursor.Position.Y);
                        }
                    }
                    x = Math.Round(x, 1);
                    if (y != 0)
                    {
                        if (y > 0)
                        {
                            y -= delta;
                            Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + 1);
                        }
                        else
                        {
                            y += delta;
                            Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - 1);
                        }
                    }
                    y = Math.Round(y, 1);

                }
            }
            mut.ReleaseMutex();
        }

        private void disconnect_Click(object sender, EventArgs e)
        {
            this.safeSetState(false, "none", "none", "none", "none", "none");
        }
    }
}
