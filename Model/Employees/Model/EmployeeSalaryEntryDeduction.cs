using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Model
{
    public class EmployeeSalaryEntryDeduction
    {
        public int Id { get; set; }
        public int EmployPayRollEntryId { get; set; }
        public EmployPayRollEntry EmployPayRollEntry { get; set; }

        public int SalaryDeductionId { get; set; }
        public SalaryDeduction SalaryDeduction { get; set; }
        public decimal Amount { get; set; }
    }
}
