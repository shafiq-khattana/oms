using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Retail.ViewModel
{
    public class CustomerVM2
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Remarks { get; set; }

        [DisplayName("Invoices")]
        public int InvoiceCount { get; set; }
    }
}
