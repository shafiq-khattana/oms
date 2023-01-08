using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Admin.ViewModel
{
    public class AlarmVm
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [DisplayName("Generate Time")]
        public DateTime GenerateTime { get; set; }

        [DisplayName("Active Date")]
        public string ActiveDate { get; set; }


        [DisplayName("Arrival Date")]
        public string EndTime { get; set; }
        public bool Status { get; set; }
    }
}
