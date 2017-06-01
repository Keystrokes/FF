using FriendsFirst.Premiums;
using FriendsFirst.Contracts;
using FriendsFirst.Calculations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst.Products;
using FriendsFirst.Validators;

namespace FriendsFirst.Calculations.Bonuses
{
    /// <summary>
    /// Calulates a TODO
    /// </summary>
    /// <summary>
    /// Bonus which is applied to a contract if it reaches Maturity
    /// </summary>
    public class Lotalty : Calculation
    {
        #region Settings
        /// <summary>
        /// Min Age in which Contract holder is to be before this
        /// bonus is applicable.
        /// </summary>
        public int MinApplicableAge { get; set; }
        public int MinApplicableTerm { get; set; }
        #endregion
        public Lotalty() : this(65, 65) { }
        public Lotalty(int minApplicableAge, int minApplicableTerm)
        {
            this.MinApplicableAge = minApplicableAge;
            this.MinApplicableTerm = minApplicableTerm;
        }

        public override IEnumerable<Validator> Validators(Contract c)
        {
            return new Validator[]
                {
                    new InvalidPremiumStatusCheck(PremiumStatuses.Holiday, PremiumStatuses.PaidUp),
                    new CoveredAreOfMinimumAge(MinApplicableAge),
                    new MinimumTermsRemaining(MinApplicableTerm)
                };

        }

        #region Rates that are obtained from the Data base (T5535) 
        IEnumerable<LotaltyBonusRate> Rates { get; }
        #endregion

        protected override double Calculate(Contract contract)
        {
            throw new NotImplementedException();
        }

    }
}
