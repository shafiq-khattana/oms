using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AppDriver.ViewModel
{
    public class DriverVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Remarks { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }

        [DisplayName("Added Date")]
        public string DateAdded { get; set; }
        public string Extra { get; set; }
        //public string Company { get; set; }

        public int Schedules { get; set; }
        public string Efficiency { get; set; }
    }
}
