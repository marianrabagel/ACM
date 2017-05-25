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
                OriginalImagePanel.BackgroundImage = fractalCoder.GetBitmap();
            }
        }

        private void ProcessButton_Click(object sender, EventArgs e)
        {
            fractalCoder.Process(progressBar1);
        }
    }
}
