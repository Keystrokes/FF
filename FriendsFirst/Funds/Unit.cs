using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Funds
{
    /// <summary>
    /// Fund units which are equatable to cash
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("I-{Inital} A-{Accumulated} B-{Bid}")]
    public struct Unit
    {
        public Unit(
            double inital,
            double accumulated,
            double bid = 1,
            double capital = 1,
            double nonBonus = 0)
        {
            if (bid <= 0) bid= 1;
            this.Bid = bid;
            this.Capital = capital;
            this.Inital = inital;
            this.Accumulated = accumulated;
            this.NonBonus = nonBonus;
        }

        public double Bid { get; private set; }
        public double Capital { get; private set; }

        public double Inital { get; private set; }
        public double Accumulated { get; private set; }
        public double NonBonus { get; private set; }


        #region Implicit Conversions
        public static implicit operator double(Unit u)
        {
            return 0;
        }
        #endregion
        /// <summary>
        /// Add the equivalent amount in Units
        /// </summary>
        public static Unit operator +(Unit c1, double amount)
        {

            var accumulated = c1.Accumulated + (amount / c1.Bid);

            return new Unit(
                c1.Inital,
                accumulated,
                c1.Bid,
                c1.NonBonus);
        }

        /// <summary>
        /// Subtracts the equivalent amount in Units
        /// </summary>
        public static Unit operator -(Unit c1, double c2)
        {
            if (c2 == 0)
                return c1;

            var inital = c1.Inital;
            var accumulated = c1.Accumulated;

            //Remove the amount from the accumilated first
            accumulated -= c2;
            
            //If Amount was more than aviable Accumulated
            if (accumulated < 0)
            {
                //Remove the deficet from the inital
                inital += accumulated;
                //Set the accumulated to Zero
                accumulated = 0;
            }

            //Return back new Unit Structure.
            return new Unit(
                inital, 
                accumulated, 
                c1.Bid, 
                c1.Capital);
        }

        #region specific for Projections
        /// <summary>
        /// To be used for projections.  
        /// </summary>
        /// <returns></returns>
        internal Unit Clone()
        {
            var rtn = (Unit)MemberwiseClone();
            return rtn;
        }
        #endregion

    }
}
