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
        byte[,] original;
        byte[,] prediction;
        byte[,] decoded;
        int[,] errorP;
        int[,] errorPQ;
        int[,] errorPDQ;
        int[,] error;

        public PredictiveCoder(string inputFile)
        {
            this.inputFile = inputFile;
            bmpHeader = new byte[1078];
            original = new byte[256, 256];
            decoded = new byte[256, 256];
            errorP = new int[256, 256];
        }

        public void Encode(string predictionRule, int k, string entropicCoder)
        {
            outputFile = inputFile + ".p" + predictionRule + "k" + k + entropicCoder + ".prd";
            SaveBmpToMemory();

            for (int y = 0; y < original.GetLength(0); y++)
            {
                for (int x = 0; x < original.GetLength(1); x++)
                {
                    byte predictionValue = GetPredictionFor(predictionRule, x, y);
                    prediction[y, x] = predictionValue;

                    //get prediction
                    //calculate errorp
                    //quantize errorP
                    //dequantize errorp
                    //calculate decoded
                    //calculate error
                }
            }

        }

        private byte GetPredictionFor(string predictionRule, int x, int y)
        {
            byte value = 128;
            if (x == 0 && y == 0)
                return value;
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
                value = GetJpegLs(x,y);
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

                value = (byte)val;
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

                value = (byte)val;
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
                int val = GetA(x, y) + (GetB(x, y) - GetC(x, y))/2;
                if (val > 255)
                    val = 255;
                if (val < 0)
                    val = 0;

                value = (byte)val;
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

                value = (byte)val;
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
                value = decoded[y - 1, x - 1];
            }

            return value;
        }

        private byte GetB(int x, int y)
        {
            return y == 0 ? (byte) 128 : decoded[y - 1, x];
        }

        private byte GetA(int x, int y)
        {
            return x == 0 ? (byte) 128 : decoded[y, x - 1];
        }

        private void SaveBmpToMemory()
        {
            using (BitReader reader = new BitReader(inputFile))
            {
                for (int i = 0; i < 1078; i++)
                    bmpHeader[i] = (byte) reader.ReadNBit(8);

                for (int y = original.GetLength(0) - 1; y > 0; y--)
                {
                    for (int x = 0; x < original.GetLength(1); x++)
                    {
                        original[y, x] = (byte) reader.ReadNBit(8);
                    }
                }
            }
        }

        public Bitmap GetOriginalBitmap()
        {
            Bitmap bitmap = new Bitmap(original.GetLength(1), original.GetLength(0));

            for (int y = 0; y < original.GetLength(1); y++)
            {
                for (int x = 0; x < original.GetLength(0); x++)
                {
                    byte value = original[y, x];
                    Color color = Color.FromArgb(255, value, value, value);
                    bitmap.SetPixel(x, y, color);
                }
            }

            return bitmap;
        }
    }
}
