
using System;
using System.Drawing;
using System.IO;
using ACM;

namespace Wavelet
{
    public class WaveletBase
    {
        protected byte[] BmpHeader;
        public byte[,] Original;
        public double[,] WaveletMatrix;
        public int Size { get; }

        protected double[] analysisL;
        protected double[] analysisH;

        double[] synthesisL;
        double[] synthesisH;

        public double max;
        public double min;

        public string OutputFile { get; private set; }
        //protected double[,] scaledMatrix;

        public WaveletBase(int size)
        {
            Size = size;
            BmpHeader = new byte[1078];
            Original = new byte[Size, Size];
            WaveletMatrix = new double[Size, Size];

            analysisL = new[]
           {
                0.026748757411,
                -0.016864118443,
                -0.078223266529,
                0.266864118443,
                0.602949018236,
                0.266864118443,
                -0.078223266529,
                -0.016864118443,
                0.026748757411
            };
            analysisH = new[]
            {
                0,
                0.091271763114,
                -0.057543526229,
                -0.591271763114,
                1.115087052457,
                -0.591271763114,
                -0.057543526229,
                0.091271763114,
                0
            };

            synthesisL = new double[]
            {
                0.000000000000
                , -0.091271763114
                , -0.057543526229
                , 0.591271763114
                , 1.115087052457
                , 0.591271763114
                , -0.057543526229
                , -0.091271763114
                , 0.000000000000
            };
            synthesisH = new double[]
            {
                0.026748757411
                , 0.016864118443
                , -0.078223266529
                , -0.266864118443
                , 0.602949018236
                , -0.266864118443
                , -0.078223266529
                , 0.016864118443
                , 0.026748757411
            };


            InitializeMatrices();
        }

