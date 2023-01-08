using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Model
{
    public class SalaryAllowance
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public List<EmployeeSalaryEntryAllowance> EmployeeSalaryEntryAllowances { get; set; }
        public SalaryAllowance()
        {
            EmployeeSalaryEntryAllowances = new List<EmployeeSalaryEntryAllowance>();
        }
    }
}
