using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AppWeighBridge.ViewModel
{
    public class WeighBridgeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        [DisplayName("Type")]
        public string WeighBrideType { get; set; }
        public int Schedules { get; set; }
        public string Efficiency { get; set; }
    }
}
