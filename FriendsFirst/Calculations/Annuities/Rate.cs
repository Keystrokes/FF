using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Calculations.Annuities
{
    public class Rate
    {
        public Rate()
        {
            RETIREMENTAGE = 0;
            SEX = string.Empty;
            INTERESTRATE = 0;
            INDEXATION = 0;
            CURRENTANNUITYRATE = 0;
        }

        public double RETIREMENTAGE { get; set; }
        public string SEX { get; set; }
        public double INTERESTRATE { get; set; }
        public double INDEXATION { get; set; }
        public double CURRENTANNUITYRATE { get; set; }
    }

}
