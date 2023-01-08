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
using Khattana.Display;
using Khattana.Model;
using WinFom.Financials.Forms;

namespace WinFom.Financials.Forms
{
    public partial class AccountsListForm : Form
    {
        private List<GeneralAccount> bankAccounts = null;
        private string btndgvtrans = "btndgvtrans23414";
        private string btndgvopenbalanceequity = "btndgvopenbalanceequity";
        private string btndgvlinkaccount = "btndgvlinkaccount";
        private AppSettings AppSett = Helper.AppSet;
        private List<GeneralAccountVM> accountVMList = null;
        public AccountsListForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadBankList()
        {
            try
            {
                if(accountVMList != null)
                {
                    accountVMList.Clear();
                    accountVMList = null;
                }
                accountVMList = new List<GeneralAccountVM>();
                using (Context db = new Context())
                {
                    bankAccounts = db.Accounts.OfType<GeneralAccount>()
                        .AsParallel().ToList().OrderBy(a => a.Title).ToList();
                    decimal bal = 0;
                    foreach (var item in bankAccounts)
                    {
                        decimal linkBal = 0;
                        item.LinkAccounts = db.Accounts.OfType<GeneralAccount>()
                            .Where(a => a.ParentAccountId == item.Id).ToList();
                        foreach (var act in item.LinkAccounts)
                        {
                            decimal bal2 = 0;
                            var trans2 = db.AccountTransactions.Where(a => a.GeneralAccountId == act.Id)
                             .OrderByDescending(a => a.Id).FirstOrDefault();
                            if (trans2 != null)
                            {
                                bal2 = trans2.Balance;
                            }
                            if(item.AccountNature == act.AccountNature)
                            {
                                linkBal += bal2;
                            }
                            else
                            {
                                linkBal -= bal2;
                            }
                        }
                        bal = 0;
                        var trans = db.AccountTransactions.Where(a => a.GeneralAccountId == item.Id)
                            .OrderByDescending(a => a.Id).FirstOrDefault();
                        if (trans != null)
                        {
                            bal = trans.Balance;
                        }
                        item.Balance = bal;

                        GeneralAccountVM vm = new GeneralAccountVM
                        {
                            Balance = item.Balance,
                            EffectiveBalance = item.Balance + linkBal,
                            Id = item.Id,
                            LinkBalance = linkBal,
                            Links = item.LinkAccounts.Count,
                            Title = item.Title
                        };

                        accountVMList.Add(vm);
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
                Gujjar.AddDatagridviewButton(dgv, btndgvopenbalanceequity, "+Opening Balance", "+Opening Balance", 140);
                Gujjar.AddDatagridviewButton(dgv, btndgvtrans, "Transactions", "Transactions", 110);
                Gujjar.AddDatagridviewButton(dgv, btndgvlinkaccount, "Link Accounts", "Link Accounts", 110);

                WaitForm wait1 = new WaitForm(LoadBankList);
                wait1.ShowDialog();
                UpdateDgv(accountVMList);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void UpdateDgv(List<GeneralAccountVM> accounts)
        {
            generalAccountVMBindingSource.List.Clear();
            foreach (var item in accounts)
            {               
                generalAccountVMBindingSource.List.Add(item);
            }
            tbCount.Text = accounts.Count.ToString();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                int ci = e.ColumnIndex;

                if (ri == -1 || ri == dgv.NewRowIndex)
                {
                    return;
                }

                if (dgv.Columns[btndgvtrans].Index == ci)
                {
                    string acctId = dgv.Rows[ri].Cells[0].Value.ToString();
                    AccountTransactionForm form = new AccountTransactionForm(acctId);
                    form.ShowDialog();
                }
                if (dgv.Columns[btndgvopenbalanceequity].Index == ci)
                {
                    string acctId = dgv.Rows[ri].Cells[0].Value.ToString();
                    AccountOpeningBalanceEquityForm form = new AccountOpeningBalanceEquityForm(acctId);
                    form.ShowDialog();
                    if(form.IsDone)
                    {
                        WaitForm wait1 = new WaitForm(LoadBankList);
                        wait1.ShowDialog();
                        UpdateDgv(accountVMList);
                    }
                }
                if(dgv.Columns[btndgvlinkaccount].Index == ci)
                {
                    string acctId = dgv.Rows[ri].Cells[0].Value.ToString();
                    AddLinkAccountForm form = new AddLinkAccountForm(acctId);
                    form.ShowDialog();

                    if(form.IsDone)
                    {
                        WaitForm wait1 = new WaitForm(LoadBankList);
                        wait1.ShowDialog();
                        UpdateDgv(accountVMList);
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbAccountTitle_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string txt = tbAccountTitle.Text;
                if (string.IsNullOrEmpty(txt))
                {
                    UpdateDgv(accountVMList);
                }
                else
                {
                    var accts = accountVMList.Where(a => a.Title.ToLower().Contains(txt.ToLower())).ToList();
                    UpdateDgv(accts);
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
    }
}
