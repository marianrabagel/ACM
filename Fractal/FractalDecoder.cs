
using System;
using System.Drawing;
using ACM;

namespace Fractal
{
    public class FractalDecoder
    {
        protected byte[,] Original;
        protected byte[] BmpHeader;
        int Size = 512;
        FractalParameters[,] fractalParameters;

        protected string outputFileName;

        public FractalDecoder()
        {
            BmpHeader = new byte[1078];
            Original = new byte[Size, Size];
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

        int[,] temp;

        public void Decode()
        {
            temp = new int[Size, Size];
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    temp[y, x] = Original[y, x];
                }
            }

            for (int y = 0; y < fractalParameters.GetLength(0); y++)
            {
                for (int x = 0; x < fractalParameters.GetLength(1); x++)
                {
                    ApplyUsingIzometry(fractalParameters[y, x],y,x);
                }
            }

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    int value = temp[y, x];

                    if (value < 0)
                        value = 0;
                    if (value > 255)
                        value = 255;
                    Original[y, x] = Convert.ToByte(value);
                }
            }
        }

        private void ApplyUsingIzometry(FractalParameters fractalParameter,int y, int x)
        {
            int xd = fractalParameter.Xd;
            int yd = fractalParameter.Yd;
            int izoIndex = fractalParameter.IzoIndex;
            int scale = fractalParameter.Scale;
            int offset = fractalParameter.Offset;

            if (fractalParameter.IzoIndex == 0)
            {
                for (int yr = 0; yr < 8; yr++)
                {
                    for (int xr = 0; xr < 8; xr++)
                    {
                        temp[yd + yr, xd + xr] = Original[yd + yr, xd + xr]*scale + offset;
                    }
                }
            }
            else if (izoIndex == 1)
            {
                for (int yr = 0; yr < 8; yr++)
                {
                    for (int xr = 0; xr < 8; xr++)
                    {
                        temp[yd + yr, xd + xr] = Original[yr + yd, 7 - xr + xd] * scale + offset;
                    }
                }
            }
            else if (izoIndex == 2)
            {
                for (int yr = 0; yr < 8; yr++)
                {
                    for (int xr = 0; xr < 8; xr++)
                    {
                        temp[yd + yr, xd + xr] = Original[7 - yr + yd, xr + xd] * scale + offset;
                    }
                }
            }
            else if (izoIndex == 3)
            {
                for (int yr = 0; yr < 8; yr++)
                {
                    for (int xr = 0; xr < 8; xr++)
                    {
                        temp[yd + yr, xd + xr] = Original[xr + xd, yr + yd]*scale + offset;
                    }
                }
            }
            else if (izoIndex == 4)
            {
                for (int yr = 0; yr < 8; yr++)
                {
                    for (int xr = 0; xr < 8; xr++)
                    {
                        temp[yd + yr, xd + xr] = Original[8 - xr + xd, 7 - yr + yd] * scale + offset;
                    }
                }
            }
            else if (izoIndex == 5)
            {
                for (int yr = 8 - 1; yr > 0; yr--)
                {
                    for (int xr = 0; xr < 8; xr++)
                    {
                        temp[7 - yd + yr, xd + xr] = Original[yr + yd, xr + xd]*scale + offset;
                    }
                }
            }
            else if (izoIndex == 6)
            {
                for (int yr = 8 - 1; yr > 0; yr--)
                {
                    for (int xr = 8 - 1; xr > 0; xr--)
                    {
                        temp[7 - yd + yr, 7 - xd + xr] = Original[yr + yd, xr + xd] * scale + offset;
                    }
                }
            }
            else if (izoIndex == 7)
            {
                for (int yr = 0; yr < 8; yr++)
                {
                    for (int xr = 8 - 1; xr > 0; xr--)
                    {
                        temp[yd + yr, 7 - xd + xr] = Original[yr + yd, xr + xd] * scale + offset;
                    }
                }
            }
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

