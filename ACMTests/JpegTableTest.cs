using System;
using ACM;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACMTests
{
    [TestClass]
    public class JpegTableTest
    {
        PredictiveCoder predictiveCoder = new PredictiveCoder("");

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
            JpegCoding solution = predictiveCoder.GetCodingFor(5);
            uint val = 117;
            Assert.AreEqual(solution.Coding, val);
            Assert.AreEqual(solution.Length, 7);
        }

        [TestMethod]
        public void CodingFor0()
        {
            JpegCoding solution = predictiveCoder.GetCodingFor(0);
            uint val = 0;
            Assert.AreEqual(solution.Coding, val);
            Assert.AreEqual(solution.Length, 1);
        }

        [TestMethod]
        public void CodingFor1()
        {
            JpegCoding solution = predictiveCoder.GetCodingFor(1);
            uint val = 5;
            Assert.AreEqual(solution.Coding, val);
            Assert.AreEqual(solution.Length, 3);
        }

        [TestMethod]
        public void CodingForNot5()
        {
            JpegCoding solution = predictiveCoder.GetCodingFor(-5);
            uint val = 114;
            Assert.AreEqual(solution.Coding, val);
            Assert.AreEqual(solution.Length, 7);
        }

        [TestMethod]
        public void CodingForNot13()
        {
            JpegCoding solution = predictiveCoder.GetCodingFor(-13);
            uint val = 482;
            Assert.AreEqual(solution.Coding, val);
            Assert.AreEqual(solution.Length, 9);
        }

        [TestMethod]
        public void CodingForNot26()
        {
            JpegCoding solution = predictiveCoder.GetCodingFor(-26);
            uint val = 1989;
            Assert.AreEqual(solution.Coding, val);
            Assert.AreEqual(solution.Length, 11);
        }

        [TestMethod]
        public void CodingFor104()
        {
            JpegCoding solution = predictiveCoder.GetCodingFor(104);
            uint val = 32616;
            Assert.AreEqual(solution.Coding, val);
            Assert.AreEqual(solution.Length, 15);
        }

        [TestMethod]
        public void CodingFor255()
        {
            JpegCoding solution = predictiveCoder.GetCodingFor(255);
            uint val = 130815;
            Assert.AreEqual(solution.Coding, val);
            Assert.AreEqual(solution.Length, 17);
        }

        [TestMethod]
        public void CodingForNot135()
        {
            JpegCoding solution = predictiveCoder.GetCodingFor(-135);
            uint val = 130680;
            Assert.AreEqual(solution.Coding, val);
            Assert.AreEqual(solution.Length, 17);
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
