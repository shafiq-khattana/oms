using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.OilDealManaged.Reports.Model
{
    public class RCOSchRVM
    {
        public string SchNo { get; set; }
        public string Broker { get; set; }
        public string Selector { get; set; }
        public string Driver { get; set; }
        public string Vehicle { get; set; }
        public decimal VehicleEmptyWeight { get; set; }
        public decimal LoadedQty { get; set; }
        public decimal WeighBridgeWeight { get; set; }
        public string WeighBridge { get; set; }
        public string TradeUnit { get; set; }
        public decimal PerTradeUnit { get; set; }
        public decimal PerTURate { get; set; }
        public decimal TotalTUs { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal BrokerSharePercentage { get; set; }
        public decimal BrokerShareAmount { get; set; }
        public decimal NetPrice { get; set; }
        public byte[] DriverPic { get; set; }
        public byte[] SelectorPic { get; set; }
        public byte[] DriverBioMet { get; set; }
        public byte[] SelectorBioMet { get; set; }
        public string Dated { get; set; }
        public string ServedBy { get; set; }
        public string SelectorNIC { get; set; }
        public string DriverNIC { get; set; }
    }
}
