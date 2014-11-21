namespace nKidReader
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyReader = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.updateNFCIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeDataToCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.automaticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAvatarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableUploadingAvatarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbCameraPreview = new System.Windows.Forms.PictureBox();
            this.chkWcOn = new System.Windows.Forms.CheckBox();
            this.cbCamList = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCameraPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyReader
            // 
            this.notifyReader.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyReader.ContextMenuStrip = this.contextMenuStripMain;
            this.notifyReader.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyReader.Icon")));
            this.notifyReader.Text = "notifyReader";
            this.notifyReader.Visible = true;
            this.notifyReader.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyReader_MouseClick);
            // 
            // contextMenuStripMain
            // 
            this.contextMenuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateNFCIDToolStripMenuItem,
            this.writeDataToCSVToolStripMenuItem,
            this.showAvatarToolStripMenuItem,
            this.enableUploadingAvatarToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.contextMenuStripMain.Name = "contextMenuStripMain";
            this.contextMenuStripMain.Size = new System.Drawing.Size(202, 120);
            // 
            // updateNFCIDToolStripMenuItem
            // 
            this.updateNFCIDToolStripMenuItem.CheckOnClick = true;
            this.updateNFCIDToolStripMenuItem.Name = "updateNFCIDToolStripMenuItem";
            this.updateNFCIDToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.updateNFCIDToolStripMenuItem.Text = "Update NFC ID";
            this.updateNFCIDToolStripMenuItem.Click += new System.EventHandler(this.updateNFCIDToolStripMenuItem_Click);
            // 
            // writeDataToCSVToolStripMenuItem
            // 
            this.writeDataToCSVToolStripMenuItem.CheckOnClick = true;
            this.writeDataToCSVToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manualToolStripMenuItem,
            this.automaticToolStripMenuItem});
            this.writeDataToCSVToolStripMenuItem.Name = "writeDataToCSVToolStripMenuItem";
            this.writeDataToCSVToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.writeDataToCSVToolStripMenuItem.Text = "Write data to CSV";
            this.writeDataToCSVToolStripMenuItem.Click += new System.EventHandler(this.writeDataToCSVToolStripMenuItem_Click);
            // 
            // manualToolStripMenuItem
            // 
            this.manualToolStripMenuItem.Checked = true;
            this.manualToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            this.manualToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.manualToolStripMenuItem.Text = "Manual";
            this.manualToolStripMenuItem.Click += new System.EventHandler(this.manualToolStripMenuItem_Click);
            // 
            // automaticToolStripMenuItem
            // 
            this.automaticToolStripMenuItem.Name = "automaticToolStripMenuItem";
            this.automaticToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.automaticToolStripMenuItem.Text = "Automatic";
            this.automaticToolStripMenuItem.Click += new System.EventHandler(this.automaticToolStripMenuItem_Click);
            // 
            // showAvatarToolStripMenuItem
            // 
            this.showAvatarToolStripMenuItem.CheckOnClick = true;
            this.showAvatarToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.showAvatarToolStripMenuItem.Name = "showAvatarToolStripMenuItem";
            this.showAvatarToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.showAvatarToolStripMenuItem.Text = "Show avatar";
            this.showAvatarToolStripMenuItem.Click += new System.EventHandler(this.showAvatarToolStripMenuItem_Click);
            // 
            // enableUploadingAvatarToolStripMenuItem
            // 
            this.enableUploadingAvatarToolStripMenuItem.CheckOnClick = true;
            this.enableUploadingAvatarToolStripMenuItem.Name = "enableUploadingAvatarToolStripMenuItem";
            this.enableUploadingAvatarToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.enableUploadingAvatarToolStripMenuItem.Text = "Enable uploading avatar";
            this.enableUploadingAvatarToolStripMenuItem.Click += new System.EventHandler(this.enableUploadingAvatarToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(198, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // pbCameraPreview
            // 
            this.pbCameraPreview.Location = new System.Drawing.Point(376, 50);
            this.pbCameraPreview.Name = "pbCameraPreview";
            this.pbCameraPreview.Size = new System.Drawing.Size(320, 240);
            this.pbCameraPreview.TabIndex = 1;
            this.pbCameraPreview.TabStop = false;
            // 
            // chkWcOn
            // 
            this.chkWcOn.AutoSize = true;
            this.chkWcOn.Location = new System.Drawing.Point(388, 16);
            this.chkWcOn.Name = "chkWcOn";
            this.chkWcOn.Size = new System.Drawing.Size(109, 17);
            this.chkWcOn.TabIndex = 2;
            this.chkWcOn.Text = "Turn on Webcam";
            this.chkWcOn.UseVisualStyleBackColor = true;
            this.chkWcOn.CheckedChanged += new System.EventHandler(this.cbWcOn_CheckedChanged);
            // 
            // cbCamList
            // 
            this.cbCamList.Enabled = false;
            this.cbCamList.FormattingEnabled = true;
            this.cbCamList.Location = new System.Drawing.Point(549, 14);
            this.cbCamList.Name = "cbCamList";
            this.cbCamList.Size = new System.Drawing.Size(121, 21);
            this.cbCamList.TabIndex = 3;
            this.cbCamList.SelectedIndexChanged += new System.EventHandler(this.cbCamList_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Image = global::nKidReader.Properties.Resources.nkid;
            this.pictureBox1.Location = new System.Drawing.Point(121, 50);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(116, 182);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(63, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 22);
            this.label1.TabIndex = 5;
            this.label1.Text = "Proximity Reader by NKID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(111, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "2014 © NKID JSC.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label3.Location = new System.Drawing.Point(119, 263);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "All Rights Reserved";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.toolStripMenuItem1.Text = "Enable Webcam";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 315);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cbCamList);
            this.Controls.Add(this.chkWcOn);
            this.Controls.Add(this.pbCameraPreview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "nKid Reader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.contextMenuStripMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCameraPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyReader;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMain;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.PictureBox pbCameraPreview;
        private System.Windows.Forms.CheckBox chkWcOn;
        private System.Windows.Forms.ComboBox cbCamList;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem updateNFCIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writeDataToCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAvatarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableUploadingAvatarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem manualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem automaticToolStripMenuItem;
    }
}

