using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.OilDirtStuff.Model
{
    public class OilDirtAlarm
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime GenerateDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ActiveDate { get; set; }
        public bool IsActive { get; set; }
    }
}
