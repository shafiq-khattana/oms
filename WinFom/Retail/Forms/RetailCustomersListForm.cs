using Khattana.Common;
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
using Model.Deal.Model;
using Model.ReadyStuff.Model;
using WinFom.Common.Forms;
using Model.Admin.Model;
using WinFom.Common.Model;
using Model.Retail.Model;
using Model.Retail.ViewModel;
using Model.Financials.Model;
using WinFom.Retail.Reports.ViewModel;
using DevExpress.XtraReports.UI;
using WinFom.Retail.Reports.XtraReports;

namespace WinFom.Retail.Forms
{
    public partial class RetailCustomersListForm : Form
    {
        private List<Customer> customers = null;
        private List<CustomerTrialBalanceVM> customerListVM = null;
        private AppSettings appSett = null;
        public RetailCustomersListForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait1 = new WaitForm(LoadCustomers);
                wait1.ShowDialog();

                customerTrialBalanceVMBindingSource.List.Clear();
                foreach (var item in customerListVM)
                {
                    customerTrialBalanceVMBindingSource.List.Add(item);
                }
                tbReceivableAmount.Text = customerListVM.Sum(a => a.Balance).ToString("n1");
                tbTotalCustomers.Text = customerListVM.Count.ToString();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        public void LoadCustomers()
        {
            try
            {
                if (customers != null)
                {
                    customers.Clear();
                    customers = null;
                }
                if (customerListVM != null)
                {
                    customerListVM.Clear();
                    customerListVM = null;
                }
                customerListVM = new List<CustomerTrialBalanceVM>();
                using (Context db = new Context())
                {
                    appSett = db.AppSettings.FirstOrDefault();

                    customers = db.Customers.Where(a => a.Id != 1).OrderBy(a => a.Name).ToList();
                    foreach (var item in customers)
                    {
                        decimal balance = 0;
                        var acct = db.Accounts.Find(item.GeneralAccountId) as GeneralAccount;
                        var trans = db.AccountTransactions.Where(a => a.GeneralAccountId == acct.Id).OrderByDescending(a => a.Id)
                            .FirstOrDefault();
                        if (trans != null)
                        {
                            balance = trans.Balance;
                        }

                        CustomerTrialBalanceVM vm = new CustomerTrialBalanceVM
                        {
                            Account = acct.Title,
                            Balance = balance,
                            Cell = item.Contact,
                            Name = item.Name
                        };
                        customerListVM.Add(vm);
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);

            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerTrialBalanceReport rep = new CustomerTrialBalanceReport();
                List<CustomerTrialBalanceVM> list = customerTrialBalanceVMBindingSource.List.OfType<CustomerTrialBalanceVM>().ToList();
                if (list.Count == 0)
                {
                    throw new Exception("There is no customer's data to show in report");
                }

                List<CompVM> compDataSource = new List<CompVM>();
                CompVM compvm = new CompVM
                {
                    Address = appSett.Address,
                    LogoImg = appSett.Logo,
                    Name = appSett.Name,
                    Phone = appSett.PhoneNo
                };
                compDataSource.Add(compvm);

                List<CustomerTrialRVM> customerDataSource = new List<CustomerTrialRVM>();
                list.ForEach((a) =>
                {
                    customerDataSource.Add(new CustomerTrialRVM
                    {

                        Account = a.Account,
                        Balance = a.Balance,
                        Cell = a.Cell,
                        Name = a.Name
                    });
                });

                DetailReportBand hBand = rep.Bands["DetailReport"] as DetailReportBand;
                DetailReportBand cBand = rep.Bands["DetailReport2"] as DetailReportBand;

                hBand.DataSource = compDataSource;
                cBand.DataSource = customerDataSource;

                var dateParameter = rep.Parameters["RepDateTime"];
                dateParameter.Value = DateTime.Now.ToString();
                dateParameter.Visible = false;

                var totalBalance = rep.Parameters["TotalBalance"];
                totalBalance.Value = list.Sum(a => a.Balance).ToString("n1");
                totalBalance.Visible = false;
                var lengendsParam = rep.Parameters["Legends"];
                lengendsParam.Visible = false;
                lengendsParam.Value = Helper.Legends();
                rep.ShowPrintMarginsWarning = false;
                rep.ShowPrintStatusDialog = false;

                rep.ShowPreview();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
