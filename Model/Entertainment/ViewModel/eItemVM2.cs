using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entertainment.ViewModel
{
    public class eItemVM2
    {
        public int Id { get; set; }

        [DisplayName("Name Eng")]
        public string Title { get; set; }

        [DisplayName("Name Urdu")]
        public string TitleUrdu { get; set; }

        public decimal Qty { get; set; }
    }
}
