using System;
using System.Drawing;

namespace ACM
{
    public class PredictiveBase
    {
        public string InputFileName;
        public string OutputFileName;
        protected byte[] BmpHeader;
        public byte[,] Original { get; }
        public byte[,] Prediction { get; }
        public byte[,] Decoded { get; }
        public int[,] ErrorP { get; }
        public int[,] ErrorPq { get; }
        public int[,] ErrorPdq { get; }
        protected int Size = 256;

        public int Min;
        public int Max;

        protected int PredictionRule;
        protected int K;
        
        public PredictiveBase(string inputFileName)
        {
            InputFileName = inputFileName;
            BmpHeader = new byte[1078];
            Original = new byte[Size, Size];
            Prediction = new byte[Size, Size];
            Decoded = new byte[Size, Size];
            ErrorP = new int[Size, Size];
            ErrorPq = new int[Size, Size];
            ErrorPdq = new int[Size, Size];
            InitializeMatrices();
        }

        private void InitializeMatrices()
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
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

        public void CalculateError(byte[,] originalMatrix, byte[,] decodedMatrix)
        {
            Max = int.MinValue;
            Min = int.MaxValue;

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    var val = originalMatrix[y, x] - decodedMatrix[y, x];
                    if (val < Min)
                        Min = val;
                    if (val > Max)
                        Max = val;
                }
            }
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
                value = GetAbc(x, y);
            }
            else if (predictionRule == 5)
            {
                value = GetAbc2(x, y);
            }
            else if (predictionRule == 6)
            {
                value = GetBac2(x, y);
            }
            else if (predictionRule == 7)
            {
                value = GetAb2(x, y);
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
                byte a = GetA(x, y);
                byte b = GetB(x, y);
                byte c = GetC(x, y);

                if (c >= Math.Max(a, b))
                    value = Math.Min(a, b);
                else if (c <= Math.Min(a, b))
                    value = Math.Max(a, b);
                else
                    value = GetAbc(x, y);
            }

            return value;
        }

        private byte GetAb2(int x, int y)
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

        private byte GetBac2(int x, int y)
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

        private byte GetAbc2(int x, int y)
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

        private byte GetAbc(int x, int y)
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
            return x == 0 ? (byte) 128 : Decoded[y, x - 1];
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

        public void WriteBmpHeader(BitWriter writer)
        {
            foreach (byte b in BmpHeader)
                writer.WriteNBiti(b, 8);
        }

        public void ReadBmpHeader(BitReader reader)
        {
            for (int i = 0; i < 1078; i++)
                BmpHeader[i] = (byte)reader.ReadNBit(8);
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
            Coding = coding;
            Length = length;
        }
    }
}
