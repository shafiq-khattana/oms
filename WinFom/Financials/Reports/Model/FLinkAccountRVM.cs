using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.Financials.Reports.Model
{
    public class FLinkAccountRVM
    {
        public int No { get; set; }
        public string Title { get; set; }
        public decimal Balance { get; set; }
        public string Type { get; set; }
        public List<FTransactionRVM> Transactions { get; set; }

        public FLinkAccountRVM()
        {
            Transactions = new List<FTransactionRVM>();
        }
    }
}
