using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.IO;
using DirectShowLib;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Net;
using System.Configuration;
using RestSharp;
using Newtonsoft.Json.Linq;
using ReadWriteCsv;

namespace nKidReader
{
    public partial class MainForm : Form
    {
        IntPtr m_ip = IntPtr.Zero;
        private Capture cam;
        private List<DsDevice> availableVideoInputDevices;
        private DisplayImage frmDisplay = new DisplayImage();
        private NFCCardIdUpload frmNFCUpConfirm;
        private string ftpAddress = ConfigurationManager.AppSettings["ftpAddress"];
        private string ftpUsername = ConfigurationManager.AppSettings["ftpUsername"];
        private string ftpPassword = ConfigurationManager.AppSettings["ftpPassword"];
        private string ftpUploadFolder = ConfigurationManager.AppSettings["ftpUploadFolder"];
        private string ftpSyncFolder = ConfigurationManager.AppSettings["ftpSyncFolder"];
        private string csvFilePath = ConfigurationManager.AppSettings["csvFilePath"];
        private List<CsvData> cardList = new List<CsvData>();
        
       // string accessToken = "";
        ServiceHandle serviceHandle = new ServiceHandle();
        public MainForm()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue("nKid Reader", "\"" + Application.ExecutablePath + "\"");
            }
            InitializeComponent();
            frmNFCUpConfirm = new NFCCardIdUpload();
            //Load camera
            availableVideoInputDevices = new List<DsDevice>();
            getAvailableVideoInputDevices();

            //Load reader
            //Magnetic stripe reader: VID_6352 PID_213A
            //Semnox NFC reader: VID_08FF PID_0009
            //Default optText = 00000

            //This is demo, load mrs:
            scan("VID_6352", "PID_213A");
            // Or semnox :)
            scan("VID_08FF", "PID_0009");
            
