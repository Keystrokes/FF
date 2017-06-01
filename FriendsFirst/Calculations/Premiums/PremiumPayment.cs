using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst.Contracts;
using FriendsFirst.Validators;
using FriendsFirst.Premiums;

namespace FriendsFirst.Calculations.Premiums
{
    class PremiumPayment : Calculation
    {
        public override IEnumerable<Validator> Validators(Contract contract)
        {
            var rtn = new Validator[]{
            };

            return rtn;
        }
        protected override double Calculate(Contract contract)
        {
            //TODO: These should be validators
            if (contract.Premium.Status != PremiumStatuses.InForce)   //DO not Apply if this is a Single Premium (Unless it is the Zero Term)
                return 0;

            if (contract.Premium.Frequencey == PremiumFrequency.Single)   
                return 0;

            return contract.Premium.Amount;
        }
    }
}
