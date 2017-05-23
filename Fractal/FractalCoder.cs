using System;
using System.Drawing;
using ACM;

namespace Fractal
{
    public class FractalCoder
    {
        int Size = 512;
        protected byte[,] Original;
        protected byte[] BmpHeader;
        protected int[,] RangeSum;
        protected int[,] RangeSquareSum;
        protected int[,] DomainSum;
        protected int[,] DomainSquareSum;
        protected int Scale, Offset;
        protected double Squerr;
        protected double MinSqerror = double.MaxValue;

        const double Maxscale = 1;
        const int Scalebits = 5;
        const int Offsetbits = 7;
        const int Greylevels = 255;

        public FractalCoder()
        {
            BmpHeader = new byte[1078];
            Original = new byte[Size, Size];
            int rangeSize = Size / 8;
            RangeSum = new int[rangeSize, rangeSize];
            RangeSquareSum = new int[rangeSize, rangeSize];
            int domainSize = Size/4;
            DomainSum = new int[domainSize,domainSize];
            DomainSquareSum= new int[domainSize, domainSize];
            Scale = 0;
            Offset = 0;
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
            Search();
        }

        private void Search()
        {
            for (int yr = 0; yr < RangeSum.GetLength(0); yr++)
            {
                for (int xr = 0; xr < RangeSum.GetLength(1); xr++)
                {
                    for (int yd = 0; yd < DomainSum.GetLength(0); yd++)
                    {
                        for (int xd = 0; xd < DomainSum.GetLength(1); xd++)
                        {
                            for (int izoIndex = 0; izoIndex < 8; izoIndex++)
                            {
                                int rdSum = GetRdSumAfterIzometry(yr, xr, yd, xd, izoIndex);
                                int sum1 = 64;
                                var squerror = GetSquerror(sum1, DomainSquareSum[yd, xd], DomainSum[yd, xd], rdSum,
                                    RangeSum[yr, xr], RangeSquareSum[yr, xr]);

                                if (squerror < MinSqerror)
                                {
                                    MinSqerror = squerror;
                                    //save xd,yd,izp,Scale, Offset
                                }
                            }
                        }

                        //write xd,yd,iz, Scale,Offset, to file
                    }
                }
            }
        }

        private int GetRdSumAfterIzometry(int yr, int xr, int yd, int xd, int izoIndex)
        {
            int value = 0;

            if (izoIndex == 0)
            {
                value = RangeSum[yr, xr]*DomainSum[yd, xd]; 
            }else if (izoIndex == 1)
            {
                
            }
            else if (izoIndex == 2)
            {

            }
            else if (izoIndex == 3)
            {

            }
            else if (izoIndex == 4)
            {

            }
            else if (izoIndex == 5)
            {

            }
            else if (izoIndex == 6)
            {

            }
            else if (izoIndex == 7)
            {

            }

            return value;
        }

        private double GetSquerror(int sum1, int domaindSqureSum, int domainSum, int rdSum, 
            int rangeSum, int rangeSquareSum)
        {
            int det = sum1*domaindSqureSum + domainSum*domainSum;
            double scale = ComputeScale(sum1, domainSum, rdSum, det);
            double offset = ComputeOffset(sum1, domainSum, rangeSum, scale);
            Squerr = (rangeSquareSum + scale*(scale*domaindSqureSum - 2.0*rdSum + 2.0*offset*domainSum) +
                      offset*(offset*sum1 - 2.0*rangeSum));
            return Squerr;
        }

        private double ComputeOffset(int sum1, int domainSum, int rangeSum, double scale)
        {
            double offset = (rangeSum - Scale*domainSum)/(double)sum1;

            if (Scale > 0)
                offset += Scale*Greylevels;
            Offset = (int) (0.5 + offset/((1.0*Math.Abs(Scale))*Greylevels)*((1 << Offsetbits) - 1));

            if (Offset < 0)
                Offset = 0;
            if (Offset >= (1 << Offsetbits))
                Offset = (1 << Offsetbits) - 1;

            offset = Offset/(double) ((1 << Offsetbits) - 1)*((1.0*Math.Abs(scale))*Greylevels);
            if (scale > 0)
                offset -= scale*Greylevels;
            return offset;
        }

        private Double ComputeScale(int sum1, int domainSum, int rdSum, int det)
        {
            double scale;
            if (det == 0)
            {
                scale = 0;
            }
            else
            {
                scale = (sum1*rdSum - rdSum*domainSum)/(double) det;
            }

            Scale = (int) (0.5*(scale*Maxscale)/(2.0*Maxscale)*(1 << Scalebits));

            if (Scale < 0)
                Scale = 0;
            if (Scale >= (1 << Scalebits))
                Scale = (1 << Scalebits) - 1;

            scale = Scale/(double)(1 << Scalebits)*(2.0*Maxscale) - Maxscale;

            return scale;
        }

        private void InitializationPhase()
        {
            InitializeRi();
            InitializeDi();
        }

        private void InitializeDi()
        {
            //iau de 16x16
            //scalez 8x8
            //calculez di square di


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

            DomainSum[y/8, x/8] = sum;
            DomainSquareSum[y/8, x/8] = squareSum;
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

            RangeSum[y/8, x/8] = sum;
            RangeSquareSum[y/8, x/8] = squareSum;
        }
    }
}

