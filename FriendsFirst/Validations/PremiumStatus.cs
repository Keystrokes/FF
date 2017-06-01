using FriendsFirst.Contracts;
using FriendsFirst.Premiums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Validators
{
    //public class InvalidPremiumStatusCheck : PremiumStatusCheck
    //{
    //    public InvalidPremiumStatusCheck()
    //}
    /// <summary>
    /// Prevents a Calculation if the contract premium matches one of the provided PremiumStatus enum
    /// </summary>
    /// <remarks>Pass in all Premium status which are to cause the Validaion to fail</remarks>
    public class InvalidPremiumStatusCheck : Validator<double, Contract>
    {
        /// <summary>
        /// Failes validation if the contract in is the listing
        /// of provided 'Invalid' states
        /// </summary>
        /// <param name="statusList">Statues in which validation fails if contract is in that status</param>
        public InvalidPremiumStatusCheck(params PremiumStatuses[] statusList)
        {
            this.StatusList = statusList;
        }
        InvalidPremiumStatusCheck(PremiumStatuses s) : this()
        {
            this.Description = $"Invalid { this.GetType().Name } Status : {s.ToString()}";
        }

        /// <summary>
        /// Status values that cause validation to fail
        /// </summary>
        protected PremiumStatuses[] StatusList { get; set; }

        internal override Validator<double, Contract> Validate(Contract c, double r)
        {
            //Check the premium status of the contract
            if (this.StatusList.Contains(c.Premium.Status))
                return new InvalidPremiumStatusCheck(c.Premium.Status);

            return null;
        }
    }
}
