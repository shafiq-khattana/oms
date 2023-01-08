using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.ViewModel
{
    public class ItemPurchaseEntryVM
    {
        public int Id { get; set; }
        public string Category { get; set; }
        [DisplayName("Item")]
        public string RepItem { get; set; }

        [DisplayName("Work Place")]
        public string Place { get; set; }
        public decimal Qty { get; set; }

        [DisplayName("Total Price")]
        public decimal TotalPrice { get; set; }

        [DisplayName("Unit Price")]
        public decimal UnitPrice { get; set; }
        public string Remarks { get; set; }
    }
}
