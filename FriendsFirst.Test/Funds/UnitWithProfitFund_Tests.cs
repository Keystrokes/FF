using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FriendsFirst.Premiums;

namespace FriendsFirst.Funds.Test
{
    [TestClass]
    public class UnitWithProfitFund_Tests
    {
        [TestCategory("MAV")]
        [TestMethod]
        public void Test_MarketValueAdjustment()
        {
            var fund = new Funds.UtalisedWithProfit();

            fund.MarketValueAdjustment = 100;
            fund.Units = new Funds.Unit(0, 10, 2, 0);
            Assert.AreEqual(0, fund.Accumulated());

            fund.MarketValueAdjustment = 50;
            fund.Units = new Funds.Unit(0, 10, 2, 0);
            Assert.AreEqual(10, fund.Accumulated());

            fund.MarketValueAdjustment = 0;
            fund.Units = new Funds.Unit(0, 10, 2, 0);
            Assert.AreEqual(20, fund.Accumulated());
        }

        [TestCategory("MVA")]
        [TestMethod]
        public void Test_MarketValueAdjustment_OutOfRange()
        {
            var fund = new Funds.UtalisedWithProfit();
            try {
                fund.MarketValueAdjustment = -1;
                Assert.Fail("Expected Out of Range Exception");
            }
            catch (ArgumentOutOfRangeException) {}
            catch (Exception)
            {
                Assert.Fail("Expected Out of Range Exception");
            }

            try
            {
                fund.MarketValueAdjustment = 101;
                Assert.Fail("Expected Out of Range Exception");
            }
            catch (ArgumentOutOfRangeException) { }
            catch (Exception)
            {
                Assert.Fail("Expected Out of Range Exception");
            }
        }
    }
}
