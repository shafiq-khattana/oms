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
    public partial class TransactionForms : Form
    {
        private bool IsDated = false;
        private DateTime fromDt = DateTime.Now;
        private DateTime toDt = DateTime.Now;
        private DateTime todayDt = DateTime.Now.Date;
        private List<AccountTransaction> trans = null;
        private AppSettings AppSett = Helper.AppSet;
        public TransactionForms()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadTrans()
        {
            try
            {
                if(trans != null)
                {
                    trans.Clear();
                    trans = null;
                    GC.Collect();
                }
                using (Context db = new Context())
                {
                    if(IsDated)
                    {
                        trans = db.AccountTransactions.ToList().Where(a => a.Date.Date >= fromDt && a.Date.Date <= toDt).ToList();
                    }
                    else
                    {
                        trans = db.AccountTransactions.ToList().Where(a => a.Date.Date == todayDt).ToList();
                    }
                    if(generalAccount.Id != "gen")
                    {
                        trans = trans.Where(a => a.GeneralAccountId == generalAccount.Id).ToList();
                    }
                    //foreach (var item in trans)
                    //{
                    //    item.Account = db.Accounts.OfType<GeneralAccount>().FirstOrDefault(a => a.Id == item.GeneralAccountId);
                        
                    //}
                    switch(transType)
                    {
                        case "Debit":
                            trans = trans.Where(a => a.AccountTransactionType == AccountTransactionType.Debit).ToList();
                            break;
                        case "Credit":
                            trans = trans.Where(a => a.AccountTransactionType == AccountTransactionType.Credit).ToList();
                            break;
                    }
                }
                    
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private List<GeneralAccount> generalAccounts = null;
        private GeneralAccount generalAccount = null;
        private void LoadAccounts()
        {
            try
            {
                using (Context db = new Context())
                {
                    generalAccounts = db.Accounts.OfType<GeneralAccount>().ToList();
                    var generalAccount2 = new GeneralAccount
                    {
                        Title = "All Accounts",
                        Id = "gen"
                    };
                    generalAccounts.Add(generalAccount2);
                    generalAccounts = generalAccounts.OrderBy(a => a.Title).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void BindCbAccounts()
        {
            cbAccounts.DataSource = generalAccounts;
            cbAccounts.DisplayMember = "Title";
            cbAccounts.ValueMember = "Id";

            cbAccounts.SelectedItem = cbAccounts.Items.OfType<GeneralAccount>()
                .FirstOrDefault(a => a.Id == "gen");
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                dtpFrom.MinDate = dtpTo.MinDate =  AppSett.StartDate;
                dtpTo.MaxDate = dtpFrom.MaxDate = AppSett.EndDate;
                IsDated = false;
                WaitForm wait2 = new WaitForm(LoadAccounts);
                wait2.ShowDialog();
                BindCbAccounts();

                WaitForm wait1 = new WaitForm(LoadTrans);
                wait1.ShowDialog();

                BindTrans(trans);
                cbTransTypes.Text = "Both";
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void BindTrans(List<AccountTransaction> trans)
        {
            try
            {
                decimal debit = trans.Sum(a => a.DebitAmount);
                decimal credit = trans.Sum(a => a.CreditAmount);

                decimal bal = debit - credit;
                string balMsg = "";
                if(bal > 0)
                {
                    balMsg = string.Format("{0}-/ (Dr)", bal.ToString("n4"));
                }
                else if(bal < 0)
                {
                    balMsg = string.Format("{0}-/ (Cr)", bal.ToString("n4"));
                }
                else
                {
                    balMsg = "0.00";
                }
                tbDebitCreditBalance.Text = balMsg;

                if(IsDated)
                {
                    label4.Text = string.Format("Dated Results ({0})", transType);
                }
                else
                {
                    label4.Text = string.Format("Today's results ({0})", transType);
                }

                tbCredit.Text = credit.ToString("n4");
                tbDebit.Text = debit.ToString("n4");
                tbEntriesCount.Text = trans.Count.ToString();

                transactionsVMBindingSource.List.Clear();
                foreach (var item in trans)
                {
                    TransactionsVM vm = new TransactionsVM
                    {
                        Id = item.Id,
                        //Account = item.Account.Title,
                        //AccountTransactionType = item.AccountTransactionType.ToString(),
                        CreditAmount = item.CreditAmount.ToString("n4"),
                        Balance = item.Balance.ToString("n4"),
                        Date = item.Date.ToString(),
                        DebitAmount = item.DebitAmount.ToString("n4"),
                        DayBookId = item.DayBookId,
                        Description = item.Description
                    };
                    transactionsVMBindingSource.List.Add(vm);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void cbAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAccounts.SelectedIndex == -1)
                return;
            generalAccount = cbAccounts.SelectedItem as GeneralAccount;
        }

        string transType = "Both";
        private void cbTransTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTransTypes.SelectedIndex == -1)
                return;
            transType = cbTransTypes.Text;
        }

        private void btnAccountView_Click(object sender, EventArgs e)
        {
            try
            {
                List<AccountTransaction> trans2 = null;
                if (generalAccount.Id != "gen")
                {
                    trans2 = trans.Where(a => a.GeneralAccountId == generalAccount.Id).ToList();
                }
                else
                {
                    trans2 = trans.ToList();
                }
                
                switch (transType)
                {
                    case "Debit":
                        trans2 = trans2.Where(a => a.AccountTransactionType == AccountTransactionType.Debit).ToList();
                        break;
                    case "Credit":
                        trans2 = trans2.Where(a => a.AccountTransactionType == AccountTransactionType.Credit).ToList();
                        break;
                }

                BindTrans(trans2);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                IsDated = true;
                fromDt = dtpFrom.Value.Date;
                toDt = dtpTo.Value.Date;

                WaitForm wait1 = new WaitForm(LoadTrans);
                wait1.ShowDialog();

                BindTrans(trans);
            }
            catch (Exception ep)
            {
                Gujjar.ErrMsg(ep);
            }
        }

        private void btnTodayOnly_Click(object sender, EventArgs e)
        {
            try
            {
                IsDated = false;
                WaitForm wait1 = new WaitForm(LoadTrans);
                wait1.ShowDialog();

                BindTrans(trans);
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
