using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst.Contracts;
using FriendsFirst.Validators;
using FriendsFirst.Premiums;

namespace FriendsFirst.Calculations.Charges
{
    /// <summary>
    /// Calculates the Indexation amount applicable to the Contract Policy Charge
    /// </summary>
    /// <remarks>Indexation is an increase to the charge at a specified rate on an annual basies.</remarks>
    public class PolicyChargeIndexation : Calculations.Calculation
    {
        public PolicyChargeIndexation() { }
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
            var rate = contract.Product.ChargeIncreasesRate;
            var charge = contract.Product.Charge;

            return Calculate(charge, rate);
        }

        public double Calculate(double ChargeAmount, double Rate)
        {
            if (ChargeAmount < 0)
                return 0;

            var rtn = ChargeAmount * Rate;
            return rtn;
        }


    }
}
