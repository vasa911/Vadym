using Starcounter;
using Vadym.Database;
using System.Linq;

namespace Vadym.ViewModels
{
    /// <summary>
    /// Represent viewmodel for Corporation entity
    /// </summary>
    partial class CorporationJson : Json
    {
        static CorporationJson()
        {
            //DefaultTemplate.FranchiseOffices.ElementType.InstanceType = typeof(FranchiseTableItemJson);
            DefaultTemplate.PartialPage.InstanceType = typeof(FranchisesTablePartialPageJson);
        }

        protected override void OnData()
        {
            base.OnData();

            RefreshPartial();
        }

        /// <summary>
        /// Handle Crea of new franchise office
        /// </summary>
        /// <param name="action"></param>
        void Handle(Input.CreateNewFranchiseOfficeTrigger action)
        {
            var address = new Address();

            new Franchise
            {
                Name = this.NewFranchiseOfficeName,
                Address = address,
                Owner = this.Data as Corporation,
            };

            Transaction.Commit();
            //refresh table in order to view new office
            RefreshPartial();
        }

        /// <summary>
        /// Refresh table that contains franchises list
        /// </summary>
        private void RefreshPartial()
        {
            PartialPage = new FranchisesTablePartialPageJson
            {
                FranchiseOffices = (this.Data as Corporation).FranchiseOffices
            };
        }

        /// <summary>
        /// Handle sort of franchise offices by number of sold homes
        /// </summary>
        /// <param name="action"></param>
        void Handle(Input.SortByNumberOfHomesTrigger action)
        {
            var result = Db.SQL<Franchise>("SELECT f FROM Franchise f WHERE f.Owner = ?  ORDER BY f.NumberOfHomesSold DESC",
                (this.Data as Corporation));

            PartialPage = new FranchisesTablePartialPageJson
            {
                FranchiseOffices = result
            };
        }


        /// <summary>
        /// Handle sort of franchise offices by total sales commission
        /// </summary>
        /// <param name="action"></param>
        void Handle(Input.SortByTotalCommissionTrigger action)
        {
            var result = Db.SQL<Franchise>("SELECT f FROM Franchise f WHERE f.Owner = ?  ORDER BY f.TotalCommission DESC",
                (this.Data as Corporation));

            PartialPage = new FranchisesTablePartialPageJson
            {
                FranchiseOffices = result
            };
        }

        /// <summary>
        /// Handle sort of franchise offices by average commission
        /// </summary>
        /// <param name="action"></param>
        void Handle(Input.SortByAverageCommissionTrigger action)
        {
            var result = Db.SQL<Franchise>("SELECT f FROM Franchise f WHERE f.Owner = ?  ORDER BY f.AvarageCommission DESC",
                (this.Data as Corporation));

            PartialPage = new FranchisesTablePartialPageJson
            {
                FranchiseOffices = result
            };
        }

        /// <summary>
        /// Handle sort of franchise offices by trend
        /// </summary>
        /// <param name="action"></param>
        void Handle(Input.SortByTrendTrigger action)
        {

            //TODO: change sql when trend will be calculated
            var result = Db.SQL<Franchise>("SELECT f FROM Franchise f WHERE f.Owner = ? ",
              (this.Data as Corporation));

            //var result = Db.SQL<Franchise>("SELECT f FROM Franchise f WHERE f.Owner = ?  ORDER BY f.Trend DESC",
            //    (this.Data as Corporation));

            PartialPage = new FranchisesTablePartialPageJson
            {
                FranchiseOffices = result
            };
        }

    }
}
