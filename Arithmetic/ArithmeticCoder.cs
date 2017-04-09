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

                    EncodeSymbol(EOF, writer);
                    //writer.WriteNBiti(_low, 32); //de scris folosind OutputBitPlusFollow --> o functie de inchidere
                    _bitsToFollow++;

                    if (_low < firstQuarter)
                        OutputBitPlusFollow(0, writer);
                    else
                        OutputBitPlusFollow(1, writer);
                    
                    //writer.WriteNBiti(0, 30);
                    writer.WriteNBiti(0, 7);
                }
            }
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

                _low = _low << 1;
                _high = (_high  << 1) | 1;
            }
        }

        private void OutputBitPlusFollow(int bit, BitWriter writer)
        {
            writer.WriteBit(bit);

            while (_bitsToFollow > 0)
            {
                writer.WriteBit(~bit & 1);
                _bitsToFollow--;
            }
        }
    }
}
