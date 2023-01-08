using DevExpress.XtraEditors;
using Khattana.Common;
using Model.Employees.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFom.Admin.Forms;
using WinFom.AppCompany.Forms;
using WinFom.AppDriver.Forms;
using WinFom.AppGoodCompany.Forms;
using WinFom.AppWeighBridge.Forms;
using WinFom.Common.Forms;
using WinFom.Common.Model;
using WinFom.Deal.Forms;
using WinFom.Employees.Forms;
using WinFom.Financials.Forms;
using WinFom.OilDealManaged.Forms;
using WinFom.OilDirtStuff.Forms;
using WinFom.ReadyStuff.Forms;
using WinFom.Reports.Forms;
using WinFom.Retail.Forms;
using WinFom.ScheduleSelector.Forms;

namespace WinFom.Client.Forms
{
    public partial class ClientForm : Form
    {
        public ClientForm()
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
            mainLabel.Text = string.Format("Welcome, dear user '{0}'", SingleTon.LoginForm.appUser.Name.ToUpper());
            
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
    }
}
