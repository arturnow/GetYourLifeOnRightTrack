using System;
using System.Runtime.InteropServices;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WasteBatchConsole;

namespace WasteBatchTests
{
    [TestClass]
    public class DateTimeHelperTest
    {
        private readonly DateTime _monday = new DateTime(2014, 8, 11);
        private readonly DateTime _tuesday = new DateTime(2014, 8, 12);
        private readonly DateTime _wednesday = new DateTime(2014, 8, 13);
        private readonly DateTime _thursday = new DateTime(2014, 8, 14);
        private readonly DateTime _friday = new DateTime(2014, 8, 15);
        private readonly DateTime _saturday = new DateTime(2014, 8, 16);
        private readonly DateTime _sunday = new DateTime(2014, 8, 17);
        
        [TestMethod]
        public void GetWeekStart_20140811_Returns20140811()
        {
            //arrange
            //act
            var result = DateTimeHelper.GetWeekStart(_monday);
            //assert
            Assert.AreEqual(new DateTime(2014, 8, 11), result);
        }

        [TestMethod]
        public void GetWeekStart_20140812_Returns20140811()
        {
            //arrange
            //act
            var result = DateTimeHelper.GetWeekStart(_tuesday);
            //assert
            Assert.AreEqual(new DateTime(2014, 8, 11), result);
        }
        
        [TestMethod]
        public void GetWeekStart_20140813_Returns20140811()
        {
            //arrange
            //act
            var result = DateTimeHelper.GetWeekStart(_wednesday);
            //assert
            Assert.AreEqual(new DateTime(2014, 8, 11), result);
        }
        
        [TestMethod]
        public void GetWeekStart_20140814_Returns20140811()
        {
            //arrange
            //act
            var result = DateTimeHelper.GetWeekStart(_thursday);
            //assert
            Assert.AreEqual(new DateTime(2014, 8, 11), result);
        }

        [TestMethod]
        public void GetWeekStart_20140815_Returns20140811()
        {
            //arrange
            //act
            var result = DateTimeHelper.GetWeekStart(_friday);
            //assert
            Assert.AreEqual(new DateTime(2014, 8, 11), result);
        }

        [TestMethod]
        public void GetWeekStart_20140816_Returns20140811()
        {
            //arrange
            //act
            var result = DateTimeHelper.GetWeekStart(_saturday);
            //assert
            Assert.AreEqual(new DateTime(2014, 8, 11), result);
        }

        [TestMethod]
        public void GetWeekStart_20140817_Returns20140811()
        {
            //arrange
            //act
            var result = DateTimeHelper.GetWeekStart(_sunday);
            //assert
            Assert.AreEqual(new DateTime(2014, 8, 11), result);
        }
        [TestMethod]
        public void GetMonthStart_20140817_Returns20140801()
        {
            //arrange
            var expected = new DateTime(2014, 8, 1);
            //act
            var result = DateTimeHelper.GetMonthStart(new DateTime(2014,8,17));
            //assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void GetMonthStart_20140801_Returns20140801()
        {
            //arrange
            var expected = new DateTime(2014, 8, 1);
            //act
            var result = DateTimeHelper.GetMonthStart(new DateTime(2014, 8, 1));
            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetMonthStart_20140831_Returns20140801()
        {
            //arrange
            var expected = new DateTime(2014, 8, 1);
            //act
            var result = DateTimeHelper.GetMonthStart(new DateTime(2014, 8, 31));
            //assert
            Assert.AreEqual(expected, result);
        }
    }
}
