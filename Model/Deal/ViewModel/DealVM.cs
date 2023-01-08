using Model.Deal.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Deal.ViewModel
{
    public class DealVM
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Broker { get; set; }
        public string Item { get; set; }

        [DisplayName("Packing Units")]
        public string PackingUnits { get; set; }

        [DisplayName("Trade Units")]
        public string TradeUnits { get; set; }

        [DisplayName("Total Qty")]
        public string TotalQty { get; set; }

        [DisplayName("Total Price")]
        public string TotalPrice { get; set; }

        [DisplayName("Deal Date")]
        public string Date { get; set; }

        [Browsable(false)]
        public int TotalSchedules { get; set; }

        [Browsable(false)]
        public int ReceivedSchedules { get; set; }

        [Browsable(false)]
        public int RemainingSchedules
        {
            get
            {
                return TotalSchedules - ReceivedSchedules;
            }
               
        }

        [Browsable(false)]
        public decimal LoadedQty { get; set; }

        [Browsable(false)]
        public decimal ActualReceivedQty { get; set; }

        [Browsable(false)]
        public decimal QtyDifference
        {
            get
            {
                return LoadedQty - ActualReceivedQty;
            }
        }


        [Browsable(false)]
        public string Loss { get; set; }

        [Browsable(false)]
        public string LossPercentage { get; set; }

        [DisplayName("Completed")]
        public bool IsCompleted { get; set; }

        [DisplayName("Deal Status")]
        public string DealStatus { get; set; }

        [DisplayName("Comp Status")]
        public string CompletionStatus { get; set; }
        public string Rate { get; set; }
    }
}
