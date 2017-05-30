using Starcounter;

namespace Vadym.ViewModels
{
    /// <summary>
    /// Represent viewmodel for address entity
    /// </summary>
    partial class AddressJson : Json
    {
        public string FullAddress => Street + " " + Number + ", " + ZipCode + " " + City + ", " + Country;
    }
}
