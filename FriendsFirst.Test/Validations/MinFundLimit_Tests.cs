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
    public class MinFundLimit_Tests
    {
        [TestCategory("Validation")]
        [TestMethod]
        public void MinFundLimit_Test()
        {
            var v = new FriendsFirst.Validators.MinimimFundLimit();

            Assert.IsNotNull(v.Validate(100, 91, 10));
            Assert.IsNull(v.Validate(100, 90, 10));
            Assert.IsNull(v.Validate(100, 89, 10));

        }

    }
}
