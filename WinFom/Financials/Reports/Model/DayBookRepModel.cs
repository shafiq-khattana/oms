using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.Financials.Reports.Model
{
    public class DayBookRepModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string DebitTrace { get; set; }
        public string CreditTrace { get; set; }
        public decimal Amount { get; set; }
    }
}
