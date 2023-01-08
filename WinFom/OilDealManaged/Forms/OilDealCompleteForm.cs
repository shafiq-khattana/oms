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
using WinFom.ReadyStuff.Forms;
using WinFom.Deal.Forms;
using Model.Financials.Model;

namespace WinFom.OilDealManaged.Forms
{
    public partial class OilDealCompleteForm : Form
    {
        private OilDealVM1 oilDealVm = null;
        public bool IsDone = false;
        AppSettings appSett = Helper.AppSet;
        private List<WeighBridge> weighBridges = null;
        private WeighBridge weighBridge = null;
        private string appUser = "N/A";
        private OilDeal oilDeal = null;

        private string schNo = "N/A";
        public OilDealCompleteForm(OilDealVM1 vm)
        {
            InitializeComponent();
            oilDealVm = vm;

        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void LoadWeighBridges()
        {
            try
            {
                using (Context db = new Context())
                {
                    weighBridges = db.WeighBridges.Where(a => a.WeighBrideType == WeighBridgeType.Receiving)
                        .OrderBy(a => a.Name).ToList();

                    oilDeal = db.OilDeals.Find(oilDealVm.Id);

                    oilDeal.TradeUnit = db.OilTradeUnits.Find(oilDeal.OilTradeUnitId);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindCBWeighBridges()
        {
            cbWeighBridges.DataSource = weighBridges;
            cbWeighBridges.DisplayMember = "Name";
            cbWeighBridges.ValueMember = "Id";
        }
        private void LoadBoth()
        {
            LoadWeighBridges();
        }
        private void BindBoth()
        {
            BindCBWeighBridges();
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                tbBroker.Text = oilDealVm.Broker;
                tbDealDate.Text = oilDealVm.DealDate;
                tbDealItem.Text = oilDealVm.Item;
                tbDealNo.Text = oilDealVm.Id.ToString();
                tbDealQty.Text = oilDealVm.DealQty;
                tbLoadedWeight.Text = oilDealVm.VehicleLoadedQty;
                tbReadyDate.Text = oilDealVm.ReadyDate;
                tbDriver.Text = oilDealVm.Driver;
                tbSelector.Text = oilDealVm.Selector;
                tbVehicleNo.Text = oilDealVm.VehicleNo;
                tbVehicleWeightEmpty.Text = oilDealVm.VehicleEmptyWeight;

                WaitForm wait1 = new WaitForm(LoadBoth);
                wait1.ShowDialog();
                BindBoth();

                tbBrokerSharePercentage.Enabled = appSett.AllowToChangeOilBrokerSharePercentage;
                tbBrokerSharePercentage.Text = appSett.OilBrokerSharePercentage.ToString("n3");
                tbTradeUnit.Text = oilDeal.TradeUnit.Title;

                tbPerTradeUnitQty.Text = oilDeal.TradeUnit.UnitQty.ToString("n3");
                tbPerTradeUnitPrice.Text = oilDeal.PerTradeUnitPrice.ToString("n3");
                tbRate.Text = oilDeal.PerTradeUnitPrice.ToString("n3");

                //decimal vehicleWeightEmpty = tbVehicleWeightEmpty.Text.ToDecimal();
                //decimal loadQty = tbLoadedWeight.Text.ToDecimal();
                //decimal totalWt = vehicleWeightEmpty + loadQty;
                //tbVehicleWeightLoaded.Text = totalWt.ToString("n3");
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

        List<DayBook> pDaybooks = new List<DayBook>();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }
                if (!Helper.ConfirmUserPassword())
                {
                    return;
                }

                DialogResult res = Gujjar.ConfirmYesNo("Please confirm ... All information is correct before to proceed on?");
                if (res == DialogResult.No)
                    return;

                if (weighBridge == null || weighBridge.Name == "N/A")
                {
                    throw new Exception("Please choose a weigh bridge");
                }
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var oilDeal = db.OilDeals.Find(oilDealVm.Id);
                            schNo = string.Format("Deal No. {0}", oilDeal.Id);

                            appUser = SingleTon.LoginForm.appUser.Name;


                            #region "Financial transactions"
                            #region Item whole sale Credit account
                            string wholeSaleFinMessage = string.Format("Whole Sale of item ({0}). Qty ({1}) Kg. Total Price ({2}). Broker ({3}). Brokery ({4}). Net Price ({5}). Schedule No ({6}). By ({7})",
                                                                                tbDealItem.Text, tbWeighBridgeWeight.Text, tbTotalPrice.Text, tbBroker.Text,
                                                                               tbBrokerShare.Text, tbNetPrice.Text, schNo, appUser);
                            decimal grossPrice = tbTotalPrice.Text.ToDecimal();
                            DayBook wholeSaleItemDayBook = new DayBook
                            {
                                Id = 0,
                                Amount = grossPrice,
                                Date = dtp.Value,
                                Description = wholeSaleFinMessage,
                                CanRollBack = false,
                                OilDealId = oilDeal.Id,
                                InDate = DateTime.Now.Date
                            };
                            wholeSaleItemDayBook = db.DayBooks.Add(wholeSaleItemDayBook);
                            db.SaveChanges();

                            OilDealItem dealItem = db.OilDealItems.Find(oilDeal.OilDealItemId);
                            GeneralAccount wholeSaleItemAccount = db.Accounts.Find(dealItem.GeneralAccountId) as GeneralAccount;

                            AccountTransaction itemCreditTrans = new AccountTransaction
                            {
                                Id = 0,
                                GeneralAccountId = wholeSaleItemAccount.Id,
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Credit,
                                CreditAmount = grossPrice,
                                Balance = grossPrice,
                                Date = DateTime.Now,
                                DayBookId = wholeSaleItemDayBook.Id,
                                DebitAmount = 0,
                                Description = wholeSaleFinMessage
                            };

                            var itemDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == wholeSaleItemAccount.Id)
                                .AsParallel().ToList().Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date)
                                .OrderByDescending(a => a.Id)
                                .FirstOrDefault();
                            if (itemDbEntry != null)
                            {
                                itemCreditTrans.Balance += itemDbEntry.Balance;
                            }
                            itemCreditTrans = db.AccountTransactions.Add(itemCreditTrans);
                            db.SaveChanges();

                            var wholeSaleItemDayBookDb = db.DayBooks.Find(wholeSaleItemDayBook.Id);
                            wholeSaleItemDayBookDb.CreditTrace = string.Format("{0}. Trans Id: {1}", wholeSaleItemAccount.Title, itemCreditTrans.Id);
                            wholeSaleItemDayBookDb.DebitTrace = "N/A";
                            wholeSaleItemDayBookDb.CreditAccountId = wholeSaleItemAccount.Id;
                            wholeSaleItemDayBookDb.DebitAccountId = null;
                            db.Entry(wholeSaleItemDayBookDb).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            pDaybooks.Add(wholeSaleItemDayBookDb);

                            #endregion

                            #region Brokery expense
                            string brokeryExpenseMessage = string.Format("Brokery (oil) expense amount ({0}). Broker share percetnage ({1}%). Total Amount ({2}). ({3}). By ({4})",
                                tbBrokerShare.Text, tbBrokerSharePercentage.Text, tbTotalPrice.Text, schNo, appUser);
                            OilDealBroker oilDealBroker = db.OilDealBrokers.Find(oilDeal.OilDealBrokerId);
                            decimal brokerShareAmount = tbBrokerShare.Text.ToDecimal();
                            DayBook daybookBrokeryExpense = new DayBook
                            {
                                Id = 0,
                                DebitTrace = "",
                                Date = dtp.Value,
                                Amount = brokerShareAmount,
                                Description = brokeryExpenseMessage,
                                CanRollBack = false,
                                OilDealId = oilDeal.Id,
                                InDate = DateTime.Now.Date
                            };

                            daybookBrokeryExpense = db.DayBooks.Add(daybookBrokeryExpense);
                            db.SaveChanges();

                            GeneralAccount brokeryExpenseAccount = db.Accounts.Find(oilDealBroker.BrokerExpenseAccountId) as GeneralAccount;
                            decimal brokeryExpenseAmount = brokerShareAmount;
                            AccountTransaction brokeryExpenseDebitTrans = new AccountTransaction
                            {
                                Id = 0,
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Debit,
                                Balance = brokeryExpenseAmount,
                                CreditAmount = 0,
                                Date = DateTime.Now,
                                DayBookId = daybookBrokeryExpense.Id,
                                Description = brokeryExpenseMessage,
                                DebitAmount = brokeryExpenseAmount,
                                GeneralAccountId = brokeryExpenseAccount.Id
                            };

                            var expenseDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == brokeryExpenseAccount.Id)
                                .AsParallel().ToList().Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date)
                                .OrderByDescending(a => a.Id)
                                .FirstOrDefault();
                            if (expenseDbEntry != null)
                            {
                                brokeryExpenseDebitTrans.Balance += expenseDbEntry.Balance;
                            }
                            brokeryExpenseDebitTrans = db.AccountTransactions.Add(brokeryExpenseDebitTrans);
                            db.SaveChanges();

