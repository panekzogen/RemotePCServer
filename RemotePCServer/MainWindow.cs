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

namespace RemotePCServer
{
    public partial class MainWindow : MetroForm
    {
        BluetoothServer servSocket;
        public MainWindow()
        {
            InitializeComponent();
            servSocket = new BluetoothServer();
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
    }
}
