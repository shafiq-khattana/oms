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
using Model.Financials.Model;
using Model.Admin.Model;
using WinFom.Common.Model;
using WinFom.Common.Forms;
using Model.Financials.ViewModel;
using Model.Employees.Model;
using Model.Employees.ViewModel;

namespace WinFom.Financials.Forms
{
    public partial class EmployeeAdvanceListForm : Form
    {
        private AppSettings AppSett = Helper.AppSet;
        public int empId { get; set; }
        private Employee employee = null;
        private List<CreditEntry> creditEntries = null;
        DateTime today = DateTime.Now.Date;
        public EmployeeAdvanceListForm(int employId)
        {
            InitializeComponent();
            empId = employId;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadEntries()
        {
            try
            {
                if(creditEntries != null)
                {
                    creditEntries.Clear();
                    creditEntries = null;
                }
                using (Context db = new Context())
                {
                    creditEntries = db.CreditEntries.
                        Where(a => a.EmployeeId == empId).AsParallel().ToList()
                        .Where(a => a.Date.Month == today.Month && a.Date.Year == today.Year)
                        .ToList();

                    if(employee == null)
                    {
                        employee = db.Employees.Find(empId);
                    }
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
                
                WaitForm wait1 = new WaitForm(LoadEntries);
                wait1.ShowDialog();
                lblHeading.Text = string.Format("Advances of employee : ({0})", employee.Name);
                UpdateDgv();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void UpdateDgv()
        {
            tbTotalEntries.Text = creditEntries.Count.ToString();
            tbTotal.Text = creditEntries.Sum(a => a.Amount).ToString("n2");

            employeeAdvanceVMBindingSource.List.Clear();
            foreach (var item in creditEntries)
            {
                EmployeeAdvanceVM vm = new EmployeeAdvanceVM
                {
                    Amount = item.Amount.ToString("n2"),
                    Id = item.Id,
                    Remarks = item.Remarks,
                    Date = item.Date.ToString()
                };
                employeeAdvanceVMBindingSource.List.Add(vm);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Helper.ConfirmAdminPassword())
                    return;

                EmployeeGiveAdvaceForm form = new EmployeeGiveAdvaceForm(empId);
                form.ShowDialog();

                if(form.IsDone)
                {
                    WaitForm wait1 = new WaitForm(LoadEntries);
                    wait1.ShowDialog();

                    UpdateDgv();
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
                
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        DateTime dtFrom = DateTime.Now;
        DateTime dtTo = DateTime.Now;

        private void LoadDataDated()
        {
            if (creditEntries != null)
            {
                creditEntries.Clear();
                creditEntries = null;
            }

            using (Context db = new Context())
            {
                
                creditEntries = db.CreditEntries.Where(a => a.EmployeeId == empId).AsParallel()
                       .ToList().Where(a => a.Date.Date >= dtFrom.Date && a.Date.Date <= dtTo.Date).ToList();
            }
        }
        private void btnDatedEntries_Click(object sender, EventArgs e)
        {
            try
            {
                dtFrom = dtpFrom.Value.Date;
                dtTo = dtpTo.Value.Date;

                WaitForm wait1 = new WaitForm(LoadDataDated);
                wait1.ShowDialog();

                UpdateDgv();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnCurrentMonthEntries_Click(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait1 = new WaitForm(LoadEntries);
                wait1.ShowDialog();

                UpdateDgv();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
