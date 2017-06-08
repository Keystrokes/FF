using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FriendsFirst.Premiums;

namespace FriendsFirst.Funds.Test
{
    
    [TestClass]
    public class InvestedFunds_Tests
    {
        /// <summary>
        /// Make sure that a Double value can be added to a Unit Amount
        /// </summary>
        [TestCategory("Funds")]
        [TestMethod]
        public void FiftyFiftySplit()
        {
            var investedFunds = new InvestedFunds()
            {
                Items = new Fund[]
                {
                    new UnitLinkedFund(){Name = "Fund A 50%", InvestmentPercent = 0.5 },
                    new UnitLinkedFund(){Name = "Fund B 50%", InvestmentPercent = 0.5 }
                }
            };

            investedFunds += 50;
            var actual = investedFunds.First().Gross();

            Assert.AreEqual(25, investedFunds.First().Gross(), 2);
            Assert.AreEqual(25, investedFunds.Last().Gross(), 2);

        }

        /// <summary>
        /// Make sure that a Double value can be added to a Unit Amount
        /// </summary>
        [TestCategory("Funds")]
        [TestMethod]
        public void Addition_Operator()
        {
            var investedFunds = new InvestedFunds()
            {
                Items = new Fund[]
                {
                    new UnitLinkedFund(){Name = "Fund B 100%", InvestmentPercent = 1 }
                }
            };

            var expected = 50;
            investedFunds += expected;

            var actual = investedFunds.Gross();

            Assert.AreEqual(expected, actual, 2);

        }


    }
}
