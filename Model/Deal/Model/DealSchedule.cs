using Model.Deal.Common;
using Model.Employees.Model;
using Model.Financials.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Deal.Model
{
    public class DealSchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }       
        
        public decimal ScheduledPrice { get; set; }
        public decimal ScheduledSubTradeUnits { get; set; }
        public decimal ScheduledPackingsUnits { get; set; }
        public decimal ScheduledTradeUnits { get; set; }

        public decimal LoadedPrice { get; set; }
        public decimal LoadedSubTradeUnits { get; set; }
        public decimal LoadedPackingsUnits { get; set; }
        public decimal LoadedTradeUnits { get; set; }

        public decimal ReceivedPrice { get; set; }
        public decimal ReceivedSubTradeUnits { get; set; }
        public decimal ReceivedPackingsUnits { get; set; }
        public decimal ReceivedTradeUnits { get; set; }


        public decimal DiffLoadedPrice
        { get; set; }
        public decimal DiffLoadedSubTradeUnits
        { get; set; }
        public decimal DiffLoadedPackingUnits
        { get; set; }
        public decimal DiffLoadedTradeUnits
        { get; set; }

        public string TradeUnitTitle { get; set; }
        public string PackingUnitTitle { get; set; }
        public DateTime ReadyDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public bool IsArrived { get; set; }
        
        
        public string Remarks { get; set; }

        public int? DriverId { get; set; }
        public Driver Driver { get; set; }

        public int? VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int? SelectorId { get; set; }
        public Selector Selector { get; set; }

        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int? GoodCompanyId { get; set; }
        public GoodCompany GoodCompany { get; set; }

        public int? OmeDealId { get; set; }
        public OmeDeal OmeDeal { get; set; }

        public int AppDealId { get; set; }
        public AppDeal AppDeal { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }
        public string UpdatedBy { get; set; }
        public ScheduleStatus Status { get; set; }
        public ScheduleAlarm Alarm { get; set; }

        public bool IsDispatched { get; set; }
        public DateTime? DispatchedDate { get; set; }

        public bool IsLoaded { get; set; }
        public DateTime? LoadedDate { get; set; }

        public string Unum { get; set; }

        public string ScheduleRemarks { get; set; }
        public string DispatchRemarks { get; set; }
        public string LoadRemarks { get; set; }
        public string ReceiveRemarks { get; set; }

        public decimal DispatchLoadPackingDifference
        { get; set; }
        public decimal DispatchLoadTradeUnitDifference
        { get; set; }
        public decimal DispatchLoadSubTradeUnitDifference
        { get; set; }
        public decimal DispatchLoadPriceDifference
        { get; set; }

        public decimal AllPackingDifference
        {
            get
            {
                return ScheduledPackingsUnits - ReceivedPackingsUnits;
            }
        }
        public decimal AllTradeUnitDifference
        {
            get
            {
                return ScheduledTradeUnits - ReceivedTradeUnits;
            }
        }
        public decimal AllSubTradeUnitDifference
        {
            get
            {
                return ScheduledSubTradeUnits - ReceivedSubTradeUnits;
            }
        }
        public decimal AllPriceDifference
        {
            get
            {
                return ScheduledPrice - ReceivedPrice;
            }
        }
        public decimal FareDealAmount { get; set; }
        public decimal FareActualPaid { get; set; }
        public decimal FareDifference
        {
            get
            {
                return FareDealAmount - FareActualPaid;
            }
        }
        public List<ScheduleWeighBridge> ScheduleWeighBridges { get; set; }
        public List<ScheduleLoadPacking> ScheduleLoadPackings { get; set; }

        public int TransId { get; set; }

        public decimal LaborExpenses { get; set; }
        public string LaborExpensesDescription { get; set; }

        public List<DayBook> DayBookEntries { get; set; }
        public decimal TracktorLaborAmount { get; set; }
        public DealSchedule()
        {
            ScheduleWeighBridges = new List<ScheduleWeighBridge>();
            ScheduleLoadPackings = new List<ScheduleLoadPacking>();
            DayBookEntries = new List<DayBook>();
    }
    }
}
