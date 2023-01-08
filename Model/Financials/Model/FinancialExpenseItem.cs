using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Financials.Model
{
    public class ExpenseItem
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ExpenseType ExpenseType { get; set; }
        public GeneralAccount Account { get; set; }
        public string GeneralAccountId { get; set; }

        public List<ExpenseItemEntry> ExpenseEntries { get; set; }

        public ExpenseItem()
        {
            ExpenseEntries = new List<ExpenseItemEntry>();
        }
    }

    public enum ExpenseType
    {
        Financial,
        General
    }
    public class ExpenseItemEntry
    {
        public int Id { get; set; }

        public int ExpenseItemId { get; set; }
        public ExpenseItem ExpenseItem { get; set; }
        public decimal ExpenseAmount { get; set; }

        public DateTime ExpenseDate { get; set; }
        public string Remarks { get; set; }
        public string Description { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public int DayBookId { get; set; }
        public string CreditAccountId { get; set; }
    }
}
