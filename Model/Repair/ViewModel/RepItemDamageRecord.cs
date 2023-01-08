using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.ViewModel
{
    public class RepItemDamageRecordVM
    {
        public int Id { get; set; }
        public decimal Qty { get; set; }
        public string Dated { get; set; }
        public string Remarks { get; set; }
    }
}
