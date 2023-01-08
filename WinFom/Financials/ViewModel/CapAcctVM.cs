using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.Financials.ViewModel
{
    public class CapAcctVM
    {
        [DisplayName("Id")]
        public string AcctId { get; set; }
        public string Title { get; set; }

        [DisplayName("Capital Amount")]
        public decimal DepositedAmount { get; set; }

        [DisplayName("Share-(%)")]
        public float SharePercentage { get; set; }

        [DisplayName("Share")]
        public decimal SharedAmount { get; set; }

        [DisplayName("Deduct (%)")]
        public float DeductionRatio { get; set; }

        [DisplayName("Deduct Amount")]
        public float DeductionAmount { get; set; }

        [DisplayName("Total Profit")]
        public decimal TotalProfit { get; set; }
    }
}
