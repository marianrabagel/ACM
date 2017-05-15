
namespace Wavelet
{
    public class WaveletDecoder : WaveletBase
    {
        double[] synthesisL;
        double[] synthesisH;

        public WaveletDecoder(int size) : base(size)
        {
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
        }

        

        private double[] SynthesisHighHorizontal(int length, double[] low)
        {
            return ApplyCuantizor(length, synthesisH, low);
        }

        private double[] SynthesisLowHorizontal(int length, double[] high)
        {
            return ApplyCuantizor(length, synthesisL, high);
        }

        private double[] ConstructHighHorizontal(int y, int length)
        {
            double[] high = new double[length];
            for (int x = 0; x < length/2; x++)
            {
                high[x*2] = 0;
                high[x*2 + 1] = WaveletMatrix[y, length/2 + x];
            }
            return high;
        }

        private double[] ConstructLowHorizontal(int y, int length)
        {
            double[] low = new double[length];
            for (int x = 0; x < length/2; x++)
            {
                low[x*2] = WaveletMatrix[y, x];
                low[x*2 + 1] = 0;
            }

            return low;
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
            return  ApplyCuantizor(length, synthesisL, low);
        }

        private double[] SynthesisHighVertical(int length, double[] high)
        {
            return ApplyCuantizor(length, synthesisH, high);
        }

        public void SyH1()
        {
            SynthesisHorizontal(Size);
        }

        public void SyH2()
        {
            SynthesisHorizontal(Size/2);
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

        public void SyV1()
        {
            SynthesysVertical(Size);
        }

        public void SyV2()
        {
            SynthesysVertical(Size/2);
        }

        public void SyV3()
        {
            SynthesysVertical(Size/ 4);
        }

        public void SyV4()
        {
            SynthesysVertical(Size/8);
        }

        public void SyV5()
        {
            SynthesysVertical(Size/16);
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
    }
}
