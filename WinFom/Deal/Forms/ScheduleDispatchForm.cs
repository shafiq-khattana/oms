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
using WinFom.Common.Forms;
using Model.Deal.ViewModel;
using Model.Deal.Common;
using WinFom.Common.Model;
using Model.Admin.Model;
using Model.Financials.Model;

namespace WinFom.Deal.Forms
{
    public partial class ScheduleDispatchForm : Form
    {
        private int _scheduleId = 0;
        private DealSchedule dealSchedule = null;
        private AppDeal _deal = null;
        private List<GoodCompany> goodsCompanies = new List<GoodCompany>();
        private List<Driver> drivers = new List<Driver>();
        private List<VehicleVM> vehicleVmList = new List<VehicleVM>();
        GoodCompany goodCompany = null;
        VehicleVM vehicle = null;
        public bool IsDone = false;
        private AppSettings appSett = Helper.AppSet;
        public ScheduleDispatchForm(int scheduleId)
        {
            InitializeComponent();
            _scheduleId = scheduleId;
        }
        private void LoadDealSchedule()
        {
            try
            {
                using (Context db = new Context())
                {
                    dealSchedule = db.DealSchedules.FirstOrDefault(a => a.Id == _scheduleId);
                    _deal = db.AppDeals.Find(dealSchedule.AppDealId);
                    _deal.Company = db.Companies.Find(_deal.CompanyId);
                    _deal.Broker = db.Brokers.Find(_deal.BrokerId);
                    _deal.DealItem = db.DealItems.Find(_deal.DealItemId);
                    _deal.Packing = db.DealPackings.Find(_deal.DealPackingId);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
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
                Helper.IsOkApplied();
                WaitForm wait2 = new WaitForm(LoadDealSchedule);
                wait2.ShowDialog();

                tbBroker.Text = _deal.Broker.Name;
                tbCompany.Text = _deal.Company.Name;
                tbDealDate.Text = _deal.DealDate.ToShortDateString();
                tbDealItem.Text = _deal.DealItem.Name;
                tbDealNo.Text = _deal.Id.ToString();
                tbScheduledTradeUnits.Text = dealSchedule.ScheduledTradeUnits.ToString("n4");
                tbSchedulePackings.Text = dealSchedule.ScheduledPackingsUnits.ToString("n2");
                tbSchedulePrice.Text = dealSchedule.ScheduledPrice.ToString("n4");
                tbScheduleTotalQty.Text = dealSchedule.ScheduledSubTradeUnits.ToString("n4");
                tbPerTradeUnitPrice.Text = _deal.TradeUnitPrice.ToString("n2");
                lblPackings.Text = _deal.Packing.Name;
                lblTradeUnit.Text = dealSchedule.TradeUnitTitle;
                lblTotalQty.Text = dealSchedule.PackingUnitTitle;

                WaitForm wait = new WaitForm(LoadGoodsCompanies);
                wait.ShowDialog();

                BindGoodsCompanies();
                WaitForm wait3 = new WaitForm(LoadCombos);
                wait3.ShowDialog();
                BindCombos();

                Gujjar.TBOptional(tbRemarks);
                Gujjar.NumbersOnly(tbFareAmount);
                dtp.MinDate = appSett.StartDate;
                dtp.MaxDate = appSett.EndDate;
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

        private void LoadCombos()
        {
            
            LoadDrivers();
            LoadVehicles();
        }
        private void BindCombos()
        {
            
            BindDrivers();
            BindVehicles();
        }
        private void LoadGoodsCompanies()
        {
            try
            {
                using (Context db = new Context())
                {
                    goodsCompanies = db.GoodCompanies.OrderBy(a => a.Name).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindGoodsCompanies()
        {
            cbGoodsCompanies.DataSource = goodsCompanies;
            cbGoodsCompanies.DisplayMember = "Name";
            cbGoodsCompanies.ValueMember = "Id";
        }
        private void LoadDrivers()
        {
            try
            {
                using (Context db = new Context())
                {
                    drivers = db.Drivers.Where(a => a.IsAvailable)
                        .OrderBy(a => a.Name).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadVehicles()
        {
            try
            {
                using (Context db = new Context())
                {
                    vehicleVmList = new List<VehicleVM>();
                    var vehicles = db.Vehicles.Where(a => a.Status == VehicleStatus.Available).OrderBy(a => a.No).ToList();
                    foreach (var item in vehicles)
                    {
                        VehicleVM vm = new VehicleVM
                        {
                            Id = item.Id,
                            Name = string.Format("{0} {1}", item.VehicleType, item.No)
                        };
                        vehicleVmList.Add(vm);
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindDrivers()
        {
            cbDrivers.DataSource = drivers;
            cbDrivers.DisplayMember = "Name";
            cbDrivers.ValueMember = "Id";
        }

        private void BindVehicles()
        {
            cbVehicles.DataSource = vehicleVmList;
            cbVehicles.DisplayMember = "Name";
            cbVehicles.ValueMember = "Id";
        }

        private void btnAddGoodsCompany_Click(object sender, EventArgs e)
        {
            try
            {
                AddGoodsCompanyForm form = new AddGoodsCompanyForm();
                form.ShowDialog();

                int cid = form.GoodCompanyId;
                if(cid != 0)
                {
                    WaitForm wait1 = new WaitForm(LoadGoodsCompanies);
                    wait1.ShowDialog();
                    BindGoodsCompanies();

                    cbGoodsCompanies.SelectedItem = cbGoodsCompanies.Items.OfType<GoodCompany>().FirstOrDefault(a => a.Id == cid);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void cbGoodsCompanies_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(cbGoodsCompanies.SelectedIndex == -1)
                {
                    goodCompany = null;
                    return;
                }
                goodCompany = cbGoodsCompanies.SelectedItem as GoodCompany;
                
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddDriver_Click(object sender, EventArgs e)
        {
            try
            {
                

                AddDriverForm form = new AddDriverForm();
                form.ShowDialog();

                int gid = form.DriverId;
                if(gid != 0)
                {
                    WaitForm wait = new WaitForm(LoadDrivers);
                    wait.ShowDialog();

                    BindDrivers();

                    cbDrivers.SelectedItem = cbDrivers.Items.OfType<Driver>().FirstOrDefault(a => a.Id == gid);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddVehicle_Click(object sender, EventArgs e)
        {
            try
            {
               

                AddVehicleForm form = new AddVehicleForm(/*goodCompany.Id, goodCompany.Name*/);
                form.ShowDialog();

                int gid = form.VehicleId;
                if (gid != 0)
                {
                    WaitForm wait = new WaitForm(LoadVehicles);
                    wait.ShowDialog();

                    BindVehicles();

                    cbVehicles.SelectedItem = cbVehicles.Items.OfType<VehicleVM>().FirstOrDefault(a => a.Id == gid);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(cbDrivers.SelectedIndex == -1)
                {
                    throw new Exception("Please select driver");
                }
                if(cbVehicles.SelectedIndex == -1)
                {
                    throw new Exception("Please select vehicle");
                }
                if(cbGoodsCompanies.SelectedIndex == -1)
                {
                    throw new Exception("Please select good company");
                }

                DialogResult confirm = Gujjar.ConfirmYesNo("Are you sured to dispatch this schedule");
                if (confirm == DialogResult.No)
                    return;
                //Driver driver = cbDrivers.SelectedItem as Driver;
                //VehicleVM vehicle = cbVehicles.SelectedItem as VehicleVM;

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var schedule2 = db.DealSchedules.Find(_scheduleId);
                            schedule2.DispatchedDate = dtp.Value;
                            schedule2.IsDispatched = true;
                            ScheduleStatus oldStatus = schedule2.Status;
                            schedule2.Status = ScheduleStatus.Dispatched;
                            schedule2.VehicleId = vehicle.Id;
                            schedule2.DriverId = driver.Id;
                            schedule2.GoodCompanyId = goodCompany.Id;
                            schedule2.DispatchRemarks = tbRemarks.Text;
                            schedule2.FareDealAmount = tbFareAmount.Text.ToDecimal();
                            db.Entry(schedule2).State = System.Data.Entity.EntityState.Modified;


                            var vehicleObj = db.Vehicles.Find(vehicle.Id);
                            vehicleObj.Status = VehicleStatus.Dispatched;
                            db.Entry(vehicleObj).State = System.Data.Entity.EntityState.Modified;

                            var driverObj = db.Drivers.Find(driver.Id);
                            driverObj.IsAvailable = false;
                            db.Entry(driverObj).State = System.Data.Entity.EntityState.Modified;

                            db.SaveChanges();

                            

                            trans.Commit();
                            Gujjar.InfoMsg(string.Format("Schedule state is changed from ({0}) to ({1}).", oldStatus, schedule2.Status));
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

        Driver driver = null;
        private void cbDrivers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDrivers.SelectedIndex == -1)
            {
                driver = null;
                return;
            }

            driver = cbDrivers.SelectedItem as Driver;
        }
        
        private void cbVehicles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbVehicles.SelectedIndex == -1)
            {
                vehicle = null;
                return;
            }

            vehicle = cbVehicles.SelectedItem as VehicleVM;
        }

        private void tbFareAmount_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
