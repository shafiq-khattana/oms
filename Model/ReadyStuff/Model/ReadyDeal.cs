using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Deal.Model;

namespace Model.ReadyStuff.Model
{
    public class ReadyDeal
    {
        public int Id { get; set; }
        public int ReadyBrokerId { get; set; }
        public ReadyBroker Broker { get; set; }

        public int ReadyItemId { get; set; }
        public ReadyItem ReadyItem { get; set; }
        public ReadyTradeUnit TradeUnit { get; set; }
        public int? ReadyTradeUnitId { get; set; }
        public decimal TotalTradeUnits { get; set; }
        public decimal PerTradeUnitPrice { get; set; }
        public decimal EstimatedPrice { get; set; }
        public int NoOfVehicles { get; set; }
        public decimal TotalWeight { get; set; }
        public DateTime DealDate { get; set; }
        public bool IsCompleted { get; set; }
        public string CompletionStatus { get; set; }
        public List<ReadySchedule> ReadySchedules { get; set; }
        public AppDealStatus DealStatus { get; set; }
        public string Description { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ReadyDeal()
        {
            ReadySchedules = new List<ReadySchedule>();
        }
    }
}
