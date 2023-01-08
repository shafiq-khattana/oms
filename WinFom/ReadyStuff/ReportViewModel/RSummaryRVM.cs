using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.ReadyStuff.ReportViewModel
{
    public class RSummaryRVM
    {
        public decimal TotalLoadedWeight { get; set; }
        public decimal TotalWeighBridgeWeight { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal BrokerShareAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TotalPackings { get; set; }
    }
}
