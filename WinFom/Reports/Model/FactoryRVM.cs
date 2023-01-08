using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.Reports.Model
{
    public class FactoryRVM
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Dated { get; set; }
        public string Misc1 { get; set; }
        public string Misc2 { get; set; }
        public string Misc3 { get; set; }
        public string Misc4 { get; set; }
        public byte[] LogoImg { get; set; }
        public byte[] QrImg { get; set; }
        public byte[] Barcode { get; set; }
    }
}
