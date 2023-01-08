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
    public partial class PayZakatForm : Form
    {
        private AppSettings appSett = Helper.AppSet;
        private decimal balance = 0;
        GeneralAccount creditAccount = null;
        GeneralAccount debitAccount = null;
        public bool IsDone { get; set; }
        public PayZakatForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadCapitalAccounts()
        {
            try
            {
                using (Context db = new Context())
                {
                    creditAccount = db.Accounts.Find(Properties.Resources.CashInHand) as GeneralAccount;
                    debitAccount = db.Accounts.Find(Properties.Resources.ZakatPayableAccount) as GeneralAccount;
                    balance = 0;
                    var trans = db.AccountTransactions.OrderByDescending(a => a.Id)
                        .FirstOrDefault(a => a.GeneralAccountId == debitAccount.Id);
                    if (trans != null)
                    {
                        balance = trans.Balance;
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
                Gujjar.TB4(pMain);
                Gujjar.NumbersOnly(tbZakatAmount);
                Gujjar.TBOptional(tbDescription);
                tbDescription.Text = "N/A";

                WaitForm wait = new WaitForm(LoadCapitalAccounts);
                wait.ShowDialog();
                tbBalance.Text = balance.ToString("n2");
                IsDone = false;
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
                if (!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }
                if (!Helper.ConfirmAdminPassword())
                {
                    return;
                }
                
                decimal amount = tbZakatAmount.Text.ToDecimal();
                string description = tbDescription.Text;

                if(amount > balance)
                {
                    throw new Exception(string.Format("You entered more amount from account balance. Please enter valid amount"));
                }
                string paidPerson = tbPaidTo.Text;
                string confMsg = string.Format("Please confirm following things before to proceed on\nZakat amount ({0}) is going to paid to ({1})\nfrom account ({2})\nDescription: {3}\n\nPlease confirm !!",
                    amount.ToString("n2"), paidPerson, debitAccount.Title, description);
                DialogResult res = Gujjar.ConfirmYesNo(confMsg);
                if (res == DialogResult.No)
                    return;


                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            #region "Financials"

                            

                            string msg = string.Format("Paid Zakat amount ({0}) to ({1}), description: {2}, dated ({3})",
                                amount.ToString("n2"), paidPerson, description, dtp.Value.ToShortDateString());

                            DayBook daybookEntry = new DayBook
                            {
                                Id = 0,
                                Amount = amount,
                                Date = dtp.Value,
                                Description = msg,
                                CanRollBack = false,
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
                                Balance = -amount,
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
                                Balance = -amount,
                                CreditAmount = amount,
                                Date = DateTime.Now,
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

                            trans.Commit();

                            if (appSett.PrintFinancialTransactions)
                            {
                                Helper.PrintTransactions(new List<DayBook> { daybookdb });
                            }

                            Gujjar.InfoMsg(string.Format(msg));
                            IsDone = true;
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
