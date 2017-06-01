using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Funds
{
    public class UtalisedWithProfit : Fund
    {
        double _MarketValueAdjustment = 0;
        public double MarketValueAdjustment {
            get { return _MarketValueAdjustment; }
            set {
                if (value > 100 || value < 0)
                    throw new ArgumentOutOfRangeException("MarketValueAdjustment", "Percentage Value, Must be between 0 and 100");

                _MarketValueAdjustment = value;
            }
        }

        /// <summary>
        /// Applies an MVA to the Accumulated Value.
        /// </summary>
        /// <returns></returns>
        public override double Accumulated()
        {
            var rtn = base.Accumulated();
            rtn *= (1 - this.MarketValueAdjustment / 100.0);
            return rtn;
        }
    }
}
