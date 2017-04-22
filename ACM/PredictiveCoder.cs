using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ACM
{
    public class PredictiveCoder : PredictiveBase
    {
        public PredictiveCoder(string inputFile) : base(inputFile)
        {
            
        }

        public void Encode(string predictionRule, int k, string entropicCoder)
        {
            outputFileName = inputFileName + ".p" + predictionRule + "k" + k + entropicCoder + ".prd";
            SaveBmpToMemory();

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    byte predictionValue = GetPredictionFor(predictionRule, x, y);
                    Prediction[y, x] = predictionValue;
                    ErrorP[y, x] = Original[y, x] - predictionValue;
                    ErrorPq[y, x] = Convert.ToInt32(Math.Floor((ErrorP[y, x] + k) / (double)(2 * k + 1))); 
                    ErrorPdq[y, x] = ErrorPq[y, x] * (2 * k + 1);
                    Decoded[y, x] = (byte) (ErrorPdq[y, x] + Prediction[y, x]);
                }
            }
        }

        private void SaveBmpToMemory()
        {
            using (BitReader reader = new BitReader(inputFileName))
            {
                for (int i = 0; i < 1078; i++)
                    bmpHeader[i] = (byte) reader.ReadNBit(8);

                for (int y = Original.GetLength(0) - 1; y > 0; y--)
                {
                    for (int x = 0; x < Original.GetLength(1); x++)
                    {
                        Original[y, x] = (byte) reader.ReadNBit(8);
                    }
                }
            }
        }

        public Bitmap GetBitmap(byte[,] matrix)
        {
            Bitmap bitmap = new Bitmap(matrix.GetLength(1), matrix.GetLength(0));

            for (int y = 0; y < matrix.GetLength(1); y++)
            {
                for (int x = 0; x < matrix.GetLength(0); x++)
                {
                    byte value = matrix[y, x];
                    Color color = Color.FromArgb(255, value, value, value);
                    bitmap.SetPixel(x, y, color);
                }
            }

            return bitmap;
        }

        public Bitmap GetBitmap(int[,] matrix)
        {
            Bitmap bitmap = new Bitmap(matrix.GetLength(1), matrix.GetLength(0));

            for (int y = 0; y < matrix.GetLength(1); y++)
            {
                for (int x = 0; x < matrix.GetLength(0); x++)
                {
                    int value = matrix[y, x];

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

        public int[,] ApplyScale(int[,] matrix, double scale)
        {
            int[,] newMatrix = new int[matrix.GetLength(1), matrix.GetLength(0)];

            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    newMatrix[y, x] = (int) (matrix[y, x] * scale);
                }
            }

            return newMatrix;
        }

        public int[] GetFrequencies(byte[,] matrix, int[] frequencies)
        {
            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    int indexValue = matrix[y, x] + 255;
                    frequencies[indexValue]++;
                }
            }

            return frequencies;
        }

        public int[] GetFrequencies(int[,] matrix, int[] frequencies)
        {
            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    int indexValue = matrix[y, x] + 255;
                    frequencies[indexValue]++;
                }
            }

            return frequencies;
        }

        public void SaveEncodedFile(int statisticModelIndex)
        {
            if (statisticModelIndex == 0)
            {
                using (FileStream fileStream = new FileStream(outputFileName, FileMode.Create))
                    fileStream.Write(bmpHeader,0, bmpHeader.Length);

                using (BitWriter writer = new BitWriter(outputFileName))
                {
                    for (int y = 0; y < ErrorP.GetLength(0); y++)
                    {
                        for (int x = 0; x < ErrorP.GetLength(1); x++)
                        {
                            uint value = Convert.ToUInt32(ErrorP[y,x] + 255);
                            writer.WriteNBiti(value, 9);
                        }
                    }
                }
            }
        }
    }
}
