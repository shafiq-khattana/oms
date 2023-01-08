using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Financials.ViewModel
{
    public class ExpenseEntryVM
    {
        public int Id { get; set; }

        [DisplayName("Expense Item")]
        public string ExpenseItem { get; set; }

        [DisplayName("Amount")]
        public string ExpenseAmount { get; set; }

        [DisplayName("Date")]
        public string ExpenseDate { get; set; }
        public string Remarks { get; set; }
        public string Description { get; set; }

        [DisplayName("D.B")]
        public int DayBookId { get; set; }
    }
}
