using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.Deal.Reports.RepModel
{
    public class RecSchRVM
    {
        public string DealScheduleNo { get; set; }
        public string ConttonFactory { get; set; }
        public int NoOfBori { get; set; }
        public string SchDispatchDate { get; set; }
        public string SchReceivedDate { get; set; }
        public string Selector { get; set; }
        public string Broker { get; set; }
        public decimal SchWeight { get; set; }
        public decimal LoadedWeight { get; set; }
        public decimal ReceivedWeight { get; set; }
        public decimal LossInWeight { get; set; }
        public decimal SchMonds { get; set; }
        public decimal LoadedMonds { get; set; }
        public decimal LoadedPricePaidAmount { get; set; }
        public decimal ReceivedMonds { get; set; }
        public decimal LossInMonds { get; set; }
        public decimal LossInCash { get; set; }
        public string Driver { get; set; }
        public string VehicleNO { get; set; }
        public decimal VehicleFare { get; set; }
        public string GoodsCompany { get; set; }
        public string LoadingWeighBridge { get; set; }
        public string ReceingWeightBridge { get; set; }
        public decimal PerMondPriceRate { get; set; }
    }

    public class RecSchSummaryRVM
    {
        public string TotalSchWeight { get; set; }
        public string TotalSchMonds { get; set; }
        public string TotalSchPrice { get; set; }

        public string TotalLoadedWeight { get; set; }
        public string TotalLoadedMonds { get; set; }
        public string TotalLoadedPrice { get; set; }

        public string TotalReceivedWeight { get; set; }
        public string TotalReceivedMonds { get; set; }
        public string TotalReceivedPrice { get; set; }

        public string TotalLossInWeight { get; set; }
        public string TotalLossInMonds { get; set; }
        public string TotalLossInPrice { get; set; }

    }

    public class RecCompRVM
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string RepDate { get; set; }
        public string RepTitle { get; set; }
        public byte[] Logo { get; set; }
        public byte[] Qrcode { get; set; }
        public byte[] Barcode { get; set; }
    }
}
