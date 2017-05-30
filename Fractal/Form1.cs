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

namespace Fractal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        FractalCoder fractalCoder;

        private void LoadBmpButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                if (Path.GetExtension(fileName).ToUpper() != ".BMP")
                {
                    MessageBox.Show("selectati un fisier bmp");
                    return;
                }

                fractalCoder = new FractalCoder();
                fractalCoder.LoadBmpFile(fileName);
                OriginalImagePanel.BackgroundImage = fractalCoder.GetBitmap(fractalCoder.Original);
            }
        }

        private void ProcessButton_Click(object sender, EventArgs e)
        {
            fractalCoder.Process(progressBar1);
        }

        FractalDecoder fractalDecoder;

        private void LoadInitialButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(openFileDialog1.FileName).ToUpper() != ".BMP")
                {
                    MessageBox.Show("selectati un fisier bmp");
                    return;
                }

                fractalDecoder = new FractalDecoder();
                fractalDecoder.LoadInitialBmp(openFileDialog1.FileName);

                var bitmap = fractalDecoder.GetBitmap();
                DecodedImagePanel.BackgroundImage = bitmap;
            }
        }

        private void LoadFrcButton_Click(object sender, EventArgs e)
        {
            if (fractalDecoder != null)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (Path.GetExtension(openFileDialog1.FileName).ToUpper() != ".FRC")
                    {
                        MessageBox.Show("Selectati un fisier .prc");
                        return;
                    }

                    fractalDecoder.LoadFrcFile(openFileDialog1.FileName);
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            fractalCoder.Save();
        }

        private void DecodeNbStepsButton_Click(object sender, EventArgs e)
        {
            fractalDecoder.Decode();
            var bitmap = fractalDecoder.GetBitmap();
            DecodedImagePanel.BackgroundImage = bitmap;
        }

        private void OriginalImagePanel_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseEventArgs = ((MouseEventArgs) e);
            int x = mouseEventArgs.X;
            int y = mouseEventArgs.Y;

            byte[,] border = fractalCoder.DrawBorder(x, y);
            var bitmap = fractalCoder.GetBitmap(border);
            OriginalImagePanel.BackgroundImage = bitmap;
        }
    }
}
