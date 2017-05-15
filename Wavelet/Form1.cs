using System;
using System.Drawing;
using System.IO;
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
        }

        private void AnV1_Click(object sender, EventArgs e)
        {
            waveletCoder.AnV1();
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
            double scale = (double) ScaleNumericUpDown.Value;
            int offset = (int) OffsetNumericUpDown.Value;
            int x = (int) XNumericUpDown.Value;
            int y = (int) YNumericUpDown.Value;
            waveletDecoder.ApplyScale(scale, offset, x, y);
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

            var iterations = Convert.ToInt32(LevelesNumericUpDown.Value);
            for (int i = 1; i < iterations; i++)
            {
                waveletCoder.AnalysisHorizontal((int) (waveletCoder.Size/Math.Pow(2, i)));
                waveletCoder.AnalysisVertical((int)(waveletCoder.Size / Math.Pow(2, i)));
            }
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

        private void SynthesisButton_Click(object sender, EventArgs e)
        {
            var iterations = Convert.ToInt32(LevelesNumericUpDown.Value);
            for (int i = iterations-1; i >= 0; i--)
            {
                var size = (int)(waveletDecoder.Size / Math.Pow(2, i));
                waveletDecoder.SynthesysVertical(size);
                waveletDecoder.SynthesisHorizontal(size);
            }
        }

        private void TestErrorButton_Click(object sender, EventArgs e)
        {
            Calculate();
            MinValueLabel.Text = min.ToString();
            MaxValueLabel.Text = max.ToString();
        }
        
        public double min = int.MaxValue;
        public double max = int.MinValue;

        public void Calculate()
        {
            for (int y = 0; y < waveletDecoder.Size; y++)
            {
                for (int x = 0; x < waveletDecoder.Size; x++)
                {
                    var val = waveletCoder.Original[y, x] - Math.Round(waveletDecoder.WaveletMatrix[y, x]);
                    if (val < min)
                        min = val;
                    if (val > max)
                        max = val;
                }
            }
        }
    }
}
