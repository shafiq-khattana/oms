using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.Model
{
    public class RepairReceiveRecord
    {
        public int Id { get; set; }
        public decimal BillAmount { get; set; }
        public string BillId { get; set; }
        public DateTime ReceivedDate { get; set; }
        public DateTime InDate { get; set; }
        public List<RepEntry> Entries { get; set; }        
        public int DispatchId { get; set; }
        public string Unum { get; set; }
        public RepPlace Place { get; set; }
        public int RepPlaceId { get; set; }
        public string ReceivingPerson { get; set; }
        public decimal QtyReceived { get; set; }

        public RepairReceiveRecord()
        {
            Entries = new List<RepEntry>();
        }
    }
}
