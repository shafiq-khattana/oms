using Model.Repair.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.ViewModel
{
    public class RepairEntryVM2
    {
        public string Id { get; set; }

        [DisplayName("Item")]
        public string RepItem { get; set; }

        [Browsable(false)]
        public int RepItemId { get; set; }

        [DisplayName("Disp.Qty")]
        public decimal DispatchingQty { get; set; }

        public decimal Weight { get; set; }

        [DisplayName("T.O.Person")]
        public string TakingOutPerson { get; set; }

        public string RepPlace { get; set; }
        public int RepPlaceId { get; set; }
        public DateTime DispatchDate { get; set; }
        public DateTime? ReceivingDate { get; set; }
        public string DispatchingRemarks { get; set; }
        public string ReceivingRemarks { get; set; }
        
    }
}
