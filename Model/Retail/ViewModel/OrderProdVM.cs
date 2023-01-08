using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Retail.ViewModel
{
    public class OrderProdVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal TotalPrice
        {
            get
            {
                return Qty * UnitPrice;
            }
        }
        public decimal SKU { get; set; }
        public string Barcode { get; set; }
        public bool AlertOnSale { get; set; }
        public bool IsDicountable { get; set; }
        public decimal ProductDiscountAmount { get; set; }
        public decimal ProductDiscountPercentage { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal NetPrice { get; set; }
        public decimal Qty { get; set; }
        public decimal ProductWholeSaleAmount { get; set; }
        public bool ApplyLaborExpense { get; set; }
        public bool DeductBardanaExpense { get; set; }
    }
}
