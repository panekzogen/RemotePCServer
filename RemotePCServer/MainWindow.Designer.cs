namespace RemotePCServer
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.StatusBar = new MetroFramework.Controls.MetroLabel();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.metroContextMenu1 = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitButton = new System.Windows.Forms.ToolStripMenuItem();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.HistoryGrid = new System.Windows.Forms.DataGridView();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.disconnect = new MetroFramework.Controls.MetroButton();
            this.metroTile5 = new MetroFramework.Controls.MetroTile();
            this.version = new MetroFramework.Controls.MetroLabel();
            this.metroTile4 = new MetroFramework.Controls.MetroTile();
            this.model = new MetroFramework.Controls.MetroLabel();
            this.metroTile3 = new MetroFramework.Controls.MetroTile();
            this.device = new MetroFramework.Controls.MetroLabel();
            this.deviceAddress = new MetroFramework.Controls.MetroLabel();
            this.metroTile2 = new MetroFramework.Controls.MetroTile();
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.deviceName = new MetroFramework.Controls.MetroLabel();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroContextMenu1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryGrid)).BeginInit();
            this.metroTabPage1.SuspendLayout();
            this.metroTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusBar
            // 
            this.StatusBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.StatusBar.Location = new System.Drawing.Point(23, 446);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(590, 20);
            this.StatusBar.TabIndex = 1;
            this.StatusBar.Text = "Waiting for connection . . .";
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.metroContextMenu1;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "Remote PC Server";
            this.trayIcon.Visible = true;
            this.trayIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trayIcon_MouseDown);
            // 
            // metroContextMenu1
            // 
            this.metroContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator1,
            this.ExitButton});
            this.metroContextMenu1.Name = "metroContextMenu1";
            this.metroContextMenu1.Size = new System.Drawing.Size(159, 54);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem1.Text = "Favorite devices";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(155, 6);
            // 
            // ExitButton
            // 
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(158, 22);
            this.ExitButton.Text = "Exit";
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.HorizontalScrollbarSize = 10;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(592, 338);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "     Settings     ";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            this.metroTabPage3.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.VerticalScrollbarSize = 10;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.HistoryGrid);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 10;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(592, 338);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "     History     ";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            // 
            // HistoryGrid
            // 
            this.HistoryGrid.AllowUserToAddRows = false;
            this.HistoryGrid.AllowUserToDeleteRows = false;
            this.HistoryGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.HistoryGrid.BackgroundColor = System.Drawing.Color.White;
            this.HistoryGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.HistoryGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HistoryGrid.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.HistoryGrid.Location = new System.Drawing.Point(13, 3);
            this.HistoryGrid.Name = "HistoryGrid";
            this.HistoryGrid.ReadOnly = true;
            this.HistoryGrid.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.HistoryGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.HistoryGrid.Size = new System.Drawing.Size(576, 332);
            this.HistoryGrid.TabIndex = 2;
            this.HistoryGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.HistoryGrid_RowsAdded);
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.AutoScroll = true;
            this.metroTabPage1.Controls.Add(this.disconnect);
            this.metroTabPage1.Controls.Add(this.metroTile5);
            this.metroTabPage1.Controls.Add(this.version);
            this.metroTabPage1.Controls.Add(this.metroTile4);
            this.metroTabPage1.Controls.Add(this.model);
            this.metroTabPage1.Controls.Add(this.metroTile3);
            this.metroTabPage1.Controls.Add(this.device);
            this.metroTabPage1.Controls.Add(this.deviceAddress);
            this.metroTabPage1.Controls.Add(this.metroTile2);
            this.metroTabPage1.Controls.Add(this.metroTile1);
            this.metroTabPage1.Controls.Add(this.deviceName);
            this.metroTabPage1.HorizontalScrollbar = true;
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(592, 338);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "     Connected Device     ";
            this.metroTabPage1.VerticalScrollbar = true;
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // disconnect
            // 
            this.disconnect.Location = new System.Drawing.Point(383, 38);
            this.disconnect.Name = "disconnect";
            this.disconnect.Size = new System.Drawing.Size(103, 23);
            this.disconnect.TabIndex = 13;
            this.disconnect.Text = "Disconnect";
            this.disconnect.UseSelectable = true;
            this.disconnect.Click += new System.EventHandler(this.disconnect_Click);
            // 
            // metroTile5
            // 
            this.metroTile5.ActiveControl = null;
            this.metroTile5.Location = new System.Drawing.Point(124, 257);
            this.metroTile5.Name = "metroTile5";
            this.metroTile5.Size = new System.Drawing.Size(100, 20);
            this.metroTile5.TabIndex = 11;
            this.metroTile5.Text = "Version";
            this.metroTile5.UseSelectable = true;
            // 
            // version
            // 
            this.version.AutoSize = true;
            this.version.Location = new System.Drawing.Point(176, 280);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(38, 19);
            this.version.TabIndex = 10;
            this.version.Text = "none";
            // 
            // metroTile4
            // 
            this.metroTile4.ActiveControl = null;
            this.metroTile4.Location = new System.Drawing.Point(124, 203);
            this.metroTile4.Name = "metroTile4";
            this.metroTile4.Size = new System.Drawing.Size(100, 20);
            this.metroTile4.TabIndex = 9;
            this.metroTile4.Text = "Model";
            this.metroTile4.UseSelectable = true;
            // 
            // model
            // 
            this.model.AutoSize = true;
            this.model.Location = new System.Drawing.Point(176, 226);
            this.model.Name = "model";
            this.model.Size = new System.Drawing.Size(38, 19);
            this.model.TabIndex = 8;
            this.model.Text = "none";
            // 
            // metroTile3
            // 
            this.metroTile3.ActiveControl = null;
            this.metroTile3.Location = new System.Drawing.Point(124, 149);
            this.metroTile3.Name = "metroTile3";
            this.metroTile3.Size = new System.Drawing.Size(100, 20);
            this.metroTile3.TabIndex = 7;
            this.metroTile3.Text = "Device";
            this.metroTile3.UseSelectable = true;
            // 
            // device
            // 
            this.device.AutoSize = true;
            this.device.Location = new System.Drawing.Point(176, 172);
            this.device.Name = "device";
            this.device.Size = new System.Drawing.Size(38, 19);
            this.device.TabIndex = 6;
            this.device.Text = "none";
            // 
            // deviceAddress
            // 
            this.deviceAddress.AutoSize = true;
            this.deviceAddress.Location = new System.Drawing.Point(176, 115);
            this.deviceAddress.Name = "deviceAddress";
            this.deviceAddress.Size = new System.Drawing.Size(38, 19);
            this.deviceAddress.TabIndex = 5;
            this.deviceAddress.Text = "none";
            // 
            // metroTile2
            // 
            this.metroTile2.ActiveControl = null;
            this.metroTile2.Location = new System.Drawing.Point(124, 92);
            this.metroTile2.Name = "metroTile2";
            this.metroTile2.Size = new System.Drawing.Size(100, 20);
            this.metroTile2.TabIndex = 4;
            this.metroTile2.Text = "Address";
            this.metroTile2.UseSelectable = true;
            // 
            // metroTile1
            // 
            this.metroTile1.ActiveControl = null;
            this.metroTile1.Location = new System.Drawing.Point(124, 38);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(100, 20);
            this.metroTile1.TabIndex = 3;
            this.metroTile1.Text = "Name";
            this.metroTile1.UseSelectable = true;
            // 
            // deviceName
            // 
            this.deviceName.AutoSize = true;
            this.deviceName.Location = new System.Drawing.Point(176, 61);
            this.deviceName.Name = "deviceName";
            this.deviceName.Size = new System.Drawing.Size(38, 19);
            this.deviceName.TabIndex = 2;
            this.deviceName.Text = "none";
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Controls.Add(this.metroTabPage3);
            this.metroTabControl1.Enabled = false;
            this.metroTabControl1.Location = new System.Drawing.Point(23, 63);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(600, 380);
            this.metroTabControl1.TabIndex = 0;
            this.metroTabControl1.UseSelectable = true;
            this.metroTabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.metroTabControl1_Selected);
            this.metroTabControl1.EnabledChanged += new System.EventHandler(this.metroTabControl1_EnabledChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.metroTabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.Resizable = false;
            this.Text = "Remote PC Server";
            this.metroContextMenu1.ResumeLayout(false);
            this.metroTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HistoryGrid)).EndInit();
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage1.PerformLayout();
            this.metroTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroLabel StatusBar;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private MetroFramework.Controls.MetroContextMenu metroContextMenu1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExitButton;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroLabel deviceAddress;
        private MetroFramework.Controls.MetroTile metroTile2;
        private MetroFramework.Controls.MetroTile metroTile1;
        private MetroFramework.Controls.MetroLabel deviceName;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private System.Windows.Forms.DataGridView HistoryGrid;
        private MetroFramework.Controls.MetroTile metroTile3;
        private MetroFramework.Controls.MetroLabel device;
        private MetroFramework.Controls.MetroTile metroTile5;
        private MetroFramework.Controls.MetroLabel version;
        private MetroFramework.Controls.MetroTile metroTile4;
        private MetroFramework.Controls.MetroLabel model;
        private MetroFramework.Controls.MetroButton disconnect;

    }
}

