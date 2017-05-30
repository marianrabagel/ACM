using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ACM;

namespace Fractal
{
    public class FractalCoder
    {
        int Size = 512;
        public byte[,] Original;
        protected byte[] BmpHeader;
        protected int[,] RangeSum;
        protected int[,] RangeSquareSum;
        protected int[,] DomainSum;
        protected int[,] DomainSquareSum;
        protected int Scale, Offset;
        protected double Squerr;
        protected double MinSqerror = double.MaxValue;
        FractalParameters[,] fractalParameters;

        protected string outputFileName;

        const double Maxscale = 1;
        const int Scalebits = 5;
        const int Offsetbits = 7;
        const int Greylevels = 255;

        public FractalCoder()
        {
            BmpHeader = new byte[1078];
            Original = new byte[Size, Size];
            int rangeSize = Size/8;
            RangeSum = new int[rangeSize, rangeSize];
            RangeSquareSum = new int[rangeSize, rangeSize];
            DomainSum = new int[rangeSize, rangeSize];
            DomainSquareSum = new int[rangeSize, rangeSize];
            Scale = 0;
            Offset = 0;

            fractalParameters = new FractalParameters[rangeSize, rangeSize];

            for (int y = 0; y < fractalParameters.GetLength(0); y++)
            {
                for (int x = 0; x < fractalParameters.GetLength(1); x++)
                {
                    fractalParameters[y, x] = new FractalParameters();
                }
            }
        }

        public void LoadBmpFile(string fileName)
        {
            this.outputFileName = fileName + ".frc";
            ReadBmpHeaderAndLoadImageToMemory(fileName);
        }

        private void ReadBmpHeaderAndLoadImageToMemory(string fileName)
        {
            using (BitReader reader = new BitReader(fileName))
            {
                ReadBmpHeader(reader);

                for (int y = Size - 1; y >= 0; y--)
                    for (int x = 0; x < Size; x++)
                        Original[y, x] = (byte) reader.ReadNBit(8);
            }
        }

        public void ReadBmpHeader(BitReader reader)
        {
            for (int i = 0; i < 1078; i++)
                BmpHeader[i] = (byte) reader.ReadNBit(8);
        }

