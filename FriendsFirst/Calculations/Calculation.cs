using FriendsFirst.Contracts;
using FriendsFirst.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Calculations
{
    /// <summary>
    /// Calculation that will operate with double values.
    /// </summary>
    public abstract class Calculation : Calculation<double, Contract>
    {
        public Calculation() : base(null) { }
    }
    /// <summary>
    /// Implements Validation Before and after the Calculation.
    /// </summary>
    /// <typeparam name="R">Return Type of the Calculation</typeparam>
    /// <typeparam name="I">Input object that provides all required values for the calculation that is passing in via the Value method.</typeparam>
    public abstract class Calculation<R, I>
    {
        #region 'Value' Method.  Allows use of class as and 'Instance'
        private I CalculationInputs { get; set; }
        public Calculation(I calculationInputs) {
            this.CalculationInputs = calculationInputs;
        }
        public R Value() {
            if (this.CalculationInputs == null)
                return default(R);

            return this.Value(this.CalculationInputs);
        }
        #endregion

        #region Validation
        public List<Validator<R, I>> ValidationFailures{get; protected set;}

        [System.Diagnostics.DebuggerStepThrough]
        public virtual IEnumerable<Validator> Validators(I contract) { return new Validator<R, I>[] { }; }

        [System.Diagnostics.DebuggerStepThrough]
        protected IEnumerable<Validator<R, I>> Validate(I contract, ValidationExecutions execution, R result)
        {
            var rtn = new List<Validator<R, I>>();
            foreach (var v in this.Validators(contract).Where(x=>x.Execution == execution))
            {
                Validator<R, I> validation = (Validator < R, I > )v.validate(contract, result);
                if (validation != null)
                    rtn.Add(validation);
            }

            return rtn;
        }
        #endregion

        /// <summary>
        /// Value that is to be added to a product
        /// </summary>
        /// <returns></returns>
        //[System.Diagnostics.DebuggerStepThrough]
        public R Value(I contract)
        {
            var rtn = default(R);

            this.ValidationFailures = new List<Validator<R, I>>();
            this.ValidationFailures.AddRange(this.Validate(contract, ValidationExecutions.Pre, rtn));
            if(this.ValidationFailures.Where(v=>v.PreventsExecution).Count() > 0)
                return default(R);

            rtn =  this.Calculate(contract);

            this.ValidationFailures.AddRange(this.Validate(contract, ValidationExecutions.Post, rtn));
            if (this.ValidationFailures.Where(v => v.PreventsExecution).Count() > 0)
                return default(R);

            return rtn;
        }

        /// <summary>
        /// Calcualtion Entry accepting a Contract object.
        /// All Values needed for Calculation are taken from 
        /// The contract and related objects.
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        protected abstract R Calculate(I contract);
    }


}
