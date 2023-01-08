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
using Model.Retail.ViewModel;
using System.Data.Entity;
using Model.Retail.Model;
using System.Threading;
using Model.AppCompany.ViewModel;
using WinFom.Retail.Reports.XtraReports;
using DevExpress.XtraReports.UI;
using WinFom.Retail.Reports.ViewModel;
using Zen.Barcode;
using Model.Financials.Model;
using Model.Employees.Model;
using Model.RetailBardanaManaged.Model;
using WinFom.Financials.Forms;

namespace WinFom.Retail.Forms
{
    public partial class SaleOrderForm : Form
    {
        private Random rnd = new Random();
        private List<OrderLineVM> orderLines = new List<OrderLineVM>();
        private List<OrderProdVM> productVMList = new List<OrderProdVM>();
        private OrderProdVM tempProd = null;
        private Color tbBackColor = Color.Gainsboro;
        private Customer customer = null;
        private AppSettings AppSett = Helper.AppSet;
        private DateTime OrderDate = DateTime.Now.Date;
        private AppUser AppUser = SingleTon.LoginForm.appUser;
        private RetailBardanaItem bardanaItem = null;
        private bool shouldCustomerPrint = false;
        private decimal totalBardanaBharti = 0;
        public SaleOrderForm()
        {
            InitializeComponent();
        }

