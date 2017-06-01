using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst.Contracts;

namespace FriendsFirst.Validators
{
    /// <summary>
    /// verified that if the provided amount id removed from the 
    /// fund that it will not reduce below the deemed min value
    /// amount of that fund.
    /// </summary>
    public sealed class MinimimFundLimit : Validator<double, Contract>
    {
        /// <summary>
        /// Amount that is to be removed.
        /// </summary>
        double AmountToRemovedFromFund { get; set; }

        public MinimimFundLimit() : this(0) { }
        internal MinimimFundLimit(double amountToRemovedFromFund)
        {
            this.AmountToRemovedFromFund = amountToRemovedFromFund;
        }


        MinimimFundLimit(double amountToBeRemoved, double minFundAmount)
        {
            this.Description = $"Removing the amount {amountToBeRemoved} from the Current Fund will cause it to be below the Min fund threshold of {minFundAmount}";
        }
        internal override Validator<double, Contract> Validate(Contract c, double result)
        {
            var totalFund = c.Funds.Accumulated();
            var min = c.Product.MinFundAmount;

            return Validate(
                totalFund,
                this.AmountToRemovedFromFund,
                min);
        }

        public Validator<double, Contract> Validate(
           double TotalFund,
           double AmountToRemovedFromFund,
           double MinFundAmount)
        {
            var newFundTotal = TotalFund - AmountToRemovedFromFund;

            if (newFundTotal < MinFundAmount)
                return new MinimimFundLimit(newFundTotal, MinFundAmount);

            return null;
        }

    }
}
