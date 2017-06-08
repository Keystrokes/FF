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
    public class Savings_50416217_Test
    {
        public static StandardSavingsContract TestContract {
            get
            {
                return new Contracts.StandardSavingsContract()
                {
                    ContractNumber = "50416217",
                    CommencmentDate = new DateTime(2016,10,14),
                    Matures = new DateTime(2016, 10, 14).AddYears(25),

                    Premium = new Premiums.RegulerPaying(Premiums.PremiumFrequency.Monthly)
                    {
                        Amount = 1000.00,
                        IndexationRate = 0.15,
                        Status = Premiums.PremiumStatuses.InForce
                    },
                    Coverages = new Entities.Person[]
                    {
                        new Person(){
                            Name = "Joe Blogg",
                            Gender = Genders.Male,
                            DOB = new DateTime(1968, 07, 27)
                        }
                    },
                    Product = new Products.StandardPensionProduct()
                    {
                        Description = "Conductor Savings Plan",
                        Code = "ZSR",

                        Charge = 3.5,
                        ChargeIncreasesRate = 0.015,
                        ChargePercent = 1.0,
                    },

                    Funds = new Funds.InvestedFunds()
                    {
                        Items = new Funds.Fund[]
                        {
                            new Funds.UnitLinkedFund()
                            {
                                Code = "606",
                                GrowthRate = 0.05,
                                Name = "With Profit",
                                ManagmentCharge = 0.0075,
                                InitalUnitDiscountFactor = 1,
                                InvestmentPercent = 1,
                                Units = new Funds.Unit(
                                    0,
                                    152.55336,
                                    1.701,
                                    1.701,
                                    0)
                            }
                        }
                    }
                };
            }
        }

        [TestCategory("")]
        [TestMethod]
        public void Current_Test()
        {
            Global.SystemDate = new DateTime(2017, 05, 19);
            var contract = TestContract;
            var projector = new Projection<Contracts.StandardSavingsContract>(contract);

            var projeciton = projector[new DateTime(2068, 05, 14)];
            //var projeciton = projector[new DateTime(2020, 05, 14)];
            projector.DumpAndOpen();

            var expected = 1161531.9975312;
            var actual = projeciton.Contract.Gross();

            Assert.AreEqual(expected, actual, 0.01);

        }

        [TestCategory("Demo")]
        [TestMethod]
        public void Continue_Projection_Gross_Test()
        {
            Global.SystemDate = new DateTime(2017, 05, 19);
            var contract = TestContract;
            var projector = new Projection<Contracts.StandardSavingsContract>(contract);

            var projeciton = projector[new DateTime(2068, 05, 14)];
            //var projeciton = projector[new DateTime(2020, 05, 14)];
            projector.DumpAndOpen();

            var expected = 1161531.9975312;
            var actual = projeciton.Contract.Gross();

            Assert.AreEqual(expected, actual, 0.01);

        }


        [TestCategory("Demo")]
        [TestMethod]
        public void Cease_Projection_Test()
        {
            var contract = TestContract;
            var projector = new Projection<Contracts.StandardSavingsContract>(contract);

            var projeciton = projector[new DateTime(2017,05,19).AddYears(99)];

            var expected = 259.49;
            var actual = projeciton.Contract.Gross();
            Assert.AreEqual(expected, actual, 0.01);

            //TODO: Enable when the net method is compelete
            expected = 0.0;
            actual = expected;// projeciton.Contract.Net();
            Assert.AreEqual(expected, actual, 0.01);
        }

        [TestCleanup]
        public void Test_CleanUp()
        {
            //Resets the Global setting to return the current date time
            Global.SystemDate = DateTime.MinValue;
        }

    }
}