        public Bitmap GetBitmap(byte[,] matrix)
        {
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

        public void Process(ProgressBar progressBar1)
        {
            InitializationPhase();
            Search(progressBar1);
        }

        

        private void Search(ProgressBar progressBar1)
        {
            MinSqerror = double.MaxValue;
            //using (StreamWriter writer = new StreamWriter(outputFileName))
            using (BitWriter writer = new BitWriter(outputFileName))
            {
                for (int yr = 0; yr < RangeSum.GetLength(0); yr++)
                {
                    for (int xr = 0; xr < RangeSum.GetLength(1); xr++)
                    {
                        for (int yd = 0; yd < DomainSum.GetLength(0); yd++)
                        {
                            for (int xd = 0; xd < DomainSum.GetLength(1); xd++)
                            {
                                for (int izoIndex = 0; izoIndex < 2; izoIndex++)
                                {
                                    int rdSum = GetRdSumAfterIzometry(yr, xr, yd, xd, izoIndex);
                                    int sum1 = 64;
                                    var squerorParameters = GetSquerror(sum1, DomainSquareSum[yd, xd], DomainSum[yd, xd],
                                        rdSum, RangeSum[yr, xr], RangeSquareSum[yr, xr]);

                                    if (squerorParameters.Squerror < MinSqerror)
                                    {
                                        MinSqerror = squerorParameters.Squerror;
                                        SaveMinCoordinate(xd, yd, izoIndex, squerorParameters.Scale,
                                            squerorParameters.Offset);
                                    }
                                }
                            }
                        }

                        //string val = $"{minXd}    {minYd}    {minIzoIndex}    {minScale}    {minOffset}";
                        //writer.WriteLine(val);      

                        fractalParameters[yr, xr].Xd = minXd;
                        fractalParameters[yr, xr].Yd = minYd;
                        fractalParameters[yr, xr].IzoIndex = minIzoIndex;
                        fractalParameters[yr, xr].Scale = Convert.ToInt32(minScale);
                        fractalParameters[yr, xr].Offset = Convert.ToInt32(minOffset);

                        MinSqerror = double.MaxValue;
                        SaveMinCoordinate(int.MaxValue, int.MaxValue, int.MaxValue, double.MaxValue, double.MaxValue);
                        progressBar1.Value++;
                    }
                }
            }
        }

        protected int minXd = int.MaxValue;
        protected int minYd = int.MaxValue;
        protected int minIzoIndex = int.MaxValue;
        protected double minScale = double.MaxValue;
        protected double minOffset = double.MaxValue;

        private void SaveMinCoordinate(int xd, int yd, int izoIndex, double scale, double offset)
        {
            minXd = xd;
            minYd = yd;
            minIzoIndex = izoIndex;
            minScale = scale;
            minOffset = offset;
        }

        private int GetRdSumAfterIzometry(int yr, int xr, int yd, int xd, int izoIndex)
        {
            int value = 0;

            if (izoIndex == 0)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        value += Original[y + yr, x + xr]*Original[y + yd, x + xd];
                    }
                }
            }
            else if (izoIndex == 1)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        value += Original[y + yr, x + xr]*Original[y + yd, 7 - x + xd];
                    }
                }
            }
            else if (izoIndex == 2)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        value += Original[y + yr, x + xr]*Original[7 - y + yd, x + xd];
                    }
                }
            }
            else if (izoIndex == 3)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        value += Original[y + yr, x + xr]*Original[x + xd, y + yd];
                    }
                }
            }
            else if (izoIndex == 4)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        value += Original[y + yr, x + xr]*Original[7 - x + xd, 7 - y + yd];
                    }
                }
            }
            else if (izoIndex == 5)
            {
                for (int y = 8 - 1; y > 0; y--)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        value += Original[y + yr, x + xr]*Original[y + yd, x + xd];
                    }
                }
            }
            else if (izoIndex == 6)
            {
                for (int y = 8 - 1; y > 0; y--)
                {
                    for (int x = 8 - 1; x > 0; x--)
                    {
                        value += Original[y + yr, x + xr]*Original[y + yd, x + xd];
                    }
                }
            }
            else if (izoIndex == 7)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 8 - 1; x > 0; x--)
                    {
                        value += Original[y + yr, x + xr]*Original[y + yd, x + xd];
                    }
                }
            }

            return value;
        }

        private SquerorParameters GetSquerror(int sum1, int domaindSqureSum, int domainSum, int rdSum,
            int rangeSum, int rangeSquareSum)
        {
            int det = sum1*domaindSqureSum + domainSum*domainSum;
            double scale = ComputeScale(sum1, domainSum, rdSum, det);
            double offset = ComputeOffset(sum1, domainSum, rangeSum, scale);
            Squerr = (rangeSquareSum + scale*(scale*domaindSqureSum - 2.0*rdSum + 2.0*offset*domainSum) +
                      offset*(offset*sum1 - 2.0*rangeSum));

            var squerorParameters = new SquerorParameters()
            {
                Offset = Offset,
                Scale = Scale,
                Squerror = Squerr
            };

            return squerorParameters;
        }

        private double ComputeOffset(int sum1, int domainSum, int rangeSum, double scale)
        {
            double offset = (rangeSum - Scale*domainSum)/(double) sum1;

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
                scale = 0;
            else
                scale = (sum1*rdSum - rdSum*domainSum)/(double) det;
            Scale = (int) (0.5*(scale*Maxscale)/(2.0*Maxscale)*(1 << Scalebits));
            if (Scale < 0)
                Scale = 0;
            if (Scale >= (1 << Scalebits))
                Scale = (1 << Scalebits) - 1;
            scale = Scale/(double) (1 << Scalebits)*(2.0*Maxscale) - Maxscale;

            return scale;
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

            for (int ydi = 0; ydi < 8; ydi += 2)
            {
                for (int xdi = 0; xdi < 8; xdi += 2)
                {
                    int value = (Original[ydi, xdi] + Original[ydi, xdi + 1] + Original[ydi + 1, xdi] +
                                 Original[ydi + 1, xdi + 1])/4;
                    sum += value;
                    squareSum += value*value;
                }
            }

            DomainSum[y/8, x/8] = sum;
            DomainSquareSum[y/8, x/8] = squareSum;
        }

        private void InitializeRi()
        {
            for (int y = 0; y < Size - 8-1; y += 8)
            {
                for (int x = 0; x < Size - 8-1; x += 8)
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

        public void Save()
        {
            using (BitWriter writer = new BitWriter(outputFileName))
            {
                /*for (int i = 0; i < BmpHeader.GetLength(0); i++)
                {
                    writer.WriteNBiti(BmpHeader[i], 8);
                }
                */
                for (int y = 0; y < fractalParameters.GetLength(0); y++)
                {
                    for (int x = 0; x < fractalParameters.GetLength(1); x++)
                    {
                        writer.WriteNBiti(Convert.ToUInt32(fractalParameters[y,x].Xd), 6);
                        writer.WriteNBiti(Convert.ToUInt32(fractalParameters[y,x].Yd), 6);
                        writer.WriteNBiti(Convert.ToUInt32(fractalParameters[y,x].IzoIndex), 3);
                        writer.WriteNBiti(Convert.ToUInt32(fractalParameters[y,x].Scale), 5);
                        writer.WriteNBiti(Convert.ToUInt32(fractalParameters[y,x].Offset), 7);
                    }
                }
            }
        }

        public byte[,] DrawBorder(int x, int y)
        {
            byte[,] temp = new byte[Size, Size];

            for (int yr = 0; yr < Size; yr++)
            {
                for (int xr = 0; xr < Size; xr++)
                {
                    temp[yr, xr] = Original[yr, xr];
                }
            }
            for (int i = 0; i < 8; i++)
            {
                byte color = 255;
                temp[y - 1, x + i] = color;
                temp[y + i, x - 1] = color;
                temp[y + 1 + 8, x + i] = color;
                temp[y + i, x + 1 + 8] = color;
            }

            return temp;
        }
    }

    public class SquerorParameters
    {
        public double Scale;
        public double Offset;
        public double Squerror;
    }
}


