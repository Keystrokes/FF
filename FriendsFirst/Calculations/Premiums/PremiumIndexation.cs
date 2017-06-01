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
    /// <summary>
    /// Calculates the Indexation amount applicable to the Contracts Premium Amount
    /// </summary>
    /// <remarks>Indexation is an increase to the charge at a specified rate on an annual basies.</remarks>
    public class PremiumIndexation : Calculations.Calculation
    {
        public PremiumIndexation() { }
        public override IEnumerable<Validator> Validators(Contract c)
        {
            return new Validator[] {
                    new InvalidPremiumStatusCheck(
                        PremiumStatuses.PaidUp,
                        PremiumStatuses.SinglePayment),

                    new OnlyAtRenewal()

                };
        }

        protected override double Calculate(Contract contract)
        {
            var rate = contract.Premium.IndexationRate;
            var premium = contract.Premium.Amount;

            return Calculate(premium, rate);
        }

        public double Calculate(double PremiumAmount, double Rate)
        {
            if (PremiumAmount < 0)
                return 0;

            var rtn = PremiumAmount * Rate;
            return rtn;
        }


    }
}
