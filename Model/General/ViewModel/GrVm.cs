using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.General.ViewModel
{
    public class GrVm
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Dated { get; set; }

        [Browsable(false)]
        public decimal Amount { get; set; }

        [Browsable(false)]
        public string Unum { get; set; }
    }
}
