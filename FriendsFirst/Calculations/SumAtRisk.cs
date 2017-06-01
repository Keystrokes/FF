using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst.Contracts;
using FriendsFirst.Validators;

namespace FriendsFirst.Calculations
{
    /// <summary>
    /// Calculates the Exposed Sum At Risk considering the current 
    /// Contract value.
    /// </summary>
    public class SumAtRisk : Calculation
    {
        public int Method { get; set; }

        protected override double Calculate(Contract contract)
        {
            var sumAssured = contract.Benefits.SumAssured();
            var totalFundValue = contract.Funds.Gross();

            return Calculate(sumAssured, totalFundValue);
        }

        public double Calculate(double SumAssured, double TotalFundValue)
        {
            var rtn = SumAssured - TotalFundValue;
            if (rtn < 0)
                return 0;

            return rtn;
        }
    }
}
