using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst.Validators;
using FriendsFirst.Contracts;

namespace FriendsFirst.Projections
{
    /// <summary>
    /// Implements mortification to contract to 
    /// play out "What If" :) scenarios.
    /// 
    /// A Projection consists of taking a contract and
    /// calculating its new value at the next premium payment 
    /// 
    /// This would include implementing:
    ///     Part Surrenders     
    ///     Premium Alterations
    ///     Charges
    ///     Single Premium Injections
    ///     Life Styling
    ///     Interest
    ///     Loyalty Bonus
    ///     
    /// </summary>
    public class Projection<T>
        where T : Contract
    {
        #region Allows the use of the class with either a Term or Date Indexer
        private T Contract { get; set; }

        /// <summary>
        /// Creats a projector for the specified contract type
        /// </summary>
        /// <param name="contract">Contract to project</param>
        public Projection(T contract)
        {
            this.Contract = (T)contract.Clone();
        }

        public ProjectedContract<T> this[DateTime d]
        {
            get
            {
                return this.Project(this.Contract, d).Last();
            }
        }

        public ProjectedContract<T> this[int term]
        {
            get
            {
                //Get the number of months into the contract that the 
                //Term Repersents.
                var monthsInToContract = 0;
                if (this.Contract.Premium.Frequencey == FriendsFirst.Premiums.PremiumFrequency.Single)
                    monthsInToContract = term;
                else
                    monthsInToContract = (int)this.Contract.Premium.Frequencey * term;

                var termDate = this.Contract.CommencmentDate.AddMonths(monthsInToContract);

                var rtn = this[termDate];
                return rtn;
            }
        }

        #endregion
        
        /// <summary>
        /// Projection of all premiums.
        /// </summary>
        public IEnumerable<Premiums.Premium> Premiums
        {
            get
            {
                var rtn = this.Projections
                    .Select(p =>
                    p.Contract.Premium);

                return rtn;
            }
        }

        /// <summary>
        /// Listing of alterations that are to be performed on the 
        /// projected contracts.
        /// </summary>
        public IEnumerable<PremiumAlteration> PremiumAlterations { get; set; }

        /// <summary>
        /// Create a listing of Projections of the contact to the specified 
        /// end date.
        /// </summary>
        /// <param name="c">Contract to project</param>
        /// <param name="ProjectionEndDate">Date to project to</param>
        /// <returns></returns>
        public IEnumerable<ProjectedContract<T>> Project(T c, DateTime ProjectionEndDate)
        {
            //TODO:  Put a check here to see if the projection all ready exists
            //Create a projection list starting with the contract 
            //in its present state
            var rtn = new List<ProjectedContract<T>>(){
                    new ProjectedContract<T>(c)
                };

            var nextPaymentDate = rtn.Last().Contract.NextPaymentDate;

            while (nextPaymentDate <= ProjectionEndDate &&
                rtn.Last().Contract.ContextDate != nextPaymentDate) //Do Not remove this condition.
            {
                //Clone the last projected contract 
                T contract = (T)rtn.Last().Contract.Clone();

                //Move the context date of the contract forward until the next
                //premium payment date.
                contract.ContextDate = nextPaymentDate;
                
                rtn.Add(
                    new ProjectedContract<T>(
                        contract));

                nextPaymentDate = rtn.Last().Contract.NextPaymentDate;
            }

            this.Projections = rtn;
            return rtn;
        }
        public IEnumerable<ProjectedContract<T>> Projections { get; private set; }


    }

}
