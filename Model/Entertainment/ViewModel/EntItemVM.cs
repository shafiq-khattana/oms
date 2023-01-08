using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entertainment.ViewModel
{
    public class EntItemVM
    {
        public int Id { get; set; }

        [DisplayName("Name Eng")]
        public string NameEng { get; set; }

        [DisplayName("Name Urdu")]
        public string NameUrdu { get; set; }

        [DisplayName("Qty Consumed")]
        public decimal QtyConsumed { get; set; }
    }
}
