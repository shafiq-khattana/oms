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
using Model.Financials.Model;
using System.Data.Entity;
using DevExpress.XtraPrinting;

namespace WinFom.OilDealManaged.Forms
{
    public partial class OilDealProcessForm : Form
    {
        private OilDealVM1 oilDealVm = null;
        private List<OilDealSelector> selectorList = null;
        private List<OilDealDriver> driverList = null;
        private OilDealSelector selector = null;
        private OilDealDriver driver = null;
        public bool IsDone = false;
        AppSettings appSett = Helper.AppSet;
        private CRVN crvn = null;
        private FRVN frvn = null;
        private AppUser appUser = SingleTon.LoginForm.appUser;
        private string schNo = "N/A";
        public OilDealProcessForm(OilDealVM1 vm)
        {
            InitializeComponent();
            oilDealVm = vm;
            
        }
        private void LoadDrivers()
        {
            try
            {
                using (Context db = new Context())
                {
                    driverList = db.OilDealDrivers.Where(a => a.IsActive).ToList();
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

                    selectorList = db.OilDealSelectors.Where(a => a.IsActive).ToList();
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
                Helper.IsOkApplied();
                tbBroker.Text = oilDealVm.Broker;
                tbDealDate.Text = oilDealVm.DealDate;
                tbDealItem.Text = oilDealVm.Item;
                tbDealNo.Text = oilDealVm.Id.ToString();
                tbDealQty.Text = oilDealVm.DealQty;
                tbLoadedWeight.Text = oilDealVm.DealQty;
                tbReadyDate.Text = oilDealVm.ReadyDate;

                WaitForm wait1 = new WaitForm(LoadBoth);
                wait1.ShowDialog();

                BindBoth();
                frvn = new FRVN
                {
                    Address = appSett.Address,
                    Logo = appSett.Logo,
                    Name = appSett.Name,
                    Phone = appSett.PhoneNo
                };

                tbRate.Text = oilDealVm.PerTradeUnitPrice;
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
                AddOilSelectorForm form = new AddOilSelectorForm();
                form.ShowDialog();

                var sel = form.selector;
                if (sel != null)
                {
                    LoadSelectors();
                    BindCbSelectors();

                    cbSelectors.SelectedItem = cbSelectors.Items.OfType<OilDealSelector>()
                        .FirstOrDefault(a => a.Id == sel.Id);
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
                AddOilDriverForm form = new AddOilDriverForm();
                form.ShowDialog();

                var driv = form.driver;
                if (driv != null)
                {
                    LoadDrivers();
                    BindCbDrivers();

                    cbDrivers.SelectedItem = cbDrivers.Items.OfType<OilDealDriver>()
                        .FirstOrDefault(a => a.Id == driv.Id);
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

                if(!Helper.ConfirmUserPassword())
                {
                    return;
                }

                DialogResult res = Gujjar.ConfirmYesNo("Please confirm, before to proceed");
                if (res == DialogResult.No)
                    return;

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var oilDeal = db.OilDeals.Find(oilDealVm.Id);
                            schNo = string.Format("Deal No. {0}", oilDeal.Id);
                            decimal laborCharges = tbLaborCharges.Text.ToDecimal();
                            string laborDescription = tbLaborChargesDescription.Text;


                            crvn = new CRVN
                            {
                                Broker = oilDealVm.Broker,
                                Dated = dtp.Value.ToString(),
                                Driver = driver.Name,
                                EmptyVehicleWeight = tbVehicleWeightEmpty.Text,
                                Item = oilDealVm.Item,
                                OilQty = tbLoadedWeight.Text,
                                Selector = selector.Name,
                                Vehicle = tbVehicleNo.Text,
                            };
                            Code128BarcodeDraw bcode = BarcodeDrawFactory.Code128WithChecksum;
                            Image img = bcode.Draw(oilDeal.Unum, 50);
                            byte[] imgBytes = Gujjar.GetByteArrayFromImage(img);
                            crvn.Uan = imgBytes;
                            CodeQrBarcodeDraw qrCode = BarcodeDrawFactory.CodeQr;
                            Image img2 = qrCode.Draw("GBS Solutions, Burewala. 0323-9372084", 50);
                            byte[] qrBytes = Gujjar.GetByteArrayFromImage(img2);
                            crvn.QrCode = qrBytes;


                            oilDeal.OilDealSelectorId = selector.Id;
                            oilDeal.OilDealDriverId = driver.Id;
                            oilDeal.VehicleNo = tbVehicleNo.Text;
                            oilDeal.VehicleEmptyWeight = tbVehicleWeightEmpty.Text.ToDecimal();
                            oilDeal.VehicleScheduleQty = tbLoadedWeight.Text.ToDecimal();
                            oilDeal.Status = OilDealStatus.Departed;
                            oilDeal.CompleteDate = dtp.Value;
                            oilDeal.LaborExpenses = 0;// laborCharges;
                            oilDeal.LaborExpensesDescription = "N/A";// laborDescription;
                            oilDeal.WeighBridge = null;
                            oilDeal.WeighBridgeId = null;
                            if (bsPrinterState.Value)
                            {
                                for (int i = 0; i < (int)numUDCopies.Value; i++)
                                {
                                    PrintReceipt("Floor Scale");
                                    PrintReceipt("Gate pass");
                                }
                            }
                            db.Entry(oilDeal).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            #region Financial accounting
                            //#region "Labor expense accounting"
                            //decimal laboramount = laborCharges;
                            //if (laboramount > 0 && bswLaborCharges.Value)
                            //{
                            //    string finMessage = string.Format("Labor charges amount ({0}) for ({1}-whole sale) in ({2}). Dated ({3}). Description ({4}) by ({5})", laboramount,
                            //        string.Format("Deal: {0}", oilDeal.Id), tbDealItem.Text, dtp.Value, laborDescription, appUser.Name);

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
                            //    GeneralAccount debitAccount1 = db.Accounts.Find(Properties.Resources.CrudeOilWholeSaleLaborExpenseAccount) as GeneralAccount;

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
                            

                            trans.Commit();

                            Gujjar.InfoMsg(string.Format("Deal status is changed from {0} to {1} successfully.", OilDealStatus.Scheduled.ToString(), oilDeal.Status));
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
                puser.Value = appUser.Name;

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

                if(type == "Floor Scale")
                {
                    XRPictureBox picbox = rep.FindControl("xrpic", true) as XRPictureBox;
                    picbox.Image = Properties.Resources.FloorScale;
                    picbox.Sizing = ImageSizeMode.StretchImage;
                }
                else
                {
                    XRPictureBox picbox = rep.FindControl("xrpic", true) as XRPictureBox;
                    picbox.Image = Properties.Resources.Gate_50px;
                    picbox.Sizing = ImageSizeMode.StretchImage;
                }
                var lengendsParam = rep.Parameters["Legends"];
                lengendsParam.Visible = false;
                lengendsParam.Value = Helper.Legends();

                rep.ShowPrintMarginsWarning = false;
                rep.ShowPrintStatusDialog = false;

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

            selector = cbSelectors.SelectedItem as OilDealSelector;
        }

        private void cbDrivers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDrivers.SelectedIndex == -1)
                return;

            driver = cbDrivers.SelectedItem as OilDealDriver;
        }

        private void bswLaborCharges_Click(object sender, EventArgs e)
        {
            bool res = bswLaborCharges.Value;
            tbLaborCharges.Enabled = tbLaborChargesDescription.Enabled = res;

            if(res)
            {
                tbLaborCharges.Text = "";
            }
            else
            {
                tbLaborCharges.Text = "0.0";
            }
        }

        private void tbDealQty_TextChanged(object sender, EventArgs e)
        {
            tbLoadedWeight.Text = tbDealQty.Text;
        }
    }
}