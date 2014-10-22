using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace nKidReader
{
    public partial class DisplayImage : Form
    {
        public DisplayImage()
        {
            InitializeComponent();
        }

        

        public DisplayImage(Bitmap bmpAva)
        {
            InitializeComponent();
            pictureBox1.Image = bmpAva;
            timer1.Start();
        }

        private void DisplayImage_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Close();
        }
    }
}
