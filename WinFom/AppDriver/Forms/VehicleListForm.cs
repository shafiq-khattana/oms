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
using System.Data.Entity;
using Model.AppCompany.ViewModel;
using WinFom.Common.Forms;
using Model.AppDriver.ViewModel;
using Model.Admin.Model;
using WinFom.Common.Model;

namespace WinFom.AppDriver.Forms
{
    public partial class VehicleListForm : Form
    {
        private List<Vehicle> dbVehicle = null;
        private List<VehicleDispatchVM> dispatchVehicleVMList = new List<VehicleDispatchVM>();
        private List<VehicleLoadedVM> loadedVehicleVMList = new List<VehicleLoadedVM>();
        private AppSettings appSet = Helper.AppSet;
        public VehicleListForm()
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
                WaitForm form = new WaitForm(LoadVehicles);
                form.ShowDialog();

                
                WaitForm wait = new WaitForm(LoadDispatchVehicles);
                wait.ShowDialog();

                tbCompanies.Text = driverVehicleVMBindingSource.List.Count.ToString();
                btnDispatch.Text = string.Format("Dispatched Vehicle ({0})", dispatchVehicleVMList.Count);

                btnVehicleLoaded.Text = string.Format("Loaded Vehicle ({0})", loadedVehicleVMList.Count);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void LoadDispatchVehicles()
        {
            try
            {
                using (Context db = new Context())
                {
                    var schs = db.DealSchedules.ToList()
                        .Where(a => { DateTime dt = a.AddedDate.Date; return dt >= appSet.StartDate && dt <= appSet.EndDate && a.IsDispatched && !a.IsLoaded && !a.IsArrived; })
                        .ToList();
                    foreach (var item in schs)
                    {
                        var vehi = db.Vehicles.Find(item.VehicleId);
                        var deal = db.AppDeals.Find(item.AppDealId);
                        var comp = db.Companies.Find(deal.CompanyId);
                        var packs = db.DealPackings.Find(deal.DealPackingId);
                        var packUnit = db.PackingUnits.Find(deal.PackingUnitId);
                        VehicleDispatchVM vm = new VehicleDispatchVM
                        {
                            DispatchDateTime = item.DispatchedDate.Value.ToString(),
                            Vehicle = string.Format("{0} {1}", vehi.VehicleType, vehi.No),
                            Description = string.Format("Schedule: {0}, Deal: {1}, Company: {2}, Qty: {3}", 
                            item.Id, deal.Id, string.Format("{0} ({1})", comp.Name, comp.Address),
                            string.Format("{0} {1}-(s), {2} {3}-(s)", item.ScheduledPackingsUnits, packs.Name, item.ScheduledSubTradeUnits, packUnit.Name))
                        };

                        dispatchVehicleVMList.Add(vm);
                    }
                    var schs2 = db.DealSchedules.Where(a => a.IsLoaded && !a.IsArrived).ToList()
                        .Where(a => { DateTime dt = a.AddedDate.Date; return dt >= appSet.StartDate && dt <= appSet.EndDate; }).ToList(); ;
                    foreach (var item in schs2)
                    {
                        var vehi = db.Vehicles.Find(item.VehicleId);
                        var deal = db.AppDeals.Find(item.AppDealId);
                        var comp = db.Companies.Find(deal.CompanyId);
                        var packs = db.DealPackings.Find(deal.DealPackingId);
                        var packUnit = db.PackingUnits.Find(deal.PackingUnitId);
                        VehicleLoadedVM vm = new VehicleLoadedVM
                        {
                            LoadedDateTime = item.DispatchedDate.Value.ToString(),
                            Vehicle = string.Format("{0} {1}", vehi.VehicleType, vehi.No),
                            Description = string.Format("Schedule: {0}, Deal: {1}, Company: {2}, Qty: {3}",
                            item.Id, deal.Id, string.Format("{0} ({1})", comp.Name, comp.Address),
                            string.Format("{0} {1}-(s), {2} {3}-(s)", item.ScheduledPackingsUnits, packs.Name, item.ScheduledSubTradeUnits, packUnit.Name))
                        };
                        loadedVehicleVMList.Add(vm);
                    }
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
                    dbVehicle = db.Vehicles
                        .OrderBy(a => a.Id).ToList();

                    foreach (var it in dbVehicle)
                    {
                        var schs = db.DealSchedules.Where(a => a.IsArrived && a.VehicleId == it.Id).ToList()
                            .Where(a => { DateTime dt = a.AddedDate.Date; return dt >= appSet.StartDate && dt <= appSet.EndDate; }).ToList(); 
                        //it.GoodCompany = db.GoodCompanies.Find(it.GoodCompanyId);
                        float efficiency = 0;

                        decimal totalQtyLoaded = 0;
                        decimal totalQtyReceived = 0;
                        totalQtyLoaded += schs.Sum(a => a.LoadedSubTradeUnits);
                        totalQtyReceived += schs.Sum(a => a.ReceivedSubTradeUnits);
                        
                        decimal diffQty = totalQtyLoaded - totalQtyReceived;
                        if (totalQtyLoaded > 0)
                            efficiency = (float)(totalQtyReceived / totalQtyLoaded * 100);

                        DriverVehicleVM vm = new DriverVehicleVM
                        {
                            Id = it.Id,
                            Efficiency = efficiency.ToString("n2"),
                            Schedules = schs.Count,
                            No = it.No,
                            VehicleType = it.VehicleType.ToString()
                        };

                        if (dgv.InvokeRequired)
                        {
                            dgv.Invoke(new Action(() =>
                            {
                                //driverVMBindingSource.List.Add(vm);
                                driverVehicleVMBindingSource.List.Add(vm);
                            }));
                        }
                        else
                        {
                            //driverVMBindingSource.List.Add(vm);
                            driverVehicleVMBindingSource.List.Add(vm);
                        }

                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnDispatch_Click(object sender, EventArgs e)
        {
            try
            {
                if(dispatchVehicleVMList.Count > 0)
                {
                    VehicleDispatchForm form = new VehicleDispatchForm(dispatchVehicleVMList);
                    form.ShowDialog();
                }
                else
                {
                    throw new Exception("No vehicle is dispatched");
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnVehicleLoaded_Click(object sender, EventArgs e)
        {
            try
            {
                if (loadedVehicleVMList.Count > 0)
                {
                    VehicleLoadedForm form = new VehicleLoadedForm(loadedVehicleVMList);
                    form.ShowDialog();
                }
                else
                {
                    throw new Exception("No vehicle is loaded");
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
