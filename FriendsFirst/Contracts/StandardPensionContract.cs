using FriendsFirst.Calculations;
using FriendsFirst.Calculations.Annuities;
using FriendsFirst.Premiums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Contracts
{
    /// <summary>
    /// Products that cater for Pension cover.
    /// </summary>
    public class StandardPensionContract : Contract
    {
        /// <summary>
        /// Normal Retirment Age.  Govermental defined Value.
        /// </summary>
        public static int NRA { get { return 65; } }

        public StandardPensionContract()
        {
            this.Product = new Products.StandardPensionProduct();

            this.Bonuses = new Calculation[]
            {
                new Calculations.Bonuses.Lotalty()
            };

            this.Premium = new RegulerPaying(PremiumFrequency.Monthly);

            this.Funds = new Funds.InvestedFunds();

        }

        /// <summary>
        /// Date in which the Oldest Hits NRA.
        /// </summary>
        public override DateTime Matures
        {
            get {
                var rtn = this.Coverages.Max(p=> p.NormalRetirementDate);
                return rtn;
            }
        }

        /// <summary>
        /// Payments that are drawn down from the total funds available
        /// </summary>
        public double Annuity ()
        {
            var calculation = new CPA();
            var rtn = calculation.Value(this);
            return rtn;
        }
    }
}
