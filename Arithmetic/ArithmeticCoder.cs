using System;
using ACM;

namespace Arithmetic
{
    public class ArithmeticCoder : ArithmeticBase
    {
        public void Encode(string inputFileName, string outputFileName)
        {
            using (BitReader reader = new BitReader(inputFileName))
            {
                using (BitWriter writer = new BitWriter(outputFileName))
                {
                    long length = reader.length*8;
                    while (reader.readCounter < length)
                    {
                        uint symbol = reader.ReadNBit(8);
                        EncodeSymbol(symbol, writer);
                        UpdateModel(symbol);
                    }

                    EncodeSymbol(Eof, writer);
                    BitsToFollow++;

                    if (Low < FirstQuarter)
                        OutputBitPlusFollow(0, writer);
                    else
                        OutputBitPlusFollow(1, writer);
                    
                    writer.WriteNBiti(0, 7);
                }
            }
        }

        private void EncodeSymbol(uint symbol, BitWriter writer)
        {
            UInt64 range = High - Low + 1;
            High = (uint) (Low + (range*(ulong) SymbolSums[symbol + 1])/(ulong) SymbolSums[SymbolSums.Length - 1] - 1);
            Low = (uint) (Low + (range*(ulong) SymbolSums[symbol])/(ulong) SymbolSums[SymbolSums.Length - 1]);

            for (;;)
            {
                if (High < Half)
                {
                    OutputBitPlusFollow(0, writer);
                }
                else if (Low >= Half)
                {
                    OutputBitPlusFollow(1, writer);
                    Low -= Half;
                    High -= Half;
                }
                else if (Low >= FirstQuarter && High < ThirdQuarter)
                {
                    BitsToFollow++;
                    Low -= FirstQuarter;
                    High -= FirstQuarter;
                }
                else
                    break;

                Low = Low << 1;
                High = (High  << 1) | 1;
            }
        }

        private void OutputBitPlusFollow(int bit, BitWriter writer)
        {
            writer.WriteBit(bit);

            while (BitsToFollow > 0)
            {
                writer.WriteBit(~bit & 1);
                BitsToFollow--;
            }
        }
    }
}
