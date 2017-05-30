using Starcounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vadym.Database
{
    /// <summary>
    /// Represent franchise office of Corporation
    /// </summary>
    [Database]
   public class Franchise : Place
    {
        /// <summary>
        /// Office name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Connected Corporation
        /// </summary>
        public Corporation Owner { get; set; }

        /// <summary>
        /// Transaction that are done by office
        /// </summary>
        public QueryResultRows<Transaction> ListTransactions => Db.SQL<Transaction>("SELECT t FROM Transaction t WHERE t.Owner = ?", this);

        /// <summary>
        /// Url for specific franchise
        /// </summary>
        public string Url => "/Vadym/franchise/" + this.GetObjectID();

        /// <summary>
        /// Number of sold homes
        /// </summary>
        public long NumberOfHomesSold => Db.SQL<long>("SELECT COUNT(t) FROM Transaction t WHERE t.Owner = ?", this).First;

        /// <summary>
        /// Sum of all commission
        /// </summary>
        public decimal TotalCommission => Db.SQL<decimal>("SELECT SUM(t.Commission) FROM Transaction t WHERE t.Owner = ?", this).First;

        /// <summary>
        /// Avarage commission
        /// </summary>
        public decimal AvarageCommission => Db.SQL<decimal>("SELECT AVG(t.Commission) FROM Transaction t WHERE t.Owner = ?", this).First;
    }
}
