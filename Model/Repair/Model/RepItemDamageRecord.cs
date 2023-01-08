using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.Model
{
    public class RepItemDamageRecord
    {
        public int Id { get; set; }
        public RepItem Item { get; set; }
        public int RepItemId { get; set; }
        public decimal Qty { get; set; }
        public DateTime Dated { get; set; }
        public string Remarks { get; set; }
    }

    public class RepItemPreAddRecord
    {
        public int Id { get; set; }
        public RepItem Item { get; set; }
        public int RepItemId { get; set; }
        public decimal Qty { get; set; }
        public DateTime Dated { get; set; }
        public string Remarks { get; set; }
    }
}
