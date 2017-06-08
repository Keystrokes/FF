using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FriendsFirst.Premiums;

namespace FriendsFirst.Funds.Test
{
    
    [TestClass]
    public class Unit_Tests
    {
        /// <summary>
        /// Make sure that a Double value can be added to a Unit Amount
        /// </summary>
        [TestCategory("Funds")]
        [TestMethod]
        public void Test_Addition()
        {
            var units = new FriendsFirst.Funds.Unit(0, 0, 1, 1, 0);

            //Expect there to be a value of €100 
            units += 50;

            Assert.AreEqual(50, units.Accumulated, 2);

             
        }

        /// <summary>
        /// Make sure that a Double value can be added to a Unit Amount
        /// </summary>
        [TestCategory("Funds")]
        [TestMethod]
        public void Test_Subtraction()
        {
            var units = new FriendsFirst.Funds.Unit(
                100, 
                100);

            //Expect the Accumulated to be subtract from and the
            //Inital to remain the same.
            var actual = units - 50;
            Assert.AreEqual(50, actual.Accumulated, 2);
            Assert.AreEqual(100, actual.Inital, 2);

            //Remove another 100 and expect the inital to 
            //also drop
            actual -= 100;
            Assert.AreEqual(0, actual.Accumulated, 2);
            Assert.AreEqual(50, actual.Inital, 2);


        }


    }
}
