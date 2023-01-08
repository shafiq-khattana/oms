using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.Model
{
    public class PurchaseInvoiceRecord
    {
        public int Id { get; set; }
        public string BillId { get; set; }
        public decimal TotalItems { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime InDate { get; set; }
        public ItemSupplier Supplier { get; set; }
        public int ItemSupplierId { get; set; }
        public string Unum { get; set; }
        public string Remarks { get; set; }
        public int DaybookId { get; set; }
        public List<ItemPurchaseEntry> ItemPurchaseEntries { get; set; }
        public PurchaseInvoiceRecord()
        {
            ItemPurchaseEntries = new List<ItemPurchaseEntry>();
        }
    }
}
