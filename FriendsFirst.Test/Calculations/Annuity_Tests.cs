using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FriendsFirst.Premiums;
using Moq;
using FriendsFirst.Calculations.Bonuses;
using FriendsFirst.Products;
using FriendsFirst;

namespace FriendsFirst.Calculations.Test
{
    [TestClass]
    public class Annuity_Tests
    {
        [TestCategory("Annuity")]
        [TestMethod]
        public void Annutiy_50_PercentRate()
        {
            var calc = new Calculations.Annuities.Annuity();

            var rate = 0.5;
            var fund = 100;
            var expect = 50;

            var actual = calc.Calculate(fund, rate);

            Assert.AreEqual(expect, actual, 2);
        }

        [TestCategory("Annuity")]
        [TestMethod]
        public void Annutiy_Zero_PercentRate()
        {
            var calc = new Calculations.Annuities.Annuity();

            var expect = 0;
            var actual = calc.Calculate(100, 0);

            Assert.AreEqual(expect, actual, 2);
        }

    }
}
