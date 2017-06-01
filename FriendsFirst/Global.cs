using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst
{
    public static class Global
    {
        /// <summary>
        /// DateTime in which the system operates in.
        /// </summary>
        static DateTime? _SystemDate = DateTime.Now;

        public static DateTime SystemDate {
            get {
                return _SystemDate.HasValue ? _SystemDate.Value : DateTime.Now;
            }
            set {
                if (value == DateTime.MinValue)
                    _SystemDate = null;
                else
                    _SystemDate = value;
            }
        }

        public const int MONTHS_IN_YEAR = 12;
        public const double DAYS_IN_YEAR = 365.25;

        public const int NormalRetirementAge = 67;
    }
}
