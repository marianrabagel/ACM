using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wavelet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Wavelet wavelet;

        private void LoadBmpButton_Click(object sender, EventArgs e)
        {
            var dialogResult = openFileDialog1.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;

                if (Path.GetExtension(fileName) != ".bmp")
                {
                    MessageBox.Show("Selectati un fisiser .bmp");
                    return;
                }

                wavelet = new Wavelet();
                wavelet.Decode(fileName);

                Bitmap bitmap = new Bitmap(fileName);
                OriginalImagePanel.BackgroundImage = bitmap;

            }
        }

        private void AnH1Button_Click(object sender, EventArgs e)
        {
            wavelet.AnH1();
            //waveletImage.BackgroundImage = 
        }
    }
}
