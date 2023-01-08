using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ReadyStuff.ViewModel
{
    public class ReadyDealVM
    {
        public int Id { get; set; }
        public string Broker { get; set; }

        [DisplayName("Deal Item")]
        public string ReadyItem { get; set; }

        [DisplayName("Vehicles")]
        public int NoOfVehicles { get; set; }

        [DisplayName("Total Weight")]
        public decimal TotalWeight { get; set; }

        [DisplayName("Deal Date")]
        public string DealDate { get; set; }

        [DisplayName("Completed")]
        public bool IsCompleted { get; set; }

        [DisplayName("Status")]
        public string CompletionStatus { get; set; }

        public string State { get; set; }
        public string Rate { get; set; }

    }
}
