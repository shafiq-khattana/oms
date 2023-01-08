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
using Model.ReadyStuff.ViewModel;
using WinFom.ReadyStuff.Report;
using WinFom.ReadyStuff.ReportViewModel;
using Zen.Barcode;
using DevExpress.XtraReports.UI;
using WinFom.OilDirtStuff.ViewModel;
using Model.OilDirtStuff.Model;
using Model.Financials.Model;
using DevExpress.XtraPrinting;

namespace WinFom.OilDirtStuff.Forms
{
    public partial class OilDirtScheduleProcessForm : Form
    {
        private OilDirtSchTransfer transferVM = null;
        private List<OilDirtSelector> selectorList = null;
        private List<OilDirtDriver> driverList = null;
        private OilDirtSelector selector = null;
        private OilDirtDriver driver = null;
        public bool IsDone = false;
        AppSettings appSett = Helper.AppSet;
        private CRVN crvn = null;
        private FRVN frvn = null;
        private AppUser appUser = SingleTon.LoginForm.appUser;
        private string schNo = "N/A";
        private OilDirtDeal _deal = null;
        public OilDirtScheduleProcessForm(OilDirtSchTransfer vm)
        {
            InitializeComponent();
            transferVM = vm;
        }
        private void LoadDrivers()
        {
            try
            {
                using (Context db = new Context())
                {
                    driverList = db.OilDirtDrivers.Where(a => a.IsActive).ToList();
                    var sch = db.OilDirtSchedules.Find(transferVM.ScheduleId);
                    _deal = db.OilDirtDeals.Find(sch.OilDirtDealId);
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
                using (Context db = new Context())
                {

                    selectorList = db.OilDirtSelectors.Where(a => a.IsActive).ToList();
                }
            }
            catch (Exception ep)
            {
                Gujjar.ErrMsg(ep);
            }
        }
        private void LoadBoth()
        {
            LoadSelectors();
            LoadDrivers();
        }
        private void BindCbSelectors()
        {
            cbSelectors.DataSource = selectorList;
            cbSelectors.DisplayMember = "Name";
            cbSelectors.ValueMember = "Id";

        }
        private void BindCbDrivers()
        {
            cbDrivers.DataSource = driverList;
            cbDrivers.DisplayMember = "Name";
            cbDrivers.ValueMember = "Id";
        }
        private void BindBoth()
        {
            BindCbDrivers();
            BindCbSelectors();
        }
        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                tbBroker.Text = transferVM.Broker;
                tbDealDate.Text = transferVM.DealDate;
                tbDealItem.Text = transferVM.Item;
                tbDealNo.Text = transferVM.DealSchedule;
                tbReadyDate.Text = transferVM.ReadyDate;
                
                Helper.IsOkApplied();
                WaitForm wait1 = new WaitForm(LoadBoth);
                wait1.ShowDialog();
                tbRate.Text = _deal.PerTradeUnitPrice.ToString("n3");
                BindBoth();
                frvn = new FRVN
                {
                    Address = appSett.Address,
                    Logo = appSett.Logo,
                    Name = appSett.Name,
                    Phone = appSett.PhoneNo
                };

                tbLaborCharges.Enabled = tbLaborChargesDescription.Enabled = bswLaborCharges.Value = false;
                bswLaborCharges.Enabled = false;
                Gujjar.TB4(pMain);

                Gujjar.NumbersOnly(tbLaborCharges);
                Gujjar.NumbersOnly(tbVehicleWeightEmpty);
                Gujjar.TBOptional(tbLaborChargesDescription);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void pTop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAddSelector_Click(object sender, EventArgs e)
        {
            try
            {
                AddOilDirtSelectorForm form = new AddOilDirtSelectorForm();
                form.ShowDialog();

                int sel = form.selector.Id;
                if (sel != 0)
                {
                    LoadSelectors();
                    BindCbSelectors();

                    cbSelectors.SelectedItem = cbSelectors.Items.OfType<OilDirtSelector>()
                        .FirstOrDefault(a => a.Id == sel);
                }
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
                AddOilDirtDriverForm form = new AddOilDirtDriverForm();
                form.ShowDialog();

                int driv = form.driver.Id;
                
                if (driv != 0)
                {
                    LoadDrivers();
                    BindCbDrivers();

                    cbDrivers.SelectedItem = cbDrivers.Items.OfType<OilDirtDriver>()
                        .FirstOrDefault(a => a.Id == driv);
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
                if (!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }
                if (cbDrivers.SelectedIndex == -1 || cbSelectors.SelectedIndex == -1)
                {
                    throw new Exception("Please select broker and selector");
                }
                DialogResult rest = Gujjar.ConfirmYesNo("Are you sured? please confirm!");
                if (rest == DialogResult.No)
                    return;

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var oilDirtSchedule = db.OilDirtSchedules.Find(transferVM.ScheduleId);
                            schNo = transferVM.DealSchedule;

                           
                            crvn = new CRVN
                            {
                                Broker = transferVM.Broker,
                                Dated = dtp.Value.ToString(),
                                Driver = driver.Name,
                                Item = transferVM.Item,
                                OilQty = tbLoadedWeight.Text,
                                Selector = selector.Name,
                                Vehicle = tbVehicleNo.Text,
                                EmptyVehicleWeight = tbVehicleWeightEmpty.Text
                            };
                            Code128BarcodeDraw bcode = BarcodeDrawFactory.Code128WithChecksum;
                            Image img = bcode.Draw(oilDirtSchedule.Unum, 50);
                            byte[] imgBytes = Gujjar.GetByteArrayFromImage(img);
                            crvn.Uan = imgBytes;
                            CodeQrBarcodeDraw qrCode = BarcodeDrawFactory.CodeQr;
                            Image img2 = qrCode.Draw("GBS Solutions, 0323-9372084", 50);
                            byte[] qrBytes = Gujjar.GetByteArrayFromImage(img2);
                            crvn.QrCode = qrBytes;


                            oilDirtSchedule.OilDirtSelectorId = selector.Id;
                            oilDirtSchedule.OilDirtDriverId = driver.Id;
                            oilDirtSchedule.VehicleNo = tbVehicleNo.Text;
                            oilDirtSchedule.VehicleWeightEmpty = tbVehicleWeightEmpty.Text.ToDecimal();
                            oilDirtSchedule.LoadedQty = tbLoadedWeight.Text.ToDecimal();
                            oilDirtSchedule.Status = OilDirtScheduleStatus.Departed;
                            oilDirtSchedule.CompleteDate = dtp.Value;

                            if (bsPrinterState.Value)
                            {
                                for (int i = 0; i < (int)numUDCopies.Value; i++)
                                {
                                    PrintReceipt("Floor Scale");
                                    PrintReceipt("Gate pass");
                                }
                            }
                            db.Entry(oilDirtSchedule).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();


                            decimal labourAmount = tbLaborCharges.Text.ToDecimal();
                            string laborAmountDescription = tbLaborChargesDescription.Text;
                            oilDirtSchedule.LaborExpenses = 0;// labourAmount;
                            oilDirtSchedule.LaborExpensesDescription = "N/A";// laborAmountDescription;

                            #region Labor Expenses
                            //#region "Labor expense accounting"
                            //decimal laboramount = tbLaborCharges.Text.ToDecimal();
                            //if (laboramount > 0 && bswLaborCharges.Value)
                            //{
                            //    string finMessage = string.Format("Labor charges amount ({0}) for ({1}-whole sale) in ({2}). Dated ({3}). Description ({4}) by ({5})",
                            //        laboramount, tbDealItem.Text,
                            //        string.Format("Deal: {0}, Schedule: {1}", oilDirtSchedule.OilDirtDealId, oilDirtSchedule.Id),
                            //        dtp.Value, laborAmountDescription,  appUser.Name);

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
                            //    GeneralAccount debitAccount1 = db.Accounts.Find(Properties.Resources.OilDirtWholeSaleLaborExpenseAccount) as GeneralAccount;

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
                            //    db.Entry(dbDayBook).State = System.Data.Entity.EntityState.Modified;
                            //    db.SaveChanges();
                            //    #endregion


                            //}
                            //#endregion
                            #endregion

                            trans.Commit();
                            Gujjar.InfoMsg(string.Format("Schedule status is changed from {0} to {1} successfully.", OilDirtScheduleStatus.Scheduled.ToString(), oilDirtSchedule.Status));
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
        private void PrintReceipt(string type)
        {
            try
            {
                OilFloorScale rep = new OilFloorScale();
                var ptype = rep.Parameters["pType"];
                ptype.Visible = false;
                ptype.Value = type;

                var puser = rep.Parameters["pUser"];
                puser.Visible = false;
                puser.Value = appUser.Id;

                var psch = rep.Parameters["pSch"];
                psch.Visible = false;
                psch.Value = schNo;

                List<FRVN> frvnList = new List<FRVN>
                {
                    frvn
                };
                List<CRVN> crvnList = new List<CRVN>
                {
                    crvn
                };

                DetailReportBand hBand = rep.Bands["DetailReport"] as DetailReportBand;
                DetailReportBand cBand = rep.Bands["DetailReport1"] as DetailReportBand;

                hBand.DataSource = frvnList;
                cBand.DataSource = crvnList;

                rep.ShowPrintMarginsWarning = false;
                rep.ShowPrintStatusDialog = false;
                var lengendsParam = rep.Parameters["Legends"];
                lengendsParam.Visible = false;
                lengendsParam.Value = Helper.Legends();
                if (type.Equals("Gate pass"))
                {
                    XRPictureBox picbox = rep.FindControl("xrpic", true) as XRPictureBox;
                    picbox.Image = Properties.Resources.Gate_50px;
                    picbox.Sizing = ImageSizeMode.StretchImage;
                }
                else
                {
                    XRPictureBox picbox = rep.FindControl("xrpic", true) as XRPictureBox;
                    picbox.Image = Properties.Resources.FloorScale;
                    picbox.Sizing = ImageSizeMode.StretchImage;
                }

                rep.Print(appSett.ThermalPrinter);
            }
            catch (Exception ep)
            {

                throw ep;
            }
        }
        private void bsPrinterState_Click(object sender, EventArgs e)
        {
            numUDCopies.Enabled = bsPrinterState.Value;
        }

        private void cbSelectors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSelectors.SelectedIndex == -1)
                return;

            selector = cbSelectors.SelectedItem as OilDirtSelector;
        }

        private void cbDrivers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDrivers.SelectedIndex == -1)
                return;

            driver = cbDrivers.SelectedItem as OilDirtDriver;
        }

        private void bswLaborCharges_Click(object sender, EventArgs e)
        {
            bool res = bswLaborCharges.Value;
            tbLaborCharges.Enabled = tbLaborChargesDescription.Enabled = res;

            if (res)
            {
                tbLaborCharges.Text = "";
            }
            else
            {
                tbLaborCharges.Text = "0.0";
            }
        }
    }
}