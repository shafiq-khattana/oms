using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.RetailBardanaManaged.ViewModel
{
    public class BardanaItemEntry
    {
        public int Id { get; set; }

        [DisplayName("Bardana Item")]
        public string RetailBardanaItem { get; set; }

        [DisplayName("Supplier")]
        public string RetailBardanaSupplier { get; set; }

        [DisplayName("Qty")]
        public string QtyEntered { get; set; }

        [DisplayName("Unit Price")]
        public string UnitPrice { get; set; }

        [DisplayName("Total Price")]
        public string TotalPrice { get; set; }
        public string Remarks { get; set; }
        public string Dated { get; set; }

        [Browsable(false)]
        public int DaybookId { get; set; }
    }
}
