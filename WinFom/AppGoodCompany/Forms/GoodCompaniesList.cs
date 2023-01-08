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
using Model.AppGoodCompany.ViewModel;
using WinFom.Common.Forms;
using Model.Admin.Model;
using WinFom.Common.Model;

namespace WinFom.AppGoodCompany.Forms
{
    public partial class GoodCompaniesList : Form
    {
        private string btndgvissuepacking = "btndgvissuepacking";
        private string btndgvpackingstate = "btndgvpackingstate";
        private string btndgvxissue = "btndgvasdf23423423as";
        private AppSettings appSett = Helper.AppSet;
        public GoodCompaniesList()
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
                Gujjar.AddDatagridviewButton(dgv, btndgvissuepacking, "Issue", "Issue Packing", 80);
                Gujjar.AddDatagridviewButton(dgv, btndgvpackingstate, "Packings", "Packings", 80);
                Gujjar.AddDatagridviewButton(dgv, btndgvxissue, "X Issue", "X Issue", 80);
                WaitForm wait = new WaitForm(LoadData);
                wait.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void LoadData()
        {
            try
            {
                using (Context db = new Context())
                {
                    var gComps = db.GoodCompanies.OrderBy(a => a.Name).ToList();

                    foreach (var gcomp in gComps)
                    {
                        int scheduleCount = 0;
                        decimal recQty = 0;
                        decimal loadQty = 0;
                        //var drivers = db.Drivers.Where(a => a.GoodCompanyId == gcomp.Id).ToList();
                        //int vCnt = db.Vehicles.Count(a => a.GoodCompanyId == gcomp.Id);
                        //foreach (var drv in drivers)
                        //{
                        var schs = db.DealSchedules.Where(a => a.IsArrived && a.GoodCompanyId == gcomp.Id).ToList()
                            .Where(a => { DateTime dt = a.AddedDate.Date; return dt >= appSett.StartDate && dt <= appSett.EndDate; }).ToList();

                        if (schs.Count() > 0)
                        {
                            scheduleCount += schs.Count();
                            recQty += schs.Sum(a => a.ReceivedSubTradeUnits);
                            loadQty += schs.Sum(a => a.LoadedSubTradeUnits);
                        }

                        //}

                        string effi = "0";
                        if (loadQty > 0)
                        {
                            effi = ((recQty / loadQty) * 100).ToString("n2");
                        }
                        GoodCompanyVM vm = new GoodCompanyVM
                        {
                            Id = gcomp.Id,
                            Address = gcomp.Address,
                            DateAdded = gcomp.DateAdded.ToShortDateString(),
                            IsActive = gcomp.IsActive,
                            Efficiency = effi,
                            Extra = gcomp.Extra,
                            Name = gcomp.Name,
                            Owner = gcomp.Owner,
                            OwnerContact = gcomp.OwnerContact,
                            Phone = gcomp.Phone,
                            Remarks = gcomp.Remarks,
                            Schedules = scheduleCount,
                            //Vehicles = vCnt,
                            //Drivers = drivers.Count
                        };

                        if (dgv.InvokeRequired)
                        {
                            dgv.Invoke(new Action(() => { goodCompanyVMBindingSource.List.Add(vm); }));
                        }
                        else
                        {
                            goodCompanyVMBindingSource.List.Add(vm);
                        }

                    }
                }
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

                int gcid = dgv.Rows[ri].Cells[0].Value.ToInt();
                if (dgv.Columns[btndgvissuepacking].Index == ci)
                {
                    try
                    {
                        IssuePackingCompanyForm form = new IssuePackingCompanyForm(gcid);
                        form.ShowDialog();
                    }
                    catch (Exception exp)
                    {
                        Gujjar.ErrMsg(exp);
                    }
                }
                if (dgv.Columns[btndgvpackingstate].Index == ci)
                {
                    GoodCompanyStock form = new GoodCompanyStock(gcid);
                    form.ShowDialog();
                }
                if (dgv.Columns[btndgvxissue].Index == ci)
                {
                    XIssuePackingForm form = new XIssuePackingForm(gcid);
                    form.ShowDialog();

                    if (form.IsDone)
                    {
                        goodCompanyVMBindingSource.List.Clear();

                        WaitForm wait = new WaitForm(LoadData);
                        wait.ShowDialog();
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
