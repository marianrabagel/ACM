
using System;
using System.Drawing;
using System.Web;
using ACM;

namespace Wavelet
{
    public class Wavelet
    {
        byte[] bmpHeader;
        public byte[,] Original;
        public double[,] WaveletMatrix;
        int size = 256;
        double[] analysisL;
        public double[] analysisH;

        public Wavelet()
        {
            bmpHeader = new byte[1078];
            Original = new byte[size, size];
            WaveletMatrix = new double[size, size];
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
        }

        public void Decode(string fileName)
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

        public void ReadBmpHeader(BitReader reader)
        {
            for (int i = 0; i < 1078; i++)
                bmpHeader[i] = (byte)reader.ReadNBit(8);
        }

        public void AnH1()
        {
            for (int y = 0; y < size; y++)
            {
                double[] anH = AnalysisH(y, size, Original);
                double[] anL = AnalysisL(y, size, Original);
                double[] reorderedLine = Reorder(anL, anH);

                for (int x = 0; x < size; x++)
                {
                    WaveletMatrix[y, x] = reorderedLine[x];
                }
            }
        }

        public double[] Reorder(double[] anL, double[] anH)
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

        public double[] AnalysisH(int line, int length, byte[,] matrix)
        {
            return ApplyCuantizor(line, length, analysisH, matrix);
        }

        public double[] AnalysisL(int line, int length, byte[,] matrix)
        {
            return ApplyCuantizor(line, length, analysisL, matrix);
        }

        private double[] ApplyCuantizor(int line, int length, double[] cuantizor, byte[,] source)
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
    }   
}
