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
        Bitmap originalBitmap = new Bitmap(256, 256);

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
                Bitmap bitmap = new Bitmap(inputFile);
                OriginalImage.BackgroundImage = bitmap;

            }
        }

        private void EncodeBtn_Click(object sender, EventArgs e)
        {
            string predictionRule = PredictionRulesListBox.SelectedItem.ToString();
            int k = (int) KNumericUpDown.Value;
            string entropicCoder = StatisticModelListBox.SelectedItem.ToString();
            predictiveCoder.Encode(predictionRule, k, entropicCoder);
            Bitmap bitmap = predictiveCoder.GetBitmap(predictiveCoder.ErrorP);
            errorPictureBox.Image = bitmap;

            DrawHistogram();

        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            double scale = (double) ScaleNumeric.Value;
            int[,] matrix = ErrorMatrixListBox.SelectedIndex == 0 ? predictiveCoder.ErrorP : predictiveCoder.ErrorPq;
            int[,] scaledMatrix = predictiveCoder.ApplyScale(matrix, scale);
            Bitmap bitmap = predictiveCoder.GetBitmap(scaledMatrix);
            errorPictureBox.Image = bitmap;
        }

        private void ComputeErrorBtn_Click(object sender, EventArgs e)
        {
          
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

               /* Point p1 = new Point(255, 0);
                Point p2 = new Point(255, 255);
                g.DrawLine(Pens.DeepSkyBlue, p1, p2);*/
            }
        }

        private int[] GetFrequencies(int selectedIndex)
        {
            int[] frequencies = new int[511];

            if (selectedIndex == 0)
                frequencies = predictiveCoder.GetFrequencies(predictiveCoder.Original, frequencies);
            else if (selectedIndex == 1)
                frequencies = predictiveCoder.GetFrequencies(predictiveCoder.ErrorP, frequencies);
            else if (selectedIndex == 2)
                frequencies = predictiveCoder.GetFrequencies(predictiveCoder.ErrorPq, frequencies);
            else
                frequencies = predictiveCoder.GetFrequencies(predictiveCoder.Decoded, frequencies);
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
    }
}
