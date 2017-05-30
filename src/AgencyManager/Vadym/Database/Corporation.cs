using Starcounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vadym.Database
{
    /// <summary>
    /// Represent real estate agency
    /// </summary>
    [Database]
    public class Corporation
    {
        /// <summary>
        /// Corporation name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Connected franchise offices entities
        /// </summary>
        public QueryResultRows<Franchise> FranchiseOffices => Db.SQL<Franchise>("Select f FROM Franchise f where f.Owner = ?", this);
    }
}
