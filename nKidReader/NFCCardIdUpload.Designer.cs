using System.Drawing;
using System.Windows.Forms;
namespace nKidReader
{
    partial class NFCCardIdUpload
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lbNFC = new System.Windows.Forms.Label();
            this.lbMRS = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(356, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Không có NFC trong dữ liệu. Bạn có muốn cập nhật?";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(59, 104);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 41);
            this.button1.TabIndex = 1;
            this.button1.Text = "Thêm vào thẻ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(240, 104);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 41);
            this.button2.TabIndex = 1;
            this.button2.Text = "Hủy";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lbNFC
            // 
            this.lbNFC.AutoSize = true;
            this.lbNFC.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNFC.ForeColor = System.Drawing.Color.Green;
            this.lbNFC.Location = new System.Drawing.Point(36, 11);
            this.lbNFC.Name = "lbNFC";
            this.lbNFC.Size = new System.Drawing.Size(52, 18);
            this.lbNFC.TabIndex = 0;
            this.lbNFC.Text = "NFC: ";
            // 
            // lbMRS
            // 
            this.lbMRS.AutoSize = true;
            this.lbMRS.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMRS.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbMRS.Location = new System.Drawing.Point(36, 42);
            this.lbMRS.Name = "lbMRS";
            this.lbMRS.Size = new System.Drawing.Size(55, 18);
            this.lbMRS.TabIndex = 0;
            this.lbMRS.Text = "MRS: ";
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // NFCCardIdUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(211)))), ((int)(((byte)(183)))));
            this.ClientSize = new System.Drawing.Size(445, 163);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbMRS);
            this.Controls.Add(this.lbNFC);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, 
                Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            this.Name = "NFCCardIdUpload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NFC Card Upload";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lbNFC;
        private System.Windows.Forms.Label lbMRS;
        private System.Windows.Forms.Timer timer1;
    }
}