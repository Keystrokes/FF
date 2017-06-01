using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Funds
{
    public enum UnitTypes
    {
        Inital,
        Accumulated
    }

    /// <summary>
    /// Investment used to secure a Monetry value.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Code}  - Gross={Gross().ToString(\"C\")} Inital={Initial()} Accumulated={Accumulated()}")]
    public abstract class Fund
    {
        public Fund()
        {
            this.Code = string.Empty;
            this.Name = string.Empty;

            this.Units = new Unit();
            this.InitalUnitDiscountFactor = 1;
            this.InvestmentPercent = 1;
        }

        public string Code { get; set; }
        /// <summary>
        /// Fund Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Used in the Market Value Adjustment Calculation.
        /// </summary>
        public double InvestmentPercent { get; set; }

        /// <summary>
        /// Units that makes up the value of this Pension Product
        /// </summary>
        public Unit Units { get; set; }

        double _InitalUnitDiscountFactor = 0;

         /// <summary>
        /// Charge for the management of the fund
        /// </summary>
        public double ManagmentCharge { get; set; }

        /// <summary>
        /// Growth Rate of the fund.
        /// </summary>
        public double GrowthRate { get; set; }

        /// <summary>
        /// Amount which is subtracted from Inital units which acts
        /// as a fee when purchesing new Inital Units.
        /// </summary>
        public double InitalUnitDiscountFactor {
            get { return _InitalUnitDiscountFactor; }
            set { _InitalUnitDiscountFactor = value; }
        }
        
        /// <summary>
        /// States if capital prices should be used or Bid
        /// </summary>
        public bool Capital { get; set; }

        /// <summary>
        /// Price of the Fund.  Can be either the Bid Price of the Capital price.
        /// </summary>
        /// <returns></returns>
        public double Price()
        {
            if (this.Capital)
                return this.Units.Capital;

            return this.Units.Bid;
        }

        /// <summary>
        /// Initial Units * (Capital||Bid Price) * Initial Discount Factor
        /// </summary>
        /// <returns></returns>
        public virtual double Initial()
        {
            var rtn = this.Units.Inital 
                * this.Price()
                * this.InitalUnitDiscountFactor;
            return rtn;
        }   

        /// <summary>
        /// Accumulated Units * Bid Price
        /// </summary>
        /// <returns></returns>
        public virtual double Accumulated()
        {
            var rtn = this.Units.Accumulated * this.Units.Bid;
            return rtn;
        }

        /// <summary>
        /// Non Bonus Units Holdings * Bid Price
        /// </summary>
        /// <returns></returns>
        public virtual double NonBonus()
        {
            var rtn = this.Units.NonBonus * this.Units.Bid;
            return rtn;
        }

        /// <summary>
        /// Current calculated value of the fund
        /// </summary>
        /// <returns></returns>
        public double Gross()
        {
            var rtn = this.Initial()
                + this.Accumulated()
                + this.NonBonus();

            return rtn;

        }


        #region specific for Projections
        /// <summary>
        /// To be used for projections.  
        /// </summary>
        /// <returns></returns>
        internal Fund Clone()
        {
            var rtn = (Fund)MemberwiseClone();
            rtn.Units = this.Units.Clone();
            return rtn;
        }
        #endregion

    }
}
