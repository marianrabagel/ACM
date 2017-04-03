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

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialog1.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                string inputFile = openFileDialog1.FileName;
                //string inputFile = @"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\ArithmeticStatic.txt";
                string outputFile = Path.GetDirectoryName(inputFile) + "/" + Path.GetFileName(inputFile) + ".coded";
                   

                if (File.Exists(outputFile))
                    File.Delete(outputFile);

                ArithmeticCoder arithmeticCoder = new ArithmeticCoder();
                arithmeticCoder.Encode(inputFile, outputFile);
                MessageBox.Show("done");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialog1.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                string inputFile = openFileDialog1.FileName;
                string outputFile = Path.GetDirectoryName(inputFile) + "/" + Path.GetFileNameWithoutExtension(inputFile) +
                                    ".decoded.txt";
                //string inputFile =@"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\ArithmeticStatic_output.txt";

                if (File.Exists(outputFile))
                    File.Delete(outputFile);

                ArithmeticDecoder arithmeticDecoder = new ArithmeticDecoder();
                arithmeticDecoder.Decode(inputFile, outputFile);
                MessageBox.Show("done");
            }
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
            int selectedIndex = HistogramSourceListBox.SelectedIndex;
            int[] frequencies = GetFrequencies(selectedIndex);
            float scale = (float) HistogramScaleNumeric.Value;
            DragHistogramWithScale(frequencies, scale);
        }

        private void DragHistogramWithScale(int[] frequencies, float scale)
        {
            using (Graphics g = HistogramaPanel.CreateGraphics())
            {
                g.Clear(BackColor);

                for (int i = 0; i < frequencies.Length; i++)
                {
                    if (frequencies[i] != 0)
                        g.DrawLine(Pens.Black, i, 255, i, (255 - frequencies[i]) * scale);
                }

                Point p1 = new Point(255, 0);
                Point p2 = new Point(255, 255);
                g.DrawLine(Pens.DeepSkyBlue, p1, p2);
            }
        }

        private int[] GetFrequencies(int selectedIndex)
        {
            int[] frequencies;

            if (selectedIndex == 0)
                frequencies = predictiveCoder.GetFrequencies(predictiveCoder.Original);
            else if (selectedIndex == 1)
                frequencies = predictiveCoder.GetFrequencies(predictiveCoder.ErrorP);
            else if (selectedIndex == 2)
                frequencies = predictiveCoder.GetFrequencies(predictiveCoder.ErrorPq);
            else
                frequencies = predictiveCoder.GetFrequencies(predictiveCoder.Decoded);
            return frequencies;
        }

        private void RefreshHistogramBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = HistogramSourceListBox.SelectedIndex;
            int[] frequencies = GetFrequencies(selectedIndex);
            float scale = (float)HistogramScaleNumeric.Value;
            DragHistogramWithScale(frequencies, scale);
        }
    }
}
