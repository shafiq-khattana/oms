using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AppGoodCompany.ViewModel
{
    public class GoodCompanyVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Owner { get; set; }

        [DisplayName("Owner Contact")]
        public string OwnerContact { get; set; }
        public string Remarks { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }

        [DisplayName("Date Added")]
        public string DateAdded { get; set; }
        public string Extra { get; set; }
        //public int Drivers { get; set; }
        //public int Vehicles { get; set; }
        public int Schedules { get; set; }
        public string Efficiency { get; set; }
        
    }
}
