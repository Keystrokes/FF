using System;
using System.Collections.Generic;
using System.Linq;
using FriendsFirst.Calculations;
using FriendsFirst.Contracts;
using FriendsFirst.Validators;

namespace FriendsFirst.Calculations.Charges
{
    /// <summary>
    /// Calculates the Charge for the contracts Benefits
    /// </summary>
    /// <remarks>Benefit charges are effect by the number of coverages and the Sum At Risk</remarks>
    public class BenefitCharge : Calculation
    {
        public override IEnumerable<Validator> Validators(Contract c)
        {
            return new List<Validator>()
            {
                new OnlyAtRenewal(),
            };

        }
        protected override double Calculate(Contract c)
        {
            var sumAtRisk = c.SumAtRisk();

            IEnumerable<double> rates = new double[] { };
            if (c.Benefits != null)
                rates = c.Benefits.Select(b => b.BillingRate);

            return Calculate(
                sumAtRisk,
                rates);
        }

        public double Calculate(
            double SumAtRisk,
            IEnumerable<double> Rates)
        {
            double rtn = 0;
            foreach (var rate in Rates)
            {
                var monthlyProportion = rate / Global.MONTHS_IN_YEAR;
                rtn += SumAtRisk * monthlyProportion;
            }
            return rtn;
        }
    }
}
