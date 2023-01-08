using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Deal.ViewModel
{
    public class PackingReceiveEditAble
    {
        public int Id { get; set; }

        public string Packing { get; set; }

        [DisplayName("Load Qty")]
        public decimal QtyLoaded { get; set; }

        [DisplayName("Rec Qty")]
        public decimal QtyReceived { get; set; }
        public decimal Difference
        {
            get
            {
                return QtyLoaded - QtyReceived;
            }
        }
        [Browsable(false)]
        public int PackingId { get; set; }
        
    }
}
