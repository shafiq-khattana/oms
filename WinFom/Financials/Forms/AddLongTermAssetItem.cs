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
    public partial class AddLongTermAssetItem : Form
    {
        private AppUser appUser = SingleTon.LoginForm.appUser;
        private AppSettings appSett = Helper.AppSet;
        public bool IsDone { get; set; }
        public AddLongTermAssetItem()
        {
            InitializeComponent();
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
                Gujjar.NumbersOnly(tbPrice);
                tbDescription.Text = "N/A";
                Gujjar.TBOptional(tbDescription);
                dtp.MinDate = appSett.StartDate;
                dtp.MaxDate = appSett.EndDate;
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

                string itemName = tbItemName.Text;
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var dbObj = db.LongTermAssetItems.ToList().FirstOrDefault(a => a.Title.ToLower().Equals(itemName.ToLower()));
                            if(dbObj != null)
                            {
                                throw new Exception("Long term asset item already added in database");
                            }

                            string acctTitle = string.Format("Long term asset ({0}) account", itemName);
                            GeneralAccount itemAccount = new GeneralAccount
                            {
                                Id = Guid.NewGuid().ToString(),
                                Title = acctTitle,
                                AccountNature = AccountNature.Debit,
                                AccountNo = "N/A",
                                Description = acctTitle,
                                SubHeadAccountId = Properties.Resources.LongTermAssetsSubHead
                            };
                            db.Accounts.Add(itemAccount);
                            db.SaveChanges();

                            decimal price = tbPrice.Text.ToDecimal();
                            LongTermAssetItem assetItem = new LongTermAssetItem
                            {
                                Id = 0,
                                Account = null,
                                GeneralAccountId = itemAccount.Id,
                                CurrentPrice = price,
                                DateAdded = dtp.Value,
                                Description = tbDescription.Text,
                                InitialPrice = price,
                                Title = itemName
                            };

                            db.LongTermAssetItems.Add(assetItem);
                            db.SaveChanges();


                            #region "Financials"

                            GeneralAccount debitAccount = db.Accounts.Find(assetItem.GeneralAccountId) as GeneralAccount;
                            GeneralAccount creditAccount = db.Accounts.Find(Properties.Resources.CashInHand) as GeneralAccount;

                            decimal amount = price;
                            string msg = string.Format("Purchased long term asset item ({0}) in amount ({1}), description ({2}), by ({3})",
                                itemName, price.ToString("n2"), tbDescription.Text, appUser.Name);

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
                                Balance = amount,
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

                            db.SaveChanges();
                            trans.Commit();

                            if(appSett.PrintFinancialTransactions)
                            {
                                Helper.PrintTransactions(new List<DayBook> { daybookdb });
                            }
                            Gujjar.InfoMsg("Long term item is introduced in business successfully and record is stored in database successfully");
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
