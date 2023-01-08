using Model.Admin.Model;
using Model.Deal.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ReadyStuff.Model;
using Model.OilDirtStuff.Model;
using Model.Retail.Model;
using Model.Financials.Model;
using Model.Employees.Model;
using Model.CommonModel;
using Model.RetailBardanaManaged.Model;
using Model.Repair.Model;
using Model.EFiling.Model;
using Model.General.Model;
using Model.Entertainment.Model;

namespace WinFom.Admin.Database
{
    public class Context : DbContext
    {
        public Context() : base("ConStr")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Types().Configure(a => a.MapToStoredProcedures());

            modelBuilder.Properties<decimal>().Configure(a => a.HasPrecision(16, 4));

            modelBuilder.Entity<DealSchedule>().HasOptional(d => d.Alarm)
                .WithRequired(a => a.DealSchedule);

            modelBuilder.Entity<AppUserOptions>()
                .HasKey(a => a.AppUserId);

            modelBuilder.Entity<AppUser>()
                .HasOptional(a => a.UserOptions)
                .WithRequired(o => o.AppUser);

           

            //modelBuilder.Entity<GeneralAccount>().HasOptional(a => a.Broker)
            //    .WithRequired(a => a.Account);
            //modelBuilder.Entity<GeneralAccount>().HasOptional(a => a.Company)
            //    .WithRequired(a => a.Account);
            //modelBuilder.Entity<GeneralAccount>().HasOptional(a => a.Selector)
            //    .WithRequired(a => a.Account);
            //modelBuilder.Entity<GeneralAccount>().HasOptional(a => a.GoodCompany)
            //    .WithRequired(a => a.Account);
            //modelBuilder.Entity<GeneralAccount>().HasOptional(a => a.DealItem)
            //    .WithRequired(a => a.Account);


            //modelBuilder.Entity<GeneralAccount>().HasOptional(a => a.OilDealBroker)
            //   .WithRequired(a => a.Account);
            //modelBuilder.Entity<GeneralAccount>().HasOptional(a => a.OilDealSelector)
            //    .WithRequired(a => a.Account);
            //modelBuilder.Entity<GeneralAccount>().HasOptional(a => a.ReadyBroker)
            //    .WithRequired(a => a.Account);
            //modelBuilder.Entity<GeneralAccount>().HasOptional(a => a.ReadySelector)
            //    .WithRequired(a => a.Account);
            //modelBuilder.Entity<GeneralAccount>().HasOptional(a => a.OilDealItem)
            //    .WithRequired(a => a.Account);
            //modelBuilder.Entity<GeneralAccount>().HasOptional(a => a.ReadyItem)
            //   .WithRequired(a => a.Account);



