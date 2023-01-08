using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.Model
{
    public class RepairDispatchRecord
    {
        public int Id { get; set; }
        public decimal TotalItems { get; set; }
        public string BillNo { get; set; }
        public DateTime Date { get; set; }
        public RepPlace Place { get; set; }
        public int RepPlaceId { get; set; }
        public string TOPerson { get; set; }
        public decimal ReceivedItems { get; set; }
        public decimal RemainingItems { get; set; }
        public List<RepEntry> RepEntries { get; set; }
        public decimal BillPaid { get; set; }
        public string Unum { get; set; }
        public RepairDispatchStatus Status { get; set; }
        public RepairDispatchRecord()
        {
            RepEntries = new List<RepEntry>();            
        }
    }
    public enum RepairDispatchStatus
    {
        DispatchedOnly,
        DispatchedReceived,
        TotallyReceived
    }
}
