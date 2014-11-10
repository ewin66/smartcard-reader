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
           // AnimateWindow(this.Handle, 700, AW_SLIDE | AW_VER_NEGATIVE);
        }

        public NFCCardIdUpload(string nfcID, string mrsID, string status)
        {
            InitializeComponent();
            lbNFC.Text = "NFC: ";
            lbMRS.Text = "MRS: " + mrsID;

            
            if (status == "NFC not found")
            {
                lbNFC.Text = "NFC: " + nfcID;
                button1.Visible = true;
                button2.Visible = true;
                label1.Text = "Không có mã thẻ MFC trong dữ liệu! Bạn có muốn cập nhật?";
            }

            else if (status == "ID not found")
            {
                
                button1.Visible = false;
                button2.Visible = false;
                label1.Text = "Không tìm thấy thông tin thẻ";
            }
            else
            {
                lbNFC.Text = "NFC: " + status;
                button1.Visible = false;
                button2.Visible = false;
                label1.Text = "Mã thẻ NFC đã có trong dữ liệu!";
            }
            AnimateWindow(this.Handle, 300, AW_SLIDE | AW_VER_NEGATIVE);

        }

        public delegate void PassControl(string result);

        public PassControl passControl;
        private void button1_Click(object sender, EventArgs e)
        {
            passControl("OK");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            passControl("Cancel");
            this.Close();
        }
    }
}
