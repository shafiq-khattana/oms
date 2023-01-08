using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.ViewModel
{
    public class AdvanceItemRecordVM
    {
        public int Id { get; set; }
        public decimal Qty { get; set; }
        public string Dated { get; set; }
        public string Remarks { get; set; }        
        public string Place { get; set; }

        [Browsable(false)]
        public int RepPlaceId { get; set; }
    }
    public class DispatchItemRecordVM
    {
        public int Id { get; set; }
        public decimal Qty { get; set; }
        public string Dated { get; set; }
        public string Remarks { get; set; }
        public string Place { get; set; }

        [Browsable(false)]
        public int RepPlaceId { get; set; }
    }
}
