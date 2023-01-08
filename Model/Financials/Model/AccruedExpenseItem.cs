using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Financials.Model
{
    public class AccruedExpenseItem
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string DebitAccountId { get; set; }

        [ForeignKey("DebitAccountId")]
        public GeneralAccount DebitAccount { get; set; }

        public string CreditAccountId { get; set; }

        [ForeignKey("CreditAccountId")]
        public GeneralAccount CreditAccount { get; set; }
    }
}
