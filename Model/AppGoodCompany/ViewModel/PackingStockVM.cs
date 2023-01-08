using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AppGoodCompany.ViewModel
{
    public class PackingStockVM
    {
        [DisplayName("Goods Company")]
        public string Company { get; set; }
        public string Packing { get; set; }
        public string Qty { get; set; }
        public decimal Value { get; set; }
    }
}
