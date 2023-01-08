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
    public partial class DriversListForm : Form
    {
        private List<Driver> dbDrivers = null;
        private AppSettings appSet = Helper.AppSet;
        public DriversListForm()
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
                //pMain.Visible = false;
                WaitForm form = new WaitForm(LoadDrivers);
                form.ShowDialog();
                
                tbCompanies.Text = driverVMBindingSource.List.Count.ToString();
                //dgv.Visible = false;
                //if (dgv.Visible == false)
                //    bunifuTransition1.ShowSync(dgv);
                //else
                //{
                //    bunifuTransition1.HideSync(dgv);
                //}
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadDrivers()
        {
            try
            {
                using (Context db = new Context())
                {
                    dbDrivers = db.Drivers
                        .OrderBy(a => a.Id).ToList();

                    foreach (var it in dbDrivers)
                    {
                        if (it.Name == "N/A")
                            continue;
                        var schs = db.DealSchedules.Where(a => a.IsArrived && a.DriverId == it.Id).ToList()
                            .Where(a => { DateTime dt = a.AddedDate.Date; return dt >= appSet.StartDate && dt <= appSet.EndDate; }).ToList(); ;
                        //it.GoodCompany = db.GoodCompanies.Find(it.GoodCompanyId);
                        float efficiency = 0;

                        decimal totalQtyLoaded = 0;
                        decimal totalQtyReceived = 0;
                        totalQtyLoaded += schs.Sum(a => a.LoadedSubTradeUnits);
                        totalQtyReceived += schs.Sum(a => a.ReceivedSubTradeUnits);

                        decimal diffQty = totalQtyLoaded - totalQtyReceived;
                        if (totalQtyLoaded > 0)
                            efficiency = (float)(totalQtyReceived / totalQtyLoaded * 100);

                        DriverVM vm = new DriverVM
                        {
                            Id = it.Id,
                            Address = it.Address,
                            Contact = it.Contact,
                            DateAdded = it.DateAdded.ToShortDateString(),
                            IsActive = it.IsActive,
                            //Company = it.GoodCompany.Name,
                            Extra = it.Extra,
                            Efficiency = efficiency.ToString("n2"),
                            Name = it.Name,
                            Remarks = it.Remarks,
                            Schedules = schs.Count
                        };

                        if (dgv.InvokeRequired)
                        {
                            dgv.Invoke(new Action(() =>
                            {
                                driverVMBindingSource.List.Add(vm);
                            }));
                        }
                        else
                        {
                            driverVMBindingSource.List.Add(vm);
                        }

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
