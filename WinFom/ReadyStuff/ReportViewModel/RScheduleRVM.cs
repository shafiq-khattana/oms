using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.ReadyStuff.ReportViewModel
{
    public class RScheduleRVM
    {
        public string SrNo { get; set; }
        public string DateTime { get; set; }
        public string Broker { get; set; }
        public string VehicleNo { get; set; }
        public string Driver { get; set; }
        public string Selector { get; set; }
        public decimal VehicleWeightEmpty { get; set; }
        public decimal LoadedQty { get; set; }
        public decimal VehicleWeightFull { get; set; }
        public decimal WeighBridgeWeight { get; set; }
        public decimal MondsUnit { get; set; }
        public decimal TotalMonds { get; set; }
        public decimal PerMondRate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal BrokerSharePercentage { get; set; }
        public decimal BrokerAmount { get; set; }
        public decimal EffectivePrice { get; set; }
        public List<RPackingRVM> RPackingRVMList { get; set; }
    }
}
