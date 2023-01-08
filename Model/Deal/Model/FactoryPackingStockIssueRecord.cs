using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Deal.Model
{
    public class FactoryPackingStockIssueRecord
    {
        public int Id { get; set; }
        public int DealPackingId { get; set; }
        public DealPacking DealPacking { get; set; }
        public decimal QtyIssued { get; set; }
        public DateTime IssuedDate { get; set; }
        public string Description { get; set; }
    }
}
