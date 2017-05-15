
using System;
using System.Drawing;
using ACM;

namespace Wavelet
{
    public class WaveletBase
    {
        protected byte[] BmpHeader;
        protected byte[,] Original;
        public double[,] WaveletMatrix;
        public int Size { get; } = 256;

        public WaveletBase(int size)
        {
            Size = size;
            BmpHeader = new byte[1078];
            Original = new byte[Size, Size];
            WaveletMatrix = new double[Size, Size];
        }

        public void ReadBmpHeader(BitReader reader)
        {
            for (int i = 0; i < 1078; i++)
                BmpHeader[i] = (byte)reader.ReadNBit(8);
        }

        protected double[] ApplyCuantizorVertical(int column, int length, double[] cuantizor, double[,] source)
        {
            double[] result = new double[length];

            for (int x = 0; x < length - 4; x++)
            {
                double sum = 0;
                for (int i = 0; i < 9; i++)
                {
                    int index = Math.Abs(x + i - 4);
                    sum += source[index, column] * cuantizor[i];
                }
                result[x] = sum;
            }

            for (int x = length - 4; x < length; x++)
            {
                double sum = 0;
                for (int i = 0; i < 9; i++)
                {
                    int index = Math.Abs(x + i - 4);

                    if (index > length - 1)
                        index = length - 1 - (index % (length - 1));
                    sum += source[index, column] * cuantizor[i];
                }
                result[x] = sum;
            }

            return result;
        }

        /// <summary>
        /// For coder. For the first time, when it applys Cuantizor on the original image (which is byte)
        /// </summary>
        protected double[] ApplyCuantizorHorizontal(int line, int length, double[] cuantizor, double[,] source)
        {
            double[] result = new double[length];

            for (int x = 0; x < length - 4; x++)
            {
                double sum = 0;
                for (int i = 0; i < 9; i++)
                {
                    int index = Math.Abs(x + i - 4);
                    sum += source[line, index] * cuantizor[i];
                }
                result[x] = sum;
            }

            for (int x = length - 4; x < length; x++)
            {
                double sum = 0;
                for (int i = 0; i < 9; i++)
                {
                    int index = Math.Abs(x + i - 4);

                    if (index > length - 1)
                        index = length - 1 - (index % (length - 1));
                    sum += source[line, index] * cuantizor[i];
                }
                result[x] = sum;
            }

            return result;
        }

        /// <summary>
        /// For decoder. It applies the syntesis to one line, not on the matrix
        /// </summary>
        protected double[] ApplyCuantizor(int length, double[] cuantizor, double[] source)
        {
            double[] result = new double[length];

            for (int x = 0; x < length - 4; x++)
            {
                double sum = 0;
                for (int i = 0; i < 9; i++)
                {
                    int index = Math.Abs(x + i - 4);
                    sum += source[index] * cuantizor[i];
                }
                result[x] = sum;
            }

            for (int x = length - 4; x < length; x++)
            {
                double sum = 0;
                for (int i = 0; i < 9; i++)
                {
                    int index = Math.Abs(x + i - 4);

                    if (index > length - 1)
                        index = length - 1 - (index % (length - 1));
                    sum += source[index] * cuantizor[i];
                }
                result[x] = sum;
            }

            return result;
        }

        public Bitmap GetBitmap()
        {
            double[,] matrix = WaveletMatrix;
            Bitmap bitmap = new Bitmap(matrix.GetLength(1), matrix.GetLength(0));

            for (int y = 0; y < matrix.GetLength(1); y++)
            {
                for (int x = 0; x < matrix.GetLength(0); x++)
                {
                    int value = Convert.ToInt32(Math.Round(matrix[y, x]));

                    if (value < 0)
                        value = 0;
                    if (value > 255)
                        value = 255;

                    Color color = Color.FromArgb(255, value, value, value);
                    bitmap.SetPixel(x, y, color);
                }
            }

            return bitmap;
        }
    }
}
