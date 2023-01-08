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
using Model.Deal.Common;
using WinFom.Common.Forms;
using Model.Financials.Model;
using WinFom.Common.Model;

namespace WinFom.Deal.Forms
{
    public partial class ChangeScheduleStatusForm : Form
    {
        private int _scheduleId = 0;
        private DealSchedule dealSchedule = null;
        public bool IsDone = false;
        public ChangeScheduleStatusForm(int scheduleId)
        {
            InitializeComponent();
            _scheduleId = scheduleId;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadSchedule()
        {
            try
            {
                using (Context db = new Context())
                {
                    dealSchedule = db.DealSchedules.Find(_scheduleId);
                    dealSchedule.AppDeal = db.AppDeals.Find(dealSchedule.AppDealId);
                }
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
                cbNewStatus.DataSource = Enum.GetNames(typeof(ScheduleStatus));
                WaitForm wait = new WaitForm(LoadSchedule);
                wait.ShowDialog();

                tbExistingStatus.Text = dealSchedule.Status.ToString();
                tbScheduleInfo.Text = string.Format("Schedule No. {0}", dealSchedule.Id);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var sch = db.DealSchedules.Find(_scheduleId);
                            ScheduleStatus oldStatus = sch.Status;
                            ScheduleStatus newStatus = (ScheduleStatus)Enum.Parse(typeof(ScheduleStatus), cbNewStatus.Text);

                            if (newStatus != oldStatus)
                            {
                                DialogResult res = Gujjar.ConfirmYesNo("Are you sure to change the status of schedule");
                                if (res == DialogResult.No)
                                    return;

                                switch (newStatus)
                                {
                                    case ScheduleStatus.Scheduled:
                                        sch.Status = newStatus;
                                        sch.IsArrived = false;
                                        sch.IsDispatched = false;
                                        sch.IsLoaded = false;
                                        sch.DispatchedDate = null;
                                        sch.LoadedDate = null;
                                        sch.ArrivalDate = null;

                                        var driverObj = db.Drivers.Find(sch.DriverId);
                                        driverObj.IsAvailable = true;
                                        db.Entry(driverObj).State = System.Data.Entity.EntityState.Modified;

                                        var vehicleObj = db.Vehicles.Find(sch.VehicleId);
                                        vehicleObj.Status = VehicleStatus.Available;
                                        db.Entry(vehicleObj).State = System.Data.Entity.EntityState.Modified;

                                        db.Entry(sch).State = System.Data.Entity.EntityState.Modified;
                                        db.SaveChanges();
                                        break;

                                    case ScheduleStatus.Dispatched:
                                        if (oldStatus == ScheduleStatus.Arrived || oldStatus == ScheduleStatus.Loaded)
                                        {
                                            sch.Status = newStatus;
                                            sch.IsLoaded = false;
                                            sch.IsArrived = false;
                                            sch.IsDispatched = true;
                                            sch.DispatchedDate = DateTime.Now;
                                            sch.LoadedDate = null;
                                            sch.ArrivalDate = null;

                                            var vehicleObj2 = db.Vehicles.Find(sch.VehicleId);
                                            vehicleObj2.Status = VehicleStatus.Dispatched;
                                            db.Entry(vehicleObj2).State = System.Data.Entity.EntityState.Modified;

                                            db.Entry(sch).State = System.Data.Entity.EntityState.Modified;
                                            db.SaveChanges();
                                        }
                                        else
                                        {
                                            throw new Exception("You can only change a dispatch status to schedule status only.");
                                        }
                                        break;

                                    case ScheduleStatus.Loaded:
                                        if (oldStatus != ScheduleStatus.Arrived)
                                        {
                                            throw new Exception("You can't change a load schedule to arrived schedule.");
                                        }
                                        else
                                        {
                                            sch.Status = newStatus;
                                            sch.IsLoaded = true;
                                            sch.IsArrived = false;
                                            sch.IsDispatched = true;
                                            sch.LoadedDate = DateTime.Now;
                                            sch.ArrivalDate = null;

                                            var vehicleObj3 = db.Vehicles.Find(sch.VehicleId);
                                            vehicleObj3.Status = VehicleStatus.Loaded;
                                            db.Entry(vehicleObj3).State = System.Data.Entity.EntityState.Modified;

                                            db.Entry(sch).State = System.Data.Entity.EntityState.Modified;
                                            db.SaveChanges();
                                        }
                                        break;

                                    case ScheduleStatus.Arrived:
                                        {
                                            throw new Exception("You can't make a schedule arrived directly");
                                        }


                                }

                                
                                db.SaveChanges();
                                trans.Commit();
                                Gujjar.InfoMsg(string.Format("Status is changed to ({0}) to ({1}).", oldStatus, newStatus));
                                IsDone = true;
                                Close();
                            }
                            else
                            {
                                throw new Exception("New status is same as the old status");
                            }
                        }
                        catch (Exception exp2)
                        {
                            trans.Rollback();
                            throw exp2;
                        }
                    }
                    
                }

                List<DayBook> dbooks = null;
                using (Context db = new Context())
                {
                    dbooks = db.DayBooks.Where(a => a.DealScheduleId == _scheduleId && !a.IsReversed).ToList();
                }
                if(dbooks != null && dbooks.Count > 0)
                {
                    foreach (var item in dbooks)
                    {
                        Helper.ReverseTransaction(item);
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
