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
    public partial class OpeningBalanceAccountForm : Form
    {
        private AppSettings AppSett = Helper.AppSet;
        private List<AccountTransaction> transactions = null;
        private List<AccountTransaction> tempTrans = null;
        private decimal balance = 0;
        private string obaId = "";
        public bool IsDone = false;
        GeneralAccount openingBalanceEquityAccount = null;
        AppUser appUser = SingleTon.LoginForm.appUser;
        public OpeningBalanceAccountForm()
        {
            InitializeComponent();
            
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
                    openingBalanceEquityAccount = db.Accounts.Find(obaId) as GeneralAccount;
                    transactions = db.AccountTransactions.Where(a => a.GeneralAccountId == obaId)
                        .AsParallel().ToList()
                        .Where(a => a.Date.Date >= AppSett.StartDate && a.Date.Date <= AppSett.EndDate).ToList();
                    

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
                obaId = Properties.Resources.OpeningBalanceEquityAccount;
                WaitForm wait = new WaitForm(LoadTransactions);
                wait.ShowDialog();

                lblHeading.Text = openingBalanceEquityAccount.Title;

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
                if (!Helper.ConfirmAdminPassword())
                    return;

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
                            string message = string.Format("Opening balance equity entry: amount ({0}) is added in business. Added by ({1})", amount, appUser.Name);
                            DayBook obeDaybookEntry = new DayBook
                            {
                                Id = 0,
                                Date = DateTime.Now,
                                Description = message,
                                Amount = amount,
                                CanRollBack = true,
                                InDate = DateTime.Now.Date
                            };

                            obeDaybookEntry = db.DayBooks.Add(obeDaybookEntry);
                            db.SaveChanges();

                            #region "Credit to opening balance account"
                            AccountTransaction obeCreditTrans = new AccountTransaction
                            {
                                Id = 0,
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Credit,
                                Balance = amount,
                                CreditAmount = amount,
                                Date = DateTime.Now,
                                DayBookId = obeDaybookEntry.Id,
                                DebitAmount = 0,
                                Description = message,
                                GeneralAccountId = obaId
                            };

                            var obeDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == obeCreditTrans.GeneralAccountId)
                                .AsParallel().ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                .OrderByDescending(a => a.Id).FirstOrDefault();
                            if (obeDbEntry != null)
                            {
                                obeCreditTrans.Balance += obeDbEntry.Balance;
                            }
                            obeCreditTrans = db.AccountTransactions.Add(obeCreditTrans);
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
                                DayBookId = obeDaybookEntry.Id,
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
                            GeneralAccount obeAccountCredit = db.Accounts.OfType<GeneralAccount>().FirstOrDefault(a => a.Id == obeCreditTrans.GeneralAccountId);
                            GeneralAccount cashAccountDebit = db.Accounts.OfType<GeneralAccount>().FirstOrDefault(a => a.Id == cashDebitTrans.GeneralAccountId);

                            DayBook dbDaybook = db.DayBooks.Find(obeDaybookEntry.Id);
                            dbDaybook.DebitTrace = string.Format("({0}). Trans Id: {1}", cashAccountDebit.Title, cashDebitTrans.Id);
                            dbDaybook.CreditTrace = string.Format("({0}). Trans Id: {1}", obeAccountCredit.Title, obeCreditTrans.Id);
                            dbDaybook.DebitAccountId = cashAccountDebit.Id;
                            dbDaybook.CreditAccountId = obeAccountCredit.Id;

                            db.Entry(dbDaybook).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            trans.Commit();

                            if (AppSett.PrintFinancialTransactions)
                            {
                                Helper.PrintTransactions(new List<DayBook> { dbDaybook });
                            }


                            IsDone = true;
                            Gujjar.InfoMsg("Transaction took placed successfully");
                            #endregion

                            WaitForm wait = new WaitForm(LoadTransactions);
                            wait.ShowDialog();

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
                if(!Helper.ConfirmAdminPassword())
                {
                    return;
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
                            string message = string.Format("Opening balance withdraw: Amount ({0}) withdrawn from ({1}) by user ({1}).", amount.ToString("n2"), openingBalanceEquityAccount.Title, appUser.Name);
                            DayBook obeDaybookEntry = new DayBook
                            {
                                Id = 0,
                                Date = DateTime.Now,
                                Description = message,
                                Amount = amount,
                                CanRollBack = true,
                                InDate = DateTime.Now.Date
                            };

                            obeDaybookEntry = db.DayBooks.Add(obeDaybookEntry);
                            db.SaveChanges();

                            #region "Debit to opening balance account"
                            AccountTransaction obeDebitTrans = new AccountTransaction
                            {
                                Id = 0,
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Debit,
                                Balance = -amount,
                                CreditAmount = 0,
                                Date = DateTime.Now,
                                DayBookId = obeDaybookEntry.Id,
                                DebitAmount = amount,
                                Description = message,
                                GeneralAccountId = obaId
                            };

                            var obeDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == obeDebitTrans.GeneralAccountId)
                                .AsParallel().ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                .OrderByDescending(a => a.Id).FirstOrDefault();
                            if (obeDbEntry != null)
                            {
                                obeDebitTrans.Balance += obeDbEntry.Balance;
                            }
                            obeDebitTrans = db.AccountTransactions.Add(obeDebitTrans);
                            db.SaveChanges();
                            #endregion

                            #region "Credit to cash account"
                            AccountTransaction cashCreditTrans = new AccountTransaction
                            {
                                Id = 0,
                                Balance = -amount,
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Credit,
                                CreditAmount = amount,
                                DebitAmount = 0,
                                Date = DateTime.Now,
                                DayBookId = obeDaybookEntry.Id,
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
                            GeneralAccount obeAccountDebit = db.Accounts.OfType<GeneralAccount>().FirstOrDefault(a => a.Id == obeDebitTrans.GeneralAccountId);
                            GeneralAccount cashAccountCredit = db.Accounts.OfType<GeneralAccount>().FirstOrDefault(a => a.Id == cashCreditTrans.GeneralAccountId);

                            DayBook dbDaybook = db.DayBooks.Find(obeDaybookEntry.Id);
                            dbDaybook.CreditTrace = string.Format("({0}). Trans Id: {1}", cashAccountCredit.Title, cashCreditTrans.Id);
                            dbDaybook.DebitTrace = string.Format("({0}). Trans Id: {1}", obeAccountDebit.Title, obeDebitTrans.Id);
                            dbDaybook.DebitAccountId = obeAccountDebit.Id;
                            dbDaybook.CreditAccountId = cashAccountCredit.Id;

                            db.Entry(dbDaybook).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            trans.Commit();

                            if (AppSett.PrintFinancialTransactions)
                            {
                                Helper.PrintTransactions(new List<DayBook> { dbDaybook });
                            }

                            IsDone = true;
                            Gujjar.InfoMsg("Transaction took placed successfully");
                            #endregion

                            WaitForm wait = new WaitForm(LoadTransactions);
                            wait.ShowDialog();

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

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
