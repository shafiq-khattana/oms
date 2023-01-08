using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.Retail.Reports.ViewModel
{
    public class OrdVM
    {
        public string OderNo { get; set; }
        public string Dated { get; set; }
        public string CustName { get; set; }
        public string ContactNo { get; set; }
        public string CustAddress { get; set; }
        public byte[] Unum { get; set; }
        public byte[] QrImg { get; set; }
        public int UniqItems { get; set; }
        public int TotalItems { get; set; }
        public string InvcType { get; set; }
        public string ReceiptType { get; set; }
        public byte[] ReceiptTypeImage { get; set; }
    }
}
