using Model.Deal.Model;
using Model.Financials.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Retail.Model
{
    public class SaleOrder
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime TimpStamp { get; set; }
        public int TotalItems { get; set; }
        public int UniqueItems { get; set; }
        public decimal TototPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalDiscountPercentage { get; set; }
        public decimal NetPrice { get; set; }
        public bool IsCredit { get; set; }
        public bool IsWalkIn { get; set; }
        public string Unum { get; set; }
        public bool IsExpensed { get; set; }
        public decimal CustomerDiscount { get; set; }
        public decimal CustomerDiscountPecentage { get; set; }
        public decimal ServiceCharges { get; set; }
        public decimal ServiceChargesPercentage { get; set; }
        public decimal SaleTaxAmount { get; set; }
        public decimal SaleTaxPercentage { get; set; }
        public bool IsExtraAmounted { get; set; }
        public decimal OrderDiscount { get; set; }
        public decimal OrderDiscountAmount { get; set; }
        public decimal OfferDiscount { get; set; }
        public decimal OfferDiscountPercentage { get; set; }
        public decimal AmountGiven { get; set; }
        public decimal ChangeGiven { get; set; }
        public decimal RemainingAmount { get; set; }
        public List<SaleOrderLine>   OrderLines { get; set; }
        public SaleOrderType OrderType { get; set; }
        public bool IsDone { get; set; }
        public decimal BardanaAmount { get; set; }
        public decimal LaborAmount { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal TotalPackings { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public List<DayBook>  DayBookEntries { get; set; }
        public int BardanaPackingsCount { get; set; }
        public SaleOrder()
        {
            OrderLines = new List<SaleOrderLine>();
            DayBookEntries = new List<DayBook>();
        }
    }

    public class CancelSaleOrder
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime TimpStamp { get; set; }
        public int BardanaPackingsCount { get; set; }
        public int TotalItems { get; set; }
        public int UniqueItems { get; set; }
        public decimal TototPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalDiscountPercentage { get; set; }
        public decimal NetPrice { get; set; }
        public bool IsCredit { get; set; }
        public bool IsWalkIn { get; set; }
        public string Unum { get; set; }

        public decimal CustomerDiscount { get; set; }
        public decimal CustomerDiscountPecentage { get; set; }
        public decimal ServiceCharges { get; set; }
        public decimal ServiceChargesPercentage { get; set; }
        public decimal SaleTaxAmount { get; set; }
        public decimal SaleTaxPercentage { get; set; }
        public bool IsExtraAmounted { get; set; }
        public bool IsExpensed { get; set; }
        public decimal OrderDiscount { get; set; }
        public decimal OrderDiscountAmount { get; set; }
        public decimal OfferDiscount { get; set; }
        public decimal OfferDiscountPercentage { get; set; }
        public decimal AmountGiven { get; set; }
        public decimal ChangeGiven { get; set; }
        public decimal RemainingAmount { get; set; }
        public List<CancelOrderLine> OrderLines { get; set; }
        public SaleOrderType OrderType { get; set; }
        public bool IsDone { get; set; }
        public decimal BardanaAmount { get; set; }
        public decimal LaborAmount { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal TotalPackings { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public List<DayBook> DayBookEntries { get; set; }
        public CancelSaleOrder()
        {
            OrderLines = new List<CancelOrderLine>();
        }
    }

    public class ArchiveSaleOrder
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime TimpStamp { get; set; }
        public int TotalItems { get; set; }
        public int UniqueItems { get; set; }
        public decimal TototPrice { get; set; }
        public int BardanaPackingsCount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalDiscountPercentage { get; set; }
        public decimal NetPrice { get; set; }
        public bool IsCredit { get; set; }
        public bool IsWalkIn { get; set; }
        public string Unum { get; set; }
        public bool IsExtraAmounted { get; set; }
        public bool IsExpensed { get; set; }
        public decimal CustomerDiscount { get; set; }
        public decimal CustomerDiscountPecentage { get; set; }
        public decimal ServiceCharges { get; set; }
        public decimal ServiceChargesPercentage { get; set; }
        public decimal SaleTaxAmount { get; set; }
        public decimal SaleTaxPercentage { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public decimal OrderDiscount { get; set; }
        public decimal OrderDiscountAmount { get; set; }
        public decimal OfferDiscount { get; set; }
        public decimal OfferDiscountPercentage { get; set; }
        public decimal AmountGiven { get; set; }
        public decimal ChangeGiven { get; set; }
        public decimal RemainingAmount { get; set; }
        public List<ArchiveOrderLine> OrderLines { get; set; }
        public SaleOrderType OrderType { get; set; }
        public bool IsDone { get; set; }
        public decimal BardanaAmount { get; set; }
        public decimal LaborAmount { get; set; }
        
        public decimal TotalWeight { get; set; }
        public decimal TotalPackings { get; set; }
        public List<DayBook> DayBookEntries { get; set; }
        public ArchiveSaleOrder()
        {
            OrderLines = new List<ArchiveOrderLine>();
        }
    }
    public enum SaleOrderType
    {
        Cash,
        Credit,
        Partial
    }
}