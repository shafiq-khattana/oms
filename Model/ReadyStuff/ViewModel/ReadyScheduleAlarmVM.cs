using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ReadyStuff.ViewModel
{
    public class ReadyScheduleAlarmVM
    {
        public int Id { get; set; }
        public string Description { get; set; }

        [DisplayName("Schedule Date")]
        public DateTime ScheduleDate { get; set; }

        [DisplayName("Alarm Date")]
        public DateTime AlarmDate { get; set; }

        [DisplayName("Ready Date")]
        public DateTime ReadyDate { get; set; }

        [DisplayName("Days Left")]
        public int DaysRemaining
        {
            get
            {
                return (ReadyDate - ScheduleDate).Days;
            }
        }
    }
}
