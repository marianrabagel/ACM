using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM
{
    public class PredictiveBase
    {
        public string inputFileName;
        public string outputFileName;
        protected byte[] bmpHeader;
        public byte[,] Original { get; }
        public byte[,] Prediction { get; }
        public byte[,] Decoded { get; }
        public int[,] ErrorP { get; }
        public int[,] ErrorPq { get; }
        public int[,] ErrorPdq { get; }
        public int[,] Error { get; }
        protected int size = 256;

        public PredictiveBase(string inputFileName)
        {
            this.inputFileName = inputFileName;
            bmpHeader = new byte[1078];
            Original = new byte[size, size];
            Prediction = new byte[size, size];
            Decoded = new byte[size, size];
            ErrorP = new int[size, size];
            ErrorPq = new int[size, size];
            ErrorPdq = new int[size, size];
            Error = new int[size, size];
        }
        
        protected byte GetPredictionFor(string predictionRule, int x, int y)
        {
            if (x == 0 && y == 0)
                return 128;

            byte value = 128;

            if (predictionRule == "128")
            {
                value = 128;
            }
            else if (predictionRule == "A")
            {
                value = GetA(x, y);
            }
            else if (predictionRule == "B")
            {
                value = GetB(x, y);
            }
            else if (predictionRule == "C")
            {
                value = GetC(x, y);
            }
            else if (predictionRule == "A+B-C")
            {
                value = GetABC(x, y);
            }
            else if (predictionRule == "A+(B-C)/2")
            {
                value = GetABC2(x, y);
            }
            else if (predictionRule == "B+(A-C)/2")
            {
                value = GetBAC2(x, y);
            }
            else if (predictionRule == "(A+B)/2")
            {
                value = GetAB2(x, y);
            }
            else if (predictionRule == "JPEG-LS")
            {
                value = GetJpegLs(x, y);
            }

            return value;
        }

        private byte GetJpegLs(int x, int y)
        {
            byte value;

            if (x == 0)
                value = GetA(x, y);
            else if (y == 0)
                value = GetB(x, y);
            else
            {
                byte A = GetA(x, y);
                byte B = GetB(x, y);
                byte C = GetC(x, y);

                if (C >= Math.Max(A, B))
                    value = Math.Min(A, B);
                else if (C <= Math.Min(A, B))
                    value = Math.Max(A, B);
                else
                    value = GetABC(x, y);
            }

            return value;
        }

        private byte GetAB2(int x, int y)
        {
            byte value;

            if (x == 0)
                value = GetA(x, y);
            else if (y == 0)
                value = GetB(x, y);
            else
            {
                int val = (GetA(x, y) - GetB(x, y)) / 2;
                if (val > 255)
                    val = 255;
                if (val < 0)
                    val = 0;

                value = (byte)val;
            }

            return value;
        }

        private byte GetBAC2(int x, int y)
        {
            byte value;

            if (x == 0)
                value = GetA(x, y);
            else if (y == 0)
                value = GetB(x, y);
            else
            {
                int val = GetB(x, y) + (GetA(x, y) - GetC(x, y)) / 2;
                if (val > 255)
                    val = 255;
                if (val < 0)
                    val = 0;

                value = (byte)val;
            }

            return value;
        }

        private byte GetABC2(int x, int y)
        {
            byte value;

            if (x == 0)
                value = GetA(x, y);
            else if (y == 0)
                value = GetB(x, y);
            else
            {
                int val = GetA(x, y) + (GetB(x, y) - GetC(x, y)) / 2;
                if (val > 255)
                    val = 255;
                if (val < 0)
                    val = 0;

                value = (byte)val;
            }

            return value;
        }

        private byte GetABC(int x, int y)
        {
            byte value;

            if (x == 0)
                value = GetA(x, y);
            else if (y == 0)
                value = GetB(x, y);
            else
            {
                int val = GetA(x, y) + GetB(x, y) - GetC(x, y);
                if (val > 255)
                    val = 255;
                if (val < 0)
                    val = 0;

                value = (byte)val;
            }

            return value;
        }

        private byte GetC(int x, int y)
        {
            byte value;

            if (x == 0)
                value = GetA(x, y);
            else if (y == 0)
                value = GetB(x, y);
            else
            {
                value = Decoded[y - 1, x - 1];
            }

            return value;
        }

        private byte GetB(int x, int y)
        {
            return y == 0 ? (byte)0 : Decoded[y - 1, x];
        }

        private byte GetA(int x, int y)
        {
            return x == 0 ? (byte)0 : Decoded[y, x - 1];
        }
    }
}
