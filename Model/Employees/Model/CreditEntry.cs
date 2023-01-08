using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Model
{
    public class CreditEntry
    {
        
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public int DayBookId { get; set; }
    }
}
