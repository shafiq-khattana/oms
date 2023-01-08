using Bunifu.Framework.UI;
using Khattana.Common;
using Khattana.Secrecy;
using Model.Admin.Model;
using Model.Deal.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFom.Admin.Database;
using WinFom.Common.Forms;
using WinFom.Common.Model;

namespace WinFom.Admin.Forms
{
    public partial class UserOpsForm : Form
    {
        UserOps userOps = null;
        public string NewOptions { get; set; }
        public bool IsDone { get; set; }
        private AppUser appUser = SingleTon.LoginForm.appUser;
        public UserOpsForm(UserOps ops)
        {
            InitializeComponent();
            userOps = ops;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UserOpsForm_Load(object sender, EventArgs e)
        {
            label1.Text = string.Format("User options for user ({0})", userOps.UserId);
            IsDone = false;
            #region Retail
            bswcanCancelSaleInvoice.Value = userOps.canCancelSaleInvoice;
            bswcanReprintSaleInvoiceCustomerCopy.Value = userOps.canReprintSaleInvoiceCustomerCopy;
            bswCanViewSaleOrder.Value = userOps.canViewSaleOrder;
            bswCanViewSaleOrderList.Value = userOps.canViewSaleOrderList;
            #endregion


            #region Employees
            bswCanAddNewEmployee.Value = userOps.canViewAddEmployee;
            bswCanViewEmployeeList.Value = userOps.canViewEmployeeList;
            bswCanViewEmployeePayRolls.Value = userOps.canViewEmployeePayRolls;
            #endregion

            #region Deal
            bswDealAddNewDeal.Value = userOps.canViewAddNewDeal;
            bswDealViewDashboard.Value = userOps.canViewDashboard;
            bswDealViewDealList.Value = userOps.canViewDealList;
            bswDealViewMaalRegister.Value = userOps.canViewMaalRegister;
            bswDealViewScheduleAlarmList.Value = userOps.canViewScheduleAlarmsList;
            #endregion

            #region Product
            bswProductCanAddNewCategory.Value = userOps.canViewAddNewProductCategory;
            bswProductCanAddNewProduct.Value = userOps.canViewAddNewProduct;
            bswProductCanViewBardanaDetails.Value = userOps.canViewRetailBardana;
            bswProductCanViewCatsList.Value = userOps.canViewCatgoryList;
            bswProductCanViewProductList.Value = userOps.canViewProductList;
            #endregion

            #region Misc
            bswMiscCanViewAppSettings.Value = userOps.canViewAppSettings;
            bswMiscCanViewBrokerList.Value = userOps.canViewBrokerList;
            bswMiscCanViewCompaniesList.Value = userOps.canViewCompanyList;
            bswMiscCanViewDriversList.Value = userOps.canViewDriverList;
            bswMiscCanViewGoodsCompanies.Value = userOps.canViewGoodCompaniesList;
            bswMiscCanViewPackingIssueReceiveRecords.Value = userOps.canViewPackingIssRecRecords;
            bswMiscCanViewPackingStockCompanies.Value = userOps.canViewPackingStockCompanies;
            bswMiscCanViewPackingStockFactory.Value = userOps.canViewPackingStockFactory;
            bswMiscCanViewSelectorsList.Value = userOps.canViewSelectorList;
            bswMiscCanViewVehicleList.Value = userOps.canViewVehicleList;
            bswMiscCanViewWeighBridgeList.Value = userOps.canViewWeighBridgeList;
            bswMiscCanViewGeneralReceipts.Value = userOps.canViewGeneralReceipts;
            bswMiscCanViewRefreshmentReceipts.Value = userOps.canViewRefreshmentReceipts;
            #endregion

            #region Oil cake wholesale
            bswWSCanAddWholesaleDeal.Value = userOps.canViewAddWholesaleDeal;
            bswWSCanViewAlarmList.Value = userOps.canViewWholeSaleScheduleAlarmList;
            bswWSCanViewDashboard.Value = userOps.canViewWholeSaleDashboard;
            bswWSCanViewWholesaleDeals.Value = userOps.canViewWholeSaleDealList;
            bswWSViewReports.Value = userOps.canViewWholesaleReports;
            #endregion

            #region Crude Oil
            bswOilCanAddOilDeal.Value = userOps.canViewAddNewOilDeal;
            bswOilCanViewDashboard.Value = userOps.canViewOilDealDashboard;
            bswOilCanViewDealList.Value = userOps.canViewOilDealList;
            bswOilCanViewDealSchedules.Value = userOps.canViewOilDealSchedules;
            bswOilCanViewReports.Value = false;
            #endregion

            #region Oil dirt
            bswODCanAddOilDirtDeal.Value = userOps.canViewAddOilDirtDeal;
            bswODCanViewOilDirtDeals.Value = userOps.canViewOilDirtDealList;
            #endregion

            #region Financials and inventory
            bswFICanViewEFiles.Value = userOps.canViewEFiles;
            bswFICanViewFinancials.Value = userOps.canViewFinancials;
            bswFICanViewInventory.Value = userOps.canViewInventoryItems;
            bswCanReverseFinancialEntries.Value = userOps.CanDoReverseFinancialEntries;
            #endregion

            #region Reports
            bswRetailTrialBalanceReport.Value = userOps.canViewRetailTrialBalanceReport;
            bswOilCakePurchaseReports.Value = userOps.canViewOilCakePurchasesReport;
            bswCrudeOilSaleReports.Value = userOps.canViewCrudeOilSaleReports;
            bswOilDirtSaleReports.Value = userOps.canViewOilDirtSaleReports;
            #endregion
            if (appUser.Id != "admin1" && (userOps.UserId == "admin1" || userOps.UserId == "admin24"))
            {
                btnUpdate.Enabled = false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Helper.ConfirmAdminPassword())
                {
                    return;
                }

                DialogResult res = Gujjar.ConfirmYesNo("Please confirm..!!");
                if (res == DialogResult.No)
                    return;

                using (Context db = new Context())
                {
                    
                    #region Retail
                    userOps.canCancelSaleInvoice = bswcanCancelSaleInvoice.Value;
                    userOps.canReprintSaleInvoiceCustomerCopy = bswcanReprintSaleInvoiceCustomerCopy.Value;
                    userOps.canViewSaleOrder = bswCanViewSaleOrder.Value;
                    userOps.canViewSaleOrderList = bswCanViewSaleOrderList.Value;
                    #endregion

                    #region Employees
                    userOps.canViewAddEmployee = bswCanAddNewEmployee.Value;
                    userOps.canViewEmployeeList = bswCanViewEmployeeList.Value;
                    userOps.canViewEmployeePayRolls = bswCanViewEmployeePayRolls.Value;
                    #endregion

                    #region Deal
                    userOps.canViewAddNewDeal = bswDealAddNewDeal.Value;
                    userOps.canViewDashboard = bswDealViewDashboard.Value;
                    userOps.canViewDealList = bswDealViewDealList.Value;
                    userOps.canViewMaalRegister = bswDealViewMaalRegister.Value;
                    userOps.canViewScheduleAlarmsList = bswDealViewScheduleAlarmList.Value;
                    #endregion

                    #region Product
                    userOps.canViewAddNewProductCategory = bswProductCanAddNewCategory.Value;
                    userOps.canViewAddNewProduct = bswProductCanAddNewProduct.Value;
                    userOps.canViewRetailBardana = bswProductCanViewBardanaDetails.Value;
                    userOps.canViewCatgoryList = bswProductCanViewCatsList.Value;
                    userOps.canViewProductList = bswProductCanViewProductList.Value;
                    #endregion

                    #region Misc
                    userOps.canViewAppSettings = bswMiscCanViewAppSettings.Value;
                    userOps.canViewBrokerList = bswMiscCanViewBrokerList.Value;
                    userOps.canViewCompanyList = bswMiscCanViewCompaniesList.Value;
                    userOps.canViewDriverList = bswMiscCanViewDriversList.Value;
                    userOps.canViewGoodCompaniesList = bswMiscCanViewGoodsCompanies.Value;
                    userOps.canViewPackingIssRecRecords = bswMiscCanViewPackingIssueReceiveRecords.Value;
                    userOps.canViewPackingStockCompanies = bswMiscCanViewPackingStockCompanies.Value;
                    userOps.canViewPackingStockFactory = bswMiscCanViewPackingStockFactory.Value;
                    userOps.canViewSelectorList = bswMiscCanViewSelectorsList.Value;
                    userOps.canViewVehicleList = bswMiscCanViewVehicleList.Value;
                    userOps.canViewWeighBridgeList = bswMiscCanViewWeighBridgeList.Value;
                    userOps.canViewGeneralReceipts = bswMiscCanViewGeneralReceipts.Value;
                    userOps.canViewRefreshmentReceipts = bswMiscCanViewRefreshmentReceipts.Value;
                    #endregion

                    #region Oil cake wholesale
                    userOps.canViewAddWholesaleDeal = bswWSCanAddWholesaleDeal.Value;
                    userOps.canViewWholeSaleScheduleAlarmList = bswWSCanViewAlarmList.Value;
                    userOps.canViewWholeSaleDashboard = bswWSCanViewDashboard.Value;
                    userOps.canViewWholeSaleDealList = bswWSCanViewWholesaleDeals.Value;
                    userOps.canViewWholesaleReports = bswWSViewReports.Value;
                    #endregion

                    #region Crude Oil
                    userOps.canViewAddNewOilDeal = bswOilCanAddOilDeal.Value;
                    userOps.canViewOilDealDashboard = bswOilCanViewDashboard.Value;
                    userOps.canViewOilDealList = bswOilCanViewDealList.Value;
                    userOps.canViewOilDealSchedules = bswOilCanViewDealSchedules.Value;
                    bool res1 = bswOilCanViewReports.Value;
                    #endregion

                    #region Oil dirt
                    userOps.canViewAddOilDirtDeal = bswODCanAddOilDirtDeal.Value;
                    userOps.canViewOilDirtDealList = bswODCanViewOilDirtDeals.Value;
                    #endregion

                    #region Financials and inventory
                    userOps.canViewEFiles = bswFICanViewEFiles.Value;
                    userOps.canViewFinancials = bswFICanViewFinancials.Value;
                    userOps.canViewInventoryItems = bswFICanViewInventory.Value;
                    userOps.CanDoReverseFinancialEntries = bswCanReverseFinancialEntries.Value;
                    #endregion

                    #region Reports
                    userOps.canViewRetailTrialBalanceReport = bswRetailTrialBalanceReport.Value;
                    userOps.canViewOilCakePurchasesReport = bswOilCakePurchaseReports.Value;
                    userOps.canViewCrudeOilSaleReports = bswCrudeOilSaleReports.Value;
                    userOps.canViewOilDirtSaleReports = bswOilDirtSaleReports.Value;
                    #endregion


                    string ops = Gujjar.ToXML<UserOps>(userOps);

                    var user = db.AppUsers.Find(userOps.UserId);
                    user.Options = MsrCipher.Encrypt(ops);
                    NewOptions = user.Options;

                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    IsDone = true;
                    Gujjar.InfoMsg("Options are updated");
                    Close();
                }

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SwitchONOFF(GroupBox gb, bool state)
        {
            foreach (var item in gb.Controls)
            {
                if(item is BunifuSwitch)
                {
                    BunifuSwitch sw = item as BunifuSwitch;
                    sw.Value = state;
                }
            }
        }

        private void btnRetailOptionsON_Click(object sender, EventArgs e)
        {
            SwitchONOFF(gbRetailOptions, true);
        }

        private void btnRetailOptionsOFF_Click(object sender, EventArgs e)
        {
            SwitchONOFF(gbRetailOptions, false);
        }

        private void btnEmpON_Click(object sender, EventArgs e)
        {
            SwitchONOFF(gbEmployees, true);
        }

        private void btnEmpOFF_Click(object sender, EventArgs e)
        {
            SwitchONOFF(gbEmployees, false);
        }

        private void btnDealON_Click(object sender, EventArgs e)
        {
            SwitchONOFF(gbDeal, true);
        }

        private void btnDealOFF_Click(object sender, EventArgs e)
        {
            SwitchONOFF(gbDeal, false);
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void btnMiscON_Click(object sender, EventArgs e)
        {
            SwitchONOFF(gbMisc, true);
        }

        private void btnMiscOFF_Click(object sender, EventArgs e)
        {
            SwitchONOFF(gbMisc, false);
        }

        private void btnProductON_Click(object sender, EventArgs e)
        {
            SwitchONOFF(gbProducts, true);
        }

        private void btnProductOFF_Click(object sender, EventArgs e)
        {
            SwitchONOFF(gbProducts, false);
        }

        private void btnWSON_Click(object sender, EventArgs e)
        {
            SwitchONOFF(gbWholeSale, true);
        }

        private void btnWSOFF_Click(object sender, EventArgs e)
        {
            SwitchONOFF(gbWholeSale, false);
        }

        private void btnONN_Click(object sender, EventArgs e)
        {
            AllSwitch(true);
        }

        private void AllSwitch(bool v)
        {
            foreach (var gb in panel2.Controls)
            {
                if(gb is GroupBox)
                {
                    GroupBox gb2 = gb as GroupBox;
                    SwitchONOFF(gb2, v);
                }
            }
        }

        private void btnOFF_Click(object sender, EventArgs e)
        {
            AllSwitch(false);
        }

        private void btnCrudeOilON_Click(object sender, EventArgs e)
        {
            SwitchONOFF(gbCrudeOil, true);
        }

        private void btnCrudeOilOFF_Click(object sender, EventArgs e)
        {
            SwitchONOFF(gbCrudeOil, false);
        }

        private void btnODON_Click(object sender, EventArgs e)
        {
            SwitchONOFF(gbOilDirt, true);
        }

        private void btnODOFF_Click(object sender, EventArgs e)
        {
            SwitchONOFF(gbOilDirt, false);
        }

        private void btnFION_Click(object sender, EventArgs e)
        {
            SwitchONOFF(gbFinancialAndInventory, true);
        }

        private void btnFIOFF_Click(object sender, EventArgs e)
        {
            SwitchONOFF(gbFinancialAndInventory, false);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SwitchONOFF(gbReports, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SwitchONOFF(gbReports, false);
        }
    }
}
