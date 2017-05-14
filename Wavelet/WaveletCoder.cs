
using System;
using ACM;

namespace Wavelet
{
    public class WaveletCoder : WaveletBase
    {
        public void Encode(string fileName)
        {
            ReadBmpHeaderAndLoadImageToMemory(fileName);
        }

        private void ReadBmpHeaderAndLoadImageToMemory(string fileName)
        {
            using (BitReader reader = new BitReader(fileName))
            {
                ReadBmpHeader(reader);

                for (int y = size - 1; y >= 0; y--)
                    for (int x = 0; x < size; x++)
                        Original[y, x] = (byte)reader.ReadNBit(8);
            }
        }

        public void AnH1()
        {
            for (int y = 0; y < size; y++)
            {
                double[] anH = AnalysisHighHorizontal(y, size, Original);
                double[] anL = AnalysisLowHorizontal(y, size, Original);
                double[] reorderedLine = ReorderH(anL, anH);

                for (int x = 0; x < size; x++)
                    WaveletMatrix[y, x] = reorderedLine[x];
            }
        }

        public double[] ReorderH(double[] anL, double[] anH)
        {
            double[] reorderedLine = new double[anH.Length];
            for (int i = 0; i < anL.Length; i++)
            {
                if (i%2 == 0)
                    reorderedLine[i/2] = anL[i];
                else
                    reorderedLine[i/2 + anL.Length/ 2] = anH[i];
            }

            return reorderedLine;
        }

        public double[] AnalysisHighHorizontal(int line, int length, byte[,] matrix)
        {
            return ApplyCuantizorHorizontal(line, length, analysisH, matrix);
        }

        public double[] AnalysisLowHorizontal(int line, int length, byte[,] matrix)
        {
            return ApplyCuantizorHorizontal(line, length, analysisL, matrix);
        }

        private double[] ApplyCuantizorHorizontal(int line, int length, double[] cuantizor, byte[,] source)
        {
            double[] anH = new double[length];

            for (int x = 0; x < length - 4; x++)
            {
                double sum = 0;
                for (int i = 0; i < 9; i++)
                {
                    int index = Math.Abs(x + i - 4);
                    sum += source[line, index]*cuantizor[i];
                }
                anH[x] = sum;
            }

            for (int x = length - 4; x < length; x++)
            {
                double sum = 0;
                for (int i = 0; i < 9; i++)
                {
                    int index = Math.Abs(x + i - 4);

                    if (index > length - 1)
                        index = length - 1 - (index%(length - 1));
                    sum += source[line, index]*cuantizor[i];
                }
                anH[x] = sum;
            }

            return anH;
        }

        public void AnV1()
        {
            for (int x = 0; x < size; x++)
            {
                double[] anH = AnalysisHighVertical(x, size, Original);
                double[] anL = AnalysisLowVertical(x, size, Original);
                double[] reorderedLine = ReorderH(anL, anH);

                for (int y = 0; y < size; y++)
                    WaveletMatrix[y, x] = reorderedLine[y];
            }
        }

        private double[] AnalysisLowVertical(int column, int length, byte[,] matrix)
        {
            return ApplyCuantizorVertical(column, length, analysisL, WaveletMatrix);
        }

        private double[] AnalysisHighVertical(int column, int length, byte[,] matrix)
        {
            return ApplyCuantizorVertical(column, length, analysisH, WaveletMatrix);
        }

        private double[] ApplyCuantizorVertical(int column, int length, double[] cuantizor, double[,] source)
        {
            double[] anH = new double[length];

            for (int x = 0; x < length - 4; x++)
            {
                double sum = 0;
                for (int i = 0; i < 9; i++)
                {
                    int index = Math.Abs(x + i - 4);
                    sum += source[index, column] * cuantizor[i];
                }
                anH[x] = sum;
            }

            for (int x = length - 4; x < length; x++)
            {
                double sum = 0;
                for (int i = 0; i < 9; i++)
                {
                    int index = Math.Abs(x + i - 4);

                    if (index > length - 1)
                        index = length - 1 - (index % (length - 1));
                    sum += source[index, column] * cuantizor[i];
                }
                anH[x] = sum;
            }

            return anH;
        }
    }   
}
