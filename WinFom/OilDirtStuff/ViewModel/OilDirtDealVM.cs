using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.OilDirtStuff.ViewModel
{
    public class OilDirtDealVM
    {
        public int Id { get; set; }
        public string Broker { get; set; }

        [DisplayName("Deal Item")]
        public string Item { get; set; }

        [DisplayName("Vehicles")]
        public int NoOfVehicles { get; set; }

        

        [DisplayName("Deal Date")]
        public string DealDate { get; set; }

        [DisplayName("Ready Date")]
        public string ReadyDate { get; set; }


        [DisplayName("Trade Unit")]
        public string TradeUnit { get; set; }
        public string Rate { get; set; }

        [DisplayName("Status")]
        public string CompletionStatus { get; set; }

        public string State { get; set; }

        
    }
}
