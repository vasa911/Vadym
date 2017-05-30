using Starcounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vadym.Database
{
    /// <summary>
    /// Represent transaction of sell home
    /// </summary>
    [Database]
    public class Transaction
    {
        /// <summary>
        /// Transaction date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Home sales price
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// Commission of real estate agency
        /// </summary>
        public decimal Commission { get; set; }

        /// <summary>
        /// Connected home entity
        /// </summary>
        public Home Home { get; set; }

        /// <summary>
        /// Owner of transaction
        /// </summary>
        public Franchise Owner { get; set; }
    }
}
