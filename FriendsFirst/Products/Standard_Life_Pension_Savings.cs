using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Products
{
    public class StandardPensionProduct 
        : Product
    {
        public StandardPensionProduct()
        {
            this.Code = "SPP-1";
            this.Description = "Standard Pension Product 1";


        }
    }

    public class StandardLifeProduct
        : Product
    {
        public StandardLifeProduct()
        {
            this.Code = "SLP-1";
            this.Description = "Standard Life Product 1";
        }
    }

    public class StandardSavingsProduct
        : Product
    {
        public StandardSavingsProduct()
        {
            this.Code = "SSP-1";
            this.Description = "Standard Savings Product 1";
        }
    }
}
