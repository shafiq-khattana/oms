using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Retail.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [DisplayName("Cost Price")]
        public string CostPrice { get; set; }

        [DisplayName("Cust Price")]
        public string CustomerPrice { get; set; }

        [DisplayName("Available")]
        public bool IsAvailable { get; set; }

        [DisplayName("Stock")]
        public string SKU { get; set; }
        public string Code { get; set; }

        [DisplayName("Sale Alert")]
        public bool AlertOnSale { get; set; }

        [DisplayName("Discountable")]
        public bool IsDicountable { get; set; }

        [DisplayName("Category")]
        public string ProductCategory { get; set; }

        [DisplayName("Size")]
        public string ProductSize { get; set; }
        
        
    }
}
