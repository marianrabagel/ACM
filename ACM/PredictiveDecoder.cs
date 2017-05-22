using System;

namespace ACM
{
    class PredictiveDecoder : PredictiveBase
    {
        protected string entropicCoder;

        public PredictiveDecoder(string inputFileName) : base(inputFileName)
        {
            OutputFileName = inputFileName + ".bmp";
        }

        public void LoadPrdFile()
        {
            ReadBmpHeaderAndLoadPredictionToMemory();
        }

        private void ReadBmpHeaderAndLoadPredictionToMemory()
        {
            using (BitReader reader = new BitReader(InputFileName))
            {
                ReadBmpHeader(reader);
                PredictionRule = (int) reader.ReadNBit(4);
                K = (int) reader.ReadNBit(4);
                int entropicCoding = (int) reader.ReadNBit(2);

                if (entropicCoding == 0)
                    entropicCoder = "F";
                else if (entropicCoding == 1)
                    entropicCoder = "T";
                else if (entropicCoding == 2)
                    entropicCoder = "A";

                for (int y = 0; y < Size; y++)
                    for (int x = 0; x < Size; x++)
                        ErrorPq[y, x] = GetValueFromFile(reader, entropicCoder);
            }
        }

        private int GetValueFromFile(BitReader reader, string entropicCoder)
        {
            if (entropicCoder == "F")
                return Convert.ToInt32(reader.ReadNBit(9)) - 255;
            if (entropicCoder == "T")
            {
                uint coding = 0;
                byte bit = reader.ReadBit();

                if (bit == 0)
                    return 0;

                int ct = 0;

                while (bit != 0)
                {
                    //helper
                    coding += 1;
                    coding = coding << 1;
                    //helper
                    bit = reader.ReadBit();
                    ct++;
                }

                int index = Convert.ToInt32(reader.ReadNBit(ct));
                if (index < Math.Pow(2, ct - 1))
                    return Convert.ToInt32(index - (Math.Pow(2, ct) - 1));
                else
                    return index;
            }
            if (entropicCoder == "A")
                return 0;

            throw new Exception("Codorul nu a fost setat");
        }

        public void Decode()
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    byte predictionValue = GetPredictionFor(PredictionRule, x, y);
                    Prediction[y, x] = predictionValue;
                    ErrorPdq[y, x] = ErrorPq[y, x] * (2 * K + 1);
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
            using (BitWriter writer = new BitWriter(OutputFileName))
            {
                WriteBmpHeader(writer);

                for (int y = Size - 1; y >= 0; y--)
                {
                    for (int x = 0; x < Size; x++)
                    {
                        uint value = Decoded[y, x];
                        writer.WriteNBiti(value, 8);
                    }
                }
            }
        }
    }
}
