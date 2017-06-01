using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst.Contracts;
using FriendsFirst.Validators;
using FriendsFirst.Premiums;

namespace FriendsFirst.Calculations.Bonuses
{
    /// <summary>
    /// Calculates a TODO
    /// </summary>
    public class Bonus : Calculation
    {
        public override IEnumerable<Validator> Validators(Contract c)
        {
            return new Validators.Validator[]
                {
                    new InvalidPremiumStatusCheck(PremiumStatuses.Holiday)
                };


        }
        protected override double Calculate(Contract contract)
        {
            throw new NotImplementedException();
        }
    }
}
