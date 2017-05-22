using System;

namespace ACM
{
    public class PredictiveCoder : PredictiveBase
    {
        protected int entropicCoding;

        public PredictiveCoder(string inputFile) : base(inputFile)
        {
        }

        public void Encode(int predictionRule, int k, string entropicCoder)
        {
            if (entropicCoder.Contains("Fixed"))
            {
                entropicCoder = "F";
                entropicCoding = 0;
            }
            else if (entropicCoder.Contains("Table"))
            {
                entropicCoder = "T";
                entropicCoding = 1;
            }
            else
            {
                entropicCoder = "A";
                entropicCoding = 2;
            }

            this.predictionRule = predictionRule;
            this.k = k;

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
                }
            }
        }

        private void ReadBmpHeaderAndLoadImageToMemory()
        {
            using (BitReader reader = new BitReader(inputFileName))
            {
                ReadBmpHeader(reader);

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
                writer.WriteNBiti(Convert.ToUInt32(predictionRule), 4);
                writer.WriteNBiti(Convert.ToUInt32(k), 4);
                writer.WriteNBiti(Convert.ToUInt32(entropicCoding), 2);

                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        uint value = Convert.ToUInt32(ErrorPq[y, x] + 255);
                        writer.WriteNBiti(value, 9);
                    }
                }

                writer.WriteNBiti(0, 7);
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

                writer.WriteNBiti(0, 7);
            }
        }

        private void SaveErrorPQUsingArithmetic()
        {
            throw new NotImplementedException();
        }
    }
}