        private void InitializeMatrices()
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    Original[y, x] = 0;
                    WaveletMatrix[y, x] = 0;
                }
            }
        }

        public void ReadBmpHeader(BitReader reader)
        {
            for (int i = 0; i < 1078; i++)
                BmpHeader[i] = (byte)reader.ReadNBit(8);
        }

        public void LoadBmpFile(string inputFileName)
        {
            OutputFile = inputFileName + ".wvm";
            ReadBmpHeaderAndLoadImageToMemory(inputFileName);
            CopyOriginalToWavelet();
        }

        private void CopyOriginalToWavelet()
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    WaveletMatrix[y, x] = Original[y, x];
                }
            }
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

        public void AnH1()
        {
            AnalysisHorizontal(Size);
        }

        public void AnH2()
        {
            AnalysisHorizontal(Size / 2);
        }

        public void AnH3()
        {
            AnalysisHorizontal(Size / 4);
        }

        public void AnH4()
        {
            AnalysisHorizontal(Size / 8);
        }

        public void AnH5()
        {
            AnalysisHorizontal(Size / 16);
        }

        public void AnalysisHorizontal(int size)
        {
            for (int y = 0; y < size; y++)
            {
                double[] anH = AnalysisHighHorizontal(y, size, WaveletMatrix);
                double[] anL = AnalysisLowHorizontal(y, size, WaveletMatrix);
                double[] reorderedLine = ReorderH(anL, anH);

                for (int x = 0; x < size; x++)
                    WaveletMatrix[y, x] = reorderedLine[x];
            }
        }

        public double[] AnalysisHighHorizontal(int line, int length, double[,] matrix)
        {
            return ApplyFilterHorizontal(line, length, analysisH, matrix);
        }

        public double[] AnalysisLowHorizontal(int line, int length, double[,] matrix)
        {
            return ApplyFilterHorizontal(line, length, analysisL, matrix);
        }

        public double[] ReorderH(double[] anL, double[] anH)
        {
            double[] reorderedLine = new double[anH.Length];
            for (int i = 0; i < anL.Length; i++)
            {
                if (i % 2 == 0)
                    reorderedLine[i / 2] = anL[i];
                else
                    reorderedLine[i / 2 + anL.Length / 2] = anH[i];
            }

            return reorderedLine;
        }

        public void AnV1()
        {
            AnalysisVertical(Size);
        }

        public void AnV2()
        {
            AnalysisVertical(Size / 2);
        }

        public void AnV3()
        {
            AnalysisVertical(Size / 4);
        }

        public void AnV4()
        {
            AnalysisVertical(Size / 8);
        }

        public void AnV5()
        {
            AnalysisVertical(Size / 16);
        }

        public void AnalysisVertical(int size)
        {
            for (int x = 0; x < size; x++)
            {
                double[] anH = AnalysisHighVertical(x, size, WaveletMatrix);
                double[] anL = AnalysisLowVertical(x, size, WaveletMatrix);
                double[] reorderedLine = ReorderH(anL, anH);

                for (int y = 0; y < size; y++)
                    WaveletMatrix[y, x] = reorderedLine[y];
            }
        }

        private double[] AnalysisHighVertical(int column, int length, double[,] matrix)
        {
            return ApplyFIlterVertical(column, length, analysisH, matrix);
        }

        private double[] AnalysisLowVertical(int column, int length, double[,] matrix)
        {
            return ApplyFIlterVertical(column, length, analysisL, matrix);
        }

        public void ApplyScale(double scale, int offset, int startingPositionX, int startingPositionY)
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x >= startingPositionX)
                    {
                        WaveletMatrix[y, x]  = WaveletMatrix[y, x] * scale + offset;
                    }
                    if(y >= startingPositionY)
                    {
                        WaveletMatrix[y, x] = WaveletMatrix[y, x]*scale + offset;
                    }
                }
            }
        }

        public void SyH1()
        {
            SynthesisHorizontal(Size);
        }

        public void SyH2()
        {
            SynthesisHorizontal(Size / 2);
        }

        public void SyH3()
        {
            SynthesisHorizontal(Size / 4);
        }

        public void SyH4()
        {
            SynthesisHorizontal(Size / 8);
        }

        public void SyH5()
        {
            SynthesisHorizontal(Size / 16);
        }

        public void SynthesisHorizontal(int size)
        {
            for (int y = 0; y < size; y++)
            {
                double[] low = ConstructLowHorizontal(y, size);
                double[] high = ConstructHighHorizontal(y, size);

                double[] syL = SynthesisLowHorizontal(size, low);
                double[] syH = SynthesisHighHorizontal(size, high);

                for (int x = 0; x < syH.Length; x++)
                    WaveletMatrix[y, x] = syL[x] + syH[x];
            }
        }

        private double[] ConstructHighHorizontal(int y, int length)
        {
            double[] high = new double[length];
            for (int x = 0; x < length / 2; x++)
            {
                high[x * 2] = 0;
                high[x * 2 + 1] = WaveletMatrix[y, length / 2 + x];
            }
            return high;
        }

        private double[] ConstructLowHorizontal(int y, int length)
        {
            double[] low = new double[length];
            for (int x = 0; x < length / 2; x++)
            {
                low[x * 2] = WaveletMatrix[y, x];
                low[x * 2 + 1] = 0;
            }

            return low;
        }

        private double[] SynthesisLowHorizontal(int length, double[] high)
        {
            return ApplyCuantizor(length, synthesisL, high);
        }

        private double[] SynthesisHighHorizontal(int length, double[] low)
        {
            return ApplyCuantizor(length, synthesisH, low);
        }

        public void SyV1()
        {
            SynthesysVertical(Size);
        }

        public void SyV2()
        {
            SynthesysVertical(Size / 2);
        }

        public void SyV3()
        {
            SynthesysVertical(Size / 4);
        }

        public void SyV4()
        {
            SynthesysVertical(Size / 8);
        }

        public void SyV5()
        {
            SynthesysVertical(Size / 16);
        }

        public void SynthesysVertical(int size)
        {
            for (int x = 0; x < size; x++)
            {
                double[] low = ConstructLowVertical(x, size);
                double[] high = ConstructHighVertical(x, size);

                double[] syL = SynthesisLowVertical(size, low);
                double[] syH = SynthesisHighVertical(size, high);

                for (int y = 0; y < syH.Length; y++)
                    WaveletMatrix[y, x] = syL[y] + syH[y];
            }
        }

        private double[] ConstructLowVertical(int column, int length)
        {
            double[] anL = new double[length];
            for (int y = 0; y < length / 2; y++)
            {
                anL[y * 2] = WaveletMatrix[y, column];
                anL[y * 2 + 1] = 0;
            }

            return anL;
        }

        private double[] ConstructHighVertical(int column, int length)
        {
            double[] anH = new double[length];
            for (int y = 0; y < length / 2; y++)
            {
                anH[y * 2] = 0;
                anH[y * 2 + 1] = WaveletMatrix[length / 2 + y, column];
            }
            return anH;
        }

        private double[] SynthesisLowVertical(int length, double[] low)
        {
            return ApplyCuantizor(length, synthesisL, low);
        }

        private double[] SynthesisHighVertical(int length, double[] high)
        {
            return ApplyCuantizor(length, synthesisH, high);
        }

        protected double[] ApplyFIlterVertical(int column, int length, double[] filter, double[,] source)
        {
            double[] result = new double[length];

            for (int x = 0; x < length - 4; x++)
            {
                double sum = 0;
                for (int i = 0; i < 9; i++)
                {
                    int index = Math.Abs(x + i - 4);
                    sum += source[index, column] * filter[i];
                }
                result[x] = sum;
            }

            for (int x = length - 4; x < length; x++)
            {
                double sum = 0;
                for (int i = 0; i < 9; i++)
                {
                    int index = Math.Abs(x + i - 4);

                    if (index > length - 1)
                        index = length - 1 - (index % (length - 1));
                    sum += source[index, column] * filter[i];
                }
                result[x] = sum;
            }

            return result;
        }

        /// <summary>
        /// For coder. For the first time, when it applys Cuantizor on the original image (which is byte)
        /// </summary>
        protected double[] ApplyFilterHorizontal(int line, int length, double[] filter, double[,] source)
        {
            double[] result = new double[length];

            for (int x = 0; x < length - 4; x++)
            {
                double sum = 0;
                for (int i = 0; i < 9; i++)
                {
                    int index = Math.Abs(x + i - 4);
                    sum += source[line, index] * filter[i];
                }
                result[x] = sum;
            }

            for (int x = length - 4; x < length; x++)
            {
                double sum = 0;
                for (int i = 0; i < 9; i++)
                {
                    int index = Math.Abs(x + i - 4);

                    if (index > length - 1)
                        index = length - 1 - (index % (length - 1));
                    sum += source[line, index] * filter[i];
                }
                result[x] = sum;
            }

            return result;
        }

        /// <summary>
        /// For decoder. It applies the syntesis to one line, not on the matrix
        /// </summary>
        protected double[] ApplyCuantizor(int length, double[] cuantizor, double[] source)
        {
            double[] result = new double[length];

            for (int x = 0; x < length - 4; x++)
            {
                double sum = 0;
                for (int i = 0; i < 9; i++)
                {
                    int index = Math.Abs(x + i - 4);
                    sum += source[index] * cuantizor[i];
                }
                result[x] = sum;
            }

            for (int x = length - 4; x < length; x++)
            {
                double sum = 0;
                for (int i = 0; i < 9; i++)
                {
                    int index = Math.Abs(x + i - 4);

                    if (index > length - 1)
                        index = length - 1 - (index % (length - 1));
                    sum += source[index] * cuantizor[i];
                }
                result[x] = sum;
            }

            return result;
        }

        public Bitmap GetBitmap()
        {
            double[,] matrix = WaveletMatrix;
            Bitmap bitmap = new Bitmap(matrix.GetLength(1), matrix.GetLength(0));
            for (int y = 0; y < matrix.GetLength(1); y++)
            {
                for (int x = 0; x < matrix.GetLength(0); x++)
                {
                    int value = Convert.ToInt32(Math.Round(matrix[y, x]));
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

        public void LoadWvmFile(string inputFileName)
        {
            OutputFile = inputFileName + ".bmp";

            using (StreamReader reader = new StreamReader(inputFileName))
            {
                for (int y = 0; y < Size; y++)
                {
                    for (int x = 0; x < Size; x++)
                    {
                        WaveletMatrix[y, x] = Convert.ToDouble(reader.ReadLine());
                    }
                }
            }
        }

        public void CalculateMinMax()
        {
            max = double.MinValue;
            min = double.MaxValue;

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    var val = Original[y, x] - Math.Round(WaveletMatrix[y, x]);
                    if (val < min)
                        min = val;
                    if (val > max)
                        max = val;
                }
            }
        }

        public void Load(double[,] testMatrixWithFirstLineWithInfo)
        {
            WaveletMatrix = testMatrixWithFirstLineWithInfo;
        }
    }
}
