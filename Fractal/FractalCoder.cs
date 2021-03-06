﻿using System;
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
        protected int iScale, iOffset;
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
            DomainSum = new int[rangeSize - 1, rangeSize - 1];
            DomainSquareSum = new int[rangeSize - 1, rangeSize - 1];
            iScale = 0;
            iOffset = 0;

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
            
            //using (StreamWriter writer = new StreamWriter(outputFileName))
            //using (BitWriter writer = new BitWriter(outputFileName))
            {
                for (int yr = 0; yr < RangeSum.GetLength(0); yr++)
                {
                    for (int xr = 0; xr < RangeSum.GetLength(1); xr++)
                    {
                        MinSqerror = double.MaxValue;
                        SetMinCoordinate(0,0,0,0,0);

                        for (int yd = 0; yd < DomainSum.GetLength(0); yd++)
                        {
                            for (int xd = 0; xd < DomainSum.GetLength(1); xd++)
                            {
                                for (int izoIndex = 0; izoIndex < 8; izoIndex++)
                                {
                                    int rdSum = GetRdSumAfterIzometry(yr, xr, yd, xd, izoIndex);
                                    int sum1 = 64;
                                    var squerorParameters = GetSquerror(sum1, DomainSquareSum[yd, xd], DomainSum[yd, xd],
                                        rdSum, RangeSum[yr, xr], RangeSquareSum[yr, xr]);

                                    if (squerorParameters.Squerror < MinSqerror)
                                    {
                                        MinSqerror = squerorParameters.Squerror;
                                        SetMinCoordinate(xd, yd, izoIndex, squerorParameters.Scale,
                                            squerorParameters.Offset);
                                    }
                                }
                            }
                        }
                        /*
                        string val = $"{minXd}    {minYd}    {minIzoIndex}    {minScale}    {minOffset}";
                        writer.WriteLine(val);      */

                        fractalParameters[yr, xr].Xd = minXd;
                        fractalParameters[yr, xr].Yd = minYd;
                        fractalParameters[yr, xr].IzoIndex = minIzoIndex;
                        fractalParameters[yr, xr].Scale = Convert.ToInt32(minScale);
                        fractalParameters[yr, xr].Offset = Convert.ToInt32(minOffset);

                        MinSqerror = double.MaxValue;
                        SetMinCoordinate(int.MaxValue, int.MaxValue, int.MaxValue, double.MaxValue, double.MaxValue);
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

        private void SetMinCoordinate(int xd, int yd, int izoIndex, double scale, double offset)
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

            if (izoIndex == 0) //identity
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        value += Original[y + yr, x + xr]*Original[y + yd, x + xd];
                    }
                }
            }
            else if (izoIndex == 1) //vertical mirror
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        value += Original[y + yr, x + xr]*Original[y + yd, 7 - x + xd];
                    }
                }
            }
            else if (izoIndex == 2) //horizontal mirror
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        value += Original[y + yr, x + xr]*Original[7 - y + yd, x + xd];
                    }
                }
            }
            else if (izoIndex == 3) // diagonala principala
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        value += Original[y + yr, x + xr]*Original[x + xd, y + yd];
                    }
                }
            }
            else if (izoIndex == 4) // diagonala secundara
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        value += Original[y + yr, x + xr]*Original[7 - x + xd, 7 - y + yd];
                    }
                }
            }
            else if (izoIndex == 5) //rotate 90 degress  clockwise
            {
                for (int y = 8 - 1; y > 0; y--)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        value += Original[y + yr, x + xr]*Original[y + yd, x + xd];
                    }
                }
            }
            else if (izoIndex == 6) //rotate 180 degress clockwise
            {
                for (int y = 8 - 1; y > 0; y--)
                {
                    for (int x = 8 - 1; x > 0; x--)
                    {
                        value += Original[y + yr, x + xr]*Original[y + yd, x + xd];
                    }
                }
            }
            else if (izoIndex == 7) //rotate 90 degrees counter-clockwise
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
            double scale = ComputeScale(sum1, domainSum, rdSum, det, rangeSum);
            double offset = ComputeOffset(sum1, domainSum, rangeSum, scale);
            Squerr = (rangeSquareSum + scale*(scale*domaindSqureSum - 2.0*rdSum + 2.0*offset*domainSum) +
                      offset*(offset*sum1 - 2.0*rangeSum));

            var squerorParameters = new SquerorParameters()
            {
                Offset = iOffset,
                Scale = iScale,
                Squerror = Squerr
            };

            return squerorParameters;
        }

        private double ComputeOffset(int sum1, int domainSum, int rangeSum, double scale)
        {
            double offset = (rangeSum - scale*domainSum)/(double) sum1;

            if (scale > 0)
                offset += scale*Greylevels;
            iOffset = (int) (0.5 + offset/((1.0+Math.Abs(iScale))*Greylevels)*((1 << Offsetbits) - 1));

            if (iOffset < 0)
                iOffset = 0;
            if (iOffset >= (1 << Offsetbits))
                iOffset = (1 << Offsetbits) - 1;

            offset = iOffset/(double) ((1 << Offsetbits) - 1)*((1.0+Math.Abs(scale))*Greylevels);
            if (scale > 0)
                offset -= scale*Greylevels;
            return offset;
        }

        private double ComputeScale(int sum1, int domainSum, int rdSum, int det, int rangeSum)
        {
            double scale;
            if (det == 0)
                scale = 0;
            else
                scale = (sum1*rdSum - rangeSum*domainSum)/(double) det;
            iScale = (int) (0.5*(scale + Maxscale)/(2.0*Maxscale)*(1 << Scalebits));
            if (iScale < 0)
                iScale = 0;
            if (iScale >= (1 << Scalebits))
                iScale = (1 << Scalebits) - 1;
            scale = iScale/(double) (1 << Scalebits)*(2.0*Maxscale) - Maxscale;

            return scale;
        }

        private void InitializationPhase()
        {
            InitializeRi();
            InitializeDi();
        }

        private void InitializeDi()
        {
            for (int y = 0; y < Size - 16; y += 8)
            {
                for (int x = 0; x < Size - 16; x += 8)
                {
                    CalculateDiSumAndSquareSum(y, x);
                }
            }
        }

        private void CalculateDiSumAndSquareSum(int y, int x)
        {
            int sum = 0;
            int squareSum = 0;

            for (int ydi = 0; ydi < 16; ydi += 2)
            {
                for (int xdi = 0; xdi < 16; xdi += 2)
                {
                    int value = (Original[y + ydi    , x + xdi    ] +
                                 Original[y + ydi    , x + xdi + 1] +
                                 Original[y + ydi + 1, x + xdi    ] +
                                 Original[y + ydi + 1, x + xdi + 1])/4;
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

            var xd = fractalParameters[y/8, x/8].Xd;
            var yd = fractalParameters[y/8, x/8].Yd;
            xd = xd*8;
            yd = yd*8;

            for (int i = 0; i < 8; i++)
            {
                byte color = 255;
                var yAbove = y - 1;
                yAbove = CheckValueBetweem0And511(yAbove);
                var xNext = x + i;
                xNext = CheckValueBetweem0And511(xNext);
                var xLeft = x - 1;
                xLeft = CheckValueBetweem0And511(xLeft);
                var yNext = y + i;
                yNext = CheckValueBetweem0And511(yNext);
                var yBelow = y + 1 + 8;
                yBelow = CheckValueBetweem0And511(yBelow);
                var xRight = x + 1 + 8;
                xRight = CheckValueBetweem0And511(xRight);

                temp[yAbove, xNext] = color;
                temp[yNext, xLeft] = color;
                temp[yBelow, xNext] = color;
                temp[yNext, xRight] = color;

                //matching d
                var ydAbove = yd - 1;
                ydAbove = CheckValueBetweem0And511(ydAbove);
                var xdNext = xd + i;
                xdNext = CheckValueBetweem0And511(xdNext);
                var xdLeft = xd - 1;
                xdLeft = CheckValueBetweem0And511(xdLeft);
                var ydNext = yd + i;
                ydNext = CheckValueBetweem0And511(ydNext);
                var ydBelow = yd + 1 + 16;
                ydBelow = CheckValueBetweem0And511(ydBelow);
                var xdRight = xd + 1 + 16;
                xdRight = CheckValueBetweem0And511(xdRight);

                temp[ydAbove, xdNext] = color;
                temp[ydNext, xdLeft] = color;
                temp[ydBelow, xdNext] = color;
                temp[ydNext, xdRight] = color;
            }

            return temp;
        }

        public int CheckValueBetweem0And511(int value)
        {
            if (value < 0)
                return 0;
            if (value > 511)
                return 511;
            return value;
        }

        public FractalParameters GetFractalParameters(int x, int y)
        {
            return fractalParameters[y/8, x/8];
        }
    }

    public class SquerorParameters
    {
        public double Scale;
        public double Offset;
        public double Squerror;
    }
}


