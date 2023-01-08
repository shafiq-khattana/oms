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
using Model.ReadyStuff.ViewModel;
using WinFom.Common.Forms;
using WinFom.ReadyStuff.ReportViewModel;
using Model.Admin.Model;
using WinFom.ReadyStuff.Report;
using DevExpress.XtraReports.UI;
using Zen.Barcode;
using WinFom.Common.Model;
using Model.RetailBardanaManaged.Model;
using Model.Financials.Model;
using System.Data.Entity;
using DevExpress.XtraPrinting;

namespace WinFom.ReadyStuff.Forms
{
    public partial class ScheduleProcessForm : Form
    {
        private SchProcessTransfer schProcessTransfer = null;
        public bool IsDone = false;
        private string btndgvdel = "asdsasfwer23412341";
        private ReadyDriver driver = null;
        private ReadySelector selector = null;
        private List<FRVN> frvnList = new List<FRVN>();
        private List<CRVN> crvnList = new List<CRVN>();
        private List<PackingRVN> packingRvnList = new List<PackingRVN>();
        private AppSettings appSett = null;
        private ReadySchedule readySchedule = null;
        private CRVN crvn = null;
        string schNo = "N/A";
        private AppUser appUser = SingleTon.LoginForm.appUser;
        string schItem = "N/A";
        private List<ReadyDriver> readyDrivers = null;
        private List<ReadySelector> readySelectors = null;
        private ReadyDeal _deal = null;
        //private RetailBardanaItem bardanaItem = null;
        public ScheduleProcessForm(SchProcessTransfer spt)
        {
            InitializeComponent();
            schProcessTransfer = spt;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadDrivers()
        {
            try
            {
                readyDrivers = null;
                using (Context db = new Context())
                {
                    readyDrivers = db.ReadyDrivers.Where(a => a.IsActive).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadSelectors()
        {
            try
            {
                readySelectors = null;
                using (Context db = new Context())
                {
                    readySelectors = db.ReadySelectors.Where(a => a.IsActive).ToList();

                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadBardana()
        {
            try
            {
                //bardanaItem = null;
                //using (Context db = new Context())
                //{
                //    bardanaItem = db.RetailBardanaItems.First();
                //}
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindCbDrivers()
        {
            cbDrivers.DataSource = readyDrivers;
            cbDrivers.DisplayMember = "Name";
            cbDrivers.ValueMember = "Id";
        }
        private void BindCbSelectors()
        {
            cbSelectors.DataSource = readySelectors;
            cbSelectors.DisplayMember = "Name";
            cbSelectors.ValueMember = "Id";
        }
        private void LoadBoth()
        {
            LoadDrivers();
            LoadSelectors();
        }
        private void BindBoth()
        {
            BindCbDrivers();
            BindCbSelectors();
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm form1 = new WaitForm(LoadBoth);
                form1.ShowDialog();
                BindBoth();
                LoadBardana();
                //if(bardanaItem == null)
                //{
                //    Gujjar.ErrMsg("Please add bardana (packings) in the system.");
                //    Close();
                //}
                using (Context db = new Context())
                {
                    var sch2 = db.ReadySchedules.Find(schProcessTransfer.ScheduleId);
                    _deal = db.ReadyDeals.Find(sch2.ReadyDealId);
                }

                tbBroker.Text = schProcessTransfer.Broker;
                tbDealItem.Text = schProcessTransfer.DealItem;
                tbReadyDate.Text = schProcessTransfer.ReadyDate;
                tbScheduleNo.Text = schProcessTransfer.ScheduleNo;
                tbRate.Text = _deal.PerTradeUnitPrice.ToString("n2");
                Helper.IsOkApplied();
                Gujjar.AddDatagridviewButton(dgv, btndgvdel, "Delete", "Delete", 80);
                Gujjar.TB4(pMain);
                Gujjar.TB4(panel1);
                Gujjar.NumbersOnly(tbEmptyVehicleWeight);

                using (Context db = new Context())
                {
                    appSett = db.AppSettings.First();
                }

                dtp.MinDate = appSett.StartDate;
                dtp.MaxDate = appSett.EndDate;
                //tbLaborCharges.Text = bardanaItem.UnitLaborPriceRetail.ToString("n1");
                tbLaborCharges.Text = "0.0";
                Gujjar.TB4(pMain);
                Gujjar.NumbersOnly(tbLaborCharges);

                bswLaborCharges.Value = false;
                bswLaborCharges.Enabled = tbLaborChargesDescription.Enabled = tbLaborCharges.Enabled = false;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddDriver_Click(object sender, EventArgs e)
        {
            try
            {
                AddReadyDriverForm2 form = new AddReadyDriverForm2();
                form.ShowDialog();

                int did = form.driver.Id;
                if (driver != null)
                {
                    LoadDrivers();
                    BindCbDrivers();

                    cbDrivers.SelectedItem = cbDrivers.Items.OfType<ReadyDriver>()
                        .FirstOrDefault(a => a.Id == did);
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
                AddBharthiForm form = new AddBharthiForm();
                form.ShowDialog();
                if (form.bharthiVM != null)
                {
                    var obj = bharthiVMBindingSource.List.OfType<BharthiVM>().FirstOrDefault(a => a.Id == form.bharthiVM.Id);
                    if (obj != null)
                    {

                        obj.Qty += form.bharthiVM.Qty;
                        obj.Total += form.bharthiVM.Total;
                        dgv.Refresh();
                    }
                    else
                    {
                        bharthiVMBindingSource.List.Add(form.bharthiVM);

                    }


                    decimal totalWeight = bharthiVMBindingSource.List.OfType<BharthiVM>().Sum(a => a.Total);
                    tbGrandTotal.Text = totalWeight.ToString("n2");
                    decimal totalBori = bharthiVMBindingSource.List.OfType<BharthiVM>().Sum(a => a.Qty);
                    tbTotalBori.Text = totalBori.ToString("n1");
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }



        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int ri = e.RowIndex;
            int ci = e.ColumnIndex;

            if (ri == -1 || ri == dgv.NewRowIndex)
                return;

            if (dgv.Columns[btndgvdel].Index == ci)
            {
                DialogResult result = Gujjar.ConfirmYesNo("Do you really want to delete");
                if (result == DialogResult.No)
                {
                    return;
                }
                int id = dgv.Rows[ri].Cells[0].Value.ToInt();
                var obj = bharthiVMBindingSource.List.OfType<BharthiVM>().FirstOrDefault(a => a.Id == id);
                if (obj != null)
                {
                    bharthiVMBindingSource.List.Remove(obj);
                    dgv.Refresh();
                }

                decimal total = bharthiVMBindingSource.List.OfType<BharthiVM>().Sum(a => a.Total);
                tbGrandTotal.Text = total.ToString("n4");
                decimal totalBori = bharthiVMBindingSource.List.OfType<BharthiVM>().Sum(a => a.Qty);
                tbTotalBori.Text = totalBori.ToString("n1");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void bsPrinterState_Click(object sender, EventArgs e)
        {
            numUDCopies.Enabled = bsPrinterState.Value;
        }

        private void PrintReceiptFloorScale()
        {
            try
            {
                FRVN frvn = new FRVN
                {
                    Address = appSett.Address,
                    Logo = appSett.Logo,
                    Name = appSett.Name,
                    Phone = appSett.PhoneNo
                };
                if (frvnList != null && frvnList.Count > 0)
                {
                    frvnList.Clear();
                }
                frvnList.Add(frvn);
                Code128BarcodeDraw bcode = BarcodeDrawFactory.Code128WithChecksum;
                Image img = bcode.Draw(readySchedule.Unum, 50);
                byte[] imgBytes = Gujjar.GetByteArrayFromImage(img);
                crvn.Uan = imgBytes;
                CodeQrBarcodeDraw qrCode = BarcodeDrawFactory.CodeQr;
                Image img2 = qrCode.Draw("GBS Solutions, Burewala. 0323-9372084", 50);
                byte[] qrBytes = Gujjar.GetByteArrayFromImage(img2);
                crvn.QrCode = qrBytes;
                if (crvnList != null && crvnList.Count > 0)
                {
                    crvnList.Clear();
                }
                crvnList.Add(crvn);
                FloorScale rep = new FloorScale();

                DetailReportBand band1 = rep.Bands["DetailReport"] as DetailReportBand;
                DetailReportBand band2 = rep.Bands["DetailReport1"] as DetailReportBand;
                DetailReportBand band3 = rep.Bands["DetailReport2"] as DetailReportBand;

                band1.DataSource = frvnList;
                band2.DataSource = crvnList;
                band3.DataSource = packingRvnList;

                XRPictureBox picbox = rep.FindControl("xrpic", true) as XRPictureBox;
                picbox.Image = Properties.Resources.FloorScale;
                picbox.Sizing = ImageSizeMode.StretchImage;
                rep.ShowPrintMarginsWarning = false;
                rep.ShowPrintStatusDialog = false;

                var param = rep.Parameters["receiptType"];
                param.Value = "Floor Scale";
                param.Visible = false;

                var lengendsParam = rep.Parameters["Legends"];
                lengendsParam.Visible = false;
                lengendsParam.Value = Helper.Legends();

                var param2 = rep.Parameters["schNo"];
                param2.Value = schNo;
                param2.Visible = false;

                rep.Print(appSett.ThermalPrinter);
                //rep.ShowRibbonPreview();
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }

        private void PrintReceiptGatePass()
        {
            try
            {
                FRVN frvn = new FRVN
                {
                    Address = appSett.Address,
                    Logo = appSett.Logo,
                    Name = appSett.Name,
                    Phone = appSett.PhoneNo
                };
                if (frvnList != null && frvnList.Count > 0)
                {
                    frvnList.Clear();
                }
                frvnList.Add(frvn);
                Code128BarcodeDraw bcode = BarcodeDrawFactory.Code128WithChecksum;
                Image img = bcode.Draw(readySchedule.Unum, 50);
                byte[] imgBytes = Gujjar.GetByteArrayFromImage(img);
                crvn.Uan = imgBytes;
                CodeQrBarcodeDraw qrCode = BarcodeDrawFactory.CodeQr;
                Image img2 = qrCode.Draw("GBS Solutions, Burewala. 0323-9372084", 50);
                byte[] qrBytes = Gujjar.GetByteArrayFromImage(img2);
                crvn.QrCode = qrBytes;
                if (crvnList != null && crvnList.Count > 0)
                {
                    crvnList.Clear();
                }
                crvnList.Add(crvn);
                FloorScale rep = new FloorScale();

                DetailReportBand band1 = rep.Bands["DetailReport"] as DetailReportBand;
                DetailReportBand band2 = rep.Bands["DetailReport1"] as DetailReportBand;
                DetailReportBand band3 = rep.Bands["DetailReport2"] as DetailReportBand;

                band1.DataSource = frvnList;
                band2.DataSource = crvnList;
                band3.DataSource = packingRvnList;

                XRPictureBox picbox = rep.FindControl("xrpic", true) as XRPictureBox;
                picbox.Image = Properties.Resources.Gate_50px;
                picbox.Sizing = ImageSizeMode.StretchImage;
                rep.ShowPrintMarginsWarning = false;
                rep.ShowPrintStatusDialog = false;

                var param = rep.Parameters["receiptType"];
                param.Value = "Gate pass";
                param.Visible = false;

                var param2 = rep.Parameters["schNo"];
                param2.Value = schNo;
                param2.Visible = false;

                var lengendsParam = rep.Parameters["Legends"];
                lengendsParam.Visible = false;
                lengendsParam.Value = Helper.Legends();
                rep.Print(appSett.ThermalPrinter);
                //rep.ShowRibbonPreview();
            }
            catch (Exception exp)
            {

                throw exp;
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
                if (!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill text fields");
                }

                if (bharthiVMBindingSource.List.Count == 0)
                {
                    throw new Exception("Please add some packing types");
                }


                DialogResult result2 = Gujjar.ConfirmYesNo("Please confirm, before to proceed on.");
                if (result2 == DialogResult.No)
                {
                    return;
                }

                if (!Helper.ConfirmUserPassword())
                {
                    return;
                }
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            string driverName = "N/A";
                            string selectorName = "N/A";
                            string dated = DateTime.Now.ToString();
                            ReadySchedule sch = db.ReadySchedules.Find(schProcessTransfer.ScheduleId);
                            ReadyDeal deal = db.ReadyDeals.Find(sch.ReadyDealId);
                            ReadyItem readyItem = db.ReadyItems.Find(deal.ReadyItemId);

                            schItem = readyItem.Title;
                            schNo = string.Format("Deal: {0}, Sch No: {1}", deal.Id, sch.ScheduleNo);
                            sch.IsCompleted = false;
                            sch.ScheduleType = ReadyScheduleType.Dispatched;
                            sch.DispatchedDate = dtp.Value;
                            sch.NoOfVehicles = 1;
                            sch.EmptyVehicleWeight = tbEmptyVehicleWeight.Text.ToDecimal();
                            sch.LaborExpensesDescription = "N/A";// tbLaborChargesDescription.Text;

                            if (driver == null)
                            {
                                sch.ReadyDriverId = null;
                            }
                            else
                            {
                                sch.ReadyDriverId = driver.Id;
                                driverName = driver.Name;
                            }
                            if (selector == null)
                            {
                                sch.ReadySelectorId = null;
                            }
                            else
                            {
                                sch.ReadySelectorId = selector.Id;
                                selectorName = selector.Name;
                            }

                            sch.ScheduleWeight = tbGrandTotal.Text.ToDecimal();
                            sch.TotalPackings = tbTotalBori.Text.ToDecimal();
                            sch.BardanaAmountExpense = 0;// sch.TotalPackings * bardanaItem.UnitPrice;
                            sch.LaborExpenses = sch.TotalPackings * tbLaborCharges.Text.ToDecimal();
                            sch.DispatchedDate = dtp.Value;
                            sch.VehicleNo = tbVehicleNo.Text;
                            db.Entry(sch).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            //var dbBD = db.RetailBardanaItems.Find(bardanaItem.Id);
                            //dbBD.SKU -= sch.TotalPackings;
                            //db.Entry(dbBD).State = EntityState.Modified;
                            //db.SaveChanges();

                            readySchedule = sch;
                            crvn = new CRVN
                            {
                                Dated = dated,
                                Driver = driverName,
                                Selector = selectorName,
                                Vehicle = tbVehicleNo.Text,
                                User = appUser.Id,
                                Item = schItem,
                                EmptyVehicleWeight = tbEmptyVehicleWeight.Text
                            };
                            var bharthies = bharthiVMBindingSource.List.OfType<BharthiVM>().ToList();
                            foreach (var item in bharthies)
                            {
                                PackingRVN rvn = new PackingRVN
                                {
                                    Packing = item.Type,
                                    Qty = item.Qty,
                                    Total = item.Total
                                };
                                packingRvnList.Add(rvn);
                                Bharthi bharthi = new Bharthi
                                {
                                    BharthiTypeId = item.Id,
                                    Qty = item.Qty,
                                    Total = item.Total,
                                    ReadyScheduleId = sch.Id
                                };

                                db.Bharthies.Add(bharthi);
                            }
                            if (bsPrinterState.Value)
                            {
                                for (int i = 0; i < (int)numUDCopies.Value; i++)
                                {

                                    PrintReceiptFloorScale();

                                    PrintReceiptGatePass();
                                }
                            }


                            #region Financial Accouting
                            #region Labor Expenses
                            //#region "Labor expense accounting"
                            //decimal laboramount = sch.LaborExpenses;
                            //if (laboramount > 0 && bswLaborCharges.Value)
                            //{
                            //    string finMessage = string.Format("Labor charges amount ({0}) for ({1}-whole sale) in ({2}). Total bori ({3}), rate ({4}), Dated ({5}). Description ({6}) by ({7})", 
                            //        laboramount, tbDealItem.Text, string.Format("Deal: {0}, Schedule: {1}", deal.Id, sch.Id), sch.TotalPackings.ToString("n1"), string.Format("{0}/bori", tbLaborCharges.Text), dtp.Value, tbLaborChargesDescription.Text, appUser.Name);

                            //    DayBook daybookEntry = new DayBook
                            //    {
                            //        Id = 0,
                            //        Amount = laboramount,
                            //        Date = dtp.Value,
                            //        Description = finMessage,
                            //        CanRollBack = false
                            //    };
                            //    daybookEntry = db.DayBooks.Add(daybookEntry);
                            //    db.SaveChanges();



                            //    #region "Credit entry"
                            //    GeneralAccount creditAccount1 = db.Accounts.Find(Properties.Resources.LaborAccruedExpensesAccount) as GeneralAccount;
                            //    GeneralAccount debitAccount1 = db.Accounts.Find(Properties.Resources.OilCakeWholeSaleLaborExpenseAccount) as GeneralAccount;

                            //    AccountTransaction creditTrans1 = new AccountTransaction
                            //    {
                            //        Account = null,
                            //        Description = finMessage,
                            //        AccountTransactionType = AccountTransactionType.Credit,
                            //        CreditAmount = laboramount,
                            //        Balance = laboramount,
                            //        Date = dtp.Value,
                            //        DayBookId = daybookEntry.Id,
                            //        DebitAmount = 0,
                            //        Id = 0,
                            //        GeneralAccountId = creditAccount1.Id
                            //    };

                            //    var dbProdEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == creditAccount1.Id)
                            //        .AsParallel().ToList().Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date)
                            //        .OrderByDescending(a => a.Id)
                            //        .FirstOrDefault();
                            //    if (dbProdEntry != null)
                            //    {
                            //        creditTrans1.Balance += dbProdEntry.Balance;
                            //    }

                            //    creditTrans1 = db.AccountTransactions.Add(creditTrans1);
                            //    db.SaveChanges();
                            //    #endregion

                            //    #region "Debit transaction"
                            //    AccountTransaction debitTrans = new AccountTransaction
                            //    {
                            //        Id = 0,
                            //        GeneralAccountId = debitAccount1.Id,
                            //        Account = null,
                            //        AccountTransactionType = AccountTransactionType.Debit,
                            //        CreditAmount = 0,
                            //        Balance = laboramount,
                            //        Date = dtp.Value,
                            //        DayBookId = daybookEntry.Id,
                            //        DebitAmount = laboramount,
                            //        Description = finMessage
                            //    };

                            //    var dbDebitEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == debitAccount1.Id)
                            //        .AsParallel().ToList().Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date)
                            //        .OrderByDescending(a => a.Id)
                            //        .FirstOrDefault();
                            //    if (dbDebitEntry != null)
                            //    {
                            //        debitTrans.Balance += dbDebitEntry.Balance;
                            //    }
                            //    debitTrans = db.AccountTransactions.Add(debitTrans);
                            //    db.SaveChanges();


                            //    DayBook dbDayBook = db.DayBooks.Find(daybookEntry.Id);
                            //    dbDayBook.DebitTrace = string.Format("{0}. Trans Id: {1}", debitAccount1.Title, debitTrans.Id);
                            //    dbDayBook.CreditTrace = string.Format("{0}. Trans Id: {1}", creditAccount1.Title, creditTrans1.Id);
                            //    dbDayBook.CreditAccountId = creditAccount1.Id;
                            //    dbDayBook.DebitAccountId = debitAccount1.Id;
                            //    db.Entry(dbDayBook).State = EntityState.Modified;
                            //    db.SaveChanges();
                            //    #endregion


                            //}
                            //#endregion
                            #endregion

                            //#region Bardana expenses
                            //if(sch.BardanaAmountExpense > 0)
                            //{
                            //    #region Bardana expense accounting
                            //    decimal bardanaAmount = sch.BardanaAmountExpense;
                            //    if (bardanaAmount > 0)
                            //    {


                            //        string finMessage = string.Format("Bardana expense for ({0} whole sale). Total packing ({1}-bori). per pack(bori) rate ({2}) in ({3}), total expense amount ({4}). Dated ({5}), by ({6}).",
                            //                       tbDealItem.Text, sch.TotalPackings.ToString("n1"), bardanaItem.UnitPrice.ToString("n1"), string.Format("Deal: {0}, Schedule: {1}", deal.Id, sch.Id), bardanaAmount.ToString("n2"), dtp.Value, appUser.Name);

                            //        DayBook daybookEntry = new DayBook
                            //        {
                            //            Id = 0,
                            //            Amount = bardanaAmount,
                            //            Date = dtp.Value,
                            //            Description = finMessage,
                            //            CanRollBack = false
                            //        };
                            //        daybookEntry = db.DayBooks.Add(daybookEntry);
                            //        db.SaveChanges();



                            //        #region "Product financial Credit entry"
                            //        GeneralAccount creditAccount1 = db.Accounts.Find(bardanaItem.GeneralAccountId) as GeneralAccount;
                            //        GeneralAccount debitAccount1 = db.Accounts.Find(Properties.Resources.BardanaSellingExpenseAccount) as GeneralAccount;

                            //        AccountTransaction creditTrans1 = new AccountTransaction
                            //        {
                            //            Account = null,
                            //            Description = finMessage,
                            //            AccountTransactionType = AccountTransactionType.Credit,
                            //            CreditAmount = bardanaAmount,
                            //            Balance = -bardanaAmount,
                            //            Date = dtp.Value,
                            //            DayBookId = daybookEntry.Id,
                            //            DebitAmount = 0,
                            //            Id = 0,
                            //            GeneralAccountId = creditAccount1.Id
                            //        };

                            //        var dbProdEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == creditAccount1.Id)
                            //            .AsParallel().ToList().Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date)
                            //            .OrderByDescending(a => a.Id)
                            //            .FirstOrDefault();
                            //        if (dbProdEntry != null)
                            //        {
                            //            creditTrans1.Balance += dbProdEntry.Balance;
                            //        }

                            //        creditTrans1 = db.AccountTransactions.Add(creditTrans1);
                            //        db.SaveChanges();
                            //        #endregion

                            //        #region "Debit transaction"
                            //        AccountTransaction debitTrans = new AccountTransaction
                            //        {
                            //            Id = 0,
                            //            GeneralAccountId = debitAccount1.Id,
                            //            Account = null,
                            //            AccountTransactionType = AccountTransactionType.Debit,
                            //            CreditAmount = 0,
                            //            Balance = bardanaAmount,
                            //            Date = dtp.Value,
                            //            DayBookId = daybookEntry.Id,
                            //            DebitAmount = bardanaAmount,
                            //            Description = finMessage
                            //        };

                            //        var dbDebitEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == debitAccount1.Id)
                            //            .AsParallel().ToList().Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date)
                            //            .OrderByDescending(a => a.Id)
                            //            .FirstOrDefault();
                            //        if (dbDebitEntry != null)
                            //        {
                            //            debitTrans.Balance += dbDebitEntry.Balance;
                            //        }
                            //        debitTrans = db.AccountTransactions.Add(debitTrans);
                            //        db.SaveChanges();


                            //        DayBook dbDayBook = db.DayBooks.Find(daybookEntry.Id);
                            //        dbDayBook.DebitTrace = string.Format("{0}. Trans Id: {1}", debitAccount1.Title, debitTrans.Id);
                            //        dbDayBook.CreditTrace = string.Format("{0}. Trans Id: {1}", creditAccount1.Title, creditTrans1.Id);
                            //        dbDayBook.CreditAccountId = creditAccount1.Id;
                            //        dbDayBook.DebitAccountId = debitAccount1.Id;
                            //        db.Entry(dbDayBook).State = EntityState.Modified;
                            //        db.SaveChanges();
                            //        #endregion

                            //    }
                            //    #endregion
                            //}
                            //#endregion
                            #endregion


                            db.SaveChanges();
                            trans.Commit();
                            IsDone = true;
                            Gujjar.InfoMsg("Schedule is updated. It is changed from schedule to dispatched state.");
                            Close();
                        }
                        catch (Exception p23)
                        {
                            trans.Rollback();
                            throw p23;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }


        private void btnAddSelector_Click(object sender, EventArgs e)
        {
            try
            {
                AddReadySelectorForm2 form = new AddReadySelectorForm2();
                form.ShowDialog();

                int sid = form.selector.Id;
                if (selector != null)
                {
                    LoadSelectors();
                    BindCbSelectors();

                    cbSelectors.SelectedItem = cbSelectors.Items.OfType<ReadySelector>()
                        .FirstOrDefault(a => a.Id == sid);
                }

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void cbDrivers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDrivers.SelectedIndex == -1)
                return;
            driver = cbDrivers.SelectedItem as ReadyDriver;
        }

        private void cbSelectors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSelectors.SelectedIndex == -1)
                return;

            selector = cbSelectors.SelectedItem as ReadySelector;
        }

        private void pMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bswLaborCharges_Click(object sender, EventArgs e)
        {
            tbLaborCharges.Enabled = bswLaborCharges.Value;
            bool res = bswLaborCharges.Value;
            if (res)
            {
                //tbLaborCharges.Text = bardanaItem.UnitLaborPriceRetail.ToString("n1");
            }
            else
            {
                tbLaborCharges.Text = "0.0";
            }
        }
    }


}
