using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Admin.ViewModel
{
    public class AppUserVM
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }

        [DisplayName("State")]
        public bool IsActive { get; set; }
        [Browsable(false)]
        public string Options { get; set; }
    }
}
