using Model.Retail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Deal.Model
{
    public class ReturnRecord
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public string Item { get; set; }
        public decimal Qty { get; set; }
        public decimal Amount { get; set; }
        public string AddedBy { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime InDate { get; set; }
    }
}
