using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AppGoodCompany.ViewModel
{
    public class PackingStockFacotoryVM
    {
        [Browsable(false)]
        public int Id { get; set; }

        [Browsable(false)]
        public int PackingId { get; set; }
        public string Packing { get; set; }

        [DisplayName("Stock Qty")]
        public decimal StockQty { get; set; }

        [DisplayName("Issue Qty")]
        public decimal IssueQty { get; set; }
        public decimal Balance
        {
            get
            {
                return IssueQty + StockQty;
            }
        }
    }
}
