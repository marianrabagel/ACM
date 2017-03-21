using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ACM;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACMTests
{
    [TestClass]
    public class BitWriterTests
    {
        [TestMethod]
        public void Write1ByteOf1_WriteBit()
        {
            string fileName =@"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\Value0xFF_bitWriter.txt";
            if (File.Exists(fileName))
                File.Delete(fileName);

            BitWriter bitWriter = new BitWriter(fileName);
            
            for (int i = 0; i < 8; i++)
                bitWriter.WriteBit(0x01);

            bitWriter.Dispose();

            using (FileStream reader = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                Assert.AreEqual(1, reader.Length);

                byte solution = (byte) reader.ReadByte();
                Assert.AreEqual(0xFF, solution);

            }
        }

        [TestMethod]
        public void Write1ByteOf0_WriteBit()
        {
            string fileName = @"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\Value0x00_bitWriter.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            BitWriter bitWriter = new BitWriter(fileName);

            for (int i = 0; i < 8; i++)
                bitWriter.WriteBit(0x00);

            bitWriter.Dispose();

            using (FileStream reader = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                Assert.AreEqual(1, reader.Length);

                byte solution = (byte) reader.ReadByte();
                Assert.AreEqual(0x00, solution);
            }
        }

        [TestMethod]
        public void Write0x5A_WriteBit()
        {
            string fileName = @"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\Value0x5A_bitWriter.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            BitWriter bitWriter = new BitWriter(fileName);

            bitWriter.WriteBit(0x00);
            bitWriter.WriteBit(0x01);
            bitWriter.WriteBit(0x00);
            bitWriter.WriteBit(0x01);
            bitWriter.WriteBit(0x01);
            bitWriter.WriteBit(0x00);
            bitWriter.WriteBit(0x01);
            bitWriter.WriteBit(0x00);

            bitWriter.Dispose();

            using (FileStream reader = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                Assert.AreEqual(1, reader.Length);

                byte solution = (byte)reader.ReadByte();
                Assert.AreEqual(0x5A, solution);
            }
        }


        [TestMethod]
        public void Write1ByteOf1_WriteNBiti()
        {
            string fileName = @"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\Value0xFF_WriteNBiti.txt";
            if (File.Exists(fileName))
                File.Delete(fileName);

            BitWriter bitWriter = new BitWriter(fileName);

            bitWriter.WriteNBiti(0xFF000000, 8);

            bitWriter.Dispose();

            using (FileStream reader = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                Assert.AreEqual(1, reader.Length);

                byte solution = (byte)reader.ReadByte();
                Assert.AreEqual(0xFF, solution);

            }
        }

        [TestMethod]
        public void ReadAndWriteATxtfile()
        {
            string inputFile = @"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\test.txt";
            string outputFile = @"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\test_copy.txt";

            if(File.Exists(outputFile))
                File.Delete(outputFile);

            using (BitReader reader = new BitReader(inputFile))
            {
                using (BitWriter writer = new BitWriter(outputFile))
                {
                    long nrb = 8*reader.length;

                    do
                    {
                        Random random = new Random();
                        int randomNb = random.Next(1, 33);

                        uint readNBit = reader.ReadNBit(randomNb);
                        writer.WriteNBiti(readNBit, randomNb);

                        nrb -= randomNb;

                    } while (nrb > 0);
                }
            }
        }

        [TestMethod]
        public void ReadAndWriteAJpegfile()
        {
            string inputFile = @"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\cover.jpg";
            string outputFile = @"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\cover_copy.jpg";

            if (File.Exists(outputFile))
                File.Delete(outputFile);

            using (BitReader reader = new BitReader(inputFile))
            {
                using (BitWriter writer = new BitWriter(outputFile))
                {
                    long nrb = 8 * reader.length;

                    do
                    {
                        Random random = new Random();
                        int randomNb = random.Next(1, 33);

                        uint readNBit = reader.ReadNBit(randomNb);
                        writer.WriteNBiti(readNBit, randomNb);

                        nrb -= randomNb;

                    } while (nrb > 0);
                }
            }
        }

        [TestMethod]
        public void ReadAndWriteAPdffile()
        {
            string inputFile = @"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\brin-page-98.pdf";
            string outputFile = @"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\brin-page-98_copy.pdf";

            if (File.Exists(outputFile))
                File.Delete(outputFile);

            using (BitReader reader = new BitReader(inputFile))
            {
                using (BitWriter writer = new BitWriter(outputFile))
                {
                    long nrb = 8 * reader.length;

                    do
                    {
                        Random random = new Random();
                        int randomNb = random.Next(1, 33);

                        uint readNBit = reader.ReadNBit(randomNb);
                        writer.WriteNBiti(readNBit, randomNb);

                        nrb -= randomNb;

                    } while (nrb > 0);
                }
            }
        }
    }
}
