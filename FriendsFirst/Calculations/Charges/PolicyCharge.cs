using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FriendsFirst.Calculations;
using FriendsFirst.Contracts;
using FriendsFirst.Validators;
using FriendsFirst.Premiums;

namespace FriendsFirst.Calculations.Charges
{
    /// <summary>
    /// Calculates the charge applicable on a contract
    /// </summary>
    public class PolicyCharge : Calculation
    {
        bool ApplyOnPaidUp { get; set; }

        public override IEnumerable<Validator> Validators(Contract c)
        {
            var rtn = new List<Validator>()
            {
                //new OnlyAtRenewal()  //This happens on policy 50416352
            };

            if (!c.Product.ApplyPolicyChargeOnPaidUp)
                rtn.Add(new InvalidPremiumStatusCheck(PremiumStatuses.Holiday, PremiumStatuses.PaidUp));

            return rtn;

        }


        /// <summary>
        /// Charge amount that is to be applied.
        /// Get this from the policy details.
        /// PolicyCharge()
        /// </summary>
        double ChargePerCoverage(Contract contract)
        {
            var charge = contract.Product.Charge;

            var rtn = ChargePerCoverage(
                charge,
                contract.Coverages == null ? 0 : contract.Coverages.Count());

            return rtn;
        }
        public double ChargePerCoverage(double Charge, int Coverages)
        {
            if (Coverages == 0)
                return 0;

            var rtn = Charge / Coverages;
            return rtn;
        }

        /// <summary>
        /// Current Value
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        protected override double Calculate(Contract contract)
        {
            var charge = this.ChargePerCoverage(contract);

            return Calculate(charge,
                contract.Product.ChargePercent);
        }


        public double Calculate(double Charge, double Percent)
        {
            if (Charge < 0 || Percent < 0)
                return 0;

            //var rtn = Charge * Rate * Percent;
            var rtn = Charge * Percent;
            return rtn;
        }
    }


}
