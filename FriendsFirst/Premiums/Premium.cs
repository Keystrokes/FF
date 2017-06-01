using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsFirst.Contracts;

namespace FriendsFirst.Premiums
{
    /// <summary>
    /// A Payment which is paid to substation a product in a contact.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Frequencey.ToString()} {Amount.ToString(\"C\")}")]
    public abstract class Premium 
    {
        protected Premium(PremiumFrequency frequencey) :
            this(
                frequencey,
                PremiumStatuses.InForce,
                0,
                0)
        { }

        internal Premium(
            PremiumFrequency frequencey,
            PremiumStatuses status,
            double amount,
            double indexationRate)
        {
            this.Frequencey = frequencey;
            this.Status = status;
            this.Amount = amount;
            this.IndexationRate = indexationRate;
        }
        

        /// <summary>
        /// Rate increase which occurs on the renewal date.
        /// </summary>
        public double IndexationRate { get; set; }

        /// <summary>
        /// Premium amount
        /// </summary>
        public double Amount { get; set; }  
        /// <summary>
        /// Status of the payment of this premium
        /// </summary>
        public PremiumStatuses Status { get; set; }
        /// <summary>
        /// Freqency in which payments are to be made.
        /// </summary>
        public PremiumFrequency Frequencey { get; set; }

        #region specific for Projections
        /// <summary>
        /// To be used for projections.  
        /// </summary>
        /// <returns></returns>
        internal Premium Clone()
        {
            var rtn = (Premium)MemberwiseClone();
            return rtn;
        }
        #endregion
    }
}
