using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.Retail.Reports.ViewModel
{
    public class SaleOrderHeader
    {
        public string FactoryName { get; set; }
        public string FactoryAddress { get; set; }
        public string FactoryPhone { get; set; }
        public byte[] FactoryLogo { get; set; }
        public byte[] Qrcode { get; set; }
        public byte[] Barcode { get; set; }
        public string Dated { get; set; }
        public int TotalOrders { get; set; }
        public string TotalSales { get; set; }
        public string TotalDiscount { get; set; }
        public string TotalNetSales { get; set; }
        public int PartialOrders { get; set; }
        public int CreditOrders { get; set; }
        public int CashOrders { get; set; }
        public int TotalBori { get; set; }
        public string TotalWeight { get; set; }
        public string LaborExpenses { get; set; }
        public string BardanaExpenses { get; set; }
        public decimal CreditSales { get; set; }
        public decimal CashInHand { get; set; }
        public decimal ExtraCashAmount { get; set; }
    }
}
