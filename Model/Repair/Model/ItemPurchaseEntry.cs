using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.Model
{
    public class ItemPurchaseEntry
    {
        public int Id { get; set; }
        public decimal Qty { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal UnitPrice { get; set; }
       
        public PurchaseInvoiceRecord PurchaseInvoiceRecord { get; set; }
        public int PurchaseInvoiceRecordId { get; set; }
        public RepItem RepItem { get; set; }
        public int RepItemId { get; set; }
        public string Remarks { get; set; }
        public ItemPurchaseEntry()
        {
            
        }

    }
}
