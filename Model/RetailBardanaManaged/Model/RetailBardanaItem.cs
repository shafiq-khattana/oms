using Model.Financials.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.RetailBardanaManaged.Model
{
    public class RetailBardanaItem
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
        public decimal UnitLaborPriceRetail { get; set; }
        public decimal UnitLaborPriceWholeSale { get; set; }
        public GeneralAccount Account { get; set; }
        public string GeneralAccountId { get; set; }
        public List<RetailBardanaItemEntry> Entries { get; set; }

        public RetailBardanaItem()
        {
            Entries = new List<RetailBardanaItemEntry>();
        }
        public decimal PurchasePrice { get; set; }
        public decimal RetailPrice { get; set; }

    }
}
