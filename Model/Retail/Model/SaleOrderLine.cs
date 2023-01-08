using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Retail.Model
{
    public class SaleOrderLine
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public SaleOrder SaleOrder { get; set; }
        public int SaleOrderId { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal NetPrice { get; set; }
        public bool ApplyLaborExpense { get; set; }
        public bool DeductBardanaExpense { get; set; }
    }

    public class CancelOrderLine
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public CancelSaleOrder SaleOrder { get; set; }
        public int CancelSaleOrderId { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal NetPrice { get; set; }
        public bool ApplyLaborExpense { get; set; }
        public bool DeductBardanaExpense { get; set; }
    }

    public class ArchiveOrderLine
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public ArchiveSaleOrder SaleOrder { get; set; }
        public int ArchiveSaleOrderId { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal NetPrice { get; set; }

        public bool ApplyLaborExpense { get; set; }        
        public bool DeductBardanaExpense { get; set; }
    }
}
