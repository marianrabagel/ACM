
using System.IO;

namespace ACM
{
    class PredictiveDecoder : PredictiveBase
    {
        public PredictiveDecoder(string inputFileName) :base(inputFileName)
        {
        }

        public void Decode()
        {
            string predictionRule = Path.GetExtension(inputFileName);
        }
    }
}
