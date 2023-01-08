using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Admin.Model
{
    public class UserOps
    {
        #region Retail
        public bool canViewSaleOrderList { get; set; }
        public bool canViewSaleOrder { get; set; }
        public bool canReprintSaleInvoiceCustomerCopy { get; set; }
        public bool canCancelSaleInvoice { get; set; }
        #endregion

        #region Employees
        public bool canViewAddEmployee { get; set; }
        public bool canViewEmployeeList { get; set; }
        public bool canViewEmployeePayRolls { get; set; }
        #endregion

        #region Deal
        public bool canViewAddNewDeal { get; set; }
        public bool canViewDealList { get; set; }
        public bool canViewScheduleAlarmsList { get; set; }
        public bool canViewDashboard { get; set; }
        public bool canViewMaalRegister { get; set; }
        #endregion

        #region Product
        public bool canViewAddNewProductCategory { get; set; }
        public bool canViewAddNewProduct { get; set; }
        public bool canViewCatgoryList { get; set; }
        public bool canViewProductList { get; set; }
        public bool canViewRetailBardana { get; set; }
        #endregion

        #region Misc
        public bool canViewAppSettings { get; set; }
        public bool canViewCompanyList { get; set; }
        public bool canViewBrokerList { get; set; }
        public bool canViewSelectorList { get; set; }
        public bool canViewGoodCompaniesList { get; set; }
        public bool canViewDriverList { get; set; }
        public bool canViewPackingStockCompanies { get; set; }
        public bool canViewPackingIssRecRecords { get; set; }
        public bool canViewPackingStockFactory { get; set; }
        public bool canViewWeighBridgeList { get; set; }
        public bool canViewVehicleList { get; set; }
        public bool canViewGeneralReceipts { get; set; }
        public bool canViewRefreshmentReceipts { get; set; }
        #endregion

        #region Wholesale Oil cake
        public bool canViewAddWholesaleDeal { get; set; }
        public bool canViewWholeSaleDealList { get; set; }
        public bool canViewWholeSaleScheduleAlarmList { get; set; }
        public bool canViewWholeSaleDashboard { get; set; }
        public bool canViewWholesaleReports { get; set; }
        #endregion


        #region Crude Oil Deal
        public bool canViewAddNewOilDeal { get; set; }
        public bool canViewOilDealList { get; set; }
        public bool canViewOilDealSchedules { get; set; }
        public bool canViewOilDealDashboard { get; set; }
        #endregion

        #region Oil Dirt Deal
        public bool canViewAddOilDirtDeal { get; set; }
        public bool canViewOilDirtDealList { get; set; }
        #endregion

        #region Financials
        public bool canViewFinancials { get; set; }
        public bool CanDoReverseFinancialEntries { get; set; }
        #endregion

        #region Inventory and repairing
        public bool canViewInventoryItems { get; set; }
        #endregion

        #region EFiles
        public bool canViewEFiles { get; set; }
        #endregion
        public string UserId { get; set; }

        #region People
        public bool canViewAppUsers { get; set; }
        public bool canAddAppUser { get; set; }
        public bool canViewCustomersList { get; set; }
        #endregion

        #region "Reports"
        public bool canViewRetailTrialBalanceReport { get; set; }
        public bool canViewOilCakePurchasesReport { get; set; }
        public bool canViewCrudeOilSaleReports { get; set; }
        public bool canViewOilDirtSaleReports { get; set; }
        #endregion
    }
}
