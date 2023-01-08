using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AppDriver.ViewModel
{
    public class VehicleLoadedVM
    {
        public string Vehicle { get; set; }

        [DisplayName("Loaded Time")]
        public string LoadedDateTime { get; set; }
        public string Description { get; set; }
    }
}
