using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst.Contracts;
using FriendsFirst.Validators;
using FriendsFirst.Premiums;

namespace FriendsFirst.Validators
{
    /// <summary>
    /// Fails of the contract is not on Renewal Date.
    /// </summary>
    public sealed class OnlyAtRenewal : Validator<double, Contract>
    {
        public OnlyAtRenewal() { }
        internal OnlyAtRenewal(DateTime commence, DateTime current)
        {
            this.Description = $"{current.ToShortDateString()} is Not a renewal date for {current.ToShortDateString()}";
        }

        internal override Validator<double, Contract> Validate(Contract c, double result)
        {
            return Validate(
                c.CommencmentDate,
                c.ContextDate);
        }

        public Validator<double, Contract> Validate(
            DateTime CommencmentDate,
            DateTime CurrentDate)
        {
            //Check that the day and the month match up.
            if (CommencmentDate.Month == CurrentDate.Month &&
                CommencmentDate.Year != CurrentDate.Year)
                return null;

            return new OnlyAtRenewal(CommencmentDate, CurrentDate);
        }
    }
}
