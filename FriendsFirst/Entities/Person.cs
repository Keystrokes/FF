using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Entities
{
    public enum Genders
    {
        Male,
        Female,
        Unknown,
    }
    public class Person
    {
        public string Name { get; set; }
        public Genders Gender { get; set; }
        public DateTime DOB { get; set; }
        public DateTime NormalRetirementDate { get
            {
                return DOB.AddYears(Global.NormalRetirementAge);
            }
        }
    }
    
}
