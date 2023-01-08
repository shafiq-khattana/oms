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
using Model.Repair.Model;
using Model.Repair.ViewModel;
using Model.Financials.Model;

namespace WinFom.RepairUI.Forms
{
    public partial class AddPurchasingInvoiceForm : Form
    {
        private string btndgvdelete = "btndgvdelete";
        public bool IsDone = false;
        private ItemSupplier supplier = null;
        private List<ItemSupplier> suppliers = null;
        private int purchaseId = 0;
        private AppSettings appSett = Helper.AppSet;
        public AddPurchasingInvoiceForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadSuppliers()
        {
            try
            {
                if (suppliers != null)
                {
                    suppliers.Clear();
                    suppliers = null;
                }
                using (Context db = new Context())
                {
                    suppliers = db.ItemSuppliers.OrderBy(a => a.Name).ToList();
                    var pobj = db.PurchaseInvoiceRecords.OrderByDescending(a => a.Id).FirstOrDefault();
                    if (pobj == null)
                    {
                        purchaseId = 1;
                    }
                    else
                    {
                        purchaseId = pobj.Id + 1;
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindCbSuppliers()
        {
            cbSuppliers.DataSource = suppliers;
            cbSuppliers.DisplayMember = "Name";
            cbSuppliers.ValueMember = "Id";
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait = new WaitForm(LoadSuppliers);
                wait.ShowDialog();
                BindCbSuppliers();
                Helper.IsOkApplied();
                tbPurchaseId.Text = purchaseId.ToString();
                tbRemarks.Text = "N/A";
                Gujjar.TBOptional(tbRemarks);
                Gujjar.AddDatagridviewButton(dgv, btndgvdelete, "Delete", "Delete", 80);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                AddItemSupplierForm form = new AddItemSupplierForm();
                form.ShowDialog();

                int id = form.SupplierId;
                if (id != 0)
                {
                    WaitForm wait = new WaitForm(LoadSuppliers);
                    wait.ShowDialog();

                    BindCbSuppliers();

                    cbSuppliers.SelectedItem = cbSuppliers.Items.OfType<ItemSupplier>().FirstOrDefault(a => a.Id == id);
                }
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
                AddPurchasingItemEntryForm form = new AddPurchasingItemEntryForm();
                form.ShowDialog();

                if (form.PurchaseVM != null)
                {
                    var vm = form.PurchaseVM;
                    var items = itemPurchaseEntryVMBindingSource.List.OfType<ItemPurchaseEntryVM>().ToList();
                    var obj = items.FirstOrDefault(a => a.Id == vm.Id);
                    if (obj != null)
                    {
                        obj.Qty += vm.Qty;
                        obj.UnitPrice = (vm.UnitPrice + obj.UnitPrice) / 2;
                        obj.TotalPrice = obj.Qty * obj.UnitPrice;
                    }
                    else
                    {
                        itemPurchaseEntryVMBindingSource.List.Add(vm);
                    }
                    dgv.Refresh();

                    UpdateDgv();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void UpdateDgv()
        {
            try
            {
                var list = itemPurchaseEntryVMBindingSource.List.OfType<ItemPurchaseEntryVM>().ToList();
                tbTotalItems.Text = list.Sum(a => a.Qty).ToString("n1");
                tbTotalPrice.Text = list.Sum(a => a.TotalPrice).ToString("n1");
                tbUniqueItems.Text = list.Count.ToString();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void cbSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSuppliers.SelectedIndex == -1)
                return;

            supplier = cbSuppliers.SelectedItem as ItemSupplier;
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                int ci = e.ColumnIndex;

                if (ri == -1 || ri == dgv.NewRowIndex)
                    return;

                if (dgv.Columns[btndgvdelete].Index == ci)
                {
                    DialogResult res = Gujjar.ConfirmYesNo("Are you sured to delete this entry");
                    if (res == DialogResult.No)
                        return;

                    int id = dgv.Rows[ri].Cells[0].Value.ToInt();

                    var obj = itemPurchaseEntryVMBindingSource.List.OfType<ItemPurchaseEntryVM>().FirstOrDefault(a => a.Id == id);
                    itemPurchaseEntryVMBindingSource.List.Remove(obj);
                    dgv.Refresh();
                    UpdateDgv();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddPurchase_Click(object sender, EventArgs e)
        {
            try
            {
                var list = itemPurchaseEntryVMBindingSource.List.OfType<ItemPurchaseEntryVM>().ToList();
                if (list.Count == 0)
                {
                    throw new Exception("Please all atleast one purchasing entry in list");
                }
                if (!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }
                if (string.IsNullOrEmpty(tbBillId.Text))
                {
                    tbBillId.BackColor = Color.Pink;
                    tbBillId.Focus();
                    throw new Exception("Please add bill no");
                }
                if (supplier == null)
                {
                    throw new Exception("Please select supplier from the list");
                }

                if (!Helper.ConfirmUserPassword())
                    return;

                DialogResult res = Gujjar.ConfirmYesNo("Are you confirm please about adding purchasing entry?");
                if (res == DialogResult.No)
                    return;

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            decimal amount = tbTotalPrice.Text.ToDecimal();
                            string msg = string.Format("Purchase entry: Total Items ({0}) in amount ({1}) from supplier/vendor ({2}) dated ({3}) bill no ({4})",
                                tbTotalItems.Text, amount.ToString("n2"), supplier.Name, dtp.Value.ToShortDateString(), tbBillId.Text);

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

                            PurchaseInvoiceRecord record = new PurchaseInvoiceRecord
                            {
                                BillId = tbBillId.Text,
                                Id = 0,
                                InDate = DateTime.Now,
                                ItemPurchaseEntries = null,
                                ItemSupplierId = supplier.Id,
                                PurchaseDate = dtp.Value.Date,
                                Supplier = null,
                                TotalItems = tbTotalItems.Text.ToDecimal(),
                                TotalPrice = tbTotalPrice.Text.ToDecimal(),
                                Remarks = tbRemarks.Text,
                                Unum = Helper.Unum,
                                DaybookId = daybookEntry.Id
                            };
                            record = db.PurchaseInvoiceRecords.Add(record);
                            db.SaveChanges();

                            foreach (var item in list)
                            {
                                ItemPurchaseEntry entry = new ItemPurchaseEntry
                                {
                                    Id = 0,
                                    PurchaseInvoiceRecord = null,
                                    PurchaseInvoiceRecordId = record.Id,
                                    Qty = item.Qty,
                                    RepItem = null,
                                    RepItemId = item.Id,
                                    TotalPrice = item.TotalPrice,
                                    UnitPrice = item.UnitPrice,
                                    Remarks = item.Remarks
                                };
                                db.ItemPurchaseEntries.Add(entry);

                                var dbItem = db.RepItems.Find(item.Id);
                                dbItem.SKU = item.Qty;

                                if (dbItem.UnitValue > 0)
                                {
                                    dbItem.UnitValue = (dbItem.UnitValue + item.UnitPrice) / 2;
                                }
                                else
                                {
                                    dbItem.UnitValue = item.UnitPrice;
                                }
                                dbItem.TotalValue = dbItem.UnitValue * dbItem.SKU;
                                dbItem.SACount += item.Qty;
                                db.Entry(dbItem).State = System.Data.Entity.EntityState.Modified;
                            }
                            db.SaveChanges();

                            #region "Financials"

                            GeneralAccount debitAccount = db.Accounts.Find(Properties.Resources.ItemsPurchasedAccount) as GeneralAccount;
                            GeneralAccount creditAccount = db.Accounts.Find(supplier.GeneralAccountId) as GeneralAccount;

                           

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

                            Gujjar.InfoMsg(string.Format("Purchase entry ({0}) added in database successfully", record.Id));
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
