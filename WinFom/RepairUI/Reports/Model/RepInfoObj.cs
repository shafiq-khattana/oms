using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.RepairUI.Reports.Model
{
    public class RepInfoObj
    {
        public string Dated { get; set; }
        public string ToPerson { get; set; }
        public string RepPlace { get; set; }
        public string RepItem { get; set; }
        public string WorkingPlace { get; set; }
        public string Qty { get; set; }
        public string Weight { get; set; }
        public string Operator { get; set; }
        public byte[] Barcode { get; set; }
        public byte[] QrCode { get; set; }
        public int EntryId { get; set; }
    }
}
