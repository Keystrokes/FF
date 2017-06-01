using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Export
{
    public static class CSV
    {
        public static void DumpAndOpen<C>(this Projections.Projection<C> projection, string delimiter = ", ") where C : Contracts.Contract
        {
            //var filename = $"temp_{typeof(C).Name}_{DateTime.Now.ToString("yyyyMMddhhmmss")}.csv";
            //var csv = projection.DumpCsv<C>();

            ////try
            ////{
            //    System.IO.File.WriteAllText(filename, csv);
            //    var process = System.Diagnostics.Process.Start(filename);
            //    process.Exited += (o, s) =>
            //    {
            //        System.IO.File.Delete(filename);
            //    };
            
        }
        public static string DumpCsv<C>(this Projections.Projection<C> projection, string delimiter = ", ")where C : Contracts.Contract
        {
            var rtn = new StringBuilder();
            var data = new List<object>();
            string[] header = new string[] { };

            foreach (var p in projection.Projections)
                rtn.AppendLine(
                    string.Join(delimiter, p.ToArray(out header)));

            rtn.Insert(0, string.Join(delimiter, header) + "\r\n");
            return rtn.ToString();
        }

        public static object[] ToArray<C>(
            this Projections.ProjectedContract<C> projection,
            out string[] header) where C:Contracts.Contract
        {
            var h = new List<string>
            {
                "projection.Date",
                "projection.PolicyCharge",
                "projection.PolicyChargeIndexation",
                "projection.PremiumIndexation",
                "projection.PremiumPayment",
                "projection.FundInterest",
                "projection.Contract.Gross",
            };
            var rtn = new List<object>
            {
                projection.Date,
                projection.PolicyCharge,
                projection.PolicyChargeIndexation,
                projection.PremiumIndexation,
                projection.PremiumPayment,
                projection.FundInterest,
                projection.Contract.Gross(),
                //projection.FundInterest(),
                //projection.PolicyCharge(),
            };

            string[] subHeader;
            rtn.AddRange(projection.Contract.Premium.ToArray(out subHeader));
            h.AddRange(subHeader);
            rtn.AddRange(projection.Contract.Funds.ToArray(out subHeader));

            h.AddRange(subHeader);
            header = h.ToArray();
            return rtn.ToArray();
        }
        public static object[] ToArray(this Premiums.Premium premium, out string[] header)
        {
            header = new string[]
            {
                "Premium.Amount",
                "Premium.Frequencey",
                "Premium.IndexationRate",
                "Premium.Status"
            };
            var rtn = new object[] {
                premium.Amount,
                premium.Frequencey,
                premium.IndexationRate,
                premium.Status
            };

            return rtn;
        }

        public static object[] ToArray(this IEnumerable<Funds.Fund> funds, out string[] header)
        {
            var rtn = new List<object>();
            var fundHeaders = new List<string>();
            header = new string[] { };
            foreach (var f in funds)
            {
                rtn.AddRange(f.ToArray(out header));
                fundHeaders.AddRange(header);
            }

            header = fundHeaders.ToArray();
            return rtn.ToArray();
        }
        public static object[] ToArray(this Funds.Fund fund, out string[] header)
        {
            var h = new List<string>()
            {
                "Fund.Code", 
                "Fund.Gross", 
                "Fund.GrowthRate", 
                "Fund.InvestmentPercent", 
                "Fund.InitalUnitDiscountFactor", 
                "Fund.ManagmentCharge"
            };
            var rtn = new List<object>()
            {
                fund.Code ,
                fund.Gross() ,
                fund.GrowthRate ,
                fund.InvestmentPercent ,
                fund.InitalUnitDiscountFactor ,
                fund.ManagmentCharge ,
            };

            string[] fundHeader;
            rtn.AddRange(fund.Units.ToArray(out fundHeader));

            h.AddRange(fundHeader);
            header = h.ToArray();
            return rtn.ToArray();
        }
        public static object[] ToArray(this Funds.Unit unit, out string[] header)
        {
            header = new string[]
            {
                "Unit.Inital", 
                "Unit.Accumulated",
                "Unit.Bid", 
                "Unit.Capital", 
            };
            var rtn = new object[]{
                unit.Inital ,
                unit.Accumulated, 
                unit.Bid ,
                unit.Capital 
            };

            return rtn;
        }
    }
}
