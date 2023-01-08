using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ReadyStuff.ViewModel
{
    public class OilDealVM1
    {
        [DisplayName("Deal")]
        public int Id { get; set; }

        public string Broker { get; set; }

        public string Item { get; set; }

        [DisplayName("Deal Date")]
        public string DealDate { get; set; }

        [DisplayName("Ready Date")]
        public string ReadyDate { get; set; }

        [DisplayName("Comp Date")]
        public string CompleteDate { get; set; }

        [DisplayName("Deal Qty")]
        public string DealQty { get; set; }

        public string Status { get; set; }



        [Browsable(false)]
        public string Selector { get; set; }



        [Browsable(false)]
        public string Driver { get; set; }
        [Browsable(false)]
        public string VehicleNo { get; set; }

        [Browsable(false)]
        public string VehicleEmptyWeight { get; set; }

        [Browsable(false)]
        public string VehicleFullWeight { get; set; }

        [Browsable(false)]
        public string VehicleWeightDifference { get; set; }

        [Browsable(false)]
        public string WeighBridgeWeight { get; set; }

        [Browsable(false)]
        public string BrokerSharePercentage { get; set; }

        [Browsable(false)]
        public string BrokerShareAmount { get; set; }

        [Browsable(false)]
        public string TradeUnit { get; set; }

        [Browsable(false)]
        public string PerTradeUnitQty { get; set; }

        [Browsable(false)]
        public string TotalTradeUnits { get; set; }

        [DisplayName("Rate")]
        public string PerTradeUnitPrice { get; set; }

        [Browsable(false)]
        public string TotalPrice { get; set; }

        [Browsable(false)]
        public string NetPrice { get; set; }

        [Browsable(false)]
        public string VehicleLoadedQty { get; set; }

    }
}
