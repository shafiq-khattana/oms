using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ReadyStuff.Model
{
    public class OilDealAlarm
    {
        public int Id { get; set; }
        public string AlarmText { get; set; }
        public DateTime AlarmReadyDate { get; set; }
        public DateTime GenerateDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
