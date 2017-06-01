using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst.Contracts;

namespace FriendsFirst.Calculations.Taxes
{
    public class GruTaxFactor : Calculation
    {
        double TotalDeemedTaxToDate(Contract c) { throw new NotImplementedException(); }

        protected override double Calculate(Contract c)
        {
            return Calculate(
                c.Product.ExitTaxRate,
                new MaxTax().Value(c),
                TotalDeemedTaxToDate(c),
                c.CurrentValue());
        }

        public double Calculate(
            double ExitTaxRate, 
            double MaxTax,
            double TotalDeemedTaxToDate,
            double CurrentValue)
        {
            if (CurrentValue == 0)
                return 0;

            var rtn = 1 - ExitTaxRate
               * (MaxTax - TotalDeemedTaxToDate / CurrentValue);

            return rtn;
        }
    }
}
