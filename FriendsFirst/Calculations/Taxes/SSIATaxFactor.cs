using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst.Contracts;

namespace FriendsFirst.Calculations.Taxes
{
    public class SSIATaxFactor : Calculation
    {
        protected override double Calculate(Contract c)
        {
            var mav = new Calculations.MarketValueAdjustmentFactor();
            var mvaFactor = mav.Value(c);
            var exitTaxRate = c.Product.ExitTaxRate;

            var rtn = exitTaxRate - mvaFactor;

            return rtn;
        }
    }

}
