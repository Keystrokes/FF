using FriendsFirst.Contracts;
using FriendsFirst.Entities;
using FriendsFirst.Projections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst.Export;

namespace FriendsFirst.Test.Demos
{
    [TestClass]
    public class SavingProjection_50068296
    {
        [TestCategory("Projection")]
        [TestMethod]
        public void PolicyCharges_Indexation()
        {
            Global.SystemDate = new DateTime(1999, 10, 14);

            var contract = new Contracts.StandardPensionContract()
            {
                ContractNumber = "50068296",
                CommencmentDate = Global.SystemDate,

                Coverages = new Entities.Person[]
                {
                    new Person(){
                        Gender = Genders.Female,
                        DOB = new DateTime(1970, 06, 04)
                    }
                },
                Benefits = new Products.Benefit[]
                {
                    new Products.Benefit()
                    {
                        Code = "L030",
                        Description = "Life Cover (First Death)",

                        Effective = new DateTime(1999,10,14),
                        Cessation = new DateTime(2053, 01,14),
                        BillingRate = 0.0,
                        SumAssured = 15236.86,
                        IndexationRate = 0.05
                    }
                },
                Product = new Products.StandardPensionProduct()
                {
                    //Policy Charge rates
                    Charge = 4.22,
                    ChargeIncreasesRate = 0.015,
                    ChargePercent = 1.0,
                },

                Funds = new Funds.InvestedFunds()
                {
                    Items = new Funds.Fund[]
                    {
                        new Funds.UnitLinkedFund()
                        {
                            GrowthRate = 0.0152,
                            Code = "",
                            Name = "With Profit",
                            ManagmentCharge = 0.0,
                            InitalUnitDiscountFactor = 1,
                            InvestmentPercent = 1,
                            Units = new Funds.Unit(
                                0,
                                8892.800075,
                                1.701,
                                0,
                                0)
                        }
                    }
                }
            };



            //var expected = 4.22;
            //var actual = calc.Value(contract);
            //Assert.AreEqual(expected, actual, 2);

            ////Move the system date forward a Year and expect the Charge to increass
            //Global.SystemDate = Global.SystemDate.AddYears(1);
            //expected = 4.2833;
            //actual = calc.Value(contract);
            //Assert.AreEqual(expected, actual);

            //Global.SystemDate = Global.SystemDate.AddYears(1);
            //expected = 4.3475495;
            //actual = calc.Value(contract);
            //Assert.AreEqual(expected, actual);

        }
        /// <summary>
        /// Projects a saving contract for 10 years and checks the 
        /// Contract Value on each cycle
        /// </summary>
        [TestCategory("Export")]
        [TestMethod]
        public void Projection_ToCSV()
        {
            var contract = new Contracts.StandardPensionContract()
            {
                ContractNumber = "50068296",
                CommencmentDate = new DateTime(1999, 10, 14),

                Premium = new Premiums.RegulerPaying()
                {
                    Amount = 10,
                    IndexationRate = 0.015,
                    Frequencey = Premiums.PremiumFrequency.Monthly
                },
                Coverages = new Entities.Person[]
                {
                    new Person(){
                        Gender = Genders.Female,
                        DOB = new DateTime(1970, 06, 04)
                    }
                },
                Benefits = new Products.Benefit[]
                {
                    new Products.Benefit()
                    {
                        Code = "L030",
                        Description = "Life Cover (First Death)",

                        Effective = new DateTime(1999,10,14),
                        Cessation = new DateTime(2053, 01,14),
                        BillingRate = 0.0,
                        SumAssured = 15236.86,
                        IndexationRate = 0.05
                    }
                },
                Product = new Products.StandardPensionProduct()
                {
                    Code = "VIP",
                    //Policy Charge rates
                    Charge = 4.22,
                    ChargeIncreasesRate = 0.015,
                    ChargePercent = 1.0,

                    //Annuity / Pension
                    BaseMortalityYear = 2000,
                    AdjustedAnnuityRate = 0.0,
                    MortalityImpactFactor = 0.0,
                    EscalationRate = 0.0,

                    //Tax Rates
                    ExitTaxRate = 0.0,

                    //Return Options
                    Guaranteed = false,
                    SSIS = false,

                    //Misc
                    MinFundAmount = 0
                },

                Funds = new Funds.InvestedFunds()
                {
                    Items = new Funds.Fund[]
                    {
                        new Funds.UnitLinkedFund()
                        {
                            //MarketValueAdjustment = 0.5,
                            GrowthRate = 0.0152,
                            Code = "",
                            Name = "With Profit",
                            ManagmentCharge = 0.0,
                            InitalUnitDiscountFactor = 1,
                            InvestmentPercent = 1,
                            Units = new Funds.Unit(
                                0,
                                8892.800075,
                                1.701,
                                0,
                                0)
                        }
                    }
                }

            };

            var projector = new Projection<StandardPensionContract>(contract);

            //Project 100 years into the future.
            var projection = projector[new DateTime(2125, 05,01)];

            projector.DumpAndOpen();

        }
    }
}
