using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendsFirst.Web.Models.Projections
{
    /// <summary>
    /// Selection of options that can be applied to a projection
    /// </summary>
    public class ProjectionAlteration
    {
        /// <summary>
        /// Property that is to be altered
        /// </summary>
        public System.Reflection.PropertyInfo Property { get; set; }

        /// <summary>
        /// New value to set the property to
        /// </summary>
        public object Value { get; set; }
    }

    
}