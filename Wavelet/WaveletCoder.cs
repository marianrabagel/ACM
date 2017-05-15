using System.IO;
using ACM;

namespace Wavelet
{
    public class WaveletCoder : WaveletBase
    {
        protected double[] analysisL;
        protected double[] analysisH;

        public WaveletCoder(int size) : base(size)
        {
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

        public void LoadFile(string inputFileName)
        {
            this.inputFileName = inputFileName;
            ReadBmpHeaderAndLoadImageToMemory(inputFileName);
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

        public void AnH1()
        {
            for (int y = 0; y < Size; y++)
            {
                double[] anH = AnalysisHighHorizontal(y, Size, Original);
                double[] anL = AnalysisLowHorizontal(y, Size, Original);
                double[] reorderedLine = ReorderH(anL, anH);

                for (int x = 0; x < Size; x++)
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
            double[,] convertedMatrix = ConvertMatrixFromByteToDouble(matrix);
            return ApplyCuantizorHorizontal(line, length, analysisH, convertedMatrix);
        }

        public double[] AnalysisHighHorizontal(int line, int length, double[,] matrix)
        {
            return ApplyCuantizorHorizontal(line, length, analysisH, matrix);
        }

        public double[] AnalysisLowHorizontal(int line, int length, byte[,] matrix)
        {
            double[,] convertedMatrix = ConvertMatrixFromByteToDouble(matrix);
            return ApplyCuantizorHorizontal(line, length, analysisL, convertedMatrix);
        }

        private double[,] ConvertMatrixFromByteToDouble(byte[,] matrix)
        {
            double[,] convertedMatrix = new double[matrix.GetLength(0), matrix.GetLength(0)];

            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    convertedMatrix[y, x] = matrix[y, x];
                }
            }
            return convertedMatrix;
        }

        public double[] AnalysisLowHorizontal(int line, int length, double[,] matrix)
        {
            return ApplyCuantizorHorizontal(line, length, analysisL, matrix);
        }

        private double[] AnalysisLowVertical(int column, int length, double[,] matrix)
        {
            return ApplyCuantizorVertical(column, length, analysisL, matrix);
        }

        private double[] AnalysisHighVertical(int column, int length, double[,] matrix)
        {
            return ApplyCuantizorVertical(column, length, analysisH, matrix);
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

        public void AnV1()
        {
            AnalysisVertical(Size);

            /*
            //for testing only. TO see if the vertical works on a vertical line from a test matrix.
            for (int x = 0; x < Size; x++)
            {
                double[] anH = AnalysisHighVertical(x, Size, Original);
                double[] anL = AnalysisLowVertical(x, Size, Original);
                double[] reorderedLine = ReorderH(anL, anH);

                for (int y = 0; y < Size; y++)
                    WaveletMatrix[y, x] = reorderedLine[y];
            }*/
        }

        public void AnV2()
        {
            AnalysisVertical(Size/2);
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

        public void Load(byte[,] testMatrix)
        {
            for (int y = 0; y < testMatrix.GetLength(0); y++)
            {
                for (int x = 0; x < testMatrix.GetLength(0); x++)
                {
                    Original[y, x] = testMatrix[y, x];
                }
            }
        }

        public void Save(string outputFileName)
        {
            using (StreamWriter writer = new StreamWriter(outputFileName))
            {
                for (int i = 0; i < Size; i++)
                {
                    writer.WriteLine(WaveletMatrix[0,i]);
                }
            }
        }
    }   
}
