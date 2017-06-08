using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendsFirst.Web.Models.Projections
{
    public class Projection
    {
        public ProjectionAlteration[] Alterations { get; set; }
        public Projection()
        {
            this.Alterations = new ProjectionAlteration[] { };
        }

        public FriendsFirst.Contracts.StandardSavingsContract Contract { get; set; }
        public IEnumerable<FriendsFirst.Projections.ProjectedContract<FriendsFirst.Contracts.StandardSavingsContract>> Projections { get; set; }

    }
}