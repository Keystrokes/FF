using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst.Contracts;
using FriendsFirst.Funds;
using FriendsFirst.Validators;

namespace FriendsFirst.Calculations
{
    /// <summary>
    /// Calculates the Amount generated in interest on the Contracts Funds
    ///  
    /// Note:  Fund interest is applied on a Yearly basies.  this means that the actual 
    ///         Interest amount will be a proprotion of how far in to the year the Contracts context date is
    /// </summary>
    /// <remarks>Interest amount calculated is in proportion the the Contracts Context Date thought the Year (i.e only full amount is calcualted at the end of the month)</remarks>
    public class FundInterest : Calculation
    {
        /// <summary>
        /// Calculates the Amount of interest that is applicable for a fund amount
        /// </summary>
        /// <param name="Amount"></param>
        /// <param name="InterestRate"></param>
        /// <param name="DaysIntoYear"></param>
        /// <returns></returns>
        public double Calculate(
            double Amount,
            double InterestRate,
            double DaysIntoYear)
        {
            if (Amount <= 0 || DaysIntoYear < 0)
                return 0;

            InterestRate += 1;
            var proportion = DaysIntoYear / Global.DAYS_IN_YEAR;

            var actualInterestToBeApplied = Math.Pow(InterestRate, proportion);

            var newAmount = Amount * actualInterestToBeApplied;

            //Dont want to return the New Fund amount.  only the interest amount.
            var rtn = newAmount - Amount;
            return rtn;
        }

        /// <summary>
        /// Calculates the Interest amount for each of the funds on the contract.
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        protected override double Calculate(Contract contract)
        {
            var rtn = 0.0;

            foreach (var fund in contract.Funds)
            {
                var daysUntilNextPayment = (contract.NextPaymentDate - contract.ContextDate).TotalDays;

                rtn += this.Calculate(
                        fund.Gross(),
                        fund.GrowthRate,
                        daysUntilNextPayment);
            }

            return rtn;
        }

        public double Calculate(Fund fund, double daysUntilNextPayment)
        {
            var rtn = this.Calculate(
                        fund.Gross(),
                        fund.GrowthRate,
                        daysUntilNextPayment);

            return rtn;
        }
    }


}
