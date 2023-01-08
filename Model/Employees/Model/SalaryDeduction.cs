using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Model
{
    public class SalaryDeduction
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public List<EmployeeSalaryEntryDeduction> EmployeeSalaryEntryDeductions { get; set; }

        public SalaryDeduction()
        {
            EmployeeSalaryEntryDeductions = new List<EmployeeSalaryEntryDeduction>();
        }
    }
}
