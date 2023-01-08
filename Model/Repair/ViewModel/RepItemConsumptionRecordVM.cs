using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.ViewModel
{
    public class RepItemConsumptionRecordVM
    {
        public int Id { get; set; }
        public string Item { get; set; }

        [Browsable(false)]
        public int RepItemId { get; set; }

        [DisplayName("Qty")]
        public decimal QtyConsumed { get; set; }


        public decimal Price { get; set; }
        public string Dated { get; set; }
        public string Remarks { get; set; }

        [Browsable(false)]
        public int DaybookId { get; set; }

        [Browsable(false)]        
        public string AccountId { get; set; }
    }
}
