
using ACM;

namespace Wavelet
{
    public class WaveletBase
    {
        protected byte[] bmpHeader;
        protected byte[,] Original;
        protected double[,] WaveletMatrix;
        protected int size = 256;

        protected double[] analysisL;
        protected double[] analysisH;

        public WaveletBase()
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

        public void ReadBmpHeader(BitReader reader)
        {
            for (int i = 0; i < 1078; i++)
                bmpHeader[i] = (byte)reader.ReadNBit(8);
        }
    }
}
