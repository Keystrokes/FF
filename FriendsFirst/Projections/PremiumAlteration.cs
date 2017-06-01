using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Projections
{
    /// <summary>
    /// A Change that is to be performed to the premium 
    /// amount at a specific time in the future.
    /// </summary>
    public class PremiumAlteration
    {
        /// <summary>
        /// Date alteration becomes effective.
        /// </summary>
        public DateTime Effective { get; set; }

        /// <summary>
        /// New Premium amount to be applied.
        /// </summary>
        public double Amount { get; set; }
    }


}
