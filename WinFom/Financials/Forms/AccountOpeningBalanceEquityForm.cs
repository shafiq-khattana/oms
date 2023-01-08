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
using WinFom.Properties;

namespace WinFom.Financials.Forms
{
    public partial class AccountOpeningBalanceEquityForm : Form
    {
        private string accountId = "";
        GeneralAccount debitAccount = null;
        GeneralAccount creditAccount = null;
        GeneralAccount account = null;
        private AppUser appUser = SingleTon.LoginForm.appUser;
        private AppSettings AppSett = Helper.AppSet;
        private GeneralAccount tempAcccount = null;
        public bool IsDone { get; set; }

        decimal dAmount = 0;
        decimal cAmount = 0;
        public AccountOpeningBalanceEquityForm(string acctId)
        {
            InitializeComponent();
            accountId = acctId;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                Gujjar.TB4(pMain);
                //Gujjar.NumbersOnly(tbAmount);
                tbRemarks.Text = "N/A";
                Gujjar.TBOptional(tbRemarks);
                dtp.MinDate = AppSett.StartDate;
                dtp.MaxDate = AppSett.EndDate;
                IsDone = false;

                using (Context db = new Context())
                {
                    account = db.Accounts.Find(accountId) as GeneralAccount;
                    tbAccountTitle.Text = account.Title;

                    if(account.AccountNature == AccountNature.Debit)
                    {
                        creditAccount = db.Accounts.Find(Resources.OpeningBalanceEquityAccount) as GeneralAccount;
                        debitAccount = account;
                    }
                    else
                    {
                        debitAccount = db.Accounts.Find(Resources.OpeningBalanceEquityAccount) as GeneralAccount;
                        creditAccount = account;
                    }
                }
                tbTransType.Text = account.AccountNature.ToString();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }

                decimal amount = tbAmount.Text.ToDecimal();
                if(amount == 0)
                {
                    throw new Exception("Please enter valid amount");
                }

                if (!Helper.ConfirmAdminPassword())
                    return;

                decimal absAmount = Math.Abs(amount);

                if(amount > 0)
                {
                    if (account.AccountNature == AccountNature.Debit)
                    {
                        dAmount = absAmount;
                        cAmount = absAmount;
                    }
                    else
                    {
                        cAmount = absAmount;
                        dAmount = -absAmount;
                    }
                }
                else
                {
                    tempAcccount = debitAccount;
                    debitAccount = creditAccount;
                    creditAccount = tempAcccount;

                    if(account.AccountNature == AccountNature.Debit)
                    {
                        cAmount = -absAmount;
                        dAmount = -absAmount;
                    }
                    else
                    {
                        cAmount = absAmount;
                        dAmount = -absAmount;
                    }
                }
                string remarks = tbRemarks.Text;

                string msg = string.Format("Opening balance amount ({0}) is introduced in account ({1}), by ({2})", amount.ToString("n1"),
                    account.Title, appUser.Id);

                DialogResult res = Gujjar.ConfirmYesNo(string.Format("Please confirm...\n\n{0}", msg));
                if (res == DialogResult.No)
                    return;

                using (Context db = new Context())
                {
                    #region "Financials"

                    
                    DayBook daybookEntry = new DayBook
                    {
                        Id = 0,
                        Amount = absAmount,
                        Date = dtp.Value,
                        Description = msg,
                        CanRollBack = true,
                        InDate = DateTime.Now.Date
                    };

                    daybookEntry = db.DayBooks.Add(daybookEntry);
                    db.SaveChanges();

                    #region "Debit transaction"

                    AccountTransaction debitItemTrans = new AccountTransaction
                    {
                        Id = 0,
                        Account = null,
                        AccountTransactionType = AccountTransactionType.Debit,
                        Balance = dAmount,
                        CreditAmount = 0,
                        Date = dtp.Value,
                        DayBookId = daybookEntry.Id,
                        DebitAmount = absAmount,
                        GeneralAccountId = debitAccount.Id,
                        Description = msg
                    };

                    var debitDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == debitAccount.Id).AsParallel()
                        .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                        .OrderByDescending(a => a.Id).FirstOrDefault();

                    if (debitDbEntry != null)
                    {
                        debitItemTrans.Balance += debitDbEntry.Balance;
                    }

                    debitItemTrans = db.AccountTransactions.Add(debitItemTrans);
                    db.SaveChanges();



                    #endregion

                    #region  "Credit transaction"
                    AccountTransaction creditItemTrans = new AccountTransaction
                    {
                        Id = 0,
                        Account = null,
                        AccountTransactionType = AccountTransactionType.Credit,
                        Balance = cAmount,
                        CreditAmount = absAmount,
                        Date = DateTime.Now,
                        DayBookId = daybookEntry.Id,
                        DebitAmount = 0,
                        GeneralAccountId = creditAccount.Id,
                        Description = msg
                    };

                    var creditDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == creditAccount.Id).AsParallel()
                        .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                        .OrderByDescending(a => a.Id).FirstOrDefault();

                    if (creditDbEntry != null)
                    {
                        creditItemTrans.Balance += creditDbEntry.Balance;
                    }

                    creditItemTrans = db.AccountTransactions.Add(creditItemTrans);
                    db.SaveChanges();
                    #endregion

                    var daybookdb = db.DayBooks.Find(daybookEntry.Id);
                    daybookdb.DebitTrace = string.Format("({0}). Trans Id: {1}", debitAccount.Title, debitItemTrans.Id);
                    daybookdb.CreditTrace = string.Format("({0}). Trans Id: {1}", creditAccount.Title, creditItemTrans.Id);
                    daybookdb.CreditAccountId = creditAccount.Id;
                    daybookdb.DebitAccountId = debitAccount.Id;

                    db.Entry(daybookdb).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    #endregion
                    
                    if(AppSett.PrintFinancialTransactions)
                    {
                        Helper.PrintTransactions(new List<DayBook> { daybookdb });
                    }
                    Gujjar.InfoMsg(msg);
                    IsDone = true;
                    Close();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal tamt = 0;
                string type = account.AccountNature.ToString();
                if (!string.IsNullOrEmpty(tbAmount.Text))
                {
                    tamt = tbAmount.Text.ToDecimal();
                }
                if (tamt > 0)
                {
                    type = account.AccountNature.ToString();
                }
                else if (tamt < 0)
                {
                    type = account.AccountNature == AccountNature.Credit ? "Debit" : "Credit";
                }
                tbTransType.Text = type;
            }
            catch (Exception)
            {
                
            }
            
        }
    }
}
