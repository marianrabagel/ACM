using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM
{
    public class BitWriter : IDisposable
    {
        private string fileName;
        private byte buffer;
        private int writeCounter;
        private FileStream writer;

        public BitWriter(string fileName)
        {
            writeCounter = 0;
            this.fileName = fileName;
            writer = new FileStream(fileName, FileMode.OpenOrCreate);
        }

        public void WriteBit(int bit)
        {
            buffer = (byte) (buffer | (bit << (7 - writeCounter%8)));
            writeCounter++;

            if (writeCounter%8 == 0)
            {
                writer.WriteByte(buffer);
                buffer = 0;
            }
        }

        public void WriteNBiti(uint biti, int n)
        {
            if(n > 32)
                return;

            for (int i = 0; i < n; i++)
                WriteBit((int) (biti >> (31 - i)));
        }

        public void Dispose()
        {
            writer.Dispose();
        }
    }
}
