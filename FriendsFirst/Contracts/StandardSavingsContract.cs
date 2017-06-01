using FriendsFirst.Contracts;
using FriendsFirst.Entities;
using FriendsFirst.Premiums;
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
    public class StandardSavingsContract : Contract
    {
        public StandardSavingsContract()
        {
            this.Product = new Products.StandardSavingsProduct();

            this.Premium = new Premiums.Variable(PremiumFrequency.Monthly);

            this.Funds = new Funds.InvestedFunds();
        }
    }

}
