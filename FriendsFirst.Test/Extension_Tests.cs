using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst;
using FriendsFirst.Entities;

namespace FriendsFirst.Test
{
    [TestClass]
    public class Extension_Tests
    {

        
        [TestCategory("Extensions")]
        [TestMethod]
        public void TestAtAge10()
        {
            var dob = new DateTime(2000, 1, 1);
            var atDate = new DateTime(2010, 1, 1);
            var expected = 10;
            var actual = dob.Age(atDate);

            Assert.AreEqual(expected, actual);
        }

        [TestCategory("Extensions")]
        [TestMethod]
        public void TestAtNegative()
        {
            var dob = new DateTime(2010, 1, 1);
            var atDate = new DateTime(2000, 1, 1);
            var expected = 0;
            var actual = dob.Age(atDate);

            Assert.AreEqual(expected, actual);
        }

        [TestCategory("Extensions")]
        [TestMethod]
        public void TestMaxAge()
        {
            IEnumerable<Person> list = new List<Person>()
            {
                new Person(){DOB=new DateTime(2001, 1, 1)},
                new Person(){DOB=new DateTime(2000, 1, 1)}
            };
            var atDate = new DateTime(2010, 1, 1);
            var expected = 10;
            var actual = list.MaxAge(atDate);

            Assert.AreEqual(expected, actual);
        }
        [TestCategory("Extensions")]
        [TestMethod]
        public void TestMaxAgeEmptyList()
        {
            IEnumerable<Person> list = new List<Person>()
            {
            };
            var atDate = new DateTime(2010, 1, 1);
            var expected = 0;
            var actual = list.MaxAge(atDate);

            Assert.AreEqual(expected, actual);
        }
    }
}
