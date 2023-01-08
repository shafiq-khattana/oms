using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.OilDirtStuff.ViewModel
{
    public class OilDirtSchTransfer
    {
        public int ScheduleId { get; set; }
        public string Broker { get; set; }
        public string DealSchedule { get; set; }
        public string Item { get; set; }
        public string DealDate { get; set; }
        public string ReadyDate { get; set; }
    }
}
