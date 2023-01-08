using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Financials.ViewModel
{
    public class DayBookVM
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }

        [DisplayName("Debit Trace")]
        public string DebitTrace { get; set; }

        [DisplayName("Credit Trace")]
        public string CreditTrace { get; set; }
        public string Amount { get; set; }

        [Browsable(false)]
        public bool RB { get; set; }

        [Browsable(false)]
        public bool IsReversed { get; set; }
    }
}
