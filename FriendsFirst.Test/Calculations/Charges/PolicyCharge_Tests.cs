using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FriendsFirst.Premiums;
using Moq;
using System.Collections.Generic;
using FriendsFirst.Calculations.Charges;
using FriendsFirst.Entities;
using FriendsFirst.Products;
using FriendsFirst.Validators;

namespace FriendsFirst.Calculations.Charges.Test
{
    [TestClass]
    public class PolicyCharge_Tests
    {

        [TestCategory("Projection")]
        [TestMethod]
        public void PolicyCharges_Indexation()
        {
            //New contracts are defaulted with the system date.
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


            var calc = new PolicyCharge();

            var expected = 4.22;
            var actual = calc.Value(contract);
            Assert.AreEqual(expected, actual, 2);

            //Move to the end of the year for indexation
            contract.ContextDate = contract.ContextDate.AddYears(1);
            expected = 4.2833;
            actual = calc.Value(contract);
            Assert.AreEqual(expected, actual);

            contract.ContextDate = contract.ContextDate.AddYears(1);
            expected = 4.3475495;
            actual = Math.Round(calc.Value(contract), 7);
            Assert.AreEqual(expected, actual);


            var known = new double[]
            {
                4.22,
                4.2833,
                4.3475495,
                4.4127627,
                4.4789542,
                4.5461385,
                4.6143306,
                4.6835455
            };
        }

        [TestCategory("PolicyCharge")]
        [TestMethod]
        public void PolicyCharges_Negative_Value()
        {
            var calc = new PolicyCharge();

            var expected = 0;
            var actual = calc.Calculate(
                -1,
                1);

            Assert.AreEqual(expected, actual);

            actual = calc.Calculate(
                1,
                -1);

            Assert.AreEqual(expected, actual);
        }

        [TestCategory("PolicyCharge")]
        [TestMethod]
        public void PolicyCharges_ApplyOnPaidUp()
        {
            var contract = new Contracts.StandardLifeContract()
            {
                Product = new Products.StandardPensionProduct()
                {
                    ApplyPolicyChargeOnPaidUp = true
                },

            };

            var calc= new PolicyCharge();

            var actual = calc.Validators(contract)
                .Where(v=>v is InvalidPremiumStatusCheck).Count();
            var expected = 1;

            Assert.AreNotEqual(expected, actual);
        }

        [TestCategory("PolicyCharge")]
        [TestMethod]
        public void PolicyCharges_Projection()
        {
            var contract = new Contracts.StandardLifeContract()
            {
                Premium = new RegulerPaying(PremiumFrequency.Monthly),
                Coverages = new Person[] { new Person() },
                Product = new Products.StandardPensionProduct()
                {
                    Charge = 5,
                    ChargeIncreasesRate = 0.01,
                    ChargePercent = 1.0
                }
            };

            var monthsAndRates = new Dictionary<int, double>
            {
                { 0, 5 },
                { 12, 5.05 },
                { 24, 5.1005 }
            };
            foreach (var monthAndRate in monthsAndRates)
            {
                var charge = new PolicyCharge();

                var actual = charge.Value(contract);
                var expected = monthAndRate.Value;
                Assert.AreEqual(expected, actual, 2);
            }

            
        }

    }
}
