using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.Financials.Reports.Model
{
    public class FincReportHeader
    {
        public string FactoryName { get; set; }
        public string FactoryAddress { get; set; }
        public string FactoryPhone { get; set; }
        public byte[] FactoryLogo { get; set; }
        public byte[] QrCode { get; set; }
        public byte[] Barcode { get; set; }
        public string ReportTitle { get; set; }
        public string Dated { get; set; }
        public int Entries { get; set; }
    }
}
