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
using Model.ReadyStuff.Model;
using WinFom.Common.Forms;
using Model.Admin.Model;
using WinFom.Common.Model;
using Model.Financials.Model;
using Model.Repair.Model;
using WinFom.Financials.Forms;
using Model.Financials.ViewModel;
using WinFom.Properties;

namespace WinFom.RepairUI.Forms
{
    public partial class RepItemConsumptionForm : Form
    {
        public bool IsDone { get; set; }
        private List<GeneralAccount> accountsList = null;
        private GeneralAccount creditAccount = null;
        private GeneralAccount debitAccount = null;
        private RepItem repItem = null;
        private int repItemId = 0;
        private AppUser appUser = SingleTon.LoginForm.appUser;
        private AppSettings appSett = Helper.AppSet;
        public RepItemConsumptionForm(int itemId)
        {
            InitializeComponent();
            repItemId = itemId;
        }

        private void LoadAccounts()
        {
            try
            {
                if (accountsList != null)
                {
                    accountsList.Clear();
                    accountsList = null;
                }
                using (Context db = new Context())
                {
                    accountsList = db.Accounts.OfType<GeneralAccount>()
                        .OrderBy(a => a.Title)
                        .ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void BindAccounts()
        {
            cbCreditAccounts.DataSource = accountsList;
            cbCreditAccounts.DisplayMember = "Title";
            cbCreditAccounts.ValueMember = "Id";
        }
        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                IsDone = false;
                Gujjar.TB4(pMain);
                Gujjar.TBOptional(tbRemarks);
                tbRemarks.Text = "N/A";
                Gujjar.NumbersOnly(tbQtyConsumption);

                using (Context db = new Context())
                {
                    creditAccount = db.Accounts.Find(Resources.ItemsPurchasedAccount) as GeneralAccount;
                    repItem = db.RepItems.Find(repItemId);
                    repItem.Location = db.Locations.Find(repItem.LocationId);
                }

                WaitForm wait = new WaitForm(LoadAccounts);
                wait.ShowDialog();
                BindAccounts();

                cbCreditAccounts.SelectedItem = cbCreditAccounts.Items.OfType<GeneralAccount>()
                    .FirstOrDefault(a => a.Id ==  Resources.InventoryItemsConsumptionExpenseAccount);

                cbCreditAccounts.Enabled = bswChangeAccount.Value = btnSearchAccount.Enabled = false;

                tbItem.Text = string.Format("{0}-{1}", repItem.Name, repItem.Location.Name);
                tbSKU.Text = repItem.SKU.ToString("n1");
                tbSKUUnitPrice.Text = repItem.UnitValue.ToString("n1");
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

        private void bswChangeAccount_Click(object sender, EventArgs e)
        {
            cbCreditAccounts.Enabled = btnSearchAccount.Enabled = bswChangeAccount.Value;
            if(!bswChangeAccount.Value)
            {
                cbCreditAccounts.SelectedItem = cbCreditAccounts.Items.OfType<GeneralAccount>()
                   .FirstOrDefault(a => a.Id ==  Resources.InventoryItemsConsumptionExpenseAccount);
            }
        }

        private void cbCreditAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCreditAccounts.SelectedIndex == -1)
            {
                debitAccount = null;
                return;
            }
            debitAccount = cbCreditAccounts.SelectedItem as GeneralAccount;
        }

        private void tbQtyConsumption_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal qtyCons = 0;
                if (!string.IsNullOrEmpty(tbQtyConsumption.Text))
                {
                    qtyCons = tbQtyConsumption.Text.ToDecimal();
                }

                decimal total = qtyCons * repItem.UnitValue;
                tbConsumptionPrice.Text = total.ToString("n1");
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
                if (!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }
                if (debitAccount == null)
                {
                    throw new Exception("Please choose a debit account");
                }
                decimal amount = tbConsumptionPrice.Text.ToDecimal();
                if (amount <= 0)
                {
                    throw new Exception("Invalid amount");
                }

                decimal sku = tbSKU.Text.ToDecimal();
                decimal qty = tbQtyConsumption.Text.ToDecimal();

                if (qty > sku)
                {
                    throw new Exception("Quantity is greater than stock quantity");
                }
                if (!Helper.ConfirmUserPassword())
                {
                    return;
                }
                DialogResult rest = Gujjar.ConfirmYesNo("Please confirm before to proceed on. All information is correct?");
                if (rest == DialogResult.No)
                    return;



                string msg = string.Format("Inventory item ({0}) consumption entry. Qty ({1}), price value ({2}) is consumed, dated ({3}), remarks ({4}), by ({5})",
                    tbItem.Text, tbQtyConsumption.Text, tbConsumptionPrice.Text, dtp.Value.ToShortDateString(), tbRemarks.Text, appUser.Id);

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            #region "Financials"                           

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


                            RepItemConsumptionRecord record = new RepItemConsumptionRecord
                            {
                                AccountId = debitAccount.Id,
                                Id = 0,
                                Dated = dtp.Value,
                                DaybookId = daybookdb.Id,
                                Item = null,
                                Price = tbConsumptionPrice.Text.ToDecimal(),
                                QtyConsumed = tbQtyConsumption.Text.ToDecimal(),
                                Remarks = tbRemarks.Text,
                                RepItemId = repItemId
                            };

                            db.RepItemConsumptionRecords.Add(record);
                            db.SaveChanges();

                            var itemDb = db.RepItems.Find(repItemId);
                            itemDb.SKU -= record.QtyConsumed;

                            if (itemDb.SKU == 0)
                            {
                                itemDb.UnitValue = 0;
                                itemDb.TotalValue = 0;
                            }
                            else
                            {
                                itemDb.TotalValue = itemDb.SKU * itemDb.UnitValue;
                            }

                            db.Entry(itemDb).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            trans.Commit();
                            if (appSett.PrintFinancialTransactions)
                            {
                                Helper.PrintTransactions(new List<DayBook> { daybookdb });
                            }
                            Gujjar.InfoMsg("Consumption record is added in database successfully");
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

        private void btnSearchAccount_Click(object sender, EventArgs e)
        {
            var objs = new List<AccountSearchVM>();
            accountsList.ForEach(g =>
            {
                AccountSearchVM vm = new AccountSearchVM
                {
                    Id = g.Id,
                    Title = g.Title
                };
                objs.Add(vm);
            });
            SearchAccountForm form = new SearchAccountForm(objs);
            form.ShowDialog();
            string id = form.AccountId;
            if (!string.IsNullOrEmpty(id))
            {
                cbCreditAccounts.SelectedItem = cbCreditAccounts.Items.OfType<GeneralAccount>()
                    .FirstOrDefault(a => a.Id == id);
            }
        }
    }
}
