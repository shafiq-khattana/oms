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
using Model.Admin.Model;
using WinFom.Common.Model;
using Model.Financials.Model;

namespace WinFom.ReadyStuff.Forms
{
    public partial class ReadyDealListForm : Form
    {
        private List<ReadyDeal> dealList = new List<ReadyDeal>();
        private string btnschedules = "btngdassches234";
        private string btndgvcancel = "dgvbtncancel12";
        private AppSettings appSet = Helper.AppSet;
        
        public ReadyDealListForm()
        {
            InitializeComponent();
        }
        private void LoadReadyDealList()
        {
            try
            {
                using (Context db = new Context())
                {
                    dealList = db.ReadyDeals.Include(a => a.Broker).Include(a => a.ReadyItem).Include(a => a.ReadySchedules)
                        .OrderByDescending(a => a.Id).ToList()
                        .Where(a => { DateTime dt = a.DealDate.Date; return dt >= appSet.StartDate && dt <= appSet.EndDate; }).ToList();
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
                WaitForm wait = new WaitForm(LoadReadyDealList);
                wait.ShowDialog();
                Helper.IsOkApplied();
                Gujjar.AddDatagridviewButton(dgv, btnschedules, "Schedules", "Schedules", 80);
                Gujjar.AddDatagridviewButton(dgv, btndgvcancel, "Cancel", "Cancel", 80);
                DgvUpdate(dealList);
                dtpFrom.MinDate = dtpTo.MinDate = appSet.StartDate;
                dtpTo.MaxDate = dtpFrom.MaxDate = appSet.EndDate;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void DgvUpdate(List<ReadyDeal> _deals)
        {
            btnAllDeals.Text = string.Format("All Deals ({0})", _deals.Count);
            button2.Text = string.Format("Completed ({0})", _deals.Count(a => a.IsCompleted));
            label10.Text = _deals.Count.ToString();

            readyDealVMBindingSource.List.Clear();
            int index = 0;
            foreach (var item in _deals)
            {
                int totalsch = item.ReadySchedules.Count;
                int compsch = item.ReadySchedules.Count(a => a.IsCompleted);
                ReadyDealVM vm = new ReadyDealVM
                {
                    Id = item.Id,
                    IsCompleted = item.IsCompleted,
                    Broker = item.Broker.Name,
                    CompletionStatus = string.Format("{0}/{1}", compsch, totalsch),
                    DealDate = item.DealDate.ToShortDateString(),
                    NoOfVehicles = item.NoOfVehicles,
                    ReadyItem = item.ReadyItem.Title,
                    TotalWeight = item.ReadySchedules.Sum(a => a.WeighBridgeWeight),
                    State = item.DealStatus.ToString(),
                    Rate = item.PerTradeUnitPrice.ToString("n2")
                };

                readyDealVMBindingSource.List.Add(vm);
                if(item.DealStatus == AppDealStatus.Cancelled)
                {
                    dgv.Rows[index].DefaultCellStyle.BackColor = Color.Pink;
                }
                if(item.DealStatus == AppDealStatus.Completed)
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

                if(ri == -1 || ri == dgv.NewRowIndex)
                {
                    return;
                }
                #region "Schedules"
                if(dgv.Columns[btnschedules].Index == ci)
                {
                    int did = dgv.Rows[ri].Cells[0].Value.ToInt();
                    ReadyDealVM vm = readyDealVMBindingSource.List.OfType<ReadyDealVM>().FirstOrDefault(a => a.Id == did);

                    ReadyScheduleList form = new ReadyScheduleList(vm);
                    form.ShowDialog();

                    WaitForm wait = new WaitForm(LoadReadyDealList);
                    wait.ShowDialog();                   
                    DgvUpdate(dealList);
                }
                #endregion
                #region Cancel
                if(dgv.Columns[btndgvcancel].Index == ci)
                {
                    int did = dgv.Rows[ri].Cells[0].Value.ToInt();
                    ReadyDealVM vm = readyDealVMBindingSource.List.OfType<ReadyDealVM>().FirstOrDefault(a => a.Id == did);
                    if(vm.State == AppDealStatus.Cancelled.ToString())
                    {
                        throw new Exception("Deal is cancelled");
                    }
                    if(vm.State != AppDealStatus.Scheduled.ToString())
                    {
                        throw new Exception("Only scheduled deals can be cancelled");
                    }
                    if (!Helper.ConfirmAdminPassword())
                        return;

                    DialogResult rest = Gujjar.ConfirmYesNo("Are you sured to cancel this deal?");
                    if (rest == DialogResult.No)
                        return;

                    using (Context db = new Context())
                    {
                        ReadyDeal deal = db.ReadyDeals.Find(vm.Id);
                        var schs = db.ReadySchedules.Where(a => a.ReadyDealId == deal.Id).ToList();
                        foreach (var sch in schs)
                        {
                            var daybooks = db.DayBooks.Where(a => a.ReadyScheduleId == sch.Id).ToList();
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

                            var daybook2 = daybooks.Where(a => !a.IsReversed).ToList();
                            foreach (var item in daybook2)
                            {
                                Helper.ReverseTransaction(item);
                            }
                            if (daybooks != null && daybooks.Count > 0)
                            {
                                foreach (var item in daybooks)
                                {                                    
                                    item.ReadyScheduleId = null;
                                    item.IsReversed = true;
                                    db.Entry(item).State = EntityState.Modified;
                                }
                            }
                            db.SaveChanges();
                            
                        }

                        deal.IsCompleted = false;
                        deal.CompletionStatus = "Cancelled";
                        deal.DealStatus = AppDealStatus.Cancelled;
                        deal.EstimatedPrice = 0;
                        deal.ReadyTradeUnitId = null;
                        deal.TotalWeight = 0;

                        db.Entry(deal).State = EntityState.Modified;
                        db.SaveChanges();
                        Gujjar.InfoMsg("Deal is cancelled successfully");

                        WaitForm wait = new WaitForm(LoadReadyDealList);
                        wait.ShowDialog();
                        DgvUpdate(dealList);
                    }
                }
                #endregion
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
