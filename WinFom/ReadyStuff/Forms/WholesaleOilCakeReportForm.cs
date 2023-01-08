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
using WinFom.ReadyStuff.ReportViewModel;
using Model.Admin.Model;
using WinFom.Common.Model;
using Zen.Barcode;
using WinFom.Common.Forms;
using WinFom.ReadyStuff.Report;
using DevExpress.XtraReports.UI;
using Model.ReadyStuff.Model;

namespace WinFom.ReadyStuff.Forms
{
    public partial class WholesaleOilCakeReportForm : Form
    {
        private AppSettings appSett = Helper.AppSet;
        public WholesaleOilCakeReportForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private List<RCompRVM> rcompRVMList = null;
        private List<RScheduleRVM> rscheduleRVMList = null;
        private List<RSummaryRVM> rsummaryList = null;
        private void btnTodayOnly_Click(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait = new WaitForm(LoadData);
                wait.ShowDialog();

                WholesaleOilCakeReport rep = new WholesaleOilCakeReport();

                DetailReportBand band1 = rep.Bands["DetailReport"] as DetailReportBand;
                DetailReportBand band2 = rep.Bands["DetailReport1"] as DetailReportBand;
                DetailReportBand band3 = rep.Bands["DetailReport3"] as DetailReportBand;

                band1.DataSource = rcompRVMList;
                band2.DataSource = rscheduleRVMList;
                band3.DataSource = rsummaryList;

                rep.ShowPrintMarginsWarning = false;
                rep.ShowPrintStatusDialog = false;

                var lengendsParam = rep.Parameters["Legends"];
                lengendsParam.Visible = false;
                lengendsParam.Value = Helper.Legends();

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
                using (Context db = new Context())
                {
                    if (rcompRVMList != null)
                    {
                        rcompRVMList.Clear();
                        rcompRVMList = null;
                    }
                    if (rscheduleRVMList != null)
                    {
                        rscheduleRVMList.Clear();
                        rscheduleRVMList = null;
                    }
                    if(rsummaryList != null)
                    {
                        rsummaryList.Clear();
                        rsummaryList = null;
                    }
                    rcompRVMList = new List<RCompRVM>();
                    rscheduleRVMList = new List<RScheduleRVM>();
                    rsummaryList = new List<RSummaryRVM>();

                    

                    RCompRVM rcomp = new RCompRVM
                    {
                        Address = appSett.Address,
                        Barcode = null,
                        Logo = appSett.Logo,
                        Name = appSett.Name,
                        Phone = appSett.PhoneNo,
                        Qrcode = null,
                        RepDate = DateTime.Now.ToString(),
                        RepTitle = "Whole sale Oil Cake Report"
                    };

                    Code128BarcodeDraw bcode = BarcodeDrawFactory.Code128WithChecksum;
                    Image img = bcode.Draw(Helper.Unum, 50);
                    byte[] imgBytes = Gujjar.GetByteArrayFromImage(img);
                    rcomp.Barcode = imgBytes;

                    CodeQrBarcodeDraw qrCode = BarcodeDrawFactory.CodeQr;
                    Image img2 = qrCode.Draw("GBS Solutions, Burewala. 0323-9372084", 50);
                    byte[] qrBytes = Gujjar.GetByteArrayFromImage(img2);
                    rcomp.Qrcode = qrBytes;

                    rcompRVMList.Add(rcomp);

                    DateTime today = DateTime.Now.Date;
                    var schs = db.ReadySchedules.AsParallel().ToList()
                        .Where(a => a.ReadyDate.Date == today && a.IsCompleted).ToList();
                    foreach (var item in schs)
                    {
                        var deal = db.ReadyDeals.Find(item.ReadyDealId);
                        deal.Broker = db.ReadyBrokers.Find(deal.ReadyBrokerId);
                        deal.TradeUnit = db.ReadyTradeUnits.Find(deal.ReadyTradeUnitId);
                        item.Driver = db.ReadyDrivers.Find(item.ReadyDriverId);
                        item.Selector = db.ReadySelectors.Find(item.ReadySelectorId);
                        item.Bharthies = db.Bharthies.Where(a => a.ReadyScheduleId == item.Id).ToList();

                        RScheduleRVM rschedule = new RScheduleRVM
                        {
                            BrokerAmount = item.BrokerShareAmount,
                            Broker = deal.Broker.Name,
                            BrokerSharePercentage = item.BrokerSharePercentage,
                            DateTime = item.DispatchedDate.Value.ToString(),
                            Driver = item.Driver.Name,
                            EffectivePrice = item.NetScheduleAmount,
                            LoadedQty = item.Bharthies.Sum(a => a.Total),
                            MondsUnit = deal.TradeUnit.UnitQty,
                            PerMondRate = item.PerTradeUnitPrice,
                            RPackingRVMList = new List<RPackingRVM>(),
                            Selector = item.Selector.Name,
                            SrNo = item.ScheduleNo,
                            TotalMonds = item.TotalTradeUnits,
                            TotalPrice = item.GrossPrice,
                            VehicleNo = item.VehicleNo,
                            VehicleWeightEmpty = item.EmptyVehicleWeight,
                            VehicleWeightFull = item.FullVehicleWeight,
                            WeighBridgeWeight = item.WeighBridgeWeight
                        };
                        int srno = 1;
                        foreach (var item2 in item.Bharthies)
                        {
                            item2.BharthiType = db.BharthiTypes.Find(item2.BharthiTypeId);
                            RPackingRVM rpack = new RPackingRVM
                            {
                                Packing = item2.BharthiType.Title,
                                Qty = item2.Qty,
                                SrNo = srno++
                            };
                            rpack.Total = rpack.Qty * item2.BharthiType.UnitQty;
                            rschedule.RPackingRVMList.Add(rpack);
                        }

                        rscheduleRVMList.Add(rschedule);
                    }
                    RSummaryRVM rsummary = new RSummaryRVM
                    {
                        BrokerShareAmount = schs.Sum(a => a.BrokerShareAmount),
                        NetAmount = schs.Sum(a => a.NetScheduleAmount),
                        TotalLoadedWeight = rscheduleRVMList.Sum(a => a.LoadedQty),
                        TotalPackings = schs.Sum(a => a.TotalPackings),
                        TotalPrice = schs.Sum(a => a.GrossPrice),
                        TotalWeighBridgeWeight = schs.Sum(a => a.WeighBridgeWeight)
                    };
                    rsummaryList.Add(rsummary);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        #region "Broker trial balance report"
        List<ReadyBroker> brokers = null;
        ReadyBrokerTrailBalanceReport trialRep = null;
        List<BrokerTrialRVM> dataDatasource = null;
        private void LoadTrialData()
        {
            trialRep = new ReadyBrokerTrailBalanceReport();
            dataDatasource = new List<BrokerTrialRVM>();
            using (Context db = new Context())
            {
                brokers = db.ReadyBrokers.Where(a => !a.Name.Equals("N/A")).OrderBy(a => a.Name).ToList();
                foreach (var item in brokers)
                {
                    decimal balance = 0;
                    var trans = db.AccountTransactions.Where(a => a.GeneralAccountId == item.GeneralAccountId)
                        .OrderByDescending(a => a.Id).FirstOrDefault();
                    if (trans != null)
                    {
                        balance = trans.Balance;
                    }

                    BrokerTrialRVM trialvm = new BrokerTrialRVM
                    {
                        Balance = balance,
                        Broker = item.Name,
                        CellNo = item.Contact
                    };

                    dataDatasource.Add(trialvm);
                }
            }
        }
        private void btnTrialBalance_Click(object sender, EventArgs e)
        {
            try
            {
                List<RCompRVM> compDatasource = new List<RCompRVM>();
                WaitForm wait = new WaitForm(LoadTrialData);
                wait.ShowDialog();
                if (dataDatasource.Count == 0)
                {
                    throw new Exception("There is no broker's data to show in report");
                }
                RCompRVM compvm = new RCompRVM
                {
                    Address = appSett.Address,
                    Barcode = null,
                    Logo = appSett.Logo,
                    Name = appSett.Name,
                    Phone = "Phone No. " + appSett.PhoneNo,
                    Qrcode = null,
                    RepDate = string.Format("Report date time {0}", DateTime.Now.ToString()),
                    RepTitle = "Whole sale broker trial balances"
                };
                compDatasource.Add(compvm);

                DetailReportBand compBand = trialRep.Bands["DetailReport"] as DetailReportBand;
                DetailReportBand dataBand = trialRep.Bands["DetailReport1"] as DetailReportBand;

                compBand.DataSource = compDatasource;
                dataBand.DataSource = dataDatasource;

                var tbal = trialRep.Parameters["TotalBalance"];
                tbal.Value = dataDatasource.Sum(a => a.Balance).ToString("n1");
                tbal.Visible = false;

                var lengendsParam = trialRep.Parameters["Legends"];
                lengendsParam.Visible = false;
                lengendsParam.Value = Helper.Legends();

                trialRep.ShowPrintMarginsWarning = false;
                trialRep.ShowPrintStatusDialog = false;

                trialRep.ShowPreview();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        #endregion
    }
}
