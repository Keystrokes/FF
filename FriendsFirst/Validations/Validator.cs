using FriendsFirst.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Validators
{
    public enum ValidationExecutions
    {
        Pre,
        Post
    }
    
    /// <summary>
    /// Implements a Validation routine which states if
    /// a Calculation should return the default value or
    /// the calculated value.
    /// </summary>
    /// <typeparam name="R">Type of result that would be returned by the Calculation</typeparam>
    /// <typeparam name="C">Type of the object that is to be used to provide all inputes required by the the calcualtion</typeparam>
    public abstract class Validator<R, C>: Validator
    {
        public Validator<R, C> Validate(C c)
        {
            return this.Validate(c, default(R));
        }

        internal abstract Validator<R, C> Validate(C c, R result);

        /// <summary>
        /// Used by the internal Calculation Classes to 
        /// perform validation before and after a calculation
        /// </summary>
        /// <param name="c">Object that contains all source values</param>
        /// <param name="result">Result of the validation</param>
        /// <returns></returns>
        internal override Validator validate(object c, object result)
        {
            return (Validator)this.Validate((C)c, (R)result);
        }
    }

    public abstract class Validator
    {
        /// <summary>
        /// States if the validation should be performed before or after the calcualtion is performed.
        /// </summary>
        public virtual ValidationExecutions Execution { get { return ValidationExecutions.Pre; } }

        /// <summary>
        /// Allows the Validator to be configured as optional (per say)
        /// </summary>
        public virtual bool PreventsExecution { get { return true; } }

        /// <summary>
        /// Reference code for the Validation object.
        /// </summary>
        public string Code { get; protected set; }

        /// <summary>
        /// Description of the Validation object to give indications of its 
        /// purpose.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Used by the internal Calculation Classes to 
        /// perform validation before and after a calculation
        /// </summary>
        /// <param name="c">Object that contains all source values</param>
        /// <param name="result">Result of the validation</param>
        /// <returns></returns>
        internal abstract Validator validate(object c, object result);
    }

}
