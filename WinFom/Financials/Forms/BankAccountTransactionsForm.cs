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
using Khattana.Display;
using Khattana.Model;
using Model.Financials.ViewModel;

namespace WinFom.Financials.Forms
{
    public partial class BankAccountTransactionsForm : Form
    {
        private AppSettings AppSett = Helper.AppSet;
        private List<AccountTransaction> transactions = null;
        private List<AccountTransaction> tempTrans = null;
        private decimal balance = 0;
        GeneralAccount bankAccount = null;
        private string bankAccountId = "";
        public BankAccountTransactionsForm(string bankAcctId)
        {
            InitializeComponent();
            bankAccountId = bankAcctId;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PopulateTempTrans(List<AccountTransaction> trans)
        {
            if(tempTrans != null)
            {
                tempTrans.Clear();
                tempTrans = null;
            }
            tempTrans = new List<AccountTransaction>();
            foreach (var item in trans)
            {
                tempTrans.Add(item);
            }
        }
        private void LoadTransactions()
        {
            try
            {
                if(transactions != null)
                {
                    transactions.Clear();
                    transactions = null;
                }
                using (Context db = new Context())
                {
                    bankAccount = db.Accounts.Find(bankAccountId) as GeneralAccount;

                    transactions = db.AccountTransactions.Where(a => a.GeneralAccountId == bankAccountId)
                        .AsParallel().ToList()
                        .Where(a => a.Date.Date >= AppSett.StartDate && a.Date.Date <= AppSett.EndDate).ToList();
                    //foreach (var item in transactions)
                    //{
                    //    item.Account = db.Accounts.OfType<GeneralAccount>()
                    //        .FirstOrDefault(a => a.Id == item.GeneralAccountId);
                    //}

                    PopulateTempTrans(transactions);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void UpdateDgv(List<AccountTransaction> trans)
        {
            
            
            transactionsVMBindingSource.List.Clear();
            tbEntriesCount.Text = trans.Count.ToString();

            decimal debit = 0;
            decimal credit = 0;
            if(trans.Count > 0)
            {
                debit = trans.Where(a => a.AccountTransactionType == AccountTransactionType.Debit).Sum(a => a.DebitAmount);
                credit = trans.Where(a => a.AccountTransactionType == AccountTransactionType.Credit).Sum(a => a.CreditAmount);
            }

            tbDebit.Text = debit.ToString("n4");
            tbCredit.Text = credit.ToString("n4");
            try
            {
                foreach (var item in trans)
                {
                    TransactionsVM vm = new TransactionsVM
                    {
                        Id = item.Id,
                        //Account = item.Account.Title,
                        //AccountTransactionType = item.AccountTransactionType.ToString(),
                        Balance = item.Balance.ToString("n3"),
                        CreditAmount = item.CreditAmount.ToString("n3"),
                        Date = item.Date.ToString(),
                        DayBookId = item.DayBookId,
                        DebitAmount = item.DebitAmount.ToString("n3"),
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
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait = new WaitForm(LoadTransactions);
                wait.ShowDialog();

                lblHeading.Text = bankAccount.Title;

                UpdateDgv(transactions);

                var obj = transactions.OrderByDescending(a => a.Id).FirstOrDefault();
                if (obj != null)
                {
                    balance = obj.Balance;
                }
                else
                {
                    balance = 0;
                }
                tbAccountBalance.Text = balance.ToString("n4");
                cbTransTypes.Text = "Both";
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            try
            {
                if(!SingleTon.LoginForm.RememberMeAdmin)
                {
                    SecurityConfirmForm form = new SecurityConfirmForm(PasswordType.Admin, AppSett.MasterPassword);
                    form.ShowDialog();

                    SecurityConfirmResponse respone = form.Response;
                    if(respone.IsValid == null || !respone.IsValid.Value)
                    {
                        if (respone.IsValid == null)
                            return;
                        else
                            throw new Exception("Invalid password");
                    }

                    SingleTon.LoginForm.RememberMeAdmin = form.Response.RememberMe;

                    
                }
                decimal amount = 0;
                AmountDepositWithdrawForm form2 = new AmountDepositWithdrawForm(AmountDepositWithdrawFormType.Deposit);
                form2.ShowDialog();

                amount = form2.Amount;
                if (amount == 0)
                    return;

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            string message = string.Format("Capital amount ({0}) introduced in business", amount);
                            DayBook capitalEntryDayBook = new DayBook
                            {
                                Id = 0,
                                Date = DateTime.Now,
                                Description = message,
                                Amount = amount,
                                CanRollBack = true,
                                InDate = DateTime.Now.Date
                            };

                            capitalEntryDayBook = db.DayBooks.Add(capitalEntryDayBook);
                            db.SaveChanges();

                            #region "Credit to capital account"
                            AccountTransaction capitalCreditTrans = new AccountTransaction
                            {
                                Id = 0,
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Credit,
                                Balance = amount,
                                CreditAmount = amount,
                                Date = DateTime.Now,
                                DayBookId = capitalEntryDayBook.Id,
                                DebitAmount = 0,
                                Description = message,
                                GeneralAccountId = Properties.Resources.ReadyGoodsSubHead
                            };

                            var capitalDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == capitalCreditTrans.GeneralAccountId)
                                .AsParallel().ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                .OrderByDescending(a => a.Id).FirstOrDefault();
                            if (capitalDbEntry != null)
                            {
                                capitalCreditTrans.Balance += capitalDbEntry.Balance;
                            }
                            capitalCreditTrans = db.AccountTransactions.Add(capitalCreditTrans);
                            db.SaveChanges();
                            #endregion

                            #region "Debit to cash account"
                            AccountTransaction cashDebitTrans = new AccountTransaction
                            {
                                Id = 0,
                                Balance = amount,
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Debit,
                                CreditAmount = 0,
                                DebitAmount = amount,
                                Date = DateTime.Now,
                                DayBookId = capitalEntryDayBook.Id,
                                Description = message,
                                GeneralAccountId = Properties.Resources.CashInHand
                            };

                            var cashDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == cashDebitTrans.GeneralAccountId)
                                .AsParallel().ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                .OrderByDescending(a => a.Id).FirstOrDefault();

                            if (cashDbEntry != null)
                            {
                                cashDebitTrans.Balance += cashDbEntry.Balance;
                            }

                            cashDebitTrans = db.AccountTransactions.Add(cashDebitTrans);
                            db.SaveChanges();
                            #endregion

                            #region "Accounts and daybook traces"
                            GeneralAccount generalCapitalAccountCr = db.Accounts.OfType<GeneralAccount>().FirstOrDefault(a => a.Id == capitalCreditTrans.GeneralAccountId);
                            GeneralAccount generalCashAccountDr = db.Accounts.OfType<GeneralAccount>().FirstOrDefault(a => a.Id == cashDebitTrans.GeneralAccountId);

                            DayBook dbDaybook = db.DayBooks.Find(capitalEntryDayBook.Id);
                            dbDaybook.DebitTrace = string.Format("({0}). Trans Id: {1}", generalCashAccountDr.Title, cashDebitTrans.Id);
                            dbDaybook.CreditTrace = string.Format("({0}). Trans Id: {1}", generalCapitalAccountCr.Title, capitalCreditTrans.Id);
                            dbDaybook.DebitAccountId = generalCashAccountDr.Id;
                            dbDaybook.CreditAccountId = generalCapitalAccountCr.Id;

                            db.Entry(dbDaybook).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            trans.Commit();
                            if (AppSett.PrintFinancialTransactions)
                            {
                                Helper.PrintTransactions(new List<DayBook> { dbDaybook });
                            }
                            Gujjar.InfoMsg("Transaction took placed successfully");
                            #endregion

                            WaitForm wait = new WaitForm(LoadTransactions);
                            wait.ShowDialog();

                            UpdateDgv(transactions);
                        }
                        catch (Exception exp2)
                        {
                            trans.Rollback();
                            throw exp2;
                        }
                        
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnCashwithdraw_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SingleTon.LoginForm.RememberMeAdmin)
                {
                    SecurityConfirmForm form = new SecurityConfirmForm(PasswordType.Admin, AppSett.MasterPassword);
                    form.ShowDialog();

                    SecurityConfirmResponse respone = form.Response;
                    if (respone.IsValid == null || !respone.IsValid.Value)
                    {
                        if (respone.IsValid == null)
                            return;
                        else
                            throw new Exception("Invalid password");
                    }

                    SingleTon.LoginForm.RememberMeAdmin = form.Response.RememberMe;


                }
                decimal amount = 0;
                AmountDepositWithdrawForm form2 = new AmountDepositWithdrawForm(AmountDepositWithdrawFormType.Withdraw);
                form2.ShowDialog();

                amount = form2.Amount;
                if (amount == 0)
                    return;

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            string message = string.Format("Capital amount ({0}) withdrawn by user ({1}).", amount, SingleTon.LoginForm.appUser.Name);
                            DayBook capitalEntryDayBook = new DayBook
                            {
                                Id = 0,
                                Date = DateTime.Now,
                                Description = message,
                                Amount = amount,
                                CanRollBack = true,
                                InDate = DateTime.Now.Date
                            };

                            capitalEntryDayBook = db.DayBooks.Add(capitalEntryDayBook);
                            db.SaveChanges();

                            #region "Debit to capital account"
                            AccountTransaction capitalDebitTrans = new AccountTransaction
                            {
                                Id = 0,
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Debit,
                                Balance = -amount,
                                CreditAmount = 0,
                                Date = DateTime.Now,
                                DayBookId = capitalEntryDayBook.Id,
                                DebitAmount = amount,
                                Description = message,
                                GeneralAccountId = Properties.Resources.ReadyGoodsSubHead
                            };

                            var capitalDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == capitalDebitTrans.GeneralAccountId)
                                .AsParallel().ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                .OrderByDescending(a => a.Id).FirstOrDefault();
                            if (capitalDbEntry != null)
                            {
                                capitalDebitTrans.Balance += capitalDbEntry.Balance;
                            }
                            capitalDebitTrans = db.AccountTransactions.Add(capitalDebitTrans);
                            db.SaveChanges();
                            #endregion

                            #region "Credit to cash account"
                            AccountTransaction cashCreditTrans = new AccountTransaction
                            {
                                Id = 0,
                                Balance = -amount,
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Credit,
                                CreditAmount = 0,
                                DebitAmount = amount,
                                Date = DateTime.Now,
                                DayBookId = capitalEntryDayBook.Id,
                                Description = message,
                                GeneralAccountId = Properties.Resources.CashInHand
                            };

                            var cashDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == cashCreditTrans.GeneralAccountId)
                                .AsParallel().ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                .OrderByDescending(a => a.Id).FirstOrDefault();

                            if (cashDbEntry != null)
                            {
                                cashCreditTrans.Balance += cashDbEntry.Balance;
                            }

                            cashCreditTrans = db.AccountTransactions.Add(cashCreditTrans);
                            db.SaveChanges();
                            #endregion

                            #region "Accounts and daybook traces"
                            GeneralAccount generalCapitalAccountdr = db.Accounts.OfType<GeneralAccount>().FirstOrDefault(a => a.Id == capitalDebitTrans.GeneralAccountId);
                            GeneralAccount generalCashAccountcr = db.Accounts.OfType<GeneralAccount>().FirstOrDefault(a => a.Id == cashCreditTrans.GeneralAccountId);

                            DayBook dbDaybook = db.DayBooks.Find(capitalEntryDayBook.Id);
                            dbDaybook.CreditTrace = string.Format("({0}). Trans Id: {1}", generalCashAccountcr.Title, cashCreditTrans.Id);
                            dbDaybook.DebitTrace = string.Format("({0}). Trans Id: {1}", generalCapitalAccountdr.Title, capitalDebitTrans.Id);
                            dbDaybook.DebitAccountId = generalCapitalAccountdr.Id;
                            dbDaybook.CreditAccountId = generalCashAccountcr.Id;

                            db.Entry(dbDaybook).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            trans.Commit();

                            Gujjar.InfoMsg("Transaction took placed successfully");
                            #endregion

                            WaitForm wait = new WaitForm(LoadTransactions);
                            wait.ShowDialog();

                            UpdateDgv(transactions);
                        }
                        catch (Exception exp2)
                        {
                            trans.Rollback();
                            throw exp2;
                        }

                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAccountView_Click(object sender, EventArgs e)
        {
            try
            {
                string type = cbTransTypes.Text;
                switch(type)
                {
                    case "Both":
                        UpdateDgv(tempTrans);
                        break;

                    case "Debit":
                        UpdateDgv(tempTrans.Where(a => a.AccountTransactionType == AccountTransactionType.Debit).ToList());
                        break;

                    case "Credit":
                        UpdateDgv(tempTrans.Where(a => a.AccountTransactionType == AccountTransactionType.Credit).ToList());
                        break;
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            tempTrans = transactions.Where(a => a.Date.Date >= dtpFrom.Value.Date && a.Date.Date <= dtpTo.Value.Date).ToList();
            UpdateDgv(tempTrans);
        }

        private void btnAllRecords_Click(object sender, EventArgs e)
        {
            PopulateTempTrans(transactions);
            UpdateDgv(tempTrans);
        }

        private void btnTodayOnly_Click(object sender, EventArgs e)
        {
            tempTrans = tempTrans.Where(a => a.Date.Date == DateTime.Now.Date).ToList();
            UpdateDgv(tempTrans);
        }

        private void cbTransTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTransTypes.SelectedIndex == -1)
                return;
            try
            {
                string type = cbTransTypes.Text;
                switch (type)
                {
                    case "Both":
                        UpdateDgv(tempTrans);
                        break;

                    case "Debit":
                        UpdateDgv(tempTrans.Where(a => a.AccountTransactionType == AccountTransactionType.Debit).ToList());
                        break;

                    case "Credit":
                        UpdateDgv(tempTrans.Where(a => a.AccountTransactionType == AccountTransactionType.Credit).ToList());
                        break;
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
            
        }
    }
}
