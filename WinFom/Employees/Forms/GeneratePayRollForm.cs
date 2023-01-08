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
using WinFom.Admin.Forms;

namespace WinFom.Employees.Forms
{
    public partial class GeneratePayRollForm : Form
    {
        private int employeeId = 0;
        private Employee employee = null;
        private string salaryMonthYear = "N/A";
        private int salaryMonth = 0;
        private int salaryYear = 0;
        private List<CreditEntry> advancesList = null;        
        private List<SalaryAllowance> allowances = null;
        private List<SalaryDeduction> deductions = null;
        private decimal advanceAmount = 0;
        private decimal deductionAmount = 0;
        private decimal allowanceAmount = 0;

        private string dgvdeductiondelbtn = "dgvdeductiondelbtn1211";
        private string dgvallowancedelbtn = "dgvallowancedeletebutton";
        public bool IsDone { get; set; }

        AppUser appUser = SingleTon.LoginForm.appUser;
        AppSettings appSett = Helper.AppSet;
        public GeneratePayRollForm(int empId, int month, int year)
        {
            InitializeComponent();
            employeeId = empId;
            salaryMonth = month;
            salaryYear = year;
            salaryMonthYear = string.Format("{0}/{1}", salaryMonth, salaryYear);
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadInitData()
        {
            try
            {
                using (Context db = new Context())
                {
                    employee = db.Employees.Find(employeeId);
                    advancesList = db.CreditEntries.Where(a => a.EmployeeId == employeeId && a.Date.Month == salaryMonth && a.Date.Year == salaryYear)
                        .OrderBy(a => a.Date).ToList();
                    advanceAmount = advancesList.Sum(a => a.Amount);                    
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        #region Combo boxes Loading
        private void LoadCbAllowances()
        {
            try
            {
                using (Context db = new Context())
                {
                    allowances = db.SalaryAllowances.OrderBy(a => a.Title).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadCbDeductions()
        {
            try
            {
                using (Context db = new Context())
                {
                    deductions = db.SalaryDeductions.OrderBy(a => a.Title).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        #endregion

        #region Combo boxes binding
        private void BindCbAllowances()
        {
            cbAllowances.DataSource = allowances;
            cbAllowances.DisplayMember = "Title";
            cbAllowances.ValueMember = "Id";
        }
        private void BindCbDeductions()
        {
            cbDeductions.DataSource = deductions;
            cbDeductions.DisplayMember = "Title";
            cbDeductions.ValueMember = "Id";
        }
        #endregion
        private void BindFormData()
        {
            tbEmpName.Text = employee.Name;
            tbBasicSalary.Text = employee.Salary.ToString("n1");
            tbSalaryMonthYear.Text = salaryMonthYear;
            tbAdvancesAmount.Text = advanceAmount.ToString("n2");
            foreach (var item in advancesList)
            {
                AdvanceVM vm = new AdvanceVM
                {
                    Id = item.Id,
                    Date = item.Date.ToShortDateString(),
                    Amount = item.Amount,
                    Title = item.Remarks
                };
                advanceVMBindingSource.List.Add(vm);
            }

            

            WaitForm wait1 = new WaitForm(LoadCbAllowances);
            WaitForm wait2 = new WaitForm(LoadCbDeductions);
            wait1.ShowDialog();
            wait2.ShowDialog();

            BindCbAllowances();
            BindCbDeductions();

           

            tbRemarks.Text = "N/A";
            Gujjar.TBOptional(tbRemarks);

            tbTotalAllowancesAmount.Text = allowanceAmount.ToString("n2");
            tbTotalDeductionsAmount.Text = deductionAmount.ToString("n2");

            Gujjar.AddDatagridviewButton(dgvDeductions, dgvdeductiondelbtn, "Delete", "Delete", 80);
            Gujjar.AddDatagridviewButton(dgvAllowances, dgvallowancedelbtn, "Delete", "Delete", 80);
            UpdateGrossNetPayment();
            dtp.MinDate = appSett.StartDate;
            dtp.MaxDate = appSett.EndDate;
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait = new WaitForm(LoadInitData);
                wait.ShowDialog();

                BindFormData();
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

        private void bswIsBank_Click(object sender, EventArgs e)
        {
            
        }

        private void cbAllowances_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbAllowances.SelectedIndex == -1)
            {
                tbAllowanceAmount.Text = "0.0";
                return;
            }

            SalaryAllowance vm = cbAllowances.SelectedItem as SalaryAllowance;
            tbAllowanceAmount.Text = vm.Amount.ToString("n1");
        }

        private void cbDeductions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbDeductions.SelectedIndex == -1)
            {
                tbDeductionAmount.Text = "0.0";
                return;
            }

            SalaryDeduction vm = cbDeductions.SelectedItem as SalaryDeduction;
            tbDeductionAmount.Text = vm.Amount.ToString("n1");
        }

        private void btnAddDeduction_Click(object sender, EventArgs e)
        {
            try
            {
                AddDeductionForm form = new AddDeductionForm();
                form.ShowDialog();

                int did = form.DeductionId;
                if(did != 0)
                {
                    WaitForm wait = new WaitForm(LoadCbDeductions);
                    wait.ShowDialog();
                    BindCbDeductions();

                    cbDeductions.SelectedItem = cbDeductions.Items.OfType<SalaryDeduction>()
                        .FirstOrDefault(a => a.Id == did);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddAllowance_Click(object sender, EventArgs e)
        {
            try
            {
                AddAllowanceForm form = new AddAllowanceForm();
                form.ShowDialog();

                int did = form.AllowanceId;
                if (did != 0)
                {
                    WaitForm wait = new WaitForm(LoadCbAllowances);
                    wait.ShowDialog();
                    BindCbAllowances();

                    cbAllowances.SelectedItem = cbAllowances.Items.OfType<SalaryAllowance>()
                        .FirstOrDefault(a => a.Id == did);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddDeductionAmount_Click(object sender, EventArgs e)
        {
            try
            {
                if(cbDeductions.SelectedIndex == -1)
                {
                    throw new Exception("Please select deduction title");
                }

                SalaryDeduction salaryDeduction = cbDeductions.SelectedItem as SalaryDeduction;
                if(salaryDeduction.Title == "N/A")
                {
                    throw new Exception("Invalid salary deduction title");
                }

                var obj = deductionVMBindingSource.List.OfType<DeductionVM>()
                    .FirstOrDefault(a => a.Id == salaryDeduction.Id);
                if(obj != null)
                {
                    throw new Exception("Deduction title already added in the list");
                }
                decimal amount = tbDeductionAmount.Text.ToDecimal();
                string confMessage = string.Format("Are you sure.\nYou want to add deduction title {0}\nAmount {1}.\nPlease confirm..!!",
                    salaryDeduction.Title, amount.ToString("n1"));
                DialogResult res = Gujjar.ConfirmYesNo(confMessage);

                if (res == DialogResult.No)
                    return;

                DeductionVM dvm = new DeductionVM
                {
                    Id = salaryDeduction.Id,
                    Amount = amount,
                    Title = salaryDeduction.Title
                };
                deductionVMBindingSource.List.Add(dvm);
                dgvDeductions.Refresh();

                UpdateTotalDeductions();
                UpdateGrossNetPayment();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void UpdateTotalDeductions()
        {
            var list = deductionVMBindingSource.List.OfType<DeductionVM>().ToList();

            tbTotalDeductionsAmount.Text = list.Sum(a => a.Amount).ToString("n2");
        }

        private void UpdateGrossNetPayment()
        {
            try
            {
                decimal basicSalary = 0;
                decimal advancesAmount = 0;
                decimal allDeductionAmount = 0;
                decimal totalAllowanceAmount = 0;
                decimal grossSalary = 0;
                decimal netSalary = 0;

                if(!string.IsNullOrEmpty(tbBasicSalary.Text))
                {
                    basicSalary = tbBasicSalary.Text.ToDecimal();
                }
                if(!string.IsNullOrEmpty(tbAdvancesAmount.Text))
                {
                    advancesAmount = tbAdvancesAmount.Text.ToDecimal();
                }
                if(!string.IsNullOrEmpty(tbTotalDeductionsAmount.Text))
                {
                    allDeductionAmount = tbTotalDeductionsAmount.Text.ToDecimal();
                }
                if(!string.IsNullOrEmpty(tbTotalAllowancesAmount.Text))
                {
                    totalAllowanceAmount = tbTotalAllowancesAmount.Text.ToDecimal();
                }

                grossSalary = basicSalary + totalAllowanceAmount;
                netSalary = grossSalary - advancesAmount - allDeductionAmount;

                tbGrossSalary.Text = grossSalary.ToString("n1");
                tbNetSalary.Text = netSalary.ToString("n1");

                tbTransactionAmount.Text = (basicSalary - allDeductionAmount + totalAllowanceAmount).ToString("n1");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dgvDeductions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                int ci = e.ColumnIndex;

                if (ri == -1 || ri == dgvDeductions.NewRowIndex)
                    return;

                if(dgvDeductions.Columns[dgvdeductiondelbtn].Index == ci)
                {
                    DialogResult res = Gujjar.ConfirmYesNo("Are you sured to delete..!!");
                    if (res == DialogResult.No)
                        return;

                    int id = dgvDeductions.Rows[ri].Cells[0].Value.ToInt();
                    var obj = deductionVMBindingSource.List.OfType<DeductionVM>().FirstOrDefault(a => a.Id == id);
                    deductionVMBindingSource.List.Remove(obj);
                    dgvDeductions.Refresh();

                    UpdateTotalDeductions();
                    UpdateGrossNetPayment();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddAllowanceAmount_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbAllowances.SelectedIndex == -1)
                {
                    throw new Exception("Please select allowance title");
                }

                SalaryAllowance salaryAllowance = cbAllowances.SelectedItem as SalaryAllowance;
                if (salaryAllowance.Title == "N/A")
                {
                    throw new Exception("Invalid salary allowance title");
                }

                var obj = allowanceVMBindingSource.List.OfType<AllowanceVM>()
                    .FirstOrDefault(a => a.Id == salaryAllowance.Id);
                if (obj != null)
                {
                    throw new Exception("Allowance title already added in the list");
                }
                decimal amount = tbAllowanceAmount.Text.ToDecimal();
                string confMessage = string.Format("Are you sure.\nYou want to add allowance title {0}\nAmount {1}.\nPlease confirm..!!",
                    salaryAllowance.Title, amount.ToString("n1"));
                DialogResult res = Gujjar.ConfirmYesNo(confMessage);

                if (res == DialogResult.No)
                    return;

                AllowanceVM dvm = new AllowanceVM
                {
                    Id = salaryAllowance.Id,
                    Amount = amount,
                    Title = salaryAllowance.Title
                };
                allowanceVMBindingSource.List.Add(dvm);
                dgvAdvances.Refresh();

                UpdateTotalAllowances();
                UpdateGrossNetPayment();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void UpdateTotalAllowances()
        {
            tbTotalAllowancesAmount.Text = allowanceVMBindingSource.List.OfType<AllowanceVM>()
                .Sum(a => a.Amount).ToString("n1");
        }

        private void dgvAllowances_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                int ci = e.ColumnIndex;

                if (ri == -1 || ri == dgvAllowances.NewRowIndex)
                    return;

                if(dgvAllowances.Columns[dgvallowancedelbtn].Index == ci)
                {
                    DialogResult res = Gujjar.ConfirmYesNo("Are you sured to delete.. Please confirm .. !!");
                    if (res == DialogResult.No)
                        return;

                    int id = dgvAllowances.Rows[ri].Cells[0].Value.ToInt();

                    var obj = allowanceVMBindingSource.List.OfType<AllowanceVM>().FirstOrDefault(a => a.Id == id);
                    allowanceVMBindingSource.List.Remove(obj);

                    dgvAllowances.Refresh();

                    UpdateTotalAllowances();
                    UpdateGrossNetPayment();
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
                decimal basicSalary = tbBasicSalary.Text.ToDecimal();
                decimal advancesAmount = tbAdvancesAmount.Text.ToDecimal();
                decimal totalDeductions = tbTotalDeductionsAmount.Text.ToDecimal();
                decimal totalAllowances = tbTotalAllowancesAmount.Text.ToDecimal();
                decimal grossSalary = tbGrossSalary.Text.ToDecimal();
                decimal netSalary = tbNetSalary.Text.ToDecimal();
                decimal amountTransfered = tbTransactionAmount.Text.ToDecimal();
                
                string msg = string.Format("PayRoll credit. Employee ({0}), pay month/year ({1}), paid date ({2}), basic salary ({3}), advances ({4}), deductions ({5}), allowances ({6}), gross salary ({7}), net salary ({8}), remakrs ({9}), amount transfered ({10}), by ({11})",
                    employee.Name, tbSalaryMonthYear.Text, dtp.Value.ToShortDateString(), employee.Salary.ToString("n1"), tbAdvancesAmount.Text, tbTotalDeductionsAmount.Text, tbTotalAllowancesAmount.Text, tbGrossSalary.Text, tbNetSalary.Text, tbRemarks.Text, amountTransfered.ToString("n1"), appUser.Name);

                DialogResult res = Gujjar.ConfirmYesNo(string.Format("{0}\n\nAre you sured..\nPlease confirm..!!", msg));
                if (res == DialogResult.No)
                    return;

                EmployPayRollEntry epentry = new EmployPayRollEntry
                {
                    Id = 0,
                    ActionDate = dtp.Value,
                    AllowanceAmount = totalAllowances,
                    DeductionAmount = totalDeductions,
                    EmpAdvances = advanceAmount,
                    Employee = null,
                    EmployeeId = employeeId,
                    EmployeeSalary = employee.Salary,
                    GrossSalary = grossSalary,
                    IsPaid = true,
                    Month = salaryMonth,
                    NetSalary = netSalary,
                    Remarks = tbRemarks.Text,
                    Unum = Helper.Unum,
                    Year = salaryYear
                };

                var allowances = allowanceVMBindingSource.List.OfType<AllowanceVM>().ToList();
                foreach (var allowItem in allowances)
                {
                    EmployeeSalaryEntryAllowance obj1 = new EmployeeSalaryEntryAllowance
                    {
                        Amount = allowItem.Amount,
                        SalaryAllowanceId = allowItem.Id
                    };
                    epentry.EmployeeSalaryEntryAllowances.Add(obj1);
                }

                var deductions2 = deductionVMBindingSource.List.OfType<DeductionVM>().ToList();
                foreach (var item in deductions2)
                {
                    EmployeeSalaryEntryDeduction obj2 = new EmployeeSalaryEntryDeduction
                    {
                        Amount = item.Amount,
                        SalaryDeductionId = item.Id
                    };
                    epentry.EmployeeSalaryEntryDeductions.Add(obj2);
                }

                
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            epentry = db.PayRollEntries.Add(epentry);
                            db.SaveChanges();

                            var dbEntry = db.PayRollEntries.Find(epentry.Id);
                            string str = Gujjar.ToXML<EmployPayRollEntry>(dbEntry);
                            dbEntry.XStr = str;

                            db.Entry(dbEntry).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            decimal amount = amountTransfered;
                            #region "Financials"

                            GeneralAccount debitAccount = db.Accounts.Find(Properties.Resources.SalariesAccount) as GeneralAccount;
                            GeneralAccount creditAccount = db.Accounts.Find(employee.GeneralAccountId) as GeneralAccount;
                           

                            DayBook daybookEntry = new DayBook
                            {
                                Id = 0,
                                Amount = amount,
                                Date = dtp.Value,
                                Description = msg,
                                CanRollBack = true,
                                InDate = DateTime.Now.Date
                            };

                            daybookEntry = db.DayBooks.Add(daybookEntry);
                            db.SaveChanges();

                            #region "Debit transaction"

                            AccountTransaction debitItemTrans = new AccountTransaction
                            {
                                Id = 0,
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Debit,
                                Balance = amount,
                                CreditAmount = 0,
                                Date = dtp.Value,
                                DayBookId = daybookEntry.Id,
                                DebitAmount = amount,
                                GeneralAccountId = debitAccount.Id,
                                Description = msg
                            };

                            var debitDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == debitAccount.Id).AsParallel()
                                .ToList().Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date)
                                .OrderByDescending(a => a.Id).FirstOrDefault();

                            if (debitDbEntry != null)
                            {
                                debitItemTrans.Balance += debitDbEntry.Balance;
                            }

                            debitItemTrans = db.AccountTransactions.Add(debitItemTrans);
                            db.SaveChanges();



                            #endregion

                            #region  "Credit transaction"
                            AccountTransaction creditItemTrans = new AccountTransaction
                            {
                                Id = 0,
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Credit,
                                Balance = amount,
                                CreditAmount = amount,
                                Date = DateTime.Now,
                                DayBookId = daybookEntry.Id,
                                DebitAmount = 0,
                                GeneralAccountId = creditAccount.Id,
                                Description = msg
                            };

                            var creditDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == creditAccount.Id).AsParallel()
                                .ToList().Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date)
                                .OrderByDescending(a => a.Id).FirstOrDefault();

                            if (creditDbEntry != null)
                            {
                                creditItemTrans.Balance += creditDbEntry.Balance;
                            }

                            creditItemTrans = db.AccountTransactions.Add(creditItemTrans);
                            db.SaveChanges();
                            #endregion

                            var daybookdb = db.DayBooks.Find(daybookEntry.Id);
                            daybookdb.DebitTrace = string.Format("({0}). Trans Id: {1}", debitAccount.Title, debitItemTrans.Id);
                            daybookdb.CreditTrace = string.Format("({0}). Trans Id: {1}", creditAccount.Title, creditItemTrans.Id);
                            daybookdb.CreditAccountId = creditAccount.Id;
                            daybookdb.DebitAccountId = debitAccount.Id;

                            db.Entry(daybookdb).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            #endregion

                            trans.Commit();
                            if(appSett.PrintFinancialTransactions)
                            {
                                pDaybooks.Add(daybookdb);
                                Helper.PrintTransactions(pDaybooks);
                            }
                            Gujjar.InfoMsg(string.Format("Amount ({0}) has been credited to employee ({1})'s account successfully", amountTransfered.ToString("n1"), employee.Name));
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
        private List<DayBook> pDaybooks = new List<DayBook>();
    }
}
