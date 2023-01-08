using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.OilDirtStuff.Reports.RepModel
{
    public class OilDirtSaleRVM
    {
        public string DealSchedule { get; set; }
        public string Broker { get; set; }
        public string Selector { get; set; }
        public string Driver { get; set; }
        public string VehicleNo { get; set; }
        public string VehicleWeight { get; set; }
        public string LoadedQtyKg { get; set; }
        public string WeighBridge { get; set; }
        public string WeighBridgeWeight { get; set; }
        public string WeightInDifference { get; set; }
        public string PerMondWeight { get; set; }
        public string PerMondRate { get; set; }
        public string TotalMonds { get; set; }
        public string TotalPrice { get; set; }
        public string BrokerSharePercentage { get; set; }
        public string BrokerShareAmount { get; set; }
        public string TotalReceivableAmount { get; set; }
    }
}
