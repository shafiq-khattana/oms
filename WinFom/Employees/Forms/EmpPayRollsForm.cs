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

namespace WinFom.Employees.Forms
{
    public partial class EmpPayRollsForm : Form
    {
        private DateTime today = DateTime.Now;
        private int month = 0;
        private int year = 0;
        private List<PayRollEntryVM> payRollEntries = new List<PayRollEntryVM>();
        private string btndgvpayroll = "btndgvpayroll123121";
        public EmpPayRollsForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadPayRollEntries()
        {
            
            int days2 = DateTime.DaysInMonth(year, month);
            DateTime payRollDate = new DateTime(year, month, days2);
            try
            {
                using (Context db = new Context())
                {
                    var emps = db.Employees.ToList()
                        .Where(a => a.DateAdded.Date <= payRollDate.Date).ToList();
                    
                    if(payRollEntries != null)
                    {
                        if(payRollEntries.Count > 0)
                        {
                            payRollEntries.Clear();
                            payRollEntries = null;
                        }
                        payRollEntries = new List<PayRollEntryVM>();
                        foreach (var item in emps)
                        {
                            var prentry = db.PayRollEntries.Where(a => a.EmployeeId == item.Id && a.ActionDate != null).ToList()
                                .FirstOrDefault(a => a.Month == month && a.Year == year);
                            decimal advAmount = 0;
                            var advancesEmp = db.CreditEntries.Where(a => a.EmployeeId == item.Id).ToList()
                                .Where(a => a.Date.Month == month && a.Date.Year == year);
                            if(advancesEmp.Count() > 0)
                            {
                                advAmount = advancesEmp.Sum(a => a.Amount);
                            }
                            if(prentry == null)
                            {
                                PayRollEntryVM vm = new PayRollEntryVM
                                {
                                    Id = 0,
                                    Advances = advAmount,
                                    Allowances = 0,
                                    Deductions = 0,
                                    Employee = item.Name,
                                    GrossSalary = 0,
                                    IsPaid = false,
                                    NetSalary = 0,
                                    PaidDate = "N/A",
                                    Remarks = "N/A",
                                    Salary = item.Salary,
                                    SalaryMonthYear = "N/A",
                                    Month = month,
                                    EmpId = item.Id,
                                    Year = year
                                };
                                payRollEntries.Add(vm);
                            }
                            else
                            {
                                
                                PayRollEntryVM vm = new PayRollEntryVM
                                {
                                    Id = prentry.Id,
                                    SalaryMonthYear = prentry.SalaryMonthYear,
                                    Salary = prentry.EmployeeSalary,
                                    Advances = prentry.EmpAdvances,
                                    Allowances = prentry.AllowanceAmount,
                                    Deductions = prentry.DeductionAmount,
                                    Employee = item.Name,
                                    GrossSalary = prentry.GrossSalary,
                                    IsPaid = prentry.IsPaid,
                                    NetSalary = prentry.NetSalary,
                                    PaidDate = prentry.ActionDate.Value.ToShortDateString(),
                                    Remarks = prentry.Remarks,
                                    Month = month,
                                    EmpId = item.Id,
                                    Year = year
                                };
                                payRollEntries.Add(vm);
                            }
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
                Gujjar.AddDatagridviewButton(dgv, btndgvpayroll, "Gen PayRoll", "Gen PayRoll", 120);
                cbMonths.DataSource = Month.Months;
                cbMonths.DisplayMember = "Name";
                cbMonths.SelectedItem = cbMonths.Items.OfType<Month>().FirstOrDefault(a => a.Id == today.Month - 1);

                cbMonths.ValueMember = "Id";
                cbYears.DataSource = Month.Years;

                cbPayRollType.SelectedIndex = 0;

                month = today.Month - 1;
                year = today.Year;

                WaitForm wait1 = new WaitForm(LoadPayRollEntries);
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
                payRollEntryVMBindingSource.List.Clear();               
                

                foreach (var item in payRollEntries)
                {
                    if(item.SalaryMonthYear == "N/A")
                    {
                        item.SalaryMonthYear = string.Format("{0}/{1}", (cbMonths.SelectedItem as Month).Name, year);
                    }
                    payRollEntryVMBindingSource.List.Add(item);
                }

                tbEmployees.Text = payRollEntries.Count.ToString();
                tbTotalPaid.Text = payRollEntries.Sum(a => a.NetSalary).ToString("n2");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnViewMonthYear_Click(object sender, EventArgs e)
        {
            try
            {
                year = cbYears.Text.ToInt();
                month = (cbMonths.SelectedItem as Month).Id;

                WaitForm wait = new WaitForm(LoadPayRollEntries);
                wait.ShowDialog();

                UpdateDgv();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnRollType_Click(object sender, EventArgs e)
        {
            try
            {
                bool isPaid = false;
                if(cbPayRollType.Text == "Generated")
                {
                    isPaid = true;
                }
                var items = payRollEntries.Where(a => a.IsPaid == isPaid).ToList();

                payRollEntryVMBindingSource.List.Clear();


                foreach (var item in items)
                {
                    if (item.SalaryMonthYear == "N/A")
                    {
                        item.SalaryMonthYear = string.Format("{0}/{1}", (cbMonths.SelectedItem as Month).Name, year);
                    }
                    payRollEntryVMBindingSource.List.Add(item);
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

                if(ri == -1 || ri == dgv.NewRowIndex)
                {
                    return;
                }

                if(dgv.Columns[btndgvpayroll].Index == ci)
                {
                    int eid = dgv.Rows[ri].Cells[0].Value.ToInt();
                    if(eid != 0)
                    {
                        throw new Exception("PayRoll already generated");
                    }

                    if(!Helper.ConfirmAdminPassword())
                    {
                        return;
                    }

                    string empName = dgv.Rows[ri].Cells[1].Value.ToString();
                    decimal salary = dgv.Rows[ri].Cells[3].Value.ToString().ToDecimal();

                    var obj = payRollEntryVMBindingSource.List.OfType<PayRollEntryVM>()
                        .FirstOrDefault(p => p.Employee == empName && p.Salary == salary);

                    GeneratePayRollForm form = new GeneratePayRollForm(obj.EmpId, obj.Month, obj.Year);
                    form.ShowDialog();

                    if(form.IsDone)
                    {
                        month = (cbMonths.SelectedItem as Month).Id;
                        year = today.Year;

                        WaitForm wait1 = new WaitForm(LoadPayRollEntries);
                        wait1.ShowDialog();

                        UpdateDgv();
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
