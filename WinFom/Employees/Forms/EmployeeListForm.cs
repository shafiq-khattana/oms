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
using Model.Employees.Model;
using Model.Employees.ViewModel;
using WinFom.Financials.Forms;

namespace WinFom.Employees.Forms
{
    public partial class EmployeeListForm : Form
    {
        private List<Employee> employees = null;
        private string btndgvEdit = "btneditdagasd2";
        private string btndgvadvance = "btndgvadvances2341";
        private string btndgvaccont = "dgbbtndagwaccount";
       
        DateTime today = DateTime.Now.Date;
        public EmployeeListForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadEmployees()
        {
            try
            {
                using (Context db = new Context())
                {
                    employees = db.Employees.OrderBy(a => a.Name).ToList();

                    foreach (var item in employees)
                    {
                        item.Balance = 0;
                        var obj = db.CreditEntries.Where(a => a.EmployeeId == item.Id).AsParallel().ToList()
                            .Where(a => a.Date.Month == today.Month && a.Date.Year == today.Year).ToList();
                        if(obj != null && obj.Count() > 0)
                        {
                            item.Balance = obj.Sum(a => a.Amount);
                        }
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
                Gujjar.AddDatagridviewButton(dgv, btndgvEdit, "Edit", "Edit", 100);
                Gujjar.AddDatagridviewButton(dgv, btndgvadvance, "Advances", "Advances", 100);
                //Gujjar.AddDatagridviewButton(dgv, btndgvaccont, "Account", "Account", 100);
                WaitForm wait1 = new WaitForm(LoadEmployees);
                wait1.ShowDialog();

                UpdateDgv();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void UpdateDgv()
        {
            try
            {
                tbTotalAdvance.Text = employees.Sum(a => a.Balance).ToString("n2");
                foreach (var item in employees)
                {
                    EmployeeVM vm = new EmployeeVM
                    {
                        Id = item.Id,
                        Contact = item.Contact,
                        Designation = item.Designation,
                        Name = item.Name,
                        Salary = item.Salary.ToString("n2"),
                        Advance = item.Balance.ToString("n2")
                    };

                    employeeVMBindingSource.List.Add(vm);
                }
            }
            catch (Exception exp)
            {

                throw exp;
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

                if(dgv.Columns[btndgvadvance].Index == ci)
                {
                    

                    int empId = dgv.Rows[ri].Cells[0].Value.ToString().ToInt();
                    EmployeeAdvanceListForm form = new EmployeeAdvanceListForm(empId);
                    form.ShowDialog();
                }
                if(dgv.Columns[btndgvEdit].Index == ci)
                {
                    DialogResult rest = Gujjar.ConfirmYesNo("Are you sured to edit employee?");
                    if (rest == DialogResult.No)
                        return;
                    int empId = dgv.Rows[ri].Cells[0].Value.ToString().ToInt();

                    EditEmployeeForm form = new EditEmployeeForm(empId);
                    form.ShowDialog();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
