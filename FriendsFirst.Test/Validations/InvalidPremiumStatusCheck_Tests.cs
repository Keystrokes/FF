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
    public class InvalidPremiumStatusCheck_Tests
    {
        [TestCategory("Validation")]
        [TestMethod]
        public void InvalidPremiumStatusCheck_Test()
        {
            var v = new FriendsFirst.Validators.InvalidPremiumStatusCheck(PremiumStatuses.Holiday, PremiumStatuses.PaidUp);
            var contract = new testContract();
            Assert.IsNull(v.Validate(contract));

            contract.Premium.Status = PremiumStatuses.Holiday;
            Assert.IsNotNull(v.Validate(contract));

            contract.Premium.Status = PremiumStatuses.PaidUp;
            Assert.IsNotNull(v.Validate(contract));

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
