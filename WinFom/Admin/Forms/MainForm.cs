using DevExpress.XtraEditors;
using Khattana.Common;
using Model.Admin.Model;
using Model.Employees.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFom.AppCompany.Forms;
using WinFom.AppDriver.Forms;
using WinFom.AppGoodCompany.Forms;
using WinFom.AppWeighBridge.Forms;
using WinFom.Common.Forms;
using WinFom.Common.Model;
using WinFom.Deal.Forms;
using WinFom.Employees.Forms;
using WinFom.EntertainmentUI.Forms;
using WinFom.Financials.Forms;
using WinFom.GeneralUI.Forms;
using WinFom.OilDealManaged.Forms;
using WinFom.OilDirtStuff.Forms;
using WinFom.ReadyStuff.Forms;
using WinFom.RepairUI.Forms;
using WinFom.Reports.Forms;
using WinFom.Retail.Forms;
using WinFom.ScheduleSelector.Forms;

namespace WinFom.Admin.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetPositions();
            string version = ConfigurationManager.AppSettings["version"];
            mainLabel.Text = string.Format("Welcome, dear user '{0}' ({1})", SingleTon.LoginForm.appUser.Name.ToUpper(), version);
            Helper.IsOkApplied();
            DoUserOps();
        }

        private void DoUserOps()
        {
            try
            {
                UserOps ops = SingleTon.LoginForm.UserOptions;
                btnSaleOrderList.Visible = ops.canViewSaleOrderList;
                btnSaleOrder.Visible = ops.canViewSaleOrder;
                btnAppUsers.Visible = ops.canViewAppUsers;
                btnCustomers.Visible = ops.canViewCustomersList;

                btnAddEmployee.Visible = ops.canViewAddEmployee;
                btnEmployeeList.Visible = ops.canViewEmployeeList;
                btnPayrolls.Visible = ops.canViewEmployeePayRolls;


                btnAddNewDeal.Visible = ops.canViewAddNewDeal;
                btnDealList.Visible = ops.canViewDealList;
                btnAlarmList.Visible = ops.canViewScheduleAlarmsList;
                btnDashBoard.Visible = ops.canViewDashboard;
                btnMaalRegister.Visible = ops.canViewMaalRegister;

                btnAddCategory.Visible = ops.canViewAddNewProductCategory;
                btnAddNewProduct.Visible = ops.canViewAddNewProduct;
                btnCategoriesList.Visible = ops.canViewCatgoryList;
                btnProductList.Visible = ops.canViewProductList;
                btnRetailbardana.Visible = ops.canViewRetailBardana;

                #region "Misc"
                btnAppSettings.Visible = ops.canViewAppSettings;
                btnCompanies.Visible = ops.canViewCompanyList;
                btnBrokerList.Visible = ops.canViewBrokerList;
                btnSelectors2.Visible = ops.canViewSelectorList;
                btnGoodCompanies.Visible = ops.canViewGoodCompaniesList;
                btnDriversList.Visible = ops.canViewDriverList;
                btnPackingStockList.Visible = ops.canViewPackingStockCompanies;
                btnPackingIssueReceiveRecords.Visible = ops.canViewPackingIssRecRecords;
                btnPackingStockFactory.Visible = ops.canViewPackingStockFactory;
                btnWeighBridgeList.Visible = ops.canViewWeighBridgeList;
                btnVehicles.Visible = ops.canViewVehicleList;
                btnGeneralReceipts.Visible = ops.canViewGeneralReceipts;
                btnRefreshmentList.Visible = ops.canViewRefreshmentReceipts;
                #endregion

                #region Wholesale Oil cake
                btnAddWholeSaleDeal.Visible = ops.canViewAddWholesaleDeal;
                btnReadyDealList2.Visible = ops.canViewWholeSaleDealList;
                btnReadyScheduleAlarmsList.Visible = ops.canViewWholeSaleScheduleAlarmList;
                btnReadyDashboard.Visible = ops.canViewWholeSaleDashboard;
                btnReadyReports.Visible = ops.canViewWholesaleReports;
                #endregion

                #region Crude Oil
                btnAddOilDeal.Visible = ops.canViewAddNewOilDeal;
                btnOilDealList.Visible = ops.canViewOilDealList;
                btnCrudeOilScheduleList.Visible = ops.canViewOilDealSchedules;
                btnCrudeOilDashboard.Visible = ops.canViewOilDealDashboard;
                #endregion

                #region Oil Dirt
                btnAddOilDirtDeal.Visible = ops.canViewAddOilDirtDeal;
                btnOilDirtDealList.Visible = ops.canViewOilDirtDealList;
                #endregion

                #region Financial
                btnFinancials.Visible = ops.canViewFinancials;
                #endregion
                #region EFile
                btnEFiles.Visible = ops.canViewEFiles;
                #endregion
                #region Inventory and repairing
                btnItemsSection.Visible = ops.canViewInventoryItems;
                #endregion

                #region Reports
                btnCustTrialBalance.Visible = ops.canViewRetailTrialBalanceReport;
                btnDealReport.Visible = ops.canViewOilCakePurchasesReport;
                btnCrudeOilReports.Visible = ops.canViewCrudeOilSaleReports;
                btnOilDirtReports.Visible = ops.canViewOilDirtSaleReports;
                #endregion
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void SetPositions()
        {
            int width = Screen.PrimaryScreen.Bounds.Width;
            int height = Screen.PrimaryScreen.Bounds.Height;

            int panelWidth = mainPanel.Width;
            int panelHeight = mainPanel.Height;

            int diffWidth = width - panelWidth;
            int diffHeight = height - panelHeight;

            int x = diffWidth / 2;
            int y = diffHeight / 2;

            mainPanel.Location = new Point(x, y);
        }
        private void tileItem1_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {

        }

        private void tileItem3_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {

        }

        private void tileItem25_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            try
            {
                AddOilDirtDealForm form = new AddOilDirtDealForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tileItem26_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {

        }
        private void Delay()
        {
            Thread.Sleep(1000);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void btnAddNewDeal_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            try
            {
                if(!Helper.ConfirmUserPassword())
                {
                    return;
                }
                Thread th = new Thread(() =>
                {
                    //Application.Run(new AddDealForm());
                    AddDealForm form = new AddDealForm();
                    form.ShowDialog();
                });
                th.Start();

                //AddDealForm form = new AddDealForm();
                //   form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            var obj = SingleTon.LoginForm;
            if (obj.InvokeRequired)
            {
                obj.Invoke(new Action(() =>
                {
                    SingleTon.LoginForm.Close();
                }));
            }
            else
            {
                SingleTon.LoginForm.Close();
            }
        }

        private void btnDealList_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                if (!Helper.ConfirmUserPassword())
                {
                    return;
                }


                DealListForm form = new DealListForm();
                form.ShowDialog();

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAlarmList_ItemClick(object sender, TileItemEventArgs e)
        {

            try
            {
                ScheduleAlarmsList list = new ScheduleAlarmsList();
                list.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnAppSettings_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                AppSettingsForm form = new AppSettingsForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnCompanies_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                CompaniesListForm form = new CompaniesListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnBrokerList_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                BrokerListForm form = new BrokerListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnSelectors2_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                SelectorsListForm form = new SelectorsListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tileItem17_ItemClick(object sender, TileItemEventArgs e)
        {

        }

        private void btnGoodCompanies_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                GoodCompaniesList form = new GoodCompaniesList();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnDriversList_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                DriversListForm form = new DriversListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnPackingStockList_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                PackingStockForm form = new PackingStockForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnPackingIssueReceiveRecords_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                PackingIssueReceiveRecordForm form = new PackingIssueReceiveRecordForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnPackingStockFactory_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                PackingStockFactoryForm form = new PackingStockFactoryForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnWeighBridgeList_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                WeighBridgeListForm form = new WeighBridgeListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tileControl3_Click(object sender, EventArgs e)
        {

        }

        private void btnMaalRegister_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                MaalRegisterForm form = new MaalRegisterForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnVehicles_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                VehicleListForm form = new VehicleListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnDashBoard_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                DashBoardForm form = new DashBoardForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddReadyDeal_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                AddReadyDealForm form = new AddReadyDealForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnReadyDealList2_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                ReadyDealListForm from = new ReadyDealListForm();
                from.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tileItem24_ItemClick(object sender, TileItemEventArgs e)
        {
            TestForm form = new TestForm();
            form.ShowDialog();
        }

        private void btnReadyScheduleAlarmsList_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                ReadySchedulesAlarmList list = new ReadySchedulesAlarmList();
                list.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnReadyDashboard_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                ReadyDashBoardForm form = new ReadyDashBoardForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void addOilDealBtn_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                AddOilDealForm form = new AddOilDealForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnOilDealList_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                OilDealListForm form = new OilDealListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnOilDirtDealList_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                OilDirtDealList form = new OilDirtDealList();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tileControl1_Click(object sender, EventArgs e)
        {

        }

        private void btnProductList_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                ProductListForm form = new ProductListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddNewProduct_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                AddProductForm form = new AddProductForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }

        }

        private void btnAddCategory_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                AddProductCategoryForm form = new AddProductCategoryForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnSaleOrder_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                SaleOrderForm form = new SaleOrderForm();
                form.Show();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnSaleOrderList_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                SaleOrderListForm form = new SaleOrderListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {

        }

        private void btnFinancials_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                FinancialForm form = new FinancialForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddEmployee_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                AddEmployeeForm form = new AddEmployeeForm(EmployeeType.FactoryWorker);
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnEmployeeList_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                EmployeeListForm form = new EmployeeListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnRetailbardana_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                AURetailBardanaItem form = new AURetailBardanaItem();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnPayrolls_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                EmpPayRollsForm form = new EmpPayRollsForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnRepairItems_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                RepairMainForm form = new RepairMainForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnEFiles_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                EFileListForm form = new EFileListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnReadyReports_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                WholesaleOilCakeReportForm form = new WholesaleOilCakeReportForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAboutUs_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                AboutUsForm form = new AboutUsForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAppUsers_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                AppUsersForm form = new AppUsersForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnCustomers_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                CustomersListForm form = new CustomersListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tileItem1_ItemClick_1(object sender, TileItemEventArgs e)
        {
            AddGeneralReceiptForm form = new AddGeneralReceiptForm();
            form.ShowDialog();
        }

        private void btnGeneralReceipts_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                GeneralRecepitListForm form = new GeneralRecepitListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnRefreshmentList_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                EPurchaseListForm form = new EPurchaseListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
           
        }

        private void btnCustTrialBalance_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                RetailCustomersListForm form = new RetailCustomersListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tileItem1_ItemClick_2(object sender, TileItemEventArgs e)
        {
            try
            {
                CrudeOilReportsForm form = new CrudeOilReportsForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnOilDirtReports_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                OilDirtReportsForm form = new OilDirtReportsForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnDealReport_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                DealReportsForm form = new DealReportsForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
