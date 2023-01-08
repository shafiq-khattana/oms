using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Retail.ViewModel
{
    public class OrderLineVM
    {
        public string Id { get; set; }

        [Browsable(false)]
        public int ProduuctId { get; set; }

        [DisplayName("Product")]
        public string ProductName { get; set; }
        public decimal Qty { get; set; }

        [DisplayName("Unit Price")]
        public decimal UnitPrice { get; set; }

        [DisplayName("Total Price")]
        [Browsable(false)]
        public decimal TotalPrice
        {
            get
            {
                return UnitPrice * Qty;
            }
        }

        [DisplayName("Disc-%")]
        public decimal DiscPercentage { get; set; }

        [DisplayName("Disc Price")]
        public decimal DiscountPrice { get; set; }

        [DisplayName("Net Price")]
        public decimal NetPrice { get; set; }

        [Browsable(false)]
        public bool ApplyLaborExpense { get; set; }

        [Browsable(false)]
        public bool DeductBardanaExpense { get; set; }
    }
}
