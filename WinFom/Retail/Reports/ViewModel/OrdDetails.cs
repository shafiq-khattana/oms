using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.Retail.Reports.ViewModel
{
    public class OrdDetails
    {
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalDiscountPercentage { get; set; }
        public decimal NetAmount { get; set; }
       
        public decimal SaleTaxAmount { get; set; }
        public decimal SaleTaxPercentage { get; set; }
        public decimal ServiceCharges { get; set; }
        public string Operator { get; set; }
    }
}
