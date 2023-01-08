using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.Model
{
    public class RepItemConsumptionRecord
    {
        public int Id { get; set; }
        public RepItem Item { get; set; }
        public int RepItemId { get; set; }
        public decimal QtyConsumed { get; set; }
        public decimal Price { get; set; }
        public DateTime Dated { get; set; }
        public string Remarks { get; set; }
        public int DaybookId { get; set; }
        public string AccountId { get; set; }
    }
}
