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
        string basePath = @"E:\Workspaces\Visual studio 2015";

        [TestMethod]
        public void Write1ByteOf1_WriteBit()
        {
            string fileName = $@"{basePath}\ACM\UnitTestProject1\bin\Debug\TestFiles\Value0xFF_bitWriter.txt";
            if (File.Exists(fileName))
                File.Delete(fileName);

            using (BitWriter bitWriter = new BitWriter(fileName))
            {
                for (int i = 0; i < 8; i++)
                    bitWriter.WriteBit(0x01);
            }
            using (FileStream reader = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                Assert.AreEqual(1, reader.Length);

                byte solution = (byte) reader.ReadByte();
                Assert.AreEqual(0xFF, solution);
            }
        }

        [TestMethod]
        public void WriteA()
        {
            string fileName = $@"{basePath}\ACM\UnitTestProject1\bin\Debug\TestFiles\ValueA_bitWriter.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            using (BitWriter bitWriter = new BitWriter(fileName))
            {
                bitWriter.WriteNBiti(0x41, 8);
            }

            using (FileStream reader = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                Assert.AreEqual(1, reader.Length);

                byte solution = (byte)reader.ReadByte();
                Assert.AreEqual(0x41, solution);
            }
        }

        [TestMethod]
        public void Write1ByteOf0_WriteBit()
        {
            string fileName = $@"{basePath}\ACM\UnitTestProject1\bin\Debug\TestFiles\Value0x00_bitWriter.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            using (BitWriter bitWriter = new BitWriter(fileName))
            {
                for (int i = 0; i < 8; i++)
                    bitWriter.WriteBit(0x00);
            }

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
            string fileName = $@"{basePath}\ACM\UnitTestProject1\bin\Debug\TestFiles\Value0x5A_bitWriter.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            using (BitWriter bitWriter = new BitWriter(fileName))
            {
                bitWriter.WriteBit(0x00);
                bitWriter.WriteBit(0x01);
                bitWriter.WriteBit(0x00);
                bitWriter.WriteBit(0x01);
                bitWriter.WriteBit(0x01);
                bitWriter.WriteBit(0x00);
                bitWriter.WriteBit(0x01);
                bitWriter.WriteBit(0x00);
            }

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
            string fileName = $@"{basePath}\ACM\UnitTestProject1\bin\Debug\TestFiles\Value0xFF_WriteNBiti.txt";
            if (File.Exists(fileName))
                File.Delete(fileName);

            using (BitWriter bitWriter = new BitWriter(fileName))
            {
                bitWriter.WriteNBiti(0xFF, 8);
            }

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
            string inputFile = $@"{basePath}\ACM\UnitTestProject1\bin\Debug\TestFiles\test.txt";
            string outputFile = $@"{basePath}\ACM\UnitTestProject1\bin\Debug\TestFiles\test_copy.txt";

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
            string inputFile = $@"{basePath}\ACM\UnitTestProject1\bin\Debug\TestFiles\cover.jpg";
            string outputFile = $@"{basePath}\ACM\UnitTestProject1\bin\Debug\TestFiles\cover_copy.jpg";

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
            string inputFile = $@"{basePath}\ACM\UnitTestProject1\bin\Debug\TestFiles\brin-page-98.pdf";
            string outputFile = $@"{basePath}\ACM\UnitTestProject1\bin\Debug\TestFiles\brin-page-98_copy.pdf";

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
