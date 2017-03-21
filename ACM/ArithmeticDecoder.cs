using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACM
{
    public class ArithmeticDecoder
    {
        const uint topValue = 0xFFFFFFFE;
        const uint firstQuarter = topValue / 4 + 1;
        const uint half = 2 * firstQuarter;
        const uint thirdQuarter = 3 * firstQuarter;

        int[] _symbolCounts; //{3, 3, 1, 1, 2};
        int[] _symbolSums;
        uint _numberOfSymbols = 257;
        uint _low;
        uint _high = topValue;
        uint _bitsToFollow;
        uint EOF;

        uint valueFromFile;

        public ArithmeticDecoder()
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

        public void Decode(string inputFileName, string outputFileName)
        {
            using (BitReader reader = new BitReader(inputFileName))
            {
                using (BitWriter writer = new BitWriter(outputFileName))
                {
                    valueFromFile = reader.ReadNBit(32);

                    _low = 0;
                    
                    while (reader.readCounter < reader.length)
                    {
                        uint symbol = DecodeSymbol(reader);

                        if (symbol == EOF)
                            break;

                        writer.WriteNBiti(symbol, 8);
                        UpdateModel();
                    }
                }
            }
        }

        private void UpdateModel()
        {
            return;
        }

        private uint DecodeSymbol(BitReader reader)
        {
            UInt64 range = _high - _low + 1;
            uint cum = (uint) (((valueFromFile - _low + 1)*_symbolCounts[_symbolCounts.Length - 1] - 1)/(long) range);
            uint symbol;

            for (symbol = 1; _symbolCounts[symbol] > cum; symbol++)
                ;

            _high = (uint) (_low + (range*(ulong) _symbolCounts[symbol - 1])/(ulong) _symbolCounts[_symbolCounts.Length - 1] - 1);
            _low = (uint) (_low + (range*(ulong) _symbolCounts[symbol])/(ulong) _symbolCounts[_symbolCounts.Length - 1]);

            for (;;)
            {
                if (_high < half)
                {
                    
                }
                else if (_low >= half)
                {
                    valueFromFile -= half;
                    _low -= half;
                    _high -= half;
                }else if (_low >= firstQuarter && _high < thirdQuarter)
                {
                    valueFromFile -= firstQuarter;
                    _low -= firstQuarter;
                    _high -= firstQuarter;
                }
                else
                {
                    break;
                }

                _low = 2*_low;
                _high = 2*_high + 1;
                valueFromFile = 2*valueFromFile + reader.ReadBit();
            }

            return symbol;
        }
    }
}
