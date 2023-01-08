using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Financials.ViewModel
{
    public class TransactionsVM
    {
        public int Id { get; set; }
        public string Date { get; set; }

        [DisplayName("D.B")]
        public int DayBookId { get; set; }
        public string Description { get; set; }

        [DisplayName("Debit")]
        public string DebitAmount { get; set; }

        [DisplayName("Credit")]
        public string CreditAmount { get; set; }

        [Browsable(false)]
        public string Balance { get; set; }

        
        
    }
}
