﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ACM;

namespace Wavelet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        WaveletBase waveletBase;
        //WaveletDecoder waveletDecoder;
        
        private void LoadBmpButton_Click(object sender, EventArgs e)
        {
            var dialogResult = openFileDialog1.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;

                if (Path.GetExtension(fileName).ToUpper() != ".BMP")
                {
                    MessageBox.Show("Selectati un fisiser .bmp");
                    return;
                }

                waveletBase = new WaveletBase(512);
                waveletBase.LoadBmpFile(fileName);
                Bitmap bitmap = waveletBase.GetBitmap();
                OriginalImagePanel.BackgroundImage = bitmap;
            }
        }
        
        private void AnV1_Click(object sender, EventArgs e)
        {
            waveletBase.AnV1();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string inputFileName = openFileDialog1.FileName;

                if (Path.GetExtension(inputFileName) != ".wvm")
                {
                    MessageBox.Show("Selectati un fisier cu extensia .wvm");
                    return;
                }

                waveletBase = new WaveletBase(512);
                waveletBase.LoadWvmFile(inputFileName);
                //zo be replaced with data from file
                //waveletBase.WaveletMatrix = waveletBase.WaveletMatrix;
            }
        }

       

        private void VisualizeWaveletButton_Click(object sender, EventArgs e)
        {
            double scale = (double) ScaleNumericUpDown.Value;
            int offset = (int) OffsetNumericUpDown.Value;
            int x = (int) XNumericUpDown.Value;
            int y = (int) YNumericUpDown.Value;
            waveletBase.ApplyScale(scale, offset, x, y);
            waveletImage.BackgroundImage = waveletBase.GetBitmap();
        }

        private void AnH1Button_Click(object sender, EventArgs e)
        {
            waveletBase.AnH1();
        }

        private void AnH2Button_Click(object sender, EventArgs e)
        {
            waveletBase.AnH2();
        }

        private void AnH3Button_Click(object sender, EventArgs e)
        {
            waveletBase.AnH3();
        }

        private void AnH4Button_Click(object sender, EventArgs e)
        {
            waveletBase.AnH4();
        }

        private void AnH5Button_Click(object sender, EventArgs e)
        {
            waveletBase.AnH5();
        }

        private void AnV2Button_Click(object sender, EventArgs e)
        {
            waveletBase.AnV2();
        }

        private void AnV3Button_Click(object sender, EventArgs e)
        {
            waveletBase.AnV3();
        }

        private void AnV4Button_Click(object sender, EventArgs e)
        {
            waveletBase.AnV4();
        }

        private void AnV5Button_Click(object sender, EventArgs e)
        {
            waveletBase.AnV5();
        }

        private void AnalysisButton_Click(object sender, EventArgs e)
        {
            var iterations = Convert.ToInt32(LevelesNumericUpDown.Value);
            for (int i = 0; i < iterations; i++)
            {
                waveletBase.AnalysisHorizontal((int) (waveletBase.Size/Math.Pow(2, i)));
                waveletBase.AnalysisVertical((int) (waveletBase.Size/Math.Pow(2, i)));
            }
        }

        private void SyH1Button_Click(object sender, EventArgs e)
        {
            waveletBase.SyH1();
        }

        private void SyH2Button_Click(object sender, EventArgs e)
        {
            waveletBase.SyH2();
        }

        private void SyH3Button_Click(object sender, EventArgs e)
        {
            waveletBase.SyH3();
        }

        private void SyH4Button_Click(object sender, EventArgs e)
        {
            waveletBase.SyH4();
        }

        private void SyH5Button_Click(object sender, EventArgs e)
        {
            waveletBase.SyH5();
        }

        private void SyV1Button_Click(object sender, EventArgs e)
        {
            waveletBase.SyV1();
        }
        private void SyV2Button_Click(object sender, EventArgs e)
        {
            waveletBase.SyV2();
        }

        private void SyV3Button_Click(object sender, EventArgs e)
        {
            waveletBase.SyV3();
        }

        private void SyV4Button_Click(object sender, EventArgs e)
        {
            waveletBase.SyV4();
        }

        private void SyV5Button_Click(object sender, EventArgs e)
        {
            waveletBase.SyV5();
        }

        private void SynthesisButton_Click(object sender, EventArgs e)
        {
            var iterations = Convert.ToInt32(LevelesNumericUpDown.Value);
            for (int i = iterations-1; i >= 0; i--)
            {
                var size = (int)(waveletBase.Size / Math.Pow(2, i));
                waveletBase.SynthesysVertical(size);
                waveletBase.SynthesisHorizontal(size);
            }
        }

        private void TestErrorButton_Click(object sender, EventArgs e)
        {
            waveletBase.CalculateMinMax();
            MinValueLabel.Text = waveletBase.min.ToString();
            MaxValueLabel.Text = waveletBase.max.ToString();
        }
        
       /* public void Calculate()
        {
            for (int y = 0; y < waveletDecoder.Size; y++)
            {
                for (int x = 0; x < waveletDecoder.Size; x++)
                {
                    var val = waveletBase.Original[y, x] - Math.Round(waveletDecoder.WaveletMatrix[y, x]);
                    if (val < min)
                        min = val;
                    if (val > max)
                        max = val;
                }
            }
        }*/

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string outputFileName = waveletBase.OutputFile;
            using (StreamWriter writer = new StreamWriter(outputFileName))
            {
                for (int y = 0; y < waveletBase.Size; y++)
                {
                    for (int x = 0; x < waveletBase.Size; x++)
                    {
                        writer.WriteLine(waveletBase.WaveletMatrix[y,x]);
                    }
                }
            }
        }
    }
}