        private void LoadProducts2()
        {
            try
            {
                using (Context db = new Context())
                {
                    productVMList = SingleTon.LoginForm.AppProducts
                        .Select(a =>
                        new OrderProdVM
                        {
                            Id = a.Id,
                            SKU = a.SKU,
                            AlertOnSale = a.AlertOnSale,
                            Barcode = a.Barcode,
                            IsDicountable = a.IsDicountable,
                            Title = string.Format("{0}-{1}", a.Title, a.ProductSize.Title),
                            ProductDiscountAmount = a.ProductDiscount,
                            NetPrice = a.ProductNetUnitPrice,
                            ProductDiscountPercentage = a.ProductDiscPercentage,
                            UnitPrice = a.ProductTotalUnitPrice,
                            Qty = 1,
                            ProductWholeSaleAmount = a.ProductNetUnitPriceWholeSale,
                            ApplyLaborExpense = a.ApplyLaborExpense,
                            DeductBardanaExpense = a.DeductBardanaPacking
                        })
                        .ToList();

                }
            }
            catch (Exception exp)
            {

                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadProducts()
        {
            try
            {
                using (Context db = new Context())
                {
                    productVMList = db.Products.Include(a => a.ProductSize)
                        .AsParallel().ToList().Where(a => a.IsAvailable)
                        .Select(a =>
                        new OrderProdVM
                        {
                            Id = a.Id,
                            SKU = a.SKU,
                            AlertOnSale = a.AlertOnSale,
                            Barcode = a.Barcode,
                            IsDicountable = a.IsDicountable,
                            Title = string.Format("{0}-{1}", a.Title, a.ProductSize.Title),
                            ProductDiscountAmount = a.ProductDiscount,
                            NetPrice = a.ProductNetUnitPrice,
                            ProductDiscountPercentage = a.ProductDiscPercentage,
                            UnitPrice = a.ProductTotalUnitPrice,
                            Qty = 1,
                            ProductWholeSaleAmount = a.ProductNetUnitPriceWholeSale,
                            ApplyLaborExpense = a.ApplyLaborExpense,
                            DeductBardanaExpense = a.DeductBardanaPacking
                        })
                        .ToList();

                }
            }
            catch (Exception exp)
            {

                Gujjar.ErrMsg(exp);
            }
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Centeral()
        {
            int sw = Screen.PrimaryScreen.Bounds.Width;
            int sh = Screen.PrimaryScreen.Bounds.Height;

            int pw = mainPanel.Width;
            int ph = mainPanel.Height;

            int dw = sw - pw;
            int dh = sh - ph;

            int ddw = dw / 2;
            int ddh = dh / 2;

            if (ddh > 50)
            {
                ddh = 50;
            }
            mainPanel.Location = new Point(ddw, ddh);
            timer1.Interval = 60000;
            timer1.Start();
        }

        private void BindCustomers()
        {
            cbCustomers.DataSource = null;
            cbCustomers.Items.Clear();
            cbCustomers.DataSource = SingleTon.LoginForm.AppCustomers;
            cbCustomers.ValueMember = "Id";
            cbCustomers.DisplayMember = "Name";
            cbCustomers.Refresh();
            cbCustomers.SelectedItem = cbCustomers.Items.OfType<Customer>()
                .FirstOrDefault(a => a.Id == 1);
        }
        private void LoadInitialData()
        {
            try
            {
                //WaitForm wait1 = new WaitForm(LoadProducts);
                //wait1.ShowDialog();
                LoadProducts2();
                timer2.Start();
                orderDateLabel.Text = OrderDate.ToShortDateString();
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
                Centeral();
                Helper.IsOkApplied();
                using (Context db = new Context())
                {
                    bardanaItem = db.RetailBardanaItems.FirstOrDefault();
                    if (bardanaItem == null)
                    {
                        Gujjar.ErrMsg("Retail bardana (packing) is not defined in system. Please add bardana (packing in system");
                        if (AppUser.Id == "admin1")
                        {
                            AURetailBardanaItem form = new AURetailBardanaItem();
                            form.ShowDialog();

                            bardanaItem = db.RetailBardanaItems.FirstOrDefault();
                            if (bardanaItem == null)
                            {
                                Gujjar.InfoMsg("Please add bardana for retail sales. Or contact to administrator. Thanks");
                                Close();
                            }
                        }
                        else
                        {
                            Close();
                        }

                    }
                }

                LoadInitialData();
                Gujjar.DGVDesign(dgv);
                tbBackColor = tbProdDiscAmount.BackColor;
                ClearHeader();
                BindCustomers();
                FromAppSettings();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void TimeTimer_Tick(object sender, EventArgs e)
        {
            if (labelTime.InvokeRequired)
            {
                labelTime.Invoke(new Action(() =>
                {
                    labelTime.Text = DateTime.Now.ToShortTimeString();
                }));
            }
            labelTime.Text = DateTime.Now.ToShortTimeString();
        }

        private void FromAppSettings()
        {
            tbSalesTaxPercentage.Text = AppSett.SalesTaxPercentage.ToString("n2");
            tbOfferDiscountPercentage.Text = AppSett.OfferDiscountPercentage.ToString("n2");
            tbServiceCharges.Text = AppSett.ServiceCharges.ToString("n2");
            tbSalesTax.Text = "0.00";
            tbServiceChargesPercentage.Text = "0.00";
            tbOfferDiscountAmount.Text = "0.00";

            if (AppSett.EnableDiscounts)
            {
                tbOrderDiscountAmount.BackColor = tbOrderDiscountPercentage.BackColor = Color.LightGreen;
                tbOrderDiscountPercentage.Text = tbOrderDiscountAmount.Text = "0.00";
                tbOrderDiscountAmount.Enabled = tbOrderDiscountPercentage.Enabled = true;
            }
            else
            {
                tbOrderDiscountAmount.BackColor = tbOrderDiscountPercentage.BackColor = Color.Pink;
                tbOrderDiscountPercentage.Text = tbOrderDiscountAmount.Text = "0.00";
                tbOrderDiscountAmount.Enabled = tbOrderDiscountPercentage.Enabled = false;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            int n = rnd.Next(8) + 1;
            switch (n)
            {
                case 1:
                    pMain.BackColor = Color.Gainsboro;
                    break;
                case 2:
                    pMain.BackColor = Color.DodgerBlue;
                    break;
                case 3:
                    pMain.BackColor = Color.Coral;
                    break;
                case 4:
                    pMain.BackColor = Color.Teal;
                    break;
                case 5:
                    pMain.BackColor = Color.Maroon;
                    break;
                case 8:
                    pMain.BackColor = Color.Sienna;
                    break;
                case 6:
                    pMain.BackColor = Color.SeaGreen;
                    break;
                case 7:
                    pMain.BackColor = Color.Violet;
                    break;
            }


        }

        private void ClearHeader()
        {
            textBoxBarcode.Clear();
            tbProdName.Clear();
            tbProdPrice.Clear();
            tbProdDiscPercentage.Clear();
            tbProdDiscAmount.Clear();
            tbProdNetAmount.Clear();

            tbProdDiscAmount.BackColor = tbBackColor;
            tbProdDiscPercentage.BackColor = tbBackColor;
            textBoxBarcode.Focus();
        }
        private void btnProdSearch_Click(object sender, EventArgs e)
        {
            FetchProductInHeader();
        }

        public void UpdateHeader(OrderProdVM prod)
        {
            tbProdName.Text = prod.Title;
            tbItemQty.Text = "1";
            if (customer.Id == 1 || customer.IsEmployee)
            {
                tbProdPrice.Text = prod.TotalPrice.ToString("n2");
                //tbProdNetAmount.Text = prod.NetPrice.ToString("n2");
                tbProdNetAmount.Text = prod.TotalPrice.ToString("n2");
            }
            else
            {
                tbProdPrice.Text = prod.ProductWholeSaleAmount.ToString("n2");
                //tbProdNetAmount.Text = prod.TotalPrice.ToString("n2");
                tbProdNetAmount.Text = prod.ProductWholeSaleAmount.ToString("n2");
            }


            if (prod.IsDicountable && AppSett.EnableDiscounts)
            {
                tbProdDiscAmount.BackColor = tbProdDiscPercentage.BackColor = Color.LightGreen;
                tbProdDiscPercentage.Text = prod.ProductDiscountPercentage.ToString("n2");
                tbProdDiscAmount.Text = prod.ProductDiscountAmount.ToString("n2");
                tbProdDiscAmount.Enabled = prod.IsDicountable;
                tbProdDiscPercentage.Enabled = prod.IsDicountable;

            }
            else
            {
                tbProdDiscAmount.BackColor = tbProdDiscPercentage.BackColor = Color.Pink;
                tbProdDiscPercentage.Text = "0";
                tbProdDiscAmount.Text = "0";
                tbProdDiscAmount.Enabled = prod.IsDicountable;
                tbProdDiscPercentage.Enabled = prod.IsDicountable;

            }

            tbItemQty.Focus();
            tbItemQty.Select(0, 1);
            tempProd = prod;
        }
        private void FetchProductInHeader()
        {
            try
            {
                ClearHeader();
                var data = productVMList.Select(a => new ProdSearchVM { Id = a.Id, Title = a.Title }).ToList();
                ProdSearchForm form = new ProdSearchForm(data);
                form.ShowDialog();

                int pid = form.ProdId;
                if (pid != 0)
                {
                    var prod = productVMList.First(a => a.Id == pid);
                    UpdateHeader(prod);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void tbItemQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal unitPrice = 0;
                string txt1 = tbProdPrice.Text;
                if (!string.IsNullOrEmpty(txt1))
                {
                    unitPrice = txt1.ToDecimal();
                }

                decimal qty = 0;
                string txt2 = tbItemQty.Text;
                if (!string.IsNullOrEmpty(txt2))
                {
                    qty = tbItemQty.Text.ToDecimal();
                }

                decimal disPercen = 0;
                string txt = tbProdDiscPercentage.Text;
                if (!string.IsNullOrEmpty(txt))
                {
                    disPercen = txt.ToDecimal();
                }

                decimal disAmnt = 0;
                string txt3 = tbProdDiscAmount.Text;
                if (!string.IsNullOrEmpty(txt3))
                {
                    disAmnt = txt3.ToDecimal() * qty;
                }

                decimal total = unitPrice * qty;

                decimal netAmount = total - disAmnt;
                tbProdNetAmount.Text = netAmount.ToString("n2");

                if (total > 0)
                {
                    disPercen = disAmnt / total * 100;
                }
                tbProdDiscPercentage.Text = disPercen.ToString("n2");
            }
            catch (FormatException)
            {

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void Header_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsHeaderOk() && tempProd != null)
            {
                OrderLineVM vm = new OrderLineVM
                {
                    DiscountPrice = tbProdDiscAmount.Text.ToDecimal(),
                    DiscPercentage = tbProdDiscPercentage.Text.ToDecimal(),
                    Id = tempProd.Barcode,
                    NetPrice = tbProdNetAmount.Text.ToDecimal(),
                    ProductName = tempProd.Title,
                    ProduuctId = tempProd.Id,
                    Qty = tbItemQty.Text.ToDecimal(),
                    UnitPrice = tbProdPrice.Text.ToDecimal(),
                    ApplyLaborExpense = tempProd.ApplyLaborExpense,
                    DeductBardanaExpense = tempProd.DeductBardanaExpense
                };

                AddCartLine(vm);
                tempProd = null;
                ClearHeader();
                textBoxBarcode.Focus();
            }
            if (e.KeyCode == Keys.Down && dgv.Rows.Count > 0)
            {
                SelectDgv();
            }
        }
        private void SelectDgv()
        {
            dgv.Focus();

            dgv.Rows[0].Selected = true;
        }
        private void AddCartLine(OrderLineVM vm)
        {
            try
            {
                var obj = orderLineVMBindingSource.List.OfType<OrderLineVM>()
                    .FirstOrDefault(a => a.Id == vm.Id);
                if (obj != null)
                {
                    obj.Qty += vm.Qty;
                    obj.DiscountPrice += vm.DiscountPrice;
                    obj.NetPrice += vm.NetPrice;
                    if (obj.TotalPrice > 0)
                        obj.DiscPercentage = obj.DiscountPrice / obj.TotalPrice;
                    else
                        obj.DiscPercentage = 0;
                }
                else
                {
                    if (vm.TotalPrice > 0)
                    {
                        //vm.DiscPercentage = vm.DiscountPrice / vm.TotalPrice;
                    }
                    else
                    {
                        vm.DiscPercentage = 0;
                    }
                    orderLineVMBindingSource.List.Add(vm);
                }
                ClearHeader();
                dgv.Refresh();
                dgv.ClearSelection();

                UpdateCounting();

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private bool IsHeaderOk()
        {
            string txt1 = tbProdName.Text;
            string txt2 = tbItemQty.Text;
            string txt3 = tbProdPrice.Text;
            string txt4 = tbProdDiscAmount.Text;
            string txt5 = tbProdDiscPercentage.Text;
            string txt6 = tbProdNetAmount.Text;

            return
                (!string.IsNullOrEmpty(txt1) && !string.IsNullOrEmpty(txt2) && !string.IsNullOrEmpty(txt3) && !string.IsNullOrEmpty(txt4) && !string.IsNullOrEmpty(txt5) && !string.IsNullOrEmpty(txt6));
        }
        private void SaleOrderForm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    FetchProductInHeader();
                }
                if (e.KeyCode == Keys.Escape)
                {
                    ClearHeader();
                    textBoxBarcode.Focus();
                    dgv.ClearSelection();
                }
                if (e.Control && e.KeyCode == Keys.S)
                {
                    ProcessOrder();
                }
                if (e.Control && e.KeyCode == Keys.C)
                {
                    SearchCustomerForm form = new SearchCustomerForm(SingleTon.LoginForm.AppCustomers);
                    form.ShowDialog();

                    if (form.CustomerId != 0)
                    {
                        cbCustomers.SelectedItem = cbCustomers.Items.OfType<Customer>()
                            .FirstOrDefault(a => a.Id == form.CustomerId);
                    }
                    else
                    {
                        cbCustomers.SelectedItem = cbCustomers.Items.OfType<Customer>()
                           .FirstOrDefault(a => a.Id == 1);
                    }
                }
                if (e.Control && e.KeyCode == Keys.P)
                {
                    bswPrinterState.Value = !bswPrinterState.Value;
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        decimal totalWeight = 0;
        decimal totalPackings = 0;
        int bardanaPackingCount = 0;
        private void ProcessOrder()
        {
            try
            {
                bardanaPackingCount = 0;
                totalBardanaBharti = 0;
                shouldCustomerPrint = AppSett.CustomerCopies > 0 ? true : false;
                totalWeight = 0;
                totalPackings = 0;
                var lines = orderLineVMBindingSource.List.OfType<OrderLineVM>().ToList();
                if (lines.Count == 0)
                {
                    throw new Exception("Please add some items in cart before to proceed order processing");
                }

                //DialogResult res = Gujjar.ConfirmYesNo("Please confirm!!... Are you sured to process this order?");
                //if (res == DialogResult.No)
                //    return;

                decimal dueAmount = tbDueAmount.Text.ToDecimal();
                OrderProcessForm form = new OrderProcessForm(customer, dueAmount, (int)numCopies.Value, bswPrinterState.Value, shouldCustomerPrint);
                form.ShowDialog();

                var ordModel = form.OrderProcessViewModel;
                if (ordModel != null)
                {
                    SaleOrder saleOrder = new SaleOrder
                    {
                        Id = 0,
                        AmountGiven = ordModel.AmountGiven,
                        ChangeGiven = ordModel.ChangeGiven,
                        Customer = null,
                        CustomerDiscount = tbCustomerDiscount.Text.ToDecimal(),
                        CustomerDiscountPecentage = tbCustomerDiscountPercentage.Text.ToDecimal(),
                        OrderDiscountAmount = tbOrderDiscountAmount.Text.ToDecimal(),
                        CustomerId = customer.Id,
                        IsCredit = ordModel.IsCredit,
                        SaleTaxAmount = tbSalesTax.Text.ToDecimal(),
                        IsWalkIn = ordModel.IsWalkIn,
                        NetPrice = tbDueAmount.Text.ToDecimal(),
                        OfferDiscount = tbOfferDiscountAmount.Text.ToDecimal(),
                        OfferDiscountPercentage = tbOfferDiscountPercentage.Text.ToDecimal(),
                        OrderDate = OrderDate,
                        OrderDiscount = tbOrderDiscountAmount.Text.ToDecimal(),
                        OrderId = "N/A",
                        IsExpensed = false,
                        OrderLines = null,
                        IsExtraAmounted = false,
                        SaleTaxPercentage = tbSalesTaxPercentage.Text.ToDecimal(),
                        ServiceCharges = tbServiceCharges.Text.ToDecimal(),
                        ServiceChargesPercentage = tbServiceChargesPercentage.Text.ToDecimal(),
                        TimpStamp = DateTime.Now,
                        TotalDiscount = tbTotalDiscount.Text.ToDecimal(),
                        TotalDiscountPercentage = tbTotalDiscountPercentage.Text.ToDecimal(),
                        TotalItems = tbItemQty.Text.ToInt(),
                        TototPrice = tbTotalAmount.Text.ToDecimal(),
                        UniqueItems = tbItems.Text.ToInt(),
                        Unum = Helper.Unum,
                        RemainingAmount = ordModel.RemainingAmount,
                        OrderType = ordModel.OrderType,
                        IsDone = false,
                        AppUserId = AppUser.Id
                    };

                    using (Context db = new Context())
                    {
                        using (var trans = db.Database.BeginTransaction())
                        {
                            try
                            {
                                saleOrder = db.SaleOrders.Add(saleOrder);
                                db.SaveChanges();

                                foreach (var ln in lines)
                                {
                                    SaleOrderLine line = new SaleOrderLine
                                    {
                                        Id = 0,
                                        Discount = ln.DiscountPrice,
                                        NetPrice = ln.NetPrice,
                                        Product = null,
                                        ProductId = ln.ProduuctId,
                                        Qty = ln.Qty,
                                        SaleOrder = null,
                                        SaleOrderId = saleOrder.Id,
                                        TotalPrice = ln.TotalPrice,
                                        UnitPrice = ln.UnitPrice,
                                        ApplyLaborExpense = ln.ApplyLaborExpense,
                                        DeductBardanaExpense = ln.DeductBardanaExpense
                                    };
                                    totalWeight += db.Products.Find(ln.ProduuctId).Weight * ln.Qty;
                                    if (ln.DeductBardanaExpense)
                                        bardanaPackingCount += (int)ln.Qty;

                                    totalPackings += ln.Qty;

                                    if (ln.ApplyLaborExpense)
                                        totalBardanaBharti += ln.Qty;

                                    db.SaleOrderLines.Add(line);


                                }

                                var dbBardana = db.RetailBardanaItems.First();
                                dbBardana.SKU -= totalPackings;
                                db.Entry(dbBardana).State = EntityState.Modified;
                                db.SaveChanges();

                                if (ordModel.PrinterState)
                                {
                                    for (int i = 0; i < ordModel.Copies; i++)
                                    {
                                        for (int gi = 0; gi < AppSett.GatePassCopies; gi++)
                                        {
                                            PrintOrder(saleOrder, lines, "Gate Pass Copy", ReceiptType.Gatepass);
                                        }
                                        for (int floorScale = 0; floorScale < AppSett.FloorScaleCopies; floorScale++)
                                        {
                                            PrintOrder(saleOrder, lines, "Floor Scale Copy", ReceiptType.Floorscale);
                                        }
                                        if (ordModel.CustomerPrintCopy)
                                        {
                                            for (int cc = 0; cc < AppSett.CustomerCopies; cc++)
                                            {
                                                PrintOrder(saleOrder, lines, "Customer Copy", ReceiptType.Customer);
                                            }
                                        }
                                        for (int oc = 0; oc < AppSett.OfficeCopies; oc++)
                                        {
                                            PrintOrder(saleOrder, lines, "Office Copy", ReceiptType.Office);
                                        }
                                    }
                                }

                                #region Financials
                                if (saleOrder.OrderType != SaleOrderType.Cash && customer.Id != 1)
                                {
                                    #region "Product financial Credit entry"
                                    string creditAccountId = Properties.Resources.OilCakeRetailSaleAccount;

                                    string prodFinMessage = "";
                                    decimal debitAmount = 0;

                                    if (saleOrder.OrderType == SaleOrderType.Credit)
                                    {
                                        prodFinMessage = string.Format("Credit Order Sale: Order No ({0}), Packing qty ({1}-Bori), Total Qty ({2}-Kg), Order Sale Amount ({3}), Customer ({4}) Date time ({5}). By ({6})",
                                        saleOrder.Id, saleOrder.OrderLines.Sum(a => a.Qty).ToString("n1"),
                                        totalWeight.ToString("n1"), saleOrder.NetPrice.ToString("n2"), customer.Name, DateTime.Now.ToString(), AppUser.Name);
                                        debitAmount = saleOrder.NetPrice;
                                    }
                                    else
                                    {
                                        prodFinMessage = string.Format("Partial Credit Order Sale: Order No ({0}), Packing qty ({1}-Bori), Total Qty ({2}-Kg), Order Sale Amount ({3}), Amount given ({4}), Remaining amount ({5}), Customer ({6}) Date time ({7}). By ({8})",
                                        saleOrder.Id, saleOrder.OrderLines.Sum(a => a.Qty).ToString("n1"), totalWeight.ToString("n1"), saleOrder.NetPrice.ToString("n2"), saleOrder.AmountGiven.ToString("n2"), saleOrder.RemainingAmount.ToString("n2"), customer.Name, DateTime.Now.ToString(), AppUser.Name);
                                        debitAmount = saleOrder.RemainingAmount;
                                    }
                                    if (customer.IsEmployee)
                                    {
                                        prodFinMessage = string.Format("Employee advance through sale order: Employee ({0}). {1}",
                                            customer.Name, prodFinMessage);

                                    }
                                    DayBook prodDayBookEntry = new DayBook
                                    {
                                        Id = 0,
                                        Amount = debitAmount,
                                        Date = DateTime.Now,
                                        Description = prodFinMessage,
                                        CanRollBack = false,
                                        SaleOrderId = saleOrder.Id,

                                        InDate = DateTime.Now.Date
                                    };
                                    if (saleOrder.OrderType == SaleOrderType.Partial)
                                    {
                                        prodDayBookEntry.Amount = debitAmount;
                                    }

                                    prodDayBookEntry = db.DayBooks.Add(prodDayBookEntry);
                                    db.SaveChanges();

                                    if (customer.IsEmployee)
                                    {
                                        CreditEntry creditEntry = new CreditEntry
                                        {
                                            Amount = debitAmount,
                                            Date = DateTime.Now,
                                            DayBookId = prodDayBookEntry.Id,
                                            Id = 0,
                                            Employee = null,
                                            EmployeeId = customer.EmpId.Value,
                                            Remarks = prodFinMessage
                                        };
                                        db.CreditEntries.Add(creditEntry);
                                        db.SaveChanges();
                                    }

                                    AccountTransaction prodCreditTrans = new AccountTransaction
                                    {
                                        Account = null,
                                        Description = prodFinMessage,
                                        AccountTransactionType = AccountTransactionType.Credit,
                                        CreditAmount = debitAmount,
                                        Balance = debitAmount,
                                        Date = saleOrder.OrderDate,
                                        DayBookId = prodDayBookEntry.Id,
                                        DebitAmount = 0,
                                        Id = 0,
                                        GeneralAccountId = creditAccountId
                                    };

                                    var dbProdEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == creditAccountId)
                                        .AsParallel().ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                        .OrderByDescending(a => a.Id)
                                        .FirstOrDefault();
                                    if (dbProdEntry != null)
                                    {
                                        prodCreditTrans.Balance += dbProdEntry.Balance;
                                    }

                                    prodCreditTrans = db.AccountTransactions.Add(prodCreditTrans);
                                    db.SaveChanges();
                                    #endregion

                                    string debitAccountId = customer.GeneralAccountId;

                                    if (customer.IsEmployee)
                                    {
                                        debitAmount = -debitAmount;
                                    }

                                    #region "Debit transaction"
                                    AccountTransaction debitTrans = new AccountTransaction
                                    {
                                        Id = 0,
                                        GeneralAccountId = debitAccountId,
                                        Account = null,
                                        AccountTransactionType = AccountTransactionType.Debit,
                                        CreditAmount = 0,
                                        Balance = debitAmount,
                                        Date = saleOrder.OrderDate,
                                        DayBookId = prodDayBookEntry.Id,
                                        DebitAmount = Math.Abs(debitAmount),
                                        Description = prodFinMessage
                                    };

                                    var dbDebitEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == debitAccountId)
                                        .AsParallel().ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                        .OrderByDescending(a => a.Id)
                                        .FirstOrDefault();
                                    if (dbDebitEntry != null)
                                    {
                                        debitTrans.Balance += dbDebitEntry.Balance;
                                    }
                                    debitTrans = db.AccountTransactions.Add(debitTrans);
                                    db.SaveChanges();

                                    GeneralAccount debitAccount = db.Accounts.Find(debitAccountId) as GeneralAccount;
                                    GeneralAccount creditAccount = db.Accounts.Find(creditAccountId) as GeneralAccount;

                                    DayBook dbDayBook = db.DayBooks.Find(prodDayBookEntry.Id);
                                    dbDayBook.DebitTrace = string.Format("{0}. Trans Id: {1}", debitAccount.Title, debitTrans.Id);
                                    dbDayBook.CreditTrace = string.Format("{0}. Trans Id: {1}", creditAccount.Title, prodCreditTrans.Id);
                                    dbDayBook.DebitAccountId = debitAccount.Id;
                                    dbDayBook.CreditAccountId = creditAccount.Id;
                                    db.Entry(dbDayBook).State = EntityState.Modified;
                                    db.SaveChanges();

                                    if (AppSett.PrintFinancialTransactions)
                                    {
                                        Helper.PrintTransactions(new List<DayBook> { dbDayBook });
                                    }
                                    #endregion
                                }
                                if (saleOrder.OrderType == SaleOrderType.Partial && customer.Id != 1 && ordModel.RemainingAmount < 0)
                                {
                                    List<DayBook> pDaybooks = new List<DayBook>();
                                    #region "Financials"
                                    GeneralAccount cashDebitAccount = db.Accounts.Find(Properties.Resources.CashInHand) as GeneralAccount;
                                    GeneralAccount saleCreditAccount = db.Accounts.Find(Properties.Resources.OilCakeRetailSaleAccount) as GeneralAccount;
                                    GeneralAccount customerCreditAccount = db.Accounts.Find(customer.GeneralAccountId) as GeneralAccount;
                                    var user = SingleTon.LoginForm.appUser;
                                    decimal saleAmountCredit = saleOrder.NetPrice;
                                    decimal extraAmountCredit = Math.Abs(saleOrder.RemainingAmount);
                                    decimal cashAoumtDebit = saleOrder.AmountGiven;

                                    string finMsg = string.Format("Cash sale with extra amount ({0}), net sale amount ({1}), amount given ({2}), customer ({3}). Order No ({4}), dated ({5}), by ({6})",
                                        extraAmountCredit.ToString("n1"), saleAmountCredit.ToString("n1"), cashAoumtDebit.ToString("n1"), customer.Name, saleOrder.Id, DateTime.Now.ToString(), user.Name);

                                    DayBook daybookEntry = new DayBook
                                    {
                                        Id = 0,
                                        Amount = cashAoumtDebit,
                                        Date = DateTime.Now,
                                        Description = finMsg + " (DB 1st)",
                                        CanRollBack = false,
                                        SaleOrderId = saleOrder.Id,
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
                                        Balance = cashAoumtDebit,
                                        CreditAmount = 0,
                                        Date = saleOrder.OrderDate,
                                        DayBookId = daybookEntry.Id,
                                        DebitAmount = cashAoumtDebit,
                                        GeneralAccountId = cashDebitAccount.Id,
                                        Description = finMsg
                                    };

                                    var debitDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == cashDebitAccount.Id).AsParallel()
                                        .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                        .OrderByDescending(a => a.Id).FirstOrDefault();

                                    if (debitDbEntry != null)
                                    {
                                        debitItemTrans.Balance += debitDbEntry.Balance;
                                    }

                                    debitItemTrans = db.AccountTransactions.Add(debitItemTrans);
                                    db.SaveChanges();

                                    var daybookdb1 = db.DayBooks.Find(daybookEntry.Id);
                                    daybookdb1.DebitTrace = string.Format("({0}). Trans Id: {1}", cashDebitAccount.Title, debitItemTrans.Id);
                                    daybookdb1.CreditTrace = string.Format("Partial Credit");
                                    daybookdb1.CreditAccountId = null;
                                    daybookdb1.DebitAccountId = cashDebitAccount.Id;
                                    db.Entry(daybookdb1).State = EntityState.Modified;
                                    db.SaveChanges();
                                    pDaybooks.Add(daybookdb1);
                                    #endregion

                                    #region  "Credit transaction"
                                    DayBook daybook2 = new DayBook
                                    {
                                        Id = 0,
                                        Amount = saleAmountCredit,
                                        CanRollBack = false,
                                        CreditAccountId = saleCreditAccount.Id,
                                        DebitAccountId = null,
                                        Date = DateTime.Now,
                                        Description = finMsg + " (DB 2nd)",
                                        SaleOrderId = saleOrder.Id,
                                        InDate = DateTime.Now.Date
                                    };
                                    daybook2 = db.DayBooks.Add(daybook2);
                                    db.SaveChanges();

                                    AccountTransaction creditItemTrans1 = new AccountTransaction
                                    {
                                        Id = 0,
                                        Account = null,
                                        AccountTransactionType = AccountTransactionType.Credit,
                                        Balance = saleAmountCredit,
                                        CreditAmount = saleAmountCredit,
                                        Date = DateTime.Now,
                                        DayBookId = daybook2.Id,
                                        DebitAmount = 0,
                                        GeneralAccountId = saleCreditAccount.Id,
                                        Description = finMsg
                                    };

                                    var creditDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == saleCreditAccount.Id).AsParallel()
                                        .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                        .OrderByDescending(a => a.Id).FirstOrDefault();

                                    if (creditDbEntry != null)
                                    {
                                        creditItemTrans1.Balance += creditDbEntry.Balance;
                                    }

                                    creditItemTrans1 = db.AccountTransactions.Add(creditItemTrans1);
                                    db.SaveChanges();

                                    var daybookdb2 = db.DayBooks.Find(daybook2.Id);
                                    daybookdb2.DebitTrace = string.Format("Partially credited");
                                    daybookdb2.CreditTrace = string.Format("({0}). Trans Id: {1}", saleCreditAccount.Title, creditItemTrans1.Id);
                                    daybookdb2.CreditAccountId = saleCreditAccount.Id;
                                    daybookdb2.DebitAccountId = null;
                                    db.Entry(daybookdb2).State = EntityState.Modified;
                                    db.SaveChanges();
                                    pDaybooks.Add(daybookdb2);
                                    #endregion

                                    #region  "Credit transaction"
                                    DayBook daybook3 = new DayBook
                                    {
                                        Id = 0,
                                        Amount = extraAmountCredit,
                                        CanRollBack = false,
                                        CreditAccountId = customerCreditAccount.Id,
                                        DebitAccountId = null,
                                        Date = DateTime.Now,
                                        Description = finMsg + " (DB. 3rd)",
                                        SaleOrderId = saleOrder.Id,
                                        InDate = DateTime.Now.Date
                                    };
                                    daybook3 = db.DayBooks.Add(daybook3);
                                    db.SaveChanges();

                                    AccountTransaction creditItemTrans2 = new AccountTransaction
                                    {
                                        Id = 0,
                                        Account = null,
                                        AccountTransactionType = AccountTransactionType.Credit,
                                        Balance = customer.IsEmployee ? extraAmountCredit : -extraAmountCredit,
                                        CreditAmount = extraAmountCredit,
                                        Date = DateTime.Now,
                                        DayBookId = daybook3.Id,
                                        DebitAmount = 0,
                                        GeneralAccountId = customerCreditAccount.Id,
                                        Description = finMsg
                                    };

                                    var creditDbEntry2 = db.AccountTransactions.Where(a => a.GeneralAccountId == customerCreditAccount.Id)
                                        .AsParallel()
                                        .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                        .OrderByDescending(a => a.Id).FirstOrDefault();

                                    if (creditDbEntry2 != null)
                                    {
                                        creditItemTrans2.Balance += creditDbEntry2.Balance;
                                    }

                                    creditItemTrans2 = db.AccountTransactions.Add(creditItemTrans2);
                                    db.SaveChanges();

                                    var daybookdb3 = db.DayBooks.Find(daybook3.Id);
                                    daybookdb3.DebitTrace = string.Format("Partially credited");
                                    daybookdb3.CreditTrace = string.Format("({0}). Trans Id: {1}", customerCreditAccount.Title, creditItemTrans2.Id);
                                    daybookdb3.CreditAccountId = customerCreditAccount.Id;
                                    daybookdb3.DebitAccountId = null;
                                    db.Entry(daybookdb3).State = EntityState.Modified;
                                    db.SaveChanges();
                                    pDaybooks.Add(daybookdb3);
                                    #endregion


                                    db.SaveChanges();

                                    if (AppSett.PrintFinancialTransactions)
                                    {
                                        Helper.PrintTransactions(pDaybooks);
                                    }
                                    #endregion
                                }
                                if (saleOrder.OrderType == SaleOrderType.Cash && customer.Id != 1)
                                {
                                    //List<DayBook> pDaybooks = new List<DayBook>();
                                    //#region "Financials"
                                    //GeneralAccount cashDebitAccount = db.Accounts.Find(Properties.Resources.CashInHand) as GeneralAccount;
                                    //GeneralAccount saleCreditAccount = db.Accounts.Find(Properties.Resources.OilCakeRetailSaleAccount) as GeneralAccount;
                                    //var user = SingleTon.LoginForm.appUser;
                                    //decimal saleAmountCredit = saleOrder.NetPrice;
                                    //decimal cashAoumtDebit = saleOrder.NetPrice;

                                    //string finMsg = string.Format("Cash sale with extra amount ({0}), net sale amount ({1}), amount given ({2}), customer ({3}). Order No ({4}), dated ({5}), by ({6})",
                                    //    extraAmountCredit.ToString("n1"), saleAmountCredit.ToString("n1"), cashAoumtDebit.ToString("n1"), customer.Name, saleOrder.Id, DateTime.Now.ToString(), user.Name);

                                    //DayBook daybookEntry = new DayBook
                                    //{
                                    //    Id = 0,
                                    //    Amount = cashAoumtDebit,
                                    //    Date = DateTime.Now,
                                    //    Description = finMsg + " (DB 1st)",
                                    //    CanRollBack = false,
                                    //    SaleOrderId = saleOrder.Id,
                                    //    InDate = DateTime.Now.Date
                                    //};

                                    //daybookEntry = db.DayBooks.Add(daybookEntry);
                                    //db.SaveChanges();

                                    //#region "Debit transaction"

                                    //AccountTransaction debitItemTrans = new AccountTransaction
                                    //{
                                    //    Id = 0,
                                    //    Account = null,
                                    //    AccountTransactionType = AccountTransactionType.Debit,
                                    //    Balance = cashAoumtDebit,
                                    //    CreditAmount = 0,
                                    //    Date = saleOrder.OrderDate,
                                    //    DayBookId = daybookEntry.Id,
                                    //    DebitAmount = cashAoumtDebit,
                                    //    GeneralAccountId = cashDebitAccount.Id,
                                    //    Description = finMsg
                                    //};

                                    //var debitDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == cashDebitAccount.Id).AsParallel()
                                    //    .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                    //    .OrderByDescending(a => a.Id).FirstOrDefault();

                                    //if (debitDbEntry != null)
                                    //{
                                    //    debitItemTrans.Balance += debitDbEntry.Balance;
                                    //}

                                    //debitItemTrans = db.AccountTransactions.Add(debitItemTrans);
                                    //db.SaveChanges();

                                    //var daybookdb1 = db.DayBooks.Find(daybookEntry.Id);
                                    //daybookdb1.DebitTrace = string.Format("({0}). Trans Id: {1}", cashDebitAccount.Title, debitItemTrans.Id);
                                    //daybookdb1.CreditTrace = string.Format("Partial Credit");
                                    //daybookdb1.CreditAccountId = null;
                                    //daybookdb1.DebitAccountId = cashDebitAccount.Id;
                                    //db.Entry(daybookdb1).State = EntityState.Modified;
                                    //db.SaveChanges();
                                    //pDaybooks.Add(daybookdb1);
                                    //#endregion

                                    //#region  "Credit transaction"
                                    //DayBook daybook2 = new DayBook
                                    //{
                                    //    Id = 0,
                                    //    Amount = saleAmountCredit,
                                    //    CanRollBack = false,
                                    //    CreditAccountId = saleCreditAccount.Id,
                                    //    DebitAccountId = null,
                                    //    Date = DateTime.Now,
                                    //    Description = finMsg + " (DB 2nd)",
                                    //    SaleOrderId = saleOrder.Id,
                                    //    InDate = DateTime.Now.Date
                                    //};
                                    //daybook2 = db.DayBooks.Add(daybook2);
                                    //db.SaveChanges();

                                    //AccountTransaction creditItemTrans1 = new AccountTransaction
                                    //{
                                    //    Id = 0,
                                    //    Account = null,
                                    //    AccountTransactionType = AccountTransactionType.Credit,
                                    //    Balance = saleAmountCredit,
                                    //    CreditAmount = saleAmountCredit,
                                    //    Date = DateTime.Now,
                                    //    DayBookId = daybook2.Id,
                                    //    DebitAmount = 0,
                                    //    GeneralAccountId = saleCreditAccount.Id,
                                    //    Description = finMsg
                                    //};

                                    //var creditDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == saleCreditAccount.Id).AsParallel()
                                    //    .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                    //    .OrderByDescending(a => a.Id).FirstOrDefault();

                                    //if (creditDbEntry != null)
                                    //{
                                    //    creditItemTrans1.Balance += creditDbEntry.Balance;
                                    //}

                                    //creditItemTrans1 = db.AccountTransactions.Add(creditItemTrans1);
                                    //db.SaveChanges();

                                    //var daybookdb2 = db.DayBooks.Find(daybook2.Id);
                                    //daybookdb2.DebitTrace = string.Format("Partially credited");
                                    //daybookdb2.CreditTrace = string.Format("({0}). Trans Id: {1}", saleCreditAccount.Title, creditItemTrans1.Id);
                                    //daybookdb2.CreditAccountId = saleCreditAccount.Id;
                                    //daybookdb2.DebitAccountId = null;
                                    //db.Entry(daybookdb2).State = EntityState.Modified;
                                    //db.SaveChanges();
                                    //pDaybooks.Add(daybookdb2);
                                    //#endregion

                                    //#region  "Credit transaction"
                                    //DayBook daybook3 = new DayBook
                                    //{
                                    //    Id = 0,
                                    //    Amount = extraAmountCredit,
                                    //    CanRollBack = false,
                                    //    CreditAccountId = customerCreditAccount.Id,
                                    //    DebitAccountId = null,
                                    //    Date = DateTime.Now,
                                    //    Description = finMsg + " (DB. 3rd)",
                                    //    SaleOrderId = saleOrder.Id,
                                    //    InDate = DateTime.Now.Date
                                    //};
                                    //daybook3 = db.DayBooks.Add(daybook3);
                                    //db.SaveChanges();

                                    //AccountTransaction creditItemTrans2 = new AccountTransaction
                                    //{
                                    //    Id = 0,
                                    //    Account = null,
                                    //    AccountTransactionType = AccountTransactionType.Credit,
                                    //    Balance = customer.IsEmployee ? extraAmountCredit : -extraAmountCredit,
                                    //    CreditAmount = extraAmountCredit,
                                    //    Date = DateTime.Now,
                                    //    DayBookId = daybook3.Id,
                                    //    DebitAmount = 0,
                                    //    GeneralAccountId = customerCreditAccount.Id,
                                    //    Description = finMsg
                                    //};

                                    //var creditDbEntry2 = db.AccountTransactions.Where(a => a.GeneralAccountId == customerCreditAccount.Id)
                                    //    .AsParallel()
                                    //    .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                    //    .OrderByDescending(a => a.Id).FirstOrDefault();

                                    //if (creditDbEntry2 != null)
                                    //{
                                    //    creditItemTrans2.Balance += creditDbEntry2.Balance;
                                    //}

                                    //creditItemTrans2 = db.AccountTransactions.Add(creditItemTrans2);
                                    //db.SaveChanges();

                                    //var daybookdb3 = db.DayBooks.Find(daybook3.Id);
                                    //daybookdb3.DebitTrace = string.Format("Partially credited");
                                    //daybookdb3.CreditTrace = string.Format("({0}). Trans Id: {1}", customerCreditAccount.Title, creditItemTrans2.Id);
                                    //daybookdb3.CreditAccountId = customerCreditAccount.Id;
                                    //daybookdb3.DebitAccountId = null;
                                    //db.Entry(daybookdb3).State = EntityState.Modified;
                                    //db.SaveChanges();
                                    //pDaybooks.Add(daybookdb3);
                                    //#endregion


                                    //db.SaveChanges();

                                    //if (AppSett.PrintFinancialTransactions)
                                    //{
                                    //    Helper.PrintTransactions(pDaybooks);
                                    //}
                                    //#endregion
                                }
                                #endregion

                                var saleOrderDb = db.SaleOrders.Find(saleOrder.Id);
                                saleOrderDb.TotalPackings = totalPackings;
                                saleOrderDb.TotalWeight = totalWeight;
                                saleOrderDb.BardanaAmount = bardanaItem.UnitPrice * bardanaPackingCount;
                                saleOrderDb.LaborAmount = bardanaItem.UnitLaborPriceRetail * totalBardanaBharti;
                                saleOrderDb.BardanaPackingsCount = bardanaPackingCount;
                                switch (saleOrder.OrderType)
                                {
                                    case SaleOrderType.Credit:
                                        saleOrderDb.IsDone = true;
                                        break;
                                    case SaleOrderType.Partial:
                                        if (customer.Id != 1 && ordModel.RemainingAmount < 0)
                                        {
                                            saleOrderDb.IsDone = true;
                                            saleOrder.IsExtraAmounted = true;
                                        }
                                        break;
                                    case SaleOrderType.Cash:
                                        if (customer.Id != 1 && ordModel.RemainingAmount < 0)
                                        {
                                            saleOrderDb.IsDone = false;
                                            saleOrder.IsExtraAmounted = false;
                                        }
                                        else
                                        {
                                            saleOrderDb.IsDone = false;
                                            saleOrder.IsExtraAmounted = false;
                                        }
                                        break;
                                }
                                db.Entry(saleOrderDb).State = EntityState.Modified;
                                db.SaveChanges();

                                trans.Commit();

                                int orderNo = saleOrder.Id + 1;
                                labelOrderNo.Text = orderNo.ToString().PadLeft(5, '0');

                                ResetOrder();
                                Gujjar.InfoMsg(string.Format("Order No. {0} has been placed successfully.", saleOrder.Id));
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
                throw exp;
            }
        }

        private void PrintOrder(SaleOrder saleOrder, List<OrderLineVM> lines, string receiptType, ReceiptType recType)
        {
            CompVM vm = new CompVM
            {
                Address = AppSett.Address,
                LogoImg = AppSett.Logo,
                Name = AppSett.Name,
                Phone = AppSett.PhoneNo
            };

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
        private void ResetOrder()
        {
            try
            {
                orderLineVMBindingSource.List.Clear();
                cbCustomers.SelectedItem = cbCustomers.Items.OfType<Customer>()
                    .FirstOrDefault(a => a.Id == 1);
                ClearHeader();
                tbQty.Text = "0";
                tbItems.Text = "0";
                tbTotalAmount.Text = "0.0";
                tbTotalDiscount.Text = "0.0";
                tbTotalDiscountPercentage.Text = "0.0";
                tbDueAmount.Text = "0.0";
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
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

                var lengendsParam = invoice.Parameters["Legends"];
                lengendsParam.Visible = false;
                lengendsParam.Value = Helper.Legends();

                invoice.ShowPrintMarginsWarning = false;
                invoice.ShowPrintStatusDialog = false;
                invoice.Print(AppSett.ThermalPrinter);
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }
        private void textBoxBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string txt = textBoxBarcode.Text;
                    if (string.IsNullOrEmpty(txt))
                        return;

                    var prod = productVMList.FirstOrDefault(a => a.Barcode == txt);
                    if (prod == null)
                    {
                        throw new Exception(string.Format("No product found with code ({0})", txt));
                    }
                    UpdateHeader(prod);
                }
                catch (Exception exp)
                {
                    ClearHeader();
                    Gujjar.ErrMsg(exp);
                }
            }
            if (e.KeyCode == Keys.Down && dgv.Rows.Count > 0)
            {
                SelectDgv();
            }

        }

        private void tbProdDiscPercentage_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbProdDiscAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

            }
        }

        private void tbProdDiscPercentage_Leave(object sender, EventArgs e)
        {
            PercentLeaveAction();
        }
        private void PercentLeaveAction()
        {
            try
            {
                decimal unitPrice = 0;
                string txt1 = tbProdPrice.Text;
                if (!string.IsNullOrEmpty(txt1))
                {
                    unitPrice = txt1.ToDecimal();
                }

                decimal qty = 0;
                string txt2 = tbItemQty.Text;
                if (!string.IsNullOrEmpty(txt2))
                {
                    qty = tbItemQty.Text.ToDecimal();
                }

                decimal disPercen = 0;
                string txt = tbProdDiscPercentage.Text;
                if (!string.IsNullOrEmpty(txt))
                {
                    disPercen = txt.ToDecimal();
                }

                decimal disAmnt = 0;
                string txt3 = tbProdDiscAmount.Text;
                if (!string.IsNullOrEmpty(txt3))
                {
                    disAmnt = txt3.ToDecimal();
                }

                decimal total = unitPrice * qty;
                decimal disAmountCalc = total * disPercen / 100;

                if (qty > 0)
                {
                    disAmountCalc /= qty;
                }

                tbProdDiscAmount.Text = disAmountCalc.ToString("n2");

                decimal netAmount = total - disAmountCalc * qty;
                tbProdNetAmount.Text = netAmount.ToString("n2");
            }
            catch (FormatException)
            {
                // ignore
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbProdDiscAmount_Leave(object sender, EventArgs e)
        {
            DiscAmountLeave();
        }
        private void DiscAmountLeave()
        {
            try
            {
                decimal unitPrice = 0;
                string txt1 = tbProdPrice.Text;
                if (!string.IsNullOrEmpty(txt1))
                {
                    unitPrice = txt1.ToDecimal();
                }

                decimal qty = 0;
                string txt2 = tbItemQty.Text;
                if (!string.IsNullOrEmpty(txt2))
                {
                    qty = tbItemQty.Text.ToDecimal();
                }

                decimal disPercen = 0;
                string txt = tbProdDiscPercentage.Text;
                if (!string.IsNullOrEmpty(txt))
                {
                    disPercen = txt.ToDecimal();
                }

                decimal disAmnt = 0;
                string txt3 = tbProdDiscAmount.Text;
                if (!string.IsNullOrEmpty(txt3))
                {
                    disAmnt = txt3.ToDecimal() * qty;
                }

                decimal total = unitPrice * qty;

                if (total > 0)
                {
                    decimal disPercentage = disAmnt / total * 100;
                    tbProdDiscPercentage.Text = disPercentage.ToString("n2");
                    decimal netAmount = total - disAmnt;
                    tbProdNetAmount.Text = netAmount.ToString("n2");
                }
                else
                {
                    tbProdDiscPercentage.Text = "0";
                    tbProdNetAmount.Text = "0";

                }

            }
            catch (FormatException)
            {
                //ignore
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbProdDiscPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PercentLeaveAction();
                Header_KeyDown(sender, e);
            }
        }

        private void tbProdDiscAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DiscAmountLeave();
                Header_KeyDown(sender, e);
            }
        }

        private void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dgv.Rows.Count > 0)
            {
                DialogResult result = Gujjar.ConfirmYesNo("Are you sure to delete this line?");
                if (result == DialogResult.No)
                    return;

                string id = dgv.CurrentRow.Cells[0].Value.ToString();

                var obj = orderLineVMBindingSource.List.OfType<OrderLineVM>().FirstOrDefault(a => a.Id == id);
                if (obj != null)
                {
                    orderLineVMBindingSource.List.Remove(obj);
                    dgv.Refresh();
                    ClearHeader();
                    dgv.ClearSelection();
                    UpdateCounting();
                }
            }
            if (e.KeyCode == Keys.Enter && dgv.Rows.Count > 0)
            {
                e.Handled = true;
                string id = dgv.CurrentRow.Cells[0].Value.ToString();

                var obj = orderLineVMBindingSource.List.OfType<OrderLineVM>().FirstOrDefault(a => a.Id == id);

                if (obj != null)
                {

                    tempProd = productVMList.FirstOrDefault(a => a.Id == obj.ProduuctId);
                    UpdateHeader(tempProd);
                    tbProdName.Text = obj.ProductName;
                    tbItemQty.Text = obj.Qty.ToString();
                    tbProdPrice.Text = obj.UnitPrice.ToString("n2");
                    tbProdDiscAmount.Text = obj.DiscountPrice.ToString("n2");
                    tbProdDiscPercentage.Text = (obj.DiscPercentage).ToString("n2");
                    tbProdNetAmount.Text = obj.NetPrice.ToString("n2");

                    orderLineVMBindingSource.List.Remove(obj);
                    dgv.Refresh();
                    dgv.ClearSelection();
                    tbItemQty.Focus();
                    tbItemQty.Select(0, tbItemQty.Text.Length);
                }
            }
        }

        private bool isWalikCustomer = false;
        private void cbCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbCustomers.SelectedIndex == -1)
                {
                    customer = null;
                    tbCustomerDiscountPercentage.Text = "0.00";
                }
                else
                {


                    DateTime today = DateTime.Now.Date;
                    customer = cbCustomers.SelectedItem as Customer;
                    string msg = string.Format("Customer: {0} ({1})", customer.Name, customer.Address);
                    lblHeading.Text = msg;
                    bool rest = (customer.Id == 1 || customer.IsEmployee) ? true : false;
                    if (rest != isWalikCustomer)
                    {
                        orderLineVMBindingSource.List.Clear();
                        UpdateCounting();
                        ClearHeader();
                    }
                    if (customer.Id == 1)
                    {
                        isWalikCustomer = true;
                    }
                    else
                    {
                        isWalikCustomer = false;
                    }
                    if (customer.ApplyCardDiscount && AppSett.EnableDiscounts && customer.CardStartDate <= today && customer.CardEndDate >= today)
                    {
                        tbCustomerDiscountPercentage.Text = customer.DiscountPercentage.ToString("n2");
                    }
                    else
                    {
                        tbCustomerDiscountPercentage.Text = "0.00";
                    }
                }
                UpdateCounting();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void UpdateCounting()
        {
            try
            {
                var lines = orderLineVMBindingSource.List.OfType<OrderLineVM>().ToList();

                if (lines.Count > 0)
                {
                    decimal linesTotalPrice = lines.Sum(a => a.TotalPrice);
                    decimal linesDiscount = lines.Sum(a => a.DiscountPrice * a.Qty);
                    int itemsCount = lines.Sum(a => a.Qty).ToInt();
                    int uniqueCount = lines.Count;

                    tbQty.Text = itemsCount.ToString();
                    tbItems.Text = uniqueCount.ToString();

                    #region Left panel discounts and charges
                    decimal custDiscPercent = 0;
                    string txt1 = tbCustomerDiscountPercentage.Text;
                    if (!string.IsNullOrEmpty(txt1))
                    {
                        custDiscPercent = txt1.ToDecimal();
                    }
                    decimal custDiscountAmount = custDiscPercent * linesTotalPrice / 100;
                    tbCustomerDiscount.Text = custDiscountAmount.ToString("n1");

                    decimal salesTaxPercent = 0;
                    string txt2 = tbSalesTaxPercentage.Text;
                    if (!string.IsNullOrEmpty(txt2))
                    {
                        salesTaxPercent = txt2.ToDecimal();
                    }
                    decimal salesTaxAmount = linesTotalPrice * salesTaxPercent / 100;
                    tbSalesTax.Text = salesTaxAmount.ToString("n1");

                    /// Service charges
                    decimal serviceCharges = 0;
                    string txt3 = tbServiceCharges.Text;
                    if (!string.IsNullOrEmpty(txt3))
                    {
                        serviceCharges = txt3.ToDecimal();
                    }
                    decimal serviceChargesPercent = 0;
                    if (linesTotalPrice > 0)
                    {
                        serviceChargesPercent = serviceCharges / linesTotalPrice * 100;
                    }
                    tbServiceChargesPercentage.Text = serviceChargesPercent.ToString("n2");

                    // Offer discount
                    decimal offerDiscPercentage = 0;
                    string txt4 = tbOfferDiscountPercentage.Text;
                    if (!string.IsNullOrEmpty(txt4))
                    {
                        offerDiscPercentage = txt4.ToDecimal();
                    }
                    decimal offDisAmount = linesTotalPrice * offerDiscPercentage / 100;
                    tbOfferDiscountAmount.Text = offDisAmount.ToString("n1");

                    // Order discount
                    decimal orderDiscPercent = 0;
                    string txt5 = tbOrderDiscountPercentage.Text;
                    if (!string.IsNullOrEmpty(txt5))
                    {
                        orderDiscPercent = txt5.ToDecimal();
                    }
                    decimal orderDiscontAmount = linesTotalPrice * orderDiscPercent / 100;
                    tbOrderDiscountAmount.Text = orderDiscontAmount.ToString("n1");
                    #endregion



                    decimal totalAmount = linesTotalPrice + salesTaxAmount + serviceCharges;
                    decimal totalDiscount = linesDiscount + custDiscountAmount + offDisAmount + orderDiscontAmount;
                    decimal totalDiscPercentage = 0;
                    if (totalAmount > 0)
                    {
                        totalDiscPercentage = totalDiscount / totalAmount * 100;
                    }
                    decimal dueAmount = totalAmount - totalDiscount;
                    tbTotalAmount.Text = totalAmount.ToString("n1");
                    tbTotalDiscount.Text = totalDiscount.ToString("n1");
                    tbTotalDiscountPercentage.Text = totalDiscPercentage.ToString("n2");
                    tbDueAmount.Text = dueAmount.ToString("n1");
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbOrderDiscountAmount_Leave(object sender, EventArgs e)
        {

        }

        private void tbOrderDiscountPercentage_Leave(object sender, EventArgs e)
        {

        }

        private void tbOrderDiscountPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    var lines = orderLineVMBindingSource.List.OfType<OrderLineVM>().ToList();
                    decimal discount = 0;
                    if (lines.Count > 0)
                    {
                        decimal discPercent = 0;
                        string txt1 = tbOrderDiscountPercentage.Text;
                        if (!string.IsNullOrEmpty(txt1))
                        {
                            discPercent = txt1.ToDecimal();
                        }

                        decimal totalLinesSum = lines.Sum(a => a.TotalPrice);
                        if (totalLinesSum > 0)
                        {
                            discount = totalLinesSum * discPercent / 100;
                        }
                        tbOrderDiscountAmount.Text = discount.ToString("n2");


                    }
                    else
                    {
                        tbOrderDiscountAmount.Text = discount.ToString("n2");
                    }
                    UpdateCounting();
                }
                catch (FormatException)
                {
                    //ignore
                }
                catch (Exception exp)
                {
                    Gujjar.ErrMsg(exp);
                }
            }

        }

        private void tbOrderDiscountAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    var lines = orderLineVMBindingSource.List.OfType<OrderLineVM>().ToList();
                    decimal discPercent = 0;
                    if (lines.Count > 0)
                    {
                        decimal discount = 0;
                        string txt1 = tbOrderDiscountAmount.Text;
                        if (!string.IsNullOrEmpty(txt1))
                        {
                            discount = txt1.ToDecimal();
                        }

                        decimal totalLinesSum = lines.Sum(a => a.TotalPrice);
                        if (totalLinesSum > 0)
                        {
                            discPercent = discount / totalLinesSum * 100;
                        }
                        tbOrderDiscountPercentage.Text = discPercent.ToString("n2");

                    }
                    else
                    {
                        tbOrderDiscountPercentage.Text = discPercent.ToString("n2");
                    }
                    UpdateCounting();
                }
                catch (FormatException)
                {
                    //ignore
                }
                catch (Exception exp)
                {
                    Gujjar.ErrMsg(exp);
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            labelTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            try
            {
                AddCustomerForm form = new AddCustomerForm();
                form.ShowDialog();

                int id = form.CustomerId;
                if (id != 0)
                {
                    BindCustomers();
                    cbCustomers.Refresh();
                    cbCustomers.SelectedItem = cbCustomers.Items.OfType<Customer>()
                        .FirstOrDefault(a => a.Id == id);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        bool IsClosingDone = false;
        bool IsLaborExpenseDone = false;
        bool IsBardanaExpenseDone = false;
        string closingMessage = "";
        string bardanaExpenseMessage = "";
        string laborExpenseMessage = "";
        private void btnClosing_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Helper.ConfirmUserPassword())
                {
                    return;
                }

                IsClosingDone = false;
                WaitForm wait = new WaitForm(DoClosing);
                wait.ShowDialog();

                if (IsClosingDone)
                {
                    Gujjar.InfoMsg(closingMessage);
                }
                else
                {
                    Gujjar.ErrMsg("Error due to Unknown reason");
                }

                WaitForm wait2 = new WaitForm(FixExpenseSaleExpenses);
                wait2.ShowDialog();

                if (IsBardanaExpenseDone)
                {
                    Gujjar.InfoMsg(bardanaExpenseMessage);
                }
                if (IsLaborExpenseDone)
                {
                    Gujjar.InfoMsg(laborExpenseMessage);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void FixExpenseSaleExpenses()
        {
            List<DayBook> pDaybooks = new List<DayBook>();
            totalPackings = 0;
            closingMessage = "N/A";
            IsBardanaExpenseDone = false;
            IsLaborExpenseDone = false;
            try
            {
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            DateTime startDate = AppSett.StartDate.Date;
                            DateTime todayDateTime = DateTime.Now;
                            DateTime endDate2 = DateTime.Now;
                            DateTime startDate2 = DateTime.Now;
                            var deDb = db.DEEntries.Where(a => a.Dated >= startDate && a.Dated <= todayDateTime)
                                .OrderByDescending(a => a.Dated).FirstOrDefault();
                            if (deDb != null)
                            {
                                startDate = deDb.Dated.AddDays(-1).Date;
                            }

                            var entries = db.SaleOrders.Where(a => a.OrderDate >= startDate)
                                .AsParallel().ToList().Where(a => !a.IsExpensed).OrderBy(a => a.Id)
                                .ToList();
                            if (entries == null || entries.Count == 0)
                            {
                                DEEntry deentry = new DEEntry
                                {
                                    SaleAmount = 0,
                                    Dated = todayDateTime.Date,
                                    Description = "No sale order",
                                    EndDateTime = todayDateTime,
                                    Id = 0,
                                    SaleQty = 0,
                                    StartDateTime = todayDateTime
                                };
                                db.DEEntries.Add(deentry);
                                db.SaveChanges();

                                IsBardanaExpenseDone = true;
                                IsLaborExpenseDone = true;
                                laborExpenseMessage = "Labor expenses already has been added";
                                bardanaExpenseMessage = "Bardana expenses already has been added";
                            }
                            else
                            {
                                foreach (var item in entries)
                                {
                                    item.IsExpensed = true;
                                    db.Entry(item).State = EntityState.Modified;

                                }
                                db.SaveChanges();

                                #region "Labor expense accounting"
                                decimal laboramount = entries.Sum(a => a.LaborAmount);
                                if (laboramount > 0)
                                {
                                    DateTime startDateTime = entries.First().TimpStamp;
                                    DateTime endDateTime = entries.Last().TimpStamp;
                                    endDate2 = endDateTime;
                                    startDate2 = startDateTime;

                                    totalPackings = entries.Sum(a => a.TotalPackings);

                                    string finMessage = string.Format("Day end labor expenses for retail sale of oil cake. Dated ({0}). Start date time ({1}), end date time ({2}), total orders ({3}), total ({4}-bori), per bori labor rate ({5}), total labor expense ({6})",
                                                                                    todayDateTime.ToShortDateString(), startDateTime, endDateTime, entries.Count, totalPackings.ToString("n1"), bardanaItem.UnitLaborPriceRetail.ToString("n1"), laboramount.ToString("n2"));
                                    laborExpenseMessage = finMessage;
                                    DayBook daybookEntry = new DayBook
                                    {
                                        Id = 0,
                                        Amount = laboramount,
                                        Date = todayDateTime,
                                        Description = finMessage,
                                        CanRollBack = false,
                                        InDate = DateTime.Now.Date
                                    };
                                    daybookEntry = db.DayBooks.Add(daybookEntry);
                                    db.SaveChanges();



                                    #region "Product financial Credit entry"
                                    GeneralAccount creditAccount1 = db.Accounts.Find(Properties.Resources.OilcakeLaborExpensePayableAccount) as GeneralAccount;
                                    GeneralAccount debitAccount1 = db.Accounts.Find(Properties.Resources.OilCakeRetailLaborExpenseAccount) as GeneralAccount;

                                    AccountTransaction creditTrans1 = new AccountTransaction
                                    {
                                        Account = null,
                                        Description = finMessage,
                                        AccountTransactionType = AccountTransactionType.Credit,
                                        CreditAmount = laboramount,
                                        Balance = laboramount,
                                        Date = todayDateTime,
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
                                        Balance = laboramount,
                                        Date = todayDateTime,
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
                                    #endregion

                                    IsLaborExpenseDone = true;
                                    pDaybooks.Add(dbDayBook);
                                }
                                #endregion

                                #region Bardana expense accounting
                                decimal bardanaAmount = entries.Sum(a => a.BardanaAmount);
                                if (bardanaAmount > 0)
                                {
                                    DateTime startDateTime = entries.First().TimpStamp;
                                    DateTime endDateTime = entries.Last().TimpStamp;

                                    totalPackings = entries.Sum(a => a.BardanaPackingsCount);

                                    string finMessage = string.Format("Day end bardana expenses for retail sale of oil cake. Dated ({0}). Start date time ({1}), end date time ({2}), total orders ({3}), total ({4}-bori), per bori price ({5}), total bardana expense ({6})",
                                                                                    todayDateTime.ToShortDateString(), startDateTime, endDateTime, entries.Count, totalPackings.ToString("n1"), bardanaItem.UnitPrice.ToString("n1"), bardanaAmount.ToString("n2"));
                                    bardanaExpenseMessage = finMessage;

                                    DayBook daybookEntry = new DayBook
                                    {
                                        Id = 0,
                                        Amount = bardanaAmount,
                                        Date = todayDateTime,
                                        Description = finMessage,
                                        CanRollBack = false,
                                        InDate = DateTime.Now.Date
                                    };
                                    daybookEntry = db.DayBooks.Add(daybookEntry);
                                    db.SaveChanges();



                                    #region "Product financial Credit entry"
                                    GeneralAccount creditAccount1 = db.Accounts.Find(bardanaItem.GeneralAccountId) as GeneralAccount;
                                    GeneralAccount debitAccount1 = db.Accounts.Find(Properties.Resources.BardanaSellingExpenseAccount) as GeneralAccount;

                                    AccountTransaction creditTrans1 = new AccountTransaction
                                    {
                                        Account = null,
                                        Description = finMessage,
                                        AccountTransactionType = AccountTransactionType.Credit,
                                        CreditAmount = bardanaAmount,
                                        Balance = -bardanaAmount,
                                        Date = todayDateTime,
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
                                        Date = todayDateTime,
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
                                    #endregion

                                    IsBardanaExpenseDone = true;
                                    pDaybooks.Add(dbDayBook);

                                    if (AppSett.PrintFinancialTransactions)
                                    {
                                        Helper.PrintTransactions(pDaybooks);
                                    }
                                }
                                #endregion
                                decimal saleAmount = bardanaAmount + laboramount;
                                if (saleAmount > 0)
                                {

                                    DEEntry deentry = new DEEntry
                                    {
                                        SaleAmount = saleAmount,
                                        Dated = todayDateTime.Date,
                                        Description = bardanaExpenseMessage,
                                        EndDateTime = endDate2,
                                        Id = 0,
                                        SaleQty = totalPackings,
                                        StartDateTime = startDate2
                                    };
                                    db.DEEntries.Add(deentry);
                                }


                            }
                            db.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception exp2)
                        {
                            IsClosingDone = false;
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
        private void DoClosing()
        {
            List<DayBook> pDaybooks = new List<DayBook>();
            try
            {
                totalPackings = 0;

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            DateTime startDate = AppSett.StartDate.Date;
                            DateTime todayDateTime = DateTime.Now;

                            var deDb = db.DEEntries.Where(a => a.Dated >= startDate && a.Dated <= todayDateTime)
                                .OrderByDescending(a => a.Dated).FirstOrDefault();
                            if (deDb != null)
                            {
                                startDate = deDb.Dated.AddDays(-1).Date;
                            }

                            var entries = db.SaleOrders.Where(a => a.OrderDate >= startDate.Date)
                                .AsParallel().ToList().Where(a => a.OrderType != SaleOrderType.Credit && !a.IsDone).OrderBy(a => a.Id)
                                .ToList();
                            if (entries == null || entries.Count == 0)
                            {
                                DEEntry deentry = new DEEntry
                                {
                                    SaleAmount = 0,
                                    Dated = todayDateTime.Date,
                                    Description = "No sale order",
                                    EndDateTime = todayDateTime,
                                    Id = 0,
                                    SaleQty = 0,
                                    StartDateTime = todayDateTime
                                };
                                db.DEEntries.Add(deentry);
                                db.SaveChanges();
                                trans.Commit();
                                IsClosingDone = true;
                                closingMessage = "Closing already has been done";
                            }
                            else
                            {
                                foreach (var item in entries)
                                {
                                    item.IsDone = true;
                                    db.Entry(item).State = EntityState.Modified;

                                }
                                db.SaveChanges();

                                totalPackings = entries.Sum(a => a.TotalPackings);
                                totalWeight = entries.Sum(a => a.TotalWeight);
                                decimal amount = 0;
                                amount += entries.Where(a => a.OrderType == SaleOrderType.Cash)
                                    .Sum(a => a.NetPrice);
                                amount += entries.Where(a => a.OrderType == SaleOrderType.Partial)
                                    .Sum(a => a.AmountGiven);

                                DateTime startDateTime = entries.First().TimpStamp;
                                DateTime endDateTime = entries.Last().TimpStamp;

                                string finMessage = string.Format("Day end cash sale entry of oil cake. Dated ({0}). Start date time ({1}), end date time ({2}), total orders ({3}), total sales  ({4}), total packings ({5}-bori), total weight ({6}-Kg)",
                                    todayDateTime, startDateTime, endDateTime, entries.Count, amount.ToString("n2"), totalPackings.ToString("n1"), totalWeight.ToString("n1"));
                                closingMessage = finMessage;
                                DayBook daybookEntry = new DayBook
                                {
                                    Id = 0,
                                    Amount = amount,
                                    Date = todayDateTime,
                                    Description = finMessage,
                                    CanRollBack = false,
                                    InDate = DateTime.Now.Date
                                };
                                daybookEntry = db.DayBooks.Add(daybookEntry);
                                db.SaveChanges();

                                DEEntry deentry = new DEEntry
                                {
                                    SaleAmount = amount,
                                    Dated = todayDateTime.Date,
                                    Description = finMessage,
                                    EndDateTime = endDateTime,
                                    Id = 0,
                                    SaleQty = totalPackings,
                                    StartDateTime = startDateTime
                                };
                                db.DEEntries.Add(deentry);
                                db.SaveChanges();

                                #region "Product financial Credit entry"
                                string creditAccountId = Properties.Resources.OilCakeRetailSaleAccount;




                                AccountTransaction prodCreditTrans = new AccountTransaction
                                {
                                    Account = null,
                                    Description = finMessage,
                                    AccountTransactionType = AccountTransactionType.Credit,
                                    CreditAmount = amount,
                                    Balance = amount,
                                    Date = todayDateTime,
                                    DayBookId = daybookEntry.Id,
                                    DebitAmount = 0,
                                    Id = 0,
                                    GeneralAccountId = creditAccountId
                                };

                                var dbProdEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == creditAccountId)
                                    .AsParallel().ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                    .OrderByDescending(a => a.Id)
                                    .FirstOrDefault();
                                if (dbProdEntry != null)
                                {
                                    prodCreditTrans.Balance += dbProdEntry.Balance;
                                }

                                prodCreditTrans = db.AccountTransactions.Add(prodCreditTrans);
                                db.SaveChanges();
                                #endregion

                                string debitAccountId = Properties.Resources.CashInHand;


                                #region "Debit transaction"
                                AccountTransaction debitTrans = new AccountTransaction
                                {
                                    Id = 0,
                                    GeneralAccountId = debitAccountId,
                                    Account = null,
                                    AccountTransactionType = AccountTransactionType.Debit,
                                    CreditAmount = 0,
                                    Balance = amount,
                                    Date = todayDateTime,
                                    DayBookId = daybookEntry.Id,
                                    DebitAmount = amount,
                                    Description = finMessage
                                };

                                var dbDebitEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == debitAccountId)
                                    .AsParallel().ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                    .OrderByDescending(a => a.Id)
                                    .FirstOrDefault();
                                if (dbDebitEntry != null)
                                {
                                    debitTrans.Balance += dbDebitEntry.Balance;
                                }
                                debitTrans = db.AccountTransactions.Add(debitTrans);
                                db.SaveChanges();

                                GeneralAccount debitAccount = db.Accounts.Find(debitAccountId) as GeneralAccount;
                                GeneralAccount creditAccount = db.Accounts.Find(creditAccountId) as GeneralAccount;

                                DayBook dbDayBook = db.DayBooks.Find(daybookEntry.Id);
                                dbDayBook.DebitTrace = string.Format("{0}. Trans Id: {1}", debitAccount.Title, debitTrans.Id);
                                dbDayBook.CreditTrace = string.Format("{0}. Trans Id: {1}", creditAccount.Title, prodCreditTrans.Id);
                                dbDayBook.CreditAccountId = creditAccount.Id;
                                dbDayBook.DebitAccountId = debitAccount.Id;
                                db.Entry(dbDayBook).State = EntityState.Modified;
                                db.SaveChanges();

                                trans.Commit();
                                if (AppSett.PrintFinancialTransactions)
                                {
                                    Helper.PrintTransactions(new List<DayBook> { dbDayBook });
                                }
                                IsClosingDone = true;
                                #endregion

                            }
                        }
                        catch (Exception exp2)
                        {
                            IsClosingDone = false;
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

        private void custSearchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SearchCustomerForm fomr = new SearchCustomerForm(SingleTon.LoginForm.AppCustomers);
                fomr.ShowDialog();
                int cid = fomr.CustomerId;
                if (cid != 0)
                {
                    cbCustomers.SelectedItem = cbCustomers.Items.OfType<Customer>()
                        .FirstOrDefault(a => a.Id == cid);
                }
                else
                {
                    cbCustomers.SelectedItem = cbCustomers.Items.OfType<Customer>()
                        .FirstOrDefault(a => a.Id == 1);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            try
            {
                SaleOrderListForm form = new SaleOrderListForm();
                form.ShowDialog();
            }
            catch (Exception epx)
            {
                Gujjar.ErrMsg(epx);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
    public enum ReceiptType
    {
        Gatepass,
        Floorscale,
        Customer,
        Office
    }
}
