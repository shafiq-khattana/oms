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
using Model.Financials.Model;

namespace WinFom.RepairUI.Forms
{
    public partial class ReceiveDispatchedEntryForm : Form
    {
        private int entryId = 0;
        private AppSettings appSett = Helper.AppSet;
        private AppUser appUser = SingleTon.LoginForm.appUser;
        public bool IsDone { get; set; }

        public ReceiveDispatchedEntryForm(int entId)
        {
            InitializeComponent();
            entryId = entId;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    using (Context db = new Context())
            //    {
            //        repEntry = db.RepEntries.Find(entryId);
            //        repEntry.RepItem = db.RepItems.Find(repEntry.RepItemId);
            //        repEntry.RepItem.RepCategory = db.RepCategories.Find(repEntry.RepItem.RepCategoryId);
            //        repEntry.RepPlace = db.RepPlaces.Find(repEntry.RepPlaceId);


            //    }

            //    tbCategory.Text = repEntry.RepItem.RepCategory.Title;
            //    tbItemName.Text = repEntry.RepItem.Title;
            //    tbLocation.Text = repEntry.RepItem.Location;
            //    tbDispatchingQty.Text = repEntry.DispatchingQty.ToString("n1");
            //    tbWeight.Text = repEntry.Weight.ToString("n1");
            //    tbTakingPerson.Text = repEntry.TakingOutPerson;
            //    tbRepairingPlace.Text = repEntry.RepPlace.Name;
            //    dtp.Value = repEntry.DispatchDate;
            //    tbRemarks.Text = repEntry.DispatchingRemarks;

            //    Gujjar.NumbersOnly(tbReceivingQty);
            //    Gujjar.NumbersOnly(tbRepairCost);
            //    tbReceiveRemarks.Text = "N/A";
            //    Gujjar.TBOptional(tbReceiveRemarks);
            //}
            //catch (Exception exp)
            //{
            //    Gujjar.ErrMsg(exp);
            //}
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
        //    try
        //    {
        //        if(!Gujjar.IsValidForm(pMain))
        //        {
        //            throw new Exception("Please fill all text fields");
        //        }

        //        if(!Helper.ConfirmUserPassword())
        //        {
        //            return;
        //        }

        //        DialogResult res = Gujjar.ConfirmYesNo("Are you sured... Please confirm .. !!");
        //        if (res == DialogResult.No)
        //            return;

        //        using (Context db = new Context())
        //        {
        //            using (var trans = db.Database.BeginTransaction())
        //            {
        //                try
        //                {
        //                    var obj = db.RepEntries.Find(entryId);
        //                    obj.ReceivingDate = dtpReceivingDate.Value;
        //                    obj.ReceivingQty = tbReceivingQty.Text.ToDecimal();
        //                    obj.ReceivingRemarks = tbReceiveRemarks.Text;
        //                    obj.RepairCost = tbRepairCost.Text.ToDecimal();
                            
        //                    if(obj.ReceivingQty != obj.DispatchingQty)
        //                    {
        //                        obj.Status = ReceiveStatus.Partial;
        //                    }
        //                    else
        //                    {
        //                        obj.Status = ReceiveStatus.Completed;
        //                    }

        //                    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        //                    db.SaveChanges();

        //                    #region "Financials"

        //                    GeneralAccount debitAccount = db.Accounts.Find(Properties.Resources.RepairingExpenseAccount) as GeneralAccount;
        //                    GeneralAccount creditAccount = db.Accounts.Find(repEntry.RepPlace.GeneralAccountId) as GeneralAccount;

        //                    decimal amount = obj.RepairCost;
        //                    string msg = string.Format("Repairing cost of item ({0}) qty ({1}) in payable amount ({2}) from ({3}), receiving remarks ({4}), by ({5})",
        //                        repEntry.RepItem.Title, obj.ReceivingQty.ToString("n1"), obj.RepairCost.ToString("n1"), repEntry.RepPlace.Name, obj.ReceivingRemarks, appUser.Name);

        //                    DayBook daybookEntry = new DayBook
        //                    {
        //                        Id = 0,
        //                        Amount = amount,
        //                        Date = dtp.Value,
        //                        Description = msg,
        //                        CanRollBack = false
        //                    };

        //                    daybookEntry = db.DayBooks.Add(daybookEntry);
        //                    db.SaveChanges();

        //                    #region "Debit transaction"

        //                    AccountTransaction debitItemTrans = new AccountTransaction
        //                    {
        //                        Id = 0,
        //                        Account = null,
        //                        AccountTransactionType = AccountTransactionType.Debit,
        //                        Balance = amount,
        //                        CreditAmount = 0,
        //                        Date = dtp.Value,
        //                        DayBookId = daybookEntry.Id,
        //                        DebitAmount = amount,
        //                        GeneralAccountId = debitAccount.Id,
        //                        Description = msg
        //                    };

        //                    var debitDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == debitAccount.Id).AsParallel()
        //                        .ToList().Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date)
        //                        .OrderByDescending(a => a.Id).FirstOrDefault();

        //                    if (debitDbEntry != null)
        //                    {
        //                        debitItemTrans.Balance += debitDbEntry.Balance;
        //                    }

        //                    debitItemTrans = db.AccountTransactions.Add(debitItemTrans);
        //                    db.SaveChanges();



        //                    #endregion

        //                    #region  "Credit transaction"
        //                    AccountTransaction creditItemTrans = new AccountTransaction
        //                    {
        //                        Id = 0,
        //                        Account = null,
        //                        AccountTransactionType = AccountTransactionType.Credit,
        //                        Balance = amount,
        //                        CreditAmount = amount,
        //                        Date = DateTime.Now,
        //                        DayBookId = daybookEntry.Id,
        //                        DebitAmount = 0,
        //                        GeneralAccountId = creditAccount.Id,
        //                        Description = msg
        //                    };

        //                    var creditDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == creditAccount.Id).AsParallel()
        //                        .ToList().Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date)
        //                        .OrderByDescending(a => a.Id).FirstOrDefault();

        //                    if (creditDbEntry != null)
        //                    {
        //                        creditItemTrans.Balance += creditDbEntry.Balance;
        //                    }

        //                    creditItemTrans = db.AccountTransactions.Add(creditItemTrans);
        //                    db.SaveChanges();
        //                    #endregion

        //                    var daybookdb = db.DayBooks.Find(daybookEntry.Id);
        //                    daybookdb.DebitTrace = string.Format("({0}). Trans Id: {1}", debitAccount.Title, debitItemTrans.Id);
        //                    daybookdb.CreditTrace = string.Format("({0}). Trans Id: {1}", creditAccount.Title, creditItemTrans.Id);
        //                    daybookdb.CreditAccountId = creditAccount.Id;
        //                    daybookdb.DebitAccountId = debitAccount.Id;

        //                    db.Entry(daybookdb).State = System.Data.Entity.EntityState.Modified;
        //                    db.SaveChanges();
        //                    #endregion

        //                    trans.Commit();
        //                    IsDone = true;
        //                    Gujjar.InfoMsg("Data is updated and record stored in database successfully");
        //                    Close();
        //                }
        //                catch (Exception exp2)
        //                {
        //                    trans.Rollback();
        //                    throw exp2;
        //                }
        //            }
                        
                    
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        Gujjar.ErrMsg(exp);
        //    }
        }
    }
}
