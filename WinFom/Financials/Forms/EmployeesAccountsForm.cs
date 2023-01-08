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
    public partial class EmployeesAccountsForm : Form
    {
        private List<Employee> employeeList = null;
        private AppSettings AppSett = Helper.AppSet;
        private string dgvbtnledger = "dgvbtnledger";
        private string dgvbtnpay = "dgvbtnpay";
        public EmployeesAccountsForm()
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
                if(employeeList != null)
                {
                    employeeList.Clear();
                    employeeList = null;
                }
               
                using (Context db = new Context())
                {
                    employeeList = db.Employees.OrderBy(a => a.Name).ToList();
                    foreach (var item in employeeList)
                    {
                        var obj = db.AccountTransactions.Where(a => a.GeneralAccountId == item.GeneralAccountId)
                            .OrderByDescending(a => a.Id).FirstOrDefault();
                        item.Balance = 0;
                        if(obj != null)
                        {
                            item.Balance = obj.Balance;
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
                Gujjar.AddDatagridviewButton(dgv, dgvbtnpay, "Pay Payment", "Pay Payment", 120);
                Gujjar.AddDatagridviewButton(dgv, dgvbtnledger, "Ledger", "Ledger", 120);
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
                empAccountBalanceBindingSource.List.Clear();
                foreach (var item in employeeList)
                {
                    EmpAccountBalance emp = new EmpAccountBalance
                    {
                        Balance = item.Balance,
                        Designation = item.Designation,
                        Id = item.Id,
                        Name = item.Name,
                        Salary = item.Salary
                    };
                    empAccountBalanceBindingSource.List.Add(emp);
                }

                decimal balance = employeeList.Sum(a => a.Balance);
                string balStr = "";
                if(balance < 0)
                {
                    balStr = string.Format("{0} (Cr)", Math.Abs(balance).ToString("n2"));
                }
                else if (balance > 0)
                {
                    balStr = string.Format("{0} (Dr)", balance.ToString("n2"));
                }
                else
                {
                    balStr = "0.0";
                }

                tbTotalEntries.Text = balStr;
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
                
                if(dgv.Columns[dgvbtnledger].Index == ci)
                {
                    int empId = dgv.Rows[ri].Cells[0].Value.ToInt();
                    using (Context db = new Context())
                    {
                        string acctId = db.Employees.Find(empId).GeneralAccountId;
                        AccountTransactionForm form = new AccountTransactionForm(acctId);
                        form.ShowDialog();
                    }
                }
                
                if(dgv.Columns[dgvbtnpay].Index == ci)
                {
                    int empId = dgv.Rows[ri].Cells[0].Value.ToInt();
                    using (Context db = new Context())
                    {
                        string acctId = db.Employees.Find(empId).GeneralAccountId;
                        EmployeeGivePaymentForm form = new EmployeeGivePaymentForm(acctId);
                        form.ShowDialog();

                        if(form.IsDone)
                        {
                            WaitForm wait1 = new WaitForm(LoadEmployees);
                            wait1.ShowDialog();
                            UpdateDgv();
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
