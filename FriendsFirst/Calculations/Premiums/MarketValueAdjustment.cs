using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst.Contracts;

namespace FriendsFirst.Calculations
{
    /// <summary></summary>
    public class MarketValueAdjustmentFactor : Calculation
    {
        double PenaltyRate { get { throw new NotImplementedException(); } }
        double Adjustment { get { return 0; } }

        protected override double Calculate(Contract c)
        {
            var linkedInvestmentPercent = c.Funds.InvestmentPercent<Funds.UnitLinkedFund>();

            return Calculate(linkedInvestmentPercent,
                                PenaltyRate,
                                Adjustment);
        }

        public double Calculate(
            double LinkedInvestmentPercent,
            double PenaltyRate,
            double Adjustment)
        {
            var rtn = (LinkedInvestmentPercent * (1 - PenaltyRate) * (1 - (Adjustment / 100))
                + (LinkedInvestmentPercent * (1 - PenaltyRate)));
            return rtn;
        }
    }

}
