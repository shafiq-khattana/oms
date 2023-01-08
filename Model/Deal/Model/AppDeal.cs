using Model.CommonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Deal.Model
{
    public class AppDeal : IEquatable<AppDeal>
    {
        public AppDeal()
        {
            DealSchedules = new List<DealSchedule>();
        }
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public int BrokerId { get; set; }
        public Broker Broker { get; set; }

        public int DealItemId { get; set; }
        public DealItem DealItem { get; set; }

        public int? DealPackingId { get; set; }
        public DealPacking Packing { get; set; }

        public decimal PerPackingQty { get; set; }
        public decimal TotalQty { get; set; }
        public decimal PackingQty { get; set; }

        public int TradeUnitId { get; set; }
        public TradeUnit TradeUnit { get; set; }
        public decimal TotalTradeUnits { get; set; }
        public decimal DealPrice { get; set; }

        public decimal Tax { get; set; }
        public decimal FareAmount { get; set; }


        public DateTime DealDate { get; set; }

        public string AddedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsCompleted { get; set; }

        public int PackingUnitId { get; set; }

        [ForeignKey("PackingUnitId")]
        public PackingUnit PackingUnit { get; set; }

        public List<DealSchedule> DealSchedules { get; set; }
        public int SchedulesCount { get; set; }
        public string Remarks { get; set; }
        public decimal TradeUnitPrice { get; set; }
        public string Unum { get; set; }
        public string CompletionStatus { get; set; }

        public AppDealStatus DealStatus { get; set; }

        [ForeignKey("RawBrokerShareTypeId")]
        public RawBrokerShareType RawBrokerShareType { get; set; }

        // 1. for per pack
        // 2. for percentage
        // 3. for none
        public int RawBrokerShareTypeId { get; set; }
        public decimal BrokerSharePerPackPercentage { get; set; }
        public decimal BrokerShareAmount { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public bool Equals(AppDeal ot)
        {
            bool res = ((CompanyId == ot.CompanyId)
                && (BrokerId == ot.BrokerId)
                && (CompanyId == ot.CompanyId)
                && (DealItemId == ot.DealItemId)
                && (DealPackingId == ot.DealPackingId)
                && (PackingQty == ot.PackingQty)
                && (DealDate.Date == ot.DealDate.Date)
                && (SchedulesCount == SchedulesCount));
            return res;
        }
    }
    public enum AppDealStatus
    {
        Scheduled,
        Partial,
        Completed,
        Cancelled
    }
}
