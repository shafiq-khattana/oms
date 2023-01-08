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
using Model.Deal.ViewModel;
using System.Data.Entity;
using WinFom.Common.Forms;
using System.Globalization;
using WinFom.Common.Model;
using Model.Admin.Model;
using Model.Financials.Model;

namespace WinFom.Deal.Forms
{
    public partial class DealListForm : Form
    {
        private List<DealVM> dealVMList = new List<DealVM>();
        //private string btndgvedit = "btndgvedit2";
        private string btndgvschedules = "btndgvschedules2";
        private string btndgvdel = "btndgvrwerwer";
        private string btndgvcancel = "btndgvcancel";
        private AppSettings appSett = Helper.AppSet;
        public DealListForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadDeals()
        {
            try
            {
                if(dealVMList != null && dealVMList.Count > 0)
                {
                    dealVMList.Clear();
                }
                using (Context db = new Context())
                {
                    var deals = db.AppDeals.Include(a => a.Broker).Include(a => a.Company).Include(a => a.DealItem)
                        .Include(a => a.Packing).Include(a => a.PackingUnit).Include(a => a.TradeUnit).Include(a => a.DealSchedules)
                        .OrderByDescending(a => a.Id).ToList()
                        .Where(a => { DateTime dt = a.DealDate.Date; return dt >= appSett.StartDate && dt <= appSett.EndDate; }).ToList();


                    if (dgv.InvokeRequired)
                    {
                        dgv.Invoke(new Action(() =>
                        {
                            dealVMBindingSource.List.Clear();
                        }));
                    }
                    else
                    {
                        dealVMBindingSource.List.Clear();
                    }
                    
                    foreach (var item in deals)
                    {
                        DealVM vm = new DealVM
                        {
                            Id = item.Id,
                            Company = item.Company.Name,
                            Broker = item.Broker.Name,
                            Date = item.DealDate.ToShortDateString(),
                            Item = item.DealItem.Name,
                            PackingUnits = string.Format("{0} {1}-(s)", item.PackingQty.ToString("n2"), item.Packing.Name),
                            TotalPrice = item.DealPrice.ToString("n2"),
                            TotalQty = string.Format("{0} {1}-(s)", item.TotalQty.ToString("n2"), item.PackingUnit.Name),
                            TradeUnits = string.Format("{0} {1}-(s)", item.TotalTradeUnits.ToString("n2"), item.TradeUnit.Title),
                            TotalSchedules = item.SchedulesCount,
                            IsCompleted = item.IsCompleted,
                            Rate = item.TradeUnitPrice.ToString("n1")
                        };



                        vm.ReceivedSchedules = item.DealSchedules.Count(a => a.IsArrived);
                        vm.LoadedQty = item.DealSchedules.Where(a => a.IsArrived).Sum(a => a.LoadedSubTradeUnits);
                        vm.ActualReceivedQty = item.DealSchedules.Where(a => a.IsArrived)
                            .Sum(a => a.ReceivedSubTradeUnits);

                        decimal ttu = item.TradeUnit.Qty;
                        if (ttu > 0)
                        {
                            vm.Loss = (vm.QtyDifference / item.TradeUnit.Qty * item.TradeUnitPrice).ToString("n2");
                        }
                        else
                        {
                            vm.Loss = "0";
                        }
                        vm.LossPercentage = "0";
                        if (vm.LoadedQty > 0)
                        {
                            vm.LossPercentage = (vm.QtyDifference / vm.LoadedQty * 100).ToString("n2");
                        }

                        vm.CompletionStatus = string.Format("{0}/{1}", vm.ReceivedSchedules, vm.TotalSchedules);
                       
                        vm.DealStatus = item.DealStatus.ToString();
                        dealVMList.Add(vm);
                        
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void BindDgv(bool isDate = false, bool isCompleted = false)
        {
            try
            {
                List<DealVM> list2 = dealVMList;
                dealVMBindingSource.Clear();
                btnAllDeals.Text = string.Format("{0} ({1})", "All Deals", dealVMList.Count);

                DateTime dtFrom2 = dtpFrom.Value.Date;
                DateTime dtTo2 = dtpTo.Value.Date;

                int dateCount = dealVMList.Count(a => Convert.ToDateTime(a.Date)
                .Date >= dtFrom2 && Convert.ToDateTime(a.Date).Date <= dtTo2);

                btnDateRangeView.Text = string.Format("{0} ({1})", "View Dated", dateCount);

                int compCount = dealVMList.Count(a => a.IsCompleted);

                button2.Text = string.Format("{0} ({1})", "Completed", compCount);

                if (isDate)
                {
                    DateTime dtFrom = dtpFrom.Value.Date;
                    DateTime dtTo = dtpTo.Value.Date;

                    list2 = dealVMList.Where(a => Convert.ToDateTime(a.Date)
                    .Date >= dtFrom && Convert.ToDateTime(a.Date).Date <= dtTo)
                    .ToList();
                }
                if (isCompleted)
                {
                    list2 = dealVMList.Where(a => a.IsCompleted).ToList();
                }
                if (isCompleted && isDate)
                {
                    DateTime dtFrom = dtpFrom.Value.Date;
                    DateTime dtTo = dtpTo.Value.Date;

                    list2 = dealVMList.Where(a => a.IsCompleted && Convert.ToDateTime(a.Date).Date >= dtFrom && Convert.ToDateTime(a.Date).Date <= dtTo).ToList();
                }
                int index = 0;
                foreach (var item in list2)
                {
                    dealVMBindingSource.List.Add(item);
                    if (item.DealStatus == AppDealStatus.Cancelled.ToString())
                    {
                        dgv.Rows[index].DefaultCellStyle.BackColor = Color.Pink;
                    }
                    else if (item.DealStatus == AppDealStatus.Completed.ToString())
                    {
                        dgv.Rows[index].DefaultCellStyle.BackColor = Color.PaleGreen;
                    }
                    index++;
                   
                }

                dgv.Refresh();
                label10.Text = list2.Count.ToString();

                tbTotalLossInCash.Text = list2.Sum(a => a.Loss.ToDecimal()).ToString("n2");
                tbTotalQtyLoss.Text = list2.Sum(a => a.QtyDifference).ToString("n2");
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
                WaitForm wait = new WaitForm(LoadDeals);
                wait.ShowDialog();
                //Gujjar.AddDatagridviewButton(dgv, btndgvedit, "Edit", "Edit", 80);
                Gujjar.AddDatagridviewButton(dgv, btndgvschedules, "Schedules", "Schedules", 80);
                Gujjar.AddDatagridviewButton(dgv, btndgvdel, "Delete", "Delete", 80);
                Gujjar.AddDatagridviewButton(dgv, btndgvcancel, "Cancel", "Cancel", 80);
                BindDgv();

                dtpFrom.MinDate = dtpTo.MinDate = appSett.StartDate;
                dtpTo.MaxDate = dtpFrom.MaxDate = appSett.EndDate;
                Helper.IsOkApplied();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDateRangeView_Click(object sender, EventArgs e)
        {
            try
            {
                BindDgv(true, false);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAllDeals_Click(object sender, EventArgs e)
        {
            try
            {
                BindDgv(false, false);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                BindDgv(false, true);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                int ci = e.ColumnIndex;

                if (ri == -1 || ri == dgv.NewRowIndex)
                    return;

                int id = dgv.Rows[ri].Cells[0].Value.ToInt();

                var obj = dealVMBindingSource.List.OfType<DealVM>().FirstOrDefault(a => a.Id == id);



                if (dgv.Columns[btndgvschedules].Index == ci)
                {
                    //if(obj.DealStatus == AppDealStatus.Cancelled.ToString())
                    //{
                    //    throw new Exception("This deal is cancelled");
                    //}
                    ScheduleListForm form = new ScheduleListForm(obj);
                    form.ShowDialog();

                    WaitForm wait = new WaitForm(LoadDeals);
                    wait.ShowDialog();                    
                    BindDgv();
                }
                if (dgv.Columns[btndgvdel].Index == ci)
                {
                    if (obj.DealStatus == AppDealStatus.Cancelled.ToString())
                    {
                        throw new Exception("This deal is cancelled");
                    }

                    DialogResult result = Gujjar.ConfirmYesNo("Are you sure to delete this deal?");
                    if (result == DialogResult.No)
                        return;

                    if (!Helper.ConfirmAdminPassword())
                        return;

                    dealVMBindingSource.List.Clear();

                    #region Comments

                    try
                    {
                        using (Context db = new Context())
                        {
                            List<DayBook> daybooks = new List<DayBook>();
                            using (var trans = db.Database.BeginTransaction())
                            {
                                try
                                {
                                    var deal = db.AppDeals.Find(id);
                                    
                                    var items = db.DealSchedules.Where(a => a.AppDealId == deal.Id).ToList();

                                    foreach (var item in items)
                                    {
                                        var dbooks = db.DayBooks.Where(a => a.DealScheduleId == item.Id && !a.IsReversed).ToList();
                                        var dbooks2 = db.DayBooks.Where(a => a.DealScheduleId == item.Id).ToList();
                                        foreach (var dbItem in dbooks2)
                                        {
                                            dbItem.DealScheduleId = null;
                                            db.Entry(dbItem).State = EntityState.Modified;
                                        }
                                        db.SaveChanges();
                                        daybooks.AddRange(dbooks);
                                        var als = db.ScheduleAlarms.Find(item.Id);
                                        db.ScheduleAlarms.Remove(als);
                                        var driver = db.Drivers.Find(item.DriverId);
                                        var vehicle = db.Vehicles.Find(item.VehicleId);

                                        var gComp = db.GoodCompanies.Find(item.GoodCompanyId);

                                        var slps = db.ScheduleLoadPackings.Where(a => a.DealSchedule.Id == item.Id).ToList();
                                        foreach (var item3 in slps)
                                        {
                                            if(item.IsArrived)
                                            {
                                                item3.DealPacking = db.DealPackings.Find(item3.DealPackingId);
                                                string msg3 = string.Format("Addition due to deletion of schedule. Schedule No: {0}, Deal No: {1}, Packing: {2}, Qty: {3}, Delete Date time: {4}. Driver: {5}, Vehicle: {6} {7}",
                                                    item.Id, deal.Id, item3.DealPacking.Name, item3.ReceiveQty, DateTime.Now, driver.Name, vehicle.VehicleType, vehicle.No);

                                                FactoryPackingStockAddedRecord fpsa = new FactoryPackingStockAddedRecord
                                                {
                                                    AddedDate = DateTime.Now,
                                                    DealPackingId = item3.DealPackingId,
                                                    Description = msg3,
                                                    QtyAdded = item3.ReceiveQty
                                                };

                                                db.FactoryPackingStockAddedRecords.Add(fpsa);
                                                db.SaveChanges();

                                                var fsp = db.FactoryPackingStocks.FirstOrDefault(a => a.DealPackingId == item3.DealPackingId);
                                                fsp.Quantity -= item3.ReceiveQty;

                                                db.Entry(fsp);
                                                db.SaveChanges();

                                                PackingIssueReceiveRecord pirr = new PackingIssueReceiveRecord
                                                {
                                                    DateTime = DateTime.Now,
                                                    DealPackingId = item3.DealPackingId,
                                                    Description = "Deletion Entry",
                                                    RecordType = RecordType.Issue,
                                                    Qty = item3.ReceiveQty,
                                                    Remarks = string.Format("{0}.\nDescription: {1}", msg3, "Deletion Entry"),
                                                    Unum = Helper.Unum,
                                                    GoodCompanyId = gComp.Id
                                                };
                                                db.PackingIssueReceiveRecords.Add(pirr);
                                                db.SaveChanges();

                                                var pstock = db.PackingStocks.FirstOrDefault(a => a.DealPackingId == item3.DealPackingId && a.GoodCompanyId == gComp.Id);
                                                if(pstock != null)
                                                {
                                                    pstock.Balance += item3.ReceiveQty;
                                                    db.Entry(pstock).State = EntityState.Modified;
                                                    db.SaveChanges();
                                                }
                                            }

                                            db.ScheduleLoadPackings.Remove(item3);
                                        }
                                        db.SaveChanges();

                                        var swbs = db.ScheduleWeighBridges.Where(a => a.DealScheduleId == item.Id).ToList();
                                        foreach (var item4 in swbs)
                                        {
                                            db.ScheduleWeighBridges.Remove(item4);
                                        }

                                        db.SaveChanges();
                                        db.DealSchedules.Remove(item);
                                        db.SaveChanges();

                                        
                                    }
                                    db.SaveChanges();

                                    deal = db.AppDeals.Remove(deal);

                                    db.SaveChanges();

                                    trans.Commit();
                                    if(appSett.PrintFinancialTransactions)
                                    {
                                        Helper.PrintTransactions(daybooks);
                                    }
                                    foreach (var item in daybooks)
                                    {
                                        Helper.ReverseTransaction(item);
                                    }

                                    string msg = string.Format("Deal with Id ({0}) has been deleted", deal.Id);
                                    Gujjar.InfoMsg(msg);

                                    //var item2 = dealVMBindingSource.List.OfType<DealVM>().FirstOrDefault(a => a.Id == id);
                                    //if(item2 != null)
                                    //{
                                    //    dealVMBindingSource.List.Remove(item2);
                                    //}

                                    WaitForm wait = new WaitForm(LoadDeals);
                                    wait.ShowDialog();
                                    BindDgv();
                                }
                                catch (Exception ab)
                                {
                                    trans.Rollback();
                                    throw ab;
                                }
                            }

                        }
                    }
                    catch (Exception exp)
                    {
                        Gujjar.ErrMsg(exp);
                    }
                    
                    #endregion
                }

                if (dgv.Columns[btndgvcancel].Index == ci)
                {
                    if (obj.DealStatus == AppDealStatus.Cancelled.ToString())
                    {
                        throw new Exception("This deal is cancelled");
                    }

                    if(obj.DealStatus != AppDealStatus.Scheduled.ToString())
                    {
                        throw new Exception("Only scheduled deals can be cancelled");
                    }
                    using (Context db = new Context())
                    {
                        var schs = db.DealSchedules.Where(a => a.AppDealId == id).ToList();
                        foreach (var item in schs)
                        {
                            if(item.Status == Model.Deal.Common.ScheduleStatus.Loaded || item.Status == Model.Deal.Common.ScheduleStatus.Arrived)
                            {
                                throw new Exception("This deal can't be cancelled. Because its any of schedule is loaded");
                            }
                        }
                    }
                        DialogResult result = Gujjar.ConfirmYesNo("Are you sure to cancel this deal?");
                    if (result == DialogResult.No)
                        return;

                    if (!Helper.ConfirmAdminPassword())
                        return;

                    dealVMBindingSource.List.Clear();

                    #region Comments

                    try
                    {
                        using (Context db = new Context())
                        {
                            List<DayBook> daybooks = new List<DayBook>();
                            using (var trans = db.Database.BeginTransaction())
                            {
                                try
                                {
                                    var deal = db.AppDeals.Find(id);
                                    var items = db.DealSchedules.Where(a => a.AppDealId == deal.Id).ToList();

                                    foreach (var item in items)
                                    {
                                        var dbooks = db.DayBooks.Where(a => a.DealScheduleId == item.Id && !a.IsReversed).ToList();
                                        var dbooks2 = db.DayBooks.Where(a => a.DealScheduleId == item.Id).ToList();
                                        foreach (var dbItem in dbooks2)
                                        {
                                            dbItem.DealScheduleId = null;
                                            db.Entry(dbItem).State = EntityState.Modified;
                                        }
                                        db.SaveChanges();
                                        daybooks.AddRange(dbooks);
                                        var als = db.ScheduleAlarms.Find(item.Id);                                        
                                        db.ScheduleAlarms.Remove(als);
                                        var driver = db.Drivers.Find(item.DriverId);
                                        var vehicle = db.Vehicles.Find(item.VehicleId);

                                        var gComp = db.GoodCompanies.Find(item.GoodCompanyId);

                                        var slps = db.ScheduleLoadPackings.Where(a => a.DealSchedule.Id == item.Id).ToList();
                                        foreach (var item3 in slps)
                                        {
                                            if (item.IsArrived)
                                            {
                                                item3.DealPacking = db.DealPackings.Find(item3.DealPackingId);
                                                string msg3 = string.Format("Addition due to deletion of schedule. Schedule No: {0}, Deal No: {1}, Packing: {2}, Qty: {3}, Delete Date time: {4}. Driver: {5}, Vehicle: {6} {7}",
                                                    item.Id, deal.Id, item3.DealPacking.Name, item3.ReceiveQty, DateTime.Now, driver.Name, vehicle.VehicleType, vehicle.No);

                                                FactoryPackingStockAddedRecord fpsa = new FactoryPackingStockAddedRecord
                                                {
                                                    AddedDate = DateTime.Now,
                                                    DealPackingId = item3.DealPackingId,
                                                    Description = msg3,
                                                    QtyAdded = item3.ReceiveQty
                                                };

                                                db.FactoryPackingStockAddedRecords.Add(fpsa);
                                                db.SaveChanges();

                                                var fsp = db.FactoryPackingStocks.FirstOrDefault(a => a.DealPackingId == item3.DealPackingId);
                                                fsp.Quantity -= item3.ReceiveQty;

                                                db.Entry(fsp);
                                                db.SaveChanges();

                                                PackingIssueReceiveRecord pirr = new PackingIssueReceiveRecord
                                                {
                                                    DateTime = DateTime.Now,
                                                    DealPackingId = item3.DealPackingId,
                                                    Description = "Deletion Entry",
                                                    RecordType = RecordType.Issue,
                                                    Qty = item3.ReceiveQty,
                                                    Remarks = string.Format("{0}.\nDescription: {1}", msg3, "Deletion Entry"),
                                                    Unum = Helper.Unum,
                                                    GoodCompanyId = gComp.Id
                                                };
                                                db.PackingIssueReceiveRecords.Add(pirr);
                                                db.SaveChanges();

                                                var pstock = db.PackingStocks.FirstOrDefault(a => a.DealPackingId == item3.DealPackingId && a.GoodCompanyId == gComp.Id);
                                                if (pstock != null)
                                                {
                                                    pstock.Balance += item3.ReceiveQty;
                                                    db.Entry(pstock).State = EntityState.Modified;
                                                    db.SaveChanges();
                                                }
                                            }

                                            db.ScheduleLoadPackings.Remove(item3);
                                        }
                                        db.SaveChanges();

                                        var swbs = db.ScheduleWeighBridges.Where(a => a.DealScheduleId == item.Id).ToList();
                                        foreach (var item4 in swbs)
                                        {
                                            db.ScheduleWeighBridges.Remove(item4);
                                        }

                                        //db.SaveChanges();
                                        //db.DealSchedules.Remove(item);
                                        item.IsArrived = false;
                                        item.DriverId = null;
                                        item.FareDealAmount = 0;
                                        item.GoodCompanyId = null;
                                        item.IsLoaded = false;
                                        item.IsDispatched = false;
                                        item.SelectorId = null;
                                        item.VehicleId = null;
                                        item.TracktorLaborAmount = 0;
                                        item.Status = Model.Deal.Common.ScheduleStatus.Cancelled;
                                        item.ScheduledPrice = 0;
                                        item.ReceivedPrice = 0;
                                        item.LoadedPrice = 0;
                                        item.DispatchedDate = null;
                                        db.Entry(item).State = EntityState.Modified;
                                        db.SaveChanges();

                                    }
                                    db.SaveChanges();

                                    //deal = db.AppDeals.Remove(deal);
                                    deal.CompletionStatus = "Cancelled";
                                    deal.DealStatus = AppDealStatus.Cancelled;
                                    deal.IsCompleted = false;
                                    db.Entry(deal).State = EntityState.Modified;
                                    db.SaveChanges();

                                    trans.Commit();
                                    if (appSett.PrintFinancialTransactions)
                                    {
                                        Helper.PrintTransactions(daybooks);
                                    }
                                    foreach (var item in daybooks)
                                    {
                                        Helper.ReverseTransaction(item);
                                    }

                                    string msg = string.Format("Deal with Id ({0}) has been deleted", deal.Id);
                                    Gujjar.InfoMsg(msg);

                                    //var item2 = dealVMBindingSource.List.OfType<DealVM>().FirstOrDefault(a => a.Id == id);
                                    //if(item2 != null)
                                    //{
                                    //    dealVMBindingSource.List.Remove(item2);
                                    //}

                                    WaitForm wait = new WaitForm(LoadDeals);
                                    wait.ShowDialog();
                                    BindDgv();
                                }
                                catch (Exception ab)
                                {
                                    trans.Rollback();
                                    throw ab;
                                }
                            }

                        }
                    }
                    catch (Exception exp)
                    {
                        Gujjar.ErrMsg(exp);
                    }

                    #endregion
                }
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
                int ci = e.ColumnIndex;

                if (ri == -1 || ri == dgv.NewRowIndex)
                    return;

                int id = dgv.Rows[ri].Cells[0].Value.ToInt();

                var obj = dealVMBindingSource.List.OfType<DealVM>().FirstOrDefault(a => a.Id == id);

                string unit = obj.TotalQty.Split()[1];

                tbTotalSchedules.Text = obj.TotalSchedules.ToString();
                tbReceivedSchedules.Text = obj.ReceivedSchedules.ToString();
                tbRemainingSchedules.Text = obj.RemainingSchedules.ToString();
                tbReceivedQtySchedules.Text = string.Format("{0} {1}", obj.LoadedQty.ToString("n2"), unit);
                tbReceivedQtyActual.Text = string.Format("{0} {1}", obj.ActualReceivedQty.ToString("n2"), unit);
                tbQtyDifference.Text = string.Format("{0} {1}", obj.QtyDifference.ToString("n2"), unit);
                tbLossInCash.Text = obj.Loss;
                tbLossPercentage.Text = obj.LossPercentage;

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
