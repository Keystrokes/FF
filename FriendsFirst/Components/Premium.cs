using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Components
{
    namespace Premiums
    {
        class Reguler : Premium
        {
            public override PremiumStatues Status
            {
                get
                {
                    throw new NotImplementedException();
                }
            }
        }

        public enum PremiumStatues
        {
            Due,
            Paid,
            OverDue,
            Wavied
        }

        /// <summary>
        /// A Payment which is paid to substation a product in a contact.
        /// </summary>
        abstract class Premium
        {
            /// <summary>
            /// States if the Premium was paid, overdue, etc
            /// </summary>
            public abstract PremiumStatues Status { get; }
            /// <summary>
            /// Premium amount
            /// </summary>
            public double Amount { get; set; }
            /// <summary>
            /// Date in which premium is to be paid
            /// </summary>
            public DateTime Date { get; set; }
            /// <summary>
            /// Calculates the actual cost of the premium on the contact.
            /// This would include over due fees or Waved etc,
            /// </summary>
            /// <returns></returns>
            public virtual double Cost()
            {
                if (this.Status != PremiumStatues.Due)
                    return 0;

                return this.Amount;
            }
        }
    }
}
