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
    public class InvestmentPercent
    {
        Contracts.Contract TestContract
        {
            get {
                return new Contracts.StandardLifeContract()
                {
                    Product = new Products.StandardLifeProduct(),

                    Funds = new Funds.InvestedFunds()
                    {
                        Items= new Funds.Fund[]{
                            new Funds.UtalisedWithProfit()
                            {
                                Units = new Funds.Unit(),
                                InvestmentPercent = 0.5
                            },
                            new Funds.UnitLinkedFund()
                            {
                                Units = new Funds.Unit(),
                                InvestmentPercent = 0.25
                            }
                        }
                    }
                };
            }
        }

        [TestCategory("Funds")]
        [TestMethod]
        public void UWPFund_Test()
        {
            var contract = this.TestContract;

            var actual = contract.Funds.InvestmentPercent< Funds.UtalisedWithProfit>();
            var expected = 0.5;

            Assert.AreEqual(expected, actual);
        }

        [TestCategory("Funds")]
        [TestMethod]
        public void ULFund_Test()
        {
            var contract = this.TestContract;

            var actual = contract.Funds.InvestmentPercent< Funds.UnitLinkedFund>();
            var expected = 0.25;

            Assert.AreEqual(expected, actual);
        }
    }
}
