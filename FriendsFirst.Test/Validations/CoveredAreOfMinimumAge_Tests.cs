using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FriendsFirst.Premiums;
using Moq;
using FriendsFirst.Entities;

namespace FriendsFirst.Validators.Test
{

    [TestClass]
    public class CoveredAreOfMinimumAge_Tests
    {


        

        [TestCategory("Validation")]
        [TestMethod]
        public void Test_CoveredAreOfMinimumAge()
        {
            var v = new FriendsFirst.Validators.CoveredAreOfMinimumAge(10);
            var contract = new testContract()
            {
                Coverages = new Person[]
                {
                    new Person(){DOB = Global.SystemDate.AddYears(-20)}
                }
            };

            Assert.IsNull(v.Validate(contract));

        }

        [TestCategory("Validation")]
        [TestMethod]
        public void Test_CoveredAreNotOfMinimumAge()
        {
            var v = new FriendsFirst.Validators.CoveredAreOfMinimumAge(10);
            var contract = new testContract()
            {
                Coverages = new Person[]
                {
                    new Person(){DOB = Global.SystemDate.AddYears(-1)}
                }
            };

            Assert.IsNotNull(v.Validate(contract));

        }

        class testContract : Contracts.Contract
        {
            class testProduct : Products.Product
            {
                public testProduct()
                {
                    this.Code = "UTP - 1";
                    this.Description = "Unit Test Product- 1";
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
