using Starcounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vadym.Database
{
    /// <summary>
    /// Represent address information
    /// </summary>
    [Database]
    public class Address
    {
        public string Street { get; set; }
        public string Number { get; set; }      
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string FullAddress => Street + " " + Number + ", " + ZipCode + " " + City + ", " + Country;
    }
}
