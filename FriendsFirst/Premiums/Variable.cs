using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Premiums
{
   

    public class Variable : Premium
    {
        public Variable(PremiumFrequency frequency) : base(PremiumFrequency.Variable) { }
    }
    
}
