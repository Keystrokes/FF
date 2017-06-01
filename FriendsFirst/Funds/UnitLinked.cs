using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Funds
{
    public class UnitLinkedFund : Fund
    {
        public UnitLinkedFund()
        {
            this.Units = new Unit(
                0,
                0,
                1,
                1);
        }
    }
}
