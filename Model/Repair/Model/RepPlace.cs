using Model.Financials.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.Model
{
    public class RepPlace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameUrdu { get; set; }
        public string Address { get; set; }
        public string AddressUrdu { get; set; }
        public string PhoneNo { get; set; }
        public string GeneralAccountId { get; set; }
        public GeneralAccount GeneralAccount { get; set; }

        public List<RepairDispatchRecord> RepairDispatchRecords { get; set; }
        public List<RepairReceiveRecord> RepairReceiveRecords { get; set; }
        public List<AdvanceItemRecord> AdvaneItemRecords { get; set; }
        public RepPlace()
        {
            RepairDispatchRecords = new List<RepairDispatchRecord>();
            RepairReceiveRecords = new List<RepairReceiveRecord>();
            AdvaneItemRecords = new List<AdvanceItemRecord>();
        }
    }
}
