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
using Model.RetailBardanaManaged.Model;

namespace WinFom.RetailBardanaManagedUI.Forms
{
    public partial class AddBardanaItemEntry : Form
    {
        RetailBardanaSupplier supplier = null;
        List<RetailBardanaSupplier> supplierList = null;
        RetailBardanaItem bardanaItem = null;
        private GeneralAccount debitAccount = null;
        private GeneralAccount creditAccount = null;
        private int bardanaItemId = 0;
        public bool IsDone = false;
        private AppSettings AppSett = Helper.AppSet;
        public AddBardanaItemEntry(int bardanaId)
        {
            InitializeComponent();
            bardanaItemId = bardanaId;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadSuppliers()
        {
            try
            {
                using (Context db = new Context())
                {
                    if(supplierList != null && supplierList.Count > 0)
                    {
                        supplierList.Clear();
                        supplierList = null;
                    }
                    if(bardanaItem != null)
                    {
                        bardanaItem = null;
                    }
                    bardanaItem = db.RetailBardanaItems.Find(bardanaItemId);
                    supplierList = db.RetailBardanaSuppliers.Where(a => a.IsActive).OrderBy(a => a.Name).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindSuppliers()
        {
            cbSuppliers.DataSource = supplierList;
            cbSuppliers.DisplayMember = "Name";
            cbSuppliers.ValueMember = "Id";
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                Gujjar.TB4(pMain);
                tbRemarks.Text = "N/A";
                Gujjar.TBOptional(tbRemarks);
                Gujjar.NumbersOnly(tbQty);
                Gujjar.NumbersOnly(tbTotalPrice);
                Gujjar.NumbersOnly(tbUnitPrice);

                WaitForm wait = new WaitForm(LoadSuppliers);
                wait.ShowDialog();
                BindSuppliers();
                tbItem.Text = bardanaItem.Title;
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

        private void cbSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbSuppliers.SelectedIndex == -1)
            {
                supplier = null;
                return;
            }
            supplier = cbSuppliers.SelectedItem as RetailBardanaSupplier;
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                AddRetailBardanaSupplier form = new AddRetailBardanaSupplier();
                form.ShowDialog();

                int sid = form.SupplierId;
                if(sid != 0)
                {
                    WaitForm wait = new WaitForm(LoadSuppliers);
                    wait.ShowDialog();
                    BindSuppliers();

                    cbSuppliers.SelectedItem = cbSuppliers.Items.OfType<RetailBardanaSupplier>()
                        .FirstOrDefault(a => a.Id == sid);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        List<DayBook> daybooks = new List<DayBook>();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult conf = Gujjar.ConfirmYesNo("Please confirm, are you sured to add entry");
                if (conf == DialogResult.No)
                    return;

                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }

                if (!Helper.ConfirmUserPassword())
                    return;

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {

                        try
                        {
                            #region "Financials"
                            debitAccount = db.Accounts.Find(bardanaItem.GeneralAccountId) as GeneralAccount;
                            creditAccount = db.Accounts.Find(supplier.GeneralAccountId) as GeneralAccount;

                            var user = SingleTon.LoginForm.appUser;
                            decimal amount = tbTotalPrice.Text.ToDecimal();

                            string finMsg = string.Format("Purchased retail bardana item ({0}), qty ({1}), in amount ({2}) from supplier ({3}) description ({4}). By ({5})",
                                bardanaItem.Title, tbQty.Text, amount.ToString("n1"), supplier.Name, tbRemarks.Text, user.Name);

                            DayBook daybookEntry = new DayBook
                            {
                                Id = 0,
                                Amount = amount,
                                Date = dtp.Value,
                                Description = finMsg,
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
                            daybookdb.DebitTrace = string.Format("({0}). Trans Id: {1}", debitAccount.Title, debitItemTrans.Id);
                            daybookdb.CreditTrace = string.Format("({0}). Trans Id: {1}", creditAccount.Title, creditItemTrans.Id);
                            daybookdb.CreditAccountId = creditAccount.Id;
                            daybookdb.DebitAccountId = debitAccount.Id;

                            db.Entry(daybookdb).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            daybooks.Add(daybookdb);
                            #endregion

                            RetailBardanaItemEntry entry = new RetailBardanaItemEntry
                            {
                                Id = 0,
                                Dated = dtp.Value,
                                QtyEntered = tbQty.Text.ToDecimal(),
                                Remarks = tbRemarks.Text,
                                RetailBardanaItem = null,
                                RetailBardanaItemId = bardanaItem.Id,
                                RetailBardanaSupplier = null,
                                RetailBardanaSupplierId = supplier.Id,
                                TotalPrice = tbTotalPrice.Text.ToDecimal(),
                                UnitPrice = tbUnitPrice.Text.ToDecimal(),
                                DayBookId = daybookdb.Id
                            };

                            db.RetailBardanaItemEntries.Add(entry);
                            db.SaveChanges();

                            var itemDb = db.RetailBardanaItems.Find(bardanaItemId);
                            itemDb.SKU += entry.QtyEntered;
                            decimal prevUnit = itemDb.UnitPrice;
                            decimal newUnit = entry.UnitPrice;
                            decimal affunit = newUnit;
                            if (prevUnit > 0)
                                affunit = (prevUnit + newUnit) / 2;
                            itemDb.UnitPrice = affunit;

                            db.Entry(itemDb).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            trans.Commit();
                        }
                        catch (Exception exp2)
                        {
                            trans.Rollback();
                            throw exp2;
                        }

                        
                        if (AppSett.PrintFinancialTransactions)
                        {
                            Helper.PrintTransactions(daybooks);
                        }
                        Gujjar.InfoMsg("Retail bardana item entry is added in database");
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
        private void tb_leave(object sender, EventArgs e)
        {
            try
            {
                string qtyStr = tbQty.Text, unitStr = tbUnitPrice.Text, totalStr = tbTotalPrice.Text;

                
                decimal qty = 0;
                decimal unit = 0;
                decimal total = 0;

                TextBox tb = sender as TextBox;
                switch(tb.Name)
                {
                    case "tbQty":
                    case "tbUnitPrice":
                        if (string.IsNullOrEmpty(qtyStr) || string.IsNullOrEmpty(unitStr))
                            return;
                        qty = qtyStr.ToDecimal();
                        unit = unitStr.ToDecimal();
                        total = qty * unit;
                        tbTotalPrice.Text = total.ToString("n2");
                        break;
                    case "tbTotalPrice":
                        if (string.IsNullOrEmpty(qtyStr) || string.IsNullOrEmpty(totalStr))
                            return;

                        total = totalStr.ToDecimal();
                        qty = qtyStr.ToDecimal();
                        unit = 0;
                        if(qty > 0)
                        {
                            unit = total / qty;
                        }
                        tbUnitPrice.Text = unit.ToString("n2");
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
