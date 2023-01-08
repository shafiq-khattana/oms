using Model.Deal.Model;
using Model.Financials.Model;
using System;
using System.Collections.Generic;

namespace Model.ReadyStuff.Model
{
    public class ReadySchedule
    {
        public int Id { get; set; }
        public DateTime  ScheduleDate { get; set; }
        public DateTime ReadyDate { get; set; }
        public bool IsCompleted { get; set; }
        public int NoOfVehicles { get; set; }
        public DateTime? DispatchedDate { get; set; }
        public int? ReadyDriverId { get; set; }
        public ReadyDriver Driver { get; set; }
        public decimal ScheduleWeight { get; set; }
        public decimal WeighBridgeWeight { get; set; }
        public int? WeighBridgeId { get; set; }
        public WeighBridge  WeighBridge { get; set; }

        public decimal TotalTradeUnits { get; set; }
        public decimal PerTradeUnitPrice { get; set; }

        public decimal GrossPrice { get; set; }
        public decimal BrokerSharePercentage { get; set; }
        public decimal BrokerShareAmount { get; set; }
        public decimal NetScheduleAmount { get; set; }
        public ReadyDeal ReadyDeal { get; set; }
        public int ReadyDealId { get; set; }

        public List<Bharthi> Bharthies { get; set; }
        public string ScheduleNo { get; set; }
        public ReadyScheduleType ScheduleType { get; set; }
        public string VehicleNo { get; set; }
        public ReadySelector Selector { get; set; }
        public int? ReadySelectorId { get; set; }
        public string Unum { get; set; }
        public decimal EmptyVehicleWeight { get; set; }
        public decimal FullVehicleWeight { get; set; }

        public decimal TotalPackings { get; set; }
        public decimal LaborExpenses { get; set; }
        public string LaborExpensesDescription { get; set; }

        public decimal BardanaAmountExpense { get; set; }
        public List<DayBook> DayBookEntries { get; set; }
        public ReadySchedule()
        {
            Bharthies = new List<Bharthi>();
            DayBookEntries = new List<DayBook>();
        }
    }

    public enum ReadyScheduleType
    {
        Scheduled,
        Dispatched,
        Completed,
        Cancelled
    }
}