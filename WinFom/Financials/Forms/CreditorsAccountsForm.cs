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
    public partial class CreditorsAccountsForm : Form
    {
        private string btndgvtrans = "btndgvdetails";
        private string btndgvpay = "btndgvpay12341";
        //private string btndgvpayprev = "btndgvcashreceive";
        //private string btndgvrecprev = "btndgvreceiveprevious";
        private string dgvbtnmindebitor = "dgvbtnmindebitor";
        private decimal totalBalance = 0;
        private List<GeneralAccount> creditorsAccounts = null;
        private AppSettings AppSett = Helper.AppSet;
        public CreditorsAccountsForm()
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
                if(creditorsAccounts != null)
                {
                    creditorsAccounts.Clear();
                    creditorsAccounts = null;
                }
                using (Context db = new Context())
                {
                    creditorsAccounts = db.Accounts.OfType<GeneralAccount>()
                        .Where((a => a.CrDrType == CrDrType.Creditor)).ToList();
                    foreach (var item in creditorsAccounts)
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
                string payLiability = "Pay Liability";
                string ledger = "Ledger";
                //Gujjar.AddDatagridviewButton(dgv, btndgvpayprev, addLibility, addLibility, 120);
                //Gujjar.AddDatagridviewButton(dgv, btndgvrecprev, recPrev, recPrev, 120);
                Gujjar.AddDatagridviewButton(dgv, btndgvpay, payLiability, payLiability, 120);
                Gujjar.AddDatagridviewButton(dgv, btndgvtrans, ledger, ledger, 80);
                Gujjar.AddDatagridviewButton(dgv, dgvbtnmindebitor, "Remove", "Remove", 80);
                WaitForm wait1 = new WaitForm(LoadAccounts);
                wait1.ShowDialog();

                UpdateDgv(creditorsAccounts);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void UpdateDgv(List<GeneralAccount> accounts)
        {
            tbTotalAccounts.Text = accounts.Count.ToString();
            tbTotalBalance.Text = totalBalance.ToString("n2");

            drCrVMBindingSource.List.Clear();
            foreach (var item in accounts)
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

                        UpdateDgv(creditorsAccounts);
                    }
                }
                /*
                if(dgv.Columns[btndgvpayprev].Index == ci)
                {
                    if (!Helper.ConfirmAdminPassword())
                        return;

                    string acctId = dgv.Rows[ri].Cells[0].Value.ToString();

                    CreditorPayPrevForm form = new CreditorPayPrevForm(acctId);
                    form.ShowDialog();

                    if (form.IsDone)
                    {
                        WaitForm wait = new WaitForm(LoadAccounts);
                        wait.ShowDialog();

                        UpdateDgv(creditorsAccounts);
                    }
                }
                if(dgv.Columns[btndgvrecprev].Index == ci)
                {
                    if (!Helper.ConfirmAdminPassword())
                        return;

                    string acctId = dgv.Rows[ri].Cells[0].Value.ToString();

                    CreditorReceivePrevForm form = new CreditorReceivePrevForm(acctId);
                    form.ShowDialog();

                    if (form.IsDone)
                    {
                        WaitForm wait = new WaitForm(LoadAccounts);
                        wait.ShowDialog();

                        UpdateDgv(creditorsAccounts);
                    }
                }
                */
                if (dgv.Columns[dgvbtnmindebitor].Index == ci)
                {
                    if (!Helper.ConfirmUserPassword())
                        return;

                    DialogResult rest = Gujjar.ConfirmYesNo("Are you sured please...??");
                    if (rest == DialogResult.No)
                        return;

                    string id = dgv.Rows[ri].Cells[0].Value.ToString();
                    using (Context db = new Context())
                    {
                        var obj = db.Accounts.Find(id) as GeneralAccount;
                        obj.CrDrType = CrDrType.General;
                        db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        WaitForm wait1 = new WaitForm(LoadAccounts);
                        wait1.ShowDialog();

                        UpdateDgv(creditorsAccounts);
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string term = tbSearch.Text;
            if (string.IsNullOrEmpty(term))
            {
                UpdateDgv(creditorsAccounts);
            }
            else
            {
                var accts = creditorsAccounts.Where(a => a.Title.ToLower().Contains(term.ToLower())).ToList();
                UpdateDgv(accts);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AddDrCrForm form = new AddDrCrForm(CrDrType.Creditor);
                form.ShowDialog();

                if(form.IsDone)
                {
                    WaitForm wait = new WaitForm(LoadAccounts);
                    wait.ShowDialog();

                    UpdateDgv(creditorsAccounts);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
