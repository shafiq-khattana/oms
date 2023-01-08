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
using WinFom.ReadyStuff.Forms;
using WinFom.OilDirtStuff.ViewModel;
using Model.OilDirtStuff.Model;
using Model.OilDirtStuff.ViewModel;
using Model.Financials.Model;
using WinFom.OilDirtStuff.Reports.RepModel;
using WinFom.OilDirtStuff.Reports.XtraReports;

namespace WinFom.OilDirtStuff.Forms
{
    public partial class OilDirtScheduleList : Form
    {
        private OilDirtDealVM oilDirtDealVM = null;
        private List<OilDirtSchedule> schedulelist = null;
        private string btndgvprocess = "btndgvprocess2343";
        private string btndgvcompleted = "btndgvcomplet243";
        private string btndgvreport = "btndgvreport123414";
        private string btndgvreschedule = "btndgvreschedule1231";
        private string btndgvcancel1231 = "btndgvcancel1231";
        private string appUser = SingleTon.LoginForm.appUser.Id;
        private AppSettings appSet = Helper.AppSet;
        public bool IsDone = false;
        public OilDirtScheduleList(OilDirtDealVM vm)
        {
            InitializeComponent();
            oilDirtDealVM = vm;
        }
        private void LoadScheduleList()
        {
            try
            {
                if(schedulelist != null && schedulelist.Count > 0)
                {
                    schedulelist.Clear();
                }
                using (Context db = new Context())
                {
                    schedulelist = db.OilDirtSchedules.Where(a => a.OilDirtDealId == oilDirtDealVM.Id)
                        .ToList()
                        .Where(a => { DateTime dt = a.ScheduleDate.Date; return dt >= appSet.StartDate && dt <= appSet.EndDate; }).ToList();

                }
            }
            catch (Exception ep)
            {
                Gujjar.ErrMsg(ep);
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
                tbBroker.Text = oilDirtDealVM.Broker;
                tbDealDate.Text = oilDirtDealVM.DealDate;
                tbDealItem.Text = oilDirtDealVM.Item;
                tbDealNo.Text = oilDirtDealVM.Id.ToString();
                tbVehicles.Text = oilDirtDealVM.NoOfVehicles.ToString();
                WaitForm wait = new WaitForm(LoadScheduleList);
                wait.ShowDialog();
                Helper.IsOkApplied();
                Gujjar.AddDatagridviewButton(dgv, btndgvprocess, "Process", "Process", 80);
                Gujjar.AddDatagridviewButton(dgv, btndgvcompleted, "Complete", "Complete", 80);
                Gujjar.AddDatagridviewButton(dgv, btndgvreport, "Report", "Report", 80);
                Gujjar.AddDatagridviewButton(dgv, btndgvreschedule, "Re-sch", "Re-sch", 80);
                Gujjar.AddDatagridviewButton(dgv, btndgvcancel1231, "Cancel", "Cancel", 80);
                DgvUpdate();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void DgvUpdate()
        {
            oilDirtScheduleVMBindingSource.List.Clear();
            int index = 0;
            foreach (var item in schedulelist)
            {
                OilDirtScheduleVM sch = new OilDirtScheduleVM
                {
                    BrokerShareAmount = item.BrokerShareAmount,
                    ScheduleNo = item.ScheduleNo,
                    BrokerSharePercentage = item.BrokerSharePercentage,
                    ScheduleDate = item.ScheduleDate.ToShortDateString(),
                    PerTradeUnitPrice = item.PerTradeUnitPrice.ToString("n4"),
                    WeighBridge = "N/A",
                    Driver = "N/A",
                    ReadyDate = item.ReadyDate.ToShortDateString(),
                    WeighBridgeWeight = item.WeighBridgeWeight,
                    Id = item.Id,
                    Selector = "N/A",
                    TotalActualAmount = item.TotalActualAmount,
                    Status = item.Status.ToString(),
                    TotalExpectedAmount = item.TotalExpectedAmount,
                    TotalNetAmount = item.TotalNetAmount,
                    VehicleNo = item.VehicleNo,
                    VehicleWeightEmpty = item.VehicleWeightEmpty,
                    VehicleWeightFull = item.VehicleWeightFull,
                    CompleteDate = "N/A"
                };
                using (Context db = new Context())
                {
                    if (item.WeighBridgeId != null)
                    {
                        item.WeighBridge = db.WeighBridges.Find(item.WeighBridgeId);
                        sch.WeighBridge = item.WeighBridge.Name;
                    }

                    
                    if (item.OilDirtDriverId != null)
                    {
                        item.Driver = db.OilDirtDrivers.Find(item.OilDirtDriverId);
                        sch.Driver = item.Driver.Name;
                    }
                    if (item.CompleteDate != null)
                    {
                        sch.CompleteDate = item.CompleteDate.Value.ToShortDateString();
                    }
                    if (item.OilDirtSelectorId != null)
                    {
                        item.Selector = db.OilDirtSelectors.Find(item.OilDirtSelectorId);
                        sch.Selector = item.Selector.Name;
                    }
                }

                oilDirtScheduleVMBindingSource.List.Add(sch);
                if(item.Status == OilDirtScheduleStatus.Cancelled)
                {
                    dgv.Rows[index].DefaultCellStyle.BackColor = Color.Pink;
                }
                if(item.Status == OilDirtScheduleStatus.Completed)
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
                #region Cancel
                if (dgv.Columns[btndgvcancel1231].Index == ci)
                {
                    try
                    {
                        int schid = dgv.Rows[ri].Cells[0].Value.ToInt();
                        var sObj = oilDirtScheduleVMBindingSource.List.OfType<OilDirtScheduleVM>()
                            .FirstOrDefault(a => a.Id == schid);

                        if (sObj.Status == OilDirtScheduleStatus.Cancelled.ToString())
                        {
                            throw new Exception("Scheduled is cancelled");
                        }
                        if(sObj.Status != OilDirtScheduleStatus.Scheduled.ToString())
                        {
                            throw new Exception("Only schedules are allowed to be cancelled");
                        }
                        if (!Helper.ConfirmAdminPassword())
                        {
                            return;
                        }

                        DialogResult res = Gujjar.ConfirmYesNo("Are you sure to cancel the deal");
                        if (res == DialogResult.No)
                            return;

                        using (Context db = new Context())
                        {
                            List<DayBook> daybooks = db.DayBooks.Where(a => a.OilDirtScheduleId == schid)
                                .ToList();

                            var schObj = db.OilDirtSchedules.Find(schid);
                            schObj.CompleteDate = null;
                            schObj.BrokerShareAmount = 0;
                            schObj.LoadedQty = 0;
                            schObj.OilDirtDriverId = null;
                            schObj.OilDirtSelectorId = null;                           
                            schObj.Status = OilDirtScheduleStatus.Cancelled;
                            db.Entry(schObj).State = EntityState.Modified;
                            db.SaveChanges();

                            foreach (var item in daybooks.Where(a => !a.IsReversed))
                            {
                                Helper.ReverseTransaction(item);
                            }
                            foreach (var item in daybooks)
                            {
                                item.OilDirtScheduleId = null;
                                item.IsReversed = true;
                                db.Entry(item).State = EntityState.Modified;
                            }
                            db.SaveChanges();
                            Gujjar.InfoMsg("Schedule is cancelled successfully");

                            WaitForm wait = new WaitForm(LoadScheduleList);
                            wait.ShowDialog();

                            DgvUpdate();
                            Helper.UpdateOilDirtDealStatus(schObj.Id);
                            IsDone = true;
                        }
                    }
                    catch (Exception exp)
                    {
                        Gujjar.ErrMsg(exp);
                    }
                }
                #endregion
                #region Reschedule
                if (dgv.Columns[btndgvreschedule].Index == ci)
                {
                    try
                    {
                        int schid = dgv.Rows[ri].Cells[0].Value.ToInt();
                        var sObj = oilDirtScheduleVMBindingSource.List.OfType<OilDirtScheduleVM>()
                            .FirstOrDefault(a => a.Id == schid);

                        if (sObj.Status == OilDirtScheduleStatus.Cancelled.ToString())
                        {
                            throw new Exception("Scheduled is cancelled");
                        }

                        if (sObj.Status == OilDirtStatus.Scheduled.ToString())
                        {
                            throw new Exception("Already scheduled");
                        }

                        if(!Helper.ConfirmAdminPassword())
                        {
                            return;
                        }

                        DialogResult res = Gujjar.ConfirmYesNo("Are you sure to re-schedule deal");
                        if (res == DialogResult.No)
                            return;

                        using (Context db = new Context())
                        {
                            List<DayBook> daybooks = db.DayBooks.Where(a => !a.IsReversed && a.OilDirtScheduleId == schid)
                                .ToList();

                            var schObj = db.OilDirtSchedules.Find(schid);
                            schObj.CompleteDate = null;
                            schObj.Status = OilDirtScheduleStatus.Scheduled;
                            db.Entry(schObj).State = EntityState.Modified;
                            db.SaveChanges();

                            foreach (var item in daybooks)
                            {
                                Helper.ReverseTransaction(item);
                            }

                            Gujjar.InfoMsg("Schedule is re-scheduled successfully");

                            WaitForm wait = new WaitForm(LoadScheduleList);
                            wait.ShowDialog();

                            DgvUpdate();
                        }
                    }
                    catch (Exception exp)
                    {
                        Gujjar.ErrMsg(exp);
                    }
                }
                #endregion
                #region Process
                if (dgv.Columns[btndgvprocess].Index == ci)
                {                    
                    int id = dgv.Rows[ri].Cells[0].Value.ToInt();
                    OilDirtScheduleVM obj = oilDirtScheduleVMBindingSource.List.OfType<OilDirtScheduleVM>().FirstOrDefault(a => a.Id == id);

                    if (obj.Status == OilDirtScheduleStatus.Cancelled.ToString())
                    {
                        throw new Exception("Scheduled is cancelled");
                    }

                    if (!(obj.Status == OilDirtScheduleStatus.Scheduled.ToString()))
                    {
                        throw new Exception("Only scheduled are allowed");
                    }
                    OilDirtSchTransfer scht = new OilDirtSchTransfer
                    {
                        Broker = tbBroker.Text,
                        Item = tbDealItem.Text,
                        DealDate = obj.ScheduleDate,
                        DealSchedule = string.Format("Deal No. {0}, Sch No. {1}", tbDealNo.Text, obj.ScheduleNo),
                        ReadyDate = obj.ReadyDate,
                        ScheduleId = obj.Id
                    };
                    OilDirtScheduleProcessForm form = new OilDirtScheduleProcessForm(scht);
                    form.ShowDialog();

                    if (form.IsDone)
                    {
                        //Helper.UpdateOilDirtDealStatus(id);
                        WaitForm wait2 = new WaitForm(LoadScheduleList);
                        wait2.ShowDialog();
                        DgvUpdate();
                    }
                }
                #endregion
                #region Complete
                if (dgv.Columns[btndgvcompleted].Index == ci)
                {
                    int id = dgv.Rows[ri].Cells[0].Value.ToInt();
                    var obj = oilDirtScheduleVMBindingSource.List.OfType<OilDirtScheduleVM>().First(a => a.Id == id);

                    if (obj.Status == OilDirtScheduleStatus.Cancelled.ToString())
                    {
                        throw new Exception("Scheduled is cancelled");
                    }

                    if (!(obj.Status == OilDirtScheduleStatus.Departed.ToString()))
                    {
                        throw new Exception("Only dispatched/departed schedules are allowed");
                    }
                    OilDirtSchCompTransfer sch = new OilDirtSchCompTransfer
                    {
                        Broker = tbBroker.Text,
                        Item = tbDealItem.Text,
                        Driver = obj.Driver,
                        ReadyDate = obj.ReadyDate,
                        ScheduleNo = obj.ScheduleNo,
                        VehicleNo = obj.VehicleNo
                    };

                    OilDirtSchedule readySchedule = null;
                    using (Context db = new Context())
                    {
                        readySchedule = db.OilDirtSchedules.Find(id);
                    }
                    
                    OilDirtScheduleCompleteForm form = new OilDirtScheduleCompleteForm(sch, readySchedule);
                    form.ShowDialog();

                    if (form.IsDone)
                    {
                        Helper.UpdateOilDirtDealStatus(id);
                        WaitForm wait2 = new WaitForm(LoadScheduleList);
                        wait2.ShowDialog();

                        DgvUpdate();
                        IsDone = true;
                    }
                }
                #endregion
                #region Report
                if (dgv.Columns[btndgvreport].Index == ci)
                {
                    try
                    {
                        int schid = dgv.Rows[ri].Cells[0].Value.ToInt();
                        var sObj = oilDirtScheduleVMBindingSource.List.OfType<OilDirtScheduleVM>()
                            .FirstOrDefault(a => a.Id == schid);

                        if(sObj.Status != ReadyScheduleType.Completed.ToString())
                        {
                            throw new Exception("Schedule is not completed");
                        }
                        using (Context db = new Context())
                        {
                            OilDirtSchedule schedule = db.OilDirtSchedules.Find(schid);
                            List<OilDirtCompRVM> compList = new List<OilDirtCompRVM>();
                            List<OilDirtSaleRVM> saleList = new List<OilDirtSaleRVM>();

                            OilDirtCompRVM compVM = new OilDirtCompRVM
                            {
                                Address = string.Format("{0}, phone {1}", appSet.Address, appSet.PhoneNo),
                                Barcode = null,
                                Logo = appSet.Logo,
                                Name = appSet.Name,
                                Phone = appSet.PhoneNo,
                                Qrcode = null,
                                RepDate = DateTime.Now.ToShortTimeString(),
                                RepTitle = string.Format("Oil dirt sale report dated {0}", schedule.CompleteDate.Value.ToShortDateString())
                            };

                            compList.Add(compVM);
                            OilDirtDeal oilDirtDeal = db.OilDirtDeals.Find(schedule.OilDirtDealId);
                            OilDirtBroker broker = db.OilDirtBrokers.Find(oilDirtDeal.OilDirtBrokerId);
                            OilDirtSelector selector = db.OilDirtSelectors.Find(schedule.OilDirtSelectorId);
                            OilDirtDriver driver = db.OilDirtDrivers.Find(schedule.OilDirtDriverId);
                            OilDirtTradeUnit tradeUnit = db.OilDirtTradeUnits.Find(oilDirtDeal.OilDirtTradeUnitId);
                            WeighBridge weighBridge = db.WeighBridges.Find(schedule.WeighBridgeId);

                            OilDirtSaleRVM saleVM = new OilDirtSaleRVM
                            {
                                BrokerShareAmount = schedule.BrokerShareAmount.ToString("n2"),
                                Broker = broker.Name,
                                TotalReceivableAmount = schedule.TotalNetAmount.ToString("n2"),
                                BrokerSharePercentage = schedule.BrokerSharePercentage.ToString("n2") + "%",
                                DealSchedule = string.Format("{0}/{1}", oilDirtDeal.Id, schedule.Id),
                                Driver = driver.Name,
                                LoadedQtyKg = string.Format("{0} KG", schedule.LoadedQty.ToString("n1")),
                                PerMondRate = schedule.PerTradeUnitPrice.ToString("n1"),
                                PerMondWeight = tradeUnit.UnitQty.ToString("n3") + " KG",
                                Selector = selector.Name,
                                TotalMonds = schedule.TotalTradeUnits.ToString("n2"),
                                TotalPrice = schedule.TotalActualAmount.ToString("n2"),
                                VehicleNo = schedule.VehicleNo,
                                VehicleWeight = schedule.VehicleWeightEmpty.ToString("n2"),
                                WeighBridge = weighBridge.Name,
                                WeighBridgeWeight = schedule.WeighBridgeWeight.ToString("n2"),
                                WeightInDifference = (schedule.LoadedQty - schedule.WeighBridgeWeight).ToString("n1") + " KG",

                            };
                            saleList.Add(saleVM);

                            OilDirtSaleReport report = new OilDirtSaleReport();
                            DetailReportBand compBand = report.Bands["DetailReport"] as DetailReportBand;
                            DetailReportBand saleBand = report.Bands["DetailReport1"] as DetailReportBand;

                            compBand.DataSource = compList;
                            saleBand.DataSource = saleList;

                            report.ShowPrintMarginsWarning = false;
                            report.ShowPrintStatusDialog = false;

                            var lengendsParam = report.Parameters["Legends"];
                            lengendsParam.Visible = false;
                            lengendsParam.Value = Helper.Legends();


                            report.ShowPreview();
                        }
                    }
                    catch (Exception exp)
                    {
                        Gujjar.ErrMsg(exp);
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
