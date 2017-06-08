
using FriendsFirst.Contracts;
using FriendsFirst;
using FriendsFirst.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FriendsFirst.Projections.Test
{
    [TestClass]
    public class DailyInterest_Tests
    {
        /// <summary>
        /// Test that a value of Zero is returned with 
        /// Negative days
        /// </summary>
        [TestCategory("Interest")]
        [TestMethod]
        public void NegativeDays()
        {
            var fundAmount = 100;
            var interestRate = 0.1;

            var cal = new Calculations.FundInterest();
            var actual = cal.Calculate(
                            fundAmount,
                            interestRate,
                            -100);

            var expected = 0;
            Assert.AreEqual(expected, actual, 2);
        }

        /// <summary>
        /// Test that a Zero value is returned for a Negative amount
        /// </summary>
        [TestCategory("Interest")]
        [TestMethod]
        public void NegativeAmount()
        {
            var fundAmount = -100;
            var interestRate = 0.1;

            var cal = new Calculations.FundInterest();
            var actual = cal.Calculate(
                            fundAmount,
                            interestRate,
                            Global.DAYS_IN_YEAR);

            var expected = 0;
            Assert.AreEqual(expected, actual, 2);
        }

        /// <summary>
        /// Test the full Years interest value
        /// </summary>
        [TestCategory("Interest")]
        [TestMethod]
        public void FullYearValue()
        {
            var fundAmount = 100;
            var interestRate = 0.1;

            var cal = new Calculations.FundInterest();
            var actual = cal.Calculate(
                            fundAmount,
                            interestRate,
                            Global.DAYS_IN_YEAR);

            var expected = 10;
            Assert.AreEqual(expected, actual,2);
        }

        /// <summary>
        /// HAlf Year Interest Value
        /// </summary>
        [TestCategory("Interest")]
        [TestMethod]
        public void HalfYearValue()
        {
            var fundAmount = 100;
            var interestRate = 0.1;

            var cal = new Calculations.FundInterest();
            var actual = cal.Calculate(
                            fundAmount,
                            interestRate,
                            Global.DAYS_IN_YEAR/2);

            var expected = 5;
            Assert.AreEqual(expected, actual,2);
        }

        /// <summary>
        /// Test Interest amount on Zero Day
        /// </summary>
        [TestCategory("Interest")]
        [TestMethod]
        public void StartYearValue()
        {
            var fundAmount = 100;
            var interestRate = 0.1;

            var cal = new Calculations.FundInterest();
            var actual = cal.Calculate(
                            fundAmount,
                            interestRate,
                            0);

            var expected = 0;
            Assert.AreEqual(expected, actual, 2);
        }
    }
}
