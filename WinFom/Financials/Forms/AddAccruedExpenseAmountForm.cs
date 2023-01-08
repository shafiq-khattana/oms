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

namespace WinFom.Financials.Forms
{
    public partial class AddAccruedExpenseAmountForm : Form
    {
        private AccruedExpenseItem expenseItem = null;
        private int itemId = 0;
        private AppSettings AppSett = Helper.AppSet;
        public bool IsDone { get; set; }
        public AddAccruedExpenseAmountForm(int iId)
        {
            InitializeComponent();
            itemId = iId;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                using (Context db = new Context())
                {
                    expenseItem = db.AccruedExpenseItems.Find(itemId);
                    label1.Text = expenseItem.Title;
                }

                Gujjar.TB4(pMain);
                Gujjar.NumbersOnly(tbAmount);
                Gujjar.TBOptional(tbDescription);
                tbDescription.Text = "N/A";
                
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
            string txt = tbAmount.Text;
            string description = tbDescription.Text;
            try
            {
                if (string.IsNullOrEmpty(txt))
                {
                    throw new Exception("Please enter some amount in textbox");
                }
                if (string.IsNullOrEmpty(description))
                {
                    throw new Exception("Please enter description in textbox");
                }

                DialogResult res = Gujjar.ConfirmYesNo("Are you sured to complete this transaction?");
                if (res == DialogResult.No)
                    return;

                var user = SingleTon.LoginForm.appUser;
                decimal amount = txt.ToDecimal();
                string finMsg = string.Format("Accrued expense for ({0}), amount ({1}), description ({2}), by ({3})", 
                    expenseItem.Title, amount.ToString("n2"), description, user.Name);
                
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        GeneralAccount creditAccount = db.Accounts.Find(expenseItem.CreditAccountId) as GeneralAccount;
                        GeneralAccount debitAccount = db.Accounts.Find(expenseItem.DebitAccountId) as GeneralAccount;

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

                            // Credit payable acount
                            AccountTransaction payableExpenseCreditTrans = new AccountTransaction
                            {
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Credit,
                                Balance = amount,
                                CreditAmount = amount,
                                DebitAmount = 0,
                                Date = DateTime.Now,
                                DayBookId = daybook.Id,
                                Id = 0,
                                Description = finMsg,
                                GeneralAccountId = creditAccount.Id
                            };

                            var payableDbTrans = db.AccountTransactions.Where(a => a.GeneralAccountId == creditAccount.Id).AsParallel()
                                .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                .OrderByDescending(a => a.Id).FirstOrDefault();
                            if (payableDbTrans != null)
                            {
                                payableExpenseCreditTrans.Balance += payableDbTrans.Balance;
                            }
                            payableExpenseCreditTrans = db.AccountTransactions.Add(payableExpenseCreditTrans);
                            db.SaveChanges();


                            // Debit expense account
                            AccountTransaction expenseTrans = new AccountTransaction
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
                                GeneralAccountId = debitAccount.Id
                            };

                            var expenseDebitDb = db.AccountTransactions.Where(a => a.GeneralAccountId == debitAccount.Id)
                                .AsParallel()
                               .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                               .OrderByDescending(a => a.Id).FirstOrDefault();
                            if (expenseDebitDb != null)
                            {
                                expenseTrans.Balance += expenseDebitDb.Balance;
                            }
                            expenseTrans = db.AccountTransactions.Add(expenseTrans);
                            db.SaveChanges();

                            
                            DayBook daybookDb = db.DayBooks.Find(daybook.Id);
                            daybookDb.DebitTrace = string.Format("({0}). Trans Id. {1}", debitAccount.Title, expenseTrans.Id);
                            daybookDb.CreditTrace = string.Format("({0}). Trans Id. {1}", creditAccount.Title, payableExpenseCreditTrans.Id);
                            daybookDb.CreditAccountId = creditAccount.Id;
                            daybookDb.DebitAccountId = debitAccount.Id;

                            db.Entry(daybookDb).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            trans.Commit();
                            IsDone = true;

                            if(AppSett.PrintFinancialTransactions)
                            {
                                Helper.PrintTransactions(new List<DayBook> { daybookDb });
                            }

                            Gujjar.InfoMsg("Transaction is completed successfully");
                            Close();
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
    }
}
