using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Products
{
    public class Benefit
    {
        public string Code { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Date in which the benefit becomes effective
        /// </summary>
        public DateTime Effective { get; set;}

        /// <summary>
        /// Date in which the benefit is to cease.
        /// </summary>
        public DateTime Cessation { get; set; }

        /// <summary>
        /// Billing rate 
        /// Used By: 
        ///     BenefitCharge Calculation
        /// </summary>
        public double BillingRate { get; set; }

        /// <summary>
        /// Sum Assured on the Benefit
        /// Used By: 
        ///     SumAssured Calculation
        /// </summary>
        public double SumAssured { get; set; }

        /// <summary>
        /// Rate in which the cost of the Benefit should increass on a Yearly Basies.
        /// </summary>
        public double IndexationRate { get; set; }
    }
}
