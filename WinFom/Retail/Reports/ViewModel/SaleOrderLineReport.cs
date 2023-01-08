using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.Retail.Reports.ViewModel
{
    public class SaleOrderLineReport
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string Type { get; set; }
        public string TotalSales { get; set; }
        public string UnitPrice { get; set; }
        public string Discount { get; set; }
        public string NetSales { get; set; }
        public int Bories { get; set; }
        public string Weight { get; set; }
        public string DateTime { get; set; }
    }
}
