using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entertainment.Model
{
    public class EntPurchase
    {
        public int Id { get; set; }
        public List<EntItemEntry> Entries { get; set; }
        public decimal Qty { get; set; }
        public string Remarks { get; set; }
        public DateTime Dated { get; set; }
        public string Unum { get; set; }
        public string Operator { get; set; }
        public EntPurchase()
        {
            Entries = new List<EntItemEntry>();
        }
    }
}
