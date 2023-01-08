using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.RepairUI.Reports.Model
{
    public class DispatchCompanyVM
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public byte[] Logo { get; set; }
        public byte[] Qrcode { get; set; }
        public byte[] Barcode { get; set; }
        public string ReportTitle { get; set; }
        public string Dated { get; set; }
        public string TOPerson { get; set; }
        public string BillId { get; set; }
        public string RepairShop { get; set; }
        public int RepId { get; set; }
    }
}
