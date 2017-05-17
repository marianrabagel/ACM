using System.Drawing;
using ACM;

namespace Fractal
{
    public class FractalCoder
    {
        int Size = 512;
        byte[,] Original;
        protected byte[] BmpHeader;
        int[,] rangeSum;
        int[,] rangeSquareSum;
        int[,] domainSum;
        int[,] domainSquareSum;

        public FractalCoder()
        {
            BmpHeader = new byte[1078];
            Original = new byte[Size, Size];
            int rangeSize = Size / 8;
            rangeSum = new int[rangeSize, rangeSize];
            rangeSquareSum = new int[rangeSize, rangeSize];
            int domainSize = Size/4;
            domainSum = new int[domainSize,domainSize];
            domainSquareSum= new int[domainSize, domainSize];
        }

        public void LoadBmpFile(string fileName)
        {
            ReadBmpHeaderAndLoadImageToMemory(fileName);
        }

        private void ReadBmpHeaderAndLoadImageToMemory(string fileName)
        {
            using (BitReader reader = new BitReader(fileName))
            {
                ReadBmpHeader(reader);

                for (int y = Size - 1; y >= 0; y--)
                    for (int x = 0; x < Size; x++)
                        Original[y, x] = (byte)reader.ReadNBit(8);
            }
        }

        public void ReadBmpHeader(BitReader reader)
        {
            for (int i = 0; i < 1078; i++)
                BmpHeader[i] = (byte)reader.ReadNBit(8);
        }

        public Bitmap GetBitmap()
        {
            byte[,] matrix = Original;
            Bitmap bitmap = new Bitmap(matrix.GetLength(1), matrix.GetLength(0));
            for (int y = 0; y < matrix.GetLength(1); y++)
            {
                for (int x = 0; x < matrix.GetLength(0); x++)
                {
                    byte value = matrix[y, x];
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

        public void Process()
        {
            InitializationPhase();
        }

        private void InitializationPhase()
        {
            InitializeRi();
            InitializeDi();
        }

        private void InitializeDi()
        {
            for (int y = 0; y < Size - 16; y += 16)
            {
                for (int x = 0; x < Size - 16; x += 16)
                {
                    CalculateDiSumAndSquareSum(y, x);
                }
            }
        }

        private void CalculateDiSumAndSquareSum(int y, int x)
        {
            int sum = 0;
            int squareSum = 0;

            for (int ydi = 0; ydi < 16; ydi++)
            {
                for (int xdi = 0; xdi < 16; xdi++)
                {
                    sum += Original[ydi, xdi];
                    squareSum += Original[ydi, xdi]*Original[ydi, xdi];
                }
            }

            domainSum[y/8, x/8] = sum;
            domainSquareSum[y/8, x/8] = squareSum;
        }

        private void InitializeRi()
        {
            for (int y = 0; y < Size - 8; y += 8)
            {
                for (int x = 0; x < Size - 8; x += 8)
                {
                    CalculateRiSumAndSquareSum(y, x);
                }
            }
        }

        private void CalculateRiSumAndSquareSum(int y, int x)
        {
            int sum = 0;
            int squareSum = 0;

            for (int yri = 0; yri < 8; yri++)
            {
                for (int xri = 0; xri < 8; xri++)
                {
                    sum += Original[y + yri, x + xri];
                    squareSum += Original[y + yri, x + xri]*Original[y + yri, x + xri];
                }
            }

            rangeSum[y/8, x/8] = sum;
            rangeSquareSum[y/8, x/8] = squareSum;
        }
    }
}

