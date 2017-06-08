using FriendsFirst.Calculations;
using FriendsFirst.Calculations.Premiums;
using FriendsFirst.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Calculations.Test
{
    [TestClass]
    public class Indexation_Tests
    {
        [TestCategory("Indexation")]
        [TestMethod]
        public void InvalidStatus()
        {
            //Just need to check that the Validator is there.
            //It is not up to this test to actualY test the Validations
            //functions. only to stipulate that these functions ARE Required
            //Nore does this test care if the Validation IS actualy fired.  This 
            //is the function of the base Calculate class and therefore
            //out of scope of this test class 
            var calc = new PremiumIndexation();
            var validators = calc.Validators(null)
                .Where(
                    v => v is InvalidPremiumStatusCheck);

            Assert.IsTrue(validators.Count() > 0);
        }

        [TestCategory("Indexation")]
        [TestMethod]
        public void ZeroPremium()
        {
            var calc = new PremiumIndexation();

            var expected = 0;
            var actual = calc.Calculate(0, 1);

            Assert.AreEqual(expected, actual);
        }

        [TestCategory("Indexation")]
        [TestMethod]
        public void NegativePremium()
        {
            var calc = new PremiumIndexation();

            var expected = 0;
            var actual = calc.Calculate(-100, 1);

            Assert.AreEqual(expected, actual);

        }


        [TestCategory("Indexation")]
        [TestMethod]
        public void ZeroRate()
        {
            var calc = new PremiumIndexation();

            var expected = 0;
            var actual = calc.Calculate(100, 0);

            Assert.AreEqual(expected, actual);

        }


        [TestCategory("Indexation")]
        [TestMethod]
        public void HalfRate()
        {
            var calc = new PremiumIndexation();

            var expected = 50;
            var actual = calc.Calculate(100, .5);

            Assert.AreEqual(expected, actual);

        }


        [TestCategory("Indexation")]
        [TestMethod]
        public void FullRate()
        {
            var calc = new PremiumIndexation();

            var expected = 100;
            var actual = calc.Calculate(100, 1);

            Assert.AreEqual(expected, actual);
        }

        [TestCategory("Indexation")]
        [TestMethod]
        public void Test_300_Premium_At_5_Percent()
        {
            var calc = new PremiumIndexation();

            var expected = 15;
            var actual = calc.Calculate(300, 0.05);

            Assert.AreEqual(expected, actual);
        }
    }
}
