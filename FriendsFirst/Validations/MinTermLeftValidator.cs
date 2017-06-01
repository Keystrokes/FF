using FriendsFirst.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Validators
{
    public sealed class MinimumTermsRemaining : Validator<double, Contract>
    {
        public double MIN { get; set; }
        /// <summary>
        /// Fails if the Remaining terms on the contract is below 
        /// a provided value.
        /// </summary>
        /// <param name="term">Min Terms that must be remaining on contract</param>
        public MinimumTermsRemaining(double term)
        {
            MIN = term;
        }
        MinimumTermsRemaining(double termsRemaining, double minTerm) : this(minTerm)
        {
            this.Description = $"Remaining term {termsRemaining} is less that allowed : {MIN}";
        }
        internal override Validator<double, Contract> Validate(Contract c, double r)
        {
            var termsLeft = c.RemainingTerms();
            if (termsLeft < MIN)
                return new MinimumTermsRemaining(termsLeft, MIN);

            return null;
        }
    }
}
