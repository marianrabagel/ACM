using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACMTests
{
    [TestClass]
    public class JpegTableTest
    {
        [TestMethod]
        public void JpegTable()
        {
            var jpegTable = SetJpegTable();

            Assert.AreEqual(0, jpegTable[0][0]);
            Assert.AreEqual(-2, jpegTable[2][1]);
            Assert.AreEqual(-253, jpegTable[8][2]);
        }

        [TestMethod]
        public void CodingFor5()
        {
            uint solution = GetCodingFor(5);
            uint val = 229;
            Assert.AreEqual(solution, val);
        }

        [TestMethod]
        public void CodingFor0()
        {
            uint solution = GetCodingFor(0);
            uint val = 0;
            Assert.AreEqual(solution, val);
        }

        [TestMethod]
        public void CodingFor1()
        {
            uint solution = GetCodingFor(1);
            uint val = 513;
            Assert.AreEqual(solution, val);
        }

        [TestMethod]
        public void CodingForNot5()
        {
            uint solution = GetCodingFor(-5);
            uint val = 3586;
            Assert.AreEqual(solution, val);
        }

        [TestMethod]
        public void CodingForNot13()
        {
            uint solution = GetCodingFor(-13);
            uint val = 7682;
            Assert.AreEqual(solution, val);
        }

        [TestMethod]
        public void CodingForNumbers()
        {
            uint solution = GetCodingFor(5);
            uint val = 3589;
            Assert.AreEqual(solution, val);
        }

        private uint GetCodingFor(int number)
        {
            if (number == 0)
                return 0;

            int y = Convert.ToInt32(Math.Floor(Math.Log(Math.Abs(number), 2) + 1));
            uint coding = 0;

            for (int i = 0; i < y; i++)
            {
                coding |= 0x1;
                coding = coding << 1;
            }

            uint x = number > 0 ? Convert.ToUInt32(number) : Convert.ToUInt32(number + Math.Pow(2, y) - 1);
            coding = coding << Convert.ToInt32(Math.Pow(2, y));
            coding = coding | Convert.ToUInt32(x & Convert.ToInt32(Math.Pow(2, y) - 1)); //dimensiune variabila

            return coding;
        }

        private int[][] SetJpegTable()
        {
            int[][] jpegTable = new int[9][];

            jpegTable[0] = new int[1];
            jpegTable[0][0] = 0;

            for (int i = 1; i < 9; i++)
            {
                int size = Convert.ToInt32(Math.Pow(2, i));
                jpegTable[i] = new int[size];
                int t = 0;

                for (int j = size - 1; j >= Math.Pow(2, i - 1); j--)
                {
                    jpegTable[i][t] = -j;
                    jpegTable[i][size - 1 - t] = j;
                    t++;
                }
            }
            return jpegTable;
        }
    }
}