            //modelBuilder.Entity<GeneralAccount>().HasOptional(a => a.OilDirtBroker)
            //  .WithRequired(a => a.Account);
            //modelBuilder.Entity<GeneralAccount>().HasOptional(a => a.OilDirtSelector)
            //    .WithRequired(a => a.Account);
            //modelBuilder.Entity<GeneralAccount>().HasOptional(a => a.OilDirtItem)
            //    .WithRequired(a => a.Account);
            //modelBuilder.Entity<GeneralAccount>().HasOptional(a => a.Employee)
            //    .WithRequired(a => a.Account);
            //modelBuilder.Entity<GeneralAccount>().HasOptional(a => a.Customer)
            //    .WithRequired(a => a.Account);


        }
        public DbSet<OmeDeal> OmeDeals { get; set; }
        public DbSet<DealItem> DealItems { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<DealPacking> DealPackings { get; set; }
        public DbSet<DealSegment> DealSegments { get; set; }
        public DbSet<PackingUnit> PackingUnits { get; set; }
        public DbSet<Selector> Selectors { get; set; }
        public DbSet<TradeUnit> TradeUnits { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<DealSchedule> DealSchedules { get; set; }
        public DbSet<AppDeal> AppDeals { get; set; }
        public DbSet<ScheduleAlarm> ScheduleAlarms { get; set; }
        public DbSet<GoodCompany> GoodCompanies { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<AppSettings>    AppSettings { get; set; }
        public DbSet<WeighBridge> WeighBridges { get; set; }
        public DbSet<ScheduleWeighBridge> ScheduleWeighBridges { get; set; }
        public DbSet<DealPackingSupplier> DealPackingSuppliers { get; set; }
        public DbSet<ScheduleLoadPacking> ScheduleLoadPackings { get; set; }
        public DbSet<PackingIssueReceiveRecord> PackingIssueReceiveRecords { get; set; }
        public DbSet<PackingStock> PackingStocks { get; set; }

        public DbSet<FactoryPackingStock> FactoryPackingStocks { get; set; }
        public DbSet<FactoryPackingStockAddedRecord> FactoryPackingStockAddedRecords { get; set; }
        public DbSet<FactoryPackingStockIssueRecord> FactoryPackingStockIssueRecords { get; set; }

        #region Admin
        public DbSet<AppUserOptions> UserOptions { get; set; }
        public DbSet<KeyInfo> KeyInfo { get; set; }
        #endregion
        #region "Ready Stuff"
        public DbSet<Bharthi> Bharthies { get; set; }
        public DbSet<BharthiType> BharthiTypes { get; set; }
        public DbSet<ReadyBroker> ReadyBrokers { get; set; }
        public DbSet<ReadyDeal> ReadyDeals { get; set; }
        public DbSet<ReadyDriver> ReadyDrivers { get; set; }
        public DbSet<ReadyItem> ReadyItems { get; set; }
        public DbSet<ReadySchedule> ReadySchedules { get; set; }
        public DbSet<ReadyTradeUnit> ReadyTradeUnits { get; set; }
        public DbSet<ReadyScheduleAlarm> ReadyScheduleAlarms { get; set; }
        public DbSet<ReadySelector> ReadySelectors { get; set; }
        #endregion

        #region "Oil Deal"
        public DbSet<OilDeal> OilDeals { get; set; }
        public DbSet<OilDealBroker> OilDealBrokers { get; set; }
        public DbSet<OilDealDriver> OilDealDrivers { get; set; }
        public DbSet<OilDealItem> OilDealItems { get; set; }
        public DbSet<OilDealSelector> OilDealSelectors { get; set; }
        public DbSet<OilTradeUnit> OilTradeUnits { get; set; }
        public DbSet<OilDealAlarm> OilDealAlarms { get; set; }
        #endregion

        #region "Oil dirt"
        public DbSet<OilDirtDeal>  OilDirtDeals { get; set; }
        public DbSet<OilDirtAlarm> OilDirtAlarms { get; set; }
        public DbSet<OilDirtBroker> OilDirtBrokers { get; set; }
        public DbSet<OilDirtDriver> OilDirtDrivers { get; set; }
        public DbSet<OilDirtItem> OilDirtItems { get; set; }
        public DbSet<OilDirtSelector> OilDirtSelectors { get; set; }
        public DbSet<OilDirtTradeUnit> OilDirtTradeUnits { get; set; }
        public DbSet<OilDirtSchedule> OilDirtSchedules { get; set; }
        #endregion

        #region "Retail Model"
        public DbSet<CustomerCategory>   CustomerCategories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<SaleOrderLine> SaleOrderLines { get; set; }
        public DbSet<ProductEntryRecord> ProductEntryRecords { get; set; }
        public DbSet<DEEntry> DEEntries { get; set; }
        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<SIEntry> SIEntries { get; set; }
        public DbSet<CancelOrderLine> CancelOrderLines { get; set; }
        public DbSet<CancelSaleOrder> CancelSaleOrders { get; set; }
        public DbSet<ArchiveSaleOrder> ArchiveSaleOrders { get; set; }
        public DbSet<ArchiveOrderLine> ArchiveSaleOrderLines { get; set; }
        #endregion

        #region Financials
        public DbSet<AccountBase>     Accounts { get; set; }
        public DbSet<MiscTransaction> MiscTransactions { get; set; }
        public DbSet<DayBook> DayBooks { get; set; }
        public DbSet<AccountTransaction> AccountTransactions { get; set; }

        public DbSet<ExpenseItem> ExpenseItems { get; set; }
        public DbSet<ExpenseItemEntry> ExpenseItemEntries { get; set; }

        public DbSet<AccruedExpenseItem> AccruedExpenseItems { get; set; }
        public DbSet<LongTermAssetItem> LongTermAssetItems { get; set; }
        #endregion

        #region Employees
        public DbSet<Employee> Employees { get; set; }
        public DbSet<CreditEntry> CreditEntries { get; set; }
        public DbSet<AttendanceRecord> AttendaceRecords { get; set; }
        public DbSet<EmployPayRollEntry> PayRollEntries { get; set; }
        public DbSet<SalaryAllowance> SalaryAllowances { get; set; }
        public DbSet<SalaryDeduction> SalaryDeductions { get; set; }
        #endregion

        public DbSet<RetailBardanaItemEntry> RetailBardanaItemEntries { get; set; }
        public DbSet<RetailBardanaItem> RetailBardanaItems { get; set; }
        public DbSet<RetailBardanaSupplier> RetailBardanaSuppliers { get; set; }
        public DbSet<RawBrokerShareType> RawBrokerShares { get; set; }

        #region Misc
        public DbSet<ReturnRecord> ReturnRecords { get; set; }
        public DbSet<SmsAlertSettings> SmsAlertSettings { get; set; }
        public DbSet<SmsConfigObj> SmsConfigObj { get; set; }
        public DbSet<ShopMessage> ShopMessages { get; set; }
        public DbSet<AppMessageState> AppMessageStates { get; set; }
        public DbSet<Rahzam> Anattakh { get; set; }
        public DbSet<EFCategory>  EFCategories { get; set; }
        public DbSet<EFile> EFiles { get; set; }
        public DbSet<EFileImage> EFileImages { get; set; }
        #endregion


        #region Repair
        public DbSet<RepEntry> RepEntries { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<RepItem> RepItems { get; set; }
        public DbSet<RepPlace> RepPlaces { get; set; }
        public DbSet<ItemPurchaseEntry> ItemPurchaseEntries { get; set; }
        public DbSet<ItemSupplier> ItemSuppliers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<PurchaseInvoiceRecord> PurchaseInvoiceRecords { get; set; }
        public DbSet<RepairDispatchRecord> RepairDispatchRecords { get; set; }
        public DbSet<RepairReceiveRecord> RepairReceivedRecords { get; set; }
        public DbSet<AdvanceItemRecord> AdvanceItemRecords { get; set; }
        public DbSet<ItemPlaceSKU> ItemPlaceSKUs { get; set; }
        public DbSet<RepItemDamageRecord> RepItemDamageRecords { get; set; }
        public DbSet<RepItemPreAddRecord> RepItemPreAddRecords { get; set; }
        public DbSet<RepItemConsumptionRecord> RepItemConsumptionRecords { get; set; }
        public DbSet<StoreLocation> StoreLocations { get; set; }
        #endregion

        #region General receipt
        public DbSet<GeneralReceipt> GeneralReceipts { get; set; }
        #endregion

        #region "Entertainment"
        public DbSet<EntPurchase> EntPurchases { get; set; }
        public DbSet<EntItem> EntItems { get; set; }
        public DbSet<EntZeroItemConsumption> EntZeroItemConsumptions { get; set; }
        public DbSet<EntItemEntry> EntItemEntries { get; set; }
        #endregion
    }
}
