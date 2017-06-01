using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsFirst.Documents
{
    /// <summary>
    /// All of the elements that make up a letter
    /// </summary>
    abstract class Letter
    {
        /// <summary>
        /// Listing of all of the contacts that this
        /// letter is to be sent to
        /// </summary>
        public abstract IEnumerable<object> Addresses();

    }
}
