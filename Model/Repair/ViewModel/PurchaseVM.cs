using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.ViewModel
{
    public class PurchaseVM
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Bill No")]
        public string BillId { get; set; }

        [DisplayName("Total Items")]
        public decimal TotalItems { get; set; }

        [DisplayName("Total Price")]
        public decimal TotalPrice { get; set; }

        [DisplayName("Purchase Date")]
        public string PurchaseDate { get; set; }
        public string Supplier { get; set; }
        public string Remarks { get; set; }
    }
}
