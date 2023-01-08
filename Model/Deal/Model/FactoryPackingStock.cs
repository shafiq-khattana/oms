using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Deal.Model
{
    public class FactoryPackingStock
    {
        public int Id { get; set; }
        public DealPacking DealPacking { get; set; }
        public int DealPackingId { get; set; }
        public decimal Quantity { get; set; }
    }
}
