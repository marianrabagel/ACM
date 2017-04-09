using System;

namespace Arithmetic
{
    public class ArithmeticBase
    {
        protected int[] _symbolCounts;
        protected int[] _symbolSums;
        protected uint _numberOfSymbols = 257;
        protected uint _low;
        protected uint _high = topValue;
        protected uint _bitsToFollow;
        protected uint EOF;
        protected const uint topValue = 0xFFFFFFFE;
        protected const uint firstQuarter = topValue/4 + 1;
        protected const uint half = 2*firstQuarter;
        protected const uint thirdQuarter = 3*firstQuarter;

        public ArithmeticBase()
        {
            InitializeSymbolCounts();
            InitializeSymbolSums();
            CalculateSymbolSums();
            _low = 0;
            _high = topValue;
            EOF = _numberOfSymbols - 1;
        }

        private void InitializeSymbolSums()
        {
            _symbolSums = new int[_numberOfSymbols + 1];

            for (int i = 0; i < _symbolSums.Length; i++)
                _symbolSums[i] = 0;
        }

        private void InitializeSymbolCounts()
        {
            _symbolCounts = new int[_numberOfSymbols + 1];

            for (int i = 0; i < _symbolCounts.Length; i++)
                _symbolCounts[i] = 3;
        }

        private void CalculateSymbolSums(int startingIndex = 1)
        {
            int sum =_symbolSums[startingIndex];

            for (; startingIndex < _symbolSums.Length; startingIndex++)
            {
                if (startingIndex != 0)
                    sum += _symbolCounts[startingIndex - 1];

                _symbolSums[startingIndex] = sum;
            }
        }

        protected void UpdateModel(uint symbol)
        {
            _symbolCounts[symbol]++;
            CalculateSymbolSums(Convert.ToInt32(symbol));
        }
    }
}