using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst.Contracts;

namespace FriendsFirst.Calculations.Taxes
{
    /// <summary>
    /// Calculates the Maximum Tax value of the Contract
    /// </summary>
    /// <remarks>Tax is only applicable to profits, there fore the Tax already</remarks>
    public class MaxTax : Calculation
    {
        protected override double Calculate(Contract c)
        {
            var current = c.Gross();
            var taxToDate = 0;
            var premiumsToDate = 0;

            return Calculate(
                c.CurrentValue(),
                taxToDate,
                premiumsToDate);
        }

        public double Calculate(
            double Value,
            double DeemedTaxToDate,
            double PremiumsAvaiable)
        {
            var rtn = 1 + DeemedTaxToDate / Value
                - PremiumsAvaiable / Value;

            if (rtn < 0)
                return 0;

            return rtn;
        }
    }

}
