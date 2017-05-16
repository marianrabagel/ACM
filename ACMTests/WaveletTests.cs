
using System;
using System.Security.Policy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wavelet;

namespace ACMTests
{
    [TestClass]
    public class WaveletTests
    {
        double[,] testMatrixWithFirstLineWithInfo;
        double[,] testMatrixWithFirstColumnWithInfo;
        double[] resultAnH;
        double[] resultAnL;
        double[] resultReorder;


        [TestInitialize]
        public void Setup()
        {
            int[] testValues = new[]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                9,
                9,
                9,
                9,
                9,
                3,
                2,
                7,
                5,
                2,
                8,
                2,
                55,
                2,
                7,
                3,
                1,
                6,
                9,
                1,
                3,
                2,
                66
            };
            resultAnH = new double[]
            {
                -0.865087
                , 0.250000
                , 0.182544
                , 0.000000
                , 0.000000
                , 0.000000
                , -0.091272
                , -0.125000
                , 0.432544
                , -0.125000
                , -0.091272
                , -0.547631
                , -0.293641
                , 3.767892
                , -2.801620
                , -3.484163
                , 4.930609
                , 0.134913
                , -0.771760
                , 3.924444
                , -34.154860
                , 58.557907
                , -33.894947
                , 2.356987
                , 3.996132
                , -4.853240
                , 1.460348
                , 6.123066
                , -0.325314
                , -2.014144
                , -37.644947
                , 71.067941

            };
            resultAnL = new[]
            {
                1.333641
                , 1.936590
                , 3.073267
                , 4.053498
                , 5.000000
                , 5.973251
                , 6.963367
                , 8.031705
                , 8.833180
                , 9.031705
                , 8.802874
                , 8.887195
                , 9.533891
                , 7.873111
                , 3.550919
                , 3.048637
                , 5.705122
                , 6.261889
                , 3.143611
                , 1.286614
                , 17.770563
                , 33.132475
                , 17.422570
                , 1.494277
                , 2.318917
                , 3.253989
                , 5.911796
                , 8.850036
                , 2.183380
                , -3.285223
                , 19.369724
                , 40.840501
            };
            resultReorder = new[]
            {
                1.333641
                , 3.073267
                , 5.000000
                , 6.963367
                , 8.833180
                , 8.802874
                , 9.533891
                , 3.550919
                , 5.705122
                , 3.143611
                , 17.770563
                , 17.422570
                , 2.318917
                , 5.911796
                , 2.183380
                , 19.369724
                , 0.250000
                , 0.000000
                , 0.000000
                , -0.125000
                , -0.125000
                , -0.547631
                , 3.767892
                , -3.484163
                , 0.134913
                , 3.924444
                , 58.557907
                , 2.356987
                , -4.853240
                , 6.123066
                , -2.014144
                , 71.067941
            };

            testMatrixWithFirstLineWithInfo = new double[testValues.Length, testValues.Length];
            testMatrixWithFirstColumnWithInfo = new double[testValues.Length, testValues.Length];
            for (int i = 0; i < testValues.Length; i++)
            {
                testMatrixWithFirstLineWithInfo[0, i] = Convert.ToByte(testValues[i]);
                testMatrixWithFirstColumnWithInfo[i, 0] = Convert.ToByte(testValues[i]);
            }
        }

        [TestMethod]
        public void TestAnalysisHigh()
        {
            WaveletBase waveletCoder = new WaveletBase(testMatrixWithFirstLineWithInfo.GetLength(0));
            double[] anH = waveletCoder.AnalysisHighHorizontal(0, testMatrixWithFirstLineWithInfo.GetLength(0), testMatrixWithFirstLineWithInfo);

            for (int i = 0; i < anH.Length; i++)
            {
                Assert.AreEqual(Math.Round(anH[i], 6), resultAnH[i]);
            }
        }

        [TestMethod]
        public void TestAnalysisLow()
        {
            WaveletBase waveletCoder = new WaveletBase(testMatrixWithFirstLineWithInfo.GetLength(0));
            double[] anL = waveletCoder.AnalysisLowHorizontal(0, testMatrixWithFirstLineWithInfo.GetLength(0), testMatrixWithFirstLineWithInfo);

            for (int i = 0; i < anL.Length; i++)
            {
                Assert.AreEqual(Math.Round(anL[i], 6), resultAnL[i]);
            }
        }

        [TestMethod]
        public void TestReorder()
        {
            WaveletBase waveletCoder = new WaveletBase(testMatrixWithFirstLineWithInfo.GetLength(0));
            double[] anL = waveletCoder.AnalysisLowHorizontal(0, testMatrixWithFirstLineWithInfo.GetLength(0), testMatrixWithFirstLineWithInfo);
            double[] anH = waveletCoder.AnalysisHighHorizontal(0, testMatrixWithFirstLineWithInfo.GetLength(0), testMatrixWithFirstLineWithInfo);
            double[] reorder = waveletCoder.ReorderH(anL, anH);

            for (int i = 0; i < anL.Length; i++)
            {
                Assert.AreEqual(Math.Round(reorder[i], 6), resultReorder[i]);
            }
        }

        [TestMethod]
        public void TestAnH1SyH1()
        {
            WaveletBase waveletCoder = new WaveletBase(testMatrixWithFirstLineWithInfo.GetLength(0));
            waveletCoder.Load(testMatrixWithFirstLineWithInfo);
            waveletCoder.AnH1();

            WaveletBase waveletDecoder = new WaveletBase(testMatrixWithFirstLineWithInfo.GetLength(0));
            waveletDecoder.WaveletMatrix = waveletCoder.WaveletMatrix;
            waveletDecoder.SyH1();

            for (int i = 0; i < testMatrixWithFirstLineWithInfo.GetLength(0); i++)
            {
                Assert.AreEqual(Math.Round(testMatrixWithFirstLineWithInfo[0, i]), Math.Round(waveletDecoder.WaveletMatrix[0, i]));
            }
        }

        [TestMethod]
        public void TestAnV1SyV1()
        {
            WaveletBase waveletCoder = new WaveletBase(testMatrixWithFirstColumnWithInfo.GetLength(0));
            waveletCoder.Load(testMatrixWithFirstColumnWithInfo);
            waveletCoder.AnV1();

            WaveletBase waveletDecoder = new WaveletBase(testMatrixWithFirstColumnWithInfo.GetLength(0));
            waveletDecoder.WaveletMatrix = waveletCoder.WaveletMatrix;
            waveletDecoder.SyV1();

            for (int i = 0; i < testMatrixWithFirstColumnWithInfo.GetLength(0); i++)
            {
                Assert.AreEqual(testMatrixWithFirstColumnWithInfo[i, 0], Math.Round(waveletDecoder.WaveletMatrix[i, 0]));
            }
        }
    }
}
