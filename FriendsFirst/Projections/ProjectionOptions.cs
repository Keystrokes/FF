using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Projections
{
    /// <summary>
    /// Structure to allow for the alteration of the contract during the projection
    /// </summary>
    /// <remarks>Allows for the changing of premium amounts and fund amounds to play out future scenarios</remarks>
    public class ProjectionOptions
    {
        /// <summary>
        /// Date in which premiums are to 
        /// </summary>
        public DateTime? Premium_Cessaion { get; set; }
        public DateTime? Coverage_Cessation { get; set; }

    }
}
