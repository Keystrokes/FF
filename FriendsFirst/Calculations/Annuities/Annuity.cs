using FriendsFirst.Contracts;
using FriendsFirst.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Calculations.Annuities
{
    public class Annuity : Calculations.Calculation
    {
        /// <summary> 
        /// Annuity Rate that is applicable for the Age and Escalation 
        /// </summary> 
        public double Rate(Contract contract, double age, double escalationRate)
        {
            if (contract.Product.AnnuityRates == null
                || contract.Product.AnnuityRates.Count() == 0)
                throw new NotImplementedException("No Rate Found");

            var rate = contract.Product.AnnuityRates
                    .Where(r => r.RETIREMENTAGE == age)
                    .Where(r => r.SEX == "M")                           //Can not differentiate between male and Female.  Treat all as Male. 
                    .Where(r => r.INDEXATION == escalationRate)
                    .SingleOrDefault();

            if (rate == null)
                throw new NotImplementedException($"No Rate Found: Age {age}, Escalation: {escalationRate}");

            //Using the Age and Indexation get the correct Annuity Rate from the provided rates. 
            return rate.CURRENTANNUITYRATE;
        }


        protected override double Calculate(Contract contract)
        {
            var fund = contract.Gross();

            var rate = this.Rate(
                contract,
                contract.Coverages.MaxAge(),
                contract.Product.EscalationRate);

            return Calculate(fund, rate);
           
        }

        public double Calculate(
            double Fund,
            double Rate)
        {
            if (Rate == 0)
                return 0;

            var rtn = Fund * Rate;
            return rtn;
        }

    }
}

