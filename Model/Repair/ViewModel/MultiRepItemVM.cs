using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.ViewModel
{
    public class MultiRepItemVM
    {
        [ReadOnly(true)]
        public int Id { get; set; }

        [ReadOnly(true)]
        public string Title { get; set; }
        public float Qty { get; set; }
        public string Remarks { get; set; }
        [ReadOnly(true)]
        public bool Selected { get; set; }
    }
}
