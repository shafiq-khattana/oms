using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Deal.Model
{
    public class OmeDeal : IEquatable<OmeDeal>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public int BrokerId { get; set; }
        public Broker Broker { get; set; }        

        public int DealItemId { get; set; }
        public DealItem DealItem { get; set; }

        public int DealPackingId { get; set; }
        public DealPacking Packing { get; set; }

        public decimal PerPackingQty { get; set; }
        public decimal TotalQty { get; set; }
        public decimal PackingQty { get; set; }

        public int TradeUnitId { get; set; }
        public TradeUnit TradeUnit { get; set; }
        public decimal TradeUnitPrice { get; set; }
        public decimal TotalTradeUnits { get; set; }
        public decimal DealPrice { get; set; }
       
        public decimal Tax { get; set; }
        public decimal FareAmount { get; set; }

        public decimal LaborExpenses { get; set; }
        public decimal LaborExpensesDescription { get; set; }
        public DateTime DealDate { get; set; }      

        public string AddedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsCompleted { get; set; }

        public int PackingUnitId { get; set; }

        [ForeignKey("PackingUnitId")]
        public PackingUnit PackingUnit { get; set; }     

        public List<DealSchedule> DealSchedules { get; set; }
        public int SegmentsCount { get; set; }
        public string Remarks { get; set; }
        public OmeDeal()
        {
            
            DealSchedules = new List<DealSchedule>();
        }
        public bool Equals(OmeDeal ot)
        {
            bool res = ((CompanyId == ot.CompanyId)
                && (BrokerId == ot.BrokerId)
                && (CompanyId == ot.CompanyId)
                && (DealItemId == ot.DealItemId)
                && (DealPackingId == ot.DealPackingId)
                && (PackingQty == ot.PackingQty)                
                && (DealDate.Date == ot.DealDate.Date)               
                && (SegmentsCount == SegmentsCount));
            return res;
        }
    }
}
