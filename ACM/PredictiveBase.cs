using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM
{
    public class PredictiveBase
    {
        public string inputFileName;
        public string outputFileName;
        protected byte[] bmpHeader;
        public byte[,] Original { get; }
        public byte[,] Prediction { get; }
        public byte[,] Decoded { get; }
        public int[,] ErrorP { get; }
        public int[,] ErrorPq { get; }
        public int[,] ErrorPdq { get; }
        protected int size = 256;
        private int[][] jpegTable;

        public PredictiveBase(string inputFileName)
        {
            this.inputFileName = inputFileName;
            bmpHeader = new byte[1078];
            Original = new byte[size, size];
            Prediction = new byte[size, size];
            Decoded = new byte[size, size];
            ErrorP = new int[size, size];
            ErrorPq = new int[size, size];
            ErrorPdq = new int[size, size];
            InitializeMatrices();
        }

        private void InitializeMatrices()
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    Original[y, x] = 0;
                    Prediction[y, x] = 0;
                    Decoded[y, x] = 0;
                    ErrorP[y, x] = 0;
                    ErrorPq[y, x] = 0;
                    ErrorPq[y, x] = 0;
                }
            }
        }

        public JpegCoding GetCodingFor(int number)
        {
            if (number == 0)
                return new JpegCoding();

            int y = Convert.ToInt32(Math.Floor(Math.Log(Math.Abs(number), 2) + 1));
            uint coding = 0;

            for (int i = 0; i < y; i++)
            {
                coding |= 0x1;
                coding = coding << 1;
            }

            uint x = number > 0 ? Convert.ToUInt32(number) : Convert.ToUInt32(number + Math.Pow(2, y) - 1);
            coding = coding << y;
            coding = coding | x;

            int length = Convert.ToInt32(2*y + 1 );
            JpegCoding jpegCoding = new JpegCoding(coding, length);

            return jpegCoding;
        }

        protected byte GetPredictionFor(int predictionRule, int x, int y)
        {
            if (x == 0 && y == 0)
                return 128;

            byte value = 128;

            if (predictionRule == 0)
            {
                value = 128;
            }
            else if (predictionRule == 1)
            {
                value = GetA(x, y);
            }
            else if (predictionRule == 2)
            {
                value = GetB(x, y);
            }
            else if (predictionRule == 3)
            {
                value = GetC(x, y);
            }
            else if (predictionRule == 4)
            {
                value = GetABC(x, y);
            }
            else if (predictionRule == 5)
            {
                value = GetABC2(x, y);
            }
            else if (predictionRule == 6)
            {
                value = GetBAC2(x, y);
            }
            else if (predictionRule == 7)
            {
                value = GetAB2(x, y);
            }
            else if (predictionRule == 8)
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
    }

    public class JpegCoding
    {
        public uint Coding { get; }
        public int Length { get; }

        public JpegCoding()
        {
            Coding = 0;
            Length = 1;
        }

        public JpegCoding(uint coding, int length)
        {
            this.Coding = coding;
            this.Length = length;
        }
    }
}
