using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ACM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PredictionRulesListBox.SelectedIndex = 0;
            StatisticModelListBox.SelectedIndex = 0;
            ErrorMatrixListBox.SelectedIndex = 0;
            HistogramSourceListBox.SelectedIndex = 0;
        }

        PredictiveCoder predictiveCoder;
        PredictiveDecoder predictiveDecoder;

        private void LoadOriginalBtn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialog1.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                string inputFile = openFileDialog1.FileName;

                if (Path.GetExtension(inputFile) != ".bmp")
                {
                    MessageBox.Show("Selectati un fisier bmp");
                    return;
                }

                predictiveCoder = new PredictiveCoder(inputFile);
                predictiveCoder.LoadBmp();
                Bitmap bitmap = new Bitmap(inputFile);
                OriginalImage.BackgroundImage = bitmap;
            }
        }

        private void EncodeBtn_Click(object sender, EventArgs e)
        {
            if (predictiveCoder != null)
            {
                int predictionRule = PredictionRulesListBox.SelectedIndex;
                int k = (int) KNumericUpDown.Value;
                string entropicCoder = StatisticModelListBox.SelectedItem.ToString();
                predictiveCoder.Encode(predictionRule, k, entropicCoder);
                Bitmap bitmap = predictiveCoder.GetBitmap(predictiveCoder.ErrorP);
                errorPictureBox.Image = bitmap;
                DrawHistogram();
            }
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            if (predictiveCoder != null)
            {
                double scale = (double) ScaleNumeric.Value;
                int[,] matrix = ErrorMatrixListBox.SelectedIndex == 0 ? predictiveCoder.ErrorP : predictiveCoder.ErrorPq;
                int[,] scaledMatrix = predictiveCoder.ApplyScale(matrix, scale);
                Bitmap bitmap = predictiveCoder.GetBitmap(scaledMatrix);
                errorPictureBox.Image = bitmap;
            }
        }

        private void ComputeErrorBtn_Click(object sender, EventArgs e)
        {
            if (predictiveCoder != null && predictiveDecoder != null)
            {
                predictiveCoder.CalculateError(predictiveCoder.Original, predictiveDecoder.Decoded);
                MinLabel.Text = predictiveCoder.Min.ToString();
                MaxLabel.Text = predictiveCoder.Max.ToString();
            }
            else if (predictiveCoder != null)
            {
                predictiveCoder.CalculateError(predictiveCoder.Original, predictiveCoder.Decoded);
                MinLabel.Text = predictiveCoder.Min.ToString();
                MaxLabel.Text = predictiveCoder.Max.ToString();
            }
        }

        private void DragHistogramWithScale(int[] frequencies, float scale)
        {
            using (Graphics g = HistogramaPanel.CreateGraphics())
            {
                g.Clear(BackColor);

                for (int i = 0; i < frequencies.Length; i++)
                {
                    g.DrawLine(Pens.Black, i, 255, i, 255 - frequencies[i]*scale);
                }
            }
        }

        private int[] GetFrequencies(int selectedIndex)
        {
            int[] frequencies = new int[511];

            if (selectedIndex == 0)
            {
                if (predictiveCoder != null)
                    frequencies = predictiveCoder.GetFrequencies(predictiveCoder.Original, frequencies);
                else if (predictiveDecoder != null)
                    frequencies = predictiveDecoder.GetFrequencies(predictiveDecoder.Original, frequencies);
            }
            else if (selectedIndex == 1)
            {
                if (predictiveCoder != null)
                    frequencies = predictiveCoder.GetFrequencies(predictiveCoder.ErrorP, frequencies);
                else if (predictiveDecoder != null)
                    frequencies = predictiveDecoder.GetFrequencies(predictiveDecoder.ErrorP, frequencies);
            }
            else if (selectedIndex == 2)
            {
                if (predictiveCoder != null)
                    frequencies = predictiveCoder.GetFrequencies(predictiveCoder.ErrorPq, frequencies);
                else if (predictiveDecoder != null)
                    frequencies = predictiveDecoder.GetFrequencies(predictiveDecoder.ErrorPq, frequencies);
            }
            else
            {
                if (predictiveCoder != null)
                    frequencies = predictiveCoder.GetFrequencies(predictiveCoder.Decoded, frequencies);
                else if (predictiveDecoder != null)
                    frequencies = predictiveDecoder.GetFrequencies(predictiveDecoder.Decoded, frequencies);
            }

            return frequencies;
        }

        private void RefreshHistogramBtn_Click(object sender, EventArgs e)
        {
            DrawHistogram();
        }

        private void DrawHistogram()
        {
            int selectedIndex = HistogramSourceListBox.SelectedIndex;
            int[] frequencies = GetFrequencies(selectedIndex);
            float scale = (float) HistogramScaleNumeric.Value;
            DragHistogramWithScale(frequencies, scale);
        }

        private void SaveOriginalBtn_Click(object sender, EventArgs e)
        {
            if (predictiveCoder != null)
            {
                int index = StatisticModelListBox.SelectedIndex;
                predictiveCoder.SaveEncodedFile(index);
                MessageBox.Show("File was saved");
            }
        }

        private void LoadDecodedBtn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialog1.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                string inputFile = openFileDialog1.FileName;

                if (Path.GetExtension(inputFile) != ".prd")
                {
                    MessageBox.Show("Selectati un fisier .prd");
                    return;
                }

                predictiveDecoder = new PredictiveDecoder(inputFile);
                predictiveDecoder.LoadPrdFile();
                MessageBox.Show("Done loading");
            }
        }

        private void DecodeBtn_Click(object sender, EventArgs e)
        {
            if (predictiveDecoder != null)
            {
                predictiveDecoder.Decode();
                Bitmap bitmap = predictiveDecoder.GetBitmap(predictiveDecoder.Decoded);
                DecodedImage.BackgroundImage = bitmap;
                MessageBox.Show("Done decoding");
            }
        }

        private void SaveDecodedBtn_Click(object sender, EventArgs e)
        {
            if (predictiveDecoder != null)
            {
                predictiveDecoder.SaveDecodedFile();
                MessageBox.Show("Done saving");
            }
        }
    }
}
