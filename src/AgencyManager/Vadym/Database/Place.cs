using Starcounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vadym.Database
{
    /// <summary>
    /// Abstact class that contains Address entity
    /// </summary>
    [Database]
    public abstract class Place
    {
        public Address Address { get; set; }
    }
}
