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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inputFile = @"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\ArithmeticStatic.txt";
            string outputFile = @"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\ArithmeticStatic_output.txt";

            if (File.Exists(outputFile))
                File.Delete(outputFile);

            ArithmeticCoder arithmeticCoder = new ArithmeticCoder();
            arithmeticCoder.Encode(inputFile, outputFile);
            MessageBox.Show("done");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string outputFile = @"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\ArithmeticStatic_afterDEcode.txt";
            string inputFile = @"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\ArithmeticStatic_output.txt";

            if (File.Exists(outputFile))
                File.Delete(outputFile);

            ArithmeticDecoder arithmeticDecoder = new ArithmeticDecoder();
            arithmeticDecoder.Decode(inputFile, outputFile);
            MessageBox.Show("done");
        }
    }
}
