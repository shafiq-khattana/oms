using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AppDriver.ViewModel
{
    public class DriverVehicleVM
    {
        public int Id { get; set; }

        [DisplayName("Vehicle No")]
        public string No { get; set; }

        [DisplayName("Type")]
        public string VehicleType { get; set; }

        public int Schedules { get; set; }
        public string Efficiency { get; set; }
    }
}
