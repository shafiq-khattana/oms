using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Deal.ViewModel
{
    public class AddPackingVM
    {
        public string Id { get; set; }
        public decimal Qty { get; set; }

        [Browsable(false)]
        public int PackingId { get; set; }
        public string Packing { get; set; }
    }
}
