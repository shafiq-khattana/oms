using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Model.Employees.ViewModel
{
    public class PayRollEntryVM
    {
        public int Id { get; set; }
        public string Employee { get; set; }

        [DisplayName("Salary Month")]
        public string SalaryMonthYear { get; set; }


        public decimal Salary { get; set; }
        public decimal Allowances { get; set; }

        [DisplayName("Gross Payment")]
        public decimal GrossSalary { get; set; }

        public decimal Advances { get; set; }
        public decimal Deductions { get; set; }

        [DisplayName("Net Payment")]
        public decimal NetSalary { get; set; }

        [DisplayName("Status")]
        public bool IsPaid { get; set; }

        [DisplayName("Paid Date")]
        public string PaidDate { get; set; }
        public string Remarks { get; set; }

        [Browsable(false)]
        public int EmpId { get; set; }

        [Browsable(false)]
        public int Month { get; set; }

        [Browsable(false)]
        public int Year { get; set; }
    }
}
