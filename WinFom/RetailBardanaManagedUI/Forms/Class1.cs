using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.RetailBardanaManagedUI.Forms
{
    class Class1
    {
        private void Fin()
        {
            #region "Financials"
            //debitAccount = db.Accounts.Find(bardanaItem.GeneralAccountId) as GeneralAccount;
            //creditAccount = db.Accounts.Find(supplier.GeneralAccountId) as GeneralAccount;

            //var user = SingleTon.LoginForm.appUser;
            //decimal amount = entry.TotalPrice;

            //string finMsg = string.Format("Purchased retail bardana item ({0}), qty ({1}), in amount ({2}) from supplier ({3}) description ({4}). By ({5})",
            //    bardanaItem.Title, entry.QtyEntered.ToString("n1"), amount.ToString("n1"), supplier.Name, tbRemarks.Text, user.Name);

            //DayBook daybookEntry = new DayBook
            //{
            //    Id = 0,
            //    Amount = amount,
            //    Date = DateTime.Now,
            //    Description = finMsg,
            //    CanRollBack = false
            //};

            //daybookEntry = db.DayBooks.Add(daybookEntry);
            //db.SaveChanges();

            //#region "Debit transaction"

            //AccountTransaction debitItemTrans = new AccountTransaction
            //{
            //    Id = 0,
            //    Account = null,
            //    AccountTransactionType = AccountTransactionType.Debit,
            //    Balance = amount,
            //    CreditAmount = 0,
            //    Date = dtp.Value,
            //    DayBookId = daybookEntry.Id,
            //    DebitAmount = amount,
            //    GeneralAccountId = debitAccount.Id,
            //    Description = finMsg
            //};

            //var debitDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == debitAccount.Id).AsParallel()
            //    .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
            //    .OrderByDescending(a => a.Id).FirstOrDefault();

            //if (debitDbEntry != null)
            //{
            //    debitItemTrans.Balance += debitDbEntry.Balance;
            //}

            //debitItemTrans = db.AccountTransactions.Add(debitItemTrans);




            //#endregion

            //#region  "Credit transaction"
            //AccountTransaction creditItemTrans = new AccountTransaction
            //{
            //    Id = 0,
            //    Account = null,
            //    AccountTransactionType = AccountTransactionType.Credit,
            //    Balance = amount,
            //    CreditAmount = amount,
            //    Date = DateTime.Now,
            //    DayBookId = daybookEntry.Id,
            //    DebitAmount = 0,
            //    GeneralAccountId = creditAccount.Id,
            //    Description = finMsg
            //};

            //var creditDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == creditAccount.Id).AsParallel()
            //    .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
            //    .OrderByDescending(a => a.Id).FirstOrDefault();

            //if (creditDbEntry != null)
            //{
            //    creditItemTrans.Balance += creditDbEntry.Balance;
            //}

            //creditItemTrans = db.AccountTransactions.Add(creditItemTrans);
            //db.SaveChanges();
            //#endregion

            //var daybookdb = db.DayBooks.Find(daybookEntry.Id);
            //daybookdb.DebitTrace = string.Format("({0}). Trans Id: {1}", debitAccount.Title, debitItemTrans.Id);
            //daybookdb.CreditTrace = string.Format("({0}). Trans Id: {1}", creditAccount.Title, creditItemTrans.Id);
            //daybookdb.CreditAccountId = creditAccount.Id;
            //daybookdb.DebitAccountId = debitAccount.Id;

            //db.Entry(daybookdb).State = System.Data.Entity.EntityState.Modified;
            //db.SaveChanges();
            #endregion
        }
    }
}
