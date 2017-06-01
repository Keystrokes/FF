using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Premiums
{
   /// <summary>
    /// Freqency of Monthly payment amounts
    /// </summary>
    public enum PremiumFrequency
    {

        /// <summary>
        /// Paid every 12 months
        /// </summary>
        Yearly = 12,

        /// <summary>
        /// Paid Every 6 months
        /// </summary>
        HalfYearly = 6,

        /// <summary>
        /// Paied every 4 months
        /// </summary>
        Quaterly = 4,

        /// <summary>
        /// Paid Every Month
        /// </summary>
        Monthly = 1,

        /// <summary>
        /// Only Paid on contract Concpetion.
        /// </summary>
        Single = 0,

        /// <summary>
        /// No Defined payment freqency or amount.  
        /// </summary>
        Variable = -1,
    }
}
