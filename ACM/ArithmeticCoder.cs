using System;
using System.CodeDom;

namespace ACM
{
    public class ArithmeticCoder
    {
        const uint topValue = 0xFFFFFFFE;
        const uint firstQuarter = topValue/4 + 1;
        const uint half = 2*firstQuarter;
        const uint thirdQuarter = 3*firstQuarter;
        
        // A  E  M  N  R  
        int[] _symbolCounts; //{3, 3, 1, 1, 2};
        int[] _symbolSums;
        uint _numberOfSymbols = 257;
        uint _low;
        uint _high = topValue;
        uint _bitsToFollow;
        uint EOF;

        public ArithmeticCoder()
        {
            InitializeSymbolCounts();
            InitializeSymbolSums();
            CalculateSymbolSums();
            _low = 0;
            _high = topValue;
            EOF = _numberOfSymbols-1;
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

            _symbolCounts['A'] = 3;
            _symbolCounts['E'] = 3;
            _symbolCounts['M'] = 1;
            _symbolCounts['N'] = 1;
            _symbolCounts['R'] = 2;
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

        public void Encode(string inputFileName, string outputFileName)
        {
            using (BitReader reader = new BitReader(inputFileName))
            {
                using (BitWriter writer = new BitWriter(outputFileName))
                {
                    while (reader.readCounter < reader.length)
                    {
                        uint ch = reader.ReadNBit(8);
                        uint symbol = (ch >> 24);
                        EncodeSymbol(symbol, writer);
                        UpdateModel();
                    }

                    EncodeSymbol(EOF, writer);
                    _bitsToFollow++;

                    if (_low < firstQuarter)
                        OutputBitPlusFollow(0, writer);
                    else
                        OutputBitPlusFollow(1, writer);

                    writer.WriteNBiti(0, 7);
                }
            }
        }

        private void UpdateModel()
        {
            return;
        }

        private void EncodeSymbol(uint symbol, BitWriter writer)
        {
            UInt64 range = _high - _low + 1;
            _high = (uint) (_low + (range*(ulong) _symbolSums[symbol + 1])/(ulong) _symbolSums[_symbolSums.Length - 1] - 1);
            _low = (uint) (_low + (range*(ulong) _symbolSums[symbol])/(ulong) _symbolSums[_symbolSums.Length - 1]);

            for (;;)
            {
                if (_high < half)
                {
                    OutputBitPlusFollow(0, writer);
                }
                else if (_low >= half)
                {
                    OutputBitPlusFollow(1, writer);
                    _low -= half;
                    _high -= half;
                }
                else if (_low >= firstQuarter && _high < thirdQuarter)
                {
                    _bitsToFollow++;
                    _low -= firstQuarter;
                    _high -= firstQuarter;
                }
                else
                    break;

                _low = 2*_low;
                _high = 2*_high + 1;
            }
        }

        private void OutputBitPlusFollow(int bit, BitWriter writer)
        {
            writer.WriteBit(bit);

            while (_bitsToFollow > 0)
            {
                writer.WriteBit(~bit);
                _bitsToFollow--;
            }
        }
    }
}
