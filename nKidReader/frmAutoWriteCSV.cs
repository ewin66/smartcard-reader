using ReadWriteCsv;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace nKidReader
{
    public partial class frmAutoWriteCSV : Form
    {
        private string vid;
        private string pid;
        private string nfcID;
        private List<CsvData> cardList = new List<CsvData>();
        private string csvFilePath = "";
        private string prefixID;
        private string suffixIDStart;
        public frmAutoWriteCSV()
        {
            InitializeComponent();
        }


        public frmAutoWriteCSV(string vid, string pid)
        {
            InitializeComponent();
            this.vid = vid;
            this.pid = pid;

            CardListener simpleUSBListener = new CardListener32();
            EventHandler currEventHandler = new EventHandler(CardScanCompleteEventHandle);
            bool flag = simpleUSBListener.InitializeUSBCardReader(this, currEventHandler, this.vid, this.pid, "0000");
            if (flag == false)
            {
                this.Close();
            }
            cbCardType.SelectedIndex = 0;

           // ReadCSVFile();
        }

        public void detectCardIDMethod(string cardID)
        {
            
            if (string.IsNullOrEmpty(cardID))
                return;
            notify1.BalloonTipText = cardID;
            notify1.ShowBalloonTip(100);
            Clipboard.SetText(cardID);


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
                if (listener.dInfo.deviceName.Contains("VID_08FF&PID_0009") && btnReset.Enabled)
                {
                    nfcID = cardNumber.ToLower();

                    if (cardList.Count == 0)
                    {
                        string magneticCardID = this.prefixID + this.suffixIDStart;
                        cardList.Add(new CsvData(magneticCardID, nfcID));
                    }
                    else
                    {  
                        string idSuffix = String.Format("{0:00000000}", (int.Parse(cardList[cardList.Count - 1].mrsId.Substring(4, 8)) + 1));
                        string magneticCardID = this.prefixID + idSuffix;
                   
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
                detectCardIDMethod(nfcID);
            }
            
        }

        private void ReadCSVFile()
        {

            if (!File.Exists(csvFilePath))
            {
                MessageBox.Show("Tập tin chưa có!");
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
        private void frmAutoWriteCSV_Load(object sender, EventArgs e)
        {

        }

        private void cbCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCardType.SelectedItem.ToString() == "VIK")
            {
                txtIdPrefix.Text = "2000";
            }
            else if (cbCardType.SelectedItem.ToString() == "VIP")
            {
                txtIdPrefix.Text = "3000";
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtCardAmount.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số thẻ trong lốc");
                txtCardAmount.Focus();
                return;
            }

            if (txtSuffix.Text.Length == 0 || txtSuffix.Text.Length < 8)
            {
                MessageBox.Show("Vui lòng nhập 8 ký tự cuối của mã thẻ đầu tiên");
                txtCardAmount.Focus();
                return;
            }

            
            prefixID = txtIdPrefix.Text;
            suffixIDStart = txtSuffix.Text;

            cbCardType.Enabled = false;
            txtSuffix.Enabled = false;
            txtCardAmount.Enabled = false;
            btnReset.Enabled = true;
            btnSubmit.Enabled = false;
            lbNoti.Text = "Xin mời quét thẻ";
            lbNoti.ForeColor = Color.Green;

            string suffixIDEnd = String.Format("{0:00000000}", 
                (int.Parse(suffixIDStart) + int.Parse(txtCardAmount.Text) - 1));

            csvFilePath = @"D:\nKid\" + cbCardType.SelectedItem.ToString() 
                + "_" + suffixIDStart + "_" + suffixIDEnd + ".csv";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cbCardType.Enabled = true;
            txtSuffix.Enabled = true;
            txtCardAmount.Enabled = true;
            btnReset.Enabled = false;
            btnSubmit.Enabled = true;

            lbNoti.Text = "Vui lòng nhập thông số";
            lbNoti.ForeColor = Color.Red;

            cardList.Clear();
        }
    }
}
