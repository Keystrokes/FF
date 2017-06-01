using FriendsFirst.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst.Validators;

namespace FriendsFirst.Calculations.Annuities
{
    /// <summary>
    /// Compulsory purchase Annuity:  If the Mature date is within a
    /// defined period of time then CPA rates are used.  If not then the 
    /// normal base annuity rates are used.
    /// </summary>
    public class CPA : Annuity
    {
        //const int DaysTillApplicable = 50;
        public CPA() { }

        
        protected override double Calculate(Contract contract)
        {
            return base.Calculate(contract);
        }
        ///// <summary>
        ///// States if CPA rates are applicable.
        ///// </summary>
        //public bool IsApplicable
        //{
        //    get
        //    {
        //        var daysTil_Maturity = (base.Contract.Matureity - Global.SystemDate).TotalDays;
        //        return  daysTil_Maturity < DaysTillApplicable;
        //    }
        //}

        //public override double Value(contact)
        //{
        //    if (!this.IsApplicable)
        //        return base.Value(fund, rate);

        //    throw new NotImplementedException("TODO: Perform CPA Calculation");
        //}
    }

}
