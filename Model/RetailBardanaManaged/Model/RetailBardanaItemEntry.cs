using Model.Financials.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.RetailBardanaManaged.Model
{
    public class RetailBardanaItemEntry
    {
        public int Id { get; set; }
        public int RetailBardanaItemId { get; set; }
        public RetailBardanaItem RetailBardanaItem { get; set; }
        public int RetailBardanaSupplierId { get; set; }
        public RetailBardanaSupplier RetailBardanaSupplier { get; set; }
        public decimal QtyEntered { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string Remarks { get; set; }
        public DateTime Dated { get; set; }

        public decimal PurchasePrice { get; set; }
        public decimal StockPrice { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal CustomerPrice { get; set; }        
        public int DayBookId { get; set; }
    }
}
