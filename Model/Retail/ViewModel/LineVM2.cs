using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Retail.ViewModel
{
    public class LineVM2
    {
        public string Product { get; set; }
        public string Qty { get; set; }

        [DisplayName("Unit Price")]
        public string UnitPrice { get; set; }
        public string Discount { get; set; }

        [DisplayName("Net Price")]
        public string NetPrice { get; set; }
    }
}
