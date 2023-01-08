using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Model
{
    public class EmployPayRollEntry
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string SalaryMonthYear
        {
            get
            {
                return string.Format("{0}/{1}", Month, Year);
            }
        }
        public decimal EmployeeSalary { get; set; }
        public string Unum { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? ActionDate { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public decimal AllowanceAmount { get; set; }
        public decimal DeductionAmount { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal NetSalary { get; set; }
        public string Remarks { get; set; }
        public string XStr { get; set; }
        public decimal EmpAdvances { get; set; }
        
        public List<EmployeeSalaryEntryAllowance> EmployeeSalaryEntryAllowances { get; set; }

        public List<EmployeeSalaryEntryDeduction> EmployeeSalaryEntryDeductions { get; set; }

        public decimal FinancialAmount
        {
            get
            {
                return EmployeeSalary + AllowanceAmount - DeductionAmount;
            }
        }
        public EmployPayRollEntry()
        {
            EmployeeSalaryEntryAllowances = new List<EmployeeSalaryEntryAllowance>();
            EmployeeSalaryEntryDeductions = new List<EmployeeSalaryEntryDeduction>();
        }
    }
}
