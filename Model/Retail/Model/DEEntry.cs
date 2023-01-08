using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Retail.Model
{
    public class DEEntry
    {
        public int Id { get; set; }
        public DateTime Dated { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public decimal SaleAmount { get; set; }
        public decimal SaleQty { get; set; }
        public string Description { get; set; }
    }
}
