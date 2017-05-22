using System;

namespace Arithmetic
{
    public class ArithmeticBase
    {
        protected int[] SymbolCounts;
        protected int[] SymbolSums;
        protected uint NumberOfSymbols = 257;
        protected uint Low;
        protected uint High;
        protected uint BitsToFollow;
        protected uint Eof;
        protected const uint TopValue = 0xFFFFFFFE;
        protected const uint FirstQuarter = TopValue/4 + 1;
        protected const uint Half = 2*FirstQuarter;
        protected const uint ThirdQuarter = 3*FirstQuarter;

        public ArithmeticBase()
        {
            InitializeSymbolCounts();
            InitializeSymbolSums();
            CalculateSymbolSums();
            Low = 0;
            High = TopValue;
            Eof = NumberOfSymbols - 1;
        }

        private void InitializeSymbolSums()
        {
            SymbolSums = new int[NumberOfSymbols + 1];

            for (int i = 0; i < SymbolSums.Length; i++)
                SymbolSums[i] = 0;
        }

        private void InitializeSymbolCounts()
        {
            SymbolCounts = new int[NumberOfSymbols + 1];

            for (int i = 0; i < SymbolCounts.Length; i++)
                SymbolCounts[i] = 3;
        }

        private void CalculateSymbolSums(int startingIndex = 1)
        {
            int sum =SymbolSums[startingIndex];

            for (; startingIndex < SymbolSums.Length; startingIndex++)
            {
                if (startingIndex != 0)
                    sum += SymbolCounts[startingIndex - 1];

                SymbolSums[startingIndex] = sum;
            }
        }

        protected void UpdateModel(uint symbol)
        {
            SymbolCounts[symbol]++;
            CalculateSymbolSums(Convert.ToInt32(symbol));
        }
    }
}