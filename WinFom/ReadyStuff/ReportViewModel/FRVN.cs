using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.ReadyStuff.ReportViewModel
{
    public class FRVN
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public byte[] Logo { get; set; }
    }
    public class CRVN
    {
        public string Driver { get; set; }
        public string Selector { get; set; }
        public string Vehicle { get; set; }
        public string Dated { get; set; }
        public byte[] Uan { get; set; }
        public byte[] QrCode { get; set; }
        public string User { get; set; }
        public string Item { get; set; }
        public string Broker { get; set; }
        public string OilQty { get; set; }
        public string EmptyVehicleWeight { get; set; }
    }
    public class PackingRVN
    {
        public string Packing { get; set; }
        public decimal Qty { get; set; }
        public decimal Total { get; set; }
    }
}
