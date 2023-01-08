using Model.Deal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.OilDirtStuff.Model
{
    public class OilDirtDeal
    {
        public int Id { get; set; }
        public int OilDirtBrokerId { get; set; }
        public OilDirtBroker Broker { get; set; }
        public int OilDirtItemId { get; set; }
        public OilDirtItem Item { get; set; }
        public int OilDirtTradeUnitId { get; set; }
        public OilDirtTradeUnit TradeUnit { get; set; }
        public decimal PerTradeUnitPrice { get; set; }
        public int NoOfVehicles { get; set; }
        public DateTime GenerateDate { get; set; }
        public DateTime ReadyDate { get; set; }
        public DateTime? DateCompletion { get; set; }

        public OilDirtStatus Status { get; set; }
        public string Description { get; set; }
        public string CompletionStatus { get; set; }
        public List<OilDirtSchedule> Schedules { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public OilDirtDeal()
        {
            Schedules = new List<OilDirtSchedule>();
        }
    }
    public enum OilDirtStatus
    {
        Scheduled,
        Departed,
        Completed,
        Partial,
        Cancelled
    }
}
