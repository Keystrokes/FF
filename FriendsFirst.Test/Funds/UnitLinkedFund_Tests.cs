using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FriendsFirst.Premiums;

namespace FriendsFirst.Funds.Test
{
    [TestClass]
    public class UnitLinkedFund_Tests
    {
        [TestCategory("Funds")]
        [TestMethod]
        public void Test_Inital()
        {
            var fund = new Funds.UnitLinkedFund();

            fund.Units = new Funds.Unit();
            Assert.AreEqual(0, fund.Initial());

            fund.Units = new Funds.Unit(10, 20, 1, 2);
            Assert.AreEqual(10, fund.Initial());
        }

        [TestCategory("Funds")]
        [TestMethod]
        public void Test_InitalCapital()
        {
            var fund = new Funds.UnitLinkedFund();

            fund.Capital = true;
            fund.Units = new Funds.Unit(10, 0, 0, 2);
            Assert.AreEqual(20, fund.Initial());
        }
        [TestCategory("Funds")]
        [TestMethod]
        public void Test_Accumulated()
        {
            var fund = new Funds.UnitLinkedFund();

            fund.Units = new Funds.Unit();
            Assert.AreEqual(0, fund.Accumulated());

            fund.Units = new Funds.Unit(10, 20, 1, 2);
            Assert.AreEqual(20, fund.Accumulated());

        }

        [TestCategory("Funds")]
        [TestMethod]
        public void Test_Value()
        {
            var fund = new Funds.UnitLinkedFund();

            fund.Units = new Funds.Unit();
            Assert.AreEqual(0, fund.Gross());

            fund.Units = new Funds.Unit(10, 0, 1, 2);
            Assert.AreEqual(10, fund.Gross());

            fund.Units = new Funds.Unit(0, 20, 1, 2);
            Assert.AreEqual(20, fund.Gross());

            fund.Units = new Funds.Unit(10, 20, 1, 2);
            Assert.AreEqual(30, fund.Gross());

        }
    }
}
