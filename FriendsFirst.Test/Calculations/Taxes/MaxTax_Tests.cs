using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Calculations.Taxes.Test
{
    [TestClass]
    public class MaxTax_Tests
    {
        [TestCategory("MaxTax")]
        [TestMethod]
        public void MaxTax_Basic_1()
        {
            var calc = new Calculations.Taxes.MaxTax();

            var expected = 0.90499999999999992;
            var actual = calc.Calculate(100, 0.5, 10);

            Assert.AreEqual(expected, actual);
        }
    }
}
