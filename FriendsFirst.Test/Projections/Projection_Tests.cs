
using FriendsFirst.Contracts;
using FriendsFirst;
using FriendsFirst.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst.Premiums;

namespace FriendsFirst.Projections.Test
{
    [TestClass]
    public class Projection_Tests
    {
        /// <summary>
        /// Contract should decreas in value as it will have only a single
        /// payment and a reoccuring policy charge.
        /// </summary>
        [TestCategory("Projections")]
        //[TestMethod]
        public void PensionContract_PolicyCharge()
        {
            var premium = 100;
            var contract = new Contracts.StandardPensionContract()
            {
                Premium = new Premiums.Single()
                {
                    Amount = premium
                },
                Coverages = new Entities.Person[]
                {
                    new Person(){DOB = Global.SystemDate.AddYears(-35)}
                },
                CommencmentDate = Global.SystemDate
            };

            //Set the charge amount.
            contract.Product.ChargeIncreasesRate = 0.0;
            contract.Product.ChargePercent = 1.0;
            contract.Product.Charge = 1;

            var projector = new Projection<StandardPensionContract>(contract);

            //check that there is no charge for the first term
            throw new NotImplementedException();


        }
        /// <summary>
        /// Contract should increass in value in accordance to the premiums being added
        /// to the contract.  Exclude all Fees and Interest 
        /// </summary>
        [TestCategory("Projections")]
        [TestMethod]
        public void SavingContract_OnlyPremiums()
        {
            var contract = new Contracts.StandardSavingsContract()
            {
                Premium = new RegulerPaying(PremiumFrequency.Monthly)
                {
                    Amount = 100,
                },

                CommencmentDate = Global.SystemDate
            };

            var projector = new Projection<StandardSavingsContract>(contract);

            //Check that the inital Gross of the contract is 100 as that is the inital Premium.
            var premium = 100;

            for(var term = 0; term <= 100; term++)
            {
                var expected = (premium * term) + premium;
                var projection = projector[term];
                var actual = projection.Contract.Gross();

                Assert.AreEqual(expected, actual, $"Expected Contract Value on Term {term} to be {expected} Not {actual}");
            }
        }

        /// <summary>
        /// Contract value should not increass if there are no Premiums or
        /// other alterations to the contract.
        /// </summary>
        [TestCategory("Projections")]
        [TestMethod]
        public void SavingContractSinglePremium()
        {
            var premium = 100;

            var contract = new Contracts.StandardSavingsContract()
            {
                Premium = new Premiums.Single()
                {
                    Amount = premium,
                },

                CommencmentDate = Global.SystemDate
            };

            var projector = new Projection<StandardSavingsContract>(contract);

            //Check that the inital Gross of the contract is 100 as that is the inital Premium.
            var expected = premium;

            for (var term = 0; term <= 100; term++)
            {
                var projection = projector[term];
                var actual = projection.Contract.Gross();

                Assert.AreEqual(expected, actual, $"Expected Contract Value on Term {term} to be {expected} Not {actual}");
            }
        }

       

    }
}
