using Model.Retail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Retail.ViewModel
{
    public class OrdProcessVM
    {
        public bool PrinterState { get; set; }
        public int Copies { get; set; }
        public decimal AmountGiven { get; set; }
        public decimal ChangeGiven { get; set; }
        public decimal RemainingAmount { get; set; }
        public bool IsCredit { get; set; }
        public bool IsWalkIn { get; set; }
        public SaleOrderType OrderType { get; set; }
        public bool CustomerPrintCopy { get; set; }
    }
}
