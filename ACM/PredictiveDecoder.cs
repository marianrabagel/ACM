
using System;
using System.IO;

namespace ACM
{
    class PredictiveDecoder : PredictiveBase
    {
        int predictionRule;
        private int k;
        private string entropicCoder;

        public PredictiveDecoder(string inputFileName) : base(inputFileName)
        {
            outputFileName = inputFileName + ".bmp";
        }

        public void LoadPrdFile()
        {
            string filename = Path.GetFileName(inputFileName);
            filename = filename.Substring(filename.IndexOf(".bmp.p") + 6);
            predictionRule = Convert.ToInt32(filename.Substring(0, filename.IndexOf("k")));
            k = Convert.ToInt32(filename.Substring(filename.IndexOf("k") + 1, 2));
            entropicCoder = filename.Substring(filename.IndexOf("k" + k) + 5, 1);
            ReadBmpHeaderAndLoadPredictionToMemory();
        }

        private void ReadBmpHeaderAndLoadPredictionToMemory()
        {
            using (BitReader reader = new BitReader(inputFileName))
            {
                for (int i = 0; i < 1078; i++)
                    bmpHeader[i] = (byte)reader.ReadNBit(8);

                for (int y = 0; y < size; y++)
                    for (int x = 0; x < size; x++)
                        ErrorPq[y, x] = GetValueFromFile(reader, entropicCoder) - 255;
            }
        }

        private int GetValueFromFile(BitReader reader, string coder)
        {
            if (coder == "F")
                return Convert.ToInt32(reader.ReadNBit(9));
            if (coder == "T")
            {
                return 0;
            }
            if (coder == "A")
                return 0;

            throw new Exception("Codorul nu a fost setat");
        }

        public void Decode()
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    byte predictionValue = GetPredictionFor(predictionRule, x, y);
                    Prediction[y, x] = predictionValue;
                    ErrorPdq[y, x] = ErrorPq[y, x] * (2 * k + 1);
                    int decodedValue = (ErrorPdq[y, x] + Prediction[y, x]);

                    if (decodedValue < 0)
                        decodedValue = 0;
                    if (decodedValue > 255)
                        decodedValue = 255;

                    Decoded[y, x] = (byte)decodedValue;
                }
            }
        }

        public void SaveDecodedFile()
        {
            using (BitWriter writer = new BitWriter(outputFileName))
            {
                foreach (byte b in bmpHeader)
                    writer.WriteNBiti(b, 8);

                for (int y = size - 1; y > 0; y--)

                //for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        uint value = Decoded[y, x];
                        writer.WriteNBiti(value, 8);
                    }
                }
            }
        }
    }
}
