using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Calculations.Taxes.Test
{
    [TestClass]
    public class GruTaxFactor_Tests
    {
        [TestCategory("GruTaxFactor")]
        [TestMethod]
        public void ZeroTestValue()
        {
            var calc= new Taxes.GruTaxFactor();

            var expected = 0;
            var actual = calc.Calculate(
                0,
                1000,
                0,
                0);

            Assert.AreEqual(expected, actual);
        }
    }
}
