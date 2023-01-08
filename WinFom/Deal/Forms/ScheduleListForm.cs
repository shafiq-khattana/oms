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
using WinFom.Common.Forms;
using Model.Deal.Common;
using WinFom.Common.Model;
using Model.Financials.Model;

namespace WinFom.Deal.Forms
{
    public partial class ScheduleListForm : Form
    {
        private int _dealId = 0;
        private AppDeal appDeal = null;
        private List<DealSchedule> dealSchedules = null;
        private DealVM dealVM = null;
        private string btndgvdispatch = "btndgvdispatch";
        private string btndgvload = "btndgvload";
        private string btndgvreceive = "btndgvreceive";
        private string editdgvbtn = "iakowerajoajag";
        private string dgvbtncancel = "dgvbtncancel";
        public ScheduleListForm(DealVM ddd)
        {
            InitializeComponent();
            _dealId = ddd.Id;
            dealVM = ddd;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadObjs()
        {
            try
            {
                using (Context db = new Context())
                {
                    appDeal = db.AppDeals.Find(_dealId);
                    dealSchedules = db.DealSchedules.Where(a => a.AppDealId == _dealId).OrderBy(a => a.Id).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void AddButtons()
        {
            Gujjar.AddDatagridviewButton(dgv, editdgvbtn, "Edit", "Edit", 80);
            Gujjar.AddDatagridviewButton(dgv, btndgvdispatch, "Dispatch", "Dispatch", 80);
            Gujjar.AddDatagridviewButton(dgv, btndgvload, "Load", "Load", 80);
            Gujjar.AddDatagridviewButton(dgv, btndgvreceive, "Receive", "Receive", 80);
            Gujjar.AddDatagridviewButton(dgv, dgvbtncancel, "Cancel", "Cancel", 80);
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm form = new WaitForm(LoadObjs);
                form.ShowDialog();

                AddButtons();

                BindTopHeader();
                BindSchedules();


            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void BindTopHeader()
        {
            string unit = dealVM.TotalQty.Split()[1];

            tbTotalSchedules.Text = dealVM.TotalSchedules.ToString();
            tbReceivedSchedules.Text = dealVM.ReceivedSchedules.ToString();
            tbRemainingSchedules.Text = dealVM.RemainingSchedules.ToString();
            tbReceivedQtySchedules.Text = string.Format("{0} {1}", dealVM.LoadedQty.ToString("n4"), unit);
            tbReceivedQtyActual.Text = string.Format("{0} {1}", dealVM.ActualReceivedQty.ToString("n4"), unit);
            tbQtyDifference.Text = string.Format("{0} {1}", dealVM.QtyDifference.ToString("n4"), unit);
            tbLossInCash.Text = dealVM.Loss;
            tbLossPercentage.Text = dealVM.LossPercentage;

            lblHeading.Text = string.Format("Schedule list of deal No. {0}, Company: ({1}).", dealVM.Id, dealVM.Company);
        }
        private void BindSchedules()
        {
            try
            {
                scheduleVM2BindingSource.List.Clear();
                int index = 0;
                foreach (var item in dealSchedules)
                {
                    ScheduleVM2 vm = new ScheduleVM2
                    {
                        Id = item.Id.ToString(),
                        ExpectedDate = item.ReadyDate.ToShortDateString(),
                        PackingUnits = string.Format("{0} {1}-(s)", item.ScheduledPackingsUnits.ToString("n2"), item.PackingUnitTitle),
                        Price = item.ScheduledPrice.ToString("n2"),
                        TotalSubUnits = string.Format("{0} {1}-(s)", item.ScheduledSubTradeUnits.ToString("n2"), item.PackingUnitTitle),
                        TradeUnits = string.Format("{0} {1}-(s)", item.ScheduledTradeUnits, item.TradeUnitTitle),
                        Status = item.Status.ToString()
                    };
                    if(item.DispatchedDate.HasValue)
                    {
                        vm.DispatchDateTime = item.DispatchedDate.Value.ToString();
                    }
                    else
                    {
                        vm.DispatchDateTime = "Not Dispatched";
                    }
                    if(item.LoadedDate.HasValue)
                    {
                        vm.LoadedDateTime = item.LoadedDate.Value.ToString();
                    }
                    else
                    {
                        vm.LoadedDateTime = "Not Loaded";
                    }
                    
                    
                    scheduleVM2BindingSource.List.Add(vm);
                    if (item.Status == ScheduleStatus.Cancelled)
                    {
                        var row = dgv.Rows[index];
                        row.DefaultCellStyle.BackColor = Color.Pink;
                    }
                    if (item.Status == ScheduleStatus.Arrived)
                    {
                        var row = dgv.Rows[index];
                        row.DefaultCellStyle.BackColor = Color.PaleGreen;
                    }
                    index++;
                }
                dgv.Refresh();
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

                if(dgv.Columns[dgvbtncancel].Index == ci)
                {
                    int sid = dgv.Rows[ri].Cells[0].Value.ToInt();
                    var svm2 = scheduleVM2BindingSource.List.OfType<ScheduleVM2>().FirstOrDefault(a => a.Id == sid.ToString());
                    ScheduleStatus status = (ScheduleStatus)Enum.Parse(typeof(ScheduleStatus), svm2.Status);

                    if (status == ScheduleStatus.Cancelled)
                    {
                        throw new Exception("Schedule is cancelled");
                    }

                    if(status == ScheduleStatus.Loaded || status == ScheduleStatus.Arrived)
                    {
                        throw new Exception("This schedule can't be cancelled");
                    }
                    if (!Helper.ConfirmAdminPassword())
                    {
                        return;
                    }

                    DialogResult rest = Gujjar.ConfirmYesNo("Are you sured to cancel this schedule?");
                    if (rest == DialogResult.No)
                        return;

                    using (Context db = new Context())
                    {
                        var sch = db.DealSchedules.Find(sid);
                        var dealObj = db.AppDeals.Find(sch.AppDealId);
                        dealObj.IsCompleted = false;

                        db.Entry(dealObj).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        sch.IsArrived = false;
                        sch.ArrivalDate = null;
                        sch.DispatchedDate = null;
                        sch.DriverId = null;
                        sch.EmployeeId = null;
                        sch.FareActualPaid = 0;
                        sch.FareDealAmount = 0;
                        sch.GoodCompanyId = null;
                        sch.IsDispatched = false;
                        sch.IsLoaded = false;
                        sch.LaborExpenses = 0;
                        sch.LoadedDate = null;
                        sch.LoadedPrice = 0;
                        sch.LoadedSubTradeUnits = 0;
                        sch.LoadedTradeUnits = 0;
                        sch.SelectorId = null;
                        sch.TracktorLaborAmount = 0;
                        sch.VehicleId = null;
                        sch.Status = ScheduleStatus.Cancelled;

                        db.Entry(sch).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();


                        List<DayBook> daybooks = db.DayBooks.Where(a => a.DealScheduleId == sch.Id).ToList();
                        foreach (var item in daybooks)
                        {
                            item.DealScheduleId = null;
                            if(!item.IsReversed)
                            {
                                Helper.ReverseTransaction(item);
                            }
                            item.IsReversed = true;
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                        }
                        db.SaveChanges();
                    }

                    WaitForm form4 = new WaitForm(LoadObjs);
                    form4.ShowDialog();
                    BindTopHeader();
                    BindSchedules();
                    Helper.UpdateDealStatus(sid);

                    Gujjar.InfoMsg("Deal schedule is changed to cancel successfully");
                }

                if(dgv.Columns[btndgvdispatch].Index == ci)
                {
                    int sid = dgv.Rows[ri].Cells[0].Value.ToInt();
                    var svm2 = scheduleVM2BindingSource.List.OfType<ScheduleVM2>().FirstOrDefault(a => a.Id == sid.ToString());
                    ScheduleStatus status = (ScheduleStatus)Enum.Parse(typeof(ScheduleStatus), svm2.Status);

                    if(status == ScheduleStatus.Cancelled)
                    {
                        throw new Exception("Schedule is cancelled");
                    }
                    if (!Helper.ConfirmUserPassword())
                    {
                        return;
                    }

                    
                    switch (status)
                    {
                        case ScheduleStatus.Arrived:
                            throw new Exception("Schedule has been arrived.");

                        case ScheduleStatus.Loaded:
                            throw new Exception("Schedule has been loaded.");

                        case ScheduleStatus.Dispatched:
                            throw new Exception("Schedule has already been dispatched yet.");
                    }
                    ScheduleDispatchForm form = new ScheduleDispatchForm(sid);
                    form.ShowDialog();

                    if(form.IsDone)
                    {
                        WaitForm form4 = new WaitForm(LoadObjs);
                        form4.ShowDialog();

                        

                        BindTopHeader();
                        BindSchedules();
                        Helper.UpdateDealStatus(sid);
                    }
                }

                if(dgv.Columns[btndgvload].Index == ci)
                {
                    int sid = dgv.Rows[ri].Cells[0].Value.ToInt();
                    var svm2 = scheduleVM2BindingSource.List.OfType<ScheduleVM2>().FirstOrDefault(a => a.Id == sid.ToString());
                    ScheduleStatus status = (ScheduleStatus)Enum.Parse(typeof(ScheduleStatus), svm2.Status);

                    if (status == ScheduleStatus.Cancelled)
                    {
                        throw new Exception("Schedule is cancelled");
                    }

                    if (!Helper.ConfirmUserPassword())
                    {
                        return;
                    }
                    
                    switch(status)
                    {
                        case ScheduleStatus.Arrived:
                            throw new Exception("Schedule has been arrived.");

                        case ScheduleStatus.Loaded:
                            throw new Exception("Schedule already has been loaded.");

                        case ScheduleStatus.Scheduled:
                            throw new Exception("Schedule has not been dispatched yet.");                       
                    }

                    ScheduleLoadForm form2 = new ScheduleLoadForm(sid);
                    form2.ShowDialog();

                    if(form2.IsDone)
                    {
                        WaitForm form4 = new WaitForm(LoadObjs);
                        form4.ShowDialog();



                        BindTopHeader();
                        BindSchedules();
                        Helper.UpdateDealStatus(sid);
                    }
                }

                if(dgv.Columns[btndgvreceive].Index == ci)
                {
                    int sid = dgv.Rows[ri].Cells[0].Value.ToInt();
                    var svm2 = scheduleVM2BindingSource.List.OfType<ScheduleVM2>().FirstOrDefault(a => a.Id == sid.ToString());
                    ScheduleStatus status = (ScheduleStatus)Enum.Parse(typeof(ScheduleStatus), svm2.Status);

                    if (status == ScheduleStatus.Cancelled)
                    {
                        throw new Exception("Schedule is cancelled");
                    }
                    if (!Helper.ConfirmUserPassword())
                    {
                        return;
                    }
                    
                    switch (status)
                    {
                        case ScheduleStatus.Arrived:
                            throw new Exception("Schedule has been arrived.");

                        case ScheduleStatus.Dispatched:
                            throw new Exception("Schedule has been dispatched.");

                        case ScheduleStatus.Scheduled:
                            throw new Exception("Schedule has not been dispatched yet.");
                    }

                    ScheduleReceiveForm form2 = new ScheduleReceiveForm(sid);
                    form2.ShowDialog();

                    if (form2.IsDone)
                    {
                        WaitForm form4 = new WaitForm(LoadObjs);
                        form4.ShowDialog();
                        BindTopHeader();
                        BindSchedules();

                        Helper.UpdateDealStatus(sid);
                    }
                }

                if (dgv.Columns[editdgvbtn].Index == ci)
                {
                    //throw new Exception("You can't avail this service. Thanks");

                    #region Commented code

                    int sid = dgv.Rows[ri].Cells[0].Value.ToInt();
                    var svm2 = scheduleVM2BindingSource.List.OfType<ScheduleVM2>().FirstOrDefault(a => a.Id == sid.ToString());
                    ScheduleStatus status = (ScheduleStatus)Enum.Parse(typeof(ScheduleStatus), svm2.Status);

                    if (status == ScheduleStatus.Cancelled)
                    {
                        throw new Exception("Schedule is cancelled");
                    }

                    if (status == ScheduleStatus.Scheduled)
                    {
                        throw new Exception("You can't change this schedule.");
                    }

                    ChangeScheduleStatusForm form2 = new ChangeScheduleStatusForm(sid);
                    form2.ShowDialog();

                    if (form2.IsDone)
                    {
                        using (Context db = new Context())
                        {
                            var sch = db.DealSchedules.Find(sid);
                            var dealObj = db.AppDeals.Find(sch.AppDealId);
                            dealObj.IsCompleted = false;

                            db.Entry(dealObj).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }

                        WaitForm form4 = new WaitForm(LoadObjs);
                        form4.ShowDialog();
                        BindTopHeader();
                        BindSchedules();
                        Helper.UpdateDealStatus(sid);
                    }


                    #endregion
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
