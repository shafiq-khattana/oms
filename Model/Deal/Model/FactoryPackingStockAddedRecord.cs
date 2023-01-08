using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Deal.Model
{
    public class FactoryPackingStockAddedRecord
    {
        public int Id { get; set; }
        public int DealPackingId { get; set; }
        public DealPacking DealPacking { get; set; }
        public decimal QtyAdded { get; set; }
        public DateTime AddedDate { get; set; }
        public string Description { get; set; }

        public DealPackingSupplier DealPackingSupplier { get; set; }
        public int? DealPackingSupplierId { get; set; }
    }
}
