using Model.Repair.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Model.Repair.ViewModel
{
    public class RepairEntryVMDispatch
    {
        public int Id { get; set; }      

        public string Category { get; set; }
        [DisplayName("Item")]
        public string RepItem { get; set; }        

        [DisplayName("Work Place")]
        public string WorkingPlace { get; set; }
        [DisplayName("Qty")]
        public decimal DispatchingQty { get; set; }
        public string Remarks { get; set; }

        [Browsable(false)]
        public int PlaceId { get; set; }
    }
}
