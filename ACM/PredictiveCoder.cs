using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ACM
{
    public class PredictiveCoder
    {
        public string inputFile;
        string outputFile;
        byte[] bmpHeader;
        public byte[,] Original { get; }
        public byte[,] Prediction { get; }
        public byte[,] Decoded { get; }
        public int[,] ErrorP { get; }
        public int[,] ErrorPq { get; }
        public int[,] ErrorPdq { get; }
        public int[,] Error { get; }
        int size = 256;

        public PredictiveCoder(string inputFile)
        {
            this.inputFile = inputFile;
            bmpHeader = new byte[1078];
            Original = new byte[size, size];
            Prediction = new byte[size, size];
            Decoded = new byte[size, size];
            ErrorP = new int[size, size];
            ErrorPq = new int[size, size];
            ErrorPdq = new int[size, size];
            Error = new int[size, size];
        }

        public void Encode(string predictionRule, int k, string entropicCoder)
        {
            outputFile = inputFile + ".p" + predictionRule + "k" + k + entropicCoder + ".prd";
            SaveBmpToMemory();

            for (int y = 0; y < Original.GetLength(0); y++)
            {
                for (int x = 0; x < Original.GetLength(1); x++)
                {
                    byte predictionValue = GetPredictionFor(predictionRule, x, y);
                    Prediction[y, x] = predictionValue;
                    ErrorP[y, x] = Original[y, x] - predictionValue;
                    ErrorPq[y, x] = Convert.ToInt32(Math.Floor((double) ((ErrorP[y, x] + k) / (2 * k + 1)))); 
                    ErrorPdq[y, x] = ErrorPq[y, x] * (2 * k + 1);
                    Decoded[y, x] = (byte) (ErrorPdq[y, x] + Prediction[y, x]);
                }
            }

        }

        private byte GetPredictionFor(string predictionRule, int x, int y)
        {
            if (x == 0 && y == 0)
                return 128;

            byte value = 128;

            if (predictionRule == "128")
            {
                value = 128;
            }
            else if (predictionRule == "A")
            {
                value = GetA(x, y);
            }
            else if (predictionRule == "B")
            {
                value = GetB(x, y);
            }
            else if (predictionRule == "C")
            {
                value = GetC(x, y);
            }
            else if (predictionRule == "A+B-C")
            {
                value = GetABC(x, y);
            }
            else if (predictionRule == "A+(B-C)/2")
            {
                value = GetABC2(x, y);
            }
            else if (predictionRule == "B+(A-C)/2")
            {
                value = GetBAC2(x, y);
            }
            else if (predictionRule == "(A+B)/2")
            {
                value = GetAB2(x, y);
            }
            else if (predictionRule == "JPEG-LS")
            {
                value = GetJpegLs(x, y);
            }

            return value;
        }

        private byte GetJpegLs(int x, int y)
        {
            byte value;

            if (x == 0)
                value = GetA(x, y);
            else if (y == 0)
                value = GetB(x, y);
            else
            {
                byte A = GetA(x, y);
                byte B = GetB(x, y);
                byte C = GetC(x, y);

                if (C >= Math.Max(A, B))
                    value = Math.Min(A, B);
                else if (C <= Math.Min(A, B))
                    value = Math.Max(A, B);
                else
                    value = GetABC(x, y);
            }

            return value;
        }

        private byte GetAB2(int x, int y)
        {
            byte value;

            if (x == 0)
                value = GetA(x, y);
            else if (y == 0)
                value = GetB(x, y);
            else
            {
                int val = (GetA(x, y) - GetB(x, y)) / 2;
                if (val > 255)
                    val = 255;
                if (val < 0)
                    val = 0;

                value = (byte) val;
            }

            return value;
        }

        private byte GetBAC2(int x, int y)
        {
            byte value;

            if (x == 0)
                value = GetA(x, y);
            else if (y == 0)
                value = GetB(x, y);
            else
            {
                int val = GetB(x, y) + (GetA(x, y) - GetC(x, y)) / 2;
                if (val > 255)
                    val = 255;
                if (val < 0)
                    val = 0;

                value = (byte) val;
            }

            return value;
        }

        private byte GetABC2(int x, int y)
        {
            byte value;

            if (x == 0)
                value = GetA(x, y);
            else if (y == 0)
                value = GetB(x, y);
            else
            {
                int val = GetA(x, y) + (GetB(x, y) - GetC(x, y)) / 2;
                if (val > 255)
                    val = 255;
                if (val < 0)
                    val = 0;

                value = (byte) val;
            }

            return value;
        }

        private byte GetABC(int x, int y)
        {
            byte value;

            if (x == 0)
                value = GetA(x, y);
            else if (y == 0)
                value = GetB(x, y);
            else
            {
                int val = GetA(x, y) + GetB(x, y) - GetC(x, y);
                if (val > 255)
                    val = 255;
                if (val < 0)
                    val = 0;

                value = (byte) val;
            }

            return value;
        }

        private byte GetC(int x, int y)
        {
            byte value;

            if (x == 0)
                value = GetA(x, y);
            else if (y == 0)
                value = GetB(x, y);
            else
            {
                value = Decoded[y - 1, x - 1];
            }

            return value;
        }

        private byte GetB(int x, int y)
        {
            return y == 0 ? (byte) 0 : Decoded[y - 1, x];
        }

        private byte GetA(int x, int y)
        {
            return x == 0 ? (byte) 0 : Decoded[y, x - 1];
        }

        private void SaveBmpToMemory()
        {
            using (BitReader reader = new BitReader(inputFile))
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
    }
}