                            var daybookExpenseDb = db.DayBooks.Find(daybookBrokeryExpense.Id);
                            daybookExpenseDb.DebitTrace = string.Format("{0}. Trans Id: {1}", brokeryExpenseAccount.Title, brokeryExpenseDebitTrans.Id);
                            daybookExpenseDb.CreditTrace = "N/A";

                            daybookExpenseDb.DebitAccountId = brokeryExpenseAccount.Id;
                            daybookExpenseDb.CreditAccountId = null;

                            db.Entry(daybookExpenseDb).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            pDaybooks.Add(daybookExpenseDb);
                            #endregion

                            #region Broker Receivable amount
                            decimal netAoumnt = tbNetPrice.Text.ToDecimal();
                            string brokerRxAmntStr = string.Format("Whole Sale (oil) of ({0}), total qty ({1}-kg), total price ({2}), broker ({3}), broker share ({4}), receivable amount ({5}), By ({6})",
                                schNo, tbWeighBridgeWeight.Text, tbTotalPrice.Text, tbBroker.Text, brokerShareAmount.ToString("n3"), netAoumnt.ToString("n3"), appUser);


                            DayBook daybookNet = new DayBook
                            {
                                Id = 0,
                                Amount = netAoumnt,
                                CreditTrace = "",
                                Date = dtp.Value,
                                DebitTrace = "",
                                Description = brokerRxAmntStr,
                                CanRollBack = false,
                                OilDealId = oilDeal.Id,
                                InDate = DateTime.Now.Date
                            };
                            daybookNet = db.DayBooks.Add(daybookNet);
                            db.SaveChanges();

