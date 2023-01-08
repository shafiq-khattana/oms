using Model.Financials.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Deal.Model
{
    public class DealPacking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameUrdu { get; set; }
        public decimal UnitPrice { get; set; }
        public List<OmeDeal> OmeDeals { get; set; }

        public GeneralAccount Account { get; set; }
        public int GeneralAccountId { get; set; }
        public List<ScheduleLoadPacking> ScheduleLoadPackings { get; set; }
        public List<PackingIssueReceiveRecord> PackingIssueReceiveRecords { get; set; }
        public List<PackingStock> PackingStocks { get; set; }
        public List<FactoryPackingStock> FactoryPackingStocks { get; set; }
        public List<FactoryPackingStockAddedRecord> FactoryPackingStockAddedRecords { get; set; }
        public List<FactoryPackingStockIssueRecord> FactoryPackingStockIssueRecords { get; set; }
        public DealPacking()
        {
            OmeDeals = new List<OmeDeal>();
            FactoryPackingStockAddedRecords = new List<FactoryPackingStockAddedRecord>();
            FactoryPackingStocks = new List<FactoryPackingStock>();
            ScheduleLoadPackings = new List<ScheduleLoadPacking>();
            PackingIssueReceiveRecords = new List<PackingIssueReceiveRecord>();
            PackingStocks = new List<PackingStock>();
            FactoryPackingStockIssueRecords = new List<FactoryPackingStockIssueRecord>();
        }
    }
}