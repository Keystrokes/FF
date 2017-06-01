using FriendsFirst.Calculations;
using FriendsFirst.Calculations.Charges;
using FriendsFirst.Calculations.Premiums;
using FriendsFirst.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Projections
{
    /// <summary>
    /// Wrapper for a Contract which provide a Projected view of the contract in a future date.
    /// </summary>
    /// <remarks>Created by the Projection object to create repersentation of a Contract in a Future date where all calculations are performed and Fund values are altered.</remarks>
    [System.Diagnostics.DebuggerDisplay("{Date.ToShortDateString() {PremiumPayment} {Contract.Gross()}")]
    public struct ProjectedContract<C>
            where C : Contract
    {
        public ProjectedContract(C c) 
        {
            this.Contract = (C)c.Clone();
            this.Options = new ProjectionOptions();
            this.RunCycle();
        }

        public ProjectionOptions Options { get; set; }
       
        /// <summary>
        /// Projection Date
        /// </summary>
        public DateTime Date { get { return Contract.ContextDate; } }

        /// <summary>
        /// Projected Contract
        /// </summary>
        public C Contract { get; }

        /// <summary>
        /// Execute a Calculation against the Contracts current state.
        /// </summary>
        /// <typeparam name="Calc">Calculation to be performed</typeparam>
        /// <returns>Return value of the calcultion</returns>
        double Calculate<Calc>()
            where Calc : Calculations.Calculation, new()
        {
            var rtn = 0.0;
            var calculator = new Calc();
            rtn = calculator.Value(this.Contract);
            return rtn;

        }
        public double PremiumIndexation { get { return Calculate<PremiumIndexation>(); } }
        public double PolicyChargeIndexation { get { return Calculate<PolicyChargeIndexation>(); } }
        public double PolicyCharge { get { return Calculate<PolicyCharge>(); } }
        public double BenefitCharge { get { return Calculate<BenefitCharge>(); } }
        public double FundInterest { get { return Calculate<FundInterest>(); } }
        public double PremiumPayment { get { return Calculate<PremiumPayment>(); } }

        /// <summary>
        /// Performs all monthly alterations to the contract.
        /// </summary>
        void RunCycle()
        {
            //Indexation
            this.Contract.Premium.Amount += PremiumIndexation;
            this.Contract.Product.Charge += PolicyChargeIndexation;

            //Charges
            this.Contract.Funds -= PolicyCharge;
            this.Contract.Funds -= BenefitCharge;

            //Add Fund Interest
            this.Contract.Funds += FundInterest;

            //Appy Premium payment 
            this.Contract.Funds += PremiumPayment;

        }
    }
}
