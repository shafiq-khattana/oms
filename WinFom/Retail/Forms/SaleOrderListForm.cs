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
using Model.Retail.Model;
using System.Data.Entity;
using Model.Retail.ViewModel;
using WinFom.Retail.Reports.ViewModel;
using Zen.Barcode;
using WinFom.Retail.Reports.XtraReports;
using DevExpress.XtraReports.UI;
using Model.Financials.Model;
using Model.Employees.Model;

namespace WinFom.Retail.Forms
{
    public partial class SaleOrderListForm : Form
    {
        private List<SaleOrder> saleOrders = null;
        private DateTime todayDate = DateTime.Now.Date;
        private DateTime dtFrom = DateTime.Now;
        private DateTime dtTo = DateTime.Now;
        private string btndgvCancel = "dgvbtncancel";
        private string btndgvprint = "dgvbtnprint";
        private bool IsDated = false;
        private AppUser appUser = SingleTon.LoginForm.appUser;
        private AppSettings AppSett = Helper.AppSet;
        private string userName = "";
        private UserOps ops = Helper.GetUserOps();
        private AppUser AppUser = SingleTon.LoginForm.appUser;
        public SaleOrderListForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadSaleOrders()
        {
            try
            {
                if (saleOrders != null && saleOrders.Count > 0)
                {
                    saleOrders.Clear();
                    saleOrders = null;
                }
                using (Context db = new Context())
                {
                    if (IsDated)
                    {
                        saleOrders = db.SaleOrders.Include(a => a.Customer).Include(a => a.AppUser)
                       .AsParallel().ToList().Where(a => a.OrderDate.Date >= dtFrom.Date && a.OrderDate.Date <= dtTo.Date).ToList();
                        foreach (var item in saleOrders)
                        {
                            item.OrderLines = db.SaleOrderLines.Where(a => a.SaleOrderId == item.Id).Include(a => a.Product)
                                .AsParallel().ToList();
                            foreach (var item2 in item.OrderLines)
                            {
                                item2.Product.ProductSize = db.ProductSizes.Find(item2.Product.ProductSizeId);
                            }
                        }
                    }
                    else
                    {
                        saleOrders = db.SaleOrders.Include(a => a.Customer).Include(a => a.AppUser)
                        .AsParallel().ToList().Where(a => a.OrderDate.Date == todayDate.Date).ToList();
                        foreach (var item in saleOrders)
                        {
                            item.OrderLines = db.SaleOrderLines.Where(a => a.SaleOrderId == item.Id).Include(a => a.Product)
                                .AsParallel().ToList();
                            foreach (var item2 in item.OrderLines)
                            {
                                item2.Product.ProductSize = db.ProductSizes.Find(item2.Product.ProductSizeId);
                            }
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
                IsDated = false;
                WaitForm wait1 = new WaitForm(LoadSaleOrders);
                wait1.ShowDialog();
                datedstr = string.Format("Sale orders dated ({0})", DateTime.Now);
                UpdateVMList(saleOrders);
                string cancelOrder = "Cancel";
                Helper.IsOkApplied();

                if (ops.canReprintSaleInvoiceCustomerCopy)
                {
                    Gujjar.AddDatagridviewButton(dgv, btndgvprint, "Re-Print", "Re-Print", 80);
                }
                if (ops.canCancelSaleInvoice)
                {
                    Gujjar.AddDatagridviewButton(dgv, btndgvCancel, cancelOrder, cancelOrder, 80);
                }

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void UpdateVMList(List<SaleOrder> orders)
        {
            try
            {
                saleOrderVMBindingSource.List.Clear();
                foreach (var item in orders)
                {
                    SaleOrderVM orderVM = new SaleOrderVM
                    {
                        AmountGiven = item.AmountGiven,
                        ChangeGiven = item.ChangeGiven,
                        Customer = item.Customer.Name,
                        OrderDiscountAmount = item.OrderDiscountAmount,
                        CustomerDiscount = item.CustomerDiscount,
                        SaleTaxAmount = item.SaleTaxAmount,
                        CustomerDiscountPecentage = item.CustomerDiscountPecentage,
                        BardanaExpenses = item.BardanaAmount,
                        LaborExpenses = item.LaborAmount,
                        DateTime = item.TimpStamp.ToString(),
                        Discount = item.TotalDiscount,
                        Id = item.Id,
                        IsCredit = item.IsCredit,
                        IsWalkIn = item.IsWalkIn,
                        Items = item.UniqueItems,
                        IsExtraAmounted = item.IsExtraAmounted,
                        ExtraAmount = Math.Abs(item.RemainingAmount),
                        Lines = item.OrderLines.Select(a =>
                        new LineVM2
                        {
                            Discount = a.Discount.ToString("n2"),
                            NetPrice = a.NetPrice.ToString("n2"),
                            Product = string.Format("{0}-{1}", a.Product.Title, a.Product.ProductSize.Title),
                            Qty = a.Qty.ToString("n1"),
                            UnitPrice = a.UnitPrice.ToString("n2")
                        }).ToList(),
                        NetPrice = item.NetPrice,
                        Qty = item.TotalItems,
                        OfferDiscount = item.OfferDiscount,
                        OfferDiscountPercentage = item.OfferDiscountPercentage,
                        OrderDiscount = item.OrderDiscount,
                        OrderId = item.OrderId,
                        OrdType = item.IsWalkIn ? "Walk In" : "Regular",
                        SaleTaxPercentage = item.SaleTaxPercentage,
                        ServiceCharges = item.ServiceCharges,
                        ServiceChargesPercentage = item.ServiceChargesPercentage,
                        TotalDiscountPercentage = item.TotalDiscountPercentage,
                        TotalPrice = item.TototPrice,
                        Unum = item.Unum,
                        Weight = item.OrderLines.Sum(a2 => a2.Product.Weight * a2.Qty),
                        FincType = item.OrderType.ToString(),
                        UserId = item.AppUser.Id
                    };
                    orderVM.UnitPrice = orderVM.NetPrice / orderVM.Qty;
                    saleOrderVMBindingSource.List.Add(orderVM);
                }

                tbOrderCount.Text = orders.Count.ToString();
                var items2 = saleOrderVMBindingSource.List.OfType<SaleOrderVM>();
                decimal qty = items2.Sum(a => a.Qty);
                decimal totalWeight = items2.Sum(a => a.Weight);



                tbQty.Text = qty.ToString();
                tbDiscount.Text = orders.Sum(a => a.TotalDiscount).ToString("n1");
                tbTotalWeight.Text = string.Format("{0}-Kg", totalWeight.ToString("n1"));
                tbNetPrice.Text = orders.Sum(a => a.NetPrice).ToString("n1");
                tbTotalPrice.Text = orders.Sum(a => a.TototPrice).ToString("n1");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                if (ri == -1 || ri == dgv.NewRowIndex)
                    return;

                int id = dgv.Rows[ri].Cells[0].Value.ToInt();

                var obj = saleOrderVMBindingSource.List.OfType<SaleOrderVM>()
                    .FirstOrDefault(a => a.Id == id);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = obj.Lines;
                dataGridView1.Refresh();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                int ci = e.ColumnIndex;

                if (ri == -1 || ri == dgv.NewRowIndex)
                {
                    return;
                }

                if (ops.canReprintSaleInvoiceCustomerCopy)
                {
                    if (dgv.Columns[btndgvprint].Index == ci)
                    {
                        if (!Helper.ConfirmUserPassword())
                            return;

                        DialogResult res = Gujjar.ConfirmYesNo("Are you sure!! you want to re-print the sale invoice?");
                        if (res == DialogResult.No)
                            return;

                        int ordId = dgv.Rows[ri].Cells[0].Value.ToInt();
                        SaleOrder saleOrder = null;
                        using (Context db = new Context())
                        {
                            saleOrder = db.SaleOrders.Find(ordId);
                        }

                        PrintOrder(saleOrder, "Customer Copy", ReceiptType.Customer);
                        Gujjar.InfoMsg("Sale invoice is re-printed");
                    }
                }
                if (ops.canCancelSaleInvoice)
                    if (dgv.Columns[btndgvCancel].Index == ci)
                    {
                        CancelOrderDescriptionForm form = new CancelOrderDescriptionForm();
                        form.ShowDialog();
                        if (!form.IsDone)
                            return;

                        string description = form.Description;

                        DialogResult res = Gujjar.ConfirmYesNo("Please confirm, order cancellation will roll back its financial entries. Are you sure?");
                        if (res == DialogResult.No)
                        {
                            return;
                        }

                        if (!Helper.ConfirmAdminPassword())
                        {
                            return;
                        }

                        int ordId = dgv.Rows[ri].Cells[0].Value.ToString().ToInt();
                        DateTime today = DateTime.Now;
                        using (Context db = new Context())
                        {
                            using (var trans = db.Database.BeginTransaction())
                            {
                                try
                                {
                                    SaleOrder saleOrder = db.SaleOrders.Find(ordId);

                                    if (saleOrder == null)
                                    {
                                        throw new Exception(string.Format("No order found with id ({0})", ordId));
                                    }
                                    Customer customer = db.Customers.Find(saleOrder.CustomerId);
                                    GeneralAccount salesDebitAccount = db.Accounts.Find(Properties.Resources.OilCakeRetailSaleAccount) as GeneralAccount;
                                    GeneralAccount cashCreditAccount = db.Accounts.Find(Properties.Resources.CashInHand) as GeneralAccount;
                                    GeneralAccount customerAccount = null;
                                    if (customer.Id != 1)
                                    {
                                        customerAccount = db.Accounts.Find(customer.GeneralAccountId) as GeneralAccount;
                                    }

                                    List<DayBook> pDaybooks = new List<DayBook>();
                                    switch (saleOrder.OrderType)
                                    {
                                        
                                        case SaleOrderType.Cash:
                                            #region "Cash Order"
                                            #region Over paid
                                            if (saleOrder.RemainingAmount < 0 && saleOrder.IsExtraAmounted)
                                            {
                                                #region Over paid customer debit trans
                                                decimal remainingAmount = Math.Abs(saleOrder.RemainingAmount);
                                                /* Cancel Sale Entry. Invoice No (12). It was over paid with amount (500) by customer
                                                 * (Siddique Ahmad). Total bori (12), rate (1800/bori), total (25000). Cancel description (test),
                                                 * by admin 1
                                                 */
                                                string msgOver = string.Format("Cancel Sale Entry. Invoice No ({0}). It was over paid with amount ({1}) by customer ({2}). Total bori ({3}), rate ({4}/bori), total ({5}). Cancel description ({6}), by ({7})",
                                                    saleOrder.Id, remainingAmount.ToString("n2"), customer.Name, saleOrder.TotalPackings.ToString("n1"), (saleOrder.NetPrice / saleOrder.TotalPackings).ToString("n1"), saleOrder.NetPrice.ToString("n2"), description, appUser.Name);
                                                DayBook customerDayBookOver = new DayBook
                                                {
                                                    Id = 0,
                                                    Description = msgOver,
                                                    Amount = remainingAmount,
                                                    CanRollBack = false,
                                                    CreditAccountId = cashCreditAccount.Id,
                                                    CreditTrace = "N/A",
                                                    Date = today,
                                                    DebitAccountId = customerAccount.Id,
                                                    ReadySchedule = null,
                                                    InDate = DateTime.Now.Date
                                                };

                                                customerDayBookOver = db.DayBooks.Add(customerDayBookOver);
                                                db.SaveChanges();

                                                #region  "Customer debit transaction"                                           

                                                AccountTransaction customerDebitTrans = new AccountTransaction
                                                {
                                                    Id = 0,
                                                    Account = null,
                                                    AccountTransactionType = AccountTransactionType.Debit,
                                                    Balance = remainingAmount,
                                                    CreditAmount = 0,
                                                    Date = DateTime.Now,
                                                    DayBookId = customerDayBookOver.Id,
                                                    DebitAmount = remainingAmount,
                                                    GeneralAccountId = customerAccount.Id,
                                                    Description = msgOver
                                                };

                                                var customerDebitTransEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == customerAccount.Id)
                                                    .AsParallel()
                                                    .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                                    .OrderByDescending(a => a.Id).FirstOrDefault();

                                                if (customerDebitTransEntry != null)
                                                {
                                                    customerDebitTrans.Balance += customerDebitTransEntry.Balance;
                                                }

                                                customerDebitTrans = db.AccountTransactions.Add(customerDebitTrans);
                                                db.SaveChanges();

                                                var customerDayBookOverDb = db.DayBooks.Find(customerDayBookOver.Id);
                                                customerDayBookOverDb.DebitTrace = string.Format("({0}). Trans Id: {1}", customerAccount.Title, customerDebitTrans.Id);
                                                customerDayBookOverDb.CreditTrace = string.Format("Partial Debit");
                                                customerDayBookOverDb.CreditAccountId = null;
                                                customerDayBookOverDb.DebitAccountId = customerAccount.Id;
                                                db.Entry(customerDayBookOverDb).State = EntityState.Modified;
                                                db.SaveChanges();
                                                pDaybooks.Add(customerDayBookOverDb);
                                                #endregion

                                                #endregion

                                                #region Sale amount debit
                                                decimal salesAmountDebit = saleOrder.NetPrice;

                                                string msgOverSales = string.Format("Cancel Sale Entry. Invoice No ({0}). It was over paid with amount ({1}) by customer ({2}). Total bori ({3}), rate ({4}/bori), total ({5}). Cancel description ({6}), by ({7})",
                                                    saleOrder.Id, remainingAmount.ToString("n2"), customer.Name, saleOrder.TotalPackings.ToString("n1"), (saleOrder.NetPrice / saleOrder.TotalPackings).ToString("n1"), saleOrder.NetPrice.ToString("n2"), description, appUser.Name);
                                                DayBook salesDaybook = new DayBook
                                                {
                                                    Id = 0,
                                                    Description = msgOverSales,
                                                    Amount = salesAmountDebit,
                                                    CanRollBack = false,
                                                    CreditAccountId = null,
                                                    CreditTrace = "N/A",
                                                    Date = today,
                                                    DebitAccountId = salesDebitAccount.Id,
                                                    ReadySchedule = null,
                                                    InDate = DateTime.Now.Date
                                                };

                                                salesDaybook = db.DayBooks.Add(salesDaybook);
                                                db.SaveChanges();

                                                #region  "Sales debit transaction"                                           

                                                AccountTransaction salesDebitTrans = new AccountTransaction
                                                {
                                                    Id = 0,
                                                    Account = null,
                                                    AccountTransactionType = AccountTransactionType.Debit,
                                                    Balance = -salesAmountDebit,
                                                    CreditAmount = 0,
                                                    Date = DateTime.Now,
                                                    DayBookId = salesDaybook.Id,
                                                    DebitAmount = salesAmountDebit,
                                                    GeneralAccountId = salesDebitAccount.Id,
                                                    Description = msgOverSales
                                                };

                                                var salesDebitTransEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == salesDebitAccount.Id)
                                                    .AsParallel()
                                                    .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                                    .OrderByDescending(a => a.Id).FirstOrDefault();

                                                if (salesDebitTransEntry != null)
                                                {
                                                    salesDebitTrans.Balance += salesDebitTransEntry.Balance;
                                                }

                                                salesDebitTrans = db.AccountTransactions.Add(salesDebitTrans);
                                                db.SaveChanges();

                                                var salesDayBookOverDb = db.DayBooks.Find(salesDaybook.Id);
                                                salesDayBookOverDb.DebitTrace = string.Format("({0}). Trans Id: {1}", salesDebitAccount.Title, salesDebitTrans.Id);
                                                salesDayBookOverDb.CreditTrace = string.Format("Partial Debit");
                                                salesDayBookOverDb.CreditAccountId = null;
                                                salesDayBookOverDb.DebitAccountId = salesDebitAccount.Id;
                                                db.Entry(salesDayBookOverDb).State = EntityState.Modified;
                                                db.SaveChanges();
                                                pDaybooks.Add(salesDayBookOverDb);
                                                #endregion

                                                #endregion

                                                #region Cash credit transaction

                                                decimal cashCreditAmount = saleOrder.AmountGiven;
                                                /* Cancel Sale Entry. Invoice No (12). Over paid sales. Net amount was (100), paid (120), total bori (5), rate (20/per bori), customer
                                                 * (Siddique Ahmad). Cancel description (test),
                                                 * by admin 1
                                                 */
                                                string cashCreditSaleMessage = string.Format("Cancel Sale Entry. Invoice No ({0}) Cash sale. Net amount was ({1}), paid ({2}), total bori ({3}), rate ({4}/bori) customer ({5}). Cancel description ({6}), by ({7})",
                                                    saleOrder.Id, saleOrder.NetPrice.ToString("n2"), cashCreditAmount.ToString("n2"), saleOrder.TotalPackings.ToString("n1"), (saleOrder.NetPrice / saleOrder.TotalPackings).ToString("n1"), customer.Name, description, appUser.Name);
                                                DayBook cashCreditDayBook = new DayBook
                                                {
                                                    Id = 0,
                                                    Description = cashCreditSaleMessage,
                                                    Amount = cashCreditAmount,
                                                    CanRollBack = false,
                                                    CreditAccountId = cashCreditAccount.Id,
                                                    CreditTrace = "N/A",
                                                    Date = today,
                                                    DebitAccountId = null,
                                                    ReadySchedule = null,
                                                    InDate = DateTime.Now.Date
                                                };

                                                cashCreditDayBook = db.DayBooks.Add(cashCreditDayBook);
                                                db.SaveChanges();



                                                #region  "Credit transaction"      
                                                AccountTransaction cashCreditTrans = new AccountTransaction
                                                {
                                                    Id = 0,
                                                    Account = null,
                                                    AccountTransactionType = AccountTransactionType.Credit,
                                                    Balance = -cashCreditAmount,
                                                    CreditAmount = cashCreditAmount,
                                                    Date = DateTime.Now,
                                                    DayBookId = cashCreditDayBook.Id,
                                                    DebitAmount = 0,
                                                    GeneralAccountId = cashCreditAccount.Id,
                                                    Description = cashCreditSaleMessage
                                                };
                                                var cashCreditDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == cashCreditAccount.Id).AsParallel()
                                                    .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                                    .OrderByDescending(a => a.Id).FirstOrDefault();

                                                if (cashCreditDbEntry != null)
                                                {
                                                    cashCreditTrans.Balance += cashCreditDbEntry.Balance;
                                                }
                                                cashCreditTrans = db.AccountTransactions.Add(cashCreditTrans);
                                                db.SaveChanges();

                                                var cashDaybookDb = db.DayBooks.Find(cashCreditDayBook.Id);
                                                cashDaybookDb.DebitTrace = string.Format("Partial Debit");
                                                cashDaybookDb.CreditTrace = string.Format("({0}). Trans Id: {1}", cashCreditAccount.Title, cashCreditTrans.Id);
                                                cashDaybookDb.CreditAccountId = cashCreditAccount.Id;
                                                cashDaybookDb.DebitAccountId = null;
                                                db.Entry(cashDaybookDb).State = EntityState.Modified;
                                                db.SaveChanges();
                                                pDaybooks.Add(cashDaybookDb);
                                                #endregion

                                                #endregion
                                            }
                                            #endregion

                                            #region "is included in day end"
                                            if (saleOrder.IsDone && !saleOrder.IsExtraAmounted)
                                            {
                                                decimal amount = saleOrder.NetPrice;
                                                /* Cancel Sale Entry. Invoice No (12). Total amount (500) Cash Sale, customer
                                                 * (Siddique Ahmad). Total bori (12), rate (1800/bori), total (25000). Cancel description (test),
                                                 * by admin 1
                                                 */
                                                string isDoneMsg = string.Format("Cancel Sale Entry. Invoice No ({0}) Cash Sale. Total amount ({1}), customer ({2}). Total bori ({3}), rate ({4}/bori), Cancel description ({5}), by ({6})",
                                                    saleOrder.Id, amount, customer.Name, saleOrder.TotalPackings.ToString("n1"), (saleOrder.NetPrice / saleOrder.TotalPackings).ToString("n1"), description, appUser.Name);
                                                DayBook IsDoneDaybook = new DayBook
                                                {
                                                    Id = 0,
                                                    Description = isDoneMsg,
                                                    Amount = amount,
                                                    CanRollBack = false,
                                                    CreditAccountId = cashCreditAccount.Id,
                                                    CreditTrace = "N/A",
                                                    Date = today,
                                                    DebitAccountId = salesDebitAccount.Id,
                                                    ReadySchedule = null,
                                                    InDate = DateTime.Now.Date
                                                };

                                                IsDoneDaybook = db.DayBooks.Add(IsDoneDaybook);
                                                db.SaveChanges();



                                                #region  "Credit transaction"      
                                                AccountTransaction creditTransIsDone = new AccountTransaction
                                                {
                                                    Id = 0,
                                                    Account = null,
                                                    AccountTransactionType = AccountTransactionType.Credit,
                                                    Balance = -amount,
                                                    CreditAmount = amount,
                                                    Date = DateTime.Now,
                                                    DayBookId = IsDoneDaybook.Id,
                                                    DebitAmount = 0,
                                                    GeneralAccountId = cashCreditAccount.Id,
                                                    Description = isDoneMsg
                                                };
                                                var creditDbEntryIsDone = db.AccountTransactions.Where(a => a.GeneralAccountId == cashCreditAccount.Id).AsParallel()
                                                    .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                                    .OrderByDescending(a => a.Id).FirstOrDefault();

                                                if (creditDbEntryIsDone != null)
                                                {
                                                    creditTransIsDone.Balance += creditDbEntryIsDone.Balance;
                                                }
                                                creditTransIsDone = db.AccountTransactions.Add(creditTransIsDone);
                                                db.SaveChanges();
                                                #endregion

                                                #region "Debit transaction"
                                                AccountTransaction debitTransIsDone = new AccountTransaction
                                                {
                                                    Id = 0,
                                                    Account = null,
                                                    AccountTransactionType = AccountTransactionType.Debit,
                                                    Balance = -amount,
                                                    CreditAmount = 0,
                                                    Date = saleOrder.OrderDate,
                                                    DayBookId = IsDoneDaybook.Id,
                                                    DebitAmount = amount,
                                                    GeneralAccountId = salesDebitAccount.Id,
                                                    Description = isDoneMsg
                                                };

                                                var debitDbEntryIsDone = db.AccountTransactions.Where(a => a.GeneralAccountId == salesDebitAccount.Id).AsParallel()
                                                    .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                                    .OrderByDescending(a => a.Id).FirstOrDefault();

                                                if (debitDbEntryIsDone != null)
                                                {
                                                    debitTransIsDone.Balance += debitDbEntryIsDone.Balance;
                                                }

                                                debitTransIsDone = db.AccountTransactions.Add(debitTransIsDone);
                                                db.SaveChanges();
                                                #endregion

                                                var daybookdb2 = db.DayBooks.Find(IsDoneDaybook.Id);
                                                daybookdb2.DebitTrace = string.Format("({0}). Trans Id: {1})", salesDebitAccount.Title, debitTransIsDone.Id);
                                                daybookdb2.CreditTrace = string.Format("({0}). Trans Id: {1}", cashCreditAccount.Title, creditTransIsDone.Id);
                                                daybookdb2.CreditAccountId = cashCreditAccount.Id;
                                                daybookdb2.DebitAccountId = salesDebitAccount.Id;
                                                db.Entry(daybookdb2).State = EntityState.Modified;
                                                db.SaveChanges();
                                                pDaybooks.Add(daybookdb2);
                                            }
                                            #endregion

                                            #endregion
                                            break;
                                        case SaleOrderType.Credit:
                                            #region "Credit Order"
                                            decimal amount4 = saleOrder.NetPrice;
                                            /* Cancel Sale Entry. Invoice No (12). Total amount (500) Credit Sale, customer
                                             * (Siddique Ahmad). Total bori (12), rate (1800/bori), total (25000). Cancel description (test),
                                             * by admin 1
                                             */
                                            string msg = string.Format("Cancel Sale Entry. Invoice No ({0}) Credit Sale. Total amount ({1}), customer ({2}). Total bori ({3}), rate ({4}/bori), Cancel description ({5}), by ({6})",
                                                saleOrder.Id, amount4, customer.Name, saleOrder.TotalPackings.ToString("n1"), (saleOrder.NetPrice / saleOrder.TotalPackings).ToString("n1"), description, appUser.Name);
                                            DayBook daybook = new DayBook
                                            {
                                                Id = 0,
                                                Description = msg,
                                                Amount = amount4,
                                                CanRollBack = false,
                                                CreditAccountId = customerAccount.Id,
                                                CreditTrace = "N/A",
                                                Date = today,
                                                DebitAccountId = salesDebitAccount.Id,
                                                ReadySchedule = null,
                                                InDate = DateTime.Now.Date
                                            };

                                            daybook = db.DayBooks.Add(daybook);
                                            db.SaveChanges();



                                            #region  "Credit transaction"      
                                            AccountTransaction creditTrans = new AccountTransaction
                                            {
                                                Id = 0,
                                                Account = null,
                                                AccountTransactionType = AccountTransactionType.Credit,
                                                Balance = customer.IsEmployee ? Math.Abs(amount4) : -amount4,
                                                CreditAmount = amount4,
                                                Date = DateTime.Now,
                                                DayBookId = daybook.Id,
                                                DebitAmount = 0,
                                                GeneralAccountId = customerAccount.Id,
                                                Description = msg
                                            };
                                            var creditDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == customerAccount.Id).AsParallel()
                                                .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                                .OrderByDescending(a => a.Id).FirstOrDefault();

                                            if (creditDbEntry != null)
                                            {
                                                creditTrans.Balance += creditDbEntry.Balance;
                                            }
                                            creditTrans = db.AccountTransactions.Add(creditTrans);
                                            db.SaveChanges();
                                            #endregion

                                            #region "Debit transaction"
                                            AccountTransaction debitTrans = new AccountTransaction
                                            {
                                                Id = 0,
                                                Account = null,
                                                AccountTransactionType = AccountTransactionType.Debit,
                                                Balance = -amount4,
                                                CreditAmount = 0,
                                                Date = saleOrder.OrderDate,
                                                DayBookId = daybook.Id,
                                                DebitAmount = amount4,
                                                GeneralAccountId = salesDebitAccount.Id,
                                                Description = msg
                                            };

                                            var debitDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == salesDebitAccount.Id).AsParallel()
                                                .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                                .OrderByDescending(a => a.Id).FirstOrDefault();

                                            if (debitDbEntry != null)
                                            {
                                                debitTrans.Balance += debitDbEntry.Balance;
                                            }

                                            debitTrans = db.AccountTransactions.Add(debitTrans);
                                            db.SaveChanges();
                                            #endregion

                                            var daybookdb24 = db.DayBooks.Find(daybook.Id);
                                            daybookdb24.DebitTrace = string.Format("({0}). Trans Id: {1})", salesDebitAccount.Title, debitTrans.Id);
                                            daybookdb24.CreditTrace = string.Format("({0}). Trans Id: {1}", customerAccount.Title, creditTrans.Id);
                                            daybookdb24.CreditAccountId = customerAccount.Id;
                                            daybookdb24.DebitAccountId = salesDebitAccount.Id;
                                            db.Entry(daybookdb24).State = EntityState.Modified;
                                            db.SaveChanges();
                                            pDaybooks.Add(daybookdb24);
                                            #endregion
                                            break;
                                        case SaleOrderType.Partial:
                                            #region "Partial Credit Order"

                                            #region Sale order is done
                                            if (saleOrder.IsDone)
                                            {
                                                {
                                                    #region Customer credit
                                                    decimal remainingAmount = Math.Abs(saleOrder.RemainingAmount);
                                                    decimal paidAmount = saleOrder.AmountGiven;
                                                    decimal netAmount = saleOrder.NetPrice;
                                                    /* Cancel Sale Entry. Invoice No (12). It was over paid with amount (500) by customer
                                                     * (Siddique Ahmad). Total bori (12), rate (1800/bori), total (25000). Cancel description (test),
                                                     * by admin 1
                                                     */
                                                    string msgOver = string.Format("Cancel Sale Entry. Invoice No ({0}) partial credit sale. It was paid with amount ({1}), remaining amount ({2}), by customer ({3}). Total bori ({4}), rate ({5}/bori), total ({6}). Cancel description ({7}), by ({8})",
                                                        saleOrder.Id, paidAmount.ToString("n2"), remainingAmount.ToString("n2"), customer.Name, saleOrder.TotalPackings.ToString("n1"), (saleOrder.NetPrice / saleOrder.TotalPackings).ToString("n1"), saleOrder.NetPrice.ToString("n2"), description, appUser.Name);
                                                    DayBook customerDaybook = new DayBook
                                                    {
                                                        Id = 0,
                                                        Description = msgOver,
                                                        Amount = remainingAmount,
                                                        CanRollBack = false,
                                                        CreditAccountId = customerAccount.Id,
                                                        CreditTrace = "N/A",
                                                        Date = today,
                                                        DebitAccountId = null,
                                                        ReadySchedule = null,
                                                        InDate = DateTime.Now.Date
                                                    };

                                                    customerDaybook = db.DayBooks.Add(customerDaybook);
                                                    db.SaveChanges();

                                                    #region  "Customer credit transaction"                                           

                                                    AccountTransaction customerCreditTrans = new AccountTransaction
                                                    {
                                                        Id = 0,
                                                        Account = null,
                                                        AccountTransactionType = AccountTransactionType.Credit,
                                                        Balance = -remainingAmount,
                                                        CreditAmount = remainingAmount,
                                                        Date = DateTime.Now,
                                                        DayBookId = customerDaybook.Id,
                                                        DebitAmount = 0,
                                                        GeneralAccountId = customerAccount.Id,
                                                        Description = msgOver
                                                    };

                                                    var customerCreditEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == customerAccount.Id)
                                                        .AsParallel()
                                                        .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                                        .OrderByDescending(a => a.Id).FirstOrDefault();

                                                    if (customerCreditEntry != null)
                                                    {
                                                        customerCreditTrans.Balance += customerCreditEntry.Balance;
                                                    }

                                                    customerCreditTrans = db.AccountTransactions.Add(customerCreditTrans);
                                                    db.SaveChanges();

                                                    var customerDabybookDb = db.DayBooks.Find(customerDaybook.Id);
                                                    customerDabybookDb.DebitTrace = "Partial debit";
                                                    customerDabybookDb.CreditTrace = string.Format("({0}). Trans Id: {1}", customerAccount.Title, customerCreditTrans.Id);
                                                    customerDabybookDb.CreditAccountId = customerAccount.Id;
                                                    customerDabybookDb.DebitAccountId = null;
                                                    db.Entry(customerDabybookDb).State = EntityState.Modified;
                                                    db.SaveChanges();
                                                    pDaybooks.Add(customerDabybookDb);
                                                    #endregion

                                                    #endregion

                                                    #region Cash account credit
                                                    decimal cashAccountCredit = saleOrder.AmountGiven;

                                                    string msgOverSales = string.Format("Cancel Sale Entry. Invoice No ({0}) partial credit. Customer ({1}) paid ({2}) cash. Remaining amount was ({3}). Total bori ({4}), rate ({5}/bori), total ({6}). Cancel description ({7}), by ({8})",
                                                        saleOrder.Id, customer.Name, saleOrder.NetPrice.ToString("n1"), remainingAmount.ToString("n1"), saleOrder.NetPrice.ToString(), saleOrder.TotalPackings.ToString("n1"), (saleOrder.NetPrice / saleOrder.TotalPackings).ToString("n1"), saleOrder.NetPrice.ToString("n2"), description, appUser.Name);

                                                    DayBook cashDaybook = new DayBook
                                                    {
                                                        Id = 0,
                                                        Description = msgOverSales,
                                                        Amount = cashAccountCredit,
                                                        CanRollBack = false,
                                                        CreditAccountId = null,
                                                        CreditTrace = "N/A",
                                                        Date = today,
                                                        DebitAccountId = null,
                                                        ReadySchedule = null,
                                                        InDate = DateTime.Now.Date
                                                    };

                                                    cashDaybook = db.DayBooks.Add(cashDaybook);
                                                    db.SaveChanges();

                                                    #region  "Cash credit transaction"                                           

                                                    AccountTransaction cashCreditTrans = new AccountTransaction
                                                    {
                                                        Id = 0,
                                                        Account = null,
                                                        AccountTransactionType = AccountTransactionType.Credit,
                                                        Balance = -cashAccountCredit,
                                                        CreditAmount = cashAccountCredit,
                                                        Date = DateTime.Now,
                                                        DayBookId = cashDaybook.Id,
                                                        DebitAmount = 0,
                                                        GeneralAccountId = cashCreditAccount.Id,
                                                        Description = msgOverSales
                                                    };

                                                    var salesDebitTransEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == cashCreditAccount.Id)
                                                        .AsParallel()
                                                        .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                                        .OrderByDescending(a => a.Id).FirstOrDefault();

                                                    if (salesDebitTransEntry != null)
                                                    {
                                                        cashCreditTrans.Balance += salesDebitTransEntry.Balance;
                                                    }

                                                    cashCreditTrans = db.AccountTransactions.Add(cashCreditTrans);
                                                    db.SaveChanges();

                                                    var cashDaybookDb2 = db.DayBooks.Find(cashDaybook.Id);
                                                    cashDaybookDb2.DebitTrace = "Partial credit";
                                                    cashDaybookDb2.CreditTrace = string.Format("({0}). Trans Id: {1}", cashCreditAccount.Title, cashCreditTrans.Id);
                                                    cashDaybookDb2.CreditAccountId = cashCreditAccount.Id;
                                                    cashDaybookDb2.DebitAccountId = null;
                                                    db.Entry(cashDaybookDb2).State = EntityState.Modified;
                                                    db.SaveChanges();

                                                    pDaybooks.Add(cashDaybookDb2);
                                                    #endregion

                                                    #endregion

                                                    #region Sales Debit transaction

                                                    decimal salesDebitAmount = saleOrder.NetPrice;
                                                    /* Cancel Sale Entry. Invoice No (12). Over paid sales. Net amount was (100), paid (120), total bori (5), rate (20/per bori), customer
                                                     * (Siddique Ahmad). Cancel description (test),
                                                     * by admin 1
                                                     */
                                                    string cashCreditSaleMessage = msgOverSales;
                                                    DayBook saleDebitDayBook = new DayBook
                                                    {
                                                        Id = 0,
                                                        Description = cashCreditSaleMessage,
                                                        Amount = salesDebitAmount,
                                                        CanRollBack = false,
                                                        CreditAccountId = cashCreditAccount.Id,
                                                        CreditTrace = "N/A",
                                                        Date = today,
                                                        DebitAccountId = null,
                                                        ReadySchedule = null,
                                                        InDate = DateTime.Now.Date
                                                    };

                                                    saleDebitDayBook = db.DayBooks.Add(saleDebitDayBook);
                                                    db.SaveChanges();



                                                    #region  "Debit transaction"      
                                                    AccountTransaction saleDebitTrans = new AccountTransaction
                                                    {
                                                        Id = 0,
                                                        Account = null,
                                                        AccountTransactionType = AccountTransactionType.Debit,
                                                        Balance = -salesDebitAmount,
                                                        CreditAmount = 0,
                                                        Date = DateTime.Now,
                                                        DayBookId = saleDebitDayBook.Id,
                                                        DebitAmount = salesDebitAmount,
                                                        GeneralAccountId = salesDebitAccount.Id,
                                                        Description = cashCreditSaleMessage
                                                    };
                                                    var saleDebitEntryDb = db.AccountTransactions.Where(a => a.GeneralAccountId == salesDebitAccount.Id).AsParallel()
                                                        .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                                        .OrderByDescending(a => a.Id).FirstOrDefault();

                                                    if (saleDebitEntryDb != null)
                                                    {
                                                        saleDebitTrans.Balance += saleDebitEntryDb.Balance;
                                                    }
                                                    saleDebitTrans = db.AccountTransactions.Add(saleDebitTrans);
                                                    db.SaveChanges();

                                                    var salesDabybookDb = db.DayBooks.Find(saleDebitDayBook.Id);
                                                    salesDabybookDb.DebitTrace = string.Format("({0}). Trans Id: {1}", cashCreditAccount.Title, saleDebitTrans.Id);
                                                    salesDabybookDb.CreditTrace = "Partial credit";
                                                    salesDabybookDb.CreditAccountId = null;
                                                    salesDabybookDb.DebitAccountId = salesDebitAccount.Id;
                                                    db.Entry(salesDabybookDb).State = EntityState.Modified;
                                                    db.SaveChanges();

                                                    pDaybooks.Add(salesDabybookDb);
                                                    #endregion

                                                    #endregion
                                                }
                                            }
                                            #endregion

                                            #region Sale order is not done
                                            else
                                            {
                                                #region Cash account credit
                                                decimal transAmount = saleOrder.RemainingAmount;

                                                string msgOverSales = string.Format("Cancel order entry, invoice no ({0}) partial credit. Amount ({1}), paid ({2}), remaining ({3}), customer ({4}), description ({5}), by ({6})",
                                                   saleOrder.Id, transAmount.ToString("n2"), saleOrder.AmountGiven.ToString("n2"), saleOrder.RemainingAmount.ToString("n2"), customer.Name, description, appUser.Name);

                                                DayBook cashDaybook = new DayBook
                                                {
                                                    Id = 0,
                                                    Description = msgOverSales,
                                                    Amount = transAmount,
                                                    CanRollBack = false,
                                                    CreditAccountId = null,
                                                    CreditTrace = "N/A",
                                                    Date = today,
                                                    DebitAccountId = null,
                                                    ReadySchedule = null,
                                                    InDate = DateTime.Now.Date
                                                };

                                                cashDaybook = db.DayBooks.Add(cashDaybook);
                                                db.SaveChanges();

                                                #region  "Cash credit transaction"                                           

                                                AccountTransaction customerCreditTrans = new AccountTransaction
                                                {
                                                    Id = 0,
                                                    Account = null,
                                                    AccountTransactionType = AccountTransactionType.Credit,
                                                    Balance = -transAmount,
                                                    CreditAmount = 0,
                                                    Date = DateTime.Now,
                                                    DayBookId = cashDaybook.Id,
                                                    DebitAmount = transAmount,
                                                    GeneralAccountId = customerAccount.Id,
                                                    Description = msgOverSales
                                                };

                                                var customerCreditDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == customerAccount.Id)
                                                    .AsParallel()
                                                    .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                                    .OrderByDescending(a => a.Id).FirstOrDefault();

                                                if (customerCreditDbEntry != null)
                                                {
                                                    customerCreditTrans.Balance += customerCreditDbEntry.Balance;
                                                }

                                                customerCreditTrans = db.AccountTransactions.Add(customerCreditTrans);
                                                db.SaveChanges();


                                                #endregion

                                                #endregion

                                                #region Sales Debit transaction

                                                #region  "Debit transaction"      
                                                AccountTransaction saleDebitTrans = new AccountTransaction
                                                {
                                                    Id = 0,
                                                    Account = null,
                                                    AccountTransactionType = AccountTransactionType.Debit,
                                                    Balance = -transAmount,
                                                    CreditAmount = 0,
                                                    Date = DateTime.Now,
                                                    DayBookId = cashDaybook.Id,
                                                    DebitAmount = transAmount,
                                                    GeneralAccountId = salesDebitAccount.Id,
                                                    Description = msgOverSales
                                                };
                                                var saleDebitEntryDb = db.AccountTransactions.Where(a => a.GeneralAccountId == salesDebitAccount.Id).AsParallel()
                                                    .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                                    .OrderByDescending(a => a.Id).FirstOrDefault();

                                                if (saleDebitEntryDb != null)
                                                {
                                                    saleDebitTrans.Balance += saleDebitEntryDb.Balance;
                                                }
                                                saleDebitTrans = db.AccountTransactions.Add(saleDebitTrans);
                                                db.SaveChanges();

                                                var cashDaybookDb2 = db.DayBooks.Find(cashDaybook.Id);
                                                cashDaybookDb2.DebitTrace = string.Format("({0}). Trans Id: {1}", salesDebitAccount.Title, saleDebitTrans.Id);
                                                cashDaybookDb2.CreditTrace = string.Format("({0}). Trans Id: {1}", customerAccount.Title, customerCreditTrans.Id);
                                                cashDaybookDb2.CreditAccountId = customerAccount.Id;
                                                cashDaybookDb2.DebitAccountId = salesDebitAccount.Id;
                                                db.Entry(cashDaybookDb2).State = EntityState.Modified;
                                                db.SaveChanges();
                                                pDaybooks.Add(cashDaybookDb2);
                                                #endregion

                                                #endregion
                                            }
                                            #endregion
                                            #endregion
                                            break;
                                    }
                                    if (saleOrder.IsExpensed)
                                    {
                                        
                                        {
                                            #region Bardana expense accounting
                                            decimal bardanaAmount = saleOrder.BardanaAmount;
                                            DateTime today2 = DateTime.Now;
                                            var bardanaItem = db.RetailBardanaItems.First();
                                            if (bardanaAmount > 0)
                                            {

                                                decimal totalPackings = saleOrder.TotalPackings;

                                                string finMessage = string.Format(@"Cancel order entry: order no ({0}), bardana expense reverting, total bori ({1}), rate ({2}/bori), bardana expense amount ({3}). by ({4})",
                                                    saleOrder.Id, saleOrder.TotalPackings.ToString("n1"), (saleOrder.BardanaAmount / saleOrder.TotalPackings).ToString("n1"), saleOrder.BardanaAmount, appUser.Name);

                                                DayBook daybookEntry = new DayBook
                                                {
                                                    Id = 0,
                                                    Amount = bardanaAmount,
                                                    Date = today2,
                                                    Description = finMessage,
                                                    CanRollBack = false,
                                                    InDate = DateTime.Now.Date,
                                                };
                                                daybookEntry = db.DayBooks.Add(daybookEntry);
                                                db.SaveChanges();



                                                #region "Credit entry"
                                                GeneralAccount creditAccount1 = db.Accounts.Find(Properties.Resources.BardanaSellingExpenseAccount) as GeneralAccount;
                                                GeneralAccount debitAccount1 = db.Accounts.Find(bardanaItem.GeneralAccountId) as GeneralAccount;

                                                AccountTransaction creditTrans1 = new AccountTransaction
                                                {
                                                    Account = null,
                                                    Description = finMessage,
                                                    AccountTransactionType = AccountTransactionType.Credit,
                                                    CreditAmount = bardanaAmount,
                                                    Balance = -bardanaAmount,
                                                    Date = today2,
                                                    DayBookId = daybookEntry.Id,
                                                    DebitAmount = 0,
                                                    Id = 0,
                                                    GeneralAccountId = creditAccount1.Id
                                                };

                                                var dbProdEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == creditAccount1.Id)
                                                    .AsParallel().ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                                    .OrderByDescending(a => a.Id)
                                                    .FirstOrDefault();
                                                if (dbProdEntry != null)
                                                {
                                                    creditTrans1.Balance += dbProdEntry.Balance;
                                                }

                                                creditTrans1 = db.AccountTransactions.Add(creditTrans1);
                                                db.SaveChanges();
                                                #endregion

                                                #region "Debit transaction"
                                                AccountTransaction debitTrans = new AccountTransaction
                                                {
                                                    Id = 0,
                                                    GeneralAccountId = debitAccount1.Id,
                                                    Account = null,
                                                    AccountTransactionType = AccountTransactionType.Debit,
                                                    CreditAmount = 0,
                                                    Balance = bardanaAmount,
                                                    Date = today2,
                                                    DayBookId = daybookEntry.Id,
                                                    DebitAmount = bardanaAmount,
                                                    Description = finMessage
                                                };

                                                var dbDebitEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == debitAccount1.Id)
                                                    .AsParallel().ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                                    .OrderByDescending(a => a.Id)
                                                    .FirstOrDefault();
                                                if (dbDebitEntry != null)
                                                {
                                                    debitTrans.Balance += dbDebitEntry.Balance;
                                                }
                                                debitTrans = db.AccountTransactions.Add(debitTrans);
                                                db.SaveChanges();


                                                DayBook dbDayBook = db.DayBooks.Find(daybookEntry.Id);
                                                dbDayBook.DebitTrace = string.Format("{0}. Trans Id: {1}", debitAccount1.Title, debitTrans.Id);
                                                dbDayBook.CreditTrace = string.Format("{0}. Trans Id: {1}", creditAccount1.Title, creditTrans1.Id);
                                                dbDayBook.CreditAccountId = creditAccount1.Id;
                                                dbDayBook.DebitAccountId = debitAccount1.Id;
                                                db.Entry(dbDayBook).State = EntityState.Modified;
                                                db.SaveChanges();

                                                pDaybooks.Add(dbDayBook);
                                                #endregion

                                            }
                                            #endregion
                                        }
                                        {
                                            #region "Labor expense accounting"
                                            decimal laboramount = saleOrder.LaborAmount;
                                            DateTime today3 = DateTime.Now;
                                            if (laboramount > 0)
                                            {
                                                decimal totalPackings = saleOrder.TotalPackings;

                                                string finMessage = string.Format("Cancel order entry, order no ({0}), laber expense reverting, total bori ({1}), labor rate ({2}), total labor expense amount ({3}), description ({4}). by ({5})",
                                                    saleOrder.Id, saleOrder.TotalPackings.ToString("n1"), (laboramount / saleOrder.TotalPackings).ToString("n1"), laboramount.ToString("n2"), description, appUser.Name);

                                                DayBook daybookEntry = new DayBook
                                                {
                                                    Id = 0,
                                                    Amount = laboramount,
                                                    Date = today3,
                                                    Description = finMessage,
                                                    CanRollBack = false,
                                                    InDate = DateTime.Now.Date
                                                };
                                                daybookEntry = db.DayBooks.Add(daybookEntry);
                                                db.SaveChanges();



                                                #region "Credit entry"
                                                GeneralAccount creditAccount1 = db.Accounts.Find(Properties.Resources.OilCakeRetailLaborExpenseAccount) as GeneralAccount;
                                                GeneralAccount debitAccount1 = db.Accounts.Find(Properties.Resources.OilcakeLaborExpensePayableAccount) as GeneralAccount;

                                                AccountTransaction creditTrans1 = new AccountTransaction
                                                {
                                                    Account = null,
                                                    Description = finMessage,
                                                    AccountTransactionType = AccountTransactionType.Credit,
                                                    CreditAmount = laboramount,
                                                    Balance = -laboramount,
                                                    Date = today3,
                                                    DayBookId = daybookEntry.Id,
                                                    DebitAmount = 0,
                                                    Id = 0,
                                                    GeneralAccountId = creditAccount1.Id
                                                };

                                                var dbProdEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == creditAccount1.Id)
                                                    .AsParallel().ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                                    .OrderByDescending(a => a.Id)
                                                    .FirstOrDefault();
                                                if (dbProdEntry != null)
                                                {
                                                    creditTrans1.Balance += dbProdEntry.Balance;
                                                }

                                                creditTrans1 = db.AccountTransactions.Add(creditTrans1);
                                                db.SaveChanges();
                                                #endregion

                                                #region "Debit transaction"
                                                AccountTransaction debitTrans = new AccountTransaction
                                                {
                                                    Id = 0,
                                                    GeneralAccountId = debitAccount1.Id,
                                                    Account = null,
                                                    AccountTransactionType = AccountTransactionType.Debit,
                                                    CreditAmount = 0,
                                                    Balance = -laboramount,
                                                    Date = today3,
                                                    DayBookId = daybookEntry.Id,
                                                    DebitAmount = laboramount,
                                                    Description = finMessage
                                                };

                                                var dbDebitEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == debitAccount1.Id)
                                                    .AsParallel().ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                                    .OrderByDescending(a => a.Id)
                                                    .FirstOrDefault();
                                                if (dbDebitEntry != null)
                                                {
                                                    debitTrans.Balance += dbDebitEntry.Balance;
                                                }
                                                debitTrans = db.AccountTransactions.Add(debitTrans);
                                                db.SaveChanges();


                                                DayBook dbDayBook = db.DayBooks.Find(daybookEntry.Id);
                                                dbDayBook.DebitTrace = string.Format("{0}. Trans Id: {1}", debitAccount1.Title, debitTrans.Id);
                                                dbDayBook.CreditTrace = string.Format("{0}. Trans Id: {1}", creditAccount1.Title, creditTrans1.Id);
                                                dbDayBook.CreditAccountId = creditAccount1.Id;
                                                dbDayBook.DebitAccountId = debitAccount1.Id;
                                                db.Entry(dbDayBook).State = EntityState.Modified;
                                                db.SaveChanges();
                                                pDaybooks.Add(dbDayBook);
                                                #endregion


                                            }
                                            #endregion
                                        }
                                    }
                                    if (customer.IsEmployee && saleOrder.OrderType != SaleOrderType.Cash)
                                    {

                                        decimal amount = saleOrder.NetPrice;
                                        if (saleOrder.OrderType == SaleOrderType.Partial)
                                            amount = Math.Abs(saleOrder.RemainingAmount);

                                        CreditEntry empEntry = new CreditEntry
                                        {
                                            Id = 0,
                                            Amount = -amount,
                                            Date = DateTime.Now,
                                            EmployeeId = customer.EmpId.Value,
                                            Remarks =
                                            string.Format("Cancel order entry, order no ({0}), total amount ({1}), total bori ({2}). Description ({3}), by ({4})",
                                            saleOrder.Id, saleOrder.NetPrice.ToString("n2"), saleOrder.TotalPackings.ToString("n1"), description, appUser.Name)
                                        };
                                        db.CreditEntries.Add(empEntry);
                                        db.SaveChanges();
                                    }
                                    db.SaveChanges();

