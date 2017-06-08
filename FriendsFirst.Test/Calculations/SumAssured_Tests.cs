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
    public class SumAssured_Tests
    {
        [TestCategory("SumAssured")]
        [TestMethod]
        public void SumAssured_50105759_Test()
        {
            var units = 93.935100000000006;
            var price = 1.9430000000000001;
            var benefitAumAssured = 49000;

            var contract = new Contracts.StandardLifeContract()
            {
                Premium = new RegulerPaying(PremiumFrequency.Monthly),
                Benefits = new Benefit[] {
                    new Benefit() { BillingRate = 0.0016613328 , SumAssured = benefitAumAssured, Code = "L007"},
                    new Benefit() { BillingRate = 0.00246771378, SumAssured = benefitAumAssured, Code = "C089"}
                },

                Funds = new Funds.InvestedFunds
                {
                    Items = new Funds.Fund[]{
                        new Funds.UnitLinkedFund()
                        {
                            Units = new Funds.Unit(0,units, price, 0, 0)
                        }
                    }
                }
            };

            var calc = new Calculations.SumAtRisk()
            {
                Method = 3
            };
            Assert.AreEqual(benefitAumAssured - (units * price), calc.Value(contract));

        }
        
    }
}
