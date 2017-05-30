using Starcounter;
using Vadym.Database;

namespace Vadym.ViewModels
{
    /// <summary>
    /// Represent viewmodel for start page of application
    /// </summary>
    partial class StartPage : Json
    {
        public QueryResultRows<Corporation> Corporations => Db.SQL<Corporation>("SELECT c FROM Corporation c ORDER BY ObjectNo DESC");

        static StartPage()
        {
            DefaultTemplate.Corporations.ElementType.InstanceType = typeof(CorporationJson);
        }

        /// <summary>
        /// Handle creating of new Corporation
        /// </summary>
        /// <param name="action"></param>
        void Handle(Input.CreateNewCorporationTrigger action)
        {
            new Corporation
            {
                Name = this.NewCorporationName
            };

            Transaction.Commit();
        }
    }
}
