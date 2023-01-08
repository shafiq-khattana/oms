using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Deal.Model
{
    public class ScheduleAlarm
    {
        [ForeignKey("DealSchedule")]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime GenerateTime { get; set; }
        public DateTime ActiveDate { get; set; }
        public DateTime EndTime { get; set; }
        public bool Status { get; set; }
        public DealSchedule DealSchedule { get; set; }
    }
}
