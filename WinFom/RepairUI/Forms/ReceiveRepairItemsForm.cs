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
    public partial class ReceiveRepairItemsForm : Form
    {
        private int dispatchId = 0;
        private RepairDispatchRecord dispatchRecord = null;
        private List<RepEntry> allEntries = null;
        private List<ItemQty> itemQtys = null;
        private AppSettings appSett = Helper.AppSet;
        public bool IsDone { get; set; }
        public ReceiveRepairItemsForm(int dispatchRecordId)
        {
            InitializeComponent();
            dispatchId = dispatchRecordId;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                Gujjar.TB4(panel1);
                Gujjar.NumbersOnly(tbBillAmount);
                WaitForm wait = new WaitForm(LoadInitData);
                wait.ShowDialog();

                tbBillPaid.Text = dispatchRecord.BillPaid.ToString("n2");
                tbBillId.Text = dispatchRecord.BillNo;
                tbDispatchDate.Text = dispatchRecord.Date.ToShortDateString();
                tbItemsReceived.Text = dispatchRecord.ReceivedItems.ToString("n1");
                tbRepairingPlace.Text = dispatchRecord.Place.Name;
                tbTOPerson.Text = dispatchRecord.TOPerson;
                tbTotalItemsDispatched.Text = dispatchRecord.TotalItems.ToString("n1");
                tbUnderRepairItems.Text = dispatchRecord.RemainingItems.ToString("n1");

                itemQtys = new List<ItemQty>();
                dispItemVMBindingSource.List.Clear();
                nowReceItemVMBindingSource.List.Clear();
                receItemVMBindingSource.List.Clear();

                foreach (var item in allEntries)
                {
                    if(item.Direction == RepEntryDirection.Dispatched)
                    {
                        DispItemVM vm = new DispItemVM
                        {
                            Id = item.Id,
                            Item = string.Format("{0}-{1}-{2}", item.RepItem.ItemCategory.Title, item.RepItem.Name, item.RepItem.Location.Name),
                            QtyDispatched = item.DispatchQty,
                            Remarks = item.Remarks,
                            QtyReceived = 0,
                        };
                        var recObj = allEntries.Where(a => a.RepItemId == item.RepItemId && a.Direction == RepEntryDirection.Received);
                        if(recObj != null && recObj.Count() > 0)
                        {
                            vm.QtyReceived = recObj.Sum(a => a.ReceivedQty);
                        }
                        dispItemVMBindingSource.List.Add(vm);

                        ItemQty iq = new ItemQty
                        {
                            ItemId = item.RepItemId,
                            Qty = item.DispatchQty,
                            Name = item.RepItem.Name
                        };
                        itemQtys.Add(iq);
                        if(vm.QtyRemaining > 0)
                        {
                            NowReceItemVM nowVM = new NowReceItemVM
                            {
                                Id = item.Id,
                                Item = string.Format("{0}-{1}-{2}", item.RepItem.ItemCategory.Title, item.RepItem.Name, item.RepItem.Location.Name),
                                ItemId = item.RepItemId,
                                QtyReceiving = 0,
                                QtyRemaining = vm.QtyRemaining,
                                Remarks = item.Remarks
                            };
                            nowReceItemVMBindingSource.List.Add(nowVM);
                        }
                        
                    }
                    else if(item.Direction == RepEntryDirection.Received)
                    {
                        ReceItemVM vm = new ReceItemVM
                        {
                            Id = item.Id,
                            Item = string.Format("{0}-{1}-{2}", item.RepItem.ItemCategory.Title, item.RepItem.Name, item.RepItem.Location.Name),
                            Qty = item.ReceivedQty,
                            Remarks = item.Remarks
                        };
                        receItemVMBindingSource.List.Add(vm);

                        var obj = itemQtys.FirstOrDefault(a => a.ItemId == item.RepItemId);
                        if (obj != null)
                        {
                            obj.Qty -= item.ReceivedQty;
                            if (obj.Qty == 0)
                            {
                                RemoveItemQty(obj);
                            }
                        }
                    }

                    
                }             
                

                btnUpdate.Enabled = !IsCompleted();
                IsDone = false;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void LoadInitData()
        {
            try
            {
                using (Context db = new Context())
                {
                    dispatchRecord = db.RepairDispatchRecords.Find(dispatchId);
                    dispatchRecord.Place = db.RepPlaces.Find(dispatchRecord.RepPlaceId);
                    allEntries = db.RepEntries.Where(a => a.RepairDispatchRecordId == dispatchId).ToList();

                    foreach (var item in allEntries)
                    {
                        item.RepItem = db.RepItems.Find(item.RepItemId);
                        item.RepItem.Location = db.Locations.Find(item.RepItem.LocationId);
                        item.RepItem.ItemCategory = db.ItemCategories.Find(item.RepItem.ItemCategoryId);
                    }                    
                }
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

        private bool IsCompleted()
        {
            foreach (var item in itemQtys)
            {
                if (item.Qty != 0)
                    return false;
            }
            return true;
        }

        private void btnAddEntry_Click(object sender, EventArgs e)
        {
            try
            {
                if(itemQtys.Count == 0)
                {
                    throw new Exception("There is no item to receive");
                }
                AddRepairEntryReceiveForm form = new AddRepairEntryReceiveForm(itemQtys);
                form.ShowDialog();

                var obj = form.EntryVM;
                if (obj != null)
                {
                    var objList = nowReceItemVMBindingSource.List.OfType<NowReceItemVM>().ToList();

                    var obj2 = objList.FirstOrDefault(a => a.Id == obj.Id);
                    if (obj2 != null)
                    {
                        obj2.QtyRemaining += obj.DispatchingQty;
                    }
                    else
                    {
                        NowReceItemVM vm = new NowReceItemVM
                        {
                            Id = obj.Id,
                            QtyRemaining = obj.DispatchingQty,
                            Item = obj.RepItem,
                            ItemId = obj.Id,
                            Remarks = obj.Remarks
                        };

                        nowReceItemVMBindingSource.List.Add(vm);
                    }

                    dgvNowReceivingItems.Refresh();

                    var obj3 = itemQtys.FirstOrDefault(a => a.ItemId == obj.Id);
                    obj3.Qty -= obj.DispatchingQty;
                    if(obj3.Qty == 0)
                    {
                        RemoveItemQty(obj3);
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void RemoveItemQty(ItemQty obj)
        {
            var delObj = itemQtys.FirstOrDefault(a => a.ItemId == obj.ItemId);
            if(delObj != null)
            {
                itemQtys.Remove(delObj);
               
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Gujjar.IsValidForm(panel1))
                {
                    throw new Exception("Please fill text fields");
                }

                var list = nowReceItemVMBindingSource.List.OfType<NowReceItemVM>().ToList();
                if (list.Count == 0)
                {
                    throw new Exception("Please add at least one receiving item/entry in receiving items list");
                }
                if(list.Sum(a => a.QtyReceiving) == 0)
                {
                    throw new Exception("Please atleast receive some quantity of any item");
                }
                if (!Helper.ConfirmAdminPassword())
                {
                    return;
                }

                string confMessage = string.Format("Please confirm following information\nWorkshop: ({0})\nTotal items: ({1})\nBill amount: ({2})\n\nAll information is correct?",
                    tbRepairingPlace.Text, list.Sum(a => a.QtyRemaining).ToString("n1"), tbBillAmount.Text);

                DialogResult res = Gujjar.ConfirmYesNo(confMessage);
                if (res == DialogResult.No)
                {
                    return;
                }



                decimal billAmount = tbBillAmount.Text.ToDecimal();
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            RepairReceiveRecord receRecord = new RepairReceiveRecord
                            {
                                BillAmount = billAmount,
                                BillId = tbBillId.Text,
                                DispatchId = dispatchId,
                                Entries = null,
                                Id = 0,
                                InDate = DateTime.Now,
                                Place = null,
                                RepPlaceId = dispatchRecord.RepPlaceId,
                                Unum = Helper.Unum,
                                ReceivedDate = dtp.Value,
                                ReceivingPerson = tbReceivePerson.Text,
                                QtyReceived = list.Sum(a => a.QtyReceiving)
                            };

                            receRecord = db.RepairReceivedRecords.Add(receRecord);
                            db.SaveChanges();

                            RepairDispatchRecord disObj = db.RepairDispatchRecords.Find(dispatchId);
                            disObj.ReceivedItems += receRecord.QtyReceived;
                            disObj.RemainingItems = disObj.TotalItems - disObj.ReceivedItems;

                            disObj.Status = disObj.RemainingItems == 0 ? RepairDispatchStatus.TotallyReceived : RepairDispatchStatus.DispatchedReceived;
                            disObj.BillPaid += billAmount;

                            db.Entry(receRecord).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            foreach (var item in list.Where(a => a.QtyReceiving > 0))
                            {
                                RepEntry entry = new RepEntry
                                {
                                    Direction = RepEntryDirection.Received,
                                    DispatchQty = 0,
                                    Id = 0,
                                    ReceivedQty = item.QtyReceiving,
                                    Remarks = item.Remarks,
                                    RepairDispatchRecord = null,
                                    RepairDispatchRecordId = dispatchId,
                                    RepairReceiveRecord = null,
                                    RepairReceiveRecordId = receRecord.Id,
                                    RepItem = null,
                                    RepItemId = item.ItemId
                                };

                                db.RepEntries.Add(entry);
                                db.SaveChanges();

                                var itemObj = db.RepItems.Find(item.ItemId);
                                itemObj.UR -= item.QtyReceiving;
                                itemObj.SKU += item.QtyReceiving;
                                itemObj.TotalValue = itemObj.SKU * itemObj.UnitValue;
                                db.Entry(itemObj).State = System.Data.Entity.EntityState.Modified;
                            }
                            db.SaveChanges();

                            #region "Financials"

                            GeneralAccount debitAccount = db.Accounts.Find(Properties.Resources.RepairingExpenseAccount) as GeneralAccount;
                            GeneralAccount creditAccount = db.Accounts.Find(dispatchRecord.Place.GeneralAccountId) as GeneralAccount;

                            decimal amount = billAmount;
                            string msg = string.Format("Repairing bill amount ({0}) is credited to ({1}), received items qty ({2}) after repair, received by ({3}), date ({4})",
                                tbBillAmount.Text, tbRepairingPlace.Text, receRecord.QtyReceived.ToString("n1"), tbReceivePerson.Text, receRecord.ReceivedDate.ToShortDateString());

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


                            Gujjar.InfoMsg("Items are received, stock is updated and all records are saved in database successfully");
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

        private void dgvNowReceivingItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.RowIndex == dgvNowReceivingItems.NewRowIndex)
                return;

            try
            {
                int id = dgvNowReceivingItems.Rows[e.RowIndex].Cells[0].Value.ToInt();
                var obj = nowReceItemVMBindingSource.List.OfType<NowReceItemVM>()
                    .First(a => a.Id == id);
                if(obj.QtyReceiving <= 0)
                {
                    obj.QtyReceiving = 0;
                }
                if(obj.QtyReceiving > obj.QtyRemaining)
                {
                    obj.QtyReceiving = obj.QtyRemaining;
                }

                dgvNowReceivingItems.Refresh();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }


}
