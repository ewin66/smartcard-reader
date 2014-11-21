using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace nKidReader
{
    public partial class NFCCardIdUpload : Form
    {
        const int AW_SLIDE = 0X40000;
        const int AW_VER_POSITIVE = 0X4;
        const int AW_VER_NEGATIVE = 0X8;
        const int AW_BLEND = 0X80000;

        [DllImport("user32")]
        static extern bool AnimateWindow(IntPtr hwnd, int time, int flags);

        public NFCCardIdUpload()
        {
            InitializeComponent();

        }
        private string nfcID;
        private string mrsID;
        private NotifyIcon notifyReader;
        public void getNFCCode(string nfcID)
        {
            lbNFC.Text = "NFC: " + nfcID;
            this.nfcID = nfcID;
            button1.Enabled = true;
            button2.Enabled = true;
            button1.Focus();
        }
        
        public NFCCardIdUpload(string mrsID, NotifyIcon nr)
        {
            InitializeComponent();
            this.mrsID = mrsID;
            this.notifyReader = nr;
            lbNFC.Text = "NFC: ";
            lbMRS.Text = "MRS: " + mrsID;

            
            button1.Enabled = false;
            button2.Enabled = false;
            label1.Text = "Không có mã thẻ NFC trong dữ liệu!";
        
            AnimateWindow(this.Handle, 200, AW_SLIDE | AW_VER_NEGATIVE);
            
            timer1.Start();
        }

        public delegate string PassControl(string result, string magneticCardID, string nfcID);

        public PassControl passControl;
        private void button1_Click(object sender, EventArgs e)
        {
            string result = passControl("OK", this.mrsID, this.nfcID);
            notifyReader.BalloonTipText = result;
            notifyReader.ShowBalloonTip(100);
            timer1.Stop();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string result = passControl("Cancel", this.mrsID, this.nfcID);
            notifyReader.BalloonTipText = result;
            notifyReader.ShowBalloonTip(100);
            timer1.Stop();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Close();
        }
    }
}
