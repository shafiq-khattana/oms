using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Financials.ViewModel
{
    public class LinkAcctVM
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; }
    }
}
