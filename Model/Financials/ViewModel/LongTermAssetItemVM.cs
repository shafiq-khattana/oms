using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Financials.ViewModel
{
    public class LongTermAssetItemVM
    {
        public int Id { get; set; }
        public string Item { get; set; }

        [DisplayName("Purcahsed Price")]
        public decimal PurchasedPrice { get; set; }

        [DisplayName("Current Price")]
        public decimal CurrentPrice { get; set; }

        [DisplayName("Dated Added")]
        public string DateAdded { get; set; }
        public string Description { get; set; }
    }
}
