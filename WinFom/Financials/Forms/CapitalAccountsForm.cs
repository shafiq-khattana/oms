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
using Model.Financials.Model;
using Model.Admin.Model;
using WinFom.Common.Model;
using WinFom.Common.Forms;
using Model.Financials.ViewModel;

namespace WinFom.Financials.Forms
{
    public partial class CapitalAccountsForm : Form
    {
        private string btndgvdetails = "btndgvdetails";
        private decimal totalBalance = 0;
        private List<GeneralAccount> capitalAccounts = null;
        private AppSettings AppSett = Helper.AppSet;
        public CapitalAccountsForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadCapitalAccounts()
        {
            try
            {
                totalBalance = 0;
                if(capitalAccounts != null)
                {
                    capitalAccounts.Clear();
                    capitalAccounts = null;
                }
                using (Context db = new Context())
                {
                    capitalAccounts = db.Accounts.OfType<GeneralAccount>()
                        .Where(a => a.SubHeadAccountId == Properties.Resources.CapitalAccountSubHead).ToList();
                    foreach (var item in capitalAccounts)
                    {
                        item.Balance = 0;
                        var obj = db.AccountTransactions.Where(a => a.GeneralAccountId == item.Id).AsParallel()
                            .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                            .OrderByDescending(a => a.Id)
                            .FirstOrDefault();
                        if(obj != null)
                        {
                            item.Balance = obj.Balance;
                        }
                        totalBalance += item.Balance;
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                Gujjar.AddDatagridviewButton(dgv, btndgvdetails, "Details", "Details", 80);

                WaitForm wait1 = new WaitForm(LoadCapitalAccounts);
                wait1.ShowDialog();

                UpdateDgv();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void UpdateDgv()
        {
            tbTotalAccounts.Text = capitalAccounts.Count.ToString();
            tbTotalBalance.Text = totalBalance.ToString("n2");

            captitalAccountVMBindingSource.List.Clear();
            foreach (var item in capitalAccounts)
            {
                CaptitalAccountVM vm = new CaptitalAccountVM
                {
                    Id = item.Id,
                    Balance = item.Balance.ToString("n2"),
                    Title = item.Title
                };
                if (totalBalance > 0)
                    vm.SharePercentage = string.Format("{0} %", (item.Balance / totalBalance * 100).ToString("n2"));
                else
                    vm.SharePercentage = "0.00 %";

                captitalAccountVMBindingSource.List.Add(vm);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AddCapitalAccountForm form = new AddCapitalAccountForm();
                form.ShowDialog();

                if(form.IsDone)
                {
                    WaitForm wait1 = new WaitForm(LoadCapitalAccounts);
                    wait1.ShowDialog();

                    UpdateDgv();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                int ci = e.ColumnIndex;

                if(ri == -1 || ri == dgv.NewRowIndex)
                {
                    return;
                }

                if(dgv.Columns[btndgvdetails].Index == ci)
                {
                    string acctId = dgv.Rows[ri].Cells[0].Value.ToString();

                    CapitalAccountForm form = new CapitalAccountForm(acctId);
                    form.ShowDialog();

                    if(form.IsDone)
                    {
                        WaitForm wait = new WaitForm(LoadCapitalAccounts);
                        wait.ShowDialog();

                        UpdateDgv();
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
