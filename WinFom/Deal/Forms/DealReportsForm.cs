using Khattana.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WinFom.Admin.Database;
using Model.Deal.Model;
using WinFom.ReadyStuff.ReportViewModel;
using Model.Admin.Model;
using WinFom.Common.Model;
using WinFom.Common.Forms;
using WinFom.ReadyStuff.Report;
using DevExpress.XtraReports.UI;
using WinFom.Deal.Reports.XtraReports;
using WinFom.Deal.Reports.RepModel;

namespace WinFom.Deal.Forms
{
    public partial class DealReportsForm : Form
    {
        private AppSettings appSett = Helper.AppSet;
        public DealReportsForm()
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

        
        
        
        #region "Broker trial balance report"
        List<Company> companies = null;
        CottonSeedCompaniesTrialBalanceReport trialRep = null;
        List<BrokerTrialRVM> dataDatasource = null;
        private void LoadTrialData()
        {
            trialRep = new CottonSeedCompaniesTrialBalanceReport();
            dataDatasource = new List<BrokerTrialRVM>();
            using (Context db = new Context())
            {
                companies = db.Companies.Where(a => !a.Name.Equals("N/A")).OrderBy(a => a.Name).ToList();
                foreach (var item in companies)
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
                    throw new Exception("There is no company's data to show in report");
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
                    RepTitle = "Cotton seed companies trial balances"
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


        #region Schedule received
        RecSchReport recschrep = null;
        List<RecSchRVM> recschrvmDatasource = null;
        List<RecSchSummaryRVM> recschSummaryDatasource = null;
        List<RecCompRVM> reccomprvmDatasource = null;
        DateTime dateFrom = DateTime.Now;
        DateTime dateTo = DateTime.Now;
        private void btnReceivedSchedules_Click(object sender, EventArgs e)
        {
            try
            {
                dateFrom = dtpFrom.Value.Date;
                dateTo = dtpTo.Value.AddDays(1).Date;

                WaitForm waitForm = new WaitForm(LoadRecSchReportData);
                waitForm.ShowDialog();


                DetailReportBand compBand = recschrep.Bands["DetailReport"] as DetailReportBand;
                DetailReportBand dataBand = recschrep.Bands["DetailReport1"] as DetailReportBand;
                DetailReportBand summaryBand = recschrep.Bands["DetailReport2"] as DetailReportBand;

                compBand.DataSource = reccomprvmDatasource;
                dataBand.DataSource = recschrvmDatasource;
                summaryBand.DataSource = recschSummaryDatasource;

                recschrep.ShowPrintMarginsWarning = false;
                recschrep.ShowPrintStatusDialog = false;

                recschrep.ShowPreview();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void LoadRecSchReportData()
        {
            try
            {
                recschrep = new RecSchReport();
                if(recschSummaryDatasource != null)
                {
                    recschSummaryDatasource.Clear();
                    recschSummaryDatasource = null;
                }
                recschSummaryDatasource = new List<RecSchSummaryRVM>();

                if (recschrvmDatasource != null)
                {
                    recschrvmDatasource.Clear();
                    recschrvmDatasource = null;
                }
                recschrvmDatasource = new List<RecSchRVM>();

                if (reccomprvmDatasource != null)
                {
                    reccomprvmDatasource.Clear();
                    reccomprvmDatasource = null;
                }
                reccomprvmDatasource = new List<RecCompRVM>();

                using (Context db = new Context())
                {
                    var schs = db.DealSchedules.Where(a => a.IsArrived && a.Status == Model.Deal.Common.ScheduleStatus.Arrived && a.ArrivalDate.Value > dateFrom && a.ArrivalDate.Value < dateTo).ToList();
                    foreach (var sch in schs)
                    {
                        sch.GoodCompany = db.GoodCompanies.Find(sch.GoodCompanyId);
                        sch.AppDeal = db.AppDeals.Find(sch.AppDealId);
                        sch.AppDeal.Company = db.Companies.Find(sch.AppDeal.CompanyId);
                        sch.AppDeal.Broker = db.Brokers.Find(sch.AppDeal.BrokerId);
                        sch.Employee = db.Employees.Find(sch.EmployeeId);
                        sch.ScheduleWeighBridges = db.ScheduleWeighBridges.Where(a => a.DealScheduleId == sch.Id).ToList();
                        foreach (var item in sch.ScheduleWeighBridges)
                        {
                            item.WeighBridge = db.WeighBridges.Find(item.WeighBridgeId);
                        }
                        sch.Driver = db.Drivers.Find(sch.DriverId);
                        sch.Vehicle = db.Vehicles.Find(sch.VehicleId);
                        RecSchRVM vm = new RecSchRVM
                        {
                            LoadedPricePaidAmount = sch.LoadedPrice,
                            Broker = sch.AppDeal.Broker.Name,
                            ConttonFactory =
                            sch.AppDeal.Company.Name,
                            DealScheduleNo = string.Format("{0}/{1}", sch.AppDealId, sch.Id),                            
                            Driver = sch.Driver.Name,
                            GoodsCompany = sch.GoodCompany.Name,
                            LoadedMonds = sch.LoadedTradeUnits,
                            LoadedWeight = sch.LoadedSubTradeUnits,
                            LossInCash = sch.DiffLoadedPrice,
                            LoadingWeighBridge = "N/A",
                            LossInMonds = sch.DiffLoadedTradeUnits,
                            LossInWeight = sch.DiffLoadedSubTradeUnits,
                            NoOfBori = (int)sch.LoadedPackingsUnits,
                            PerMondPriceRate = sch.AppDeal.TradeUnitPrice,
                            ReceingWeightBridge = "N/A",
                            ReceivedMonds = sch.ReceivedTradeUnits,
                            ReceivedWeight = sch.ReceivedSubTradeUnits,
                            SchDispatchDate = sch.DispatchedDate.Value.ToShortDateString(),
                            SchMonds = sch.ScheduledTradeUnits,
                            SchReceivedDate = sch.ArrivalDate.Value.ToShortDateString(),
                            SchWeight = sch.ScheduledSubTradeUnits,
                            Selector = sch.Employee.Name,
                            VehicleFare = sch.FareActualPaid,
                            VehicleNO = sch.Vehicle.No
                        };
                        
                        var loadingWeighBridge = sch.ScheduleWeighBridges.FirstOrDefault(a => a.Type == ScheduleWeighBridgeType.Load);
                        var recWeighBridge = sch.ScheduleWeighBridges.FirstOrDefault(a => a.Type == ScheduleWeighBridgeType.Receive);
                        if(loadingWeighBridge != null && loadingWeighBridge.WeighBridge != null)
                        {
                            vm.LoadingWeighBridge = loadingWeighBridge.WeighBridge.Name;
                        }
                        if(recWeighBridge != null && recWeighBridge.WeighBridge != null)
                        {
                            vm.ReceingWeightBridge = recWeighBridge.WeighBridge.Name;
                        }

                        recschrvmDatasource.Add(vm);
                    }

                    var appSett = db.AppSettings.First();
                    RecCompRVM recComp = new RecCompRVM
                    {
                        Address = appSett.Address,
                        Barcode = null,
                        Logo = appSett.Logo,
                        Name = appSett.Name,
                        Phone = appSett.PhoneNo,
                        Qrcode = null,
                        RepDate = DateTime.Now.ToString(),
                        RepTitle = string.Format("Schedule received data from ({0}) to ({1})", dateFrom.ToShortDateString(), dateTo.AddDays(-1).ToShortDateString())
                    };
                    reccomprvmDatasource.Add(recComp);
                }

                RecSchSummaryRVM summaryVM = new RecSchSummaryRVM
                {
                    TotalLoadedMonds = recschrvmDatasource.Sum(a => a.LoadedMonds).ToString("n1"),
                    TotalLoadedPrice = recschrvmDatasource.Sum(a => a.LoadedPricePaidAmount).ToString("n1"),
                    TotalLoadedWeight = recschrvmDatasource.Sum(a => a.LoadedWeight).ToString("n1"),
                    TotalLossInMonds = recschrvmDatasource.Sum(a => a.LossInMonds).ToString("n1"),
                    TotalLossInPrice = recschrvmDatasource.Sum(a => a.LossInCash).ToString("n1"),
                    TotalLossInWeight = recschrvmDatasource.Sum(a => a.LossInWeight).ToString("n1"),
                    TotalReceivedMonds = recschrvmDatasource.Sum(a => a.ReceivedMonds).ToString("n1"),
                    TotalReceivedPrice = (recschrvmDatasource.Sum(a => a.LoadedPricePaidAmount) - recschrvmDatasource.Sum(a => a.LossInCash)).ToString("n1"),
                    TotalReceivedWeight = recschrvmDatasource.Sum(a => a.ReceivedWeight).ToString("n1"),
                    TotalSchMonds = recschrvmDatasource.Sum(a => a.SchMonds).ToString("n1"),
                    TotalSchPrice = recschrvmDatasource.Count.ToString(),
                    TotalSchWeight = recschrvmDatasource.Sum(a => a.SchWeight).ToString("n1")
                };
                recschSummaryDatasource.Add(summaryVM);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        #endregion

    }
}
