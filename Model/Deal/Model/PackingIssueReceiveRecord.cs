using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Deal.Model
{
    public class PackingIssueReceiveRecord
    {
        public int Id { get; set; }
        public GoodCompany GoodCompany { get; set; }
        public int GoodCompanyId { get; set; }
        public DealPacking DealPacking { get; set; }
        public int DealPackingId { get; set; }
        public decimal Qty { get; set; }
        public DateTime DateTime { get; set; }
        public RecordType RecordType { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }

        public string Unum { get; set; }
    }
    public enum RecordType
    {
        Issue,
        Receive
    }
}
