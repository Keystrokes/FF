using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FriendsFirst.Premiums;
using Moq;
using System.Collections.Generic;
using FriendsFirst.Calculations.Charges;
using FriendsFirst.Entities;
using FriendsFirst.Products;

namespace FriendsFirst.Validators.Test
{
    [TestClass]
    public class BenefitCharge_Tests
    {
        [TestCategory("Validation")]
        [TestMethod]
        public void BenefitCharges_50105759_Test()
        {
            var calc = new BenefitCharge();

            var expected = 16.834962424838;
            var actual = calc.Calculate(
                48817.174908035529,
                new double[]{
                    0.0016613328,
                    0.00246771378
                });

            Assert.AreEqual(expected, actual,2);

        }
    }
}
