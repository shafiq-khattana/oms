﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ScheduleSelector.ViewModel
{
    public class SelectorVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Remarks { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }

        [DisplayName("Date Added")]
        public string DateAdded { get; set; }
        public string Extra { get; set; }

        [DisplayName("Schedules")]
        public int SchedulesCount { get; set; }

        [DisplayName("Loss %")]
        public string LossPercentage { get; set; }

        [DisplayName("Loss $")]
        public string LossInCash { get; set; }
        public string Efficiency { get; set; }
    }
}
