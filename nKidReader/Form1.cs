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
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Expression.Encoder;
using Microsoft.Expression.Encoder.Devices;
using System.Collections.ObjectModel;

namespace nKidReader
{
    public partial class MainForm : Form
    {
        private Thread readerThread;
        public delegate void DetectCardID(String cardID);
        public DetectCardID delegateDetectCardID;

        public delegate void ReaderError(PCSCException error);
        public ReaderError delegateReaderError;

       
        IntPtr m_ip = IntPtr.Zero;
        private Capture cam;
        Collection<EncoderDevice> lstDevice;

        public MainForm()
        {
            InitializeComponent();
            //Delegate
            delegateDetectCardID = new DetectCardID(DetectCardIDMethod);
            delegateReaderError = new ReaderError(ReaderErrorMethod);
            //Load reader thread
            readerThread = loadReader();

            
        }

        private void loadCameraList()
        {
            cbCamList.Items.Clear();
            lstDevice = EncoderDevices.FindDevices(EncoderDeviceType.Video);
            for (int i = 0; i < lstDevice.Count; i++)
            {
                if(lstDevice[i].Name != "Screen Capture Source")
                    cbCamList.Items.Add(lstDevice[i].Name);
            }
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

        private void initCamera(int camIndex)
        {
            //int VIDEODEVICE = camIndex; // zero based index of video capture device to use
            const int VIDEOWIDTH = 640; // Depends on video device caps
            const int VIDEOHEIGHT = 480; // Depends on video device caps
            const int VIDEOBITSPERPIXEL = 24; // BitsPerPixel values determined by device
            cam = new Capture(camIndex, VIDEOWIDTH, VIDEOHEIGHT, VIDEOBITSPERPIXEL, pbCameraPreview);
        }

        private void captureImage(string cardID)
        {
            Cursor.Current = Cursors.WaitCursor;

            // Release any previous buffer
            if (m_ip != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(m_ip);
                m_ip = IntPtr.Zero;
            }

            // capture image

            m_ip = cam.Click();
            Bitmap b = new Bitmap(cam.Width, cam.Height, cam.Stride, PixelFormat.Format24bppRgb, m_ip);

            // If the image is upsidedown
            b.RotateFlip(RotateFlipType.RotateNoneFlipY);
            //pictureBox1.Image = b;
            Thread.Sleep(300);
            saveToFile(b, cardID);
            Cursor.Current = Cursors.Default;
        }

        private void saveToFile(Bitmap b, string cardID)
        {
            string path = @"D:\nKid";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var s = Path.Combine(path, cardID + @".jpg");
            s = Path.GetFullPath(s);
            b.Save(s, ImageFormat.Jpeg);
            b.Dispose(); 
        }

        public void DetectCardIDMethod(string cardID )
        {
            if (string.IsNullOrEmpty(cardID))
                return;
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

            //Capture image from webcam
            if (cam != null)
            {
                captureImage(cardID);
            }
            else
            {
                MessageBox.Show("Camera chưa sẵn sàng. Vui lòng thử lại sau", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                        
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
            if (m_ip != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(m_ip);
                m_ip = IntPtr.Zero;
            }
        }

        private void cbWcOn_CheckedChanged(object sender, EventArgs e)
        {
            if (cbWcOn.Checked)
            {
                cbCamList.Enabled = true;
                loadCameraList();
                cbCamList.SelectedIndex = 0;
                
            }
            else
            {
                
                cbCamList.Enabled = false;

                turnOffCamera();
            }
        }

        private void turnOffCamera()
        {
            cam.Dispose();
            cam = null;
        }

        private void cbCamList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cam != null)
            {
                turnOffCamera();
            }
            
            
            if (cbWcOn.Checked)
            {
                initCamera(cbCamList.SelectedIndex);                
            }
        }
    }
}
