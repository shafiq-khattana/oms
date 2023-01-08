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
using Model.Admin.Model;
using WinFom.Common.Model;
using Khattana.BioMetric.SecuGenFingerPrintNonAuto;
using System.Configuration;
using Model.Employees.Model;
using Model.Financials.Model;
using Model.Retail.Model;
using System.Diagnostics;

namespace WinFom.Employees.Forms
{
    public partial class EditEmployeeForm : Form
    {
       
       
        public int EmployeeId = 0;
        private Employee employee = null;
        public EditEmployeeForm(int empId)
        {
            InitializeComponent();
            EmployeeId = empId;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadEmployee()
        {
            try
            {
                using (Context db = new Context())
                {
                    employee = db.Employees.Find(EmployeeId);
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
                Gujjar.TB4(pMain);
                Gujjar.NumbersOnly(tbSalary);
                WaitForm wait = new WaitForm(LoadEmployee);
                wait.ShowDialog();

                tbName.Text = employee.Name;
                tbDesignation.Text = employee.Designation;
                tbSalary.Text = employee.Salary.ToString("n2");
                bsStatus.Value = employee.IsActive;
                dtp.Value = employee.DateAdded;
                
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
                
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all textfields");
                }

                DialogResult confirm = Gujjar.ConfirmYesNo("Are you sure to update employee?");
                if (confirm == DialogResult.No)
                    return;

                using (Context db = new Context())
                {
                    var dbEmp = db.Employees.Find(EmployeeId);
                    dbEmp.Name = tbName.Text;
                    dbEmp.Designation = tbDesignation.Text;
                    dbEmp.Salary = tbSalary.Text.ToDecimal();
                    dbEmp.IsActive = bsStatus.Value;
                    dbEmp.DateAdded = dtp.Value.Date;

                    db.Entry(dbEmp).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    Gujjar.InfoMsg("Employee is updated");
                    Close();
                }
                
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
    }
}
