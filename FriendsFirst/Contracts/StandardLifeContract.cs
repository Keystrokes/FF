using FriendsFirst.Contracts;
using FriendsFirst.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Contracts
{
    /// <summary>
    /// Products that cater for live cover.
    /// </summary>
    public class StandardLifeContract : Contract
    {
        
        public override DateTime Matures
        {
            get { return this.RiskCessation; }
        }

        /// <summary>
        /// Date in which Coverage cease for those Covered.
        /// </summary>
        public DateTime RiskCessation { get; set; }
    }

    
}
