using Model.Financials.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.Model
{
    public class ItemSupplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameUrdu { get; set; }
        public string Address { get; set; }
        public string AddressUrdu { get; set; }
        public string Phone { get; set; }
        public GeneralAccount Account { get; set; }
        public string GeneralAccountId { get; set; }
        public List<PurchaseInvoiceRecord> PurchaseInvoiceRecords { get; set; }

        public ItemSupplier()
        {
            PurchaseInvoiceRecords = new List<PurchaseInvoiceRecord>();
        }
    }
}
