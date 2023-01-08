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
    public partial class DeductZakatForm : Form
    {
        private List<GeneralAccount> capitalAccounts = null;
        private GeneralAccount capitalAccount = null;
        private AppSettings appSett = Helper.AppSet;
        public bool IsDone { get; set; }
        public DeductZakatForm()
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
                if(capitalAccounts != null)
                {
                    capitalAccounts.Clear();
                    capitalAccounts = null;
                }
                using (Context db = new Context())
                {
                    capitalAccounts = db.Accounts.OfType<GeneralAccount>()
                        .Where(a => a.SubHeadAccountId == Properties.Resources.CapitalAccountSubHead)
                        .ToList();
                    foreach (var item in capitalAccounts)
                    {
                       
                        var trans = db.AccountTransactions.OrderByDescending(a => a.Id)
                            .FirstOrDefault(a => a.GeneralAccountId == item.Id);
                        if(trans == null)
                        {
                            item.Balance = 0;
                        }
                        else
                        {
                            item.Balance = trans.Balance;
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
                Gujjar.TB4(pMain);
                Gujjar.NumbersOnly(tbZakatAmount);
                Gujjar.TBOptional(tbDescription);
                tbDescription.Text = "N/A";

                WaitForm wait = new WaitForm(LoadCapitalAccounts);
                wait.ShowDialog();

                BindCbCapitals();
                IsDone = false;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void BindCbCapitals()
        {
            cbCapitalAccounts.DataSource = capitalAccounts;
            cbCapitalAccounts.DisplayMember = "Title";
            cbCapitalAccounts.ValueMember = "Id";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbCapitalAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbCapitalAccounts.SelectedIndex == -1)
            {
                capitalAccount = null;
                tbBalance.Text = "0.0";
                return;
            }
            capitalAccount = cbCapitalAccounts.SelectedItem as GeneralAccount;
            tbBalance.Text = capitalAccount.Balance.ToString("n2");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }
                if(!Helper.ConfirmAdminPassword())
                {
                    return;
                }
                if(capitalAccount == null)
                {
                    throw new Exception("Please add/select a capital account");
                }
                decimal amount = tbZakatAmount.Text.ToDecimal();
                string description = tbDescription.Text;

                string confMsg = string.Format("Please confirm following things before to proceed on\nZakat amount to deduct ({0})\nfrom account ({1})\nDescription: {2}\n\nPlease confirm !!",
                    amount.ToString("n2"), capitalAccount.Title, description);
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

                            GeneralAccount debitAccount = capitalAccount;
                            GeneralAccount creditAccount = db.Accounts.Find(Properties.Resources.ZakatPayableAccount) as GeneralAccount;


                            string msg = string.Format("Zakat amount ({0}) is deducted from capital account ({1}) description: {2}",
                                amount.ToString("n2"), capitalAccount.Title, description);
                            
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
                                Balance = amount,
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
