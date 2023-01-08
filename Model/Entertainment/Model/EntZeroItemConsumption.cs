using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entertainment.Model
{
    public class EntZeroItemConsumption
    {
        public int Id { get; set; }
        public string Remarks { get; set; }
        public DateTime Dated { get; set; }
        public string Operator { get; set; }
        public decimal Amount { get; set; }
        public decimal QtyConsumed { get; set; }
    }
}
