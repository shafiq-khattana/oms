using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Deal.Model
{
    public class DealSegment
    {
        public int Id { get; set; }
        public decimal TradeUnits { get; set; }
        public decimal PackingUnits { get; set; }
        public decimal Price { get; set; }
        public string TradeUnitTitle { get; set; }
        public string PackingUnitTitle { get; set; }
        public DateTime ReadyDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public bool IsArrived { get; set; }
        public int OmeDealId { get; set; }
        public OmeDeal OmeDeal { get; set; }
    }
}
