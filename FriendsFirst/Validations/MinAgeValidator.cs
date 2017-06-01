using FriendsFirst.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Validators
{
    public sealed class CoveredAreOfMinimumAge : Validator<double, Contract>
    {
        #region Settings
        public double MIN { get; set; }
        #endregion
        /// <summary>
        /// Fails validation if the MInium age of those in cover is below
        /// the age provided
        /// </summary>
        /// <param name="age">No Coverged can be below this age.</param>
        public CoveredAreOfMinimumAge(double age)
        {
            this.MIN = age;
        }
        CoveredAreOfMinimumAge(double age, double minAge) : this(minAge)
        {
            this.Description = $"Max Age under Coverage {age} is below the Minimum age {MIN} Required ";
        }

        internal override Validator<double, Contract> Validate(Contract c, double r)
        {
            var age = c.Coverages.Max(cover => cover.Age(c.ContextDate));
            if (age < MIN)
                return new CoveredAreOfMinimumAge(age, MIN);

            return null;
        }
    }
}