            ReadCSVFile();
        }

        string nfcID = "";
        string magneticCardID = "";
        private void scan(string vid, string pid, string opt = "0000")
        {
            CardListener simpleUSBListener = new CardListener32();
            EventHandler currEventHandler = new EventHandler(CardScanCompleteEventHandle);
            bool flag = simpleUSBListener.InitializeUSBCardReader(this, currEventHandler, vid, pid, opt);
            if (flag == false)
            {
               // MessageBox.Show("Failure to find the magnetic stripe reader device", "Device Message");
                this.Close();
            }
        }

        private void ReadCSVFile()
        {

            if (!File.Exists(csvFilePath))
            {
                MessageBox.Show("Tập tin không tồn tại!");
                return;
            }
            // Read sample data from CSV file
            using (CsvFileReader reader = new CsvFileReader(csvFilePath))
            {
                CsvRow row = new CsvRow();
                
                while (reader.ReadRow(row))
                {
                    CsvData data = new CsvData(row[0], row[1]);
                    cardList.Add(data);
                }
            }
        }

        private void CardScanCompleteEventHandle(object sender, EventArgs e)
        {
            
            string cardNumber;
            CardListener listener = (CardListener)sender;
            if (e is CardReaderScannedEventArgs)
            {
                CardReaderScannedEventArgs checkScannedEvent = e as CardReaderScannedEventArgs;
                cardNumber = checkScannedEvent.Message;
                    // NFC
                if (listener.dInfo.deviceName.Contains("VID_08FF&PID_0009"))
                {
                    
                    if (updateNFCIDToolStripMenuItem.Checked)
                    {
                        
                        frmNFCUpConfirm.getNFCCode(nfcID);
                    }
                    else if (manualToolStripMenuItem.Checked)
                    {
                        bool foundMRS = false;
                        nfcID = cardNumber.ToLower();
                        foreach (CsvData data in cardList)
                        {
                            if (data.mrsId == magneticCardID)
                            {
                                data.nfcId = nfcID;
                                foundMRS = true;
                                break;
                            }

                        }

                        if (!foundMRS)
                        {
                            cardList.Add(new CsvData(magneticCardID, nfcID));
                              
                        }
                        // Write to CSV
                        using (CsvFileWriter writer = new CsvFileWriter(csvFilePath))
                        {
                            
                            for (int i = 0; i < cardList.Count; i++)
                            {
                                CsvRow row = new CsvRow();
                                row.Add(cardList[i].mrsId);
                                row.Add(cardList[i].nfcId);
                                
                                writer.WriteRow(row);
                            }
                        }
                    }
                }
                    // MRS
                else if (listener.dInfo.deviceName.Contains("VID_6352&PID_213A"))
                {
                    magneticCardID = cardNumber;
                    if (updateNFCIDToolStripMenuItem.Checked)
                    {

                        if (frmNFCUpConfirm.Visible)
                        {
                            frmNFCUpConfirm.Close();
                        }
                        

                        string result = serviceHandle.makeRequest(magneticCardID);

                        if (result == "NFC not found")
                        {

                            frmNFCUpConfirm = new NFCCardIdUpload(magneticCardID);
                            frmNFCUpConfirm.BackColor = Color.FromArgb(255, 211, 183);
                            frmNFCUpConfirm.passControl = new NFCCardIdUpload.PassControl(serviceHandle.uploadNFCCode);
                            frmNFCUpConfirm.Show();

                        }
                    }
                }
                detectCardIDMethod(cardNumber);
            }
        }

        
        private void getAvailableVideoInputDevices()
        {
            DsDevice[] videoInputDevices =
                DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            availableVideoInputDevices.AddRange(videoInputDevices);
        }

        private void loadCameraList()
        {
            cbCamList.Items.Clear();
            for (int i = 0; i < availableVideoInputDevices.Count; i++)
            {
                if (availableVideoInputDevices[i].Name != "Screen Capture Source")
                    cbCamList.Items.Add(availableVideoInputDevices[i].Name);
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
            string path = @"D:\nKid\upload";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var s = Path.Combine(path, cardID + @".jpg");
            s = Path.GetFullPath(s);
            b.Save(s, ImageFormat.Jpeg);

            UploadAvatar(s.ToString());
            b.Dispose(); 
        }

        
        private void UploadAvatar(string source)
        {
            //string filename = Path.GetFileName(source);
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create
                    (ftpAddress + "/" + ftpUploadFolder + "/" + Path.GetFileName(source));
                request.Method = WebRequestMethods.Ftp.UploadFile;

                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                request.UsePassive = true;
                StreamReader sourceStream = new StreamReader(source);
                byte[] imageBuffer = File.ReadAllBytes(source);
                sourceStream.Close();
                request.ContentLength = imageBuffer.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(imageBuffer, 0, imageBuffer.Length);
                requestStream.Close();

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                MessageBox.Show("Upload thành công", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Upload lỗi: " + e.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void detectCardIDMethod(string cardID )
        {
            if (frmDisplay.Visible)
            {
                frmDisplay.Close();
            }
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
            //SendKeys.Send("^V");

            //Capture image from webcam
            if (chkWcOn.Checked)
            {
                if (enableUploadingAvatarToolStripMenuItem.Checked)
                {
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
            }
            else
            {
                if (showAvatarToolStripMenuItem.Checked)
                {
                    Bitmap bmp = GetAvatar("default.jpg");

                    frmDisplay = new DisplayImage(bmp);
                    frmDisplay.Show();
                }
                
            }
            
        }

        
        private Bitmap GetAvatar(string fileName)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create
                    (ftpAddress + "/" + ftpSyncFolder + "/" + fileName);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.UsePassive = true;
                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                
                
                Bitmap bmp = new Bitmap(responseStream);
                bmp.Save(responseStream, ImageFormat.Jpeg);
                
                
                responseStream.Close();
                response.Close();
                return bmp;
                
            }
            catch (NotSupportedException)
            {
                return GetAvatar("default.jpg"); 
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
        {/*
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;*/
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_ip != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(m_ip);
                m_ip = IntPtr.Zero;
            }
        }

        private void cbWcOn_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWcOn.Checked)
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
            
            
            if (chkWcOn.Checked)
            {
                initCamera(cbCamList.SelectedIndex);                
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void writeDataToCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void showAvatarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manualToolStripMenuItem.Checked = false;
            showAvatarToolStripMenuItem.Checked = true;
            updateNFCIDToolStripMenuItem.Checked = false;
            enableUploadingAvatarToolStripMenuItem.Checked = false;
            chkWcOn.Checked = false;
        }

        private void updateNFCIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manualToolStripMenuItem.Checked = false;
            showAvatarToolStripMenuItem.Checked = false;
            updateNFCIDToolStripMenuItem.Checked = true;
            enableUploadingAvatarToolStripMenuItem.Checked = false;
            chkWcOn.Checked = false;
        }

        private void enableUploadingAvatarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manualToolStripMenuItem.Checked = false;
            showAvatarToolStripMenuItem.Checked = false;
            updateNFCIDToolStripMenuItem.Checked = false;
            enableUploadingAvatarToolStripMenuItem.Checked = true;
            chkWcOn.Checked = true;
        }

        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manualToolStripMenuItem.Checked = true;
            showAvatarToolStripMenuItem.Checked = false;
            updateNFCIDToolStripMenuItem.Checked = false;
            enableUploadingAvatarToolStripMenuItem.Checked = false;
            chkWcOn.Checked = false;
        }

        private void automaticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAutoWriteCSV frm = new frmAutoWriteCSV("VID_08FF", "PID_0009");
            frm.Show();
            
        }
    }
}
