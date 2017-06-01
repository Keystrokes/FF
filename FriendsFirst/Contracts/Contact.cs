using FriendsFirst.Premiums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst.Projections;
using FriendsFirst.Entities;
using FriendsFirst.Products;
using FriendsFirst.Funds;
using FriendsFirst.Calculations;

namespace FriendsFirst.Contracts
{


    /// <summary>
    /// Customer and Product agreement
    /// </summary>
    public abstract class Contract
    {
        public Contract()
        {
            this.ContextDate = Global.SystemDate.Date;
            this.CommencmentDate = this.ContextDate;
        }

        /// <summary>
        /// Reference number for the contract.
        /// </summary>
        public string ContractNumber { get; set; }
        /// <summary>
        /// Date in which all time baised calulations are to be 
        /// performed on. 
        /// The day value will always be the commencement day value
        /// </summary>
        /// TODO:  This SHould be internal.  It is only public for unit testing.
        public DateTime ContextDate
        {
            get {
                return new DateTime(
                  _ContextDate.Year,
                  _ContextDate.Month,
                  CommencmentDate.Day);
            }
            set { _ContextDate = value; }
        }
        DateTime _ContextDate;

        /// <summary>
        /// Stipulates all options applicable on the contract.
        /// </summary>
        public Products.Product Product { get;set; }
        public virtual IEnumerable<Calculation> Bonuses { get; set; }
        public double Bonus()
        {
            var rtn = this.Bonuses.Sum(b => b.Value(this));
            return rtn;
        }
        public Funds.InvestedFunds Funds { get; set; }
        public IEnumerable<Benefit> Benefits { get; set; }

        /// <summary>
        /// Cessation date when the contract terminates fully.
        /// IN the case of a life contract this is the RiskCessatoin,
        /// in regards to a retirement it would be the Retirement Date or NRA/NRD
        /// </summary>
        public virtual DateTime Matures { get { return _Matures.Date; } set { _Matures = value; } }
        DateTime _Matures;

        /// <summary>
        /// Starting date of the contract
        /// </summary>
        public DateTime CommencmentDate { get { return _CommencmentDate.Date; } set { _CommencmentDate = value; } }
        DateTime _CommencmentDate;

        /// <summary>
        /// Payment used to maintain the fund growth and fees
        /// </summary>
        public Premium Premium { get; set; }
        
        /// <summary>
        /// This is the number of people that are on this Contract.
        /// Such as Duel Life and single life.
        /// </summary>
        public IEnumerable<Person> Coverages { get; set; }

        #region Repersentations of Contract Value
        /// <summary>
        /// Current value of the contract considering Current Market
        /// Adjustments.
        /// </summary>
        /// <returns>Gross Value Times MVA</returns>
        internal double CurrentValue()
        {
            var mav = new Calculations.MarketValueAdjustmentFactor();

            var mvaFactor = mav.Value(this);
            var rtn = Gross() * mvaFactor;

            return rtn;
        }
        /// <summary>
        /// Gross value of the Contracts funds.
        /// </summary>
        /// <returns></returns>
        public double Gross()
        {
            var rtn = Funds.Gross();
            return rtn;
        }
        /// <summary>
        /// Gross Value of the Funds plus any bonuses
        /// minus any deductions.
        /// </summary>
        /// <returns></returns>
        internal double Net()
        {
            var rtn = Gross() + Bonus();
            return rtn;
        }

        /// <summary>
        /// Current sum at risk offset by the current fund value.
        /// </summary>
        /// <returns></returns>
        public double SumAtRisk()
        {
            var calculation = new Calculations.SumAtRisk();
            var rtn = calculation.Value(this);
            return rtn;
        }
        #endregion

        #region specific for Projections
        /// <summary>
        /// To be used for projections.  
        /// </summary>
        /// <returns></returns>
        internal Contract Clone()
        {
            var rtn =  (Contract)MemberwiseClone();
            rtn.Funds = this.Funds.Clone();
            rtn.Premium = this.Premium.Clone();
            rtn.Product = this.Product.Clone();
            return rtn;
        }

        /// <summary>
        /// Date in which next payment is due
        /// </summary>
        public DateTime NextPaymentDate
        {
            get
            {
                var months = (int)this.Premium.Frequencey;

                if (this.Premium.Frequencey == PremiumFrequency.Single)
                    months = 1;

                var rtn = this.ContextDate.AddMonths(months);
                return rtn.Date;
            }
        }

        #endregion
    }


}
