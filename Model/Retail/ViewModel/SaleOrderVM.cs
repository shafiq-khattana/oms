using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Retail.ViewModel
{
    public class SaleOrderVM
    {
        public int Id { get; set; }
        public string Customer { get; set; }

        [DisplayName("Date time")]
        public string DateTime { get; set; }
        public int Items { get; set; }
        public decimal Qty { get; set; }
        [DisplayName("Unit Price")]
        public decimal UnitPrice { get; set; }

        [DisplayName("Total Price")]
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }

        [DisplayName("Net Price")]
        public decimal NetPrice { get; set; }

        [Browsable(false)]
        public string OrdType { get; set; }

        [Browsable(false)]

        public string OrderId { get; set; }
        [Browsable(false)]

        public decimal TotalDiscountPercentage { get; set; }

        [Browsable(false)]

        public bool IsCredit { get; set; }

        [Browsable(false)]

        public bool IsWalkIn { get; set; }


        [Browsable(false)]

        public string Unum { get; set; }

        [Browsable(false)]
        public decimal CustomerDiscount { get; set; }
        [Browsable(false)]
        public decimal CustomerDiscountPecentage { get; set; }
        [Browsable(false)]
        public decimal ServiceCharges { get; set; }
        [Browsable(false)]
        public decimal ServiceChargesPercentage { get; set; }
        [Browsable(false)]
        public decimal SaleTaxAmount { get; set; }
        [Browsable(false)]
        public decimal SaleTaxPercentage { get; set; }

        [Browsable(false)]
        public decimal OrderDiscount { get; set; }
        [Browsable(false)]
        public decimal OrderDiscountAmount { get; set; }
        [Browsable(false)]
        public decimal OfferDiscount { get; set; }
        [Browsable(false)]
        public decimal OfferDiscountPercentage { get; set; }
        [Browsable(false)]
        public decimal AmountGiven { get; set; }
        [Browsable(false)]
        public decimal ChangeGiven { get; set; }

        public List<LineVM2> Lines { get; set; }
        public decimal Weight { get; set; }

        [Browsable(false)]
        public string FincType { get; set; }

        [Browsable(false)]
        public decimal LaborExpenses { get; set; }

        [Browsable(false)]
        public decimal BardanaExpenses { get; set; }

        [Browsable(false)]
        public string UserId { get; set; }

        [Browsable(false)]
        public bool IsExtraAmounted { get; set; }

        [Browsable(false)]
        public decimal ExtraAmount { get; set; }
    }
}
