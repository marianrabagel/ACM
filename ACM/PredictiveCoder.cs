using System;

namespace ACM
{
    public class PredictiveCoder : PredictiveBase
    {
        private int _minError;
        private int _maxError;

        public PredictiveCoder(string inputFile) : base(inputFile)
        {
            _minError = Int32.MaxValue;
            _maxError = Int32.MinValue;
        }

        public void Encode(int predictionRule, int k, string entropicCoder)
        {
            _minError = Int32.MaxValue;
            _maxError = Int32.MinValue;
            
            if (entropicCoder.Contains("Fixed"))
                entropicCoder = "F";
            else if (entropicCoder.Contains("Table"))
            {
                entropicCoder = "T";
            }
            else
                entropicCoder = "A";

            outputFileName = inputFileName + ".p" + predictionRule + "k" + k.ToString().PadLeft(2, '0') + entropicCoder +
                             ".prd";
            ReadBmpHeaderAndLoadImageToMemory();

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    byte predictionValue = GetPredictionFor(predictionRule, x, y);
                    Prediction[y, x] = predictionValue;
                    ErrorP[y, x] = Original[y, x] - predictionValue;
                    ErrorPq[y, x] = Convert.ToInt32(Math.Floor((ErrorP[y, x] + k) / (double) (2 * k + 1)));
                    ErrorPdq[y, x] = ErrorPq[y, x] * (2 * k + 1);
                    int decodedValue = ErrorPdq[y, x] + Prediction[y, x];

                    if (decodedValue < 0)
                        decodedValue = 0;
                    if (decodedValue > 255)
                        decodedValue = 255;

                    Decoded[y, x] = (byte) decodedValue;
                    SetMinOrMaxError(y, x);
                }
            }
        }

        private void ReadBmpHeaderAndLoadImageToMemory()
        {
            using (BitReader reader = new BitReader(inputFileName))
            {
                for (int i = 0; i < 1078; i++)
                    bmpHeader[i] = (byte) reader.ReadNBit(8);

                for (int y = size - 1; y >= 0; y--)
                    for (int x = 0; x < size; x++)
                        Original[y, x] = (byte) reader.ReadNBit(8);
            }
        }

        public int[,] ApplyScale(int[,] matrix, double scale)
        {
            int[,] newMatrix = new int[matrix.GetLength(1), matrix.GetLength(0)];

            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    newMatrix[y, x] = (int) (matrix[y, x] * scale);
                }
            }

            return newMatrix;
        }

        public int[] GetFrequencies(byte[,] matrix, int[] frequencies)
        {
            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    int indexValue = matrix[y, x] + 255;
                    frequencies[indexValue]++;
                }
            }

            return frequencies;
        }

        public int[] GetFrequencies(int[,] matrix, int[] frequencies)
        {
            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    int indexValue = matrix[y, x] + 255;
                    frequencies[indexValue]++;
                }
            }

            return frequencies;
        }

        public void SaveEncodedFile(int statisticModelIndex)
        {
            if (statisticModelIndex == 0)
                SaveErrorPqUsingFixed9Bits();
            if (statisticModelIndex == 1)
                SaveErrorPqUsingJpegTable();
            if (statisticModelIndex == 2)
                SaveErrorPQUsingArithmetic();
        }

        private void SaveErrorPqUsingFixed9Bits()
        {
            using (BitWriter writer = new BitWriter(outputFileName))
            {
                WriteBmpHeader(writer);

                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        uint value = Convert.ToUInt32(ErrorPq[y, x] + 255);
                        writer.WriteNBiti(value, 9);
                    }
                }
            }
        }

        private void SaveErrorPqUsingJpegTable()
        {
            using (BitWriter writer = new BitWriter(outputFileName))
            {
                WriteBmpHeader(writer);

                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        JpegCoding jpegCoding = GetCodingFor(ErrorPq[y, x]);
                        writer.WriteNBiti(jpegCoding.Coding, jpegCoding.Length);
                    }
                }
            }
        }

        private void SaveErrorPQUsingArithmetic()
        {
            throw new NotImplementedException();
        }

        

        public int GetMinError()
        {
            if (_minError == Int32.MaxValue)
                return 0;

            return _minError;
        }

        private void SetMinOrMaxError(int y, int x)
        {
            int error = Original[y, x] - Decoded[y, x];

            if (error < _minError)
                _minError = error;
            if (error > _maxError)
                _maxError = error;
        }

        public int GetMaxError()
        {
            if (_maxError == Int32.MinValue)
                return 0;

            return _maxError;
        }
    }
}
