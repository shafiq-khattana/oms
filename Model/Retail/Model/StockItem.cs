using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Retail.Model
{
    public class StockItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleUrdu { get; set; }
        public decimal SKU { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice
        {
            get
            {
                return SKU * UnitPrice;
            }
        }
        public bool IsActive { get; set; }
        public string Description { get; set; }

        public List<SIEntry> SIEntries { get; set; }

        public StockItem()
        {
            SIEntries = new List<SIEntry>();
        }
    }
}
