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

        WaveletCoder waveletCoder;
        WaveletDecoder waveletDecoder;

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

                waveletCoder = new WaveletCoder(256);
                waveletCoder.LoadFile(fileName);

                Bitmap bitmap = new Bitmap(fileName);
                OriginalImagePanel.BackgroundImage = bitmap;

            }
        }

        private void AnH1Button_Click(object sender, EventArgs e)
        {
            waveletCoder.AnH1();
            MessageBox.Show("done");
        }

        private void AnV1_Click(object sender, EventArgs e)
        {
            waveletCoder.AnV1();
            MessageBox.Show("done");
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            waveletDecoder = new WaveletDecoder(256);
            //to be replaced with data from file
            waveletDecoder.WaveletMatrix = waveletCoder.WaveletMatrix;
        }

        

        private void SyH1Button_Click(object sender, EventArgs e)
        {
            waveletDecoder.SyH1();
        }

        private void VisualizeWaveletButton_Click(object sender, EventArgs e)
        {
            waveletImage.BackgroundImage = waveletDecoder.GetBitmap();
        }

        private void AnH2Button_Click(object sender, EventArgs e)
        {
            waveletCoder.AnH2();
        }

        private void AnH3Button_Click(object sender, EventArgs e)
        {
            waveletCoder.AnH3();
        }

        private void AnH4Button_Click(object sender, EventArgs e)
        {
            waveletCoder.AnH4();
        }

        private void AnH5Button_Click(object sender, EventArgs e)
        {
            waveletCoder.AnH5();
        }

        private void AnV2Button_Click(object sender, EventArgs e)
        {
            waveletCoder.AnV2();
        }

        private void AnV3Button_Click(object sender, EventArgs e)
        {
            waveletCoder.AnV3();
        }

        private void AnV4Button_Click(object sender, EventArgs e)
        {
            waveletCoder.AnV4();
        }

        private void AnV5Button_Click(object sender, EventArgs e)
        {
            waveletCoder.AnV5();
        }

        private void AnalysisButton_Click(object sender, EventArgs e)
        {
            waveletCoder.AnH1();
            waveletCoder.AnV1();

            waveletCoder.AnH2();
            waveletCoder.AnV2();

            waveletCoder.AnH3();
            waveletCoder.AnV3();

            waveletCoder.AnH4();
            waveletCoder.AnV4();

            waveletCoder.AnH5();
            waveletCoder.AnV5();
        }

        private void SyH2Button_Click(object sender, EventArgs e)
        {
            waveletDecoder.SyH2();
        }

        private void SyH3Button_Click(object sender, EventArgs e)
        {
            waveletDecoder.SyH3();
        }

        private void SyH4Button_Click(object sender, EventArgs e)
        {
            waveletDecoder.SyH4();
        }

        private void SyH5Button_Click(object sender, EventArgs e)
        {
            waveletDecoder.SyH5();
        }

        private void SyV1Button_Click(object sender, EventArgs e)
        {
            waveletDecoder.SyV1();
        }
        private void SyV2Button_Click(object sender, EventArgs e)
        {
            waveletDecoder.SyV2();
        }

        private void SyV3Button_Click(object sender, EventArgs e)
        {
            waveletDecoder.SyV3();
        }

        private void SyV4Button_Click(object sender, EventArgs e)
        {
            waveletDecoder.SyV4();
        }

        private void SyV5Button_Click(object sender, EventArgs e)
        {
            waveletDecoder.SyV5();
        }
    }
}
