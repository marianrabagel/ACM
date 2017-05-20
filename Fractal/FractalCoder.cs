using System;
using System.Drawing;
using ACM;

namespace Fractal
{
    public class FractalCoder
    {
        int Size = 512;
        byte[,] _original;
        protected byte[] BmpHeader;
        int[,] _rangeSum;
        int[,] _rangeSquareSum;
        int[,] _domainSum;
        int[,] _domainSquareSum;
        int _scale, _offset;
        double squerr;
        double minSqerror = double.MaxValue;

        const double MAXSCALE = 1;
        const int SCALEBITS = 5;
        const int OFFSETBITS = 7;
        const int GREYLEVELS = 255;

        public FractalCoder()
        {
            BmpHeader = new byte[1078];
            _original = new byte[Size, Size];
            int rangeSize = Size / 8;
            _rangeSum = new int[rangeSize, rangeSize];
            _rangeSquareSum = new int[rangeSize, rangeSize];
            int domainSize = Size/4;
            _domainSum = new int[domainSize,domainSize];
            _domainSquareSum= new int[domainSize, domainSize];
            _scale = 0;
            _offset = 0;
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
                        _original[y, x] = (byte)reader.ReadNBit(8);
            }
        }

        public void ReadBmpHeader(BitReader reader)
        {
            for (int i = 0; i < 1078; i++)
                BmpHeader[i] = (byte)reader.ReadNBit(8);
        }

        public Bitmap GetBitmap()
        {
            byte[,] matrix = _original;
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
            for (int yr = 0; yr < _rangeSum.GetLength(0); yr++)
            {
                for (int xr = 0; xr < _rangeSum.GetLength(1); xr++)
                {
                    for (int yd = 0; yd < _domainSum.GetLength(0); yd++)
                    {
                        for (int xd = 0; xd < _domainSum.GetLength(1); xd++)
                        {
                            for (int izoIndex = 0; izoIndex < 8; izoIndex++)
                            {
                                int rdSum = GetRdSumAfterIzometry(yr, xr, yd, xd, izoIndex);
                                int sum1 = 1; //?
                                var squerror = GetSquerror(sum1, _domainSquareSum[yd, xd], _domainSum[yd, xd], rdSum,
                                    _rangeSum[yr, xr], _rangeSquareSum[yr, xr]);

                                if (squerror < minSqerror)
                                {
                                    minSqerror = squerror;
                                    //save xd,yd,izp,_scale, _offset
                                }
                            }
                        }

                        //write xd,yd,iz, _scale,_offset, to file
                    }
                }
            }
        }

        private int GetRdSumAfterIzometry(int yr, int xr, int yd, int xd, int izoIndex)
        {
            //to do add izo types
            return _rangeSum[yr, xr]*_domainSum[yd, xd];
        }

        private double GetSquerror(int sum1, int domaindSqureSum, int domainSum, int rdSum, 
            int rangeSum, int rangeSquareSum)
        {
            int det = sum1*domaindSqureSum + domainSum*domainSum;
            double scale = ComputeScale(sum1, domainSum, rdSum, det);
            double offset = ComputeOffset(sum1, domainSum, rangeSum, scale);
            squerr = (rangeSquareSum + scale*(scale*domaindSqureSum - 2.0*rdSum + 2.0*offset*domainSum) +
                      offset*(offset*sum1 - 2.0*rangeSum));
            return squerr;
        }

        private double ComputeOffset(int sum1, int domainSum, int rangeSum, double scale)
        {
            double offset = (rangeSum - _scale*domainSum)/sum1;

            if (_scale > 0)
                offset += _scale*GREYLEVELS;
            _offset = (int) (0.5 + offset/((1.0*Math.Abs(_scale))*GREYLEVELS)*((1 << OFFSETBITS) - 1));

            if (_offset < 0)
                _offset = 0;
            if (_offset >= (1 << OFFSETBITS))
                _offset = (1 << OFFSETBITS) - 1;

            offset = (double) _offset/(double) ((1 << OFFSETBITS) - 1)*((1.0*Math.Abs(scale))*GREYLEVELS);
            if (scale > 0)
                offset -= scale*GREYLEVELS;
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

            _scale = (int) (0.5*(scale*MAXSCALE)/(2.0*MAXSCALE)*(1 << SCALEBITS);

            if (_scale < 0)
                _scale = 0;
            if (_scale >= (1 << SCALEBITS))
                _scale = (1 << SCALEBITS) - 1;

            scale = (double)_scale/(double)(1 << SCALEBITS)*(2.0*MAXSCALE) - MAXSCALE;

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

            for (int ydi = 0; ydi < 16; ydi++)
            {
                for (int xdi = 0; xdi < 16; xdi++)
                {
                    sum += _original[ydi, xdi];
                    squareSum += _original[ydi, xdi]*_original[ydi, xdi];
                }
            }

            _domainSum[y/8, x/8] = sum;
            _domainSquareSum[y/8, x/8] = squareSum;
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
                    sum += _original[y + yri, x + xri];
                    squareSum += _original[y + yri, x + xri]*_original[y + yri, x + xri];
                }
            }

            _rangeSum[y/8, x/8] = sum;
            _rangeSquareSum[y/8, x/8] = squareSum;
        }
    }
}

