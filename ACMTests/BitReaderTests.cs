using System;
using System.IO;
using ACM;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACMTests
{
    [TestClass]
    public class BitReaderTests
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void InexistentFile()
        {
            BitReader reader = new BitReader();
            int solution = reader.ReadBit();
        }

        [TestMethod]
        [ExpectedException( typeof(Exception), "Empty file")]
        public void EmptyFile()
        {
            string fileName = @"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\empty.txt";

            BitReader reader = new BitReader(fileName);
            int solution = reader.ReadBit();
        }

        [TestMethod]
        public void ReadBitValue1()
        {
            string fileName = @"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\Value128.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            using (FileStream writer = new FileStream(fileName, FileMode.OpenOrCreate))
                writer.WriteByte(0x80);

            BitReader reader = new BitReader(fileName);
            int solution = reader.ReadBit();

            Assert.AreEqual(1, solution);
        }

        [TestMethod]
        public void ReadBitValue0()
        {
            string fileName = @"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\Value0.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            using (FileStream writer = new FileStream(fileName, FileMode.OpenOrCreate))
                writer.WriteByte(1);

            BitReader reader = new BitReader(fileName);
            int solution = reader.ReadBit();

            Assert.AreEqual(0, solution);
        }

        [TestMethod]
        public void Read3BitsOf1()
        {
            string fileName = @"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\Value0xE0.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            using (FileStream writer = new FileStream(fileName, FileMode.OpenOrCreate))
                writer.WriteByte(0xE0);

            BitReader reader = new BitReader(fileName);
            uint solution = reader.ReadNBit(3);

            Assert.AreEqual(0xE0000000, solution);
        }

        [TestMethod]
        public void Read4BitsAlternative()
        {
            string fileName = @"C:\Users\Marian\Documents\visual studio 2015\Projects\ACM\UnitTestProject1\bin\Debug\TestFiles\Value0xAA.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            using (FileStream writer = new FileStream(fileName, FileMode.OpenOrCreate))
                writer.WriteByte(0xAA);

            BitReader reader = new BitReader(fileName);
            uint solution = reader.ReadNBit(4);

            Assert.AreEqual(0xA0000000, solution);
        }
    }
}
