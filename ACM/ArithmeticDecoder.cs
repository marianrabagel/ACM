using System;

namespace ACM
{
    public class ArithmeticDecoder : ArithmeticBase
    {
        private uint _valueFromFile;
        
        public void Decode(string inputFileName, string outputFileName)
        {/*
            string fileName =
                @"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\Low-High_decoder.txt";
            using (StreamWriter stream = new StreamWriter(fileName))*/
            using (BitReader reader = new BitReader(inputFileName))
            {
                using (BitWriter writer = new BitWriter(outputFileName))
                {
                    _valueFromFile = reader.ReadNBit(32);
                    _low = 0;
                    var length = reader.length*8;

                    while (reader.readCounter < length)
                    {
                        uint symbol = DecodeSymbol(reader);

                        if (symbol == EOF)
                            break;

                        //stream.WriteLine(_low + " --- " + _high);
                        writer.WriteNBiti(symbol, 8);
                        UpdateModel();
                    }
                }
            }
        }

        private uint DecodeSymbol(BitReader reader)
        {
            UInt64 range = _high - _low + 1;
            uint cum = (uint) (((_valueFromFile - _low + 1)*_symbolSums[_symbolSums.Length - 1] - 1)/(long) range);
            uint symbol;

            for (symbol = 1; _symbolSums[symbol] < cum; symbol++)
                ;

            _high = (uint) (_low + (range*(ulong) _symbolSums[symbol + 1])/(ulong) _symbolSums[_symbolSums.Length - 1] - 1);
            _low = (uint) (_low + (range*(ulong) _symbolSums[symbol])/(ulong) _symbolSums[_symbolSums.Length - 1]);

            for (;;)
            {
                if (_high < half)
                {
                    
                }
                else if (_low >= half)
                {
                    _valueFromFile -= half;
                    _low -= half;
                    _high -= half;
                }else if (_low >= firstQuarter && _high < thirdQuarter)
                {
                    _valueFromFile -= firstQuarter;
                    _low -= firstQuarter;
                    _high -= firstQuarter;
                }
                else
                {
                    break;
                }

                _low = _low << 1;
                _high = (_high << 1) | 1;
                _valueFromFile = (_valueFromFile << 1) | reader.ReadBit();
            }

            return symbol;
        }
    }
}
