using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.Financials.Reports.Model
{
    public class FAccountSummaryRVM
    {
        public decimal AccountBalance { get; set; }
        public decimal LinkBalance { get; set; }
        public decimal EffectiveBalance { get; set; }
    }
}
