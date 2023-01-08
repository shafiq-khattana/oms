using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entertainment.ViewModel
{
    public class ePurEntry
    {
        public int Id { get; set; }
        public string Item { get; set; }

        [DisplayName("Item Urdu")]
        public string ItemUrdu { get; set; }
        public decimal Qty { get; set; }
        
    }
}
