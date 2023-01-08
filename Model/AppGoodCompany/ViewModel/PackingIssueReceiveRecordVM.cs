using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AppGoodCompany.ViewModel
{
    public class PackingIssueReceiveRecordVM
    {
        public int Id { get; set; }

        [DisplayName("Company")]
        public string GoodCompany { get; set; }

        [Browsable(false)]
        public int GoodCompanyId { get; set; }

        [DisplayName("Packing")]
        public string DealPacking { get; set; }

        [Browsable(false)]
        public int DealPackingId { get; set; }
        public decimal Qty { get; set; }
        public DateTime DateTime { get; set; }

        [DisplayName("Type")]
        public string RecordType { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
    }
}
