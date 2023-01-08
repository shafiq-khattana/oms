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
using WinFom.Properties;

namespace WinFom.Financials.Forms
{
    public partial class LaborPayableAccountsForm : Form
    {
        private string btndgvtrans = "btndgvdetails";
        private string btndgvpay = "btndgvpay12341";
        //private string btndgvpayprev = "btndgvcashreceive";
        //private string btndgvrecprev = "btndgvreceiveprevious";

        private decimal totalBalance = 0;
        private List<GeneralAccount> laborPayableAccounts = null;
        private AppSettings AppSett = Helper.AppSet;
        public LaborPayableAccountsForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadAccounts()
        {
            try
            {
                totalBalance = 0;
                if(laborPayableAccounts != null)
                {
                    laborPayableAccounts.Clear();
                    laborPayableAccounts = null;
                }
                
                using (Context db = new Context())
                {
                    laborPayableAccounts = db.Accounts.OfType<GeneralAccount>()
                        .Where(a => a.SubHeadAccountId == Resources.LaborExpensePayableSubHead).ToList();
                    foreach (var item in laborPayableAccounts)
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
                //string addLibility = "Add Liability";
                //string recPrev = "Receive Prev";
                string payLiability = "Pay Cash";
                string ledger = "Ledger";
                //Gujjar.AddDatagridviewButton(dgv, btndgvpayprev, addLibility, addLibility, 120);
                //Gujjar.AddDatagridviewButton(dgv, btndgvrecprev, recPrev, recPrev, 120);
                Gujjar.AddDatagridviewButton(dgv, btndgvpay, payLiability, payLiability, 120);
                Gujjar.AddDatagridviewButton(dgv, btndgvtrans, ledger, ledger, 80);
                
                WaitForm wait1 = new WaitForm(LoadAccounts);
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
            tbTotalAccounts.Text = laborPayableAccounts.Count.ToString();
            tbTotalBalance.Text = totalBalance.ToString("n2");

            drCrVMBindingSource.List.Clear();
            foreach (var item in laborPayableAccounts)
            {
                DrCrVM vm = new DrCrVM
                {
                    Id = item.Id,
                    Balance = item.Balance,
                    Title = item.Title
                };
                

                drCrVMBindingSource.List.Add(vm);
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

                if(dgv.Columns[btndgvtrans].Index == ci)
                {                 
                    string acctId = dgv.Rows[ri].Cells[0].Value.ToString();
                    AccountTransactionForm form = new AccountTransactionForm(acctId);
                    form.ShowDialog();              
                }
                if(dgv.Columns[btndgvpay].Index == ci)
                {
                    if (!Helper.ConfirmAdminPassword())
                        return;

                    string acctId = dgv.Rows[ri].Cells[0].Value.ToString();

                    PayCreditForm form = new PayCreditForm(acctId);
                    form.ShowDialog();

                    if(form.IsDone)
                    {
                        WaitForm wait = new WaitForm(LoadAccounts);
                        wait.ShowDialog();

                        UpdateDgv();
                    }
                }

                //if(dgv.Columns[btndgvpayprev].Index == ci)
                //{
                //    if (!Helper.ConfirmAdminPassword())
                //        return;

                //    string acctId = dgv.Rows[ri].Cells[0].Value.ToString();

                //    CreditorPayPrevForm form = new CreditorPayPrevForm(acctId);
                //    form.ShowDialog();

                //    if (form.IsDone)
                //    {
                //        WaitForm wait = new WaitForm(LoadAccounts);
                //        wait.ShowDialog();

                //        UpdateDgv();
                //    }
                //}

                //if(dgv.Columns[btndgvrecprev].Index == ci)
                //{
                //    if (!Helper.ConfirmAdminPassword())
                //        return;

                //    string acctId = dgv.Rows[ri].Cells[0].Value.ToString();

                //    CreditorReceivePrevForm form = new CreditorReceivePrevForm(acctId);
                //    form.ShowDialog();

                //    if (form.IsDone)
                //    {
                //        WaitForm wait = new WaitForm(LoadAccounts);
                //        wait.ShowDialog();

                //        UpdateDgv();
                //    }
                //}
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
