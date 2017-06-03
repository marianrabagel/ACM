
using System;
using System.Drawing;
using ACM;

namespace Fractal
{
    public class FractalDecoder
    {
        // luat pereche de 8 in fisier
        //sursa -> imag initiala
        //destinatie construita range cu range
        //pentru fiecare range, iaud xxd,yd * 8 
        //iaa de16xx16
        // reduc la 8x8
        //aplic izo
        //scale, si offset
        //pus in r
        //iterativ

        protected int[,] Original;
        protected byte[] BmpHeader;
        int Size = 512;
        FractalParameters[,] fractalParameters;
        int[,] temp;


        const double Maxscale = 1;
        const int Scalebits = 5;
        const int Offsetbits = 7;
        const int Greylevels = 255;

        protected string outputFileName;

        public FractalDecoder()
        {
            BmpHeader = new byte[1078];
            Original = new int[Size, Size];
            temp = new int[Size, Size];
            fractalParameters = new FractalParameters[Size / 8, Size / 8];

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    Original[y, x] = 0;
                }
            }

            for (int y = 0; y < fractalParameters.GetLength(0); y++)
            {
                for (int x = 0; x < fractalParameters.GetLength(1); x++)
                {
                    fractalParameters[y, x] = new FractalParameters();
                }
            }
        }

        public void LoadInitialBmp(string bmpFileName)
        {
            ReadBmpHeaderAndLoadImageToMemory(bmpFileName);
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

        public Bitmap GetBitmap()
        {
            int[,] matrix = Original;
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

        
        public void LoadFrcFile(string inputFileName)
        {
            outputFileName = inputFileName + ".bmp";
            
            using (BitReader reader = new BitReader(inputFileName))
            {
                //ReadBmpHeader(reader);
                for (int y = 0; y < Size/8; y++)
                {
                    for (int x = 0; x < Size/8; x++)
                    {
                        fractalParameters[y, x].Xd = Convert.ToInt32(reader.ReadNBit(6));
                        fractalParameters[y, x].Yd = Convert.ToInt32(reader.ReadNBit(6));
                        fractalParameters[y, x].IzoIndex = Convert.ToInt32(reader.ReadNBit(3));
                        fractalParameters[y, x].Scale = Convert.ToInt32(reader.ReadNBit(5));
                        fractalParameters[y, x].Offset = Convert.ToInt32(reader.ReadNBit(7));
                    }
                }
            }

        }

        public void Decode()
        {
            // luat pereche de 8 in fisier
            //sursa -> imag initiala
            //destinatie construita range cu range
            //pentru fiecare range, iau xd,yd * 8 
            //iaa de 16x16
            // reduc la 8x8
            //aplic izo
            //scale, si offset
            //pus in r
            //iterativ
            
            for (int y = 0; y < fractalParameters.GetLength(0); y++)
            {
                for (int x = 0; x < fractalParameters.GetLength(1); x++)
                {
                    int xd = fractalParameters[y, x].Xd*8;
                    int yd = fractalParameters[y, x].Yd*8;
                    int[,] reducedDomain = GetReducedDomain(xd, yd);
                    int izo = fractalParameters[y, x].IzoIndex;
                    int[,] izometricDomains = ApplyIzometry(izo, reducedDomain);

                    var scale = GetScale(fractalParameters[y, x].Scale);
                    var offset = GetOffset(fractalParameters[y, x].Offset, scale);

                    for (int yr = 0; yr < 8; yr++)
                    {
                        for (int xr = 0; xr < 8; xr++)
                        {
                            temp[y*8 + yr, x*8 + xr] = Convert.ToInt32(izometricDomains[yr, xr]*scale + offset);
                        }
                    }
                }
            }

            CopyToOriginal(temp);
        }

        private double GetOffset(int offset, double scale)
        {
            double o = offset / (double)((1 << Offsetbits) - 1) * ((1.0 + Math.Abs(scale)) * Greylevels);
            if (scale > 0.0)
                o -= scale * Greylevels;

            return o;
        }

        private double GetScale(int scale)
        {
            return (double)scale / (double)(1 << Scalebits) * (2.0 * Maxscale) - Maxscale;
        }

        private void CopyToOriginal(int[,] temp)
        {
            for (int y = 0; y < Original.GetLength(0); y++)
            {
                for (int x = 0; x < Original.GetLength(1); x++)
                {
                    int value = temp[y, x];
                    Original[y, x] = Convert.ToInt32(value);
                }
            }
        }

        private int[,] ApplyIzometry(int izoIndex, int[,] reducedDomain)
        {
            int[,] izometricDomain = new int[reducedDomain.GetLength(0), reducedDomain.GetLength(1)];

            if (izoIndex == 0) //identity
            {
                return reducedDomain;
            }
            else if (izoIndex == 1) //vertical mirror
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        izometricDomain[y, x] = reducedDomain[y, 7 - x];
                    }
                }
            }
            else if (izoIndex == 2) //horizontal mirror
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        izometricDomain[y, x] = reducedDomain[7 - y, x];
                    }
                }
            }
            else if (izoIndex == 3) // diagonala principala
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        izometricDomain[y, x] = reducedDomain[x, y];
                    }
                }
            }
            else if (izoIndex == 4) // diagonala secundara
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        izometricDomain[y, x] = reducedDomain[7 - x, 7 - y];
                    }
                }
            }
            else if (izoIndex == 5) //rotate 90 degress  clockwise
            {
                for (int y = 8 - 1; y > 0; y--)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        izometricDomain[y, x] = reducedDomain[y, x];
                    }
                }
            }
            else if (izoIndex == 6) //rotate 180 degress clockwise
            {
                for (int y = 8 - 1; y > 0; y--)
                {
                    for (int x = 8 - 1; x > 0; x--)
                    {
                        izometricDomain[y, x] = reducedDomain[y, x];
                    }
                }
            }
            else if (izoIndex == 7) //rotate 90 degrees counter-clockwise
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 8 - 1; x > 0; x--)
                    {
                        izometricDomain[y, x] = reducedDomain[y, x];
                    }
                }
            }

            return izometricDomain;
        }

        private int[,] GetReducedDomain(int xd, int yd)
        {
            int[,] domain = new int[8,8];

            for (int y = 0; y < 16; y+=2)
            {
                for (int x = 0; x < 16; x+=2)
                {
                    int value = (Original[y + yd    , x + xd    ] +
                                 Original[y + yd    , x + xd + 1] +
                                 Original[y + yd + 1, x + xd    ] +
                                 Original[y + yd + 1, x + xd + 1]) / 4;
                    domain[y/2, x/2] = value;
                }
            }
            return domain;
        }

        public double CalculatePsnFor(byte[,] matrix)
        {
            Int64 sum = 0;
            Int64 originalSum = 0;
            var max = Byte.MinValue;
            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    sum += (long)Math.Pow(matrix[y,x] - Original[y,x], 2);
                    if (matrix[y, x] > max)
                        max = matrix[y, x];
                }
            }

            for (int i = 0; i < 512; i++)
            {
                for (int j = 0; j < 512; j++)
                {
                    originalSum += max*max;
                }
            }
            double psnr = 10 * Math.Log10(originalSum / sum);

            return psnr;
        }
    }

    public class FractalParameters
    {
        public int Xd;
        public int Yd;
        public int IzoIndex;
        public int Scale;
        public int Offset;
    }
}

