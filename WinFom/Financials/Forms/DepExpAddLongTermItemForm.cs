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
using Model.Employees.Model;

namespace WinFom.Financials.Forms
{

    public partial class DepExpAddLongTermItemForm : Form
    {
        private AppSettings AppSett = Helper.AppSet;
        public bool IsDone = false;
        private string creditAccountId = "";
        private GeneralAccount creditAccount = null;
        LongTermAssetItem assetItem = null;
        int assetItemId = 0;
        public DepExpAddLongTermItemForm(string acctId, int itemId)
        {
            InitializeComponent();
            creditAccountId = acctId;
            assetItemId = itemId;
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
                Gujjar.NumbersOnly(tbExpenseAmount);

                Gujjar.TBOptional(tbRemarks);
                using (Context db = new Context())
                {
                    creditAccount = db.Accounts.Find(creditAccountId) as GeneralAccount;
                    assetItem = db.LongTermAssetItems.Find(assetItemId);
                }

                tbEmployee.Text = creditAccount.Title;
                lblHeading.Text = creditAccount.Title;

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


                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        if (!Gujjar.IsValidForm(pMain))
                        {
                            throw new Exception("Please fill text fields");
                        }
                        GeneralAccount debitAccount = db.Accounts.Find(Properties.Resources.LongTermAssetsExpensesAccount) as GeneralAccount;

                        
                        var user = SingleTon.LoginForm.appUser;
                        decimal amount = tbExpenseAmount.Text.ToDecimal();

                        string finMsg = string.Format("Depreciation amount ({0}) is added of item ({1}), remarks ({2}), by ({3})", amount.ToString("n2"), assetItem.Title, tbRemarks.Text, user.Name);



                        #region "Financials"
                        DayBook daybookEntry = new DayBook
                        {
                            Id = 0,
                            Amount = amount,
                            Date = dtp.Value,
                            Description = finMsg,
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
                            Balance = amount,
                            CreditAmount = 0,
                            Date = DateTime.Now,
                            DayBookId = daybookEntry.Id,
                            DebitAmount = amount,
                            GeneralAccountId = debitAccount.Id,
                            Description = finMsg
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
                            Balance = -amount,
                            CreditAmount = amount,
                            Date = DateTime.Now,
                            DayBookId = daybookEntry.Id,
                            DebitAmount = 0,
                            GeneralAccountId = creditAccount.Id,
                            Description = finMsg
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
                        daybookdb.DebitTrace = string.Format("({0}). Trans Id: {1}", this.creditAccount.Title, debitItemTrans.Id);
                        daybookdb.CreditTrace = string.Format("({0}). Trans Id: {1}", creditAccount.Title, creditItemTrans.Id);
                        daybookdb.CreditAccountId = creditAccount.Id;
                        daybookdb.DebitAccountId = this.creditAccount.Id;

                        db.Entry(daybookdb).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        #endregion



                        trans.Commit();

                        if (AppSett.PrintFinancialTransactions)
                        {
                            Helper.PrintTransactions(new List<DayBook> { daybookdb });
                        }

                        Gujjar.InfoMsg("Depreciation is added and record is stored in database successfully");
                        IsDone = true;
                        Close();


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
