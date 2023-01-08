using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.ReadyStuff.ReportViewModel
{
    public class TopHeader
    {
        public byte[] QrCode { get; set; }
        public byte[] Barcode { get; set; }
        public string Dated { get; set; }
        public byte[] Logo { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyAddress { get; set; }
        public string Broker { get; set; }
        public string Item { get; set; }
    }

    public class Persons
    {
        public byte[] SelectorPic { get; set; }
        public byte[] SelectorThumb { get; set; }
        public byte[] DriverPic { get; set; }
        public byte[] DriverThumb { get; set; }
        public string SelectorName { get; set; }
        public string DriverName { get; set; }
        public string DealSchedule { get; set; }
        public string Dated { get; set; }
        public string DriverCNIC { get; set; }
        public string SelectorCNIC { get; set; }
    }

    public class TradeBroker
    {
        public string TradeUnitName { get; set; }
        public string TradeUnitQty { get; set; }
        public decimal TotalTradeUnits { get; set; }
        public decimal PerTradeUnitPrice { get; set; }
        public decimal TotalPrice
        {
            get
            {
                return TotalTradeUnits * PerTradeUnitPrice;
            }
        }
        public decimal BrokerSharePercentage { get; set; }
        public decimal BrokerShareAmount { get; set; }
        public decimal NetPrice { get; set; }
        public decimal WeighBridgeWeight { get; set; }
        public string WeighBridge { get; set; }
        public string VehicleNo { get; set; }
        public string VehicleEmptyWeight { get; set; }
        public string VehicleFullWeight { get; set; }
    }
}
