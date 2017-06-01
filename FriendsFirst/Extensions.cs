using FriendsFirst.Calculations.Bonuses;
using FriendsFirst.Contracts;
using FriendsFirst.Entities;
using FriendsFirst.Funds;
using FriendsFirst.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst
{
    public static class Extensions
    {
        #region Math
        /// <summary>
        /// Repersentative rate value in accordance of the number of months
        /// have elapsed in a year.  
        /// Basicly each month earns 1/12 of the rate.
        /// </summary>
        /// <param name="rate">rate to get fraction of</param>
        /// <param name="month">Number of months to apply rate to </param>
        /// <returns></returns>
        public static double Repersentative(this double rate, int month)
        {
            double fractionOfMonth = ((double)month) / Global.MONTHS_IN_YEAR;
            var rtn = Math.Pow(
                rate,
                fractionOfMonth);

            return rtn;
        }
        #endregion

        #region Contract
        /// <summary>
        /// NUmber of months that have passed since last renewal
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static int MonthsIntoYear(this Contract c)
        {
            var lastRenewal = c.LastRenewalDate();

            int month = 0;
            while (lastRenewal.AddMonths(month) < c.ContextDate)
                month++;

            return month;
        }

        /// <summary>
        /// All renewal dates from contract commencement
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> RenewalDates(this Contract c)
        {
            var rtn = new List<DateTime>();

            //Start at the commencment date add keep 
            //Adding Years 
            var date = c.CommencmentDate;
            while (date <= c.ContextDate.AddYears(1))
            {
                //Give the next renewal date as well
                rtn.Add(date);
                date = date.AddYears(1);
            }

            return rtn;
        }

        public static DateTime LastRenewalDate(this Contract c)
        {
            var renewalDates = c.RenewalDates();
            var rtn = renewalDates.Where(
                            d =>
                                d <= c.ContextDate
                            );

            return rtn.Max();
        }
        public static DateTime NextRenewalDate(this Contract c)
        {
            return c.RenewalDates().Where(
                            d =>
                                d > c.ContextDate
                            ).Min();
        }
        /// <summary>
        /// States if the Contract is due for renewal on the current
        /// operation date.
        /// 
        /// required for Yearly Policy Charge.
        /// </summary>
        /// <returns></returns>
        internal static bool DueForRenewal(this Contract c)
        {
            return c.DueForRenewal(c.ContextDate);
        }
        internal static bool DueForRenewal(this Contract c, DateTime date)
        {
            //var rtn = c.CommencmentDate.Day == date.Day &&
            //    c.CommencmentDate.Month == date.Month &&
            //    c.CommencmentDate.Year != date.Year;

            var rtn = c.CommencmentDate.Month == date.Month &&
                c.CommencmentDate.Year < date.Year;

            return rtn;
        }
       
        internal static double RemainingTerms(this Contract c)
        {
            return c.Term(c.Matures);
        }
        internal static double Term(this Contract c, DateTime date)
        {
            //Start at the commemcment date and keep
            //Adding months at the rate of Premium Freqency
            //until we reach the contract's Context Date.
            var term = 0;
            var termDate = c.CommencmentDate;
            var monthsInTerm = (int)c.Premium.Frequencey;

            while (termDate < date)
            {
                termDate = termDate.AddMonths(monthsInTerm);
                term++;
            }

            return term;
            
        }

        #endregion

        #region Person
        internal static double Age(this Person p)
        {
            return p.Age(Global.SystemDate);
        }
        internal static double Age(this Person p, DateTime atDate)
        {
            return p.DOB.Age(atDate);
        }
        public static double Age(this DateTime DateOfBirth, DateTime atDate)
        {
            var age = atDate - DateOfBirth;
            var rtn = age.TotalDays / Global.DAYS_IN_YEAR;
            rtn = Math.Round(rtn, 2);

            return rtn < 0.01 ? 0 : rtn;
        }
        internal static double MaxAge(this IEnumerable<Person> persons)
        {
            return persons.MaxAge(Global.SystemDate);
        }
        public static double MaxAge(this IEnumerable<Person> persons, DateTime atDate)
        {
            if (persons.Count() <= 0)
                return 0;

            var rtn = persons.Max(p => p.Age(atDate));
            return rtn;
        }
        #endregion

        #region Benefits
        internal static double SumAssured(this IEnumerable<Benefit> benefits)
        {
            if (benefits == null)
                return 0.0;

            var rtn = benefits.Sum(b => b.SumAssured);
            return rtn;
        }
        #endregion

        #region Bonuses
        
        #endregion
    }
}
