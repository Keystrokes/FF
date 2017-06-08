using FriendsFirst.Calculations.Bonuses;
using FriendsFirst.Calculations;
using FriendsFirst.Products;
using FriendsFirst.Premiums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Calculations.Test
{
    [TestClass]
    public class SumAtRisk_Tests
    {
        [TestCategory("SumAtRisk")]
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

                Funds = new Funds.InvestedFunds()
                {
                    Items = new Funds.Fund[] {
                        new Funds.UnitLinkedFund()
                        {
                            Units = new Funds.Unit(0, units, price, 0, 0)
                        }
                    }
                }
            };

            //TODO:  What Determins the Sum At Risk Method?
            var calc = new Calculations.SumAtRisk()
            {
                Method = 3
            };
            Assert.AreEqual(benefitAumAssured - (units * price), calc.Value(contract));

        }

        [TestCategory("Validation")]
        [TestMethod]
        public void Bonus_Test_PrmiumHoliday()
        {
            var contract = new Contracts.StandardPensionContract()
            {
                Premium = new RegulerPaying()
                {
                    Status = PremiumStatuses.Holiday
                }
            };

            var v = new Bonus();

            Assert.IsNotNull(v.Value(contract));
        }

        [TestCategory("SumAtRisk")]
        [TestMethod]
        public void Test_Basic_Full_Amount()
        {
            var calc = new FriendsFirst.Calculations.SumAtRisk();

            var expected = 100;
            var actual = calc.Calculate(100, 0);

            Assert.AreEqual(expected, actual);
        }

        [TestCategory("SumAtRisk")]
        [TestMethod]
        public void Test_Basic_Half_Amount()
        {
            var calc = new FriendsFirst.Calculations.SumAtRisk();

            var expected = 50;
            var actual = calc.Calculate(100, 50);

            Assert.AreEqual(expected, actual);
        }

        [TestCategory("SumAtRisk")]
        [TestMethod]
        public void Test_Basic_Zero_Amount()
        {
            var calc = new FriendsFirst.Calculations.SumAtRisk();

            var expected = 0;
            var actual = calc.Calculate(100, 100);

            Assert.AreEqual(expected, actual);
        }

        [TestCategory("SumAtRisk")]
        [TestMethod]
        public void Test_Basic_Negative_Amount()
        {
            var calc = new FriendsFirst.Calculations.SumAtRisk();

            var expected = 0;
            var actual = calc.Calculate(100, 200);

            Assert.AreEqual(expected, actual);
        }
    }

}
