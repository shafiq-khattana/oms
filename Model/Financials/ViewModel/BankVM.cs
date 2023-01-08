using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Financials.ViewModel
{
    public class BankAccountVM
    {
        public string Id { get; set; }

        [DisplayName("Bank")]
        public string BankName { get; set; }

        [DisplayName("Address")]
        public string BankAddress { get; set; }

        [DisplayName("Account Title")]
        public string AccountTitle { get; set; }

        [DisplayName("Account No")]
        public string AccountNo { get; set; }
        public string Description { get; set; }
        public string Balance { get; set; }
    }
}
