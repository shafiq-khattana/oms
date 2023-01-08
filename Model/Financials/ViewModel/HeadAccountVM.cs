using Model.Financials.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Financials.ViewModel
{
    public class HeadAccountVM
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public AccountNature Type { get; set; }

        [DisplayName("Sub Accts")]
        public int SubAccountCounts { get; set; }
    }
    
}
