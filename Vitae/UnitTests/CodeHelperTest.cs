using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Helper;
using System;

namespace UnitTests
{
    [TestClass]
    public class CodeHelperTest
    {
        [TestMethod]
        public void MonthYearTest1()
        {
            var startDate = new DateTime(2000, 1, 1);
            var endDate = new DateTime(2001, 1, 1);

            var dateDifference = new DateDifference(startDate, endDate);

            Assert.IsTrue(dateDifference.Years == 1);
            Assert.IsTrue(dateDifference.Months == 0);
        }

        [TestMethod]
        public void MonthYearTest2()
        {
            var startDate = new DateTime(2000, 2, 1);
            var endDate = new DateTime(2001, 1, 1);

            var dateDifference = new DateDifference(startDate, endDate);

            Assert.IsTrue(dateDifference.Years == 0);
            Assert.IsTrue(dateDifference.Months == 11);
        }

        [TestMethod]
        public void MonthYearTest3()
        {
            var startDate = new DateTime(2000, 2, 1);
            var endDate = new DateTime(2001, 2, 1);

            var dateDifference = new DateDifference(startDate, endDate);

            Assert.IsTrue(dateDifference.Years == 1);
            Assert.IsTrue(dateDifference.Months == 0);
        }
    }
}