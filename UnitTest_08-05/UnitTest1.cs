using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp1;

namespace UnitTest_08_05
{
    [TestClass]
    public class UnitTest_StringCut
    {
        [TestMethod]
        public void TestNormal()
        {
            const string smalltxt = "12345";
            const string normaltxt = "1234567";
            const string bigtxt = "123456789";
            int length = normaltxt.Length;

            var testtxt = StringHelper.Cut(smalltxt, length);
            Assert.AreEqual(smalltxt, testtxt);
            testtxt = StringHelper.Cut(normaltxt, length);
            Assert.AreEqual(normaltxt, testtxt);
            testtxt = StringHelper.Cut(bigtxt, length);
            Assert.AreEqual(normaltxt, testtxt);
        }

        [TestMethod]
        public void TestEmptyStr()
        {
            var res = StringHelper.Cut("", 7);
            Assert.AreEqual("", res);
        }

        [TestMethod]
        public void TestNullStr()
        {
            var res = StringHelper.Cut(null, 7);
            Assert.AreEqual(null, res);
        }

        [TestMethod]
        public void TestZeroLength()
        {
            var res = StringHelper.Cut("12345", 0);
            Assert.AreEqual("", res);
        }

        [TestMethod]
        public void TestLessZeroLength()
        {
            var res = StringHelper.Cut("12345", -1);
            Assert.AreEqual("", res);
        }


    }
}
