using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Retail.Model
{
    public class SIEntry
    {
        public int Id { get; set; }
        public decimal QtyAdded { get; set; }
        public DateTime Dated { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }

        public StockItem StockItem { get; set; }
        public int StockItemId { get; set; }
    }
}
