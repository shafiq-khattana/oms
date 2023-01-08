using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.General.Model
{
    public class GeneralReceipt
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string DescriptionUrdu { get; set; }
        public DateTime Dated { get; set; }
        public decimal Amount { get; set; }
        public string Unum { get; set; }
        public GeneralReceiptStatus? Status { get; set; }
    }
    public enum GeneralReceiptStatus
    {
        Pending,
        Partial,
        Done
    }
}
