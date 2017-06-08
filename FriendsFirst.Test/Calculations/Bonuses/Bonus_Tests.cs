using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FriendsFirst.Premiums;
using Moq;
using FriendsFirst.Calculations.Bonuses;
using FriendsFirst.Products;
using FriendsFirst;

namespace FriendsFirst.Calculations.Test
{
    [TestClass]
    public class Bonus_Tests
    {
        [TestCategory("Bonus")]
        [TestMethod]
        public void Bonus_Test_PrmiumHoliday()
        {
            var contract = new Contracts.StandardPensionContract()
            {
                Premium = new RegulerPaying()
                {
                    Status = PremiumStatuses.Holiday
                }
            };
            
            var v = new Bonus();

            Assert.IsNotNull(v.Value(contract));
        }

    }
}
