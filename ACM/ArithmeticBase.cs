namespace ACM
{
    public class ArithmeticBase
    {
        protected int[] _symbolCounts; //{3, 3, 1, 1, 2};
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
                _symbolCounts[i] = 1;
        }

        private void CalculateSymbolSums()
        {
            int sum = 0;

            for (int i = 1; i < _symbolSums.Length; i++)
            {
                sum += _symbolCounts[i - 1];
                _symbolSums[i] = sum;
            }
        }

        protected void UpdateModel()
        {
        }
    }
}