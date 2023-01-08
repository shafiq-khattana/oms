using Model.Deal.Model;
using Model.Financials.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ReadyStuff.Model
{
    public class OilDeal
    {
        public int Id { get; set; }
        public DateTime DealDate { get; set; }
        public DateTime ReadyDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public decimal DealQty { get; set; }

        public OilDealBroker Broker { get; set; }
        public int OilDealBrokerId { get; set; }

        public OilDealItem Item { get; set; }
        public int OilDealItemId { get; set; }
        public OilDealSelector Selector { get; set; }
        public int? OilDealSelectorId { get; set; }

        public OilDealDriver Driver { get; set; }
        public int? OilDealDriverId { get; set; }
        public string VehicleNo { get; set; }
        public decimal VehicleEmptyWeight { get; set; }
        public decimal VehicleScheduleQty { get; set; }
        public decimal VehicleFullWeight { get; set; }
        public decimal VehicleWeightDifference
        {
            get
            {
                return VehicleFullWeight - VehicleEmptyWeight;
            }
        }
        public decimal WeighBridgeWeight { get; set; }
        public decimal BrokerSharePercentage { get; set; }
        public decimal BrokerShareAmount { get; set; }

        public OilTradeUnit TradeUnit { get; set; }
        public int OilTradeUnitId { get; set; }
        public decimal TotalTradeUnits { get; set; }
        public decimal PerTradeUnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal NetPrice { get; set; }

        public OilDealStatus Status { get; set; }
        public string Unum { get; set; }

        public decimal LaborExpenses { get; set; }
        public string LaborExpensesDescription { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<DayBook> DayBookEntries { get; set; }

        public WeighBridge WeighBridge { get; set; }
        public int? WeighBridgeId { get; set; }
        public OilDeal()
        {
            DayBookEntries = new List<DayBook>();
        }
    }
    public enum OilDealStatus
    {
        Scheduled,
        Departed,
        Completed,
        Cancelled
    }
}
