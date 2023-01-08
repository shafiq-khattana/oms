using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.RepairUI.Reports.Model
{
    public class DispatchItemDetail
    {
        public int SrNo { get; set; }
        public string Item { get; set; }
        public decimal Qty { get; set; }
        public string Remarks { get; set; }
    }
}
