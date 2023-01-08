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
    public partial class AccruedExpensesForm : Form
    {
        private string dgvbtnAddExpense = "btnwit23443hdraw1231";
        private string dgvbtnpayliability = "btndepo2343sit1231";
        private string dgvbtnexpenseledger = "btndgvtrans23414";
        //private string dgvbtnpayableledger = "btndgvpayable234234";
        private List<AccruedExpenseItem> accruedItems = null;
        //private List<AccruedExpenseItemVM> vmList = null;
        private AppSettings AppSett = Helper.AppSet;
        public AccruedExpensesForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadAccruedItems()
        {
            try
            {
                if (accruedItems != null && accruedItems.Count > 0)
                {
                    accruedItems.Clear();
                    accruedItems = null;
                }

                if (dgv.InvokeRequired)
                {
                    dgv.Invoke(new Action(() =>
                    {
                        accruedExpenseItemVMBindingSource.Clear();
                    }));
                }
                else
                {
                    accruedExpenseItemVMBindingSource.List.Clear();
                }

                using (Context db = new Context())
                {
                    accruedItems = db.AccruedExpenseItems.ToList();
                    foreach (var item in accruedItems)
                    {
                        decimal creditBalance = 0;

                        var obj2 = db.AccountTransactions.Where(a => a.GeneralAccountId == item.CreditAccountId)
                            .OrderByDescending(a => a.Id).FirstOrDefault();
                        if (obj2 != null)
                        {
                            creditBalance = obj2.Balance;
                        }

                        AccruedExpenseItemVM vm = new AccruedExpenseItemVM
                        {
                            Id = item.Id,
                            Title = item.Title,
                            Balance = creditBalance
                        };

                        if (dgv.InvokeRequired)
                        {
                            dgv.Invoke(new Action(() =>
                            {
                                accruedExpenseItemVMBindingSource.List.Add(vm);
                            }));
                        }
                        else
                        {
                            accruedExpenseItemVMBindingSource.List.Add(vm);
                        }
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
                Gujjar.AddDatagridviewButton(dgv, dgvbtnAddExpense, "Add Expense", "Add Expense", 120);
                Gujjar.AddDatagridviewButton(dgv, dgvbtnpayliability, "Pay Liability", "Pay Liability", 120);
                Gujjar.AddDatagridviewButton(dgv, dgvbtnexpenseledger, "Ledger", "Ledger", 100);

                WaitForm wait1 = new WaitForm(LoadAccruedItems);
                wait1.ShowDialog();



            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AddAccruedExpenseForm form = new AddAccruedExpenseForm();
                form.ShowDialog();

                if (form.IsDone)
                {
                    WaitForm wait = new WaitForm(LoadAccruedItems);
                    wait.ShowDialog();
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

                if (ri == -1 || ri == dgv.NewRowIndex)
                {
                    return;
                }
                #region "Cash Deposit in bank"
                if (dgv.Columns[dgvbtnpayliability].Index == ci)
                {
                    if (Helper.ConfirmAdminPassword())
                    {
                        int accountId = dgv.Rows[ri].Cells[0].Value.ToString().ToInt();
                        AccruedExpenseItem expItem = null;
                        using (Context db = new Context())
                        {
                            expItem = db.AccruedExpenseItems.Find(accountId);
                        }
                        PayAccruedExpenseForm form = new PayAccruedExpenseForm(expItem.CreditAccountId);
                        form.ShowDialog();
                        bool res = form.IsDone;
                        if (res)
                        {
                            WaitForm wait = new WaitForm(LoadAccruedItems);
                            wait.ShowDialog();
                        }

                    }


                }
                #endregion

                #region "Cash withdraw from bank"
                if (dgv.Columns[dgvbtnAddExpense].Index == ci)
                {
                    if (Helper.ConfirmAdminPassword())
                    {
                        int itemId = dgv.Rows[ri].Cells[0].Value.ToString().ToInt();
                        AddAccruedExpenseAmountForm form = new AddAccruedExpenseAmountForm(itemId);
                        form.ShowDialog();
                        if (form.IsDone)
                        {
                            WaitForm wait = new WaitForm(LoadAccruedItems);
                            wait.ShowDialog();
                        }

                    }


                }
                #endregion

                #region "Bank Transactions"
                if (dgv.Columns[dgvbtnexpenseledger].Index == ci)
                {
                    int accountId = dgv.Rows[ri].Cells[0].Value.ToString().ToInt();
                    AccruedExpenseItem item = null;
                    using (Context db = new Context())
                    {
                        item = db.AccruedExpenseItems.Find(accountId);
                    }
                    BankAccountTransactionsForm form = new BankAccountTransactionsForm(item.CreditAccountId);
                    form.ShowDialog();
                }
                #endregion
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void DoBankWithdrawTrans(GeneralAccount bankAccount, BankTransactionTransferVM transObj)
        {
            try
            {
                var user = SingleTon.LoginForm.appUser;
                string finMsg = string.Format("Bank withdraw amount ({0}) to cash account. Description ({1}). Withdrawn by ({2})", transObj.Amount, transObj.Description, user.Name);
                decimal amount = transObj.Amount;
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            DayBook daybook = new DayBook
                            {
                                DebitTrace = "",
                                Amount = amount,
                                Date = DateTime.Now,
                                Description = finMsg,
                                CanRollBack = true,
                                InDate = DateTime.Now.Date
                            };
                            daybook = db.DayBooks.Add(daybook);
                            db.SaveChanges();

                            // Credit bank acount
                            AccountTransaction bankCreditTrans = new AccountTransaction
                            {
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Credit,
                                Balance = -amount,
                                CreditAmount = amount,
                                DebitAmount = 0,
                                Date = DateTime.Now,
                                DayBookId = daybook.Id,
                                Id = 0,
                                Description = finMsg,
                                GeneralAccountId = bankAccount.Id
                            };

                            var bankDbTrans = db.AccountTransactions.Where(a => a.GeneralAccountId == bankAccount.Id).AsParallel()
                                .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                .OrderByDescending(a => a.Id).FirstOrDefault();
                            if (bankDbTrans != null)
                            {
                                bankCreditTrans.Balance += bankDbTrans.Balance;
                            }
                            bankCreditTrans = db.AccountTransactions.Add(bankCreditTrans);
                            db.SaveChanges();


                            // Cash credit account
                            AccountTransaction cashDebitTrans = new AccountTransaction
                            {
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Debit,
                                Balance = amount,
                                CreditAmount = 0,
                                Date = DateTime.Now,
                                DayBookId = daybook.Id,
                                Id = 0,
                                DebitAmount = amount,
                                Description = finMsg,
                                GeneralAccountId = Properties.Resources.CashInHand
                            };

                            var cashDbTrans = db.AccountTransactions.Where(a => a.GeneralAccountId == Properties.Resources.CashInHand).AsParallel()
                               .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                               .OrderByDescending(a => a.Id).FirstOrDefault();
                            if (cashDbTrans != null)
                            {
                                cashDebitTrans.Balance += cashDbTrans.Balance;
                            }
                            cashDebitTrans = db.AccountTransactions.Add(cashDebitTrans);
                            db.SaveChanges();

                            GeneralAccount cashAccount = db.Accounts.Find(Properties.Resources.CashInHand) as GeneralAccount;

                            DayBook daybookDb = db.DayBooks.Find(daybook.Id);
                            daybookDb.DebitTrace = string.Format("({0}). Trans Id. {1}", cashAccount.Title, cashDebitTrans.Id);
                            daybookDb.CreditTrace = string.Format("({0}). Trans Id. {1}", bankAccount.Title, bankCreditTrans.Id);
                            daybookDb.CreditAccountId = bankAccount.Id;
                            daybookDb.DebitAccountId = cashAccount.Id;

                            db.Entry(daybookDb).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            trans.Commit();
                            if(AppSett.PrintFinancialTransactions)
                            {
                                Helper.PrintTransactions(new List<DayBook> { daybookDb });
                            }
                            Gujjar.InfoMsg("Transaction is completed successfully");
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
                throw exp;
            }
        }

        private void DoBankDepositTrans(GeneralAccount bankAccount, BankTransactionTransferVM transObj)
        {
            try
            {
                var user = SingleTon.LoginForm.appUser;
                string finMsg = string.Format("Bank deposit amount ({0}) from cash account. Description ({1}). Deposit by ({2})", transObj.Amount, transObj.Description, user.Name);
                decimal amount = transObj.Amount;
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            DayBook daybook = new DayBook
                            {
                                DebitTrace = "",
                                Amount = amount,
                                Date = DateTime.Now,
                                Description = finMsg,
                                CanRollBack = true,
                                InDate = DateTime.Now.Date
                            };
                            daybook = db.DayBooks.Add(daybook);
                            db.SaveChanges();

                            // Debit bank acount
                            AccountTransaction bankDebitTrans = new AccountTransaction
                            {
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Debit,
                                Balance = amount,
                                CreditAmount = 0,
                                DebitAmount = amount,
                                Date = DateTime.Now,
                                DayBookId = daybook.Id,
                                Id = 0,
                                Description = finMsg,
                                GeneralAccountId = bankAccount.Id
                            };

                            var bankDbTrans = db.AccountTransactions.Where(a => a.GeneralAccountId == bankAccount.Id).AsParallel()
                                .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                .OrderByDescending(a => a.Id).FirstOrDefault();
                            if (bankDbTrans != null)
                            {
                                bankDebitTrans.Balance += bankDbTrans.Balance;
                            }
                            bankDebitTrans = db.AccountTransactions.Add(bankDebitTrans);
                            db.SaveChanges();


                            // Cash credit account
                            AccountTransaction cashCreditTrans = new AccountTransaction
                            {
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Credit,
                                Balance = -amount,
                                CreditAmount = amount,
                                Date = DateTime.Now,
                                DayBookId = daybook.Id,
                                Id = 0,
                                DebitAmount = 0,
                                Description = finMsg,
                                GeneralAccountId = Properties.Resources.CashInHand
                            };

                            var cashDbTrans = db.AccountTransactions.Where(a => a.GeneralAccountId == Properties.Resources.CashInHand).AsParallel()
                               .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                               .OrderByDescending(a => a.Id).FirstOrDefault();
                            if (cashDbTrans != null)
                            {
                                cashCreditTrans.Balance += cashDbTrans.Balance;
                            }
                            cashCreditTrans = db.AccountTransactions.Add(cashCreditTrans);
                            db.SaveChanges();

                            GeneralAccount cashAccount = db.Accounts.Find(Properties.Resources.CashInHand) as GeneralAccount;

                            DayBook daybookDb = db.DayBooks.Find(daybook.Id);
                            daybookDb.CreditTrace = string.Format("({0}). Trans Id. {1}", cashAccount.Title, cashCreditTrans.Id);
                            daybookDb.DebitTrace = string.Format("({0}). Trans Id. {1}", bankAccount.Title, bankDebitTrans.Id);
                            daybookDb.DebitAccountId = bankAccount.Id;
                            daybookDb.CreditAccountId = cashAccount.Id;

                            db.Entry(daybookDb).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            trans.Commit();
                            if(AppSett.PrintFinancialTransactions)
                            {
                                Helper.PrintTransactions(new List<DayBook> { daybookDb });
                            }
                            Gujjar.InfoMsg("Transaction is completed successfully");
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
                throw exp;
            }
        }
    }
}
