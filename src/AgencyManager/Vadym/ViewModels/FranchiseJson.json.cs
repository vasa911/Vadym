using Starcounter;

namespace Vadym.ViewModels
{
    /// <summary>
    /// Represent viewmodel for Franchise entity
    /// </summary>
    partial class FranchiseJson : Json
    {

        static FranchiseJson()
        {
            DefaultTemplate.Address.InstanceType = typeof(AddressJson);
            DefaultTemplate.NewTransaction.InstanceType = typeof(TransactionJson);
        }

        /// <summary>
        /// Handle saving of franchise settings
        /// </summary>
        /// <param name="action"></param>
        void Handle(Input.SaveFranchiseSettingsTrigger action)
        {        
            Transaction.Commit();
        }
    }
}