                                    #region Cancel order creation
                                    CancelSaleOrder cancelOrder = new CancelSaleOrder
                                    {
                                        AmountGiven = saleOrder.AmountGiven,
                                        AppUser = null,
                                        AppUserId = saleOrder.AppUserId,
                                        BardanaAmount = saleOrder.BardanaAmount,
                                        ChangeGiven = saleOrder.ChangeGiven,
                                        Customer = saleOrder.Customer,
                                        IsExtraAmounted = saleOrder.IsExtraAmounted,
                                        CustomerDiscount = saleOrder.CustomerDiscount,
                                        CustomerDiscountPecentage = saleOrder.CustomerDiscountPecentage,
                                        LaborAmount = saleOrder.LaborAmount,
                                        CustomerId = saleOrder.CustomerId,
                                        DayBookEntries = saleOrder.DayBookEntries,
                                        OrderDiscountAmount = saleOrder.OrderDiscountAmount,
                                        Id = saleOrder.Id,
                                        IsCredit = saleOrder.IsCredit,
                                        IsDone = saleOrder.IsDone,
                                        RemainingAmount = saleOrder.RemainingAmount,
                                        IsExpensed = saleOrder.IsExpensed,
                                        IsWalkIn = saleOrder.IsWalkIn,
                                        SaleTaxAmount = saleOrder.SaleTaxAmount,
                                        NetPrice = saleOrder.NetPrice,
                                        OfferDiscount = saleOrder.OfferDiscount,
                                        OfferDiscountPercentage = saleOrder.OfferDiscountPercentage,
                                        OrderDate = saleOrder.OrderDate,
                                        OrderDiscount = saleOrder.OrderDiscount,
                                        OrderId = saleOrder.OrderId,
                                        OrderLines = null,
                                        OrderType = saleOrder.OrderType,
                                        SaleTaxPercentage = saleOrder.SaleTaxPercentage,
                                        ServiceCharges = saleOrder.ServiceCharges,
                                        ServiceChargesPercentage = saleOrder.ServiceChargesPercentage,
                                        TimpStamp = saleOrder.TimpStamp,
                                        TotalDiscount = saleOrder.TotalDiscount,
                                        TotalDiscountPercentage = saleOrder.TotalDiscountPercentage,
                                        TotalItems = saleOrder.TotalItems,
                                        TotalPackings = saleOrder.TotalPackings,
                                        TotalWeight = saleOrder.TotalWeight,
                                        TototPrice = saleOrder.TototPrice,
                                        UniqueItems = saleOrder.UniqueItems,
                                        Unum = saleOrder.Unum
                                    };
                                    cancelOrder = db.CancelSaleOrders.Add(cancelOrder);
                                    db.SaveChanges();
                                    var lines = db.SaleOrderLines.Where(a => a.SaleOrderId == saleOrder.Id).ToList();
                                    var daybooks = db.DayBooks.Where(a => a.SaleOrderId == saleOrder.Id).ToList();
                                    foreach (var line in lines)
                                    {
                                        CancelOrderLine cline = new CancelOrderLine
                                        {
                                            CancelSaleOrderId = cancelOrder.Id,
                                            Id = line.Id,
                                            Discount = line.Discount,
                                            NetPrice = line.NetPrice,
                                            Product = line.Product,
                                            ProductId = line.ProductId,
                                            Qty = line.Qty,
                                            SaleOrder = null,
                                            TotalPrice = line.TotalPrice,
                                            UnitPrice = line.UnitPrice
                                        };

                                        db.CancelOrderLines.Add(cline);
                                        db.SaleOrderLines.Remove(line);
                                    }
                                    foreach (var db2 in daybooks)
                                    {
                                        db2.SaleOrderId = null;
                                        db.Entry(db2).State = EntityState.Modified;
                                    }

