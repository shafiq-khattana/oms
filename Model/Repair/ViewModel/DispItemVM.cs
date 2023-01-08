using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.ViewModel
{
    public class DispItemVM
    {
        public int Id { get; set; }
        public string Item { get; set; }

        [DisplayName("Disp Qty")]
        public decimal QtyDispatched { get; set; }

        [DisplayName("Rece Qty")]
        public decimal QtyReceived { get; set; }

        [DisplayName("Rem Qty")]
        public decimal QtyRemaining
        {
            get
            {
                return
                    QtyDispatched - QtyReceived;
            }
            private set { }
        }
        public string Remarks { get; set; }
    }
    public class ReceItemVM
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public decimal Qty { get; set; }
        public string Remarks { get; set; }
    }
    public class NowReceItemVM
    {
        [ReadOnly(true)]
        public int Id { get; set; }

        [ReadOnly(true)]
        public string Item { get; set; }
        [Browsable(false)]
        public int ItemId { get; set; }

        [DisplayName("Rem Qty")]
        [ReadOnly(true)]
        public decimal QtyRemaining { get; set; }

        [DisplayName("Rec Qty")]
        public decimal QtyReceiving { get; set; }
        public string Remarks { get; set; }
    }
    public class ItemQty
    {
        public int ItemId { get; set; }
        public decimal Qty { get; set; }
        public string Name { get; set; }
    }
}
