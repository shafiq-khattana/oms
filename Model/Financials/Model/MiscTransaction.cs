using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Financials.Model
{
    public class MiscTransaction
    {
        public int Id { get; set; }
        public string debitAccountId { get; set; }

        [ForeignKey("debitAccountId")]
        public GeneralAccount DebitAccount { get; set; }
        public string creditAccountId { get; set; }

        [ForeignKey("creditAccountId")]
        public GeneralAccount CreditAccount { get; set; }
        public decimal Amount { get; set; }
        public DateTime Dated { get; set; }
        public string Description { get; set; }
    }
}
