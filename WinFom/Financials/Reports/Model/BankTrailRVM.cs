using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.Financials.Reports.Model
{
    public class BankTrailRVM
    {
        public string Bank { get; set; }
        public string AccountNo { get; set; }
        public decimal Balance { get; set; }
    }
}
