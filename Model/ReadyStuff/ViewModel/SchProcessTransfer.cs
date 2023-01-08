using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ReadyStuff.ViewModel
{
    public class SchProcessTransfer
    {
        public string ScheduleNo { get; set; }
        public string Broker { get; set; }
        public int Vehicles { get; set; }
        public string ReadyDate { get; set; }
        public string DealItem { get; set; }
        public int ScheduleId { get; set; }
    }
}
