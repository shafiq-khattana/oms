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
using Khattana.Display;
using Khattana.Model;
using Model.Financials.ViewModel;
using DevExpress.XtraReports.UI;
using WinFom.Financials.Reports.Model;
using WinFom.Financials.Reports.XtraReports;
using Zen.Barcode;

namespace WinFom.Financials.Forms
{
    public partial class AccountTransactionForm : Form
    {
        private AppSettings AppSett = Helper.AppSet;
        private List<AccountTransaction> transactions = null;
        private List<AccountTransaction> tempTrans = null;
        private decimal balance = 0;
        GeneralAccount account = null;
        private string accountId = "";
        private string btndgvdes = "btnda234123481";
        public AccountTransactionForm(string acctId)
        {
            InitializeComponent();
            accountId = acctId;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PopulateTempTrans(List<AccountTransaction> trans)
        {
            if (tempTrans != null)
            {
                tempTrans.Clear();
                tempTrans = null;
            }
            tempTrans = new List<AccountTransaction>();
            foreach (var item in trans)
            {
                tempTrans.Add(item);
            }
        }
        private void LoadTransactions()
        {
            try
            {
                if (transactions != null)
                {
                    transactions.Clear();
                    transactions = null;
                }
                using (Context db = new Context())
                {
                    if (account == null)
                    {
                        account = db.Accounts.Find(accountId) as GeneralAccount;
                    }

                    transactions = db.AccountTransactions.Where(a => a.GeneralAccountId == accountId)
                        .AsParallel().ToList()
                        .Where(a => a.Date.Date >= AppSett.StartDate && a.Date.Date <= AppSett.EndDate).ToList();
                    //foreach (var item in transactions)
                    //{
                    //    item.Account = db.Accounts.OfType<GeneralAccount>()
                    //        .FirstOrDefault(a => a.Id == item.GeneralAccountId);
                    //}

                    PopulateTempTrans(transactions);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void UpdateDgv(List<AccountTransaction> trans)
        {


            transactionsVMBindingSource.List.Clear();
            tbEntriesCount.Text = trans.Count.ToString();

            decimal debit = 0;
            decimal credit = 0;
            if (trans.Count > 0)
            {
                debit = trans.Where(a => a.AccountTransactionType == AccountTransactionType.Debit).Sum(a => a.DebitAmount);
                credit = trans.Where(a => a.AccountTransactionType == AccountTransactionType.Credit).Sum(a => a.CreditAmount);
            }

            tbDebit.Text = debit.ToString("n1");
            tbCredit.Text = credit.ToString("n1");
            try
            {
                foreach (var item in trans)
                {
                    TransactionsVM vm = new TransactionsVM
                    {
                        Id = item.Id,
                        Balance = item.Balance.ToString("n1"),
                        CreditAmount = item.CreditAmount.ToString("n1"),
                        Date = item.Date.ToString(),
                        DayBookId = item.DayBookId,
                        DebitAmount = item.DebitAmount.ToString("n1"),
                        Description = item.Description
                    };
                    if (item.AccountTransactionType == AccountTransactionType.Credit)
                    {
                        vm.CreditAmount = item.CreditAmount.ToString("n1");
                        vm.DebitAmount = "";
                    }
                    else
                    {
                        vm.DebitAmount = item.DebitAmount.ToString("n1");
                        vm.CreditAmount = "";
                    }
                    transactionsVMBindingSource.List.Add(vm);
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
                WaitForm wait = new WaitForm(LoadTransactions);
                wait.ShowDialog();

                lblHeading.Text = account.Title;

                UpdateDgv(transactions);

                var obj = transactions.OrderByDescending(a => a.Id).FirstOrDefault();
                if (obj != null)
                {
                    balance = obj.Balance;
                }
                else
                {
                    balance = 0;
                }
                tbAccountBalance.Text = balance.ToString("n1");
                cbTransTypes.Text = "Both";

                Gujjar.AddDatagridviewButton(dgv, btndgvdes, "Desc", "Desc", 70);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private List<DayBook> pDaybooks = new List<DayBook>();

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SingleTon.LoginForm.RememberMeAdmin)
                {
                    SecurityConfirmForm form = new SecurityConfirmForm(PasswordType.Admin, AppSett.MasterPassword);
                    form.ShowDialog();

                    SecurityConfirmResponse respone = form.Response;
                    if (respone.IsValid == null || !respone.IsValid.Value)
                    {
                        if (respone.IsValid == null)
                            return;
                        else
                            throw new Exception("Invalid password");
                    }

                    SingleTon.LoginForm.RememberMeAdmin = form.Response.RememberMe;


                }
                decimal amount = 0;
                AmountDepositWithdrawForm form2 = new AmountDepositWithdrawForm(AmountDepositWithdrawFormType.Deposit);
                form2.ShowDialog();

                amount = form2.Amount;
                if (amount == 0)
                    return;

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            string message = string.Format("Capital amount ({0}) introduced in business", amount);
                            DayBook capitalEntryDayBook = new DayBook
                            {
                                Id = 0,
                                Date = DateTime.Now,
                                Description = message,
                                Amount = amount,
                                CanRollBack = true,
                                InDate = DateTime.Now.Date
                            };

                            capitalEntryDayBook = db.DayBooks.Add(capitalEntryDayBook);
                            db.SaveChanges();

                            #region "Credit to capital account"
                            AccountTransaction capitalCreditTrans = new AccountTransaction
                            {
                                Id = 0,
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Credit,
                                Balance = amount,
                                CreditAmount = amount,
                                Date = DateTime.Now,
                                DayBookId = capitalEntryDayBook.Id,
                                DebitAmount = 0,
                                Description = message,
                                GeneralAccountId = Properties.Resources.ReadyGoodsSubHead
                            };

                            var capitalDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == capitalCreditTrans.GeneralAccountId)
                                .AsParallel().ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                .OrderByDescending(a => a.Id).FirstOrDefault();
                            if (capitalDbEntry != null)
                            {
                                capitalCreditTrans.Balance += capitalDbEntry.Balance;
                            }
                            capitalCreditTrans = db.AccountTransactions.Add(capitalCreditTrans);
                            db.SaveChanges();

                            #endregion

                            #region "Debit to cash account"
                            AccountTransaction cashDebitTrans = new AccountTransaction
                            {
                                Id = 0,
                                Balance = amount,
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Debit,
                                CreditAmount = 0,
                                DebitAmount = amount,
                                Date = DateTime.Now,
                                DayBookId = capitalEntryDayBook.Id,
                                Description = message,
                                GeneralAccountId = Properties.Resources.CashInHand
                            };

                            var cashDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == cashDebitTrans.GeneralAccountId)
                                .AsParallel().ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                .OrderByDescending(a => a.Id).FirstOrDefault();

                            if (cashDbEntry != null)
                            {
                                cashDebitTrans.Balance += cashDbEntry.Balance;
                            }

                            cashDebitTrans = db.AccountTransactions.Add(cashDebitTrans);
                            db.SaveChanges();
                            #endregion

                            #region "Accounts and daybook traces"
                            GeneralAccount generalCapitalAccountCr = db.Accounts.OfType<GeneralAccount>().FirstOrDefault(a => a.Id == capitalCreditTrans.GeneralAccountId);
                            GeneralAccount generalCashAccountDr = db.Accounts.OfType<GeneralAccount>().FirstOrDefault(a => a.Id == cashDebitTrans.GeneralAccountId);

                            DayBook dbDaybook = db.DayBooks.Find(capitalEntryDayBook.Id);
                            dbDaybook.DebitTrace = string.Format("({0}). Trans Id: {1}", generalCashAccountDr.Title, cashDebitTrans.Id);
                            dbDaybook.CreditTrace = string.Format("({0}). Trans Id: {1}", generalCapitalAccountCr.Title, capitalCreditTrans.Id);
                            dbDaybook.DebitAccountId = generalCashAccountDr.Id;
                            dbDaybook.CreditAccountId = generalCapitalAccountCr.Id;

                            db.Entry(dbDaybook).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            trans.Commit();
                            pDaybooks.Add(dbDaybook);
                            if (AppSett.PrintFinancialTransactions)
                            {
                                Helper.PrintTransactions(pDaybooks);
                            }


                            Gujjar.InfoMsg("Transaction took placed successfully");
                            #endregion

                            WaitForm wait = new WaitForm(LoadTransactions);
                            wait.ShowDialog();

                            UpdateDgv(transactions);
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

        private void btnCashwithdraw_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SingleTon.LoginForm.RememberMeAdmin)
                {
                    SecurityConfirmForm form = new SecurityConfirmForm(PasswordType.Admin, AppSett.MasterPassword);
                    form.ShowDialog();

                    SecurityConfirmResponse respone = form.Response;
                    if (respone.IsValid == null || !respone.IsValid.Value)
                    {
                        if (respone.IsValid == null)
                            return;
                        else
                            throw new Exception("Invalid password");
                    }

                    SingleTon.LoginForm.RememberMeAdmin = form.Response.RememberMe;


                }
                decimal amount = 0;
                AmountDepositWithdrawForm form2 = new AmountDepositWithdrawForm(AmountDepositWithdrawFormType.Withdraw);
                form2.ShowDialog();

                amount = form2.Amount;
                if (amount == 0)
                    return;

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            string message = string.Format("Capital amount ({0}) withdrawn by user ({1}).", amount, SingleTon.LoginForm.appUser.Name);
                            DayBook capitalEntryDayBook = new DayBook
                            {
                                Id = 0,
                                Date = DateTime.Now,
                                Description = message,
                                Amount = amount,
                                CanRollBack = true,
                                InDate = DateTime.Now.Date
                            };

                            capitalEntryDayBook = db.DayBooks.Add(capitalEntryDayBook);
                            db.SaveChanges();

                            #region "Debit to capital account"
                            AccountTransaction capitalDebitTrans = new AccountTransaction
                            {
                                Id = 0,
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Debit,
                                Balance = -amount,
                                CreditAmount = 0,
                                Date = DateTime.Now,
                                DayBookId = capitalEntryDayBook.Id,
                                DebitAmount = amount,
                                Description = message,
                                GeneralAccountId = Properties.Resources.ReadyGoodsSubHead
                            };

                            var capitalDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == capitalDebitTrans.GeneralAccountId)
                                .AsParallel().ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                .OrderByDescending(a => a.Id).FirstOrDefault();
                            if (capitalDbEntry != null)
                            {
                                capitalDebitTrans.Balance += capitalDbEntry.Balance;
                            }
                            capitalDebitTrans = db.AccountTransactions.Add(capitalDebitTrans);
                            db.SaveChanges();

                            #endregion

                            #region "Credit to cash account"
                            AccountTransaction cashCreditTrans = new AccountTransaction
                            {
                                Id = 0,
                                Balance = -amount,
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Credit,
                                CreditAmount = 0,
                                DebitAmount = amount,
                                Date = DateTime.Now,
                                DayBookId = capitalEntryDayBook.Id,
                                Description = message,
                                GeneralAccountId = Properties.Resources.CashInHand
                            };

                            var cashDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == cashCreditTrans.GeneralAccountId)
                                .AsParallel().ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                .OrderByDescending(a => a.Id).FirstOrDefault();

                            if (cashDbEntry != null)
                            {
                                cashCreditTrans.Balance += cashDbEntry.Balance;
                            }

                            cashCreditTrans = db.AccountTransactions.Add(cashCreditTrans);
                            db.SaveChanges();
                            #endregion

                            #region "Accounts and daybook traces"
                            GeneralAccount generalCapitalAccountdr = db.Accounts.OfType<GeneralAccount>().FirstOrDefault(a => a.Id == capitalDebitTrans.GeneralAccountId);
                            GeneralAccount generalCashAccountcr = db.Accounts.OfType<GeneralAccount>().FirstOrDefault(a => a.Id == cashCreditTrans.GeneralAccountId);

                            DayBook dbDaybook = db.DayBooks.Find(capitalEntryDayBook.Id);
                            dbDaybook.CreditTrace = string.Format("({0}). Trans Id: {1}", generalCashAccountcr.Title, cashCreditTrans.Id);
                            dbDaybook.DebitTrace = string.Format("({0}). Trans Id: {1}", generalCapitalAccountdr.Title, capitalDebitTrans.Id);
                            dbDaybook.CreditAccountId = generalCashAccountcr.Id;
                            dbDaybook.DebitAccountId = generalCapitalAccountdr.Id;

                            db.Entry(dbDaybook).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            trans.Commit();
                            if (AppSett.PrintFinancialTransactions)
                            {
                                Helper.PrintTransactions(new List<DayBook> { dbDaybook });
                            }
                            Gujjar.InfoMsg("Transaction took placed successfully");
                            #endregion

                            WaitForm wait = new WaitForm(LoadTransactions);
                            wait.ShowDialog();

                            UpdateDgv(transactions);
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

        private void btnAccountView_Click(object sender, EventArgs e)
        {
            try
            {
                string type = cbTransTypes.Text;
                switch (type)
                {
                    case "Both":
                        UpdateDgv(tempTrans);
                        break;

                    case "Debit":
                        UpdateDgv(tempTrans.Where(a => a.AccountTransactionType == AccountTransactionType.Debit).ToList());
                        break;

                    case "Credit":
                        UpdateDgv(tempTrans.Where(a => a.AccountTransactionType == AccountTransactionType.Credit).ToList());
                        break;
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            tempTrans = transactions.Where(a => a.Date.Date >= dtpFrom.Value.Date && a.Date.Date <= dtpTo.Value.Date).ToList();
            UpdateDgv(tempTrans);
        }

        private void btnAllRecords_Click(object sender, EventArgs e)
        {
            PopulateTempTrans(transactions);
            UpdateDgv(tempTrans);
        }

        private void btnTodayOnly_Click(object sender, EventArgs e)
        {
            tempTrans = tempTrans.Where(a => a.Date.Date == DateTime.Now.Date).ToList();
            UpdateDgv(tempTrans);
        }

        private void cbTransTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTransTypes.SelectedIndex == -1)
                return;
            try
            {
                string type = cbTransTypes.Text;
                switch (type)
                {
                    case "Both":
                        UpdateDgv(tempTrans);
                        break;

                    case "Debit":
                        UpdateDgv(tempTrans.Where(a => a.AccountTransactionType == AccountTransactionType.Debit).ToList());
                        break;

                    case "Credit":
                        UpdateDgv(tempTrans.Where(a => a.AccountTransactionType == AccountTransactionType.Credit).ToList());
                        break;
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }

        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                if (ri == -1 || ri == dgv.NewRowIndex)
                {
                    return;
                }
                if (dgv.Columns[btndgvdes].Index == e.ColumnIndex)
                {
                    string msg = dgv.Rows[ri].Cells[3].Value.ToString();
                    Gujjar.InfoMsg(msg);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        List<FCompRVM> fcompRvmList = null;
        AccountTransactionsReport rep = null;
        List<TransactionsVM> transactionList = null;
        List<FTransactionRVM> ftransactionRvmList = null;
        List<FLinkAccountRVM> fLinkAccountRvmList = null;
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.Rows.Count == 0)
                {
                    throw new Exception("There is no transaction of account to display in report");
                }
                if (ftransactionRvmList != null)
                {
                    ftransactionRvmList.Clear();
                    ftransactionRvmList = null;
                }
                if (fLinkAccountRvmList != null)
                {
                    fLinkAccountRvmList.Clear();
                    fLinkAccountRvmList = null;
                }
                fLinkAccountRvmList = new List<FLinkAccountRVM>();
                WaitForm wait = new WaitForm(LoadData);
                wait.ShowDialog();

                using (Context db = new Context())
                {
                    var links = db.Accounts.OfType<GeneralAccount>()
                        .Where(a => a.ParentAccountId == account.Id).ToList();
                    int cnt = 1;
                    foreach (var item in links)
                    {
                        var trans2 = db.AccountTransactions.Where(a => a.GeneralAccountId == item.Id).ToList();
                        FLinkAccountRVM rvm = new FLinkAccountRVM
                        {
                            Balance = 0,
                            No = cnt,
                            Title = item.Title,
                            Transactions = trans2
                            .Select(a =>
                            new FTransactionRVM
                            {
                                Balance = a.Balance.ToString("n2"),
                                Credit = (a.CreditAmount.Equals("0.0000") || a.CreditAmount.Equals("0.00")) ? "" : a.CreditAmount.ToString("n2"),
                                Debit = (a.DebitAmount.Equals("0.0000")|| a.DebitAmount.Equals("0.00")) ? "" : a.DebitAmount.ToString("n2"),
                                Description = a.Description,
                                Id = a.Id
                            }).ToList(),
                            Type = item.AccountNature.ToString()
                        };
                        if (trans2.Count > 0)
                        {
                            rvm.Balance = trans2.OrderByDescending(a => a.Id).FirstOrDefault().Balance;
                        }
                        fLinkAccountRvmList.Add(rvm);
                        cnt++;
                    }

                }

                transactionList = transactionsVMBindingSource.List.OfType<TransactionsVM>().ToList();
                ftransactionRvmList = new List<FTransactionRVM>();
                foreach (var item in transactionList)
                {
                    FTransactionRVM vm = new FTransactionRVM
                    {
                        Balance = item.Balance,
                        Credit = item.CreditAmount,
                        Debit = item.DebitAmount,
                        Description = item.Description,
                        Id = item.Id
                    };
                    ftransactionRvmList.Add(vm);
                }

                DetailReportBand band1 = rep.Bands["DetailReport"] as DetailReportBand;
                DetailReportBand band2 = rep.Bands["DetailReport1"] as DetailReportBand;
                DetailReportBand band3 = rep.Bands["DetailReport2"] as DetailReportBand;
                band1.DataSource = fcompRvmList;
                band2.DataSource = ftransactionRvmList;
                band3.DataSource = fLinkAccountRvmList;

                var p1 = rep.Parameters["cBal"];
                p1.Visible = false;
                p1.Value = tbAccountBalance.Text;

                var p2 = rep.Parameters["dBal"];
                p2.Visible = false;
                p2.Value = tbDebit.Text;

                var p3 = rep.Parameters["crBal"];
                p3.Visible = false;
                p3.Value = tbCredit.Text;

                var lengendsParam = rep.Parameters["Legends"];
                lengendsParam.Visible = false;
                lengendsParam.Value = Helper.Legends();

                decimal acctBalance = tbAccountBalance.Text.ToDecimal();
                decimal plusBal = fLinkAccountRvmList.Where(a => a.Type == account.AccountNature.ToString())
                    .Sum(a => a.Balance);
                decimal minBal = fLinkAccountRvmList.Where(a => a.Type != account.AccountNature.ToString())
                    .Sum(a => a.Balance);
                decimal linkBal = plusBal - minBal;
                decimal effBal = acctBalance + plusBal - minBal;
                int links2 = fLinkAccountRvmList.Count;

                var p4 = rep.Parameters["links"];
                p4.Value = links2;
                p4.Visible = false;

                var p5 = rep.Parameters["linkBal"];
                p5.Value = linkBal.ToString("n2");
                p5.Visible = false;

                var p6 = rep.Parameters["effBal"];
                p6.Value = effBal.ToString("n2");
                p6.Visible = false;

                rep.ShowPrintMarginsWarning = false;
                rep.ShowPrintStatusDialog = false;
                rep.ShowRibbonPreview();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadData()
        {
            try
            {
                if (fcompRvmList != null)
                {
                    fcompRvmList.Clear();
                    fcompRvmList = null;
                }

                if (rep != null)
                {
                    rep.Dispose();
                    rep = null;
                }
                rep = new AccountTransactionsReport();

                fcompRvmList = new List<FCompRVM>();


                using (Context db = new Context())
                {
                    AppSettings appSett = db.AppSettings.First();
                    FCompRVM fcompVM = new FCompRVM
                    {
                        Address = appSett.Address,
                        Barcode = null,
                        Logo = appSett.Logo,
                        Name = appSett.Name,
                        Phone = appSett.PhoneNo,
                        Qrcode = null,
                        RepDate = string.Format("Dated: {0}", DateTime.Now.ToString()),
                        RepTitle = string.Format("{0} ({1})", account.Title, account.AccountNature.ToString())
                    };
                    Code128BarcodeDraw bcode = BarcodeDrawFactory.Code128WithChecksum;
                    Image img = bcode.Draw(Helper.Unum, 50);
                    byte[] imgBytes = Gujjar.GetByteArrayFromImage(img);
                    fcompVM.Barcode = imgBytes;

                    CodeQrBarcodeDraw qrCode = BarcodeDrawFactory.CodeQr;
                    Image img2 = qrCode.Draw("GBS Solutions, Burewala. 0323-9372084", 50);
                    byte[] qrBytes = Gujjar.GetByteArrayFromImage(img2);
                    fcompVM.Qrcode = qrBytes;

                    fcompRvmList.Add(fcompVM);


                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
