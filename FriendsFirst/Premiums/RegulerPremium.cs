using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Premiums
{
    
    public class RegulerPaying : Premium
    {
        public RegulerPaying(PremiumFrequency frequency = PremiumFrequency.Monthly) : base(frequency) { }
    }
    
}
