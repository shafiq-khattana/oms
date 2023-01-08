using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Financials.ViewModel
{
    public class CaptitalAccountVM
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Balance { get; set; }

        [DisplayName("Share (%)")]
        public string SharePercentage { get; set; }
    }
}
