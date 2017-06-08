using FriendsFirst.Premiums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Projections.Funds.Test
{
    [TestClass]
    public class Premium_Tests
    {
        

        /// <summary>
        /// Check that the premium amount increasses by the specified
        /// growth amount each time Each Year
        /// </summary>
        [TestCategory("Premium")]
        [TestMethod]
        public void GrowthRate_Increass()
        {
            var premium = 10.0;
            var growth = 0.5;
            var contract = new testContract()
            {
                CommencmentDate = Global.SystemDate,

                Premium = new RegulerPaying()
                {
                    IndexationRate = growth,
                    Amount = premium,
                    Status = PremiumStatuses.InForce
                }
            };
           
            var projector = new Projection<testContract>(contract);
            
            for(var year = 1; year <=50; year++)
            {
                var projected = projector[contract.CommencmentDate.AddYears(year).AddMonths(1)];
                var actual = projected.Contract.Premium.Amount;

                //Increass the last premium by the growth rate.
                var expected = premium * (1 + growth);
                Assert.AreEqual(expected, actual, 0.01);

                //Update the expected prmium amount for the next
                //Cycle
                premium = expected;
            }

            
        }

        /// <summary>
        /// Premium amount should remain the same with a Zero growth Rate
        /// </summary>
        [TestCategory("Premium")]
        [TestMethod]
        public void GrowthRate_Zero()
        {
            var contract = new testContract()
            {
                CommencmentDate = Global.SystemDate,

                Premium = new RegulerPaying()
                {
                    Amount = 100,
                }
            };

            var projector = new Projection<testContract>(contract);

            var expected = 100;
            //Get to contract 10 years from now.
            var c = projector[contract.CommencmentDate.AddYears(10)];
            var actual = c.Contract.Premium.Amount;

            Assert.AreEqual(expected, actual, 2);

        }

        class testContract : Contracts.Contract
        {
            class testProduct : Products.Product
            {
                public testProduct()
                {
                    this.Code = "Premium_Tests-TP - 1";
                    this.Description = "Premium_Tests Product- 1";
                }
            }

            public testContract()
            {
                this.Product = new testProduct();
                this.Premium = new FriendsFirst.Premiums.RegulerPaying(PremiumFrequency.Monthly)
                {
                    Status = PremiumStatuses.InForce
                };
                this.Funds = new FriendsFirst.Funds.InvestedFunds()
                {
                    Items = new FriendsFirst.Funds.Fund[]
                    {
                        new FriendsFirst.Funds.UnitLinkedFund()
                    }
                };
            }

        }
    }
}
