using FriendsFirst.Contracts;
using FriendsFirst.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Calculations
{
    public class InitalUnitInvestment : Calculations.Calculation
    {
        /// <summary>
        /// In C++ this is hard-coded to False.
        /// </summary>
        public bool FlexibleCommission { get { return false; } }
        #region Rates that are gathered from the Allocation Rates Table
        /// <summary>
        /// ICRate
        /// </summary>
        public double InitialCommisionRate { get; set; }
        public double NFRate { get; set; }
        /// <summary>
        /// RCRate
        /// </summary>
        public double RenewalCommisionRate { get; set; }
        public double FNRate { get; set; }
        public double FFRate { get; set; }
        
        #endregion

        protected override double Calculate(Contract contract)
        {
            var rtn = ((1 - this.InitialCommisionRate) * this.NFRate)
                    + ((1 - this.RenewalCommisionRate) * this.FNRate)
                    + ((this.InitialCommisionRate + this.RenewalCommisionRate - 1) * this.FFRate);

            return rtn;
        }
    }

}
