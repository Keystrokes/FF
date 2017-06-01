using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst.Contracts;

namespace FriendsFirst.Validators
{
    public enum TimeFrames
    {
        Day,
        Week,
        Month,
        Year
    }
    /// <summary>
    /// Checks to see of the provided contract is going to mature within 
    /// a specified time frame
    /// </summary>
    public sealed class MaturesWithin : Validator<double, Contract>
    {
        TimeFrames Timeframe = TimeFrames.Month;
        int Delta = 0;
        public MaturesWithin(TimeFrames timeFrame, int delta)
        {
            this.Timeframe = timeFrame;
            this.Delta = delta;
        }

        MaturesWithin(int delta)
        {

        }
        internal override Validator<double, Contract> Validate(Contract c, double result)
        {
            throw new NotImplementedException();
        }
    }
}
