﻿using System;
using System.IO;
using ACM;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACMTests
{
    [TestClass]
    public class BitReaderTests
    {
        string basePath = @"E:\Workspaces\Visual studio 2015";

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
            string fileName = $@"{basePath}\ACM\UnitTestProject1\bin\Debug\TestFiles\empty.txt";

            BitReader reader = new BitReader(fileName);
            int solution = reader.ReadBit();
        }

        [TestMethod]
        public void ReadBitValue1()
        {
            string fileName = $@"{basePath}\ACM\UnitTestProject1\bin\Debug\TestFiles\Value128.txt";

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
            string fileName = $@"{basePath}\ACM\UnitTestProject1\bin\Debug\TestFiles\Value0.txt";

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
            string fileName = $@"{basePath}\ACM\UnitTestProject1\bin\Debug\TestFiles\Value0xE0.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            using (FileStream writer = new FileStream(fileName, FileMode.OpenOrCreate))
                writer.WriteByte(0xE0);

            BitReader reader = new BitReader(fileName);
            uint solution = reader.ReadNBit(3);

            Assert.AreEqual((uint)0x07, solution);
        }

        [TestMethod]
        public void Read4BitsAlternative()
        {
            string fileName = $@"{basePath}\ACM\UnitTestProject1\bin\Debug\TestFiles\Value0xAA.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            using (FileStream writer = new FileStream(fileName, FileMode.OpenOrCreate))
                writer.WriteByte(0xAA);

            BitReader reader = new BitReader(fileName);
            uint solution = reader.ReadNBit(4);

            Assert.AreEqual((uint)0xA, solution);
        }

        [TestMethod]
        public void Read32BitsAlternative()
        {
            string fileName = $@"{basePath}\ACM\UnitTestProject1\bin\Debug\TestFiles\Value0xAAAAAAAA.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            using (FileStream writer = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                writer.WriteByte(0xAA);
                writer.WriteByte(0xAA);
                writer.WriteByte(0xAA);
                writer.WriteByte(0xAA);
            }

            BitReader reader = new BitReader(fileName);
            uint solution = reader.ReadNBit(32);

            Assert.AreEqual(0xAAAAAAAA, solution);
        }

        [TestMethod]
        public void Read9BitsAlternative()
        {
            string fileName = $@"{basePath}\ACM\UnitTestProject1\bin\Debug\TestFiles\Value0xAAF.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            using (FileStream writer = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                writer.WriteByte(0xAA);
                writer.WriteByte(0xF0);
            }

            BitReader reader = new BitReader(fileName);
            uint solution = reader.ReadNBit(9);

            Assert.AreEqual((uint)0x155, solution);
        }

        [TestMethod]
        public void Read17BitsAlternative()
        {
            string fileName = $@"{basePath}\ACM\UnitTestProject1\bin\Debug\TestFiles\Value0xABCDE.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            using (FileStream writer = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                writer.WriteByte(0xAB);
                writer.WriteByte(0xCD);
                writer.WriteByte(0xE0);
            }

            BitReader reader = new BitReader(fileName);
            uint solution = reader.ReadNBit(17);

            Assert.AreEqual((uint)0x1579B, solution);
        }

        [TestMethod]
        public void Read25BitsAlternative()
        {
            string fileName = $@"{basePath}\ACM\UnitTestProject1\bin\Debug\TestFiles\Value0xABCDEF0.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            using (FileStream writer = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                writer.WriteByte(0xAB);
                writer.WriteByte(0xCD);
                writer.WriteByte(0xEF);
                writer.WriteByte(0x00);
            }

            BitReader reader = new BitReader(fileName);
            uint solution = reader.ReadNBit(25);

            Assert.AreEqual((uint) 0x1579BDE, solution);
        }

        [TestMethod]
        public void Read1Byte()
        {
            string fileName = $@"{basePath}\ACM\UnitTestProject1\bin\Debug\TestFiles\Value157.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            using (FileStream writer = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                writer.WriteByte(0x9D);
            }

            BitReader reader = new BitReader(fileName);
            byte solution = (byte) reader.ReadNBit(8);

            Assert.AreEqual(0x9D, solution);
        }
    }
}
