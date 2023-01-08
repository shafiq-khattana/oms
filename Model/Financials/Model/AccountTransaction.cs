using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Financials.Model
{
    public class AccountTransaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [Browsable(false)]
        public int DayBookId { get; set; }
        public string Description { get; set; }

        [DisplayName("Debit")]
        public decimal DebitAmount { get; set; }

        [DisplayName("Credit")]
        public decimal CreditAmount { get; set; }

        public decimal Balance { get; set; }

        [Browsable(false)]
        public AccountTransactionType AccountTransactionType { get; set; }

        [Browsable(false)]
        public string GeneralAccountId { get; set; }

        [ForeignKey("GeneralAccountId")]
        public GeneralAccount Account { get; set; }
        
    }
    public enum AccountTransactionType
    {
        Debit,
        Credit
    }
}