                            GeneralAccount brokerDebitAccount = db.Accounts.Find(oilDealBroker.GeneralAccountId) as GeneralAccount;
                            AccountTransaction netDebitTrans = new AccountTransaction
                            {
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Debit,
                                GeneralAccountId = brokerDebitAccount.Id,
                                CreditAmount = 0,
                                Id = 0,
                                DebitAmount = netAoumnt,
                                Balance = netAoumnt,
                                Date = DateTime.Now,
                                DayBookId = daybookNet.Id,
                                Description = brokerRxAmntStr
                            };

                            var netDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == brokerDebitAccount.Id)
                                .AsParallel().ToList().Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date)
                                .OrderByDescending(a => a.Id)
                                .FirstOrDefault();
                            if (netDbEntry != null)
                            {
                                netDebitTrans.Balance += netDbEntry.Balance;
                            }
                            netDebitTrans = db.AccountTransactions.Add(netDebitTrans);
                            db.SaveChanges();

                            var daybooknetdb = db.DayBooks.Find(daybookNet.Id);
                            daybooknetdb.DebitTrace = string.Format("{0}. Trans Id: {1}", brokerDebitAccount.Title, netDebitTrans.Id);
                            daybooknetdb.CreditTrace = "N/A";
                            daybooknetdb.DebitAccountId = brokerDebitAccount.Id;
                            daybooknetdb.CreditAccountId = null;
                            db.Entry(daybooknetdb).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            pDaybooks.Add(daybooknetdb);

                            #endregion

                            #endregion




                            Code128BarcodeDraw bcode = BarcodeDrawFactory.Code128WithChecksum;
                            Image img = bcode.Draw(oilDeal.Unum, 50);
                            byte[] imgBytes = Gujjar.GetByteArrayFromImage(img);

                            CodeQrBarcodeDraw qrCode = BarcodeDrawFactory.CodeQr;
                            Image img2 = qrCode.Draw("GBS Solutions, Burewala. 0323-9372084", 50);
                            byte[] qrBytes = Gujjar.GetByteArrayFromImage(img2);

                            oilDeal.WeighBridgeWeight = tbWeighBridgeWeight.Text.ToDecimal();
                            oilDeal.TotalTradeUnits = tbTotalTradeUnits.Text.ToDecimal();
                            oilDeal.TotalPrice = tbTotalPrice.Text.ToDecimal();
                            oilDeal.BrokerSharePercentage = tbBrokerSharePercentage.Text.ToDecimal();
                            oilDeal.BrokerShareAmount = tbBrokerShare.Text.ToDecimal();
                            oilDeal.NetPrice = tbNetPrice.Text.ToDecimal();
                            oilDeal.VehicleNo = tbVehicleNo.Text;
                            oilDeal.VehicleEmptyWeight = tbVehicleWeightEmpty.Text.ToDecimal();
                            oilDeal.VehicleScheduleQty = tbLoadedWeight.Text.ToDecimal();
                            oilDeal.WeighBridgeId = weighBridge.Id;
                            oilDeal.Status = OilDealStatus.Completed;

                            oilDeal.CompleteDate = dtp.Value;


                            db.Entry(oilDeal).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();


                            trans.Commit();
                            if (appSett.PrintFinancialTransactions)
                            {
                                Helper.PrintTransactions(pDaybooks);
                            }
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
                puser.Value = appUser;

                var psch = rep.Parameters["pSch"];
                psch.Visible = false;
                psch.Value = schNo;



                DetailReportBand hBand = rep.Bands["DetailReport"] as DetailReportBand;
                DetailReportBand cBand = rep.Bands["DetailReport1"] as DetailReportBand;


                rep.ShowPrintMarginsWarning = false;
                rep.ShowPrintStatusDialog = false;

                rep.Print(appSett.ThermalPrinter);
            }
            catch (Exception ep)
            {

                throw ep;
            }
        }



        private void tbVehicleWeightEmpty_TextChanged(object sender, EventArgs e)
        {

        }

        private void pMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void btnAddWeighBridge_Click(object sender, EventArgs e)
        {
            try
            {
                AddWeighBridgeForm form = new AddWeighBridgeForm(WeighBridgeType.Receiving);
                form.ShowDialog();
                int id = form.weighBridgeId;
                if (id != 0)
                {
                    WaitForm form2 = new WaitForm(LoadWeighBridges);
                    form2.ShowDialog();

                    BindCBWeighBridges();

                    cbWeighBridges.SelectedItem = cbWeighBridges.Items.OfType<WeighBridge>().FirstOrDefault(a => a.Id == id);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }


        private void tbWeighBridgeWeight_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal loadedQty = tbLoadedWeight.Text.ToDecimal();
                decimal weighBridgeQty = 0;
                string txt = tbWeighBridgeWeight.Text;
                if (string.IsNullOrEmpty(txt))
                {
                    weighBridgeQty = 0;
                }
                else
                {
                    weighBridgeQty = txt.ToDecimal();
                }
                decimal diff = loadedQty - weighBridgeQty;

                tbWeightDifference.Text = diff.ToString("n4");

                decimal perTradeUnitQty = 0;
                string txt2 = tbPerTradeUnitQty.Text;
                if (!string.IsNullOrEmpty(txt2))
                {
                    perTradeUnitQty = txt2.ToDecimal();
                }

                decimal totalTradeUnits = weighBridgeQty / perTradeUnitQty;
                tbTotalTradeUnits.Text = totalTradeUnits.ToString("n4");

                decimal perTradeUnitPrice = 0;
                string txt3 = tbPerTradeUnitPrice.Text;
                if (!string.IsNullOrEmpty(txt3))
                {
                    perTradeUnitPrice = txt3.ToDecimal();
                }

                decimal totalPrice = perTradeUnitPrice * totalTradeUnits;
                tbTotalPrice.Text = totalPrice.ToString("n4");

                decimal brokerSharePercentage = 0;
                string txt4 = tbBrokerSharePercentage.Text;
                if (!string.IsNullOrEmpty(txt4))
                {
                    brokerSharePercentage = txt4.ToDecimal();
                }

                decimal brokershareamount = totalPrice * brokerSharePercentage / 100;

                tbBrokerShare.Text = brokershareamount.ToString("n4");

                decimal netPrice = totalPrice - brokershareamount;
                tbNetPrice.Text = netPrice.ToString("n4");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void cbTradeUnits_SelectedIndexChanged(object sender, EventArgs e)
        {

            decimal weighBridgeQty = 0;
            string txt = tbWeighBridgeWeight.Text;
            if (string.IsNullOrEmpty(txt))
            {
                return;
            }

            //tradeUnit = cbTradeUnits.SelectedItem as ReadyTradeUnit;
            //tbPerTradeUnitQty.Text = tradeUnit.UnitQty.ToString("n4");
            weighBridgeQty = txt.ToDecimal();

            //decimal totalTradeUnits = weighBridgeQty / tradeUnit.UnitQty;
            //tbTotalTradeUnits.Text = totalTradeUnits.ToString("n4");
        }

        private void tbPerTradeUnitPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal totalTradeUnits = 0;
                if (!string.IsNullOrEmpty(tbTotalTradeUnits.Text))
                {
                    totalTradeUnits = tbTotalTradeUnits.Text.ToDecimal();
                }

                decimal perTradeUnitPrice = 0;
                if (!string.IsNullOrEmpty(tbPerTradeUnitPrice.Text))
                {
                    perTradeUnitPrice = tbPerTradeUnitPrice.Text.ToDecimal();
                }

                decimal totalPrice = totalTradeUnits * perTradeUnitPrice;

                tbTotalPrice.Text = totalPrice.ToString("n4");

                if (!tbBrokerSharePercentage.Enabled)
                {
                    decimal brokerSharePercen = 0;
                    if (!string.IsNullOrEmpty(tbBrokerSharePercentage.Text))
                    {
                        brokerSharePercen = tbBrokerSharePercentage.Text.ToDecimal();
                    }
                    decimal brokerAmount = totalPrice * brokerSharePercen / 100;
                    tbBrokerShare.Text = brokerAmount.ToString("n4");

                    decimal netPrice = totalPrice - brokerAmount;
                    tbNetPrice.Text = netPrice.ToString("n4");
                }

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbBrokerSharePercentage_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tbBrokerSharePercentage.Enabled)
                {

                    decimal totalPrice = 0;
                    if (!string.IsNullOrEmpty(tbTotalPrice.Text))
                    {
                        totalPrice = tbTotalPrice.Text.ToDecimal();
                    }
                    decimal bshper = 0;
                    if (!string.IsNullOrEmpty(tbBrokerSharePercentage.Text))
                    {
                        bshper = tbBrokerSharePercentage.Text.ToDecimal();

                    }

                    decimal brokerShare = totalPrice * bshper / 100;
                    decimal netprice = totalPrice - brokerShare;
                    tbBrokerShare.Text = brokerShare.ToString("n4");
                    tbNetPrice.Text = netprice.ToString("n4");
                }


            }
            catch (Exception)
            {
                //Gujjar.ErrMsg(ep);
            }
        }

        private void tbVehicleWeightLoaded_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal fullWeight = 0;
                if (!string.IsNullOrEmpty(tbVehicleWeightLoaded.Text))
                {
                    fullWeight = tbVehicleWeightLoaded.Text.ToDecimal();
                }
                decimal vehicleWeight = tbVehicleWeightEmpty.Text.ToDecimal();
                decimal weighWeight = fullWeight - vehicleWeight;

                tbWeighBridgeWeight.Text = weighWeight.ToString("n3");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void cbWeighBridges_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbWeighBridges.SelectedIndex == -1)
            {
                weighBridge = null;
                return;
            }
            if (cbWeighBridges.Text == "N/A")
            {
                weighBridge = null;
                return;
            }
            weighBridge = cbWeighBridges.SelectedItem as WeighBridge;
        }
    }
}