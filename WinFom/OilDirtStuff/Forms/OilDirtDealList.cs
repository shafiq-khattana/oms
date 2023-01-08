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
using Model.OilDirtStuff.Model;
using WinFom.OilDirtStuff.ViewModel;
using Model.Financials.Model;

namespace WinFom.OilDirtStuff.Forms
{
    public partial class OilDirtDealList : Form
    {
        private List<OilDirtDeal> dealList = new List<OilDirtDeal>();
        private string btnschedules = "btngdassches234";
        private string dgvbtncancel = "btndgvcanel1231";
        private AppSettings appSet = Helper.AppSet;
        public OilDirtDealList()
        {
            InitializeComponent();
        }
        private void LoadOilDirtDealList()
        {
            try
            {
                using (Context db = new Context())
                {
                    dealList = db.OilDirtDeals.Include(a => a.Broker).Include(a => a.Item).Include(a => a.Schedules).Include(a => a.TradeUnit)
                        .OrderByDescending(a => a.Id).ToList()
                        .Where(a => { DateTime dt = a.GenerateDate.Date; return dt >= appSet.StartDate && dt <= appSet.EndDate; }).ToList();

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
                WaitForm wait = new WaitForm(LoadOilDirtDealList);
                wait.ShowDialog();
                Helper.IsOkApplied();
                Gujjar.AddDatagridviewButton(dgv, btnschedules, "Schedules", "Schedules", 80);
                Gujjar.AddDatagridviewButton(dgv, dgvbtncancel, "Cancel", "Cancel", 80);
                DgvUpdate(dealList);
                dtpFrom.MinDate = dtpTo.MinDate = appSet.StartDate;
                dtpTo.MaxDate = dtpFrom.MaxDate = appSet.EndDate;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void DgvUpdate(List<OilDirtDeal> _deals)
        {
            btnAllDeals.Text = string.Format("All Deals ({0})", _deals.Count);
            button2.Text = string.Format("Completed ({0})", _deals.Count(a => a.Status == OilDirtStatus.Completed));
            label10.Text = _deals.Count.ToString();

            oilDirtDealVMBindingSource.List.Clear();
            int index = 0;
            foreach (var item in _deals)
            {
                int totalsch = item.Schedules.Count;
                int compsch = item.Schedules.Count(a => a.Status == OilDirtScheduleStatus.Completed);
                OilDirtDealVM vm = new OilDirtDealVM
                {
                    Id = item.Id,
                    Broker = item.Broker.Name,
                    CompletionStatus = string.Format("{0}/{1}", compsch, totalsch),
                    DealDate = item.GenerateDate.ToShortDateString(),
                    NoOfVehicles = item.NoOfVehicles,
                    Item = item.Item.Title,
                    Rate = item.PerTradeUnitPrice.ToString("n4"),
                    ReadyDate = item.ReadyDate.ToShortDateString(),
                    State = item.Status.ToString(),
                    TradeUnit = item.TradeUnit.Title
                };
                oilDirtDealVMBindingSource.List.Add(vm);
                if(item.Status == OilDirtStatus.Cancelled)
                {
                    dgv.Rows[index].DefaultCellStyle.BackColor = Color.Pink;
                }
                else if(item.Status == OilDirtStatus.Completed)
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

                #region Schedule process
                if(dgv.Columns[btnschedules].Index == ci)
                {
                    int did = dgv.Rows[ri].Cells[0].Value.ToInt();
                    OilDirtDealVM vm = oilDirtDealVMBindingSource.List.OfType<OilDirtDealVM>().FirstOrDefault(a => a.Id == did);
                    OilDirtScheduleList form = new OilDirtScheduleList(vm);
                    form.ShowDialog();
                    if(form.IsDone)
                    {
                        WaitForm wait = new WaitForm(LoadOilDirtDealList);
                        wait.ShowDialog();
                        DgvUpdate(dealList);
                    }

                }
                #endregion

                #region Schedule cancel
                if(dgv.Columns[dgvbtncancel].Index == ci)
                {
                    int did = dgv.Rows[ri].Cells[0].Value.ToInt();
                    OilDirtDealVM vm = oilDirtDealVMBindingSource.List.OfType<OilDirtDealVM>().FirstOrDefault(a => a.Id == did);
                    if(vm.State == OilDirtStatus.Cancelled.ToString())
                    {
                        throw new Exception("Deal is cancelled");
                    }
                    if(vm.State != OilDirtStatus.Scheduled.ToString())
                    {
                        throw new Exception("Only scheduled deals are allowed to be cancelled");
                    }
                    if (!Helper.ConfirmAdminPassword())
                        return;
                    DialogResult rest = Gujjar.ConfirmYesNo("Are you sured to cancel this deal?..");
                    if (rest == DialogResult.No)
                        return;

                    using (Context db = new Context())
                    {
                        var deal = db.OilDirtDeals.Find(did);
                        var schs = db.OilDirtSchedules.Where(a => a.OilDirtDealId == deal.Id).ToList();
                        foreach (var schObj in schs)
                        {
                            List<DayBook> daybooks = db.DayBooks.Where(a => a.OilDirtScheduleId == schObj.Id)
                                .ToList();
                            
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
                                item.OilDealId = null;
                                item.IsReversed = true;
                                db.Entry(item).State = EntityState.Modified;
                            }
                            db.SaveChanges();
                        }
                        db.SaveChanges();
                        deal.CompletionStatus = "Cancelled";
                        deal.DateCompletion = null;
                        deal.Status = OilDirtStatus.Cancelled;

                        db.Entry(deal).State = EntityState.Modified;
                        db.SaveChanges();

                        Gujjar.InfoMsg("Deal is cancelled");

                        WaitForm wait = new WaitForm(LoadOilDirtDealList);
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
