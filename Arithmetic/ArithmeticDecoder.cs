using System;
using ACM;

namespace Arithmetic
{
    public class ArithmeticDecoder : ArithmeticBase
    {
        private uint _valueFromFile;
        
        public void Decode(string inputFileName, string outputFileName)
        {
            using (BitReader reader = new BitReader(inputFileName))
            {
                using (BitWriter writer = new BitWriter(outputFileName))
                {
                    _valueFromFile = reader.ReadNBit(32);
                    Low = 0;
                    High = TopValue;

                    while (true)
                    {
                        uint symbol = DecodeSymbol(reader);

                        if (symbol == Eof)
                            break;

                        writer.WriteNBiti(symbol, 8); 
                        UpdateModel(symbol);
                    }
                }
            }
        }

        private uint DecodeSymbol(BitReader reader)
        {
            UInt64 range = High - Low + 1;
            uint cum = (uint) (((_valueFromFile - Low + 1)*SymbolSums[SymbolSums.Length - 1] - 1)/(long) range);
            uint symbol;

            for (symbol = 0; cum >= SymbolSums[symbol+1]; symbol++)
            {
            }

            High = (uint) (Low + (range*(ulong) SymbolSums[symbol + 1])/(ulong) SymbolSums[SymbolSums.Length - 1] - 1);
            Low = (uint) (Low + (range*(ulong) SymbolSums[symbol])/(ulong) SymbolSums[SymbolSums.Length - 1]);

            for (;;)
            {
                if (High < Half)
                {
                    
                }
                else if (Low >= Half)
                {
                    _valueFromFile -= Half;
                    Low -= Half;
                    High -= Half;
                }else if (Low >= FirstQuarter && High < ThirdQuarter)
                {
                    _valueFromFile -= FirstQuarter;
                    Low -= FirstQuarter;
                    High -= FirstQuarter;
                }
                else
                {
                    break;
                }

                Low = Low << 1;
                High = (High << 1) | 1;
                _valueFromFile = (_valueFromFile << 1) | reader.ReadBit();
            }

            return symbol;
        }
    }
}
