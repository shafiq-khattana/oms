using Model.Deal.Model;
using Model.Financials.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.OilDirtStuff.Model
{
    public class OilDirtSchedule
    {
        public int Id { get; set; }
        public string ScheduleNo { get; set; }

        public int? OilDirtSelectorId { get; set; }
        public OilDirtSelector Selector { get; set; }
        public int? OilDirtDriverId { get; set; }
        public OilDirtDriver Driver { get; set; }

        public string VehicleNo { get; set; }
        public decimal VehicleWeightEmpty { get; set; }
        public decimal VehicleWeightFull { get; set; }
        public decimal LoadedQty { get; set; }
        public decimal DealQty { get; set; }
        public decimal WeighBridgeWeight { get; set; }
        public int? WeighBridgeId { get; set; }
        public WeighBridge WeighBridge { get; set; }

        public decimal BrokerSharePercentage { get; set; }
        public decimal BrokerShareAmount { get; set; }
        public decimal TotalExpectedAmount { get; set; }
        public decimal TotalActualAmount { get; set; }
        public decimal TotalNetAmount { get; set; }

        public DateTime ReadyDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public int OilDirtDealId { get; set; }
        public OilDirtDeal Deal { get; set; }
        public decimal PerTradeUnitPrice { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string Unum { get; set; }
        public OilDirtScheduleStatus Status { get; set; }
        public decimal TotalTradeUnits { get; set; }

        public decimal LaborExpenses { get; set; }
        public string LaborExpensesDescription { get; set; }
        public List<DayBook> DayBookEntries { get; set; }

        public OilDirtSchedule()
        {
            DayBookEntries = new List<DayBook>();
        }
    }

    public enum OilDirtScheduleStatus
    {
        Scheduled,
        Departed,
        Completed,
        Cancelled
    }
}
