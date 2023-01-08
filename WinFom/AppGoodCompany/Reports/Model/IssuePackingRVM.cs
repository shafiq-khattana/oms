using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.AppGoodCompany.Reports.Model
{
    public class IssuePackingRVM
    {
        public string Company { get; set; }
        public string IssuePackings { get; set; }
        public string IssueDateTime { get; set; }
        public string Description { get; set; }
        public byte[] Barcode { get; set; }
    }
}
