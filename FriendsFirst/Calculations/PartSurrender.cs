using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst.Contracts;
using FriendsFirst.Validators;
using FriendsFirst.Calculations.Taxes;

namespace FriendsFirst.Calculations
{
    //FFULPremComponent.cpp (ApplyPartSurrender)
    /// <summary>
    /// Calculates a TODO
    /// </summary>
    public class PartSurrender : Calculation
    {

        public override IEnumerable<Validator> Validators(Contract contract)
        {
            return new Validator[]
            {
                new Validators.MinimimFundLimit(this.SurrenderAmount)
            };
        }

        public double SurrenderAmount { get; set; }

        protected override double Calculate(Contract c)
        {
            var mvaFactor = new MarketValueAdjustmentFactor().Value(c);
            var ssiaFactor = new SSIATaxFactor().Value(c);

            double rtn = 0;
            if (c.Product.SSIS)
                rtn = SurrenderAmount / mvaFactor;
            else
                rtn = SurrenderAmount / ssiaFactor;
            return rtn;
        }
    }
}
