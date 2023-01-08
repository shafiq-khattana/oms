using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AppDriver.ViewModel
{
    public class VehicleDispatchVM
    {
        public string Vehicle { get; set; }

        [DisplayName("Dispatch Time")]
        public string DispatchDateTime { get; set; }
        public string Description { get; set; }
    }
}
