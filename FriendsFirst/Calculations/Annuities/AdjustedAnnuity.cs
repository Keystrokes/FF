using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst.Contracts;

namespace FriendsFirst.Calculations.Annuities
{
    public class AdjustedAnnuity : Annuity
    {
        protected override double Calculate(Contract contract)
        {
            var baseRate =  base.Calculate(contract);

            var term = contract.Matures.Year - contract.Product.BaseMortalityYear;
            var factor = Math.Pow(
                contract.Product.MortalityImpactFactor, 
                term);

            var rtn = baseRate * factor;
            return rtn;
        }
    }
}
