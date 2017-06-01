using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Funds
{
    /// <summary>
    /// Collection of funds which are attached to a contract 
    /// to which it gives value.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Gross().ToString(\"C\")}")]
    public class InvestedFunds : IEnumerable<Fund>
    {
        public InvestedFunds()
        {
            this.Items = new List<Fund>()
            {
                new UnitLinkedFund()
            };
        }

        /// <summary>
        /// Each individual Funds invested in.
        /// </summary>
        public IEnumerable<Fund> Items { get; set; }

        /// <summary>
        /// Increasses the underlying Units of each fund in accordance 
        /// to the fund split
        /// </summary>
        /// <param name="funds">Fund to increass underlying unit amounts</param>
        /// <param name="amount">Amount to be used to buy units at bid price</param>
        /// <returns></returns>
        public static InvestedFunds operator + (InvestedFunds funds, double amount)
        {
            var newItems = new List<Fund>();
            foreach(var f in funds)
            {
                var fund = f.Clone();
                fund.Units += amount * fund.InvestmentPercent;

                newItems.Add(fund);
            }

            var rtn = new InvestedFunds()
            {
                Items = newItems
            };

            return rtn;
        }

        /// <summary>
        /// Decreasses the underlying Units of each fund in accordance 
        /// to the fund split
        /// </summary>
        /// <param name="funds">Fund to increass underlying unit amounts</param>
        /// <param name="amount">Amount to be used to buy units at bid price</param>
        /// <returns></returns>

        public static InvestedFunds operator - (InvestedFunds funds, double amount)
        {
            var newItems = new List<Fund>();
            foreach (var f in funds)
            {
                var fund = f.Clone();
                fund.Units -= amount * fund.InvestmentPercent;

                newItems.Add(fund);
            }

            var rtn = new InvestedFunds()
            {
                Items = newItems
            };

            return rtn;
        }
        /// <summary>
        /// Total Inital Value of Funds
        /// </summary>
        /// <param name="funds"></param>
        /// <returns></returns>
        internal double Inital()
        {
            var rtn = Items
                .Sum(f => f.Initial());
            return rtn;
        }

        /// <summary>
        /// Total Investement Percent of Funds
        /// </summary>
        /// <param name="funds"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public double InvestmentPercent<F>()
        {
            var rtn = Items
                .Where(f => f.GetType() == typeof(F))
                .Sum(f => f.InvestmentPercent);
            return rtn;
        }
        
        /// <summary>
        /// Total Accumulated Value Funds
        /// </summary>
        /// <param name="funds"></param>
        /// <returns></returns>
        internal double Accumulated()
        {
            var rtn = Items
                .Sum(f => f.Accumulated());
            return rtn;
        }

        /// <summary>
        /// Total Value of all funds
        /// </summary>
        /// <param name="funds"></param>
        /// <returns></returns>
        public double Gross()
        {
            var rtn = Items.Sum(f => f.Gross());
            return rtn;
        }

        #region IEnumerator
        public IEnumerator<Fund> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        #endregion

        #region specific for Projections
        /// <summary>
        /// To be used for projections.  
        /// </summary>
        /// <returns></returns>
        internal InvestedFunds Clone()
        {
            var items = new List<Funds.Fund>();
            foreach (var item in this.Items)
                items.Add(item.Clone());

            var rtn = (InvestedFunds)MemberwiseClone();
            rtn.Items = items;

            return rtn;
        }
        #endregion
    }
}
