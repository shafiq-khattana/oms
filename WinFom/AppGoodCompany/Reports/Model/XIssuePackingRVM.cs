using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.AppGoodCompany.Reports.Model
{
    public class XIssuePackingRVM
    {
        public string FromCompany { get; set; }
        public string ToCompany { get; set; }
        public string IssueDate { get; set; }
        public string IssuePackings { get; set; }
        public string Description { get; set; }
        public byte[] Barcode { get; set; }
    }
}
