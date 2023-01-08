using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Deal.ViewModel
{
    public class ScheduleVM
    {
        public string Id { get; set; }
        [DisplayName("Trade Units")]
        public string TradeUnits { get; set; }

        [DisplayName("Packings")]
        public string PackingUnits { get; set; }
        public string Price { get; set; }

        [DisplayName("Ready Date")]
        public string ExpectedDate { get; set; }

        [DisplayName("Total Qty")]
        public string TotalSubUnits { get; set; }

        public string Remarks { get; set; }
    }
}
