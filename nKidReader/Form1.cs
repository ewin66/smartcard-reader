using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PCSC;

namespace nKidReader
{
    public partial class MainForm : Form
    {
        private Thread readerThread;
        public delegate void DetectCardID(String cardID);
        public DetectCardID delegateDetectCardID;

        public delegate void ReaderError(PCSCException error);
        public ReaderError delegateReaderError;

        public MainForm()
        {
            InitializeComponent();
            //Delegate
            delegateDetectCardID = new DetectCardID(DetectCardIDMethod);
            delegateReaderError = new ReaderError(ReaderErrorMethod);

            //Load reader thread
            readerThread = loadReader();
        }

        private Thread loadReader()
        {
            var t = new Thread(() => PCSCSharp.Ready(this));
            t.Start();
            return t;
        }

        public void ReaderErrorMethod(PCSCException error)
        {
            string message = "Error: " + SCardHelper.StringifyError(error.SCardError);
            message += "\nReader có vấn đề. Xin kiểm tra lại";
            //notifyReader.BalloonTipText = message;
            //notifyReader.ShowBalloonTip(1000);

            DialogResult result = DialogResult.Retry;
            result = MessageBox.Show(message, "Error", MessageBoxButtons.RetryCancel);
            if (result == DialogResult.Retry)
            {
                readerThread = loadReader();
            }
            else if (result == DialogResult.Cancel)
            {
                readerThread.Abort();
                Application.Exit();
            }
           
        }

        public void DetectCardIDMethod(string cardID )
        {
            notifyReader.BalloonTipText = cardID;
            notifyReader.ShowBalloonTip(100);
            Clipboard.SetText(cardID);
            
            /*
            //Clear current input (current row)
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
            SendKeys.Send("{BS}");
            */
            
            //Then Ctrl + V
            SendKeys.Send("^V");


            
        }

        /// <summary>
        /// Control Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.ShowInTaskbar = false;
            }
        }

        private void notifyReader_MouseClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            readerThread.Abort();
        }
    }
}
