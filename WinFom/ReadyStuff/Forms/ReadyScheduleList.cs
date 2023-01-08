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
using Model.ReadyStuff.ViewModel;
using System.Data.Entity;
using WinFom.Common.Model;
using Zen.Barcode;
using WinFom.ReadyStuff.Report;
using DevExpress.XtraReports.UI;
using WinFom.ReadyStuff.ReportViewModel;
using Model.Admin.Model;
using Model.Financials.Model;

namespace WinFom.ReadyStuff.Forms
{
    public partial class ReadyScheduleList : Form
    {
        private ReadyDealVM readyDealVM = null;
        private List<ReadySchedule> schedulelist = null;
        private string btndgvprocess = "btndgvprocess2343";
        private string btndgvcompleted = "btndgvcomplet243";
        private string btndgvreport = "btndgvreport123414";
        private string btndgvreschedule = "btndgvreschedule1231";
        private string btndgvcancel = "btndgvcancel";
        private string appUser = SingleTon.LoginForm.appUser.Id;
        private AppSettings appSet = Helper.AppSet;
        public ReadyScheduleList(ReadyDealVM vm)
        {
            InitializeComponent();
            readyDealVM = vm;
        }
        private void LoadScheduleList()
        {
            try
            {
                if (schedulelist != null && schedulelist.Count > 0)
                {
                    schedulelist.Clear();
                }
                using (Context db = new Context())
                {
                    schedulelist = db.ReadySchedules.Where(a => a.ReadyDealId == readyDealVM.Id)
                        .ToList()
                        .Where(a => { DateTime dt = a.ScheduleDate.Date; return dt >= appSet.StartDate && dt <= appSet.EndDate; }).ToList();

                }
            }
            catch (Exception ep)
            {
                throw ep;
            }
        }
        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                tbBroker.Text = readyDealVM.Broker;
                tbDealDate.Text = readyDealVM.DealDate;
                tbDealItem.Text = readyDealVM.ReadyItem;
                tbDealNo.Text = readyDealVM.Id.ToString();
                tbVehicles.Text = readyDealVM.NoOfVehicles.ToString();
                WaitForm wait = new WaitForm(LoadScheduleList);
                wait.ShowDialog();

