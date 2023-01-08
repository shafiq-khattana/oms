using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entertainment.Model
{
    public class EntItemEntry
    {
        public int Id { get; set; }
        public int EntItemId { get; set; }
        public EntItem Item { get; set; }
        public decimal Qty { get; set; }
        public EntPurchase Purchase { get; set; }
        public int EntPurchaseId { get; set; }
    }
}