                                    db.SaveChanges();

                                    var orderDb = db.SaleOrders.Find(saleOrder.Id);
                                    db.SaleOrders.Remove(orderDb);
                                    db.SaveChanges();


                                    #endregion
                                    trans.Commit();

                                    if(AppSett.PrintFinancialTransactions && pDaybooks.Count > 0)
                                    {
                                        Helper.PrintTransactions(pDaybooks);
                                    }
                                    Gujjar.InfoMsg("Order has been cancelled successfully and record is stored in database");

                                    WaitForm wait = new WaitForm(LoadSaleOrders);
                                    wait.ShowDialog();

                                    UpdateVMList(saleOrders);
                                }
                                catch (Exception exp2)
                                {
                                    trans.Rollback();
                                    throw exp2;
                                }
                            }
                        }
                    }

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        List<SaleOrderVM> items = null;
        List<SaleOrderLineReport> reportLines = null;
        List<SaleOrderHeader> headerList = null;
        List<Pack56KRepModel> pack56List = null;
        bool isReportDataLoaded = false;
        string datedstr = "na";
        private void LoadReportData()
        {
            try
            {
                AppSettings appSett = Helper.AppSet;
                decimal totalSales = items.Sum(a => a.TotalPrice);
                decimal disc = items.Sum(a => a.Discount);
                string discStr = disc == 0 ? "0.0" : disc.ToString("n2");

                decimal netSales = items.Sum(a => a.NetPrice);
                decimal fullCreditSales = items.Where(a => a.FincType == "Credit").Sum(a => a.NetPrice);
                decimal partialCreditSales = items.Where(a => a.FincType == "Partial").Sum(a => a.NetPrice - a.AmountGiven);
                decimal creditAmount = fullCreditSales + partialCreditSales;
                decimal cashSales = netSales - creditAmount;

                decimal laborExp = items.Sum(a => a.LaborExpenses);
                decimal bardanaExp = items.Sum(a => a.BardanaExpenses);
                decimal extraCashAmount = items.Where(a => a.IsExtraAmounted).Sum(a => a.ExtraAmount);
                decimal cashInHand = cashSales + extraCashAmount;

                decimal netSalesFor56 = totalSales - disc - laborExp - bardanaExp;
                decimal totalWeight = items.Sum(a => a.Weight);
                decimal packing56kCount = totalWeight / 56;
                decimal unitKgPrice = netSalesFor56 / totalWeight;
                decimal packing56kPrice = 56 * unitKgPrice;
                netSales = totalSales - disc - laborExp - bardanaExp;
                SaleOrderHeader header = new SaleOrderHeader
                {
                    FactoryAddress = appSett.Address,
                    Barcode = null,
                    CashOrders = items.Count(a => a.FincType == "Cash"),
                    CreditOrders = items.Count(a => a.FincType == "Credit"),
                    Dated = datedstr,
                    FactoryLogo = appSett.Logo,
                    FactoryName = appSett.Name,
                    FactoryPhone = appSett.PhoneNo,
                    PartialOrders = items.Count(a => a.FincType == "Partial"),
                    Qrcode = null,
                    TotalBori = (int)items.Sum(a => a.Qty),
                    TotalDiscount = discStr,
                    TotalNetSales = netSales.ToString("n2"),
                    BardanaExpenses = bardanaExp.ToString("n2"),
                    LaborExpenses = laborExp.ToString("n2"),
                    TotalOrders = items.Count,
                    TotalSales = totalSales.ToString("n2"),
                    TotalWeight = totalWeight.ToString("n1"),
                    CashInHand = cashInHand,
                    CreditSales = creditAmount,
                    ExtraCashAmount = extraCashAmount
                };
                Pack56KRepModel pack56Model = new Pack56KRepModel
                {
                    Pack56KCount = packing56kCount,
                    UnitPrice = packing56kPrice
                };

                Code128BarcodeDraw bcode = BarcodeDrawFactory.Code128WithChecksum;
                Image img = bcode.Draw(Helper.Unum, 50);
                byte[] imgBytes = Gujjar.GetByteArrayFromImage(img);
                header.Barcode = imgBytes;

                CodeQrBarcodeDraw qrCode = BarcodeDrawFactory.CodeQr;
                Image img2 = qrCode.Draw("GBS Solutions, Burewala. 0323-9372084", 50);
                byte[] qrBytes = Gujjar.GetByteArrayFromImage(img2);
                header.Qrcode = qrBytes;

                if (headerList != null && headerList.Count > 0)
                {
                    headerList.Clear();
                    headerList = null;
                }
                headerList = new List<SaleOrderHeader> { header };

                if (reportLines != null && reportLines.Count > 0)
                {
                    reportLines.Clear();
                    reportLines = null;
                }
                reportLines = new List<SaleOrderLineReport>();

                if (pack56List != null && pack56List.Count > 0)
                {
                    pack56List.Clear();
                    pack56List = null;
                }

                pack56List = new List<Pack56KRepModel> { pack56Model };

                foreach (var item in items)
                {
                    SaleOrderLineReport line = new SaleOrderLineReport
                    {
                        Id = item.Id,
                        Bories = (int)item.Qty,
                        Customer = item.Customer,
                        DateTime = item.DateTime,
                        Discount = item.Discount.ToString("n2"),
                        NetSales = item.NetPrice.ToString("n2"),
                        TotalSales = item.TotalPrice.ToString("n2"),
                        Type = item.FincType,
                        Weight = item.Weight.ToString("n0") + "-Kg",
                        UnitPrice = item.UnitPrice.ToString("n1")
                    };
                    reportLines.Add(line);
                }
                isReportDataLoaded = true;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                isReportDataLoaded = false;
                userName = "All Users";
                items = saleOrderVMBindingSource.List.OfType<SaleOrderVM>().ToList();
                if (items.Count == 0)
                {
                    throw new Exception("There is not item to display in report");
                }

                WaitForm wait = new WaitForm(LoadReportData);
                wait.ShowDialog();

                if (isReportDataLoaded)
                {
                    SaleOrdersReport rep = new SaleOrdersReport();

                    DetailReportBand hBand = rep.Bands["DetailReport"] as DetailReportBand;
                    DetailReportBand cBand = rep.Bands["DetailReport1"] as DetailReportBand;
                    DetailReportBand pBand = rep.Bands["DetailReport2"] as DetailReportBand;

                    hBand.DataSource = headerList;
                    cBand.DataSource = reportLines;
                    pBand.DataSource = pack56List;

                    var user2 = rep.Parameters["repUser"];
                    user2.Value = userName;
                    user2.Visible = false;
                    var lengendsParam = rep.Parameters["Legends"];
                    lengendsParam.Visible = false;
                    lengendsParam.Value = Helper.Legends();
                    rep.ShowPrintMarginsWarning = false;
                    rep.ShowPrintStatusDialog = false;

                    rep.ShowRibbonPreview();
                }
                else
                {
                    throw new Exception("Report data is not loaded...(unknown reasons");
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnTodayOnly_Click(object sender, EventArgs e)
        {
            try
            {
                datedstr = string.Format("Order sales report dated ({0})", DateTime.Now.ToString());

                IsDated = false;
                IsDated = false;
                WaitForm wait1 = new WaitForm(LoadSaleOrders);
                wait1.ShowDialog();

                UpdateVMList(saleOrders);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                IsDated = true;
                dtFrom = dtpFrom.Value.Date;
                dtTo = dtpTo.Value.Date;
                datedstr = string.Format("Order sales report from ({0}) to ({1})", dtFrom.ToShortDateString(), dtTo.ToShortDateString());

                WaitForm wait1 = new WaitForm(LoadSaleOrders);
                wait1.ShowDialog();

                UpdateVMList(saleOrders);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnReportSelf_Click(object sender, EventArgs e)
        {
            try
            {
                isReportDataLoaded = false;
                userName = string.Format("{0}", appUser.Name);
                items = saleOrderVMBindingSource.List.OfType<SaleOrderVM>().ToList();
                if (items.Count == 0)
                {
                    throw new Exception("There is not item to display in report");
                }

                WaitForm wait = new WaitForm(LoadReportDataUser);
                wait.ShowDialog();

                if (isReportDataLoaded)
                {
                    SaleOrdersReport rep = new SaleOrdersReport();

                    DetailReportBand hBand = rep.Bands["DetailReport"] as DetailReportBand;
                    DetailReportBand cBand = rep.Bands["DetailReport1"] as DetailReportBand;
                    DetailReportBand pBand = rep.Bands["DetailReport2"] as DetailReportBand;

                    hBand.DataSource = headerList;
                    cBand.DataSource = reportLines;
                    pBand.DataSource = pack56List;

                    var user2 = rep.Parameters["repUser"];
                    user2.Value = userName;
                    user2.Visible = false;
                    var lengendsParam = rep.Parameters["Legends"];
                    lengendsParam.Visible = false;
                    lengendsParam.Value = Helper.Legends();

                    rep.ShowPrintMarginsWarning = false;
                    rep.ShowPrintStatusDialog = false;

                    rep.ShowRibbonPreview();
                }
                else
                {
                    throw new Exception("Report data is not loaded...(unknown reasons");
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void LoadReportDataUser()
        {
            try
            {
                AppSettings appSett = Helper.AppSet;
                var items2 = items.Where(a => a.UserId == appUser.Id).ToList();


                decimal totalSales = items2.Sum(a => a.TotalPrice);
                decimal disc = items2.Sum(a => a.Discount);
                string discStr = disc == 0 ? "0.0" : disc.ToString("n2");

                decimal netSales = items2.Sum(a => a.NetPrice);
                decimal fullCreditSales = items2.Where(a => a.FincType == "Credit").Sum(a => a.NetPrice);
                decimal partialCreditSales = items2.Where(a => a.FincType == "Partial").Sum(a => a.NetPrice - a.AmountGiven);
                decimal creditAmount = fullCreditSales + partialCreditSales;
                decimal cashSales = netSales - creditAmount;

                decimal laborExp = items2.Sum(a => a.LaborExpenses);
                decimal bardanaExp = items2.Sum(a => a.BardanaExpenses);
                decimal extraCashAmount = items2.Where(a => a.IsExtraAmounted).Sum(a => a.ExtraAmount);
                decimal cashInHand = cashSales + extraCashAmount;

                decimal netSalesFor56 = totalSales - disc - laborExp - bardanaExp;

                decimal totalWeight = items2.Sum(a => a.Weight);
                decimal packing56kCount = totalWeight / 56;
                decimal unitKgPrice = netSalesFor56 / totalWeight;
                decimal packing56kPrice = 56 * unitKgPrice;
                netSales = totalSales - disc - laborExp - bardanaExp;
                SaleOrderHeader header = new SaleOrderHeader
                {
                    FactoryAddress = appSett.Address,
                    Barcode = null,
                    CashOrders = items2.Count(a => a.FincType == "Cash"),
                    CreditOrders = items2.Count(a => a.FincType == "Credit"),
                    Dated = datedstr,
                    FactoryLogo = appSett.Logo,
                    FactoryName = appSett.Name,
                    FactoryPhone = appSett.PhoneNo,
                    PartialOrders = items2.Count(a => a.FincType == "Partial"),
                    Qrcode = null,
                    TotalBori = (int)items2.Sum(a => a.Qty),
                    TotalDiscount = discStr,
                    TotalNetSales = netSales.ToString("n2"),
                    BardanaExpenses = bardanaExp.ToString("n2"),
                    LaborExpenses = laborExp.ToString("n2"),
                    TotalOrders = items2.Count,
                    TotalSales = totalSales.ToString("n2"),
                    TotalWeight = totalWeight.ToString("n1"),
                    CashInHand = cashInHand,
                    CreditSales = creditAmount,
                    ExtraCashAmount = extraCashAmount
                };

                Code128BarcodeDraw bcode = BarcodeDrawFactory.Code128WithChecksum;
                Image img = bcode.Draw(Helper.Unum, 50);
                byte[] imgBytes = Gujjar.GetByteArrayFromImage(img);
                header.Barcode = imgBytes;

                CodeQrBarcodeDraw qrCode = BarcodeDrawFactory.CodeQr;
                Image img2 = qrCode.Draw("GBS Solutions, Burewala. 0323-9372084", 50);
                byte[] qrBytes = Gujjar.GetByteArrayFromImage(img2);
                header.Qrcode = qrBytes;

                if (headerList != null && headerList.Count > 0)
                {
                    headerList.Clear();
                    headerList = null;
                }
                headerList = new List<SaleOrderHeader> { header };

                if (reportLines != null && reportLines.Count > 0)
                {
                    reportLines.Clear();
                    reportLines = null;
                }
                reportLines = new List<SaleOrderLineReport>();

                Pack56KRepModel pack56Model = new Pack56KRepModel
                {
                    Pack56KCount = packing56kCount,
                    UnitPrice = packing56kPrice
                };

                if (pack56List != null && pack56List.Count > 0)
                {
                    pack56List.Clear();
                    pack56List = null;
                }

                pack56List = new List<Pack56KRepModel> { pack56Model };

                foreach (var item in items2)
                {
                    SaleOrderLineReport line = new SaleOrderLineReport
                    {
                        Id = item.Id,
                        Bories = (int)item.Qty,
                        Customer = item.Customer,
                        DateTime = item.DateTime,
                        Discount = item.Discount.ToString("n2"),
                        NetSales = item.NetPrice.ToString("n2"),
                        TotalSales = item.TotalPrice.ToString("n2"),
                        Type = item.FincType,
                        Weight = item.Weight.ToString("n0") + "-Kg",
                        UnitPrice = item.UnitPrice.ToString("n1")
                    };
                    reportLines.Add(line);
                }
                isReportDataLoaded = true;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void PrintOrder(SaleOrder saleOrder, string receiptType, ReceiptType recType)
        {
            Customer customer = null;
            List<OrderLineVM> lines = new List<OrderLineVM>();
            CompVM vm = new CompVM
            {
                Address = AppSett.Address,
                LogoImg = AppSett.Logo,
                Name = AppSett.Name,
                Phone = AppSett.PhoneNo
            };
            using (Context db = new Context())
            {
                customer = db.Customers.Find(saleOrder.CustomerId);
                var ordLines = db.SaleOrderLines.Where(a => a.SaleOrderId == saleOrder.Id).ToList();
                foreach (var ordLine in ordLines)
                {
                    ordLine.Product = db.Products.Find(ordLine.ProductId);

                    OrderLineVM vm4 = new OrderLineVM
                    {
                        ApplyLaborExpense = ordLine.ApplyLaborExpense,
                        Id = ordLine.Id.ToString(),
                        DeductBardanaExpense = ordLine.DeductBardanaExpense,
                        DiscountPrice = ordLine.Discount,
                        DiscPercentage = 0,
                        NetPrice = ordLine.NetPrice,
                        ProductName = ordLine.Product.Title,
                        ProduuctId = ordLine.ProductId,
                        Qty = ordLine.Qty,
                        UnitPrice = ordLine.UnitPrice
                    };
                    lines.Add(vm4);
                }
            }


            Code128BarcodeDraw bcode = BarcodeDrawFactory.Code128WithChecksum;
            Image img = bcode.Draw(saleOrder.Unum, 50);
            byte[] imgBytes = Gujjar.GetByteArrayFromImage(img);
            CodeQrBarcodeDraw qrCode = BarcodeDrawFactory.CodeQr;
            Image img2 = qrCode.Draw("GBS Solutions, Burewala. 0323-9372084", 50);
            byte[] qrBytes = Gujjar.GetByteArrayFromImage(img2);

            OrdVM ordvm = new OrdVM
            {
                CustAddress = customer.Address,
                ContactNo = customer.Contact,
                CustName = string.Format("{0} ({1})", customer.Name, customer.Contact),
                Dated = saleOrder.TimpStamp.ToString(),
                OderNo = saleOrder.Id.ToString().PadLeft(5, '0'),
                QrImg = qrBytes,
                TotalItems = saleOrder.TotalItems,
                UniqItems = saleOrder.UniqueItems,
                Unum = imgBytes,
                InvcType = saleOrder.OrderType.ToString(),
                ReceiptType = receiptType,

            };
            List<LineVM> lineVmList = lines.Select(a => new LineVM
            {
                Product = a.ProductName,
                Qty = a.Qty,
                TotalPrice = a.TotalPrice,
                UnitPrice = a.UnitPrice
            }).ToList();

            OrdDetails ordDetails = new OrdDetails
            {
                NetAmount = saleOrder.NetPrice,
                SaleTaxAmount = saleOrder.SaleTaxAmount,
                SaleTaxPercentage =
                saleOrder.SaleTaxPercentage,
                ServiceCharges = saleOrder.ServiceCharges,
                TotalAmount = saleOrder.TototPrice,
                TotalDiscount = saleOrder.TotalDiscount,
                TotalDiscountPercentage = saleOrder.TotalDiscountPercentage,
                Operator = AppUser.Name
            };
            PrintInvoice(vm, ordvm, lineVmList, ordDetails, recType);
        }
        private void PrintInvoice(CompVM companyVM, OrdVM ordVm, List<LineVM> lineVmList, OrdDetails ordDetails, ReceiptType rType)
        {
            try
            {
                List<CompVM> companies = new List<CompVM> { companyVM };
                List<OrdVM> ordVMList = new List<OrdVM> { ordVm };
                List<OrdDetails> ordDetailsList = new List<OrdDetails> { ordDetails };
                XtraReport invoice = null;
                switch (rType)
                {
                    case ReceiptType.Customer:
                        invoice = new SaleOrderInvoice();
                        break;
                    case ReceiptType.Floorscale:
                        invoice = new SaleInvoiceFloorScale();
                        break;
                    case ReceiptType.Gatepass:
                        invoice = new SaleInvoiceGatePass();
                        break;
                    case ReceiptType.Office:
                        invoice = new SaleInvoiceOffice();
                        break;
                }


                DetailReportBand band1 = invoice.Bands["DetailReport"] as DetailReportBand;
                band1.DataSource = companies;

                DetailReportBand band2 = invoice.Bands["DetailReport1"] as DetailReportBand;
                band2.DataSource = ordVMList;

                DetailReportBand band3 = invoice.Bands["DetailReport2"] as DetailReportBand;
                band3.DataSource = lineVmList;

                DetailReportBand band4 = invoice.Bands["DetailReport3"] as DetailReportBand;
                band4.DataSource = ordDetailsList;



                invoice.ShowPrintMarginsWarning = false;
                invoice.ShowPrintStatusDialog = false;
                invoice.Print(AppSett.ThermalPrinter);
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