                Gujjar.AddDatagridviewButton(dgv, btndgvprocess, "Process", "Process", 80);
                Gujjar.AddDatagridviewButton(dgv, btndgvcompleted, "Complete", "Complete", 80);
                Gujjar.AddDatagridviewButton(dgv, btndgvreport, "Report", "Report", 80);
                Gujjar.AddDatagridviewButton(dgv, btndgvreschedule, "Re-Sch", "Re-Sch", 80);
                Gujjar.AddDatagridviewButton(dgv, btndgvcancel, "Cancel", "Cancel", 80);
                DgvUpdate();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void DgvUpdate()
        {
            schVM2BindingSource.List.Clear();
            int index = 0;
            foreach (var item in schedulelist)
            {
                SchVM2 sch = new SchVM2
                {
                    BrokerShareAmount = item.BrokerShareAmount,
                    ScheduleNo = item.ScheduleNo,
                    Bharthies = item.Bharthies.Count,
                    BrokerSharePercentage = item.BrokerSharePercentage,
                    NetScheduleAmount = item.NetScheduleAmount,
                    GrossPrice = item.GrossPrice,
                    IsCompleted = item.IsCompleted,
                    NoOfVehicles = item.NoOfVehicles,
                    ScheduleDate = item.ScheduleDate.ToShortDateString(),
                    PerTradeUnitPrice = item.PerTradeUnitPrice,
                    ReadyDealId = item.ReadyDealId,
                    WeighBridge = "N/A",
                    DispatchedDate = "N/A",
                    Driver = "N/A",
                    ReadyDate = item.ReadyDate.ToShortDateString(),
                    ScheduleWeight = item.ScheduleWeight,
                    TotalTradeUnits = item.TotalTradeUnits,
                    TradeUnit = "N/A",
                    WeighBridgeWeight = item.WeighBridgeWeight,
                    Id = item.Id,
                    ScheduleStatus = item.ScheduleType.ToString(),
                    Selector = "N/A",
                    VehicleEmptyWeight = item.EmptyVehicleWeight
                };
                using (Context db = new Context())
                {
                    if (item.WeighBridgeId != null)
                    {
                        item.WeighBridge = db.WeighBridges.Find(item.WeighBridgeId);
                        sch.WeighBridge = item.WeighBridge.Name;
                    }
                    if (item.IsCompleted)
                    {
                        sch.DispatchedDate = item.DispatchedDate.Value.ToShortDateString();
                    }
                    //if(item.ReadyTradeUnitId != null)
                    //{
                    //    item.TradeUnit = db.ReadyTradeUnits.Find(item.ReadyTradeUnitId);
                    //    sch.TradeUnit = string.Format("{0}-({1})", item.TradeUnit.Title, item.TradeUnit.UnitQty);
                    //}
                    if (item.ReadyDriverId != null)
                    {
                        item.Driver = db.ReadyDrivers.Find(item.ReadyDriverId);
                        sch.Driver = item.Driver.Name;
                    }
                    if (item.DispatchedDate != null)
                    {
                        sch.DispatchedDate = item.DispatchedDate.Value.ToShortDateString();
                    }
                    if (item.ReadySelectorId != null)
                    {
                        item.Selector = db.ReadySelectors.Find(item.ReadySelectorId);
                        sch.Selector = item.Selector.Name;
                    }
                }

                schVM2BindingSource.List.Add(sch);

                if (item.ScheduleType == ReadyScheduleType.Cancelled)
                {
                    dgv.Rows[index].DefaultCellStyle.BackColor = Color.Pink;
                }
                if (item.ScheduleType == ReadyScheduleType.Completed)
                {
                    dgv.Rows[index].DefaultCellStyle.BackColor = Color.PaleGreen;
                }
                index++;
            }
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
                #region
                if (dgv.Columns[btndgvprocess].Index == ci)
                {
                    SchProcessTransfer scht = new SchProcessTransfer
                    {
                        Broker = tbBroker.Text,
                        DealItem = tbDealItem.Text,
                        Vehicles = tbVehicles.Text.ToInt(),

                    };

                    int id = dgv.Rows[ri].Cells[0].Value.ToInt();
                    SchVM2 obj = schVM2BindingSource.List.OfType<SchVM2>().FirstOrDefault(a => a.Id == id);
                    if (obj.ScheduleStatus == ReadyScheduleType.Cancelled.ToString())
                    {
                        throw new Exception("Schedule is cancelled");
                    }
                    if (!(obj.ScheduleStatus == ReadyScheduleType.Scheduled.ToString()))
                    {
                        throw new Exception("Only scheduled are allowed");
                    }
                    scht.ReadyDate = obj.ReadyDate;
                    scht.ScheduleNo = obj.ScheduleNo;
                    scht.ScheduleId = id;
                    ScheduleProcessForm form = new ScheduleProcessForm(scht);
                    form.ShowDialog();

                    if (form.IsDone)
                    {
                        Helper.UpdateReadyDealStatus(id);
                        WaitForm wait2 = new WaitForm(LoadScheduleList);
                        wait2.ShowDialog();

                        DgvUpdate();
                    }
                }

                if (dgv.Columns[btndgvcompleted].Index == ci)
                {
                    int id = dgv.Rows[ri].Cells[0].Value.ToInt();
                    var obj = schVM2BindingSource.List.OfType<SchVM2>().First(a => a.Id == id);
                    if (!(obj.ScheduleStatus == ReadyScheduleType.Dispatched.ToString()))
                    {
                        throw new Exception("Only dispatched schedules are allowed");
                    }
                    SchCompTransfer sch = new SchCompTransfer
                    {
                        Broker = tbBroker.Text,
                        DealItem = tbDealItem.Text,
                        Driver = obj.Driver,
                        ReadyDate = obj.ReadyDate,
                        ScheduleNo = obj.ScheduleNo,
                        VehicleEmpltyWeight = obj.VehicleEmptyWeight.ToString()
                    };

                    ReadySchedule readySchedule = null;
                    using (Context db = new Context())
                    {
                        readySchedule = db.ReadySchedules.Find(id);
                    }
                    sch.VehicleNo = readySchedule.VehicleNo;

                    ScheduleCompleteForm form = new ScheduleCompleteForm(sch, readySchedule);
                    form.ShowDialog();

                    if (form.IsDone)
                    {
                        Helper.UpdateReadyDealStatus(id);
                        WaitForm wait2 = new WaitForm(LoadScheduleList);
                        wait2.ShowDialog();

                        DgvUpdate();
                    }
                }

                #endregion
                #region Report
                if (dgv.Columns[btndgvreport].Index == ci)
                {
                    int schid = dgv.Rows[ri].Cells[0].Value.ToString().ToInt();
                    try
                    {
                        var obj = schVM2BindingSource.List.OfType<SchVM2>().First(a => a.Id == schid);

                        if (obj.ScheduleStatus == ReadyScheduleType.Cancelled.ToString())
                        {
                            throw new Exception("Schedule is cancelled");
                        }

                        if (!(obj.ScheduleStatus == ReadyScheduleType.Completed.ToString()))
                        {
                            throw new Exception("Only completed schedules are allowed");
                        }
                        List<PackingRVN> packingList = new List<PackingRVN>();
                        List<TradeBroker> tradeBrokerList = new List<TradeBroker>();
                        using (Context db = new Context())
                        {
                            var sett = db.AppSettings.First();
                            ReadySchedule schedule = db.ReadySchedules.Find(schid);
                            schedule.WeighBridge = db.WeighBridges.Find(schedule.WeighBridgeId);
                            ReadyDeal deal = db.ReadyDeals.Find(schedule.ReadyDealId);
                            deal.TradeUnit = db.ReadyTradeUnits.Find(deal.ReadyTradeUnitId);
                            ReadyBroker broker = db.ReadyBrokers.Find(deal.ReadyBrokerId);
                            ReadyItem readyItem = db.ReadyItems.Find(deal.ReadyItemId);
                            var packings = db.Bharthies.Where(a => a.ReadyScheduleId == schedule.Id).ToList();
                            List<TopHeader> topHeaderList = new List<TopHeader>();
                            TopHeader header = new TopHeader
                            {
                                CompanyAddress = sett.Address,
                                CompanyName = sett.Name,
                                CompanyPhone = sett.PhoneNo,
                                Logo = sett.Logo,
                                Dated = DateTime.Now.ToString(),
                                Item = readyItem.Title
                            };


                            TradeBroker tradeBroker = new TradeBroker
                            {
                                BrokerShareAmount = schedule.BrokerShareAmount,
                                BrokerSharePercentage = schedule.BrokerSharePercentage,
                                NetPrice = schedule.NetScheduleAmount,
                                PerTradeUnitPrice = schedule.PerTradeUnitPrice,
                                TotalTradeUnits = schedule.TotalTradeUnits,
                                TradeUnitName = deal.TradeUnit.Title,
                                TradeUnitQty = deal.TradeUnit.UnitQty.ToString(),
                                WeighBridge = schedule.WeighBridge.Name,
                                WeighBridgeWeight = schedule.WeighBridgeWeight,
                                VehicleEmptyWeight = schedule.EmptyVehicleWeight.ToString("n4"),
                                VehicleFullWeight = schedule.FullVehicleWeight.ToString("n4"),
                                VehicleNo = schedule.VehicleNo
                            };
                            tradeBrokerList.Add(tradeBroker);

                            foreach (var item in packings)
                            {
                                item.BharthiType = db.BharthiTypes.Find(item.BharthiTypeId);

                                PackingRVN pack = new PackingRVN
                                {
                                    Packing = string.Format("{0}-({1})", item.BharthiType.Title, item.BharthiType.UnitQty.ToString("n1")),
                                    Qty = item.Qty,
                                    Total = item.Total
                                };
                                packingList.Add(pack);
                            }

                            header.Broker = broker.Name;
                            byte[] driverPic = null;
                            byte[] driverThumb = null;
                            byte[] selectorPic = null;
                            byte[] selectorThum = null;
                            string driverName = "N/A";
                            string selectorName = "N/A";
                            string driverCnic = "";
                            string selectorCnic = "";
                            string dealSchedule = string.Format("Deal No. {0}, Schedule No. {1}", deal.Id, schedule.ScheduleNo);
                            if (schedule.ReadyDriverId != null)
                            {
                                var drv = db.ReadyDrivers.Find(schedule.ReadyDriverId);
                                if (drv.PicData != null)
                                {
                                    driverPic = drv.PicData;
                                }
                                if (drv.ThumbPicData != null)
                                {
                                    driverThumb = drv.ThumbPicData;
                                }
                                driverName = drv.Name;
                                driverCnic = drv.CNIC;
                            }
                            if (schedule.ReadySelectorId != null)
                            {
                                var slc = db.ReadySelectors.Find(schedule.ReadySelectorId);
                                if (slc.PicData != null)
                                {
                                    selectorPic = slc.PicData;
                                }
                                if (slc.ThumbPicData != null)
                                {
                                    selectorThum = slc.ThumbPicData;
                                }
                                selectorName = slc.Name;
                                selectorCnic = slc.CNIC;
                            }
                            Image picImg = Properties.Resources.atomix_user31;
                            Image thumbImg = Properties.Resources.fingerprint_reader;
                            if (driverPic == null)
                            {
                                driverPic = Gujjar.GetByteArrayFromImage(picImg);
                            }
                            if (driverThumb == null)
                            {
                                driverThumb = Gujjar.GetByteArrayFromImage(thumbImg);
                            }
                            if (selectorPic == null)
                            {
                                selectorPic = Gujjar.GetByteArrayFromImage(picImg);
                            }
                            if (selectorThum == null)
                            {
                                selectorThum = Gujjar.GetByteArrayFromImage(thumbImg);
                            }
                            Persons persons = new Persons
                            {
                                Dated = schedule.DispatchedDate.ToString(),
                                DealSchedule = dealSchedule,
                                DriverName = driverName,
                                DriverPic = driverPic,
                                DriverThumb = driverThumb,
                                SelectorName = selectorName,
                                SelectorPic = selectorPic,
                                SelectorThumb = selectorThum,
                                DriverCNIC = driverCnic,
                                SelectorCNIC = selectorCnic
                            };

                            List<Persons> personsList = new List<Persons>();
                            personsList.Add(persons);

                            Code128BarcodeDraw draw = BarcodeDrawFactory.Code128WithChecksum;
                            var image1 = draw.Draw(Helper.Unum, 50);
                            header.Barcode = Gujjar.GetByteArrayFromImage(image1);

                            CodeQrBarcodeDraw draw2 = BarcodeDrawFactory.CodeQr;
                            var image2 = draw2.Draw("GBS Solutions, Burewala. 0333-9372084", 50);
                            header.QrCode = Gujjar.GetByteArrayFromImage(image2);

                            topHeaderList.Add(header);
                            SCSReport rep = new SCSReport();

                            DetailReportBand topBand = rep.Bands["DetailReport"] as DetailReportBand;
                            topBand.DataSource = topHeaderList;
                            DetailReportBand pBand = rep.Bands["DetailReport1"] as DetailReportBand;
                            pBand.DataSource = personsList;
                            DetailReportBand packBand = rep.Bands["DetailReport2"] as DetailReportBand;
                            packBand.DataSource = packingList;
                            DetailReportBand bBand = rep.Bands["DetailReport3"] as DetailReportBand;
                            bBand.DataSource = tradeBrokerList;

                            var param = rep.Parameters["appUser"];
                            param.Visible = false;
                            param.Value = appUser;

                            var lengendsParam = rep.Parameters["Legends"];
                            lengendsParam.Visible = false;
                            lengendsParam.Value = Helper.Legends();

                            rep.ShowPrintMarginsWarning = false;
                            rep.ShowPrintStatusDialog = false;

                            rep.ShowRibbonPreview();
                            //Gujjar.InfoMsg("Report has been saved/printed");
                        }
                    }
                    catch (Exception exp)
                    {
                        Gujjar.ErrMsg(exp);
                    }
                }
                #endregion
                #region Re-schedule
                if (dgv.Columns[btndgvreschedule].Index == ci)
                {
                    try
                    {
                        int sid = dgv.Rows[ri].Cells[0].Value.ToInt();
                        var sObj = schVM2BindingSource.List.OfType<SchVM2>()
                            .FirstOrDefault(a => a.Id == sid);
                        if (sObj.ScheduleStatus == ReadyScheduleType.Cancelled.ToString())
                        {
                            throw new Exception("Schedule is cancelled");
                        }
                        if (sObj.ScheduleStatus == ReadyScheduleType.Scheduled.ToString())
                        {
                            throw new Exception("Already scheduled");
                        }

                        if (!Helper.ConfirmAdminPassword())
                        {
                            return;
                        }
                        DialogResult rest = Gujjar.ConfirmYesNo("Please confirm!!.. Are you sured to re-schedule. All concerned financial entries will be reversed");
                        if (rest == DialogResult.No)
                            return;

                        List<DayBook> daybooks = null;
                        using (Context db = new Context())
                        {
                            daybooks = db.DayBooks.Where(a => a.ReadyScheduleId == sid && !a.IsReversed).ToList();

                            var sch = db.ReadySchedules.Find(sid);
                            sch.IsCompleted = false;
                            sch.ScheduleType = ReadyScheduleType.Scheduled;
                            sch.DispatchedDate = null;
                            sch.NoOfVehicles = 1;
                            sch.EmptyVehicleWeight = 0;
                            sch.LaborExpensesDescription = "N/A";
                            sch.ScheduleWeight = 0;
                            sch.TotalPackings = 0;
                            sch.BardanaAmountExpense = 0;
                            sch.LaborExpenses = 0;

                            sch.VehicleNo = "N/A";
                            sch.WeighBridgeWeight = 0;
                            sch.WeighBridgeId = null;
                            sch.GrossPrice = 0;
                            sch.BrokerSharePercentage = 0;
                            sch.BrokerShareAmount = 0;
                            sch.FullVehicleWeight = 0;
                            sch.NetScheduleAmount = 0;

                            db.Entry(sch).State = EntityState.Modified;
                            db.SaveChanges();

                            if (daybooks != null && daybooks.Count > 0)
                            {
                                foreach (var item in daybooks)
                                {
                                    Helper.ReverseTransaction(item);
                                }
                            }

                            Gujjar.InfoMsg("Schedule is re-scheduled successfully");

                            sObj.IsCompleted = false;
                            sObj.ScheduleStatus = ReadyScheduleType.Scheduled.ToString();
                            dgv.Refresh();

                            var deal = db.ReadyDeals.Find(sch.ReadyDealId);
                            deal.IsCompleted = false;
                            db.Entry(deal).State = EntityState.Modified;
                            db.SaveChanges();

                            Helper.UpdateReadyDealStatus(deal.Id);

                            WaitForm wait2 = new WaitForm(LoadScheduleList);
                            wait2.ShowDialog();
                            DgvUpdate();
                        }
                    }
                    catch (Exception exp2)
                    {
                        throw exp2;
                    }
                }
                #endregion
                #region Cancel
                if (dgv.Columns[btndgvcancel].Index == ci)
                {
                    try
                    {
                        int sid = dgv.Rows[ri].Cells[0].Value.ToInt();
                        var sObj = schVM2BindingSource.List.OfType<SchVM2>()
                            .FirstOrDefault(a => a.Id == sid);
                        if (sObj.ScheduleStatus == ReadyScheduleType.Cancelled.ToString())
                        {
                            throw new Exception("Schedule is cancelled");
                        }
                        if(sObj.ScheduleStatus != ReadyScheduleType.Scheduled.ToString())
                        {
                            throw new Exception("Only schedules can be cancelled");
                        }
                        if (!Helper.ConfirmAdminPassword())
                        {
                            return;
                        }
                        DialogResult rest = Gujjar.ConfirmYesNo("Please confirm!!.. Are you sured to cancel this schedule. All concerned financial entries will be reversed");
                        if (rest == DialogResult.No)
                            return;

                        List<DayBook> daybooks = null;
                        using (Context db = new Context())
                        {
                            daybooks = db.DayBooks.Where(a => a.ReadyScheduleId == sid && !a.IsReversed).ToList();

                            var sch = db.ReadySchedules.Find(sid);
                            sch.IsCompleted = false;
                            sch.ScheduleType = ReadyScheduleType.Cancelled;
                            sch.DispatchedDate = null;
                            sch.NoOfVehicles = 1;
                            sch.EmptyVehicleWeight = 0;
                            sch.LaborExpensesDescription = "N/A";
                            sch.ScheduleWeight = 0;
                            sch.TotalPackings = 0;
                            sch.BardanaAmountExpense = 0;
                            sch.LaborExpenses = 0;
                            sch.VehicleNo = "N/A";
                            sch.WeighBridgeWeight = 0;
                            sch.WeighBridgeId = null;
                            sch.GrossPrice = 0;
                            sch.BrokerSharePercentage = 0;
                            sch.BrokerShareAmount = 0;
                            sch.FullVehicleWeight = 0;
                            sch.NetScheduleAmount = 0;
                            db.Entry(sch).State = EntityState.Modified;
                            db.SaveChanges();

                            if (daybooks != null && daybooks.Count > 0)
                            {
                                foreach (var item in daybooks)
                                {
                                    Helper.ReverseTransaction(item);
                                    item.ReadyScheduleId = null;
                                    item.IsReversed = true;
                                    db.Entry(item).State = EntityState.Modified;
                                }
                            }
                            db.SaveChanges();
                            Gujjar.InfoMsg("Schedule is cancelled successfully");
                            sObj.IsCompleted = false;
                            sObj.ScheduleStatus = ReadyScheduleType.Cancelled.ToString();
                            dgv.Refresh();
                            var deal = db.ReadyDeals.Find(sch.ReadyDealId);
                            deal.IsCompleted = false;
                            db.Entry(deal).State = EntityState.Modified;
                            db.SaveChanges();
                            Helper.UpdateReadyDealStatus(deal.Id);
                            WaitForm wait2 = new WaitForm(LoadScheduleList);
                            wait2.ShowDialog();
                            DgvUpdate();
                        }
                    }
                    catch (Exception eabc)
                    {
                        throw eabc;
                    }
                }
                #endregion
            }

            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void ShowReport()
        {

        }
    }
}
