
using FriendsFirst.Contracts;
using FriendsFirst;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using FriendsFirst.Export;

namespace FriendsFirst.Demos
{
    [TestClass]
    public class Savings_Projection_50416352_Test
    {
        public static Contracts.StandardSavingsContract Contract
        {
            get
            {
                ///Replication of Contract 50416352
                var rtn = new Contracts.StandardSavingsContract()
                {
                    CommencmentDate = new DateTime(2016, 11, 01),
                    Coverages = new Entities.Person[] { },
                    Funds = new Funds.InvestedFunds()
                    {
                        Items = new Funds.Fund[] {
                        new Funds.UnitLinkedFund(){
                            Code = "A48",
                            Name = "Compass Cautious",
                            Units = new Funds.Unit(
                                0,
                                5304.00599,
                                1.701,
                                0),
                            InvestmentPercent = 0.4937,
                            ManagmentCharge = 0.0082,
                            GrowthRate = 0.03,
                        },
                        new Funds.UnitLinkedFund{
                            Code = "A49",
                            Name = "Magnet Cautious",
                            Units = new Funds.Unit(
                                0,
                                5439.38176,
                                1.701,
                                0),
                            InvestmentPercent = 0.5063,
                            ManagmentCharge = 0.0082,
                            GrowthRate = 0.033},
                    }
                    },
                    Premium = new Premiums.RegulerPaying()
                    {
                        Amount = 300,
                        IndexationRate = 0.05
                    }
                };

                
                return rtn;
            }
        }
        
        [TestCategory("Demo")]
        [TestMethod]
        public void StandardProjection()
        {
            //Keep the system date the same as when the expected values where calculated.
            Global.SystemDate = new DateTime(2017, 05, 15);
            var c = Savings_Projection_50416352_Test.Contract;

            var monthlyPorjector = new Projections.Projection<Contracts.StandardSavingsContract>(c);

            var projection = monthlyPorjector[new DateTime(2027, 05, 01)];

            /*
             *  Fund Growth rate            =   0.03
             *  Fund Tax rate               =   0.0
             *  Funsd Management Charge     =   0.0082
             *  Plan Managment Charge       =   0.0
             *  Net Fund Growth Rate        =   0.0218
             */


            #region Expected Values
            var current_Gross = 18274.50;
            var current_Net = 15721.184;
            var projected_Net = 67515.56;
            var projected_Gross = 68807.485;
            var projected_Today = 16886.755;

            var tax_Deduction = 548.235;
            var estimatedFutureTax = 4322.054;
            var exitTax = 2005.0834;
            #endregion

            //Avaliable Premiums
            var actual = monthlyPorjector.Premiums.Sum(p => p.Amount);
            var expected = 45623.755;
            //Assert.AreEqual(expected, actual, 2);

            //Current Gross
            var currentGross = c.Funds.Gross();
            Assert.AreEqual(18274.50, currentGross, 2);

            //Current Net
            //var currentNet = c.Funds.Net();
            //Assert.AreEqual(15721.18, currentNet, 2);

            //Projected Gross Amount
            var projectedGross = projection.Contract.Funds.Gross();
            //Assert.AreEqual(67515.56, projectedGross, 2);

            //Projected Net Amount
            //var projectedNet = projection.Contract.Funds.Net();
            //Assert.AreEqual(67515.56, projectedNet, 2);

            //Projected Tax
            //var projectedTax = projection.Contract.Tax();
            //Assert.AreEqual(4322.05, projectedTax, 2);

            monthlyPorjector.DumpAndOpen();
        }
    }
}
