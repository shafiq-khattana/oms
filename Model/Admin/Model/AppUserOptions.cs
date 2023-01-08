using Model.Deal.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Admin.Model
{
    public class AppUserOptions
    {
        [Key, ForeignKey("AppUser")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        #region Users, Employees, Delivery boys
        public bool CanAddNewCustomer { get; set; }
        public bool CanViewTopCustomers { get; set; }
        public bool CanViewCustomerList { get; set; }
        public bool CanAddNewAppUser { get; set; }
        public bool CanViewAppUsers { get; set; }
        public bool CanAddNewDeliveryBoy { get; set; }
        public bool CanViewDeliveryBoyList { get; set; }
        public bool CanViewDeliveryBoyPerformance { get; set; }
        public bool CanAddNewEmployee { get; set; }
        public bool CanViewEmployeeList { get; set; }
        public bool CanUseAttendanceInForm { get; set; }
        public bool CanUseAttendanceOutForm { get; set; }
        public bool CanViewAttendanceList { get; set; }
        #endregion

        #region Products
        public bool CanAddNewProduct { get; set; }
        public bool CanViewProductList { get; set; }
        public bool CanAddNewProductCategory { get; set; }
        public bool CanViewProductCategories { get; set; }
        public bool CanUseShopInfoForm { get; set; }
        public bool CanWastageItem { get; set; }
        public bool CanViewWastageItems { get; set; }
        #endregion

        #region Orders
        public bool CanPlaceNewOrder { get; set; }
        public bool CanViewOrdersList { get; set; }
        public bool CanReprintAnOrder { get; set; }
        public bool CanCanelAnOrder { get; set; }
        public bool CanViewHomeDeliveryOrders { get; set; }
        public bool CanUpdateHomeDeliveryOrder { get; set; }
        public bool CanAddNewDeal { get; set; }
        public bool CanViewDealList { get; set; }
        public bool CanAddNewServiceTable { get; set; }
        public bool CanViewTableServiceList { get; set; }
        public bool CanManageTableServices { get; set; }
        public bool CanViewCancelOrders { get; set; }
        #endregion

        #region Charts, Reports
        public bool CanViewProfitChart { get; set; }
        public bool CanViewStockDetails { get; set; }
        public bool CanAddDailyExpense { get; set; }
        public bool CanViewExpenses { get; set; }
        public bool CanViewDailyOrdersChart { get; set; }
        public bool CanViewDailyReports { get; set; }
        public bool CanViewDailySalesChart { get; set; }
        public bool CanViewDailyConsumption { get; set; }
        #endregion

        #region Sms
        public bool CanViewSmsSubscribers { get; set; }
        public bool CanAddNewSmsSubscriber { get; set; }
        public bool CanViewSmsSettingsForm { get; set; }
        public bool CanSendSmsToCustomers { get; set; }
        #endregion

        #region Raw Material
        public bool CanAddNewRawCategory { get; set; }
        public bool CanViewRawCategories { get; set; }
        public bool CanAddRawUnit { get; set; }
        public bool CanViewRawUnits { get; set; }
        public bool CanAddNewSupplier { get; set; }
        public bool CanViewSuppliers { get; set; }
        public bool CanAddNewRawItem { get; set; }
        public bool CanViewRawItems { get; set; }
        public bool CanAddItemInStock { get; set; }
        public bool CanViewItemAddedStockHistory { get; set; }
        public bool CanConsumeRawItem { get; set; }
        public bool CanViewConsumptionRawItemList { get; set; }
        public bool CanIssueRawItem { get; set; }
        public bool CanViewIssueRawItemRecords { get; set; }
        public bool CanViewPaymentRecords { get; set; }
        public bool CanAddPackings { get; set; }
        public bool CanViewPackings { get; set; }
        #endregion

        #region Assets
        public bool CanAddAssetCompany { get; set; }
        public bool CanViewAssetCompanies { get; set; }
        public bool CanAddAssetCategory { get; set; }
        public bool CanViewAssetCategories { get; set; }
        public bool CanAddAssetItem { get; set; }
        public bool CanViewAssetItem { get; set; }
        public bool CanViewAssetItemReport { get; set; }
        public bool CanWastageAssetItem { get; set; }

        #endregion


        #region Miscellneous

        //Can return
        public bool Misc1 { get; set; }
        public bool Misc2 { get; set; }
        public bool Misc3 { get; set; }
        public bool Misc4 { get; set; }
        public bool Misc5 { get; set; }
        public bool Misc6 { get; set; }
        public bool Misc7 { get; set; }
        public bool Misc8 { get; set; }
        public bool Misc9 { get; set; }
        public bool Misc10 { get; set; }
        public bool Misc11 { get; set; }
        #endregion
    }
}
