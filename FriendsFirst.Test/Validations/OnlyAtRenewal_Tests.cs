using FriendsFirst.Entities;
using FriendsFirst.Premiums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Test.Validations
{
    [TestClass]
    public class OnlyAtRenewal_Tests
    {
        [TestCategory("Validation")]
        [TestMethod]
        public void OnlyAtRenewal_WithContract_Test()
        {
            var v = new FriendsFirst.Validators.OnlyAtRenewal();
            var contract = new testContract();

            var commencmentDate = new DateTime(2000, 01, 01);

            var renewalDates = new DateTime[]
            {
                new DateTime(2001,02,01),
                new DateTime(2010,02,01),
            };


            foreach (var d in renewalDates)
            {
                contract.CommencmentDate = commencmentDate;
                Global.SystemDate = d;
                Assert.IsNotNull(v.Validate(contract));
            }

        }
        [TestCategory("Validation")]
        [TestMethod]
        public void OnlyAtRenewal_NonRenewalDates_Test()
        {
            var v = new FriendsFirst.Validators.OnlyAtRenewal();

            var commencmentDate = new DateTime(2000, 01, 01);

            var renewalDates = new DateTime[]
            {
                new DateTime(2001,02,01),
                new DateTime(2002,02,01),
                new DateTime(2003,02,01),
                new DateTime(2004,02,01),
            };


            foreach (var d in renewalDates)
            {
                Assert.IsNotNull(v.Validate(commencmentDate, d));
            }

        }

        [TestCategory("Validation")]
        [TestMethod]
        public void OnlyAtRenewal_RenewalDates_Test()
        {
            var v = new FriendsFirst.Validators.OnlyAtRenewal();

            var commencmentDate = new DateTime(2000, 01, 01);

            var renewalDates = new DateTime[]
            {
                new DateTime(2001,01,01),
                new DateTime(2002,01,01),
                new DateTime(2003,01,01),
                new DateTime(2004,01,01),
            };


            foreach (var d in renewalDates)
            {
                Assert.IsNull(v.Validate(commencmentDate, d));
            }

        }

        class testContract : Contracts.Contract
        {
            class testProduct : Products.Product
            {
                public testProduct()
                {
                    this.Code = "MinimumTermsRemaining-TP - 1";
                    this.Description = "MinimumTermsRemaining Test Product- 1";
                }
            }

            public testContract()
            {
                this.Product = new testProduct();
                this.Premium = new FriendsFirst.Premiums.RegulerPaying(PremiumFrequency.Monthly)
                {
                    Status = PremiumStatuses.InForce
                };
            }

        }
    }
}
