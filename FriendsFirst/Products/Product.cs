using FriendsFirst.Premiums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Products
{
    /// <summary>
    /// Product that is sold by Friends First.
    /// 
    /// Specifies all of the variables that are considered
    /// in the calculation of the contract
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Description}")]
    public abstract class Product 
    {
        #region Meta Data
        /// <summary>
        /// ID Code of the product
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Description of the product
        /// </summary>
        public string Description { get; set; }
        #endregion

        #region Policy Charges
        public double Charge { get; set; }
        public double ChargeIncreasesRate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>1 = 100%</remarks>
        public double ChargePercent { get; set; }
        public bool ApplyPolicyChargeOnPaidUp { get; set; }
        #endregion

        #region Annuity / Pension Charges / Rates
        public double BaseMortalityYear { get; set; }
        public double AdjustedAnnuityRate { get; set; }
        public double MortalityImpactFactor { get; set; }

        /// <summary>
        /// Indexation Value used to obtain the Annutiy Rate
        /// from the database. (Changes the Annuity Value)
        /// </summary>
        public double EscalationRate { get; set; }
        public IEnumerable<Calculations.Annuities.Rate> AnnuityRates { get; set; }
        #endregion

        #region Tax Options
        public double ExitTaxRate { get; set; }
        #endregion

        #region Return Options
        public bool Guaranteed { get; set; }
        public bool SSIS { get; set; }
        #endregion

        public double MinFundAmount { get; set; }

        #region specific for Projections
        /// <summary>
        /// To be used for projections.  
        /// </summary>
        /// <returns></returns>
        internal Product Clone()
        {
            var rtn = (Product)MemberwiseClone();
            return rtn;
        }
        #endregion
    }
}
