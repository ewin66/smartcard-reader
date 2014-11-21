namespace nKidReader
{
    partial class frmAutoWriteCSV
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
            this.cbCardType = new System.Windows.Forms.ComboBox();
            this.txtIdPrefix = new System.Windows.Forms.TextBox();
            this.txtSuffix = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.notify1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.txtCardAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbNoti = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbCardType
            // 
            this.cbCardType.FormattingEnabled = true;
            this.cbCardType.Items.AddRange(new object[] {
            "VIP",
            "VIK"});
            this.cbCardType.Location = new System.Drawing.Point(108, 59);
            this.cbCardType.Name = "cbCardType";
            this.cbCardType.Size = new System.Drawing.Size(75, 21);
            this.cbCardType.TabIndex = 0;
            this.cbCardType.SelectedIndexChanged += new System.EventHandler(this.cbCardType_SelectedIndexChanged);
            // 
            // txtIdPrefix
            // 
            this.txtIdPrefix.Location = new System.Drawing.Point(108, 100);
            this.txtIdPrefix.Name = "txtIdPrefix";
            this.txtIdPrefix.ReadOnly = true;
            this.txtIdPrefix.Size = new System.Drawing.Size(75, 20);
            this.txtIdPrefix.TabIndex = 2;
            // 
            // txtSuffix
            // 
            this.txtSuffix.Location = new System.Drawing.Point(383, 100);
            this.txtSuffix.MaxLength = 8;
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Size = new System.Drawing.Size(100, 20);
            this.txtSuffix.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(225, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mã thẻ đầu tiên (8 chữ số sau)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mã loại thẻ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Loại";
            // 
            // notify1
            // 
            this.notify1.Text = "notifyIcon1";
            this.notify1.Visible = true;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(157, 138);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Text = "Xác nhận";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnReset
            // 
            this.btnReset.Enabled = false;
            this.btnReset.Location = new System.Drawing.Point(280, 138);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Làm lại";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // txtCardAmount
            // 
            this.txtCardAmount.Location = new System.Drawing.Point(383, 60);
            this.txtCardAmount.MaxLength = 8;
            this.txtCardAmount.Name = "txtCardAmount";
            this.txtCardAmount.Size = new System.Drawing.Size(100, 20);
            this.txtCardAmount.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(225, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Số thẻ trong lốc";
            // 
            // lbNoti
            // 
            this.lbNoti.AutoSize = true;
            this.lbNoti.ForeColor = System.Drawing.Color.Red;
            this.lbNoti.Location = new System.Drawing.Point(182, 25);
            this.lbNoti.Name = "lbNoti";
            this.lbNoti.Size = new System.Drawing.Size(116, 13);
            this.lbNoti.TabIndex = 8;
            this.lbNoti.Text = "Vui lòng nhập thông số";
            // 
            // frmAutoWriteCSV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 192);
            this.Controls.Add(this.lbNoti);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCardAmount);
            this.Controls.Add(this.txtSuffix);
            this.Controls.Add(this.txtIdPrefix);
            this.Controls.Add(this.cbCardType);
            this.Name = "frmAutoWriteCSV";
            this.Text = "Write Card Data";
            this.Load += new System.EventHandler(this.frmAutoWriteCSV_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbCardType;
        private System.Windows.Forms.TextBox txtIdPrefix;
        private System.Windows.Forms.TextBox txtSuffix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NotifyIcon notify1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TextBox txtCardAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbNoti;
    }
}