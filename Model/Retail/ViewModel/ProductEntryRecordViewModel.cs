using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Retail.ViewModel
{
    public class ProductEntryRecordViewModel
    {
        public int Id { get; set; }
        
        [DisplayName("Date added")]
        public string DateAdded { get; set; }

        [DisplayName("Qty Added")]
        public string QtyAdded { get; set; }

        [DisplayName("Unit Price")]
        public string UnitPrice { get; set; }

        [DisplayName("Total Price")]
        public string TotalPrice { get; set; }
        public string Remarks { get; set; }

    }
}
