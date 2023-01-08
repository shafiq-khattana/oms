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
    public partial class JVForm : Form
    {
        private List<GeneralAccount> accountsList = null;
        private List<GeneralAccount> debitAccounts = null;
        private List<GeneralAccount> creditAccounts = null;
        private GeneralAccount debitAccount = null;
        private GeneralAccount creditAccount = null;
        private List<AccountSearchVM> accountSearchList = null;
        public bool IsDone { get; set; }
        public JVForm()
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
                using (Context db = new Context())
                {
                    AppSettings appSettings = db.AppSettings.First();
                    if(accountsList != null)
                    {
                        accountsList.Clear();
                        accountsList = null;
                    }
                    accountsList = db.Accounts.OfType<GeneralAccount>().ToList().OrderBy(a => a.Title).ToList();
                    decimal balance = 0;
                    var transList = db.AccountTransactions.Where(a => a.Date >= appSettings.StartDate && a.Date <= appSettings.EndDate)
                        .AsParallel().ToList();
                    foreach (var item in accountsList)
                    {
                        balance = 0;
                        var trans = transList.Where(a => a.GeneralAccountId == item.Id).LastOrDefault();
                        if(trans != null)
                        {
                            balance = trans.Balance;
                        }
                        item.Balance = balance;
                    }
                }
                LoadBothAccounts();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadBothAccounts()
        {
            try
            {
                debitAccounts = new List<GeneralAccount>();
                creditAccounts = new List<GeneralAccount>();
                accountSearchList = new List<AccountSearchVM>();

                foreach (var item in accountsList)
                {
                    GeneralAccount g = new GeneralAccount
                    {
                        AccountNature = item.AccountNature,
                        Balance = item.Balance,
                        Id = item.Id,
                        Title = item.Title,
                        SubHeadAccountId = item.SubHeadAccountId
                    };

                    GeneralAccount g2 = new GeneralAccount
                    {
                        AccountNature = item.AccountNature,
                        Balance = item.Balance,
                        Id = item.Id,
                        Title = item.Title,
                        SubHeadAccountId = item.SubHeadAccountId
                    };

                    AccountSearchVM asvm = new AccountSearchVM
                    {
                        Id = item.Id,
                        Title = item.Title
                    };
                    debitAccounts.Add(g);
                    creditAccounts.Add(g2);
                    accountSearchList.Add(asvm);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void BindCbS()
        {
            cbCreditAccounts.DataSource = creditAccounts;
            cbCreditAccounts.DisplayMember = "Title";
            cbCreditAccounts.ValueMember = "Id";

            cbCreditAccounts.SelectedItem = cbCreditAccounts.Items.OfType<GeneralAccount>()
                .FirstOrDefault(a => a.Id == Properties.Resources.CashInHand);
            cbDebitAccounts.DataSource = debitAccounts;
            cbDebitAccounts.DisplayMember = "Title";
            cbDebitAccounts.ValueMember = "Id";

            cbDebitAccounts.SelectedItem = cbDebitAccounts.Items.OfType<GeneralAccount>()
                .FirstOrDefault(a => a.Id == Properties.Resources.CashInHand);

        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait1 = new WaitForm(LoadAccounts);
                wait1.ShowDialog();

                BindCbS();

                Gujjar.TB4(pMain);
                Gujjar.TBOptional(tbDescription);
                tbDescription.Text = "N/A";
                Gujjar.NumbersOnly(tbTransAmount);
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

        private void cbDebitAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbDebitAccounts.SelectedIndex == -1)
            {
                debitAccount = null;
                tbDebitBalance.Text = "0.0";
                return;
            }
            debitAccount = cbDebitAccounts.SelectedItem as GeneralAccount;
            tbDebitBalance.Text = debitAccount.Balance.ToString("n2");
        }

        private void cbCreditAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbCreditAccounts.SelectedIndex == -1)
            {
                creditAccount = null;
                tbCreditBalance.Text = "0.0";
            }
            creditAccount = cbCreditAccounts.SelectedItem as GeneralAccount;
            tbCreditBalance.Text = creditAccount.Balance.ToString("n2");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }
                if(debitAccount == null)
                {
                    throw new Exception("Please select debit account");
                }
                if(creditAccount == null)
                {
                    throw new Exception("Please select credit account");
                }

                if(debitAccount.Id == creditAccount.Id)
                {
                    throw new Exception("Debit and credit account can't be same.");
                }

                if(!Helper.ConfirmUserPassword())
                {
                    return;
                }

                decimal amount = tbTransAmount.Text.ToDecimal();
                string description = tbDescription.Text;

                string msg = string.Format("General Transaction: Debit account ({0}), credit account ({1}), transaction amount ({2}), description ({3}).\nPleasse confirm .. !!",
                    debitAccount.Title, creditAccount.Title, amount.ToString("n2"), description);

                DialogResult res = Gujjar.ConfirmYesNo(msg);
                if (res == DialogResult.No)
                    return;

                decimal debitAmount;
                decimal creditAmount;
                debitAmount = creditAmount = amount;

                if (debitAccount.AccountNature != AccountNature.Debit)
                    debitAmount = -debitAmount;
                if (creditAccount.AccountNature != AccountNature.Credit)
                    creditAmount = -creditAmount;

                using (Context db = new Context())
                {
                    #region "Financials"


                    AppSettings appSett = db.AppSettings.First();

                    string msg2 = string.Format("General Transaction. Amount ({0}) is debitted to account ({1}) and creditted to account ({2}), description: {3}, dated ({4})",
                        amount.ToString("n2"), debitAccount.Title, creditAccount.Title, description, dtp.Value.ToShortDateString());

                    DayBook daybookEntry = new DayBook
                    {
                        Id = 0,
                        Amount = amount,
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
                        Balance = debitAmount,
                        CreditAmount = 0,
                        Date = dtp.Value,
                        DayBookId = daybookEntry.Id,
                        DebitAmount = amount,
                        GeneralAccountId = debitAccount.Id,
                        Description = msg
                    };

                    var debitDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == debitAccount.Id).AsParallel()
                        .ToList().Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date)
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
                        Balance = creditAmount,
                        CreditAmount = amount,
                        Date = dtp.Value,
                        DayBookId = daybookEntry.Id,
                        DebitAmount = 0,
                        GeneralAccountId = creditAccount.Id,
                        Description = msg
                    };

                    var creditDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == creditAccount.Id).AsParallel()
                        .ToList().Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date)
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

                    if (appSett.PrintFinancialTransactions)
                    {
                        Helper.PrintTransactions(new List<DayBook> { daybookdb });
                    }
                    Gujjar.InfoMsg(msg2);
                    IsDone = true;
                    Close();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnDebitSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchAccountForm form = new SearchAccountForm(accountSearchList);
                form.ShowDialog();
                string id = form.AccountId;
                if(!string.IsNullOrEmpty(id))
                {
                    cbDebitAccounts.SelectedItem = cbDebitAccounts.Items.OfType<GeneralAccount>()
                        .FirstOrDefault(a => a.Id == id);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SearchAccountForm form = new SearchAccountForm(accountSearchList);
                form.ShowDialog();
                string id = form.AccountId;
                if (!string.IsNullOrEmpty(id))
                {
                    cbCreditAccounts.SelectedItem = cbCreditAccounts.Items.OfType<GeneralAccount>()
                        .FirstOrDefault(a => a.Id == id);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
