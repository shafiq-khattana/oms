using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.Model
{
    public class RepItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameUrdu { get; set; }
        public decimal SKU { get; set; }
        public decimal USCount { get; set; }
        public decimal SACount { get; set; }
        public decimal UR { get; set; }
        public decimal Adv { get; set; }
        public decimal UnitValue { get; set; }
        public decimal TotalValue { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
        public decimal Weight { get; set; }
        public int ItemCategoryId { get; set; }
        public ItemCategory ItemCategory { get; set; }
        public int StoreLocationId { get; set; }
        public StoreLocation StoreLocation { get; set; }
        public List<ItemPurchaseEntry> ItemPurchaseEntries { get; set; }
        public List<RepEntry> RepEntries { get; set; }
        public List<AdvanceItemRecord> AdvaneItemRecords { get; set; }
        public List<RepItemDamageRecord> ItemDamageRecords { get; set; }
        public List<RepItemPreAddRecord> RepItemPreAddRecords { get; set; }
        public List<RepItemConsumptionRecord> RepItemConsumptionRecords { get; set; }
        public RepItem()
        {
            RepEntries = new List<RepEntry>();
            ItemPurchaseEntries = new List<ItemPurchaseEntry>();
            AdvaneItemRecords = new List<AdvanceItemRecord>();
            ItemDamageRecords = new List<RepItemDamageRecord>();
            RepItemPreAddRecords = new List<RepItemPreAddRecord>();
        }
    }
}
