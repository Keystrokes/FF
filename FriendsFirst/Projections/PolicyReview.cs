using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Projections
{
    /// <summary>
    /// Performs a Policy review process on a contract.
    /// </summary>
    public class PolicyReview
    {
        /*
            Validation:
                Invalid:    
                    Policy status = PU || PH
                    Premium frequency = Undefined || Single

            Variations
               ffProtectPolicyReviewDualLife
                ffProtectPolicyReview
                ffMortgagePolicyReviewDualLife  (will not be coded)
                ffIncomePolicyReviewDualLife    (will not be coded)
            
            Options
                IgnoreDebit -   States if the Debt on the contract should be considered  (More information required)

            
            Method
                1. Get the SustainabilityStart Date.
                    Not 100% sure what this is. 

        ***** Every thing hinges around the PerformMonthlyProjection() method
        ***** Gpgint to look in to that before this.
                
                
        */
    }
}
