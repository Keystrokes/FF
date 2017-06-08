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
    public class MinimumTermsRemaining_Tests
    {
        [TestCategory("Validation")]
        [TestMethod]
        public void Test_MinimumTermsRemaining_GreaterThanMin()
        {
            var v = new FriendsFirst.Validators.MinimumTermsRemaining(2);
            var contract = new testContract()
            {
                Matures = Global.SystemDate.AddMonths(5),
            };

            Assert.IsNull(v.Validate(contract));

        }

        [TestCategory("Validation")]
        [TestMethod]
        public void Test_MinimumTermsRemaining_AtMin()
        {
            var v = new FriendsFirst.Validators.MinimumTermsRemaining(2);
            var contract = new testContract()
            {
                Matures = Global.SystemDate.AddMonths(2),
            };

            Assert.IsNull(v.Validate(contract));
            
        }
        [TestCategory("Validation")]
        [TestMethod]
        public void Test_MinimumTermsRemaining_LessThanMin()
        {
            var v = new FriendsFirst.Validators.MinimumTermsRemaining(2);
            var contract = new testContract()
            {
                Matures = Global.SystemDate.AddMonths(1)
            };

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
