using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.Model
{
    public class RepEntry
    {
        public int Id { get; set; }
        public RepItem RepItem { get; set; }
        public int RepItemId { get; set; }

        public decimal DispatchQty { get; set; }
        public decimal ReceivedQty { get; set; }
        public string Remarks { get; set; }

        public RepairDispatchRecord RepairDispatchRecord { get; set; }
        public int? RepairDispatchRecordId { get; set; }

        public RepairReceiveRecord RepairReceiveRecord { get; set; }
        public int? RepairReceiveRecordId { get; set; }
        public RepEntryDirection Direction { get; set; }
    }
    public enum RepEntryDirection
    {
        Dispatched,
        Received
    }
}
