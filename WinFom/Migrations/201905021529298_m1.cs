namespace WinFom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountBases",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AccountNo = c.String(),
                        Address = c.String(),
                        AddressUrdu = c.String(),
                        Title = c.String(),
                        TitleUrdu = c.String(),
                        Description = c.String(),
                        AccountNature = c.Int(nullable: false),
                        ExplicitilyCreated = c.Boolean(nullable: false),
                        SubHeadAccountId = c.String(maxLength: 128),
                        BankName = c.String(),
                        Balance = c.Decimal(precision: 16, scale: 4),
                        CrDrType = c.Int(),
                        ParentAccountId = c.String(maxLength: 128),
                        TopHeadAccountId = c.String(maxLength: 128),
                        HeadAccountId = c.String(maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.ParentAccountId)
                .ForeignKey("dbo.AccountBases", t => t.SubHeadAccountId)
                .ForeignKey("dbo.AccountBases", t => t.HeadAccountId)
                .ForeignKey("dbo.AccountBases", t => t.TopHeadAccountId)
                .Index(t => t.SubHeadAccountId)
                .Index(t => t.ParentAccountId)
                .Index(t => t.TopHeadAccountId)
                .Index(t => t.HeadAccountId);
            
            CreateTable(
                "dbo.AccountTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        DayBookId = c.Int(nullable: false),
                        Description = c.String(),
                        DebitAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        CreditAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Balance = c.Decimal(nullable: false, precision: 16, scale: 4),
                        AccountTransactionType = c.Int(nullable: false),
                        GeneralAccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.Brokers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Extra = c.String(),
                        GeneralAccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.AppDeals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        BrokerId = c.Int(nullable: false),
                        DealItemId = c.Int(nullable: false),
                        DealPackingId = c.Int(),
                        PerPackingQty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalQty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        PackingQty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TradeUnitId = c.Int(nullable: false),
                        TotalTradeUnits = c.Decimal(nullable: false, precision: 16, scale: 4),
                        DealPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Tax = c.Decimal(nullable: false, precision: 16, scale: 4),
                        FareAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        DealDate = c.DateTime(nullable: false),
                        AddedBy = c.String(),
                        UpdatedBy = c.String(),
                        IsCompleted = c.Boolean(nullable: false),
                        PackingUnitId = c.Int(nullable: false),
                        SchedulesCount = c.Int(nullable: false),
                        Remarks = c.String(),
                        TradeUnitPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Unum = c.String(),
                        CompletionStatus = c.String(),
                        DealStatus = c.Int(nullable: false),
                        RawBrokerShareTypeId = c.Int(nullable: false),
                        BrokerSharePerPackPercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        BrokerShareAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        AppUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.DealItems", t => t.DealItemId, cascadeDelete: true)
                .ForeignKey("dbo.TradeUnits", t => t.TradeUnitId, cascadeDelete: true)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId)
                .ForeignKey("dbo.Brokers", t => t.BrokerId, cascadeDelete: true)
                .ForeignKey("dbo.DealPackings", t => t.DealPackingId)
                .ForeignKey("dbo.PackingUnits", t => t.PackingUnitId, cascadeDelete: true)
                .ForeignKey("dbo.RawBrokerShareTypes", t => t.RawBrokerShareTypeId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.BrokerId)
                .Index(t => t.DealItemId)
                .Index(t => t.DealPackingId)
                .Index(t => t.TradeUnitId)
                .Index(t => t.PackingUnitId)
                .Index(t => t.RawBrokerShareTypeId)
                .Index(t => t.AppUserId);
            
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Password = c.String(),
                        Name = c.String(),
                        Contact = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Options = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArchiveSaleOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.String(),
                        CustomerId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        TimpStamp = c.DateTime(nullable: false),
                        TotalItems = c.Int(nullable: false),
                        UniqueItems = c.Int(nullable: false),
                        TototPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        BardanaPackingsCount = c.Int(nullable: false),
                        TotalDiscount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalDiscountPercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        NetPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        IsCredit = c.Boolean(nullable: false),
                        IsWalkIn = c.Boolean(nullable: false),
                        Unum = c.String(),
                        IsExtraAmounted = c.Boolean(nullable: false),
                        IsExpensed = c.Boolean(nullable: false),
                        CustomerDiscount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        CustomerDiscountPecentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ServiceCharges = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ServiceChargesPercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        SaleTaxAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        SaleTaxPercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        AppUserId = c.String(maxLength: 128),
                        OrderDiscount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        OrderDiscountAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        OfferDiscount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        OfferDiscountPercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        AmountGiven = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ChangeGiven = c.Decimal(nullable: false, precision: 16, scale: 4),
                        RemainingAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        OrderType = c.Int(nullable: false),
                        IsDone = c.Boolean(nullable: false),
                        BardanaAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        LaborAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalWeight = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalPackings = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.AppUserId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardNo = c.String(),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        AddressUrdu = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        CustomerCategoryId = c.Int(nullable: false),
                        CNIC = c.String(),
                        Points = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ApplyCardDiscount = c.Boolean(nullable: false),
                        CardStartDate = c.DateTime(nullable: false),
                        CardEndDate = c.DateTime(nullable: false),
                        DiscountPercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        IsEmployee = c.Boolean(nullable: false),
                        GeneralAccountId = c.String(maxLength: 128),
                        EmpId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .ForeignKey("dbo.CustomerCategories", t => t.CustomerCategoryId, cascadeDelete: true)
                .Index(t => t.CustomerCategoryId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.CustomerCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        TitleUrdu = c.String(),
                        Discount = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SaleOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.String(),
                        CustomerId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        TimpStamp = c.DateTime(nullable: false),
                        TotalItems = c.Int(nullable: false),
                        UniqueItems = c.Int(nullable: false),
                        TototPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalDiscount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalDiscountPercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        NetPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        IsCredit = c.Boolean(nullable: false),
                        IsWalkIn = c.Boolean(nullable: false),
                        Unum = c.String(),
                        IsExpensed = c.Boolean(nullable: false),
                        CustomerDiscount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        CustomerDiscountPecentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ServiceCharges = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ServiceChargesPercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        SaleTaxAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        SaleTaxPercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        IsExtraAmounted = c.Boolean(nullable: false),
                        OrderDiscount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        OrderDiscountAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        OfferDiscount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        OfferDiscountPercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        AmountGiven = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ChangeGiven = c.Decimal(nullable: false, precision: 16, scale: 4),
                        RemainingAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        OrderType = c.Int(nullable: false),
                        IsDone = c.Boolean(nullable: false),
                        BardanaAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        LaborAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalWeight = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalPackings = c.Decimal(nullable: false, precision: 16, scale: 4),
                        AppUserId = c.String(maxLength: 128),
                        BardanaPackingsCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.AppUserId);
            
            CreateTable(
                "dbo.DayBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(),
                        DebitTrace = c.String(),
                        CreditTrace = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        IsReversed = c.Boolean(nullable: false),
                        DebitAccountId = c.String(),
                        CreditAccountId = c.String(),
                        CanRollBack = c.Boolean(nullable: false),
                        SaleOrderId = c.Int(),
                        InDate = c.DateTime(nullable: false),
                        DealScheduleId = c.Int(),
                        OilDirtScheduleId = c.Int(),
                        OilDealId = c.Int(),
                        ReadyScheduleId = c.Int(),
                        ArchiveSaleOrder_Id = c.Int(),
                        CancelSaleOrder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DealSchedules", t => t.DealScheduleId)
                .ForeignKey("dbo.OilDeals", t => t.OilDealId)
                .ForeignKey("dbo.OilDirtSchedules", t => t.OilDirtScheduleId)
                .ForeignKey("dbo.ReadySchedules", t => t.ReadyScheduleId)
                .ForeignKey("dbo.SaleOrders", t => t.SaleOrderId)
                .ForeignKey("dbo.ArchiveSaleOrders", t => t.ArchiveSaleOrder_Id)
                .ForeignKey("dbo.CancelSaleOrders", t => t.CancelSaleOrder_Id)
                .Index(t => t.SaleOrderId)
                .Index(t => t.DealScheduleId)
                .Index(t => t.OilDirtScheduleId)
                .Index(t => t.OilDealId)
                .Index(t => t.ReadyScheduleId)
                .Index(t => t.ArchiveSaleOrder_Id)
                .Index(t => t.CancelSaleOrder_Id);
            
            CreateTable(
                "dbo.DealSchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScheduledPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ScheduledSubTradeUnits = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ScheduledPackingsUnits = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ScheduledTradeUnits = c.Decimal(nullable: false, precision: 16, scale: 4),
                        LoadedPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        LoadedSubTradeUnits = c.Decimal(nullable: false, precision: 16, scale: 4),
                        LoadedPackingsUnits = c.Decimal(nullable: false, precision: 16, scale: 4),
                        LoadedTradeUnits = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ReceivedPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ReceivedSubTradeUnits = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ReceivedPackingsUnits = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ReceivedTradeUnits = c.Decimal(nullable: false, precision: 16, scale: 4),
                        DiffLoadedPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        DiffLoadedSubTradeUnits = c.Decimal(nullable: false, precision: 16, scale: 4),
                        DiffLoadedPackingUnits = c.Decimal(nullable: false, precision: 16, scale: 4),
                        DiffLoadedTradeUnits = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TradeUnitTitle = c.String(),
                        PackingUnitTitle = c.String(),
                        ReadyDate = c.DateTime(nullable: false),
                        ArrivalDate = c.DateTime(),
                        IsArrived = c.Boolean(nullable: false),
                        Remarks = c.String(),
                        DriverId = c.Int(),
                        VehicleId = c.Int(),
                        SelectorId = c.Int(),
                        EmployeeId = c.Int(),
                        GoodCompanyId = c.Int(),
                        OmeDealId = c.Int(),
                        AppDealId = c.Int(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        AddedBy = c.String(),
                        UpdatedBy = c.String(),
                        Status = c.Int(nullable: false),
                        IsDispatched = c.Boolean(nullable: false),
                        DispatchedDate = c.DateTime(),
                        IsLoaded = c.Boolean(nullable: false),
                        LoadedDate = c.DateTime(),
                        Unum = c.String(),
                        ScheduleRemarks = c.String(),
                        DispatchRemarks = c.String(),
                        LoadRemarks = c.String(),
                        ReceiveRemarks = c.String(),
                        DispatchLoadPackingDifference = c.Decimal(nullable: false, precision: 16, scale: 4),
                        DispatchLoadTradeUnitDifference = c.Decimal(nullable: false, precision: 16, scale: 4),
                        DispatchLoadSubTradeUnitDifference = c.Decimal(nullable: false, precision: 16, scale: 4),
                        DispatchLoadPriceDifference = c.Decimal(nullable: false, precision: 16, scale: 4),
                        FareDealAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        FareActualPaid = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TransId = c.Int(nullable: false),
                        LaborExpenses = c.Decimal(nullable: false, precision: 16, scale: 4),
                        LaborExpensesDescription = c.String(),
                        TracktorLaborAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppDeals", t => t.AppDealId, cascadeDelete: true)
                .ForeignKey("dbo.Drivers", t => t.DriverId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.OmeDeals", t => t.OmeDealId)
                .ForeignKey("dbo.GoodCompanies", t => t.GoodCompanyId)
                .ForeignKey("dbo.Selectors", t => t.SelectorId)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId)
                .Index(t => t.DriverId)
                .Index(t => t.VehicleId)
                .Index(t => t.SelectorId)
                .Index(t => t.EmployeeId)
                .Index(t => t.GoodCompanyId)
                .Index(t => t.OmeDealId)
                .Index(t => t.AppDealId);
            
            CreateTable(
                "dbo.ScheduleAlarms",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Title = c.String(),
                        GenerateTime = c.DateTime(nullable: false),
                        ActiveDate = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DealSchedules", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Extra = c.String(),
                        IsAvailable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        Designation = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        DateEnded = c.DateTime(nullable: false),
                        Extra = c.String(),
                        CNIC = c.String(),
                        PicData = c.Binary(),
                        ThumbData = c.Binary(),
                        ThumbPicData = c.Binary(),
                        IsAvailable = c.Boolean(nullable: false),
                        GeneralAccountId = c.String(maxLength: 128),
                        Salary = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Balance = c.Decimal(nullable: false, precision: 16, scale: 4),
                        CanLogin = c.Boolean(nullable: false),
                        UserId = c.String(),
                        Password = c.String(),
                        EmployeeType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.AttendanceRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.String(),
                        AttendIn = c.DateTime(nullable: false),
                        AttendOut = c.DateTime(),
                        Employee_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .Index(t => t.Employee_Id);
            
            CreateTable(
                "dbo.CreditEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Date = c.DateTime(nullable: false),
                        Remarks = c.String(),
                        DayBookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmployPayRollEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Month = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        EmployeeSalary = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Unum = c.String(),
                        IsPaid = c.Boolean(nullable: false),
                        ActionDate = c.DateTime(),
                        EmployeeId = c.Int(nullable: false),
                        AllowanceAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        DeductionAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        GrossSalary = c.Decimal(nullable: false, precision: 16, scale: 4),
                        NetSalary = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Remarks = c.String(),
                        XStr = c.String(),
                        EmpAdvances = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmployeeSalaryEntryAllowances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployPayRollEntryId = c.Int(nullable: false),
                        SalaryAllowanceId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployPayRollEntries", t => t.EmployPayRollEntryId, cascadeDelete: true)
                .ForeignKey("dbo.SalaryAllowances", t => t.SalaryAllowanceId, cascadeDelete: true)
                .Index(t => t.EmployPayRollEntryId)
                .Index(t => t.SalaryAllowanceId);
            
            CreateTable(
                "dbo.SalaryAllowances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeeSalaryEntryDeductions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployPayRollEntryId = c.Int(nullable: false),
                        SalaryDeductionId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployPayRollEntries", t => t.EmployPayRollEntryId, cascadeDelete: true)
                .ForeignKey("dbo.SalaryDeductions", t => t.SalaryDeductionId, cascadeDelete: true)
                .Index(t => t.EmployPayRollEntryId)
                .Index(t => t.SalaryDeductionId);
            
            CreateTable(
                "dbo.SalaryDeductions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GoodCompanies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        Owner = c.String(),
                        OwnerContact = c.String(),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Extra = c.String(),
                        GeneralAccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.PackingIssueReceiveRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GoodCompanyId = c.Int(nullable: false),
                        DealPackingId = c.Int(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        DateTime = c.DateTime(nullable: false),
                        RecordType = c.Int(nullable: false),
                        Description = c.String(),
                        Remarks = c.String(),
                        Unum = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DealPackings", t => t.DealPackingId, cascadeDelete: true)
                .ForeignKey("dbo.GoodCompanies", t => t.GoodCompanyId, cascadeDelete: true)
                .Index(t => t.GoodCompanyId)
                .Index(t => t.DealPackingId);
            
            CreateTable(
                "dbo.DealPackings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        UnitPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        GeneralAccountId = c.Int(nullable: false),
                        Account_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.FactoryPackingStockAddedRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DealPackingId = c.Int(nullable: false),
                        QtyAdded = c.Decimal(nullable: false, precision: 16, scale: 4),
                        AddedDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        DealPackingSupplierId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DealPackings", t => t.DealPackingId, cascadeDelete: true)
                .ForeignKey("dbo.DealPackingSuppliers", t => t.DealPackingSupplierId)
                .Index(t => t.DealPackingId)
                .Index(t => t.DealPackingSupplierId);
            
            CreateTable(
                "dbo.DealPackingSuppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        Contact = c.String(),
                        Address = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Remarks = c.String(),
                        GeneralAccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.FactoryPackingStockIssueRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DealPackingId = c.Int(nullable: false),
                        QtyIssued = c.Decimal(nullable: false, precision: 16, scale: 4),
                        IssuedDate = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DealPackings", t => t.DealPackingId, cascadeDelete: true)
                .Index(t => t.DealPackingId);
            
            CreateTable(
                "dbo.FactoryPackingStocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DealPackingId = c.Int(nullable: false),
                        Quantity = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DealPackings", t => t.DealPackingId, cascadeDelete: true)
                .Index(t => t.DealPackingId);
            
            CreateTable(
                "dbo.OmeDeals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        BrokerId = c.Int(nullable: false),
                        DealItemId = c.Int(nullable: false),
                        DealPackingId = c.Int(nullable: false),
                        PerPackingQty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalQty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        PackingQty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TradeUnitId = c.Int(nullable: false),
                        TradeUnitPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalTradeUnits = c.Decimal(nullable: false, precision: 16, scale: 4),
                        DealPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Tax = c.Decimal(nullable: false, precision: 16, scale: 4),
                        FareAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        LaborExpenses = c.Decimal(nullable: false, precision: 16, scale: 4),
                        LaborExpensesDescription = c.Decimal(nullable: false, precision: 16, scale: 4),
                        DealDate = c.DateTime(nullable: false),
                        AddedBy = c.String(),
                        UpdatedBy = c.String(),
                        IsCompleted = c.Boolean(nullable: false),
                        PackingUnitId = c.Int(nullable: false),
                        SegmentsCount = c.Int(nullable: false),
                        Remarks = c.String(),
                        Selector_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brokers", t => t.BrokerId, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.DealItems", t => t.DealItemId, cascadeDelete: true)
                .ForeignKey("dbo.DealPackings", t => t.DealPackingId, cascadeDelete: true)
                .ForeignKey("dbo.PackingUnits", t => t.PackingUnitId, cascadeDelete: true)
                .ForeignKey("dbo.TradeUnits", t => t.TradeUnitId, cascadeDelete: true)
                .ForeignKey("dbo.Selectors", t => t.Selector_Id)
                .Index(t => t.CompanyId)
                .Index(t => t.BrokerId)
                .Index(t => t.DealItemId)
                .Index(t => t.DealPackingId)
                .Index(t => t.TradeUnitId)
                .Index(t => t.PackingUnitId)
                .Index(t => t.Selector_Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Extra = c.String(),
                        GeneralAccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.DealItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        SKU = c.Decimal(nullable: false, precision: 16, scale: 4),
                        GeneralAccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.PackingUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TradeUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        TitleUrdu = c.String(),
                        Qty = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PackingStocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GoodCompanyId = c.Int(nullable: false),
                        DealPackingId = c.Int(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DealPackings", t => t.DealPackingId, cascadeDelete: true)
                .ForeignKey("dbo.GoodCompanies", t => t.GoodCompanyId, cascadeDelete: true)
                .Index(t => t.GoodCompanyId)
                .Index(t => t.DealPackingId);
            
            CreateTable(
                "dbo.ScheduleLoadPackings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DealScheduleId = c.Int(nullable: false),
                        DealPackingId = c.Int(nullable: false),
                        LoadQty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ReceiveQty = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DealPackings", t => t.DealPackingId, cascadeDelete: true)
                .ForeignKey("dbo.DealSchedules", t => t.DealScheduleId, cascadeDelete: true)
                .Index(t => t.DealScheduleId)
                .Index(t => t.DealPackingId);
            
            CreateTable(
                "dbo.ScheduleWeighBridges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DealScheduleId = c.Int(nullable: false),
                        WeighBridgeId = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DealSchedules", t => t.DealScheduleId, cascadeDelete: true)
                .ForeignKey("dbo.WeighBridges", t => t.WeighBridgeId, cascadeDelete: true)
                .Index(t => t.DealScheduleId)
                .Index(t => t.WeighBridgeId);
            
            CreateTable(
                "dbo.WeighBridges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        WeighBrideType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Selectors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Extra = c.String(),
                        GeneralAccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        No = c.String(),
                        VehicleType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OilDeals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DealDate = c.DateTime(nullable: false),
                        ReadyDate = c.DateTime(nullable: false),
                        CompleteDate = c.DateTime(),
                        DealQty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        OilDealBrokerId = c.Int(nullable: false),
                        OilDealItemId = c.Int(nullable: false),
                        OilDealSelectorId = c.Int(),
                        OilDealDriverId = c.Int(),
                        VehicleNo = c.String(),
                        VehicleEmptyWeight = c.Decimal(nullable: false, precision: 16, scale: 4),
                        VehicleScheduleQty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        VehicleFullWeight = c.Decimal(nullable: false, precision: 16, scale: 4),
                        WeighBridgeWeight = c.Decimal(nullable: false, precision: 16, scale: 4),
                        BrokerSharePercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        BrokerShareAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        OilTradeUnitId = c.Int(nullable: false),
                        TotalTradeUnits = c.Decimal(nullable: false, precision: 16, scale: 4),
                        PerTradeUnitPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        NetPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Status = c.Int(nullable: false),
                        Unum = c.String(),
                        LaborExpenses = c.Decimal(nullable: false, precision: 16, scale: 4),
                        LaborExpensesDescription = c.String(),
                        AppUserId = c.String(maxLength: 128),
                        WeighBridgeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId)
                .ForeignKey("dbo.OilDealBrokers", t => t.OilDealBrokerId, cascadeDelete: true)
                .ForeignKey("dbo.OilDealDrivers", t => t.OilDealDriverId)
                .ForeignKey("dbo.OilDealItems", t => t.OilDealItemId, cascadeDelete: true)
                .ForeignKey("dbo.OilDealSelectors", t => t.OilDealSelectorId)
                .ForeignKey("dbo.OilTradeUnits", t => t.OilTradeUnitId, cascadeDelete: true)
                .ForeignKey("dbo.WeighBridges", t => t.WeighBridgeId)
                .Index(t => t.OilDealBrokerId)
                .Index(t => t.OilDealItemId)
                .Index(t => t.OilDealSelectorId)
                .Index(t => t.OilDealDriverId)
                .Index(t => t.OilTradeUnitId)
                .Index(t => t.AppUserId)
                .Index(t => t.WeighBridgeId);
            
            CreateTable(
                "dbo.OilDealBrokers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Extra = c.String(),
                        GeneralAccountId = c.String(maxLength: 128),
                        BrokerExpenseAccountId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.OilDealDrivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Extra = c.String(),
                        CNIC = c.String(),
                        PicData = c.Binary(),
                        ThumbData = c.Binary(),
                        ThumbPicData = c.Binary(),
                        IsAvailable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OilDealItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        TitleUrdu = c.String(),
                        GeneralAccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.OilDealSelectors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Extra = c.String(),
                        CNIC = c.String(),
                        PicData = c.Binary(),
                        ThumbData = c.Binary(),
                        ThumbPicData = c.Binary(),
                        IsAvailable = c.Boolean(nullable: false),
                        GeneralAccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.OilTradeUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        UnitQty = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OilDirtSchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScheduleNo = c.String(),
                        OilDirtSelectorId = c.Int(),
                        OilDirtDriverId = c.Int(),
                        VehicleNo = c.String(),
                        VehicleWeightEmpty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        VehicleWeightFull = c.Decimal(nullable: false, precision: 16, scale: 4),
                        LoadedQty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        DealQty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        WeighBridgeWeight = c.Decimal(nullable: false, precision: 16, scale: 4),
                        WeighBridgeId = c.Int(),
                        BrokerSharePercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        BrokerShareAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalExpectedAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalActualAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalNetAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ReadyDate = c.DateTime(nullable: false),
                        CompleteDate = c.DateTime(),
                        OilDirtDealId = c.Int(nullable: false),
                        PerTradeUnitPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ScheduleDate = c.DateTime(nullable: false),
                        Unum = c.String(),
                        Status = c.Int(nullable: false),
                        TotalTradeUnits = c.Decimal(nullable: false, precision: 16, scale: 4),
                        LaborExpenses = c.Decimal(nullable: false, precision: 16, scale: 4),
                        LaborExpensesDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OilDirtDeals", t => t.OilDirtDealId, cascadeDelete: true)
                .ForeignKey("dbo.OilDirtDrivers", t => t.OilDirtDriverId)
                .ForeignKey("dbo.OilDirtSelectors", t => t.OilDirtSelectorId)
                .ForeignKey("dbo.WeighBridges", t => t.WeighBridgeId)
                .Index(t => t.OilDirtSelectorId)
                .Index(t => t.OilDirtDriverId)
                .Index(t => t.WeighBridgeId)
                .Index(t => t.OilDirtDealId);
            
            CreateTable(
                "dbo.OilDirtDeals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OilDirtBrokerId = c.Int(nullable: false),
                        OilDirtItemId = c.Int(nullable: false),
                        OilDirtTradeUnitId = c.Int(nullable: false),
                        PerTradeUnitPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        NoOfVehicles = c.Int(nullable: false),
                        GenerateDate = c.DateTime(nullable: false),
                        ReadyDate = c.DateTime(nullable: false),
                        DateCompletion = c.DateTime(),
                        Status = c.Int(nullable: false),
                        Description = c.String(),
                        CompletionStatus = c.String(),
                        AppUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId)
                .ForeignKey("dbo.OilDirtBrokers", t => t.OilDirtBrokerId, cascadeDelete: true)
                .ForeignKey("dbo.OilDirtItems", t => t.OilDirtItemId, cascadeDelete: true)
                .ForeignKey("dbo.OilDirtTradeUnits", t => t.OilDirtTradeUnitId, cascadeDelete: true)
                .Index(t => t.OilDirtBrokerId)
                .Index(t => t.OilDirtItemId)
                .Index(t => t.OilDirtTradeUnitId)
                .Index(t => t.AppUserId);
            
            CreateTable(
                "dbo.OilDirtBrokers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Extra = c.String(),
                        GeneralAccountId = c.String(maxLength: 128),
                        BrokerExpenseAccountId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.OilDirtItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        TitleUrdu = c.String(),
                        GeneralAccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.OilDirtTradeUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        UnitQty = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OilDirtDrivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Extra = c.String(),
                        CNIC = c.String(),
                        PicData = c.Binary(),
                        ThumbData = c.Binary(),
                        ThumbPicData = c.Binary(),
                        IsAvailable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OilDirtSelectors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Extra = c.String(),
                        CNIC = c.String(),
                        PicData = c.Binary(),
                        ThumbData = c.Binary(),
                        ThumbPicData = c.Binary(),
                        IsAvailable = c.Boolean(nullable: false),
                        GeneralAccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.ReadySchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScheduleDate = c.DateTime(nullable: false),
                        ReadyDate = c.DateTime(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        NoOfVehicles = c.Int(nullable: false),
                        DispatchedDate = c.DateTime(),
                        ReadyDriverId = c.Int(),
                        ScheduleWeight = c.Decimal(nullable: false, precision: 16, scale: 4),
                        WeighBridgeWeight = c.Decimal(nullable: false, precision: 16, scale: 4),
                        WeighBridgeId = c.Int(),
                        TotalTradeUnits = c.Decimal(nullable: false, precision: 16, scale: 4),
                        PerTradeUnitPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        GrossPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        BrokerSharePercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        BrokerShareAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        NetScheduleAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ReadyDealId = c.Int(nullable: false),
                        ScheduleNo = c.String(),
                        ScheduleType = c.Int(nullable: false),
                        VehicleNo = c.String(),
                        ReadySelectorId = c.Int(),
                        Unum = c.String(),
                        EmptyVehicleWeight = c.Decimal(nullable: false, precision: 16, scale: 4),
                        FullVehicleWeight = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalPackings = c.Decimal(nullable: false, precision: 16, scale: 4),
                        LaborExpenses = c.Decimal(nullable: false, precision: 16, scale: 4),
                        LaborExpensesDescription = c.String(),
                        BardanaAmountExpense = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReadyDrivers", t => t.ReadyDriverId)
                .ForeignKey("dbo.ReadyDeals", t => t.ReadyDealId, cascadeDelete: true)
                .ForeignKey("dbo.ReadySelectors", t => t.ReadySelectorId)
                .ForeignKey("dbo.WeighBridges", t => t.WeighBridgeId)
                .Index(t => t.ReadyDriverId)
                .Index(t => t.WeighBridgeId)
                .Index(t => t.ReadyDealId)
                .Index(t => t.ReadySelectorId);
            
            CreateTable(
                "dbo.Bharthis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BharthiTypeId = c.Int(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Total = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ReadyScheduleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BharthiTypes", t => t.BharthiTypeId, cascadeDelete: true)
                .ForeignKey("dbo.ReadySchedules", t => t.ReadyScheduleId, cascadeDelete: true)
                .Index(t => t.BharthiTypeId)
                .Index(t => t.ReadyScheduleId);
            
            CreateTable(
                "dbo.BharthiTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        UnitQty = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReadyDrivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Extra = c.String(),
                        CNIC = c.String(),
                        PicData = c.Binary(),
                        ThumbData = c.Binary(),
                        ThumbPicData = c.Binary(),
                        IsAvailable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReadyDeals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReadyBrokerId = c.Int(nullable: false),
                        ReadyItemId = c.Int(nullable: false),
                        ReadyTradeUnitId = c.Int(),
                        TotalTradeUnits = c.Decimal(nullable: false, precision: 16, scale: 4),
                        PerTradeUnitPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        EstimatedPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        NoOfVehicles = c.Int(nullable: false),
                        TotalWeight = c.Decimal(nullable: false, precision: 16, scale: 4),
                        DealDate = c.DateTime(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        CompletionStatus = c.String(),
                        DealStatus = c.Int(nullable: false),
                        Description = c.String(),
                        AppUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId)
                .ForeignKey("dbo.ReadyBrokers", t => t.ReadyBrokerId, cascadeDelete: true)
                .ForeignKey("dbo.ReadyItems", t => t.ReadyItemId, cascadeDelete: true)
                .ForeignKey("dbo.ReadyTradeUnits", t => t.ReadyTradeUnitId)
                .Index(t => t.ReadyBrokerId)
                .Index(t => t.ReadyItemId)
                .Index(t => t.ReadyTradeUnitId)
                .Index(t => t.AppUserId);
            
            CreateTable(
                "dbo.ReadyBrokers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Extra = c.String(),
                        GeneralAccountId = c.String(maxLength: 128),
                        BrokerExpenseAccountId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.ReadyItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        TitleUrdu = c.String(),
                        StockQty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        GeneralAccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.ReadyTradeUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        UnitQty = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReadySelectors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Extra = c.String(),
                        CNIC = c.String(),
                        PicData = c.Binary(),
                        ThumbData = c.Binary(),
                        ThumbPicData = c.Binary(),
                        IsAvailable = c.Boolean(nullable: false),
                        GeneralAccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.SaleOrderLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        SaleOrderId = c.Int(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        UnitPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Discount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        NetPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ApplyLaborExpense = c.Boolean(nullable: false),
                        DeductBardanaExpense = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.SaleOrders", t => t.SaleOrderId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.SaleOrderId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        TitleUrdu = c.String(),
                        CostPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ProductTotalUnitPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ProductNetUnitPriceWholeSale = c.Decimal(nullable: false, precision: 16, scale: 4),
                        IsAvailable = c.Boolean(nullable: false),
                        SKU = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Barcode = c.String(),
                        AlertOnSale = c.Boolean(nullable: false),
                        IsDicountable = c.Boolean(nullable: false),
                        ProductCategoryId = c.Int(nullable: false),
                        ProductDiscPercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ProductSizeId = c.Int(nullable: false),
                        ProductPoints = c.Decimal(nullable: false, precision: 16, scale: 4),
                        GeneralAccountId = c.String(maxLength: 128),
                        StockItemId = c.Int(),
                        Weight = c.Decimal(nullable: false, precision: 16, scale: 4),
                        DeductBardanaPacking = c.Boolean(nullable: false),
                        ApplyLaborExpense = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.ProductSizes", t => t.ProductSizeId, cascadeDelete: true)
                .ForeignKey("dbo.StockItems", t => t.StockItemId)
                .Index(t => t.ProductCategoryId)
                .Index(t => t.ProductSizeId)
                .Index(t => t.GeneralAccountId)
                .Index(t => t.StockItemId);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        TitleUrdu = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductSizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        TitleUrdu = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StockItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        TitleUrdu = c.String(),
                        SKU = c.Decimal(nullable: false, precision: 16, scale: 4),
                        UnitPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        IsActive = c.Boolean(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SIEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QtyAdded = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Dated = c.DateTime(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Description = c.String(),
                        StockItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StockItems", t => t.StockItemId, cascadeDelete: true)
                .Index(t => t.StockItemId);
            
            CreateTable(
                "dbo.ArchiveOrderLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ArchiveSaleOrderId = c.Int(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        UnitPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Discount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        NetPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ApplyLaborExpense = c.Boolean(nullable: false),
                        DeductBardanaExpense = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ArchiveSaleOrders", t => t.ArchiveSaleOrderId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ArchiveSaleOrderId);
            
            CreateTable(
                "dbo.CancelSaleOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.String(),
                        CustomerId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        TimpStamp = c.DateTime(nullable: false),
                        BardanaPackingsCount = c.Int(nullable: false),
                        TotalItems = c.Int(nullable: false),
                        UniqueItems = c.Int(nullable: false),
                        TototPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalDiscount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalDiscountPercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        NetPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        IsCredit = c.Boolean(nullable: false),
                        IsWalkIn = c.Boolean(nullable: false),
                        Unum = c.String(),
                        CustomerDiscount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        CustomerDiscountPecentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ServiceCharges = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ServiceChargesPercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        SaleTaxAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        SaleTaxPercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        IsExtraAmounted = c.Boolean(nullable: false),
                        IsExpensed = c.Boolean(nullable: false),
                        OrderDiscount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        OrderDiscountAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        OfferDiscount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        OfferDiscountPercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        AmountGiven = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ChangeGiven = c.Decimal(nullable: false, precision: 16, scale: 4),
                        RemainingAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        OrderType = c.Int(nullable: false),
                        IsDone = c.Boolean(nullable: false),
                        BardanaAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        LaborAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalWeight = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalPackings = c.Decimal(nullable: false, precision: 16, scale: 4),
                        AppUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.AppUserId);
            
            CreateTable(
                "dbo.CancelOrderLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        CancelSaleOrderId = c.Int(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        UnitPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Discount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        NetPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ApplyLaborExpense = c.Boolean(nullable: false),
                        DeductBardanaExpense = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.CancelSaleOrders", t => t.CancelSaleOrderId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CancelSaleOrderId);
            
            CreateTable(
                "dbo.AppUserOptions",
                c => new
                    {
                        AppUserId = c.String(nullable: false, maxLength: 128),
                        CanAddNewCustomer = c.Boolean(nullable: false),
                        CanViewTopCustomers = c.Boolean(nullable: false),
                        CanViewCustomerList = c.Boolean(nullable: false),
                        CanAddNewAppUser = c.Boolean(nullable: false),
                        CanViewAppUsers = c.Boolean(nullable: false),
                        CanAddNewDeliveryBoy = c.Boolean(nullable: false),
                        CanViewDeliveryBoyList = c.Boolean(nullable: false),
                        CanViewDeliveryBoyPerformance = c.Boolean(nullable: false),
                        CanAddNewEmployee = c.Boolean(nullable: false),
                        CanViewEmployeeList = c.Boolean(nullable: false),
                        CanUseAttendanceInForm = c.Boolean(nullable: false),
                        CanUseAttendanceOutForm = c.Boolean(nullable: false),
                        CanViewAttendanceList = c.Boolean(nullable: false),
                        CanAddNewProduct = c.Boolean(nullable: false),
                        CanViewProductList = c.Boolean(nullable: false),
                        CanAddNewProductCategory = c.Boolean(nullable: false),
                        CanViewProductCategories = c.Boolean(nullable: false),
                        CanUseShopInfoForm = c.Boolean(nullable: false),
                        CanWastageItem = c.Boolean(nullable: false),
                        CanViewWastageItems = c.Boolean(nullable: false),
                        CanPlaceNewOrder = c.Boolean(nullable: false),
                        CanViewOrdersList = c.Boolean(nullable: false),
                        CanReprintAnOrder = c.Boolean(nullable: false),
                        CanCanelAnOrder = c.Boolean(nullable: false),
                        CanViewHomeDeliveryOrders = c.Boolean(nullable: false),
                        CanUpdateHomeDeliveryOrder = c.Boolean(nullable: false),
                        CanAddNewDeal = c.Boolean(nullable: false),
                        CanViewDealList = c.Boolean(nullable: false),
                        CanAddNewServiceTable = c.Boolean(nullable: false),
                        CanViewTableServiceList = c.Boolean(nullable: false),
                        CanManageTableServices = c.Boolean(nullable: false),
                        CanViewCancelOrders = c.Boolean(nullable: false),
                        CanViewProfitChart = c.Boolean(nullable: false),
                        CanViewStockDetails = c.Boolean(nullable: false),
                        CanAddDailyExpense = c.Boolean(nullable: false),
                        CanViewExpenses = c.Boolean(nullable: false),
                        CanViewDailyOrdersChart = c.Boolean(nullable: false),
                        CanViewDailyReports = c.Boolean(nullable: false),
                        CanViewDailySalesChart = c.Boolean(nullable: false),
                        CanViewDailyConsumption = c.Boolean(nullable: false),
                        CanViewSmsSubscribers = c.Boolean(nullable: false),
                        CanAddNewSmsSubscriber = c.Boolean(nullable: false),
                        CanViewSmsSettingsForm = c.Boolean(nullable: false),
                        CanSendSmsToCustomers = c.Boolean(nullable: false),
                        CanAddNewRawCategory = c.Boolean(nullable: false),
                        CanViewRawCategories = c.Boolean(nullable: false),
                        CanAddRawUnit = c.Boolean(nullable: false),
                        CanViewRawUnits = c.Boolean(nullable: false),
                        CanAddNewSupplier = c.Boolean(nullable: false),
                        CanViewSuppliers = c.Boolean(nullable: false),
                        CanAddNewRawItem = c.Boolean(nullable: false),
                        CanViewRawItems = c.Boolean(nullable: false),
                        CanAddItemInStock = c.Boolean(nullable: false),
                        CanViewItemAddedStockHistory = c.Boolean(nullable: false),
                        CanConsumeRawItem = c.Boolean(nullable: false),
                        CanViewConsumptionRawItemList = c.Boolean(nullable: false),
                        CanIssueRawItem = c.Boolean(nullable: false),
                        CanViewIssueRawItemRecords = c.Boolean(nullable: false),
                        CanViewPaymentRecords = c.Boolean(nullable: false),
                        CanAddPackings = c.Boolean(nullable: false),
                        CanViewPackings = c.Boolean(nullable: false),
                        CanAddAssetCompany = c.Boolean(nullable: false),
                        CanViewAssetCompanies = c.Boolean(nullable: false),
                        CanAddAssetCategory = c.Boolean(nullable: false),
                        CanViewAssetCategories = c.Boolean(nullable: false),
                        CanAddAssetItem = c.Boolean(nullable: false),
                        CanViewAssetItem = c.Boolean(nullable: false),
                        CanViewAssetItemReport = c.Boolean(nullable: false),
                        CanWastageAssetItem = c.Boolean(nullable: false),
                        Misc1 = c.Boolean(nullable: false),
                        Misc2 = c.Boolean(nullable: false),
                        Misc3 = c.Boolean(nullable: false),
                        Misc4 = c.Boolean(nullable: false),
                        Misc5 = c.Boolean(nullable: false),
                        Misc6 = c.Boolean(nullable: false),
                        Misc7 = c.Boolean(nullable: false),
                        Misc8 = c.Boolean(nullable: false),
                        Misc9 = c.Boolean(nullable: false),
                        Misc10 = c.Boolean(nullable: false),
                        Misc11 = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AppUserId)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId)
                .Index(t => t.AppUserId);
            
            CreateTable(
                "dbo.RawBrokerShareTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AccruedExpenseItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        DebitAccountId = c.String(maxLength: 128),
                        CreditAccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.CreditAccountId)
                .ForeignKey("dbo.AccountBases", t => t.DebitAccountId)
                .Index(t => t.DebitAccountId)
                .Index(t => t.CreditAccountId);
            
            CreateTable(
                "dbo.AdvanceItemRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Qty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Dated = c.DateTime(nullable: false),
                        Remarks = c.String(),
                        RepItemId = c.Int(nullable: false),
                        RepPlaceId = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RepItems", t => t.RepItemId, cascadeDelete: true)
                .ForeignKey("dbo.RepPlaces", t => t.RepPlaceId, cascadeDelete: true)
                .Index(t => t.RepItemId)
                .Index(t => t.RepPlaceId);
            
            CreateTable(
                "dbo.RepItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        SKU = c.Decimal(nullable: false, precision: 16, scale: 4),
                        USCount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        SACount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        UR = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Adv = c.Decimal(nullable: false, precision: 16, scale: 4),
                        UnitValue = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalValue = c.Decimal(nullable: false, precision: 16, scale: 4),
                        LocationId = c.Int(nullable: false),
                        Weight = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ItemCategoryId = c.Int(nullable: false),
                        StoreLocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ItemCategories", t => t.ItemCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .ForeignKey("dbo.StoreLocations", t => t.StoreLocationId, cascadeDelete: true)
                .Index(t => t.LocationId)
                .Index(t => t.ItemCategoryId)
                .Index(t => t.StoreLocationId);
            
            CreateTable(
                "dbo.ItemCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        TitleUrdu = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RepItemDamageRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RepItemId = c.Int(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Dated = c.DateTime(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RepItems", t => t.RepItemId, cascadeDelete: true)
                .Index(t => t.RepItemId);
            
            CreateTable(
                "dbo.ItemPurchaseEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Qty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        UnitPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        PurchaseInvoiceRecordId = c.Int(nullable: false),
                        RepItemId = c.Int(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PurchaseInvoiceRecords", t => t.PurchaseInvoiceRecordId, cascadeDelete: true)
                .ForeignKey("dbo.RepItems", t => t.RepItemId, cascadeDelete: true)
                .Index(t => t.PurchaseInvoiceRecordId)
                .Index(t => t.RepItemId);
            
            CreateTable(
                "dbo.PurchaseInvoiceRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BillId = c.String(),
                        TotalItems = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        PurchaseDate = c.DateTime(nullable: false),
                        InDate = c.DateTime(nullable: false),
                        ItemSupplierId = c.Int(nullable: false),
                        Unum = c.String(),
                        Remarks = c.String(),
                        DaybookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ItemSuppliers", t => t.ItemSupplierId, cascadeDelete: true)
                .Index(t => t.ItemSupplierId);
            
            CreateTable(
                "dbo.ItemSuppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        Address = c.String(),
                        AddressUrdu = c.String(),
                        Phone = c.String(),
                        GeneralAccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameUrdu = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RepEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RepItemId = c.Int(nullable: false),
                        DispatchQty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ReceivedQty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Remarks = c.String(),
                        RepairDispatchRecordId = c.Int(),
                        RepairReceiveRecordId = c.Int(),
                        Direction = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RepairReceiveRecords", t => t.RepairReceiveRecordId)
                .ForeignKey("dbo.RepairDispatchRecords", t => t.RepairDispatchRecordId)
                .ForeignKey("dbo.RepItems", t => t.RepItemId, cascadeDelete: true)
                .Index(t => t.RepItemId)
                .Index(t => t.RepairDispatchRecordId)
                .Index(t => t.RepairReceiveRecordId);
            
            CreateTable(
                "dbo.RepairDispatchRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalItems = c.Decimal(nullable: false, precision: 16, scale: 4),
                        BillNo = c.String(),
                        Date = c.DateTime(nullable: false),
                        RepPlaceId = c.Int(nullable: false),
                        TOPerson = c.String(),
                        ReceivedItems = c.Decimal(nullable: false, precision: 16, scale: 4),
                        RemainingItems = c.Decimal(nullable: false, precision: 16, scale: 4),
                        BillPaid = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Unum = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RepPlaces", t => t.RepPlaceId, cascadeDelete: true)
                .Index(t => t.RepPlaceId);
            
            CreateTable(
                "dbo.RepPlaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        Address = c.String(),
                        AddressUrdu = c.String(),
                        PhoneNo = c.String(),
                        GeneralAccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.RepairReceiveRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BillAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        BillId = c.String(),
                        ReceivedDate = c.DateTime(nullable: false),
                        InDate = c.DateTime(nullable: false),
                        DispatchId = c.Int(nullable: false),
                        Unum = c.String(),
                        RepPlaceId = c.Int(nullable: false),
                        ReceivingPerson = c.String(),
                        QtyReceived = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RepPlaces", t => t.RepPlaceId, cascadeDelete: true)
                .Index(t => t.RepPlaceId);
            
            CreateTable(
                "dbo.RepItemConsumptionRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RepItemId = c.Int(nullable: false),
                        QtyConsumed = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Price = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Dated = c.DateTime(nullable: false),
                        Remarks = c.String(),
                        DaybookId = c.Int(nullable: false),
                        AccountId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RepItems", t => t.RepItemId, cascadeDelete: true)
                .Index(t => t.RepItemId);
            
            CreateTable(
                "dbo.RepItemPreAddRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RepItemId = c.Int(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Dated = c.DateTime(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RepItems", t => t.RepItemId, cascadeDelete: true)
                .Index(t => t.RepItemId);
            
            CreateTable(
                "dbo.StoreLocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LocationName = c.String(),
                        LocationNameUrdu = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rahzams",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ItheyRakh = c.String(),
                        ChalJanDey = c.String(),
                        HunAramEe = c.String(),
                        ChalBasKerYar = c.String(),
                        ChangaPhir = c.String(),
                        RabRakha = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppMessageStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShopMessageId = c.Int(nullable: false),
                        SmsSubsriberId = c.Int(nullable: false),
                        State = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShopMessages", t => t.ShopMessageId, cascadeDelete: true)
                .ForeignKey("dbo.SmsSubscribers", t => t.SmsSubsriberId, cascadeDelete: true)
                .Index(t => t.ShopMessageId)
                .Index(t => t.SmsSubsriberId);
            
            CreateTable(
                "dbo.ShopMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SmsSubscribers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CellNo = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        ShouldReceiveCloseReportAlert = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppSettings",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        PhoneNo = c.String(),
                        Address = c.String(),
                        AddressUrdu = c.String(),
                        MainPrinter = c.String(),
                        ThermalPrinter = c.String(),
                        DaysAlertBeforeReady = c.Int(nullable: false),
                        Logo = c.Binary(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        OilCakeBrokerSharePercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        AllowToChargeOilCakeBrokerSharePercentage = c.Boolean(nullable: false),
                        OilBrokerSharePercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        AllowToChangeOilBrokerSharePercentage = c.Boolean(nullable: false),
                        MailBrokerSharePercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        AllowToChangeMailBrokerSharePercentage = c.Boolean(nullable: false),
                        SalesTaxPercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        OfferDiscountPercentage = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ServiceCharges = c.Decimal(nullable: false, precision: 16, scale: 4),
                        EnableDiscounts = c.Boolean(nullable: false),
                        MasterPassword = c.String(),
                        SecurityPassword = c.String(),
                        ApplyRetailOilCakeLaborExpense = c.Boolean(nullable: false),
                        ApplayWholeSaleOilCakeLaborExpense = c.Boolean(nullable: false),
                        ApplyCrudeOilLaborExpense = c.Boolean(nullable: false),
                        ApplyOilDirtLaborExpense = c.Boolean(nullable: false),
                        ApplyPurchasedItemLaborExpense = c.Boolean(nullable: false),
                        ApplyBardanaExpenseForRetailOilCake = c.Boolean(nullable: false),
                        ApplyBardanaExpenseForWholeSaleOilCake = c.Boolean(nullable: false),
                        bool1 = c.Boolean(nullable: false),
                        bool2 = c.Boolean(nullable: false),
                        bool3 = c.Boolean(nullable: false),
                        PrintFinancialTransactions = c.Boolean(nullable: false),
                        bool5 = c.Boolean(nullable: false),
                        string1 = c.String(),
                        string2 = c.String(),
                        string3 = c.String(),
                        string4 = c.String(),
                        string5 = c.String(),
                        decimal1 = c.Decimal(nullable: false, precision: 16, scale: 4),
                        decimal2 = c.Decimal(nullable: false, precision: 16, scale: 4),
                        decimal3 = c.Decimal(nullable: false, precision: 16, scale: 4),
                        decimal4 = c.Decimal(nullable: false, precision: 16, scale: 4),
                        decimal5 = c.Decimal(nullable: false, precision: 16, scale: 4),
                        GatePassCopies = c.Int(nullable: false),
                        FloorScaleCopies = c.Int(nullable: false),
                        CustomerCopies = c.Int(nullable: false),
                        OfficeCopies = c.Int(nullable: false),
                        int5 = c.Int(nullable: false),
                        int1 = c.Int(nullable: false),
                        int2 = c.Int(nullable: false),
                        dt1 = c.DateTime(nullable: false),
                        dt2 = c.DateTime(nullable: false),
                        dt3 = c.DateTime(nullable: false),
                        dt4 = c.DateTime(nullable: false),
                        bin1 = c.Binary(),
                        bin2 = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DealSegments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TradeUnits = c.Decimal(nullable: false, precision: 16, scale: 4),
                        PackingUnits = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Price = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TradeUnitTitle = c.String(),
                        PackingUnitTitle = c.String(),
                        ReadyDate = c.DateTime(nullable: false),
                        ArrivalDate = c.DateTime(),
                        IsArrived = c.Boolean(nullable: false),
                        OmeDealId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OmeDeals", t => t.OmeDealId, cascadeDelete: true)
                .Index(t => t.OmeDealId);
            
            CreateTable(
                "dbo.DEEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Dated = c.DateTime(nullable: false),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                        SaleAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        SaleQty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EFCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        Description = c.String(),
                        EFCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EFCategories", t => t.EFCategoryId, cascadeDelete: true)
                .Index(t => t.EFCategoryId);
            
            CreateTable(
                "dbo.EFileImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        MimeType = c.String(),
                        PicData = c.Binary(),
                        EFileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EFiles", t => t.EFileId, cascadeDelete: true)
                .Index(t => t.EFileId);
            
            CreateTable(
                "dbo.EntItemEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntItemId = c.Int(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        EntPurchaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EntItems", t => t.EntItemId, cascadeDelete: true)
                .ForeignKey("dbo.EntPurchases", t => t.EntPurchaseId, cascadeDelete: true)
                .Index(t => t.EntItemId)
                .Index(t => t.EntPurchaseId);
            
            CreateTable(
                "dbo.EntItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        NameUrdu = c.String(),
                        QtyConsumed = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EntPurchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Qty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Remarks = c.String(),
                        Dated = c.DateTime(nullable: false),
                        Unum = c.String(),
                        Operator = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EntZeroItemConsumptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Remarks = c.String(),
                        Dated = c.DateTime(nullable: false),
                        Operator = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        QtyConsumed = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExpenseItemEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExpenseItemId = c.Int(nullable: false),
                        ExpenseAmount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ExpenseDate = c.DateTime(nullable: false),
                        Remarks = c.String(),
                        Description = c.String(),
                        ExpenseType = c.Int(nullable: false),
                        DayBookId = c.Int(nullable: false),
                        CreditAccountId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpenseItems", t => t.ExpenseItemId, cascadeDelete: true)
                .Index(t => t.ExpenseItemId);
            
            CreateTable(
                "dbo.ExpenseItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ExpenseType = c.Int(nullable: false),
                        GeneralAccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.GeneralReceipts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        DescriptionUrdu = c.String(),
                        Dated = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Unum = c.String(),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemPlaceSKUs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RepItemId = c.Int(nullable: false),
                        RepPlaceId = c.Int(nullable: false),
                        SKU = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KeyInfoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Key = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        IsValid = c.String(),
                        WahJi = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LongTermAssetItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        InitialPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        CurrentPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        DateAdded = c.DateTime(nullable: false),
                        Description = c.String(),
                        GeneralAccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.MiscTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        debitAccountId = c.String(maxLength: 128),
                        creditAccountId = c.String(maxLength: 128),
                        Amount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Dated = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.creditAccountId)
                .ForeignKey("dbo.AccountBases", t => t.debitAccountId)
                .Index(t => t.debitAccountId)
                .Index(t => t.creditAccountId);
            
            CreateTable(
                "dbo.OilDealAlarms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlarmText = c.String(),
                        AlarmReadyDate = c.DateTime(nullable: false),
                        GenerateDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OilDirtAlarms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        GenerateDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ActiveDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductEntryRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        UnitPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Remarks = c.String(),
                        AddedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ReadyScheduleAlarms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlarmText = c.String(),
                        AlarmReadyDate = c.DateTime(nullable: false),
                        GenerateDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RetailBardanaItemEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RetailBardanaItemId = c.Int(nullable: false),
                        RetailBardanaSupplierId = c.Int(nullable: false),
                        QtyEntered = c.Decimal(nullable: false, precision: 16, scale: 4),
                        UnitPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        TotalPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Remarks = c.String(),
                        Dated = c.DateTime(nullable: false),
                        PurchasePrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        StockPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        RetailPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        CustomerPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        DayBookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RetailBardanaItems", t => t.RetailBardanaItemId, cascadeDelete: true)
                .ForeignKey("dbo.RetailBardanaSuppliers", t => t.RetailBardanaSupplierId, cascadeDelete: true)
                .Index(t => t.RetailBardanaItemId)
                .Index(t => t.RetailBardanaSupplierId);
            
            CreateTable(
                "dbo.RetailBardanaItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        TitleUrdu = c.String(),
                        SKU = c.Decimal(nullable: false, precision: 16, scale: 4),
                        UnitPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        UnitLaborPriceRetail = c.Decimal(nullable: false, precision: 16, scale: 4),
                        UnitLaborPriceWholeSale = c.Decimal(nullable: false, precision: 16, scale: 4),
                        GeneralAccountId = c.String(maxLength: 128),
                        PurchasePrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                        RetailPrice = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.RetailBardanaSuppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameUrdu = c.String(),
                        Contact = c.String(),
                        Address = c.String(),
                        AddressUrdu = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Remarks = c.String(),
                        GeneralAccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountBases", t => t.GeneralAccountId)
                .Index(t => t.GeneralAccountId);
            
            CreateTable(
                "dbo.ReturnRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Item = c.String(),
                        Qty = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Amount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        AddedBy = c.String(),
                        ReturnDate = c.DateTime(nullable: false),
                        InDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.SmsAlertSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Options = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SmsConfigObjs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        string1 = c.String(),
                        string2 = c.String(),
                        int1 = c.Int(nullable: false),
                        int2 = c.Int(nullable: false),
                        double1 = c.Double(nullable: false),
                        double2 = c.Double(nullable: false),
                        decimal1 = c.Decimal(nullable: false, precision: 16, scale: 4),
                        decimal2 = c.Decimal(nullable: false, precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateStoredProcedure(
                "dbo.AccountBase_Insert",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        AccountNo = p.String(),
                        Address = p.String(),
                        AddressUrdu = p.String(),
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        Description = p.String(),
                        AccountNature = p.Int(),
                        ExplicitilyCreated = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[AccountBases]([Id], [AccountNo], [Address], [AddressUrdu], [Title], [TitleUrdu], [Description], [AccountNature], [ExplicitilyCreated], [SubHeadAccountId], [BankName], [Balance], [CrDrType], [ParentAccountId], [TopHeadAccountId], [HeadAccountId], [Discriminator])
                      VALUES (@Id, @AccountNo, @Address, @AddressUrdu, @Title, @TitleUrdu, @Description, @AccountNature, @ExplicitilyCreated, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'AccountBase')"
            );
            
            CreateStoredProcedure(
                "dbo.AccountBase_Update",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        AccountNo = p.String(),
                        Address = p.String(),
                        AddressUrdu = p.String(),
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        Description = p.String(),
                        AccountNature = p.Int(),
                        ExplicitilyCreated = p.Boolean(),
                    },
                body:
                    @"UPDATE [dbo].[AccountBases]
                      SET [AccountNo] = @AccountNo, [Address] = @Address, [AddressUrdu] = @AddressUrdu, [Title] = @Title, [TitleUrdu] = @TitleUrdu, [Description] = @Description, [AccountNature] = @AccountNature, [ExplicitilyCreated] = @ExplicitilyCreated
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.AccountBase_Delete",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                    },
                body:
                    @"DELETE [dbo].[AccountBases]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.GeneralAccount_Insert",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        AccountNo = p.String(),
                        Address = p.String(),
                        AddressUrdu = p.String(),
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        Description = p.String(),
                        AccountNature = p.Int(),
                        ExplicitilyCreated = p.Boolean(),
                        SubHeadAccountId = p.String(maxLength: 128),
                        BankName = p.String(),
                        Balance = p.Decimal(precision: 16, scale: 4),
                        CrDrType = p.Int(),
                        ParentAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[AccountBases]([Id], [AccountNo], [Address], [AddressUrdu], [Title], [TitleUrdu], [Description], [AccountNature], [ExplicitilyCreated], [SubHeadAccountId], [BankName], [Balance], [CrDrType], [ParentAccountId], [TopHeadAccountId], [HeadAccountId], [Discriminator])
                      VALUES (@Id, @AccountNo, @Address, @AddressUrdu, @Title, @TitleUrdu, @Description, @AccountNature, @ExplicitilyCreated, @SubHeadAccountId, @BankName, @Balance, @CrDrType, @ParentAccountId, NULL, NULL, N'GeneralAccount')"
            );
            
            CreateStoredProcedure(
                "dbo.GeneralAccount_Update",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        AccountNo = p.String(),
                        Address = p.String(),
                        AddressUrdu = p.String(),
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        Description = p.String(),
                        AccountNature = p.Int(),
                        ExplicitilyCreated = p.Boolean(),
                        SubHeadAccountId = p.String(maxLength: 128),
                        BankName = p.String(),
                        Balance = p.Decimal(precision: 16, scale: 4),
                        CrDrType = p.Int(),
                        ParentAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[AccountBases]
                      SET [AccountNo] = @AccountNo, [Address] = @Address, [AddressUrdu] = @AddressUrdu, [Title] = @Title, [TitleUrdu] = @TitleUrdu, [Description] = @Description, [AccountNature] = @AccountNature, [ExplicitilyCreated] = @ExplicitilyCreated, [SubHeadAccountId] = @SubHeadAccountId, [BankName] = @BankName, [Balance] = @Balance, [CrDrType] = @CrDrType, [ParentAccountId] = @ParentAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.GeneralAccount_Delete",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                    },
                body:
                    @"DELETE [dbo].[AccountBases]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.SubHeadAccount_Insert",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        AccountNo = p.String(),
                        Address = p.String(),
                        AddressUrdu = p.String(),
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        Description = p.String(),
                        AccountNature = p.Int(),
                        ExplicitilyCreated = p.Boolean(),
                        TopHeadAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[AccountBases]([Id], [AccountNo], [Address], [AddressUrdu], [Title], [TitleUrdu], [Description], [AccountNature], [ExplicitilyCreated], [SubHeadAccountId], [BankName], [Balance], [CrDrType], [ParentAccountId], [TopHeadAccountId], [HeadAccountId], [Discriminator])
                      VALUES (@Id, @AccountNo, @Address, @AddressUrdu, @Title, @TitleUrdu, @Description, @AccountNature, @ExplicitilyCreated, NULL, NULL, NULL, NULL, NULL, @TopHeadAccountId, NULL, N'SubHeadAccount')"
            );
            
            CreateStoredProcedure(
                "dbo.SubHeadAccount_Update",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        AccountNo = p.String(),
                        Address = p.String(),
                        AddressUrdu = p.String(),
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        Description = p.String(),
                        AccountNature = p.Int(),
                        ExplicitilyCreated = p.Boolean(),
                        TopHeadAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[AccountBases]
                      SET [AccountNo] = @AccountNo, [Address] = @Address, [AddressUrdu] = @AddressUrdu, [Title] = @Title, [TitleUrdu] = @TitleUrdu, [Description] = @Description, [AccountNature] = @AccountNature, [ExplicitilyCreated] = @ExplicitilyCreated, [TopHeadAccountId] = @TopHeadAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.SubHeadAccount_Delete",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                    },
                body:
                    @"DELETE [dbo].[AccountBases]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.TopHeadAccount_Insert",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        AccountNo = p.String(),
                        Address = p.String(),
                        AddressUrdu = p.String(),
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        Description = p.String(),
                        AccountNature = p.Int(),
                        ExplicitilyCreated = p.Boolean(),
                        HeadAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[AccountBases]([Id], [AccountNo], [Address], [AddressUrdu], [Title], [TitleUrdu], [Description], [AccountNature], [ExplicitilyCreated], [SubHeadAccountId], [BankName], [Balance], [CrDrType], [ParentAccountId], [TopHeadAccountId], [HeadAccountId], [Discriminator])
                      VALUES (@Id, @AccountNo, @Address, @AddressUrdu, @Title, @TitleUrdu, @Description, @AccountNature, @ExplicitilyCreated, NULL, NULL, NULL, NULL, NULL, NULL, @HeadAccountId, N'TopHeadAccount')"
            );
            
            CreateStoredProcedure(
                "dbo.TopHeadAccount_Update",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        AccountNo = p.String(),
                        Address = p.String(),
                        AddressUrdu = p.String(),
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        Description = p.String(),
                        AccountNature = p.Int(),
                        ExplicitilyCreated = p.Boolean(),
                        HeadAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[AccountBases]
                      SET [AccountNo] = @AccountNo, [Address] = @Address, [AddressUrdu] = @AddressUrdu, [Title] = @Title, [TitleUrdu] = @TitleUrdu, [Description] = @Description, [AccountNature] = @AccountNature, [ExplicitilyCreated] = @ExplicitilyCreated, [HeadAccountId] = @HeadAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.TopHeadAccount_Delete",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                    },
                body:
                    @"DELETE [dbo].[AccountBases]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.HeadAccount_Insert",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        AccountNo = p.String(),
                        Address = p.String(),
                        AddressUrdu = p.String(),
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        Description = p.String(),
                        AccountNature = p.Int(),
                        ExplicitilyCreated = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[AccountBases]([Id], [AccountNo], [Address], [AddressUrdu], [Title], [TitleUrdu], [Description], [AccountNature], [ExplicitilyCreated], [SubHeadAccountId], [BankName], [Balance], [CrDrType], [ParentAccountId], [TopHeadAccountId], [HeadAccountId], [Discriminator])
                      VALUES (@Id, @AccountNo, @Address, @AddressUrdu, @Title, @TitleUrdu, @Description, @AccountNature, @ExplicitilyCreated, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'HeadAccount')"
            );
            
            CreateStoredProcedure(
                "dbo.HeadAccount_Update",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        AccountNo = p.String(),
                        Address = p.String(),
                        AddressUrdu = p.String(),
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        Description = p.String(),
                        AccountNature = p.Int(),
                        ExplicitilyCreated = p.Boolean(),
                    },
                body:
                    @"UPDATE [dbo].[AccountBases]
                      SET [AccountNo] = @AccountNo, [Address] = @Address, [AddressUrdu] = @AddressUrdu, [Title] = @Title, [TitleUrdu] = @TitleUrdu, [Description] = @Description, [AccountNature] = @AccountNature, [ExplicitilyCreated] = @ExplicitilyCreated
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.HeadAccount_Delete",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                    },
                body:
                    @"DELETE [dbo].[AccountBases]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.AccountTransaction_Insert",
                p => new
                    {
                        Date = p.DateTime(),
                        DayBookId = p.Int(),
                        Description = p.String(),
                        DebitAmount = p.Decimal(precision: 16, scale: 4),
                        CreditAmount = p.Decimal(precision: 16, scale: 4),
                        Balance = p.Decimal(precision: 16, scale: 4),
                        AccountTransactionType = p.Int(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[AccountTransactions]([Date], [DayBookId], [Description], [DebitAmount], [CreditAmount], [Balance], [AccountTransactionType], [GeneralAccountId])
                      VALUES (@Date, @DayBookId, @Description, @DebitAmount, @CreditAmount, @Balance, @AccountTransactionType, @GeneralAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[AccountTransactions]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[AccountTransactions] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.AccountTransaction_Update",
                p => new
                    {
                        Id = p.Int(),
                        Date = p.DateTime(),
                        DayBookId = p.Int(),
                        Description = p.String(),
                        DebitAmount = p.Decimal(precision: 16, scale: 4),
                        CreditAmount = p.Decimal(precision: 16, scale: 4),
                        Balance = p.Decimal(precision: 16, scale: 4),
                        AccountTransactionType = p.Int(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[AccountTransactions]
                      SET [Date] = @Date, [DayBookId] = @DayBookId, [Description] = @Description, [DebitAmount] = @DebitAmount, [CreditAmount] = @CreditAmount, [Balance] = @Balance, [AccountTransactionType] = @AccountTransactionType, [GeneralAccountId] = @GeneralAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.AccountTransaction_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[AccountTransactions]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Broker_Insert",
                p => new
                    {
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[Brokers]([Name], [NameUrdu], [Address], [Contact], [Remarks], [IsActive], [DateAdded], [Extra], [GeneralAccountId])
                      VALUES (@Name, @NameUrdu, @Address, @Contact, @Remarks, @IsActive, @DateAdded, @Extra, @GeneralAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Brokers]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Brokers] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Broker_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[Brokers]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu, [Address] = @Address, [Contact] = @Contact, [Remarks] = @Remarks, [IsActive] = @IsActive, [DateAdded] = @DateAdded, [Extra] = @Extra, [GeneralAccountId] = @GeneralAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Broker_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Brokers]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.AppDeal_Insert",
                p => new
                    {
                        CompanyId = p.Int(),
                        BrokerId = p.Int(),
                        DealItemId = p.Int(),
                        DealPackingId = p.Int(),
                        PerPackingQty = p.Decimal(precision: 16, scale: 4),
                        TotalQty = p.Decimal(precision: 16, scale: 4),
                        PackingQty = p.Decimal(precision: 16, scale: 4),
                        TradeUnitId = p.Int(),
                        TotalTradeUnits = p.Decimal(precision: 16, scale: 4),
                        DealPrice = p.Decimal(precision: 16, scale: 4),
                        Tax = p.Decimal(precision: 16, scale: 4),
                        FareAmount = p.Decimal(precision: 16, scale: 4),
                        DealDate = p.DateTime(),
                        AddedBy = p.String(),
                        UpdatedBy = p.String(),
                        IsCompleted = p.Boolean(),
                        PackingUnitId = p.Int(),
                        SchedulesCount = p.Int(),
                        Remarks = p.String(),
                        TradeUnitPrice = p.Decimal(precision: 16, scale: 4),
                        Unum = p.String(),
                        CompletionStatus = p.String(),
                        DealStatus = p.Int(),
                        RawBrokerShareTypeId = p.Int(),
                        BrokerSharePerPackPercentage = p.Decimal(precision: 16, scale: 4),
                        BrokerShareAmount = p.Decimal(precision: 16, scale: 4),
                        AppUserId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[AppDeals]([CompanyId], [BrokerId], [DealItemId], [DealPackingId], [PerPackingQty], [TotalQty], [PackingQty], [TradeUnitId], [TotalTradeUnits], [DealPrice], [Tax], [FareAmount], [DealDate], [AddedBy], [UpdatedBy], [IsCompleted], [PackingUnitId], [SchedulesCount], [Remarks], [TradeUnitPrice], [Unum], [CompletionStatus], [DealStatus], [RawBrokerShareTypeId], [BrokerSharePerPackPercentage], [BrokerShareAmount], [AppUserId])
                      VALUES (@CompanyId, @BrokerId, @DealItemId, @DealPackingId, @PerPackingQty, @TotalQty, @PackingQty, @TradeUnitId, @TotalTradeUnits, @DealPrice, @Tax, @FareAmount, @DealDate, @AddedBy, @UpdatedBy, @IsCompleted, @PackingUnitId, @SchedulesCount, @Remarks, @TradeUnitPrice, @Unum, @CompletionStatus, @DealStatus, @RawBrokerShareTypeId, @BrokerSharePerPackPercentage, @BrokerShareAmount, @AppUserId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[AppDeals]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[AppDeals] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.AppDeal_Update",
                p => new
                    {
                        Id = p.Int(),
                        CompanyId = p.Int(),
                        BrokerId = p.Int(),
                        DealItemId = p.Int(),
                        DealPackingId = p.Int(),
                        PerPackingQty = p.Decimal(precision: 16, scale: 4),
                        TotalQty = p.Decimal(precision: 16, scale: 4),
                        PackingQty = p.Decimal(precision: 16, scale: 4),
                        TradeUnitId = p.Int(),
                        TotalTradeUnits = p.Decimal(precision: 16, scale: 4),
                        DealPrice = p.Decimal(precision: 16, scale: 4),
                        Tax = p.Decimal(precision: 16, scale: 4),
                        FareAmount = p.Decimal(precision: 16, scale: 4),
                        DealDate = p.DateTime(),
                        AddedBy = p.String(),
                        UpdatedBy = p.String(),
                        IsCompleted = p.Boolean(),
                        PackingUnitId = p.Int(),
                        SchedulesCount = p.Int(),
                        Remarks = p.String(),
                        TradeUnitPrice = p.Decimal(precision: 16, scale: 4),
                        Unum = p.String(),
                        CompletionStatus = p.String(),
                        DealStatus = p.Int(),
                        RawBrokerShareTypeId = p.Int(),
                        BrokerSharePerPackPercentage = p.Decimal(precision: 16, scale: 4),
                        BrokerShareAmount = p.Decimal(precision: 16, scale: 4),
                        AppUserId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[AppDeals]
                      SET [CompanyId] = @CompanyId, [BrokerId] = @BrokerId, [DealItemId] = @DealItemId, [DealPackingId] = @DealPackingId, [PerPackingQty] = @PerPackingQty, [TotalQty] = @TotalQty, [PackingQty] = @PackingQty, [TradeUnitId] = @TradeUnitId, [TotalTradeUnits] = @TotalTradeUnits, [DealPrice] = @DealPrice, [Tax] = @Tax, [FareAmount] = @FareAmount, [DealDate] = @DealDate, [AddedBy] = @AddedBy, [UpdatedBy] = @UpdatedBy, [IsCompleted] = @IsCompleted, [PackingUnitId] = @PackingUnitId, [SchedulesCount] = @SchedulesCount, [Remarks] = @Remarks, [TradeUnitPrice] = @TradeUnitPrice, [Unum] = @Unum, [CompletionStatus] = @CompletionStatus, [DealStatus] = @DealStatus, [RawBrokerShareTypeId] = @RawBrokerShareTypeId, [BrokerSharePerPackPercentage] = @BrokerSharePerPackPercentage, [BrokerShareAmount] = @BrokerShareAmount, [AppUserId] = @AppUserId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.AppDeal_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[AppDeals]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.AppUser_Insert",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        Password = p.String(),
                        Name = p.String(),
                        Contact = p.String(),
                        IsActive = p.Boolean(),
                        Options = p.String(),
                    },
                body:
                    @"INSERT [dbo].[AppUsers]([Id], [Password], [Name], [Contact], [IsActive], [Options])
                      VALUES (@Id, @Password, @Name, @Contact, @IsActive, @Options)"
            );
            
            CreateStoredProcedure(
                "dbo.AppUser_Update",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        Password = p.String(),
                        Name = p.String(),
                        Contact = p.String(),
                        IsActive = p.Boolean(),
                        Options = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[AppUsers]
                      SET [Password] = @Password, [Name] = @Name, [Contact] = @Contact, [IsActive] = @IsActive, [Options] = @Options
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.AppUser_Delete",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                    },
                body:
                    @"DELETE [dbo].[AppUsers]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ArchiveSaleOrder_Insert",
                p => new
                    {
                        OrderId = p.String(),
                        CustomerId = p.Int(),
                        OrderDate = p.DateTime(),
                        TimpStamp = p.DateTime(),
                        TotalItems = p.Int(),
                        UniqueItems = p.Int(),
                        TototPrice = p.Decimal(precision: 16, scale: 4),
                        BardanaPackingsCount = p.Int(),
                        TotalDiscount = p.Decimal(precision: 16, scale: 4),
                        TotalDiscountPercentage = p.Decimal(precision: 16, scale: 4),
                        NetPrice = p.Decimal(precision: 16, scale: 4),
                        IsCredit = p.Boolean(),
                        IsWalkIn = p.Boolean(),
                        Unum = p.String(),
                        IsExtraAmounted = p.Boolean(),
                        IsExpensed = p.Boolean(),
                        CustomerDiscount = p.Decimal(precision: 16, scale: 4),
                        CustomerDiscountPecentage = p.Decimal(precision: 16, scale: 4),
                        ServiceCharges = p.Decimal(precision: 16, scale: 4),
                        ServiceChargesPercentage = p.Decimal(precision: 16, scale: 4),
                        SaleTaxAmount = p.Decimal(precision: 16, scale: 4),
                        SaleTaxPercentage = p.Decimal(precision: 16, scale: 4),
                        AppUserId = p.String(maxLength: 128),
                        OrderDiscount = p.Decimal(precision: 16, scale: 4),
                        OrderDiscountAmount = p.Decimal(precision: 16, scale: 4),
                        OfferDiscount = p.Decimal(precision: 16, scale: 4),
                        OfferDiscountPercentage = p.Decimal(precision: 16, scale: 4),
                        AmountGiven = p.Decimal(precision: 16, scale: 4),
                        ChangeGiven = p.Decimal(precision: 16, scale: 4),
                        RemainingAmount = p.Decimal(precision: 16, scale: 4),
                        OrderType = p.Int(),
                        IsDone = p.Boolean(),
                        BardanaAmount = p.Decimal(precision: 16, scale: 4),
                        LaborAmount = p.Decimal(precision: 16, scale: 4),
                        TotalWeight = p.Decimal(precision: 16, scale: 4),
                        TotalPackings = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[ArchiveSaleOrders]([OrderId], [CustomerId], [OrderDate], [TimpStamp], [TotalItems], [UniqueItems], [TototPrice], [BardanaPackingsCount], [TotalDiscount], [TotalDiscountPercentage], [NetPrice], [IsCredit], [IsWalkIn], [Unum], [IsExtraAmounted], [IsExpensed], [CustomerDiscount], [CustomerDiscountPecentage], [ServiceCharges], [ServiceChargesPercentage], [SaleTaxAmount], [SaleTaxPercentage], [AppUserId], [OrderDiscount], [OrderDiscountAmount], [OfferDiscount], [OfferDiscountPercentage], [AmountGiven], [ChangeGiven], [RemainingAmount], [OrderType], [IsDone], [BardanaAmount], [LaborAmount], [TotalWeight], [TotalPackings])
                      VALUES (@OrderId, @CustomerId, @OrderDate, @TimpStamp, @TotalItems, @UniqueItems, @TototPrice, @BardanaPackingsCount, @TotalDiscount, @TotalDiscountPercentage, @NetPrice, @IsCredit, @IsWalkIn, @Unum, @IsExtraAmounted, @IsExpensed, @CustomerDiscount, @CustomerDiscountPecentage, @ServiceCharges, @ServiceChargesPercentage, @SaleTaxAmount, @SaleTaxPercentage, @AppUserId, @OrderDiscount, @OrderDiscountAmount, @OfferDiscount, @OfferDiscountPercentage, @AmountGiven, @ChangeGiven, @RemainingAmount, @OrderType, @IsDone, @BardanaAmount, @LaborAmount, @TotalWeight, @TotalPackings)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ArchiveSaleOrders]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ArchiveSaleOrders] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ArchiveSaleOrder_Update",
                p => new
                    {
                        Id = p.Int(),
                        OrderId = p.String(),
                        CustomerId = p.Int(),
                        OrderDate = p.DateTime(),
                        TimpStamp = p.DateTime(),
                        TotalItems = p.Int(),
                        UniqueItems = p.Int(),
                        TototPrice = p.Decimal(precision: 16, scale: 4),
                        BardanaPackingsCount = p.Int(),
                        TotalDiscount = p.Decimal(precision: 16, scale: 4),
                        TotalDiscountPercentage = p.Decimal(precision: 16, scale: 4),
                        NetPrice = p.Decimal(precision: 16, scale: 4),
                        IsCredit = p.Boolean(),
                        IsWalkIn = p.Boolean(),
                        Unum = p.String(),
                        IsExtraAmounted = p.Boolean(),
                        IsExpensed = p.Boolean(),
                        CustomerDiscount = p.Decimal(precision: 16, scale: 4),
                        CustomerDiscountPecentage = p.Decimal(precision: 16, scale: 4),
                        ServiceCharges = p.Decimal(precision: 16, scale: 4),
                        ServiceChargesPercentage = p.Decimal(precision: 16, scale: 4),
                        SaleTaxAmount = p.Decimal(precision: 16, scale: 4),
                        SaleTaxPercentage = p.Decimal(precision: 16, scale: 4),
                        AppUserId = p.String(maxLength: 128),
                        OrderDiscount = p.Decimal(precision: 16, scale: 4),
                        OrderDiscountAmount = p.Decimal(precision: 16, scale: 4),
                        OfferDiscount = p.Decimal(precision: 16, scale: 4),
                        OfferDiscountPercentage = p.Decimal(precision: 16, scale: 4),
                        AmountGiven = p.Decimal(precision: 16, scale: 4),
                        ChangeGiven = p.Decimal(precision: 16, scale: 4),
                        RemainingAmount = p.Decimal(precision: 16, scale: 4),
                        OrderType = p.Int(),
                        IsDone = p.Boolean(),
                        BardanaAmount = p.Decimal(precision: 16, scale: 4),
                        LaborAmount = p.Decimal(precision: 16, scale: 4),
                        TotalWeight = p.Decimal(precision: 16, scale: 4),
                        TotalPackings = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[ArchiveSaleOrders]
                      SET [OrderId] = @OrderId, [CustomerId] = @CustomerId, [OrderDate] = @OrderDate, [TimpStamp] = @TimpStamp, [TotalItems] = @TotalItems, [UniqueItems] = @UniqueItems, [TototPrice] = @TototPrice, [BardanaPackingsCount] = @BardanaPackingsCount, [TotalDiscount] = @TotalDiscount, [TotalDiscountPercentage] = @TotalDiscountPercentage, [NetPrice] = @NetPrice, [IsCredit] = @IsCredit, [IsWalkIn] = @IsWalkIn, [Unum] = @Unum, [IsExtraAmounted] = @IsExtraAmounted, [IsExpensed] = @IsExpensed, [CustomerDiscount] = @CustomerDiscount, [CustomerDiscountPecentage] = @CustomerDiscountPecentage, [ServiceCharges] = @ServiceCharges, [ServiceChargesPercentage] = @ServiceChargesPercentage, [SaleTaxAmount] = @SaleTaxAmount, [SaleTaxPercentage] = @SaleTaxPercentage, [AppUserId] = @AppUserId, [OrderDiscount] = @OrderDiscount, [OrderDiscountAmount] = @OrderDiscountAmount, [OfferDiscount] = @OfferDiscount, [OfferDiscountPercentage] = @OfferDiscountPercentage, [AmountGiven] = @AmountGiven, [ChangeGiven] = @ChangeGiven, [RemainingAmount] = @RemainingAmount, [OrderType] = @OrderType, [IsDone] = @IsDone, [BardanaAmount] = @BardanaAmount, [LaborAmount] = @LaborAmount, [TotalWeight] = @TotalWeight, [TotalPackings] = @TotalPackings
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ArchiveSaleOrder_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ArchiveSaleOrders]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Customer_Insert",
                p => new
                    {
                        CardNo = p.String(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        AddressUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        CustomerCategoryId = p.Int(),
                        CNIC = p.String(),
                        Points = p.Decimal(precision: 16, scale: 4),
                        ApplyCardDiscount = p.Boolean(),
                        CardStartDate = p.DateTime(),
                        CardEndDate = p.DateTime(),
                        DiscountPercentage = p.Decimal(precision: 16, scale: 4),
                        IsEmployee = p.Boolean(),
                        GeneralAccountId = p.String(maxLength: 128),
                        EmpId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Customers]([CardNo], [Name], [NameUrdu], [AddressUrdu], [Address], [Contact], [Remarks], [IsActive], [DateAdded], [CustomerCategoryId], [CNIC], [Points], [ApplyCardDiscount], [CardStartDate], [CardEndDate], [DiscountPercentage], [IsEmployee], [GeneralAccountId], [EmpId])
                      VALUES (@CardNo, @Name, @NameUrdu, @AddressUrdu, @Address, @Contact, @Remarks, @IsActive, @DateAdded, @CustomerCategoryId, @CNIC, @Points, @ApplyCardDiscount, @CardStartDate, @CardEndDate, @DiscountPercentage, @IsEmployee, @GeneralAccountId, @EmpId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Customers]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Customers] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Customer_Update",
                p => new
                    {
                        Id = p.Int(),
                        CardNo = p.String(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        AddressUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        CustomerCategoryId = p.Int(),
                        CNIC = p.String(),
                        Points = p.Decimal(precision: 16, scale: 4),
                        ApplyCardDiscount = p.Boolean(),
                        CardStartDate = p.DateTime(),
                        CardEndDate = p.DateTime(),
                        DiscountPercentage = p.Decimal(precision: 16, scale: 4),
                        IsEmployee = p.Boolean(),
                        GeneralAccountId = p.String(maxLength: 128),
                        EmpId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Customers]
                      SET [CardNo] = @CardNo, [Name] = @Name, [NameUrdu] = @NameUrdu, [AddressUrdu] = @AddressUrdu, [Address] = @Address, [Contact] = @Contact, [Remarks] = @Remarks, [IsActive] = @IsActive, [DateAdded] = @DateAdded, [CustomerCategoryId] = @CustomerCategoryId, [CNIC] = @CNIC, [Points] = @Points, [ApplyCardDiscount] = @ApplyCardDiscount, [CardStartDate] = @CardStartDate, [CardEndDate] = @CardEndDate, [DiscountPercentage] = @DiscountPercentage, [IsEmployee] = @IsEmployee, [GeneralAccountId] = @GeneralAccountId, [EmpId] = @EmpId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Customer_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Customers]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.CustomerCategory_Insert",
                p => new
                    {
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        Discount = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[CustomerCategories]([Title], [TitleUrdu], [Discount])
                      VALUES (@Title, @TitleUrdu, @Discount)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[CustomerCategories]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[CustomerCategories] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.CustomerCategory_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        Discount = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[CustomerCategories]
                      SET [Title] = @Title, [TitleUrdu] = @TitleUrdu, [Discount] = @Discount
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.CustomerCategory_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[CustomerCategories]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.SaleOrder_Insert",
                p => new
                    {
                        OrderId = p.String(),
                        CustomerId = p.Int(),
                        OrderDate = p.DateTime(),
                        TimpStamp = p.DateTime(),
                        TotalItems = p.Int(),
                        UniqueItems = p.Int(),
                        TototPrice = p.Decimal(precision: 16, scale: 4),
                        TotalDiscount = p.Decimal(precision: 16, scale: 4),
                        TotalDiscountPercentage = p.Decimal(precision: 16, scale: 4),
                        NetPrice = p.Decimal(precision: 16, scale: 4),
                        IsCredit = p.Boolean(),
                        IsWalkIn = p.Boolean(),
                        Unum = p.String(),
                        IsExpensed = p.Boolean(),
                        CustomerDiscount = p.Decimal(precision: 16, scale: 4),
                        CustomerDiscountPecentage = p.Decimal(precision: 16, scale: 4),
                        ServiceCharges = p.Decimal(precision: 16, scale: 4),
                        ServiceChargesPercentage = p.Decimal(precision: 16, scale: 4),
                        SaleTaxAmount = p.Decimal(precision: 16, scale: 4),
                        SaleTaxPercentage = p.Decimal(precision: 16, scale: 4),
                        IsExtraAmounted = p.Boolean(),
                        OrderDiscount = p.Decimal(precision: 16, scale: 4),
                        OrderDiscountAmount = p.Decimal(precision: 16, scale: 4),
                        OfferDiscount = p.Decimal(precision: 16, scale: 4),
                        OfferDiscountPercentage = p.Decimal(precision: 16, scale: 4),
                        AmountGiven = p.Decimal(precision: 16, scale: 4),
                        ChangeGiven = p.Decimal(precision: 16, scale: 4),
                        RemainingAmount = p.Decimal(precision: 16, scale: 4),
                        OrderType = p.Int(),
                        IsDone = p.Boolean(),
                        BardanaAmount = p.Decimal(precision: 16, scale: 4),
                        LaborAmount = p.Decimal(precision: 16, scale: 4),
                        TotalWeight = p.Decimal(precision: 16, scale: 4),
                        TotalPackings = p.Decimal(precision: 16, scale: 4),
                        AppUserId = p.String(maxLength: 128),
                        BardanaPackingsCount = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[SaleOrders]([OrderId], [CustomerId], [OrderDate], [TimpStamp], [TotalItems], [UniqueItems], [TototPrice], [TotalDiscount], [TotalDiscountPercentage], [NetPrice], [IsCredit], [IsWalkIn], [Unum], [IsExpensed], [CustomerDiscount], [CustomerDiscountPecentage], [ServiceCharges], [ServiceChargesPercentage], [SaleTaxAmount], [SaleTaxPercentage], [IsExtraAmounted], [OrderDiscount], [OrderDiscountAmount], [OfferDiscount], [OfferDiscountPercentage], [AmountGiven], [ChangeGiven], [RemainingAmount], [OrderType], [IsDone], [BardanaAmount], [LaborAmount], [TotalWeight], [TotalPackings], [AppUserId], [BardanaPackingsCount])
                      VALUES (@OrderId, @CustomerId, @OrderDate, @TimpStamp, @TotalItems, @UniqueItems, @TototPrice, @TotalDiscount, @TotalDiscountPercentage, @NetPrice, @IsCredit, @IsWalkIn, @Unum, @IsExpensed, @CustomerDiscount, @CustomerDiscountPecentage, @ServiceCharges, @ServiceChargesPercentage, @SaleTaxAmount, @SaleTaxPercentage, @IsExtraAmounted, @OrderDiscount, @OrderDiscountAmount, @OfferDiscount, @OfferDiscountPercentage, @AmountGiven, @ChangeGiven, @RemainingAmount, @OrderType, @IsDone, @BardanaAmount, @LaborAmount, @TotalWeight, @TotalPackings, @AppUserId, @BardanaPackingsCount)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[SaleOrders]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[SaleOrders] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.SaleOrder_Update",
                p => new
                    {
                        Id = p.Int(),
                        OrderId = p.String(),
                        CustomerId = p.Int(),
                        OrderDate = p.DateTime(),
                        TimpStamp = p.DateTime(),
                        TotalItems = p.Int(),
                        UniqueItems = p.Int(),
                        TototPrice = p.Decimal(precision: 16, scale: 4),
                        TotalDiscount = p.Decimal(precision: 16, scale: 4),
                        TotalDiscountPercentage = p.Decimal(precision: 16, scale: 4),
                        NetPrice = p.Decimal(precision: 16, scale: 4),
                        IsCredit = p.Boolean(),
                        IsWalkIn = p.Boolean(),
                        Unum = p.String(),
                        IsExpensed = p.Boolean(),
                        CustomerDiscount = p.Decimal(precision: 16, scale: 4),
                        CustomerDiscountPecentage = p.Decimal(precision: 16, scale: 4),
                        ServiceCharges = p.Decimal(precision: 16, scale: 4),
                        ServiceChargesPercentage = p.Decimal(precision: 16, scale: 4),
                        SaleTaxAmount = p.Decimal(precision: 16, scale: 4),
                        SaleTaxPercentage = p.Decimal(precision: 16, scale: 4),
                        IsExtraAmounted = p.Boolean(),
                        OrderDiscount = p.Decimal(precision: 16, scale: 4),
                        OrderDiscountAmount = p.Decimal(precision: 16, scale: 4),
                        OfferDiscount = p.Decimal(precision: 16, scale: 4),
                        OfferDiscountPercentage = p.Decimal(precision: 16, scale: 4),
                        AmountGiven = p.Decimal(precision: 16, scale: 4),
                        ChangeGiven = p.Decimal(precision: 16, scale: 4),
                        RemainingAmount = p.Decimal(precision: 16, scale: 4),
                        OrderType = p.Int(),
                        IsDone = p.Boolean(),
                        BardanaAmount = p.Decimal(precision: 16, scale: 4),
                        LaborAmount = p.Decimal(precision: 16, scale: 4),
                        TotalWeight = p.Decimal(precision: 16, scale: 4),
                        TotalPackings = p.Decimal(precision: 16, scale: 4),
                        AppUserId = p.String(maxLength: 128),
                        BardanaPackingsCount = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[SaleOrders]
                      SET [OrderId] = @OrderId, [CustomerId] = @CustomerId, [OrderDate] = @OrderDate, [TimpStamp] = @TimpStamp, [TotalItems] = @TotalItems, [UniqueItems] = @UniqueItems, [TototPrice] = @TototPrice, [TotalDiscount] = @TotalDiscount, [TotalDiscountPercentage] = @TotalDiscountPercentage, [NetPrice] = @NetPrice, [IsCredit] = @IsCredit, [IsWalkIn] = @IsWalkIn, [Unum] = @Unum, [IsExpensed] = @IsExpensed, [CustomerDiscount] = @CustomerDiscount, [CustomerDiscountPecentage] = @CustomerDiscountPecentage, [ServiceCharges] = @ServiceCharges, [ServiceChargesPercentage] = @ServiceChargesPercentage, [SaleTaxAmount] = @SaleTaxAmount, [SaleTaxPercentage] = @SaleTaxPercentage, [IsExtraAmounted] = @IsExtraAmounted, [OrderDiscount] = @OrderDiscount, [OrderDiscountAmount] = @OrderDiscountAmount, [OfferDiscount] = @OfferDiscount, [OfferDiscountPercentage] = @OfferDiscountPercentage, [AmountGiven] = @AmountGiven, [ChangeGiven] = @ChangeGiven, [RemainingAmount] = @RemainingAmount, [OrderType] = @OrderType, [IsDone] = @IsDone, [BardanaAmount] = @BardanaAmount, [LaborAmount] = @LaborAmount, [TotalWeight] = @TotalWeight, [TotalPackings] = @TotalPackings, [AppUserId] = @AppUserId, [BardanaPackingsCount] = @BardanaPackingsCount
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.SaleOrder_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[SaleOrders]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.DayBook_Insert",
                p => new
                    {
                        Date = p.DateTime(),
                        Description = p.String(),
                        DebitTrace = p.String(),
                        CreditTrace = p.String(),
                        Amount = p.Decimal(precision: 16, scale: 4),
                        IsReversed = p.Boolean(),
                        DebitAccountId = p.String(),
                        CreditAccountId = p.String(),
                        CanRollBack = p.Boolean(),
                        SaleOrderId = p.Int(),
                        InDate = p.DateTime(),
                        DealScheduleId = p.Int(),
                        OilDirtScheduleId = p.Int(),
                        OilDealId = p.Int(),
                        ReadyScheduleId = p.Int(),
                        ArchiveSaleOrder_Id = p.Int(),
                        CancelSaleOrder_Id = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[DayBooks]([Date], [Description], [DebitTrace], [CreditTrace], [Amount], [IsReversed], [DebitAccountId], [CreditAccountId], [CanRollBack], [SaleOrderId], [InDate], [DealScheduleId], [OilDirtScheduleId], [OilDealId], [ReadyScheduleId], [ArchiveSaleOrder_Id], [CancelSaleOrder_Id])
                      VALUES (@Date, @Description, @DebitTrace, @CreditTrace, @Amount, @IsReversed, @DebitAccountId, @CreditAccountId, @CanRollBack, @SaleOrderId, @InDate, @DealScheduleId, @OilDirtScheduleId, @OilDealId, @ReadyScheduleId, @ArchiveSaleOrder_Id, @CancelSaleOrder_Id)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[DayBooks]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[DayBooks] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.DayBook_Update",
                p => new
                    {
                        Id = p.Int(),
                        Date = p.DateTime(),
                        Description = p.String(),
                        DebitTrace = p.String(),
                        CreditTrace = p.String(),
                        Amount = p.Decimal(precision: 16, scale: 4),
                        IsReversed = p.Boolean(),
                        DebitAccountId = p.String(),
                        CreditAccountId = p.String(),
                        CanRollBack = p.Boolean(),
                        SaleOrderId = p.Int(),
                        InDate = p.DateTime(),
                        DealScheduleId = p.Int(),
                        OilDirtScheduleId = p.Int(),
                        OilDealId = p.Int(),
                        ReadyScheduleId = p.Int(),
                        ArchiveSaleOrder_Id = p.Int(),
                        CancelSaleOrder_Id = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[DayBooks]
                      SET [Date] = @Date, [Description] = @Description, [DebitTrace] = @DebitTrace, [CreditTrace] = @CreditTrace, [Amount] = @Amount, [IsReversed] = @IsReversed, [DebitAccountId] = @DebitAccountId, [CreditAccountId] = @CreditAccountId, [CanRollBack] = @CanRollBack, [SaleOrderId] = @SaleOrderId, [InDate] = @InDate, [DealScheduleId] = @DealScheduleId, [OilDirtScheduleId] = @OilDirtScheduleId, [OilDealId] = @OilDealId, [ReadyScheduleId] = @ReadyScheduleId, [ArchiveSaleOrder_Id] = @ArchiveSaleOrder_Id, [CancelSaleOrder_Id] = @CancelSaleOrder_Id
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.DayBook_Delete",
                p => new
                    {
                        Id = p.Int(),
                        ArchiveSaleOrder_Id = p.Int(),
                        CancelSaleOrder_Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[DayBooks]
                      WHERE ((([Id] = @Id) AND (([ArchiveSaleOrder_Id] = @ArchiveSaleOrder_Id) OR ([ArchiveSaleOrder_Id] IS NULL AND @ArchiveSaleOrder_Id IS NULL))) AND (([CancelSaleOrder_Id] = @CancelSaleOrder_Id) OR ([CancelSaleOrder_Id] IS NULL AND @CancelSaleOrder_Id IS NULL)))"
            );
            
            CreateStoredProcedure(
                "dbo.DealSchedule_Insert",
                p => new
                    {
                        ScheduledPrice = p.Decimal(precision: 16, scale: 4),
                        ScheduledSubTradeUnits = p.Decimal(precision: 16, scale: 4),
                        ScheduledPackingsUnits = p.Decimal(precision: 16, scale: 4),
                        ScheduledTradeUnits = p.Decimal(precision: 16, scale: 4),
                        LoadedPrice = p.Decimal(precision: 16, scale: 4),
                        LoadedSubTradeUnits = p.Decimal(precision: 16, scale: 4),
                        LoadedPackingsUnits = p.Decimal(precision: 16, scale: 4),
                        LoadedTradeUnits = p.Decimal(precision: 16, scale: 4),
                        ReceivedPrice = p.Decimal(precision: 16, scale: 4),
                        ReceivedSubTradeUnits = p.Decimal(precision: 16, scale: 4),
                        ReceivedPackingsUnits = p.Decimal(precision: 16, scale: 4),
                        ReceivedTradeUnits = p.Decimal(precision: 16, scale: 4),
                        DiffLoadedPrice = p.Decimal(precision: 16, scale: 4),
                        DiffLoadedSubTradeUnits = p.Decimal(precision: 16, scale: 4),
                        DiffLoadedPackingUnits = p.Decimal(precision: 16, scale: 4),
                        DiffLoadedTradeUnits = p.Decimal(precision: 16, scale: 4),
                        TradeUnitTitle = p.String(),
                        PackingUnitTitle = p.String(),
                        ReadyDate = p.DateTime(),
                        ArrivalDate = p.DateTime(),
                        IsArrived = p.Boolean(),
                        Remarks = p.String(),
                        DriverId = p.Int(),
                        VehicleId = p.Int(),
                        SelectorId = p.Int(),
                        EmployeeId = p.Int(),
                        GoodCompanyId = p.Int(),
                        OmeDealId = p.Int(),
                        AppDealId = p.Int(),
                        AddedDate = p.DateTime(),
                        AddedBy = p.String(),
                        UpdatedBy = p.String(),
                        Status = p.Int(),
                        IsDispatched = p.Boolean(),
                        DispatchedDate = p.DateTime(),
                        IsLoaded = p.Boolean(),
                        LoadedDate = p.DateTime(),
                        Unum = p.String(),
                        ScheduleRemarks = p.String(),
                        DispatchRemarks = p.String(),
                        LoadRemarks = p.String(),
                        ReceiveRemarks = p.String(),
                        DispatchLoadPackingDifference = p.Decimal(precision: 16, scale: 4),
                        DispatchLoadTradeUnitDifference = p.Decimal(precision: 16, scale: 4),
                        DispatchLoadSubTradeUnitDifference = p.Decimal(precision: 16, scale: 4),
                        DispatchLoadPriceDifference = p.Decimal(precision: 16, scale: 4),
                        FareDealAmount = p.Decimal(precision: 16, scale: 4),
                        FareActualPaid = p.Decimal(precision: 16, scale: 4),
                        TransId = p.Int(),
                        LaborExpenses = p.Decimal(precision: 16, scale: 4),
                        LaborExpensesDescription = p.String(),
                        TracktorLaborAmount = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[DealSchedules]([ScheduledPrice], [ScheduledSubTradeUnits], [ScheduledPackingsUnits], [ScheduledTradeUnits], [LoadedPrice], [LoadedSubTradeUnits], [LoadedPackingsUnits], [LoadedTradeUnits], [ReceivedPrice], [ReceivedSubTradeUnits], [ReceivedPackingsUnits], [ReceivedTradeUnits], [DiffLoadedPrice], [DiffLoadedSubTradeUnits], [DiffLoadedPackingUnits], [DiffLoadedTradeUnits], [TradeUnitTitle], [PackingUnitTitle], [ReadyDate], [ArrivalDate], [IsArrived], [Remarks], [DriverId], [VehicleId], [SelectorId], [EmployeeId], [GoodCompanyId], [OmeDealId], [AppDealId], [AddedDate], [AddedBy], [UpdatedBy], [Status], [IsDispatched], [DispatchedDate], [IsLoaded], [LoadedDate], [Unum], [ScheduleRemarks], [DispatchRemarks], [LoadRemarks], [ReceiveRemarks], [DispatchLoadPackingDifference], [DispatchLoadTradeUnitDifference], [DispatchLoadSubTradeUnitDifference], [DispatchLoadPriceDifference], [FareDealAmount], [FareActualPaid], [TransId], [LaborExpenses], [LaborExpensesDescription], [TracktorLaborAmount])
                      VALUES (@ScheduledPrice, @ScheduledSubTradeUnits, @ScheduledPackingsUnits, @ScheduledTradeUnits, @LoadedPrice, @LoadedSubTradeUnits, @LoadedPackingsUnits, @LoadedTradeUnits, @ReceivedPrice, @ReceivedSubTradeUnits, @ReceivedPackingsUnits, @ReceivedTradeUnits, @DiffLoadedPrice, @DiffLoadedSubTradeUnits, @DiffLoadedPackingUnits, @DiffLoadedTradeUnits, @TradeUnitTitle, @PackingUnitTitle, @ReadyDate, @ArrivalDate, @IsArrived, @Remarks, @DriverId, @VehicleId, @SelectorId, @EmployeeId, @GoodCompanyId, @OmeDealId, @AppDealId, @AddedDate, @AddedBy, @UpdatedBy, @Status, @IsDispatched, @DispatchedDate, @IsLoaded, @LoadedDate, @Unum, @ScheduleRemarks, @DispatchRemarks, @LoadRemarks, @ReceiveRemarks, @DispatchLoadPackingDifference, @DispatchLoadTradeUnitDifference, @DispatchLoadSubTradeUnitDifference, @DispatchLoadPriceDifference, @FareDealAmount, @FareActualPaid, @TransId, @LaborExpenses, @LaborExpensesDescription, @TracktorLaborAmount)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[DealSchedules]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[DealSchedules] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.DealSchedule_Update",
                p => new
                    {
                        Id = p.Int(),
                        ScheduledPrice = p.Decimal(precision: 16, scale: 4),
                        ScheduledSubTradeUnits = p.Decimal(precision: 16, scale: 4),
                        ScheduledPackingsUnits = p.Decimal(precision: 16, scale: 4),
                        ScheduledTradeUnits = p.Decimal(precision: 16, scale: 4),
                        LoadedPrice = p.Decimal(precision: 16, scale: 4),
                        LoadedSubTradeUnits = p.Decimal(precision: 16, scale: 4),
                        LoadedPackingsUnits = p.Decimal(precision: 16, scale: 4),
                        LoadedTradeUnits = p.Decimal(precision: 16, scale: 4),
                        ReceivedPrice = p.Decimal(precision: 16, scale: 4),
                        ReceivedSubTradeUnits = p.Decimal(precision: 16, scale: 4),
                        ReceivedPackingsUnits = p.Decimal(precision: 16, scale: 4),
                        ReceivedTradeUnits = p.Decimal(precision: 16, scale: 4),
                        DiffLoadedPrice = p.Decimal(precision: 16, scale: 4),
                        DiffLoadedSubTradeUnits = p.Decimal(precision: 16, scale: 4),
                        DiffLoadedPackingUnits = p.Decimal(precision: 16, scale: 4),
                        DiffLoadedTradeUnits = p.Decimal(precision: 16, scale: 4),
                        TradeUnitTitle = p.String(),
                        PackingUnitTitle = p.String(),
                        ReadyDate = p.DateTime(),
                        ArrivalDate = p.DateTime(),
                        IsArrived = p.Boolean(),
                        Remarks = p.String(),
                        DriverId = p.Int(),
                        VehicleId = p.Int(),
                        SelectorId = p.Int(),
                        EmployeeId = p.Int(),
                        GoodCompanyId = p.Int(),
                        OmeDealId = p.Int(),
                        AppDealId = p.Int(),
                        AddedDate = p.DateTime(),
                        AddedBy = p.String(),
                        UpdatedBy = p.String(),
                        Status = p.Int(),
                        IsDispatched = p.Boolean(),
                        DispatchedDate = p.DateTime(),
                        IsLoaded = p.Boolean(),
                        LoadedDate = p.DateTime(),
                        Unum = p.String(),
                        ScheduleRemarks = p.String(),
                        DispatchRemarks = p.String(),
                        LoadRemarks = p.String(),
                        ReceiveRemarks = p.String(),
                        DispatchLoadPackingDifference = p.Decimal(precision: 16, scale: 4),
                        DispatchLoadTradeUnitDifference = p.Decimal(precision: 16, scale: 4),
                        DispatchLoadSubTradeUnitDifference = p.Decimal(precision: 16, scale: 4),
                        DispatchLoadPriceDifference = p.Decimal(precision: 16, scale: 4),
                        FareDealAmount = p.Decimal(precision: 16, scale: 4),
                        FareActualPaid = p.Decimal(precision: 16, scale: 4),
                        TransId = p.Int(),
                        LaborExpenses = p.Decimal(precision: 16, scale: 4),
                        LaborExpensesDescription = p.String(),
                        TracktorLaborAmount = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[DealSchedules]
                      SET [ScheduledPrice] = @ScheduledPrice, [ScheduledSubTradeUnits] = @ScheduledSubTradeUnits, [ScheduledPackingsUnits] = @ScheduledPackingsUnits, [ScheduledTradeUnits] = @ScheduledTradeUnits, [LoadedPrice] = @LoadedPrice, [LoadedSubTradeUnits] = @LoadedSubTradeUnits, [LoadedPackingsUnits] = @LoadedPackingsUnits, [LoadedTradeUnits] = @LoadedTradeUnits, [ReceivedPrice] = @ReceivedPrice, [ReceivedSubTradeUnits] = @ReceivedSubTradeUnits, [ReceivedPackingsUnits] = @ReceivedPackingsUnits, [ReceivedTradeUnits] = @ReceivedTradeUnits, [DiffLoadedPrice] = @DiffLoadedPrice, [DiffLoadedSubTradeUnits] = @DiffLoadedSubTradeUnits, [DiffLoadedPackingUnits] = @DiffLoadedPackingUnits, [DiffLoadedTradeUnits] = @DiffLoadedTradeUnits, [TradeUnitTitle] = @TradeUnitTitle, [PackingUnitTitle] = @PackingUnitTitle, [ReadyDate] = @ReadyDate, [ArrivalDate] = @ArrivalDate, [IsArrived] = @IsArrived, [Remarks] = @Remarks, [DriverId] = @DriverId, [VehicleId] = @VehicleId, [SelectorId] = @SelectorId, [EmployeeId] = @EmployeeId, [GoodCompanyId] = @GoodCompanyId, [OmeDealId] = @OmeDealId, [AppDealId] = @AppDealId, [AddedDate] = @AddedDate, [AddedBy] = @AddedBy, [UpdatedBy] = @UpdatedBy, [Status] = @Status, [IsDispatched] = @IsDispatched, [DispatchedDate] = @DispatchedDate, [IsLoaded] = @IsLoaded, [LoadedDate] = @LoadedDate, [Unum] = @Unum, [ScheduleRemarks] = @ScheduleRemarks, [DispatchRemarks] = @DispatchRemarks, [LoadRemarks] = @LoadRemarks, [ReceiveRemarks] = @ReceiveRemarks, [DispatchLoadPackingDifference] = @DispatchLoadPackingDifference, [DispatchLoadTradeUnitDifference] = @DispatchLoadTradeUnitDifference, [DispatchLoadSubTradeUnitDifference] = @DispatchLoadSubTradeUnitDifference, [DispatchLoadPriceDifference] = @DispatchLoadPriceDifference, [FareDealAmount] = @FareDealAmount, [FareActualPaid] = @FareActualPaid, [TransId] = @TransId, [LaborExpenses] = @LaborExpenses, [LaborExpensesDescription] = @LaborExpensesDescription, [TracktorLaborAmount] = @TracktorLaborAmount
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.DealSchedule_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[DealSchedules]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ScheduleAlarm_Insert",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        GenerateTime = p.DateTime(),
                        ActiveDate = p.DateTime(),
                        EndTime = p.DateTime(),
                        Status = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[ScheduleAlarms]([Id], [Title], [GenerateTime], [ActiveDate], [EndTime], [Status])
                      VALUES (@Id, @Title, @GenerateTime, @ActiveDate, @EndTime, @Status)"
            );
            
            CreateStoredProcedure(
                "dbo.ScheduleAlarm_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        GenerateTime = p.DateTime(),
                        ActiveDate = p.DateTime(),
                        EndTime = p.DateTime(),
                        Status = p.Boolean(),
                    },
                body:
                    @"UPDATE [dbo].[ScheduleAlarms]
                      SET [Title] = @Title, [GenerateTime] = @GenerateTime, [ActiveDate] = @ActiveDate, [EndTime] = @EndTime, [Status] = @Status
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ScheduleAlarm_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ScheduleAlarms]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Driver_Insert",
                p => new
                    {
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        IsAvailable = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[Drivers]([Name], [NameUrdu], [Address], [Contact], [Remarks], [IsActive], [DateAdded], [Extra], [IsAvailable])
                      VALUES (@Name, @NameUrdu, @Address, @Contact, @Remarks, @IsActive, @DateAdded, @Extra, @IsAvailable)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Drivers]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Drivers] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Driver_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        IsAvailable = p.Boolean(),
                    },
                body:
                    @"UPDATE [dbo].[Drivers]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu, [Address] = @Address, [Contact] = @Contact, [Remarks] = @Remarks, [IsActive] = @IsActive, [DateAdded] = @DateAdded, [Extra] = @Extra, [IsAvailable] = @IsAvailable
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Driver_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Drivers]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Employee_Insert",
                p => new
                    {
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Designation = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        DateEnded = p.DateTime(),
                        Extra = p.String(),
                        CNIC = p.String(),
                        PicData = p.Binary(),
                        ThumbData = p.Binary(),
                        ThumbPicData = p.Binary(),
                        IsAvailable = p.Boolean(),
                        GeneralAccountId = p.String(maxLength: 128),
                        Salary = p.Decimal(precision: 16, scale: 4),
                        Balance = p.Decimal(precision: 16, scale: 4),
                        CanLogin = p.Boolean(),
                        UserId = p.String(),
                        Password = p.String(),
                        EmployeeType = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Employees]([Name], [NameUrdu], [Designation], [Address], [Contact], [Remarks], [IsActive], [DateAdded], [DateEnded], [Extra], [CNIC], [PicData], [ThumbData], [ThumbPicData], [IsAvailable], [GeneralAccountId], [Salary], [Balance], [CanLogin], [UserId], [Password], [EmployeeType])
                      VALUES (@Name, @NameUrdu, @Designation, @Address, @Contact, @Remarks, @IsActive, @DateAdded, @DateEnded, @Extra, @CNIC, @PicData, @ThumbData, @ThumbPicData, @IsAvailable, @GeneralAccountId, @Salary, @Balance, @CanLogin, @UserId, @Password, @EmployeeType)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Employees]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Employees] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Employee_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Designation = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        DateEnded = p.DateTime(),
                        Extra = p.String(),
                        CNIC = p.String(),
                        PicData = p.Binary(),
                        ThumbData = p.Binary(),
                        ThumbPicData = p.Binary(),
                        IsAvailable = p.Boolean(),
                        GeneralAccountId = p.String(maxLength: 128),
                        Salary = p.Decimal(precision: 16, scale: 4),
                        Balance = p.Decimal(precision: 16, scale: 4),
                        CanLogin = p.Boolean(),
                        UserId = p.String(),
                        Password = p.String(),
                        EmployeeType = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Employees]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu, [Designation] = @Designation, [Address] = @Address, [Contact] = @Contact, [Remarks] = @Remarks, [IsActive] = @IsActive, [DateAdded] = @DateAdded, [DateEnded] = @DateEnded, [Extra] = @Extra, [CNIC] = @CNIC, [PicData] = @PicData, [ThumbData] = @ThumbData, [ThumbPicData] = @ThumbPicData, [IsAvailable] = @IsAvailable, [GeneralAccountId] = @GeneralAccountId, [Salary] = @Salary, [Balance] = @Balance, [CanLogin] = @CanLogin, [UserId] = @UserId, [Password] = @Password, [EmployeeType] = @EmployeeType
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Employee_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Employees]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.AttendanceRecord_Insert",
                p => new
                    {
                        EmployeeId = p.String(),
                        AttendIn = p.DateTime(),
                        AttendOut = p.DateTime(),
                        Employee_Id = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[AttendanceRecords]([EmployeeId], [AttendIn], [AttendOut], [Employee_Id])
                      VALUES (@EmployeeId, @AttendIn, @AttendOut, @Employee_Id)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[AttendanceRecords]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[AttendanceRecords] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.AttendanceRecord_Update",
                p => new
                    {
                        Id = p.Int(),
                        EmployeeId = p.String(),
                        AttendIn = p.DateTime(),
                        AttendOut = p.DateTime(),
                        Employee_Id = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[AttendanceRecords]
                      SET [EmployeeId] = @EmployeeId, [AttendIn] = @AttendIn, [AttendOut] = @AttendOut, [Employee_Id] = @Employee_Id
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.AttendanceRecord_Delete",
                p => new
                    {
                        Id = p.Int(),
                        Employee_Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[AttendanceRecords]
                      WHERE (([Id] = @Id) AND (([Employee_Id] = @Employee_Id) OR ([Employee_Id] IS NULL AND @Employee_Id IS NULL)))"
            );
            
            CreateStoredProcedure(
                "dbo.CreditEntry_Insert",
                p => new
                    {
                        EmployeeId = p.Int(),
                        Amount = p.Decimal(precision: 16, scale: 4),
                        Date = p.DateTime(),
                        Remarks = p.String(),
                        DayBookId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[CreditEntries]([EmployeeId], [Amount], [Date], [Remarks], [DayBookId])
                      VALUES (@EmployeeId, @Amount, @Date, @Remarks, @DayBookId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[CreditEntries]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[CreditEntries] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.CreditEntry_Update",
                p => new
                    {
                        Id = p.Int(),
                        EmployeeId = p.Int(),
                        Amount = p.Decimal(precision: 16, scale: 4),
                        Date = p.DateTime(),
                        Remarks = p.String(),
                        DayBookId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[CreditEntries]
                      SET [EmployeeId] = @EmployeeId, [Amount] = @Amount, [Date] = @Date, [Remarks] = @Remarks, [DayBookId] = @DayBookId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.CreditEntry_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[CreditEntries]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.EmployPayRollEntry_Insert",
                p => new
                    {
                        Month = p.Int(),
                        Year = p.Int(),
                        EmployeeSalary = p.Decimal(precision: 16, scale: 4),
                        Unum = p.String(),
                        IsPaid = p.Boolean(),
                        ActionDate = p.DateTime(),
                        EmployeeId = p.Int(),
                        AllowanceAmount = p.Decimal(precision: 16, scale: 4),
                        DeductionAmount = p.Decimal(precision: 16, scale: 4),
                        GrossSalary = p.Decimal(precision: 16, scale: 4),
                        NetSalary = p.Decimal(precision: 16, scale: 4),
                        Remarks = p.String(),
                        XStr = p.String(),
                        EmpAdvances = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[EmployPayRollEntries]([Month], [Year], [EmployeeSalary], [Unum], [IsPaid], [ActionDate], [EmployeeId], [AllowanceAmount], [DeductionAmount], [GrossSalary], [NetSalary], [Remarks], [XStr], [EmpAdvances])
                      VALUES (@Month, @Year, @EmployeeSalary, @Unum, @IsPaid, @ActionDate, @EmployeeId, @AllowanceAmount, @DeductionAmount, @GrossSalary, @NetSalary, @Remarks, @XStr, @EmpAdvances)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[EmployPayRollEntries]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[EmployPayRollEntries] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.EmployPayRollEntry_Update",
                p => new
                    {
                        Id = p.Int(),
                        Month = p.Int(),
                        Year = p.Int(),
                        EmployeeSalary = p.Decimal(precision: 16, scale: 4),
                        Unum = p.String(),
                        IsPaid = p.Boolean(),
                        ActionDate = p.DateTime(),
                        EmployeeId = p.Int(),
                        AllowanceAmount = p.Decimal(precision: 16, scale: 4),
                        DeductionAmount = p.Decimal(precision: 16, scale: 4),
                        GrossSalary = p.Decimal(precision: 16, scale: 4),
                        NetSalary = p.Decimal(precision: 16, scale: 4),
                        Remarks = p.String(),
                        XStr = p.String(),
                        EmpAdvances = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[EmployPayRollEntries]
                      SET [Month] = @Month, [Year] = @Year, [EmployeeSalary] = @EmployeeSalary, [Unum] = @Unum, [IsPaid] = @IsPaid, [ActionDate] = @ActionDate, [EmployeeId] = @EmployeeId, [AllowanceAmount] = @AllowanceAmount, [DeductionAmount] = @DeductionAmount, [GrossSalary] = @GrossSalary, [NetSalary] = @NetSalary, [Remarks] = @Remarks, [XStr] = @XStr, [EmpAdvances] = @EmpAdvances
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.EmployPayRollEntry_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[EmployPayRollEntries]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.EmployeeSalaryEntryAllowance_Insert",
                p => new
                    {
                        EmployPayRollEntryId = p.Int(),
                        SalaryAllowanceId = p.Int(),
                        Amount = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[EmployeeSalaryEntryAllowances]([EmployPayRollEntryId], [SalaryAllowanceId], [Amount])
                      VALUES (@EmployPayRollEntryId, @SalaryAllowanceId, @Amount)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[EmployeeSalaryEntryAllowances]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[EmployeeSalaryEntryAllowances] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.EmployeeSalaryEntryAllowance_Update",
                p => new
                    {
                        Id = p.Int(),
                        EmployPayRollEntryId = p.Int(),
                        SalaryAllowanceId = p.Int(),
                        Amount = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[EmployeeSalaryEntryAllowances]
                      SET [EmployPayRollEntryId] = @EmployPayRollEntryId, [SalaryAllowanceId] = @SalaryAllowanceId, [Amount] = @Amount
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.EmployeeSalaryEntryAllowance_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[EmployeeSalaryEntryAllowances]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.SalaryAllowance_Insert",
                p => new
                    {
                        Title = p.String(),
                        Amount = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[SalaryAllowances]([Title], [Amount])
                      VALUES (@Title, @Amount)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[SalaryAllowances]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[SalaryAllowances] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.SalaryAllowance_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        Amount = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[SalaryAllowances]
                      SET [Title] = @Title, [Amount] = @Amount
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.SalaryAllowance_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[SalaryAllowances]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.EmployeeSalaryEntryDeduction_Insert",
                p => new
                    {
                        EmployPayRollEntryId = p.Int(),
                        SalaryDeductionId = p.Int(),
                        Amount = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[EmployeeSalaryEntryDeductions]([EmployPayRollEntryId], [SalaryDeductionId], [Amount])
                      VALUES (@EmployPayRollEntryId, @SalaryDeductionId, @Amount)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[EmployeeSalaryEntryDeductions]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[EmployeeSalaryEntryDeductions] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.EmployeeSalaryEntryDeduction_Update",
                p => new
                    {
                        Id = p.Int(),
                        EmployPayRollEntryId = p.Int(),
                        SalaryDeductionId = p.Int(),
                        Amount = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[EmployeeSalaryEntryDeductions]
                      SET [EmployPayRollEntryId] = @EmployPayRollEntryId, [SalaryDeductionId] = @SalaryDeductionId, [Amount] = @Amount
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.EmployeeSalaryEntryDeduction_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[EmployeeSalaryEntryDeductions]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.SalaryDeduction_Insert",
                p => new
                    {
                        Title = p.String(),
                        Amount = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[SalaryDeductions]([Title], [Amount])
                      VALUES (@Title, @Amount)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[SalaryDeductions]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[SalaryDeductions] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.SalaryDeduction_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        Amount = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[SalaryDeductions]
                      SET [Title] = @Title, [Amount] = @Amount
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.SalaryDeduction_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[SalaryDeductions]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.GoodCompany_Insert",
                p => new
                    {
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Phone = p.String(),
                        Address = p.String(),
                        Owner = p.String(),
                        OwnerContact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[GoodCompanies]([Name], [NameUrdu], [Phone], [Address], [Owner], [OwnerContact], [Remarks], [IsActive], [DateAdded], [Extra], [GeneralAccountId])
                      VALUES (@Name, @NameUrdu, @Phone, @Address, @Owner, @OwnerContact, @Remarks, @IsActive, @DateAdded, @Extra, @GeneralAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[GoodCompanies]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[GoodCompanies] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.GoodCompany_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Phone = p.String(),
                        Address = p.String(),
                        Owner = p.String(),
                        OwnerContact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[GoodCompanies]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu, [Phone] = @Phone, [Address] = @Address, [Owner] = @Owner, [OwnerContact] = @OwnerContact, [Remarks] = @Remarks, [IsActive] = @IsActive, [DateAdded] = @DateAdded, [Extra] = @Extra, [GeneralAccountId] = @GeneralAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.GoodCompany_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[GoodCompanies]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.PackingIssueReceiveRecord_Insert",
                p => new
                    {
                        GoodCompanyId = p.Int(),
                        DealPackingId = p.Int(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        DateTime = p.DateTime(),
                        RecordType = p.Int(),
                        Description = p.String(),
                        Remarks = p.String(),
                        Unum = p.String(),
                    },
                body:
                    @"INSERT [dbo].[PackingIssueReceiveRecords]([GoodCompanyId], [DealPackingId], [Qty], [DateTime], [RecordType], [Description], [Remarks], [Unum])
                      VALUES (@GoodCompanyId, @DealPackingId, @Qty, @DateTime, @RecordType, @Description, @Remarks, @Unum)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[PackingIssueReceiveRecords]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[PackingIssueReceiveRecords] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.PackingIssueReceiveRecord_Update",
                p => new
                    {
                        Id = p.Int(),
                        GoodCompanyId = p.Int(),
                        DealPackingId = p.Int(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        DateTime = p.DateTime(),
                        RecordType = p.Int(),
                        Description = p.String(),
                        Remarks = p.String(),
                        Unum = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[PackingIssueReceiveRecords]
                      SET [GoodCompanyId] = @GoodCompanyId, [DealPackingId] = @DealPackingId, [Qty] = @Qty, [DateTime] = @DateTime, [RecordType] = @RecordType, [Description] = @Description, [Remarks] = @Remarks, [Unum] = @Unum
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.PackingIssueReceiveRecord_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[PackingIssueReceiveRecords]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.DealPacking_Insert",
                p => new
                    {
                        Name = p.String(),
                        NameUrdu = p.String(),
                        UnitPrice = p.Decimal(precision: 16, scale: 4),
                        GeneralAccountId = p.Int(),
                        Account_Id = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[DealPackings]([Name], [NameUrdu], [UnitPrice], [GeneralAccountId], [Account_Id])
                      VALUES (@Name, @NameUrdu, @UnitPrice, @GeneralAccountId, @Account_Id)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[DealPackings]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[DealPackings] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.DealPacking_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        UnitPrice = p.Decimal(precision: 16, scale: 4),
                        GeneralAccountId = p.Int(),
                        Account_Id = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[DealPackings]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu, [UnitPrice] = @UnitPrice, [GeneralAccountId] = @GeneralAccountId, [Account_Id] = @Account_Id
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.DealPacking_Delete",
                p => new
                    {
                        Id = p.Int(),
                        Account_Id = p.String(maxLength: 128),
                    },
                body:
                    @"DELETE [dbo].[DealPackings]
                      WHERE (([Id] = @Id) AND (([Account_Id] = @Account_Id) OR ([Account_Id] IS NULL AND @Account_Id IS NULL)))"
            );
            
            CreateStoredProcedure(
                "dbo.FactoryPackingStockAddedRecord_Insert",
                p => new
                    {
                        DealPackingId = p.Int(),
                        QtyAdded = p.Decimal(precision: 16, scale: 4),
                        AddedDate = p.DateTime(),
                        Description = p.String(),
                        DealPackingSupplierId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[FactoryPackingStockAddedRecords]([DealPackingId], [QtyAdded], [AddedDate], [Description], [DealPackingSupplierId])
                      VALUES (@DealPackingId, @QtyAdded, @AddedDate, @Description, @DealPackingSupplierId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[FactoryPackingStockAddedRecords]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[FactoryPackingStockAddedRecords] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.FactoryPackingStockAddedRecord_Update",
                p => new
                    {
                        Id = p.Int(),
                        DealPackingId = p.Int(),
                        QtyAdded = p.Decimal(precision: 16, scale: 4),
                        AddedDate = p.DateTime(),
                        Description = p.String(),
                        DealPackingSupplierId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[FactoryPackingStockAddedRecords]
                      SET [DealPackingId] = @DealPackingId, [QtyAdded] = @QtyAdded, [AddedDate] = @AddedDate, [Description] = @Description, [DealPackingSupplierId] = @DealPackingSupplierId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.FactoryPackingStockAddedRecord_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[FactoryPackingStockAddedRecords]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.DealPackingSupplier_Insert",
                p => new
                    {
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Contact = p.String(),
                        Address = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Remarks = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[DealPackingSuppliers]([Name], [NameUrdu], [Contact], [Address], [IsActive], [DateAdded], [Remarks], [GeneralAccountId])
                      VALUES (@Name, @NameUrdu, @Contact, @Address, @IsActive, @DateAdded, @Remarks, @GeneralAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[DealPackingSuppliers]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[DealPackingSuppliers] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.DealPackingSupplier_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Contact = p.String(),
                        Address = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Remarks = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[DealPackingSuppliers]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu, [Contact] = @Contact, [Address] = @Address, [IsActive] = @IsActive, [DateAdded] = @DateAdded, [Remarks] = @Remarks, [GeneralAccountId] = @GeneralAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.DealPackingSupplier_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[DealPackingSuppliers]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.FactoryPackingStockIssueRecord_Insert",
                p => new
                    {
                        DealPackingId = p.Int(),
                        QtyIssued = p.Decimal(precision: 16, scale: 4),
                        IssuedDate = p.DateTime(),
                        Description = p.String(),
                    },
                body:
                    @"INSERT [dbo].[FactoryPackingStockIssueRecords]([DealPackingId], [QtyIssued], [IssuedDate], [Description])
                      VALUES (@DealPackingId, @QtyIssued, @IssuedDate, @Description)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[FactoryPackingStockIssueRecords]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[FactoryPackingStockIssueRecords] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.FactoryPackingStockIssueRecord_Update",
                p => new
                    {
                        Id = p.Int(),
                        DealPackingId = p.Int(),
                        QtyIssued = p.Decimal(precision: 16, scale: 4),
                        IssuedDate = p.DateTime(),
                        Description = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[FactoryPackingStockIssueRecords]
                      SET [DealPackingId] = @DealPackingId, [QtyIssued] = @QtyIssued, [IssuedDate] = @IssuedDate, [Description] = @Description
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.FactoryPackingStockIssueRecord_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[FactoryPackingStockIssueRecords]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.FactoryPackingStock_Insert",
                p => new
                    {
                        DealPackingId = p.Int(),
                        Quantity = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[FactoryPackingStocks]([DealPackingId], [Quantity])
                      VALUES (@DealPackingId, @Quantity)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[FactoryPackingStocks]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[FactoryPackingStocks] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.FactoryPackingStock_Update",
                p => new
                    {
                        Id = p.Int(),
                        DealPackingId = p.Int(),
                        Quantity = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[FactoryPackingStocks]
                      SET [DealPackingId] = @DealPackingId, [Quantity] = @Quantity
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.FactoryPackingStock_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[FactoryPackingStocks]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OmeDeal_Insert",
                p => new
                    {
                        CompanyId = p.Int(),
                        BrokerId = p.Int(),
                        DealItemId = p.Int(),
                        DealPackingId = p.Int(),
                        PerPackingQty = p.Decimal(precision: 16, scale: 4),
                        TotalQty = p.Decimal(precision: 16, scale: 4),
                        PackingQty = p.Decimal(precision: 16, scale: 4),
                        TradeUnitId = p.Int(),
                        TradeUnitPrice = p.Decimal(precision: 16, scale: 4),
                        TotalTradeUnits = p.Decimal(precision: 16, scale: 4),
                        DealPrice = p.Decimal(precision: 16, scale: 4),
                        Tax = p.Decimal(precision: 16, scale: 4),
                        FareAmount = p.Decimal(precision: 16, scale: 4),
                        LaborExpenses = p.Decimal(precision: 16, scale: 4),
                        LaborExpensesDescription = p.Decimal(precision: 16, scale: 4),
                        DealDate = p.DateTime(),
                        AddedBy = p.String(),
                        UpdatedBy = p.String(),
                        IsCompleted = p.Boolean(),
                        PackingUnitId = p.Int(),
                        SegmentsCount = p.Int(),
                        Remarks = p.String(),
                        Selector_Id = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[OmeDeals]([CompanyId], [BrokerId], [DealItemId], [DealPackingId], [PerPackingQty], [TotalQty], [PackingQty], [TradeUnitId], [TradeUnitPrice], [TotalTradeUnits], [DealPrice], [Tax], [FareAmount], [LaborExpenses], [LaborExpensesDescription], [DealDate], [AddedBy], [UpdatedBy], [IsCompleted], [PackingUnitId], [SegmentsCount], [Remarks], [Selector_Id])
                      VALUES (@CompanyId, @BrokerId, @DealItemId, @DealPackingId, @PerPackingQty, @TotalQty, @PackingQty, @TradeUnitId, @TradeUnitPrice, @TotalTradeUnits, @DealPrice, @Tax, @FareAmount, @LaborExpenses, @LaborExpensesDescription, @DealDate, @AddedBy, @UpdatedBy, @IsCompleted, @PackingUnitId, @SegmentsCount, @Remarks, @Selector_Id)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[OmeDeals]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[OmeDeals] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.OmeDeal_Update",
                p => new
                    {
                        Id = p.Int(),
                        CompanyId = p.Int(),
                        BrokerId = p.Int(),
                        DealItemId = p.Int(),
                        DealPackingId = p.Int(),
                        PerPackingQty = p.Decimal(precision: 16, scale: 4),
                        TotalQty = p.Decimal(precision: 16, scale: 4),
                        PackingQty = p.Decimal(precision: 16, scale: 4),
                        TradeUnitId = p.Int(),
                        TradeUnitPrice = p.Decimal(precision: 16, scale: 4),
                        TotalTradeUnits = p.Decimal(precision: 16, scale: 4),
                        DealPrice = p.Decimal(precision: 16, scale: 4),
                        Tax = p.Decimal(precision: 16, scale: 4),
                        FareAmount = p.Decimal(precision: 16, scale: 4),
                        LaborExpenses = p.Decimal(precision: 16, scale: 4),
                        LaborExpensesDescription = p.Decimal(precision: 16, scale: 4),
                        DealDate = p.DateTime(),
                        AddedBy = p.String(),
                        UpdatedBy = p.String(),
                        IsCompleted = p.Boolean(),
                        PackingUnitId = p.Int(),
                        SegmentsCount = p.Int(),
                        Remarks = p.String(),
                        Selector_Id = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[OmeDeals]
                      SET [CompanyId] = @CompanyId, [BrokerId] = @BrokerId, [DealItemId] = @DealItemId, [DealPackingId] = @DealPackingId, [PerPackingQty] = @PerPackingQty, [TotalQty] = @TotalQty, [PackingQty] = @PackingQty, [TradeUnitId] = @TradeUnitId, [TradeUnitPrice] = @TradeUnitPrice, [TotalTradeUnits] = @TotalTradeUnits, [DealPrice] = @DealPrice, [Tax] = @Tax, [FareAmount] = @FareAmount, [LaborExpenses] = @LaborExpenses, [LaborExpensesDescription] = @LaborExpensesDescription, [DealDate] = @DealDate, [AddedBy] = @AddedBy, [UpdatedBy] = @UpdatedBy, [IsCompleted] = @IsCompleted, [PackingUnitId] = @PackingUnitId, [SegmentsCount] = @SegmentsCount, [Remarks] = @Remarks, [Selector_Id] = @Selector_Id
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OmeDeal_Delete",
                p => new
                    {
                        Id = p.Int(),
                        Selector_Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[OmeDeals]
                      WHERE (([Id] = @Id) AND (([Selector_Id] = @Selector_Id) OR ([Selector_Id] IS NULL AND @Selector_Id IS NULL)))"
            );
            
            CreateStoredProcedure(
                "dbo.Company_Insert",
                p => new
                    {
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[Companies]([Name], [NameUrdu], [Address], [Contact], [Remarks], [IsActive], [DateAdded], [Extra], [GeneralAccountId])
                      VALUES (@Name, @NameUrdu, @Address, @Contact, @Remarks, @IsActive, @DateAdded, @Extra, @GeneralAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Companies]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Companies] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Company_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[Companies]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu, [Address] = @Address, [Contact] = @Contact, [Remarks] = @Remarks, [IsActive] = @IsActive, [DateAdded] = @DateAdded, [Extra] = @Extra, [GeneralAccountId] = @GeneralAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Company_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Companies]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.DealItem_Insert",
                p => new
                    {
                        Name = p.String(),
                        NameUrdu = p.String(),
                        SKU = p.Decimal(precision: 16, scale: 4),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[DealItems]([Name], [NameUrdu], [SKU], [GeneralAccountId])
                      VALUES (@Name, @NameUrdu, @SKU, @GeneralAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[DealItems]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[DealItems] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.DealItem_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        SKU = p.Decimal(precision: 16, scale: 4),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[DealItems]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu, [SKU] = @SKU, [GeneralAccountId] = @GeneralAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.DealItem_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[DealItems]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.PackingUnit_Insert",
                p => new
                    {
                        Name = p.String(),
                    },
                body:
                    @"INSERT [dbo].[PackingUnits]([Name])
                      VALUES (@Name)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[PackingUnits]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[PackingUnits] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.PackingUnit_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[PackingUnits]
                      SET [Name] = @Name
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.PackingUnit_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[PackingUnits]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.TradeUnit_Insert",
                p => new
                    {
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[TradeUnits]([Title], [TitleUrdu], [Qty])
                      VALUES (@Title, @TitleUrdu, @Qty)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[TradeUnits]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[TradeUnits] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.TradeUnit_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[TradeUnits]
                      SET [Title] = @Title, [TitleUrdu] = @TitleUrdu, [Qty] = @Qty
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.TradeUnit_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[TradeUnits]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.PackingStock_Insert",
                p => new
                    {
                        GoodCompanyId = p.Int(),
                        DealPackingId = p.Int(),
                        Balance = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[PackingStocks]([GoodCompanyId], [DealPackingId], [Balance])
                      VALUES (@GoodCompanyId, @DealPackingId, @Balance)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[PackingStocks]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[PackingStocks] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.PackingStock_Update",
                p => new
                    {
                        Id = p.Int(),
                        GoodCompanyId = p.Int(),
                        DealPackingId = p.Int(),
                        Balance = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[PackingStocks]
                      SET [GoodCompanyId] = @GoodCompanyId, [DealPackingId] = @DealPackingId, [Balance] = @Balance
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.PackingStock_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[PackingStocks]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ScheduleLoadPacking_Insert",
                p => new
                    {
                        DealScheduleId = p.Int(),
                        DealPackingId = p.Int(),
                        LoadQty = p.Decimal(precision: 16, scale: 4),
                        ReceiveQty = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[ScheduleLoadPackings]([DealScheduleId], [DealPackingId], [LoadQty], [ReceiveQty])
                      VALUES (@DealScheduleId, @DealPackingId, @LoadQty, @ReceiveQty)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ScheduleLoadPackings]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ScheduleLoadPackings] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ScheduleLoadPacking_Update",
                p => new
                    {
                        Id = p.Int(),
                        DealScheduleId = p.Int(),
                        DealPackingId = p.Int(),
                        LoadQty = p.Decimal(precision: 16, scale: 4),
                        ReceiveQty = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[ScheduleLoadPackings]
                      SET [DealScheduleId] = @DealScheduleId, [DealPackingId] = @DealPackingId, [LoadQty] = @LoadQty, [ReceiveQty] = @ReceiveQty
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ScheduleLoadPacking_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ScheduleLoadPackings]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ScheduleWeighBridge_Insert",
                p => new
                    {
                        DealScheduleId = p.Int(),
                        WeighBridgeId = p.Int(),
                        Type = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[ScheduleWeighBridges]([DealScheduleId], [WeighBridgeId], [Type])
                      VALUES (@DealScheduleId, @WeighBridgeId, @Type)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ScheduleWeighBridges]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ScheduleWeighBridges] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ScheduleWeighBridge_Update",
                p => new
                    {
                        Id = p.Int(),
                        DealScheduleId = p.Int(),
                        WeighBridgeId = p.Int(),
                        Type = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[ScheduleWeighBridges]
                      SET [DealScheduleId] = @DealScheduleId, [WeighBridgeId] = @WeighBridgeId, [Type] = @Type
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ScheduleWeighBridge_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ScheduleWeighBridges]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.WeighBridge_Insert",
                p => new
                    {
                        Name = p.String(),
                        Address = p.String(),
                        Phone = p.String(),
                        WeighBrideType = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[WeighBridges]([Name], [Address], [Phone], [WeighBrideType])
                      VALUES (@Name, @Address, @Phone, @WeighBrideType)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[WeighBridges]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[WeighBridges] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.WeighBridge_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        Address = p.String(),
                        Phone = p.String(),
                        WeighBrideType = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[WeighBridges]
                      SET [Name] = @Name, [Address] = @Address, [Phone] = @Phone, [WeighBrideType] = @WeighBrideType
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.WeighBridge_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[WeighBridges]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Selector_Insert",
                p => new
                    {
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[Selectors]([Name], [NameUrdu], [Address], [Contact], [Remarks], [IsActive], [DateAdded], [Extra], [GeneralAccountId])
                      VALUES (@Name, @NameUrdu, @Address, @Contact, @Remarks, @IsActive, @DateAdded, @Extra, @GeneralAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Selectors]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Selectors] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Selector_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[Selectors]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu, [Address] = @Address, [Contact] = @Contact, [Remarks] = @Remarks, [IsActive] = @IsActive, [DateAdded] = @DateAdded, [Extra] = @Extra, [GeneralAccountId] = @GeneralAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Selector_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Selectors]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Vehicle_Insert",
                p => new
                    {
                        No = p.String(),
                        VehicleType = p.Int(),
                        Status = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Vehicles]([No], [VehicleType], [Status])
                      VALUES (@No, @VehicleType, @Status)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Vehicles]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Vehicles] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Vehicle_Update",
                p => new
                    {
                        Id = p.Int(),
                        No = p.String(),
                        VehicleType = p.Int(),
                        Status = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Vehicles]
                      SET [No] = @No, [VehicleType] = @VehicleType, [Status] = @Status
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Vehicle_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Vehicles]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDeal_Insert",
                p => new
                    {
                        DealDate = p.DateTime(),
                        ReadyDate = p.DateTime(),
                        CompleteDate = p.DateTime(),
                        DealQty = p.Decimal(precision: 16, scale: 4),
                        OilDealBrokerId = p.Int(),
                        OilDealItemId = p.Int(),
                        OilDealSelectorId = p.Int(),
                        OilDealDriverId = p.Int(),
                        VehicleNo = p.String(),
                        VehicleEmptyWeight = p.Decimal(precision: 16, scale: 4),
                        VehicleScheduleQty = p.Decimal(precision: 16, scale: 4),
                        VehicleFullWeight = p.Decimal(precision: 16, scale: 4),
                        WeighBridgeWeight = p.Decimal(precision: 16, scale: 4),
                        BrokerSharePercentage = p.Decimal(precision: 16, scale: 4),
                        BrokerShareAmount = p.Decimal(precision: 16, scale: 4),
                        OilTradeUnitId = p.Int(),
                        TotalTradeUnits = p.Decimal(precision: 16, scale: 4),
                        PerTradeUnitPrice = p.Decimal(precision: 16, scale: 4),
                        TotalPrice = p.Decimal(precision: 16, scale: 4),
                        NetPrice = p.Decimal(precision: 16, scale: 4),
                        Status = p.Int(),
                        Unum = p.String(),
                        LaborExpenses = p.Decimal(precision: 16, scale: 4),
                        LaborExpensesDescription = p.String(),
                        AppUserId = p.String(maxLength: 128),
                        WeighBridgeId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[OilDeals]([DealDate], [ReadyDate], [CompleteDate], [DealQty], [OilDealBrokerId], [OilDealItemId], [OilDealSelectorId], [OilDealDriverId], [VehicleNo], [VehicleEmptyWeight], [VehicleScheduleQty], [VehicleFullWeight], [WeighBridgeWeight], [BrokerSharePercentage], [BrokerShareAmount], [OilTradeUnitId], [TotalTradeUnits], [PerTradeUnitPrice], [TotalPrice], [NetPrice], [Status], [Unum], [LaborExpenses], [LaborExpensesDescription], [AppUserId], [WeighBridgeId])
                      VALUES (@DealDate, @ReadyDate, @CompleteDate, @DealQty, @OilDealBrokerId, @OilDealItemId, @OilDealSelectorId, @OilDealDriverId, @VehicleNo, @VehicleEmptyWeight, @VehicleScheduleQty, @VehicleFullWeight, @WeighBridgeWeight, @BrokerSharePercentage, @BrokerShareAmount, @OilTradeUnitId, @TotalTradeUnits, @PerTradeUnitPrice, @TotalPrice, @NetPrice, @Status, @Unum, @LaborExpenses, @LaborExpensesDescription, @AppUserId, @WeighBridgeId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[OilDeals]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[OilDeals] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.OilDeal_Update",
                p => new
                    {
                        Id = p.Int(),
                        DealDate = p.DateTime(),
                        ReadyDate = p.DateTime(),
                        CompleteDate = p.DateTime(),
                        DealQty = p.Decimal(precision: 16, scale: 4),
                        OilDealBrokerId = p.Int(),
                        OilDealItemId = p.Int(),
                        OilDealSelectorId = p.Int(),
                        OilDealDriverId = p.Int(),
                        VehicleNo = p.String(),
                        VehicleEmptyWeight = p.Decimal(precision: 16, scale: 4),
                        VehicleScheduleQty = p.Decimal(precision: 16, scale: 4),
                        VehicleFullWeight = p.Decimal(precision: 16, scale: 4),
                        WeighBridgeWeight = p.Decimal(precision: 16, scale: 4),
                        BrokerSharePercentage = p.Decimal(precision: 16, scale: 4),
                        BrokerShareAmount = p.Decimal(precision: 16, scale: 4),
                        OilTradeUnitId = p.Int(),
                        TotalTradeUnits = p.Decimal(precision: 16, scale: 4),
                        PerTradeUnitPrice = p.Decimal(precision: 16, scale: 4),
                        TotalPrice = p.Decimal(precision: 16, scale: 4),
                        NetPrice = p.Decimal(precision: 16, scale: 4),
                        Status = p.Int(),
                        Unum = p.String(),
                        LaborExpenses = p.Decimal(precision: 16, scale: 4),
                        LaborExpensesDescription = p.String(),
                        AppUserId = p.String(maxLength: 128),
                        WeighBridgeId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[OilDeals]
                      SET [DealDate] = @DealDate, [ReadyDate] = @ReadyDate, [CompleteDate] = @CompleteDate, [DealQty] = @DealQty, [OilDealBrokerId] = @OilDealBrokerId, [OilDealItemId] = @OilDealItemId, [OilDealSelectorId] = @OilDealSelectorId, [OilDealDriverId] = @OilDealDriverId, [VehicleNo] = @VehicleNo, [VehicleEmptyWeight] = @VehicleEmptyWeight, [VehicleScheduleQty] = @VehicleScheduleQty, [VehicleFullWeight] = @VehicleFullWeight, [WeighBridgeWeight] = @WeighBridgeWeight, [BrokerSharePercentage] = @BrokerSharePercentage, [BrokerShareAmount] = @BrokerShareAmount, [OilTradeUnitId] = @OilTradeUnitId, [TotalTradeUnits] = @TotalTradeUnits, [PerTradeUnitPrice] = @PerTradeUnitPrice, [TotalPrice] = @TotalPrice, [NetPrice] = @NetPrice, [Status] = @Status, [Unum] = @Unum, [LaborExpenses] = @LaborExpenses, [LaborExpensesDescription] = @LaborExpensesDescription, [AppUserId] = @AppUserId, [WeighBridgeId] = @WeighBridgeId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDeal_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[OilDeals]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDealBroker_Insert",
                p => new
                    {
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                        BrokerExpenseAccountId = p.String(),
                    },
                body:
                    @"INSERT [dbo].[OilDealBrokers]([Name], [NameUrdu], [Address], [Contact], [Remarks], [IsActive], [DateAdded], [Extra], [GeneralAccountId], [BrokerExpenseAccountId])
                      VALUES (@Name, @NameUrdu, @Address, @Contact, @Remarks, @IsActive, @DateAdded, @Extra, @GeneralAccountId, @BrokerExpenseAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[OilDealBrokers]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[OilDealBrokers] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.OilDealBroker_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                        BrokerExpenseAccountId = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[OilDealBrokers]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu, [Address] = @Address, [Contact] = @Contact, [Remarks] = @Remarks, [IsActive] = @IsActive, [DateAdded] = @DateAdded, [Extra] = @Extra, [GeneralAccountId] = @GeneralAccountId, [BrokerExpenseAccountId] = @BrokerExpenseAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDealBroker_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[OilDealBrokers]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDealDriver_Insert",
                p => new
                    {
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        CNIC = p.String(),
                        PicData = p.Binary(),
                        ThumbData = p.Binary(),
                        ThumbPicData = p.Binary(),
                        IsAvailable = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[OilDealDrivers]([Name], [NameUrdu], [Address], [Contact], [Remarks], [IsActive], [DateAdded], [Extra], [CNIC], [PicData], [ThumbData], [ThumbPicData], [IsAvailable])
                      VALUES (@Name, @NameUrdu, @Address, @Contact, @Remarks, @IsActive, @DateAdded, @Extra, @CNIC, @PicData, @ThumbData, @ThumbPicData, @IsAvailable)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[OilDealDrivers]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[OilDealDrivers] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.OilDealDriver_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        CNIC = p.String(),
                        PicData = p.Binary(),
                        ThumbData = p.Binary(),
                        ThumbPicData = p.Binary(),
                        IsAvailable = p.Boolean(),
                    },
                body:
                    @"UPDATE [dbo].[OilDealDrivers]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu, [Address] = @Address, [Contact] = @Contact, [Remarks] = @Remarks, [IsActive] = @IsActive, [DateAdded] = @DateAdded, [Extra] = @Extra, [CNIC] = @CNIC, [PicData] = @PicData, [ThumbData] = @ThumbData, [ThumbPicData] = @ThumbPicData, [IsAvailable] = @IsAvailable
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDealDriver_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[OilDealDrivers]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDealItem_Insert",
                p => new
                    {
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[OilDealItems]([Title], [TitleUrdu], [GeneralAccountId])
                      VALUES (@Title, @TitleUrdu, @GeneralAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[OilDealItems]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[OilDealItems] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.OilDealItem_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[OilDealItems]
                      SET [Title] = @Title, [TitleUrdu] = @TitleUrdu, [GeneralAccountId] = @GeneralAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDealItem_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[OilDealItems]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDealSelector_Insert",
                p => new
                    {
                        Name = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        CNIC = p.String(),
                        PicData = p.Binary(),
                        ThumbData = p.Binary(),
                        ThumbPicData = p.Binary(),
                        IsAvailable = p.Boolean(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[OilDealSelectors]([Name], [Address], [Contact], [Remarks], [IsActive], [DateAdded], [Extra], [CNIC], [PicData], [ThumbData], [ThumbPicData], [IsAvailable], [GeneralAccountId])
                      VALUES (@Name, @Address, @Contact, @Remarks, @IsActive, @DateAdded, @Extra, @CNIC, @PicData, @ThumbData, @ThumbPicData, @IsAvailable, @GeneralAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[OilDealSelectors]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[OilDealSelectors] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.OilDealSelector_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        CNIC = p.String(),
                        PicData = p.Binary(),
                        ThumbData = p.Binary(),
                        ThumbPicData = p.Binary(),
                        IsAvailable = p.Boolean(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[OilDealSelectors]
                      SET [Name] = @Name, [Address] = @Address, [Contact] = @Contact, [Remarks] = @Remarks, [IsActive] = @IsActive, [DateAdded] = @DateAdded, [Extra] = @Extra, [CNIC] = @CNIC, [PicData] = @PicData, [ThumbData] = @ThumbData, [ThumbPicData] = @ThumbPicData, [IsAvailable] = @IsAvailable, [GeneralAccountId] = @GeneralAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDealSelector_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[OilDealSelectors]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilTradeUnit_Insert",
                p => new
                    {
                        Title = p.String(),
                        UnitQty = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[OilTradeUnits]([Title], [UnitQty])
                      VALUES (@Title, @UnitQty)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[OilTradeUnits]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[OilTradeUnits] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.OilTradeUnit_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        UnitQty = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[OilTradeUnits]
                      SET [Title] = @Title, [UnitQty] = @UnitQty
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilTradeUnit_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[OilTradeUnits]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtSchedule_Insert",
                p => new
                    {
                        ScheduleNo = p.String(),
                        OilDirtSelectorId = p.Int(),
                        OilDirtDriverId = p.Int(),
                        VehicleNo = p.String(),
                        VehicleWeightEmpty = p.Decimal(precision: 16, scale: 4),
                        VehicleWeightFull = p.Decimal(precision: 16, scale: 4),
                        LoadedQty = p.Decimal(precision: 16, scale: 4),
                        DealQty = p.Decimal(precision: 16, scale: 4),
                        WeighBridgeWeight = p.Decimal(precision: 16, scale: 4),
                        WeighBridgeId = p.Int(),
                        BrokerSharePercentage = p.Decimal(precision: 16, scale: 4),
                        BrokerShareAmount = p.Decimal(precision: 16, scale: 4),
                        TotalExpectedAmount = p.Decimal(precision: 16, scale: 4),
                        TotalActualAmount = p.Decimal(precision: 16, scale: 4),
                        TotalNetAmount = p.Decimal(precision: 16, scale: 4),
                        ReadyDate = p.DateTime(),
                        CompleteDate = p.DateTime(),
                        OilDirtDealId = p.Int(),
                        PerTradeUnitPrice = p.Decimal(precision: 16, scale: 4),
                        ScheduleDate = p.DateTime(),
                        Unum = p.String(),
                        Status = p.Int(),
                        TotalTradeUnits = p.Decimal(precision: 16, scale: 4),
                        LaborExpenses = p.Decimal(precision: 16, scale: 4),
                        LaborExpensesDescription = p.String(),
                    },
                body:
                    @"INSERT [dbo].[OilDirtSchedules]([ScheduleNo], [OilDirtSelectorId], [OilDirtDriverId], [VehicleNo], [VehicleWeightEmpty], [VehicleWeightFull], [LoadedQty], [DealQty], [WeighBridgeWeight], [WeighBridgeId], [BrokerSharePercentage], [BrokerShareAmount], [TotalExpectedAmount], [TotalActualAmount], [TotalNetAmount], [ReadyDate], [CompleteDate], [OilDirtDealId], [PerTradeUnitPrice], [ScheduleDate], [Unum], [Status], [TotalTradeUnits], [LaborExpenses], [LaborExpensesDescription])
                      VALUES (@ScheduleNo, @OilDirtSelectorId, @OilDirtDriverId, @VehicleNo, @VehicleWeightEmpty, @VehicleWeightFull, @LoadedQty, @DealQty, @WeighBridgeWeight, @WeighBridgeId, @BrokerSharePercentage, @BrokerShareAmount, @TotalExpectedAmount, @TotalActualAmount, @TotalNetAmount, @ReadyDate, @CompleteDate, @OilDirtDealId, @PerTradeUnitPrice, @ScheduleDate, @Unum, @Status, @TotalTradeUnits, @LaborExpenses, @LaborExpensesDescription)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[OilDirtSchedules]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[OilDirtSchedules] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtSchedule_Update",
                p => new
                    {
                        Id = p.Int(),
                        ScheduleNo = p.String(),
                        OilDirtSelectorId = p.Int(),
                        OilDirtDriverId = p.Int(),
                        VehicleNo = p.String(),
                        VehicleWeightEmpty = p.Decimal(precision: 16, scale: 4),
                        VehicleWeightFull = p.Decimal(precision: 16, scale: 4),
                        LoadedQty = p.Decimal(precision: 16, scale: 4),
                        DealQty = p.Decimal(precision: 16, scale: 4),
                        WeighBridgeWeight = p.Decimal(precision: 16, scale: 4),
                        WeighBridgeId = p.Int(),
                        BrokerSharePercentage = p.Decimal(precision: 16, scale: 4),
                        BrokerShareAmount = p.Decimal(precision: 16, scale: 4),
                        TotalExpectedAmount = p.Decimal(precision: 16, scale: 4),
                        TotalActualAmount = p.Decimal(precision: 16, scale: 4),
                        TotalNetAmount = p.Decimal(precision: 16, scale: 4),
                        ReadyDate = p.DateTime(),
                        CompleteDate = p.DateTime(),
                        OilDirtDealId = p.Int(),
                        PerTradeUnitPrice = p.Decimal(precision: 16, scale: 4),
                        ScheduleDate = p.DateTime(),
                        Unum = p.String(),
                        Status = p.Int(),
                        TotalTradeUnits = p.Decimal(precision: 16, scale: 4),
                        LaborExpenses = p.Decimal(precision: 16, scale: 4),
                        LaborExpensesDescription = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[OilDirtSchedules]
                      SET [ScheduleNo] = @ScheduleNo, [OilDirtSelectorId] = @OilDirtSelectorId, [OilDirtDriverId] = @OilDirtDriverId, [VehicleNo] = @VehicleNo, [VehicleWeightEmpty] = @VehicleWeightEmpty, [VehicleWeightFull] = @VehicleWeightFull, [LoadedQty] = @LoadedQty, [DealQty] = @DealQty, [WeighBridgeWeight] = @WeighBridgeWeight, [WeighBridgeId] = @WeighBridgeId, [BrokerSharePercentage] = @BrokerSharePercentage, [BrokerShareAmount] = @BrokerShareAmount, [TotalExpectedAmount] = @TotalExpectedAmount, [TotalActualAmount] = @TotalActualAmount, [TotalNetAmount] = @TotalNetAmount, [ReadyDate] = @ReadyDate, [CompleteDate] = @CompleteDate, [OilDirtDealId] = @OilDirtDealId, [PerTradeUnitPrice] = @PerTradeUnitPrice, [ScheduleDate] = @ScheduleDate, [Unum] = @Unum, [Status] = @Status, [TotalTradeUnits] = @TotalTradeUnits, [LaborExpenses] = @LaborExpenses, [LaborExpensesDescription] = @LaborExpensesDescription
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtSchedule_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[OilDirtSchedules]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtDeal_Insert",
                p => new
                    {
                        OilDirtBrokerId = p.Int(),
                        OilDirtItemId = p.Int(),
                        OilDirtTradeUnitId = p.Int(),
                        PerTradeUnitPrice = p.Decimal(precision: 16, scale: 4),
                        NoOfVehicles = p.Int(),
                        GenerateDate = p.DateTime(),
                        ReadyDate = p.DateTime(),
                        DateCompletion = p.DateTime(),
                        Status = p.Int(),
                        Description = p.String(),
                        CompletionStatus = p.String(),
                        AppUserId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[OilDirtDeals]([OilDirtBrokerId], [OilDirtItemId], [OilDirtTradeUnitId], [PerTradeUnitPrice], [NoOfVehicles], [GenerateDate], [ReadyDate], [DateCompletion], [Status], [Description], [CompletionStatus], [AppUserId])
                      VALUES (@OilDirtBrokerId, @OilDirtItemId, @OilDirtTradeUnitId, @PerTradeUnitPrice, @NoOfVehicles, @GenerateDate, @ReadyDate, @DateCompletion, @Status, @Description, @CompletionStatus, @AppUserId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[OilDirtDeals]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[OilDirtDeals] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtDeal_Update",
                p => new
                    {
                        Id = p.Int(),
                        OilDirtBrokerId = p.Int(),
                        OilDirtItemId = p.Int(),
                        OilDirtTradeUnitId = p.Int(),
                        PerTradeUnitPrice = p.Decimal(precision: 16, scale: 4),
                        NoOfVehicles = p.Int(),
                        GenerateDate = p.DateTime(),
                        ReadyDate = p.DateTime(),
                        DateCompletion = p.DateTime(),
                        Status = p.Int(),
                        Description = p.String(),
                        CompletionStatus = p.String(),
                        AppUserId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[OilDirtDeals]
                      SET [OilDirtBrokerId] = @OilDirtBrokerId, [OilDirtItemId] = @OilDirtItemId, [OilDirtTradeUnitId] = @OilDirtTradeUnitId, [PerTradeUnitPrice] = @PerTradeUnitPrice, [NoOfVehicles] = @NoOfVehicles, [GenerateDate] = @GenerateDate, [ReadyDate] = @ReadyDate, [DateCompletion] = @DateCompletion, [Status] = @Status, [Description] = @Description, [CompletionStatus] = @CompletionStatus, [AppUserId] = @AppUserId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtDeal_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[OilDirtDeals]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtBroker_Insert",
                p => new
                    {
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                        BrokerExpenseAccountId = p.String(),
                    },
                body:
                    @"INSERT [dbo].[OilDirtBrokers]([Name], [NameUrdu], [Address], [Contact], [Remarks], [IsActive], [DateAdded], [Extra], [GeneralAccountId], [BrokerExpenseAccountId])
                      VALUES (@Name, @NameUrdu, @Address, @Contact, @Remarks, @IsActive, @DateAdded, @Extra, @GeneralAccountId, @BrokerExpenseAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[OilDirtBrokers]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[OilDirtBrokers] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtBroker_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                        BrokerExpenseAccountId = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[OilDirtBrokers]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu, [Address] = @Address, [Contact] = @Contact, [Remarks] = @Remarks, [IsActive] = @IsActive, [DateAdded] = @DateAdded, [Extra] = @Extra, [GeneralAccountId] = @GeneralAccountId, [BrokerExpenseAccountId] = @BrokerExpenseAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtBroker_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[OilDirtBrokers]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtItem_Insert",
                p => new
                    {
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[OilDirtItems]([Title], [TitleUrdu], [GeneralAccountId])
                      VALUES (@Title, @TitleUrdu, @GeneralAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[OilDirtItems]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[OilDirtItems] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtItem_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[OilDirtItems]
                      SET [Title] = @Title, [TitleUrdu] = @TitleUrdu, [GeneralAccountId] = @GeneralAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtItem_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[OilDirtItems]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtTradeUnit_Insert",
                p => new
                    {
                        Title = p.String(),
                        UnitQty = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[OilDirtTradeUnits]([Title], [UnitQty])
                      VALUES (@Title, @UnitQty)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[OilDirtTradeUnits]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[OilDirtTradeUnits] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtTradeUnit_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        UnitQty = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[OilDirtTradeUnits]
                      SET [Title] = @Title, [UnitQty] = @UnitQty
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtTradeUnit_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[OilDirtTradeUnits]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtDriver_Insert",
                p => new
                    {
                        Name = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        CNIC = p.String(),
                        PicData = p.Binary(),
                        ThumbData = p.Binary(),
                        ThumbPicData = p.Binary(),
                        IsAvailable = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[OilDirtDrivers]([Name], [Address], [Contact], [Remarks], [IsActive], [DateAdded], [Extra], [CNIC], [PicData], [ThumbData], [ThumbPicData], [IsAvailable])
                      VALUES (@Name, @Address, @Contact, @Remarks, @IsActive, @DateAdded, @Extra, @CNIC, @PicData, @ThumbData, @ThumbPicData, @IsAvailable)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[OilDirtDrivers]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[OilDirtDrivers] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtDriver_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        CNIC = p.String(),
                        PicData = p.Binary(),
                        ThumbData = p.Binary(),
                        ThumbPicData = p.Binary(),
                        IsAvailable = p.Boolean(),
                    },
                body:
                    @"UPDATE [dbo].[OilDirtDrivers]
                      SET [Name] = @Name, [Address] = @Address, [Contact] = @Contact, [Remarks] = @Remarks, [IsActive] = @IsActive, [DateAdded] = @DateAdded, [Extra] = @Extra, [CNIC] = @CNIC, [PicData] = @PicData, [ThumbData] = @ThumbData, [ThumbPicData] = @ThumbPicData, [IsAvailable] = @IsAvailable
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtDriver_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[OilDirtDrivers]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtSelector_Insert",
                p => new
                    {
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        CNIC = p.String(),
                        PicData = p.Binary(),
                        ThumbData = p.Binary(),
                        ThumbPicData = p.Binary(),
                        IsAvailable = p.Boolean(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[OilDirtSelectors]([Name], [NameUrdu], [Address], [Contact], [Remarks], [IsActive], [DateAdded], [Extra], [CNIC], [PicData], [ThumbData], [ThumbPicData], [IsAvailable], [GeneralAccountId])
                      VALUES (@Name, @NameUrdu, @Address, @Contact, @Remarks, @IsActive, @DateAdded, @Extra, @CNIC, @PicData, @ThumbData, @ThumbPicData, @IsAvailable, @GeneralAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[OilDirtSelectors]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[OilDirtSelectors] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtSelector_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        CNIC = p.String(),
                        PicData = p.Binary(),
                        ThumbData = p.Binary(),
                        ThumbPicData = p.Binary(),
                        IsAvailable = p.Boolean(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[OilDirtSelectors]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu, [Address] = @Address, [Contact] = @Contact, [Remarks] = @Remarks, [IsActive] = @IsActive, [DateAdded] = @DateAdded, [Extra] = @Extra, [CNIC] = @CNIC, [PicData] = @PicData, [ThumbData] = @ThumbData, [ThumbPicData] = @ThumbPicData, [IsAvailable] = @IsAvailable, [GeneralAccountId] = @GeneralAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtSelector_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[OilDirtSelectors]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ReadySchedule_Insert",
                p => new
                    {
                        ScheduleDate = p.DateTime(),
                        ReadyDate = p.DateTime(),
                        IsCompleted = p.Boolean(),
                        NoOfVehicles = p.Int(),
                        DispatchedDate = p.DateTime(),
                        ReadyDriverId = p.Int(),
                        ScheduleWeight = p.Decimal(precision: 16, scale: 4),
                        WeighBridgeWeight = p.Decimal(precision: 16, scale: 4),
                        WeighBridgeId = p.Int(),
                        TotalTradeUnits = p.Decimal(precision: 16, scale: 4),
                        PerTradeUnitPrice = p.Decimal(precision: 16, scale: 4),
                        GrossPrice = p.Decimal(precision: 16, scale: 4),
                        BrokerSharePercentage = p.Decimal(precision: 16, scale: 4),
                        BrokerShareAmount = p.Decimal(precision: 16, scale: 4),
                        NetScheduleAmount = p.Decimal(precision: 16, scale: 4),
                        ReadyDealId = p.Int(),
                        ScheduleNo = p.String(),
                        ScheduleType = p.Int(),
                        VehicleNo = p.String(),
                        ReadySelectorId = p.Int(),
                        Unum = p.String(),
                        EmptyVehicleWeight = p.Decimal(precision: 16, scale: 4),
                        FullVehicleWeight = p.Decimal(precision: 16, scale: 4),
                        TotalPackings = p.Decimal(precision: 16, scale: 4),
                        LaborExpenses = p.Decimal(precision: 16, scale: 4),
                        LaborExpensesDescription = p.String(),
                        BardanaAmountExpense = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[ReadySchedules]([ScheduleDate], [ReadyDate], [IsCompleted], [NoOfVehicles], [DispatchedDate], [ReadyDriverId], [ScheduleWeight], [WeighBridgeWeight], [WeighBridgeId], [TotalTradeUnits], [PerTradeUnitPrice], [GrossPrice], [BrokerSharePercentage], [BrokerShareAmount], [NetScheduleAmount], [ReadyDealId], [ScheduleNo], [ScheduleType], [VehicleNo], [ReadySelectorId], [Unum], [EmptyVehicleWeight], [FullVehicleWeight], [TotalPackings], [LaborExpenses], [LaborExpensesDescription], [BardanaAmountExpense])
                      VALUES (@ScheduleDate, @ReadyDate, @IsCompleted, @NoOfVehicles, @DispatchedDate, @ReadyDriverId, @ScheduleWeight, @WeighBridgeWeight, @WeighBridgeId, @TotalTradeUnits, @PerTradeUnitPrice, @GrossPrice, @BrokerSharePercentage, @BrokerShareAmount, @NetScheduleAmount, @ReadyDealId, @ScheduleNo, @ScheduleType, @VehicleNo, @ReadySelectorId, @Unum, @EmptyVehicleWeight, @FullVehicleWeight, @TotalPackings, @LaborExpenses, @LaborExpensesDescription, @BardanaAmountExpense)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ReadySchedules]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ReadySchedules] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ReadySchedule_Update",
                p => new
                    {
                        Id = p.Int(),
                        ScheduleDate = p.DateTime(),
                        ReadyDate = p.DateTime(),
                        IsCompleted = p.Boolean(),
                        NoOfVehicles = p.Int(),
                        DispatchedDate = p.DateTime(),
                        ReadyDriverId = p.Int(),
                        ScheduleWeight = p.Decimal(precision: 16, scale: 4),
                        WeighBridgeWeight = p.Decimal(precision: 16, scale: 4),
                        WeighBridgeId = p.Int(),
                        TotalTradeUnits = p.Decimal(precision: 16, scale: 4),
                        PerTradeUnitPrice = p.Decimal(precision: 16, scale: 4),
                        GrossPrice = p.Decimal(precision: 16, scale: 4),
                        BrokerSharePercentage = p.Decimal(precision: 16, scale: 4),
                        BrokerShareAmount = p.Decimal(precision: 16, scale: 4),
                        NetScheduleAmount = p.Decimal(precision: 16, scale: 4),
                        ReadyDealId = p.Int(),
                        ScheduleNo = p.String(),
                        ScheduleType = p.Int(),
                        VehicleNo = p.String(),
                        ReadySelectorId = p.Int(),
                        Unum = p.String(),
                        EmptyVehicleWeight = p.Decimal(precision: 16, scale: 4),
                        FullVehicleWeight = p.Decimal(precision: 16, scale: 4),
                        TotalPackings = p.Decimal(precision: 16, scale: 4),
                        LaborExpenses = p.Decimal(precision: 16, scale: 4),
                        LaborExpensesDescription = p.String(),
                        BardanaAmountExpense = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[ReadySchedules]
                      SET [ScheduleDate] = @ScheduleDate, [ReadyDate] = @ReadyDate, [IsCompleted] = @IsCompleted, [NoOfVehicles] = @NoOfVehicles, [DispatchedDate] = @DispatchedDate, [ReadyDriverId] = @ReadyDriverId, [ScheduleWeight] = @ScheduleWeight, [WeighBridgeWeight] = @WeighBridgeWeight, [WeighBridgeId] = @WeighBridgeId, [TotalTradeUnits] = @TotalTradeUnits, [PerTradeUnitPrice] = @PerTradeUnitPrice, [GrossPrice] = @GrossPrice, [BrokerSharePercentage] = @BrokerSharePercentage, [BrokerShareAmount] = @BrokerShareAmount, [NetScheduleAmount] = @NetScheduleAmount, [ReadyDealId] = @ReadyDealId, [ScheduleNo] = @ScheduleNo, [ScheduleType] = @ScheduleType, [VehicleNo] = @VehicleNo, [ReadySelectorId] = @ReadySelectorId, [Unum] = @Unum, [EmptyVehicleWeight] = @EmptyVehicleWeight, [FullVehicleWeight] = @FullVehicleWeight, [TotalPackings] = @TotalPackings, [LaborExpenses] = @LaborExpenses, [LaborExpensesDescription] = @LaborExpensesDescription, [BardanaAmountExpense] = @BardanaAmountExpense
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ReadySchedule_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ReadySchedules]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Bharthi_Insert",
                p => new
                    {
                        BharthiTypeId = p.Int(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        Total = p.Decimal(precision: 16, scale: 4),
                        ReadyScheduleId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Bharthis]([BharthiTypeId], [Qty], [Total], [ReadyScheduleId])
                      VALUES (@BharthiTypeId, @Qty, @Total, @ReadyScheduleId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Bharthis]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Bharthis] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Bharthi_Update",
                p => new
                    {
                        Id = p.Int(),
                        BharthiTypeId = p.Int(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        Total = p.Decimal(precision: 16, scale: 4),
                        ReadyScheduleId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Bharthis]
                      SET [BharthiTypeId] = @BharthiTypeId, [Qty] = @Qty, [Total] = @Total, [ReadyScheduleId] = @ReadyScheduleId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Bharthi_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Bharthis]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.BharthiType_Insert",
                p => new
                    {
                        Title = p.String(),
                        UnitQty = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[BharthiTypes]([Title], [UnitQty])
                      VALUES (@Title, @UnitQty)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[BharthiTypes]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[BharthiTypes] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.BharthiType_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        UnitQty = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[BharthiTypes]
                      SET [Title] = @Title, [UnitQty] = @UnitQty
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.BharthiType_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[BharthiTypes]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ReadyDriver_Insert",
                p => new
                    {
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        CNIC = p.String(),
                        PicData = p.Binary(),
                        ThumbData = p.Binary(),
                        ThumbPicData = p.Binary(),
                        IsAvailable = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[ReadyDrivers]([Name], [NameUrdu], [Address], [Contact], [Remarks], [IsActive], [DateAdded], [Extra], [CNIC], [PicData], [ThumbData], [ThumbPicData], [IsAvailable])
                      VALUES (@Name, @NameUrdu, @Address, @Contact, @Remarks, @IsActive, @DateAdded, @Extra, @CNIC, @PicData, @ThumbData, @ThumbPicData, @IsAvailable)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ReadyDrivers]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ReadyDrivers] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ReadyDriver_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        CNIC = p.String(),
                        PicData = p.Binary(),
                        ThumbData = p.Binary(),
                        ThumbPicData = p.Binary(),
                        IsAvailable = p.Boolean(),
                    },
                body:
                    @"UPDATE [dbo].[ReadyDrivers]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu, [Address] = @Address, [Contact] = @Contact, [Remarks] = @Remarks, [IsActive] = @IsActive, [DateAdded] = @DateAdded, [Extra] = @Extra, [CNIC] = @CNIC, [PicData] = @PicData, [ThumbData] = @ThumbData, [ThumbPicData] = @ThumbPicData, [IsAvailable] = @IsAvailable
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ReadyDriver_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ReadyDrivers]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ReadyDeal_Insert",
                p => new
                    {
                        ReadyBrokerId = p.Int(),
                        ReadyItemId = p.Int(),
                        ReadyTradeUnitId = p.Int(),
                        TotalTradeUnits = p.Decimal(precision: 16, scale: 4),
                        PerTradeUnitPrice = p.Decimal(precision: 16, scale: 4),
                        EstimatedPrice = p.Decimal(precision: 16, scale: 4),
                        NoOfVehicles = p.Int(),
                        TotalWeight = p.Decimal(precision: 16, scale: 4),
                        DealDate = p.DateTime(),
                        IsCompleted = p.Boolean(),
                        CompletionStatus = p.String(),
                        DealStatus = p.Int(),
                        Description = p.String(),
                        AppUserId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[ReadyDeals]([ReadyBrokerId], [ReadyItemId], [ReadyTradeUnitId], [TotalTradeUnits], [PerTradeUnitPrice], [EstimatedPrice], [NoOfVehicles], [TotalWeight], [DealDate], [IsCompleted], [CompletionStatus], [DealStatus], [Description], [AppUserId])
                      VALUES (@ReadyBrokerId, @ReadyItemId, @ReadyTradeUnitId, @TotalTradeUnits, @PerTradeUnitPrice, @EstimatedPrice, @NoOfVehicles, @TotalWeight, @DealDate, @IsCompleted, @CompletionStatus, @DealStatus, @Description, @AppUserId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ReadyDeals]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ReadyDeals] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ReadyDeal_Update",
                p => new
                    {
                        Id = p.Int(),
                        ReadyBrokerId = p.Int(),
                        ReadyItemId = p.Int(),
                        ReadyTradeUnitId = p.Int(),
                        TotalTradeUnits = p.Decimal(precision: 16, scale: 4),
                        PerTradeUnitPrice = p.Decimal(precision: 16, scale: 4),
                        EstimatedPrice = p.Decimal(precision: 16, scale: 4),
                        NoOfVehicles = p.Int(),
                        TotalWeight = p.Decimal(precision: 16, scale: 4),
                        DealDate = p.DateTime(),
                        IsCompleted = p.Boolean(),
                        CompletionStatus = p.String(),
                        DealStatus = p.Int(),
                        Description = p.String(),
                        AppUserId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[ReadyDeals]
                      SET [ReadyBrokerId] = @ReadyBrokerId, [ReadyItemId] = @ReadyItemId, [ReadyTradeUnitId] = @ReadyTradeUnitId, [TotalTradeUnits] = @TotalTradeUnits, [PerTradeUnitPrice] = @PerTradeUnitPrice, [EstimatedPrice] = @EstimatedPrice, [NoOfVehicles] = @NoOfVehicles, [TotalWeight] = @TotalWeight, [DealDate] = @DealDate, [IsCompleted] = @IsCompleted, [CompletionStatus] = @CompletionStatus, [DealStatus] = @DealStatus, [Description] = @Description, [AppUserId] = @AppUserId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ReadyDeal_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ReadyDeals]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ReadyBroker_Insert",
                p => new
                    {
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                        BrokerExpenseAccountId = p.String(),
                    },
                body:
                    @"INSERT [dbo].[ReadyBrokers]([Name], [NameUrdu], [Address], [Contact], [Remarks], [IsActive], [DateAdded], [Extra], [GeneralAccountId], [BrokerExpenseAccountId])
                      VALUES (@Name, @NameUrdu, @Address, @Contact, @Remarks, @IsActive, @DateAdded, @Extra, @GeneralAccountId, @BrokerExpenseAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ReadyBrokers]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ReadyBrokers] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ReadyBroker_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                        BrokerExpenseAccountId = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[ReadyBrokers]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu, [Address] = @Address, [Contact] = @Contact, [Remarks] = @Remarks, [IsActive] = @IsActive, [DateAdded] = @DateAdded, [Extra] = @Extra, [GeneralAccountId] = @GeneralAccountId, [BrokerExpenseAccountId] = @BrokerExpenseAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ReadyBroker_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ReadyBrokers]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ReadyItem_Insert",
                p => new
                    {
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        StockQty = p.Decimal(precision: 16, scale: 4),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[ReadyItems]([Title], [TitleUrdu], [StockQty], [GeneralAccountId])
                      VALUES (@Title, @TitleUrdu, @StockQty, @GeneralAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ReadyItems]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ReadyItems] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ReadyItem_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        StockQty = p.Decimal(precision: 16, scale: 4),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[ReadyItems]
                      SET [Title] = @Title, [TitleUrdu] = @TitleUrdu, [StockQty] = @StockQty, [GeneralAccountId] = @GeneralAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ReadyItem_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ReadyItems]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ReadyTradeUnit_Insert",
                p => new
                    {
                        Title = p.String(),
                        UnitQty = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[ReadyTradeUnits]([Title], [UnitQty])
                      VALUES (@Title, @UnitQty)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ReadyTradeUnits]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ReadyTradeUnits] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ReadyTradeUnit_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        UnitQty = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[ReadyTradeUnits]
                      SET [Title] = @Title, [UnitQty] = @UnitQty
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ReadyTradeUnit_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ReadyTradeUnits]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ReadySelector_Insert",
                p => new
                    {
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        CNIC = p.String(),
                        PicData = p.Binary(),
                        ThumbData = p.Binary(),
                        ThumbPicData = p.Binary(),
                        IsAvailable = p.Boolean(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[ReadySelectors]([Name], [NameUrdu], [Address], [Contact], [Remarks], [IsActive], [DateAdded], [Extra], [CNIC], [PicData], [ThumbData], [ThumbPicData], [IsAvailable], [GeneralAccountId])
                      VALUES (@Name, @NameUrdu, @Address, @Contact, @Remarks, @IsActive, @DateAdded, @Extra, @CNIC, @PicData, @ThumbData, @ThumbPicData, @IsAvailable, @GeneralAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ReadySelectors]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ReadySelectors] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ReadySelector_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        Contact = p.String(),
                        Remarks = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Extra = p.String(),
                        CNIC = p.String(),
                        PicData = p.Binary(),
                        ThumbData = p.Binary(),
                        ThumbPicData = p.Binary(),
                        IsAvailable = p.Boolean(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[ReadySelectors]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu, [Address] = @Address, [Contact] = @Contact, [Remarks] = @Remarks, [IsActive] = @IsActive, [DateAdded] = @DateAdded, [Extra] = @Extra, [CNIC] = @CNIC, [PicData] = @PicData, [ThumbData] = @ThumbData, [ThumbPicData] = @ThumbPicData, [IsAvailable] = @IsAvailable, [GeneralAccountId] = @GeneralAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ReadySelector_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ReadySelectors]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.SaleOrderLine_Insert",
                p => new
                    {
                        ProductId = p.Int(),
                        SaleOrderId = p.Int(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        UnitPrice = p.Decimal(precision: 16, scale: 4),
                        TotalPrice = p.Decimal(precision: 16, scale: 4),
                        Discount = p.Decimal(precision: 16, scale: 4),
                        NetPrice = p.Decimal(precision: 16, scale: 4),
                        ApplyLaborExpense = p.Boolean(),
                        DeductBardanaExpense = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[SaleOrderLines]([ProductId], [SaleOrderId], [Qty], [UnitPrice], [TotalPrice], [Discount], [NetPrice], [ApplyLaborExpense], [DeductBardanaExpense])
                      VALUES (@ProductId, @SaleOrderId, @Qty, @UnitPrice, @TotalPrice, @Discount, @NetPrice, @ApplyLaborExpense, @DeductBardanaExpense)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[SaleOrderLines]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[SaleOrderLines] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.SaleOrderLine_Update",
                p => new
                    {
                        Id = p.Int(),
                        ProductId = p.Int(),
                        SaleOrderId = p.Int(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        UnitPrice = p.Decimal(precision: 16, scale: 4),
                        TotalPrice = p.Decimal(precision: 16, scale: 4),
                        Discount = p.Decimal(precision: 16, scale: 4),
                        NetPrice = p.Decimal(precision: 16, scale: 4),
                        ApplyLaborExpense = p.Boolean(),
                        DeductBardanaExpense = p.Boolean(),
                    },
                body:
                    @"UPDATE [dbo].[SaleOrderLines]
                      SET [ProductId] = @ProductId, [SaleOrderId] = @SaleOrderId, [Qty] = @Qty, [UnitPrice] = @UnitPrice, [TotalPrice] = @TotalPrice, [Discount] = @Discount, [NetPrice] = @NetPrice, [ApplyLaborExpense] = @ApplyLaborExpense, [DeductBardanaExpense] = @DeductBardanaExpense
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.SaleOrderLine_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[SaleOrderLines]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Product_Insert",
                p => new
                    {
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        CostPrice = p.Decimal(precision: 16, scale: 4),
                        ProductTotalUnitPrice = p.Decimal(precision: 16, scale: 4),
                        ProductNetUnitPriceWholeSale = p.Decimal(precision: 16, scale: 4),
                        IsAvailable = p.Boolean(),
                        SKU = p.Decimal(precision: 16, scale: 4),
                        Barcode = p.String(),
                        AlertOnSale = p.Boolean(),
                        IsDicountable = p.Boolean(),
                        ProductCategoryId = p.Int(),
                        ProductDiscPercentage = p.Decimal(precision: 16, scale: 4),
                        ProductSizeId = p.Int(),
                        ProductPoints = p.Decimal(precision: 16, scale: 4),
                        GeneralAccountId = p.String(maxLength: 128),
                        StockItemId = p.Int(),
                        Weight = p.Decimal(precision: 16, scale: 4),
                        DeductBardanaPacking = p.Boolean(),
                        ApplyLaborExpense = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[Products]([Title], [TitleUrdu], [CostPrice], [ProductTotalUnitPrice], [ProductNetUnitPriceWholeSale], [IsAvailable], [SKU], [Barcode], [AlertOnSale], [IsDicountable], [ProductCategoryId], [ProductDiscPercentage], [ProductSizeId], [ProductPoints], [GeneralAccountId], [StockItemId], [Weight], [DeductBardanaPacking], [ApplyLaborExpense])
                      VALUES (@Title, @TitleUrdu, @CostPrice, @ProductTotalUnitPrice, @ProductNetUnitPriceWholeSale, @IsAvailable, @SKU, @Barcode, @AlertOnSale, @IsDicountable, @ProductCategoryId, @ProductDiscPercentage, @ProductSizeId, @ProductPoints, @GeneralAccountId, @StockItemId, @Weight, @DeductBardanaPacking, @ApplyLaborExpense)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Products]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Products] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Product_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        CostPrice = p.Decimal(precision: 16, scale: 4),
                        ProductTotalUnitPrice = p.Decimal(precision: 16, scale: 4),
                        ProductNetUnitPriceWholeSale = p.Decimal(precision: 16, scale: 4),
                        IsAvailable = p.Boolean(),
                        SKU = p.Decimal(precision: 16, scale: 4),
                        Barcode = p.String(),
                        AlertOnSale = p.Boolean(),
                        IsDicountable = p.Boolean(),
                        ProductCategoryId = p.Int(),
                        ProductDiscPercentage = p.Decimal(precision: 16, scale: 4),
                        ProductSizeId = p.Int(),
                        ProductPoints = p.Decimal(precision: 16, scale: 4),
                        GeneralAccountId = p.String(maxLength: 128),
                        StockItemId = p.Int(),
                        Weight = p.Decimal(precision: 16, scale: 4),
                        DeductBardanaPacking = p.Boolean(),
                        ApplyLaborExpense = p.Boolean(),
                    },
                body:
                    @"UPDATE [dbo].[Products]
                      SET [Title] = @Title, [TitleUrdu] = @TitleUrdu, [CostPrice] = @CostPrice, [ProductTotalUnitPrice] = @ProductTotalUnitPrice, [ProductNetUnitPriceWholeSale] = @ProductNetUnitPriceWholeSale, [IsAvailable] = @IsAvailable, [SKU] = @SKU, [Barcode] = @Barcode, [AlertOnSale] = @AlertOnSale, [IsDicountable] = @IsDicountable, [ProductCategoryId] = @ProductCategoryId, [ProductDiscPercentage] = @ProductDiscPercentage, [ProductSizeId] = @ProductSizeId, [ProductPoints] = @ProductPoints, [GeneralAccountId] = @GeneralAccountId, [StockItemId] = @StockItemId, [Weight] = @Weight, [DeductBardanaPacking] = @DeductBardanaPacking, [ApplyLaborExpense] = @ApplyLaborExpense
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Product_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Products]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ProductCategory_Insert",
                p => new
                    {
                        Title = p.String(),
                        TitleUrdu = p.String(),
                    },
                body:
                    @"INSERT [dbo].[ProductCategories]([Title], [TitleUrdu])
                      VALUES (@Title, @TitleUrdu)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ProductCategories]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ProductCategories] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ProductCategory_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        TitleUrdu = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[ProductCategories]
                      SET [Title] = @Title, [TitleUrdu] = @TitleUrdu
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ProductCategory_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ProductCategories]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ProductSize_Insert",
                p => new
                    {
                        Title = p.String(),
                        TitleUrdu = p.String(),
                    },
                body:
                    @"INSERT [dbo].[ProductSizes]([Title], [TitleUrdu])
                      VALUES (@Title, @TitleUrdu)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ProductSizes]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ProductSizes] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ProductSize_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        TitleUrdu = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[ProductSizes]
                      SET [Title] = @Title, [TitleUrdu] = @TitleUrdu
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ProductSize_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ProductSizes]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.StockItem_Insert",
                p => new
                    {
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        SKU = p.Decimal(precision: 16, scale: 4),
                        UnitPrice = p.Decimal(precision: 16, scale: 4),
                        IsActive = p.Boolean(),
                        Description = p.String(),
                    },
                body:
                    @"INSERT [dbo].[StockItems]([Title], [TitleUrdu], [SKU], [UnitPrice], [IsActive], [Description])
                      VALUES (@Title, @TitleUrdu, @SKU, @UnitPrice, @IsActive, @Description)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[StockItems]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[StockItems] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.StockItem_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        SKU = p.Decimal(precision: 16, scale: 4),
                        UnitPrice = p.Decimal(precision: 16, scale: 4),
                        IsActive = p.Boolean(),
                        Description = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[StockItems]
                      SET [Title] = @Title, [TitleUrdu] = @TitleUrdu, [SKU] = @SKU, [UnitPrice] = @UnitPrice, [IsActive] = @IsActive, [Description] = @Description
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.StockItem_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[StockItems]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.SIEntry_Insert",
                p => new
                    {
                        QtyAdded = p.Decimal(precision: 16, scale: 4),
                        Dated = p.DateTime(),
                        DateTime = p.DateTime(),
                        Description = p.String(),
                        StockItemId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[SIEntries]([QtyAdded], [Dated], [DateTime], [Description], [StockItemId])
                      VALUES (@QtyAdded, @Dated, @DateTime, @Description, @StockItemId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[SIEntries]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[SIEntries] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.SIEntry_Update",
                p => new
                    {
                        Id = p.Int(),
                        QtyAdded = p.Decimal(precision: 16, scale: 4),
                        Dated = p.DateTime(),
                        DateTime = p.DateTime(),
                        Description = p.String(),
                        StockItemId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[SIEntries]
                      SET [QtyAdded] = @QtyAdded, [Dated] = @Dated, [DateTime] = @DateTime, [Description] = @Description, [StockItemId] = @StockItemId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.SIEntry_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[SIEntries]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ArchiveOrderLine_Insert",
                p => new
                    {
                        ProductId = p.Int(),
                        ArchiveSaleOrderId = p.Int(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        UnitPrice = p.Decimal(precision: 16, scale: 4),
                        TotalPrice = p.Decimal(precision: 16, scale: 4),
                        Discount = p.Decimal(precision: 16, scale: 4),
                        NetPrice = p.Decimal(precision: 16, scale: 4),
                        ApplyLaborExpense = p.Boolean(),
                        DeductBardanaExpense = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[ArchiveOrderLines]([ProductId], [ArchiveSaleOrderId], [Qty], [UnitPrice], [TotalPrice], [Discount], [NetPrice], [ApplyLaborExpense], [DeductBardanaExpense])
                      VALUES (@ProductId, @ArchiveSaleOrderId, @Qty, @UnitPrice, @TotalPrice, @Discount, @NetPrice, @ApplyLaborExpense, @DeductBardanaExpense)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ArchiveOrderLines]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ArchiveOrderLines] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ArchiveOrderLine_Update",
                p => new
                    {
                        Id = p.Int(),
                        ProductId = p.Int(),
                        ArchiveSaleOrderId = p.Int(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        UnitPrice = p.Decimal(precision: 16, scale: 4),
                        TotalPrice = p.Decimal(precision: 16, scale: 4),
                        Discount = p.Decimal(precision: 16, scale: 4),
                        NetPrice = p.Decimal(precision: 16, scale: 4),
                        ApplyLaborExpense = p.Boolean(),
                        DeductBardanaExpense = p.Boolean(),
                    },
                body:
                    @"UPDATE [dbo].[ArchiveOrderLines]
                      SET [ProductId] = @ProductId, [ArchiveSaleOrderId] = @ArchiveSaleOrderId, [Qty] = @Qty, [UnitPrice] = @UnitPrice, [TotalPrice] = @TotalPrice, [Discount] = @Discount, [NetPrice] = @NetPrice, [ApplyLaborExpense] = @ApplyLaborExpense, [DeductBardanaExpense] = @DeductBardanaExpense
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ArchiveOrderLine_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ArchiveOrderLines]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.CancelSaleOrder_Insert",
                p => new
                    {
                        OrderId = p.String(),
                        CustomerId = p.Int(),
                        OrderDate = p.DateTime(),
                        TimpStamp = p.DateTime(),
                        BardanaPackingsCount = p.Int(),
                        TotalItems = p.Int(),
                        UniqueItems = p.Int(),
                        TototPrice = p.Decimal(precision: 16, scale: 4),
                        TotalDiscount = p.Decimal(precision: 16, scale: 4),
                        TotalDiscountPercentage = p.Decimal(precision: 16, scale: 4),
                        NetPrice = p.Decimal(precision: 16, scale: 4),
                        IsCredit = p.Boolean(),
                        IsWalkIn = p.Boolean(),
                        Unum = p.String(),
                        CustomerDiscount = p.Decimal(precision: 16, scale: 4),
                        CustomerDiscountPecentage = p.Decimal(precision: 16, scale: 4),
                        ServiceCharges = p.Decimal(precision: 16, scale: 4),
                        ServiceChargesPercentage = p.Decimal(precision: 16, scale: 4),
                        SaleTaxAmount = p.Decimal(precision: 16, scale: 4),
                        SaleTaxPercentage = p.Decimal(precision: 16, scale: 4),
                        IsExtraAmounted = p.Boolean(),
                        IsExpensed = p.Boolean(),
                        OrderDiscount = p.Decimal(precision: 16, scale: 4),
                        OrderDiscountAmount = p.Decimal(precision: 16, scale: 4),
                        OfferDiscount = p.Decimal(precision: 16, scale: 4),
                        OfferDiscountPercentage = p.Decimal(precision: 16, scale: 4),
                        AmountGiven = p.Decimal(precision: 16, scale: 4),
                        ChangeGiven = p.Decimal(precision: 16, scale: 4),
                        RemainingAmount = p.Decimal(precision: 16, scale: 4),
                        OrderType = p.Int(),
                        IsDone = p.Boolean(),
                        BardanaAmount = p.Decimal(precision: 16, scale: 4),
                        LaborAmount = p.Decimal(precision: 16, scale: 4),
                        TotalWeight = p.Decimal(precision: 16, scale: 4),
                        TotalPackings = p.Decimal(precision: 16, scale: 4),
                        AppUserId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[CancelSaleOrders]([OrderId], [CustomerId], [OrderDate], [TimpStamp], [BardanaPackingsCount], [TotalItems], [UniqueItems], [TototPrice], [TotalDiscount], [TotalDiscountPercentage], [NetPrice], [IsCredit], [IsWalkIn], [Unum], [CustomerDiscount], [CustomerDiscountPecentage], [ServiceCharges], [ServiceChargesPercentage], [SaleTaxAmount], [SaleTaxPercentage], [IsExtraAmounted], [IsExpensed], [OrderDiscount], [OrderDiscountAmount], [OfferDiscount], [OfferDiscountPercentage], [AmountGiven], [ChangeGiven], [RemainingAmount], [OrderType], [IsDone], [BardanaAmount], [LaborAmount], [TotalWeight], [TotalPackings], [AppUserId])
                      VALUES (@OrderId, @CustomerId, @OrderDate, @TimpStamp, @BardanaPackingsCount, @TotalItems, @UniqueItems, @TototPrice, @TotalDiscount, @TotalDiscountPercentage, @NetPrice, @IsCredit, @IsWalkIn, @Unum, @CustomerDiscount, @CustomerDiscountPecentage, @ServiceCharges, @ServiceChargesPercentage, @SaleTaxAmount, @SaleTaxPercentage, @IsExtraAmounted, @IsExpensed, @OrderDiscount, @OrderDiscountAmount, @OfferDiscount, @OfferDiscountPercentage, @AmountGiven, @ChangeGiven, @RemainingAmount, @OrderType, @IsDone, @BardanaAmount, @LaborAmount, @TotalWeight, @TotalPackings, @AppUserId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[CancelSaleOrders]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[CancelSaleOrders] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.CancelSaleOrder_Update",
                p => new
                    {
                        Id = p.Int(),
                        OrderId = p.String(),
                        CustomerId = p.Int(),
                        OrderDate = p.DateTime(),
                        TimpStamp = p.DateTime(),
                        BardanaPackingsCount = p.Int(),
                        TotalItems = p.Int(),
                        UniqueItems = p.Int(),
                        TototPrice = p.Decimal(precision: 16, scale: 4),
                        TotalDiscount = p.Decimal(precision: 16, scale: 4),
                        TotalDiscountPercentage = p.Decimal(precision: 16, scale: 4),
                        NetPrice = p.Decimal(precision: 16, scale: 4),
                        IsCredit = p.Boolean(),
                        IsWalkIn = p.Boolean(),
                        Unum = p.String(),
                        CustomerDiscount = p.Decimal(precision: 16, scale: 4),
                        CustomerDiscountPecentage = p.Decimal(precision: 16, scale: 4),
                        ServiceCharges = p.Decimal(precision: 16, scale: 4),
                        ServiceChargesPercentage = p.Decimal(precision: 16, scale: 4),
                        SaleTaxAmount = p.Decimal(precision: 16, scale: 4),
                        SaleTaxPercentage = p.Decimal(precision: 16, scale: 4),
                        IsExtraAmounted = p.Boolean(),
                        IsExpensed = p.Boolean(),
                        OrderDiscount = p.Decimal(precision: 16, scale: 4),
                        OrderDiscountAmount = p.Decimal(precision: 16, scale: 4),
                        OfferDiscount = p.Decimal(precision: 16, scale: 4),
                        OfferDiscountPercentage = p.Decimal(precision: 16, scale: 4),
                        AmountGiven = p.Decimal(precision: 16, scale: 4),
                        ChangeGiven = p.Decimal(precision: 16, scale: 4),
                        RemainingAmount = p.Decimal(precision: 16, scale: 4),
                        OrderType = p.Int(),
                        IsDone = p.Boolean(),
                        BardanaAmount = p.Decimal(precision: 16, scale: 4),
                        LaborAmount = p.Decimal(precision: 16, scale: 4),
                        TotalWeight = p.Decimal(precision: 16, scale: 4),
                        TotalPackings = p.Decimal(precision: 16, scale: 4),
                        AppUserId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[CancelSaleOrders]
                      SET [OrderId] = @OrderId, [CustomerId] = @CustomerId, [OrderDate] = @OrderDate, [TimpStamp] = @TimpStamp, [BardanaPackingsCount] = @BardanaPackingsCount, [TotalItems] = @TotalItems, [UniqueItems] = @UniqueItems, [TototPrice] = @TototPrice, [TotalDiscount] = @TotalDiscount, [TotalDiscountPercentage] = @TotalDiscountPercentage, [NetPrice] = @NetPrice, [IsCredit] = @IsCredit, [IsWalkIn] = @IsWalkIn, [Unum] = @Unum, [CustomerDiscount] = @CustomerDiscount, [CustomerDiscountPecentage] = @CustomerDiscountPecentage, [ServiceCharges] = @ServiceCharges, [ServiceChargesPercentage] = @ServiceChargesPercentage, [SaleTaxAmount] = @SaleTaxAmount, [SaleTaxPercentage] = @SaleTaxPercentage, [IsExtraAmounted] = @IsExtraAmounted, [IsExpensed] = @IsExpensed, [OrderDiscount] = @OrderDiscount, [OrderDiscountAmount] = @OrderDiscountAmount, [OfferDiscount] = @OfferDiscount, [OfferDiscountPercentage] = @OfferDiscountPercentage, [AmountGiven] = @AmountGiven, [ChangeGiven] = @ChangeGiven, [RemainingAmount] = @RemainingAmount, [OrderType] = @OrderType, [IsDone] = @IsDone, [BardanaAmount] = @BardanaAmount, [LaborAmount] = @LaborAmount, [TotalWeight] = @TotalWeight, [TotalPackings] = @TotalPackings, [AppUserId] = @AppUserId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.CancelSaleOrder_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[CancelSaleOrders]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.CancelOrderLine_Insert",
                p => new
                    {
                        ProductId = p.Int(),
                        CancelSaleOrderId = p.Int(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        UnitPrice = p.Decimal(precision: 16, scale: 4),
                        TotalPrice = p.Decimal(precision: 16, scale: 4),
                        Discount = p.Decimal(precision: 16, scale: 4),
                        NetPrice = p.Decimal(precision: 16, scale: 4),
                        ApplyLaborExpense = p.Boolean(),
                        DeductBardanaExpense = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[CancelOrderLines]([ProductId], [CancelSaleOrderId], [Qty], [UnitPrice], [TotalPrice], [Discount], [NetPrice], [ApplyLaborExpense], [DeductBardanaExpense])
                      VALUES (@ProductId, @CancelSaleOrderId, @Qty, @UnitPrice, @TotalPrice, @Discount, @NetPrice, @ApplyLaborExpense, @DeductBardanaExpense)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[CancelOrderLines]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[CancelOrderLines] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.CancelOrderLine_Update",
                p => new
                    {
                        Id = p.Int(),
                        ProductId = p.Int(),
                        CancelSaleOrderId = p.Int(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        UnitPrice = p.Decimal(precision: 16, scale: 4),
                        TotalPrice = p.Decimal(precision: 16, scale: 4),
                        Discount = p.Decimal(precision: 16, scale: 4),
                        NetPrice = p.Decimal(precision: 16, scale: 4),
                        ApplyLaborExpense = p.Boolean(),
                        DeductBardanaExpense = p.Boolean(),
                    },
                body:
                    @"UPDATE [dbo].[CancelOrderLines]
                      SET [ProductId] = @ProductId, [CancelSaleOrderId] = @CancelSaleOrderId, [Qty] = @Qty, [UnitPrice] = @UnitPrice, [TotalPrice] = @TotalPrice, [Discount] = @Discount, [NetPrice] = @NetPrice, [ApplyLaborExpense] = @ApplyLaborExpense, [DeductBardanaExpense] = @DeductBardanaExpense
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.CancelOrderLine_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[CancelOrderLines]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.AppUserOptions_Insert",
                p => new
                    {
                        AppUserId = p.String(maxLength: 128),
                        CanAddNewCustomer = p.Boolean(),
                        CanViewTopCustomers = p.Boolean(),
                        CanViewCustomerList = p.Boolean(),
                        CanAddNewAppUser = p.Boolean(),
                        CanViewAppUsers = p.Boolean(),
                        CanAddNewDeliveryBoy = p.Boolean(),
                        CanViewDeliveryBoyList = p.Boolean(),
                        CanViewDeliveryBoyPerformance = p.Boolean(),
                        CanAddNewEmployee = p.Boolean(),
                        CanViewEmployeeList = p.Boolean(),
                        CanUseAttendanceInForm = p.Boolean(),
                        CanUseAttendanceOutForm = p.Boolean(),
                        CanViewAttendanceList = p.Boolean(),
                        CanAddNewProduct = p.Boolean(),
                        CanViewProductList = p.Boolean(),
                        CanAddNewProductCategory = p.Boolean(),
                        CanViewProductCategories = p.Boolean(),
                        CanUseShopInfoForm = p.Boolean(),
                        CanWastageItem = p.Boolean(),
                        CanViewWastageItems = p.Boolean(),
                        CanPlaceNewOrder = p.Boolean(),
                        CanViewOrdersList = p.Boolean(),
                        CanReprintAnOrder = p.Boolean(),
                        CanCanelAnOrder = p.Boolean(),
                        CanViewHomeDeliveryOrders = p.Boolean(),
                        CanUpdateHomeDeliveryOrder = p.Boolean(),
                        CanAddNewDeal = p.Boolean(),
                        CanViewDealList = p.Boolean(),
                        CanAddNewServiceTable = p.Boolean(),
                        CanViewTableServiceList = p.Boolean(),
                        CanManageTableServices = p.Boolean(),
                        CanViewCancelOrders = p.Boolean(),
                        CanViewProfitChart = p.Boolean(),
                        CanViewStockDetails = p.Boolean(),
                        CanAddDailyExpense = p.Boolean(),
                        CanViewExpenses = p.Boolean(),
                        CanViewDailyOrdersChart = p.Boolean(),
                        CanViewDailyReports = p.Boolean(),
                        CanViewDailySalesChart = p.Boolean(),
                        CanViewDailyConsumption = p.Boolean(),
                        CanViewSmsSubscribers = p.Boolean(),
                        CanAddNewSmsSubscriber = p.Boolean(),
                        CanViewSmsSettingsForm = p.Boolean(),
                        CanSendSmsToCustomers = p.Boolean(),
                        CanAddNewRawCategory = p.Boolean(),
                        CanViewRawCategories = p.Boolean(),
                        CanAddRawUnit = p.Boolean(),
                        CanViewRawUnits = p.Boolean(),
                        CanAddNewSupplier = p.Boolean(),
                        CanViewSuppliers = p.Boolean(),
                        CanAddNewRawItem = p.Boolean(),
                        CanViewRawItems = p.Boolean(),
                        CanAddItemInStock = p.Boolean(),
                        CanViewItemAddedStockHistory = p.Boolean(),
                        CanConsumeRawItem = p.Boolean(),
                        CanViewConsumptionRawItemList = p.Boolean(),
                        CanIssueRawItem = p.Boolean(),
                        CanViewIssueRawItemRecords = p.Boolean(),
                        CanViewPaymentRecords = p.Boolean(),
                        CanAddPackings = p.Boolean(),
                        CanViewPackings = p.Boolean(),
                        CanAddAssetCompany = p.Boolean(),
                        CanViewAssetCompanies = p.Boolean(),
                        CanAddAssetCategory = p.Boolean(),
                        CanViewAssetCategories = p.Boolean(),
                        CanAddAssetItem = p.Boolean(),
                        CanViewAssetItem = p.Boolean(),
                        CanViewAssetItemReport = p.Boolean(),
                        CanWastageAssetItem = p.Boolean(),
                        Misc1 = p.Boolean(),
                        Misc2 = p.Boolean(),
                        Misc3 = p.Boolean(),
                        Misc4 = p.Boolean(),
                        Misc5 = p.Boolean(),
                        Misc6 = p.Boolean(),
                        Misc7 = p.Boolean(),
                        Misc8 = p.Boolean(),
                        Misc9 = p.Boolean(),
                        Misc10 = p.Boolean(),
                        Misc11 = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[AppUserOptions]([AppUserId], [CanAddNewCustomer], [CanViewTopCustomers], [CanViewCustomerList], [CanAddNewAppUser], [CanViewAppUsers], [CanAddNewDeliveryBoy], [CanViewDeliveryBoyList], [CanViewDeliveryBoyPerformance], [CanAddNewEmployee], [CanViewEmployeeList], [CanUseAttendanceInForm], [CanUseAttendanceOutForm], [CanViewAttendanceList], [CanAddNewProduct], [CanViewProductList], [CanAddNewProductCategory], [CanViewProductCategories], [CanUseShopInfoForm], [CanWastageItem], [CanViewWastageItems], [CanPlaceNewOrder], [CanViewOrdersList], [CanReprintAnOrder], [CanCanelAnOrder], [CanViewHomeDeliveryOrders], [CanUpdateHomeDeliveryOrder], [CanAddNewDeal], [CanViewDealList], [CanAddNewServiceTable], [CanViewTableServiceList], [CanManageTableServices], [CanViewCancelOrders], [CanViewProfitChart], [CanViewStockDetails], [CanAddDailyExpense], [CanViewExpenses], [CanViewDailyOrdersChart], [CanViewDailyReports], [CanViewDailySalesChart], [CanViewDailyConsumption], [CanViewSmsSubscribers], [CanAddNewSmsSubscriber], [CanViewSmsSettingsForm], [CanSendSmsToCustomers], [CanAddNewRawCategory], [CanViewRawCategories], [CanAddRawUnit], [CanViewRawUnits], [CanAddNewSupplier], [CanViewSuppliers], [CanAddNewRawItem], [CanViewRawItems], [CanAddItemInStock], [CanViewItemAddedStockHistory], [CanConsumeRawItem], [CanViewConsumptionRawItemList], [CanIssueRawItem], [CanViewIssueRawItemRecords], [CanViewPaymentRecords], [CanAddPackings], [CanViewPackings], [CanAddAssetCompany], [CanViewAssetCompanies], [CanAddAssetCategory], [CanViewAssetCategories], [CanAddAssetItem], [CanViewAssetItem], [CanViewAssetItemReport], [CanWastageAssetItem], [Misc1], [Misc2], [Misc3], [Misc4], [Misc5], [Misc6], [Misc7], [Misc8], [Misc9], [Misc10], [Misc11])
                      VALUES (@AppUserId, @CanAddNewCustomer, @CanViewTopCustomers, @CanViewCustomerList, @CanAddNewAppUser, @CanViewAppUsers, @CanAddNewDeliveryBoy, @CanViewDeliveryBoyList, @CanViewDeliveryBoyPerformance, @CanAddNewEmployee, @CanViewEmployeeList, @CanUseAttendanceInForm, @CanUseAttendanceOutForm, @CanViewAttendanceList, @CanAddNewProduct, @CanViewProductList, @CanAddNewProductCategory, @CanViewProductCategories, @CanUseShopInfoForm, @CanWastageItem, @CanViewWastageItems, @CanPlaceNewOrder, @CanViewOrdersList, @CanReprintAnOrder, @CanCanelAnOrder, @CanViewHomeDeliveryOrders, @CanUpdateHomeDeliveryOrder, @CanAddNewDeal, @CanViewDealList, @CanAddNewServiceTable, @CanViewTableServiceList, @CanManageTableServices, @CanViewCancelOrders, @CanViewProfitChart, @CanViewStockDetails, @CanAddDailyExpense, @CanViewExpenses, @CanViewDailyOrdersChart, @CanViewDailyReports, @CanViewDailySalesChart, @CanViewDailyConsumption, @CanViewSmsSubscribers, @CanAddNewSmsSubscriber, @CanViewSmsSettingsForm, @CanSendSmsToCustomers, @CanAddNewRawCategory, @CanViewRawCategories, @CanAddRawUnit, @CanViewRawUnits, @CanAddNewSupplier, @CanViewSuppliers, @CanAddNewRawItem, @CanViewRawItems, @CanAddItemInStock, @CanViewItemAddedStockHistory, @CanConsumeRawItem, @CanViewConsumptionRawItemList, @CanIssueRawItem, @CanViewIssueRawItemRecords, @CanViewPaymentRecords, @CanAddPackings, @CanViewPackings, @CanAddAssetCompany, @CanViewAssetCompanies, @CanAddAssetCategory, @CanViewAssetCategories, @CanAddAssetItem, @CanViewAssetItem, @CanViewAssetItemReport, @CanWastageAssetItem, @Misc1, @Misc2, @Misc3, @Misc4, @Misc5, @Misc6, @Misc7, @Misc8, @Misc9, @Misc10, @Misc11)"
            );
            
            CreateStoredProcedure(
                "dbo.AppUserOptions_Update",
                p => new
                    {
                        AppUserId = p.String(maxLength: 128),
                        CanAddNewCustomer = p.Boolean(),
                        CanViewTopCustomers = p.Boolean(),
                        CanViewCustomerList = p.Boolean(),
                        CanAddNewAppUser = p.Boolean(),
                        CanViewAppUsers = p.Boolean(),
                        CanAddNewDeliveryBoy = p.Boolean(),
                        CanViewDeliveryBoyList = p.Boolean(),
                        CanViewDeliveryBoyPerformance = p.Boolean(),
                        CanAddNewEmployee = p.Boolean(),
                        CanViewEmployeeList = p.Boolean(),
                        CanUseAttendanceInForm = p.Boolean(),
                        CanUseAttendanceOutForm = p.Boolean(),
                        CanViewAttendanceList = p.Boolean(),
                        CanAddNewProduct = p.Boolean(),
                        CanViewProductList = p.Boolean(),
                        CanAddNewProductCategory = p.Boolean(),
                        CanViewProductCategories = p.Boolean(),
                        CanUseShopInfoForm = p.Boolean(),
                        CanWastageItem = p.Boolean(),
                        CanViewWastageItems = p.Boolean(),
                        CanPlaceNewOrder = p.Boolean(),
                        CanViewOrdersList = p.Boolean(),
                        CanReprintAnOrder = p.Boolean(),
                        CanCanelAnOrder = p.Boolean(),
                        CanViewHomeDeliveryOrders = p.Boolean(),
                        CanUpdateHomeDeliveryOrder = p.Boolean(),
                        CanAddNewDeal = p.Boolean(),
                        CanViewDealList = p.Boolean(),
                        CanAddNewServiceTable = p.Boolean(),
                        CanViewTableServiceList = p.Boolean(),
                        CanManageTableServices = p.Boolean(),
                        CanViewCancelOrders = p.Boolean(),
                        CanViewProfitChart = p.Boolean(),
                        CanViewStockDetails = p.Boolean(),
                        CanAddDailyExpense = p.Boolean(),
                        CanViewExpenses = p.Boolean(),
                        CanViewDailyOrdersChart = p.Boolean(),
                        CanViewDailyReports = p.Boolean(),
                        CanViewDailySalesChart = p.Boolean(),
                        CanViewDailyConsumption = p.Boolean(),
                        CanViewSmsSubscribers = p.Boolean(),
                        CanAddNewSmsSubscriber = p.Boolean(),
                        CanViewSmsSettingsForm = p.Boolean(),
                        CanSendSmsToCustomers = p.Boolean(),
                        CanAddNewRawCategory = p.Boolean(),
                        CanViewRawCategories = p.Boolean(),
                        CanAddRawUnit = p.Boolean(),
                        CanViewRawUnits = p.Boolean(),
                        CanAddNewSupplier = p.Boolean(),
                        CanViewSuppliers = p.Boolean(),
                        CanAddNewRawItem = p.Boolean(),
                        CanViewRawItems = p.Boolean(),
                        CanAddItemInStock = p.Boolean(),
                        CanViewItemAddedStockHistory = p.Boolean(),
                        CanConsumeRawItem = p.Boolean(),
                        CanViewConsumptionRawItemList = p.Boolean(),
                        CanIssueRawItem = p.Boolean(),
                        CanViewIssueRawItemRecords = p.Boolean(),
                        CanViewPaymentRecords = p.Boolean(),
                        CanAddPackings = p.Boolean(),
                        CanViewPackings = p.Boolean(),
                        CanAddAssetCompany = p.Boolean(),
                        CanViewAssetCompanies = p.Boolean(),
                        CanAddAssetCategory = p.Boolean(),
                        CanViewAssetCategories = p.Boolean(),
                        CanAddAssetItem = p.Boolean(),
                        CanViewAssetItem = p.Boolean(),
                        CanViewAssetItemReport = p.Boolean(),
                        CanWastageAssetItem = p.Boolean(),
                        Misc1 = p.Boolean(),
                        Misc2 = p.Boolean(),
                        Misc3 = p.Boolean(),
                        Misc4 = p.Boolean(),
                        Misc5 = p.Boolean(),
                        Misc6 = p.Boolean(),
                        Misc7 = p.Boolean(),
                        Misc8 = p.Boolean(),
                        Misc9 = p.Boolean(),
                        Misc10 = p.Boolean(),
                        Misc11 = p.Boolean(),
                    },
                body:
                    @"UPDATE [dbo].[AppUserOptions]
                      SET [CanAddNewCustomer] = @CanAddNewCustomer, [CanViewTopCustomers] = @CanViewTopCustomers, [CanViewCustomerList] = @CanViewCustomerList, [CanAddNewAppUser] = @CanAddNewAppUser, [CanViewAppUsers] = @CanViewAppUsers, [CanAddNewDeliveryBoy] = @CanAddNewDeliveryBoy, [CanViewDeliveryBoyList] = @CanViewDeliveryBoyList, [CanViewDeliveryBoyPerformance] = @CanViewDeliveryBoyPerformance, [CanAddNewEmployee] = @CanAddNewEmployee, [CanViewEmployeeList] = @CanViewEmployeeList, [CanUseAttendanceInForm] = @CanUseAttendanceInForm, [CanUseAttendanceOutForm] = @CanUseAttendanceOutForm, [CanViewAttendanceList] = @CanViewAttendanceList, [CanAddNewProduct] = @CanAddNewProduct, [CanViewProductList] = @CanViewProductList, [CanAddNewProductCategory] = @CanAddNewProductCategory, [CanViewProductCategories] = @CanViewProductCategories, [CanUseShopInfoForm] = @CanUseShopInfoForm, [CanWastageItem] = @CanWastageItem, [CanViewWastageItems] = @CanViewWastageItems, [CanPlaceNewOrder] = @CanPlaceNewOrder, [CanViewOrdersList] = @CanViewOrdersList, [CanReprintAnOrder] = @CanReprintAnOrder, [CanCanelAnOrder] = @CanCanelAnOrder, [CanViewHomeDeliveryOrders] = @CanViewHomeDeliveryOrders, [CanUpdateHomeDeliveryOrder] = @CanUpdateHomeDeliveryOrder, [CanAddNewDeal] = @CanAddNewDeal, [CanViewDealList] = @CanViewDealList, [CanAddNewServiceTable] = @CanAddNewServiceTable, [CanViewTableServiceList] = @CanViewTableServiceList, [CanManageTableServices] = @CanManageTableServices, [CanViewCancelOrders] = @CanViewCancelOrders, [CanViewProfitChart] = @CanViewProfitChart, [CanViewStockDetails] = @CanViewStockDetails, [CanAddDailyExpense] = @CanAddDailyExpense, [CanViewExpenses] = @CanViewExpenses, [CanViewDailyOrdersChart] = @CanViewDailyOrdersChart, [CanViewDailyReports] = @CanViewDailyReports, [CanViewDailySalesChart] = @CanViewDailySalesChart, [CanViewDailyConsumption] = @CanViewDailyConsumption, [CanViewSmsSubscribers] = @CanViewSmsSubscribers, [CanAddNewSmsSubscriber] = @CanAddNewSmsSubscriber, [CanViewSmsSettingsForm] = @CanViewSmsSettingsForm, [CanSendSmsToCustomers] = @CanSendSmsToCustomers, [CanAddNewRawCategory] = @CanAddNewRawCategory, [CanViewRawCategories] = @CanViewRawCategories, [CanAddRawUnit] = @CanAddRawUnit, [CanViewRawUnits] = @CanViewRawUnits, [CanAddNewSupplier] = @CanAddNewSupplier, [CanViewSuppliers] = @CanViewSuppliers, [CanAddNewRawItem] = @CanAddNewRawItem, [CanViewRawItems] = @CanViewRawItems, [CanAddItemInStock] = @CanAddItemInStock, [CanViewItemAddedStockHistory] = @CanViewItemAddedStockHistory, [CanConsumeRawItem] = @CanConsumeRawItem, [CanViewConsumptionRawItemList] = @CanViewConsumptionRawItemList, [CanIssueRawItem] = @CanIssueRawItem, [CanViewIssueRawItemRecords] = @CanViewIssueRawItemRecords, [CanViewPaymentRecords] = @CanViewPaymentRecords, [CanAddPackings] = @CanAddPackings, [CanViewPackings] = @CanViewPackings, [CanAddAssetCompany] = @CanAddAssetCompany, [CanViewAssetCompanies] = @CanViewAssetCompanies, [CanAddAssetCategory] = @CanAddAssetCategory, [CanViewAssetCategories] = @CanViewAssetCategories, [CanAddAssetItem] = @CanAddAssetItem, [CanViewAssetItem] = @CanViewAssetItem, [CanViewAssetItemReport] = @CanViewAssetItemReport, [CanWastageAssetItem] = @CanWastageAssetItem, [Misc1] = @Misc1, [Misc2] = @Misc2, [Misc3] = @Misc3, [Misc4] = @Misc4, [Misc5] = @Misc5, [Misc6] = @Misc6, [Misc7] = @Misc7, [Misc8] = @Misc8, [Misc9] = @Misc9, [Misc10] = @Misc10, [Misc11] = @Misc11
                      WHERE ([AppUserId] = @AppUserId)"
            );
            
            CreateStoredProcedure(
                "dbo.AppUserOptions_Delete",
                p => new
                    {
                        AppUserId = p.String(maxLength: 128),
                    },
                body:
                    @"DELETE [dbo].[AppUserOptions]
                      WHERE ([AppUserId] = @AppUserId)"
            );
            
            CreateStoredProcedure(
                "dbo.RawBrokerShareType_Insert",
                p => new
                    {
                        Title = p.String(),
                    },
                body:
                    @"INSERT [dbo].[RawBrokerShareTypes]([Title])
                      VALUES (@Title)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[RawBrokerShareTypes]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[RawBrokerShareTypes] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.RawBrokerShareType_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[RawBrokerShareTypes]
                      SET [Title] = @Title
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RawBrokerShareType_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[RawBrokerShareTypes]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.AccruedExpenseItem_Insert",
                p => new
                    {
                        Title = p.String(),
                        DebitAccountId = p.String(maxLength: 128),
                        CreditAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[AccruedExpenseItems]([Title], [DebitAccountId], [CreditAccountId])
                      VALUES (@Title, @DebitAccountId, @CreditAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[AccruedExpenseItems]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[AccruedExpenseItems] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.AccruedExpenseItem_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        DebitAccountId = p.String(maxLength: 128),
                        CreditAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[AccruedExpenseItems]
                      SET [Title] = @Title, [DebitAccountId] = @DebitAccountId, [CreditAccountId] = @CreditAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.AccruedExpenseItem_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[AccruedExpenseItems]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.AdvanceItemRecord_Insert",
                p => new
                    {
                        Qty = p.Decimal(precision: 16, scale: 4),
                        Dated = p.DateTime(),
                        Remarks = p.String(),
                        RepItemId = p.Int(),
                        RepPlaceId = p.Int(),
                        Type = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[AdvanceItemRecords]([Qty], [Dated], [Remarks], [RepItemId], [RepPlaceId], [Type])
                      VALUES (@Qty, @Dated, @Remarks, @RepItemId, @RepPlaceId, @Type)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[AdvanceItemRecords]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[AdvanceItemRecords] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.AdvanceItemRecord_Update",
                p => new
                    {
                        Id = p.Int(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        Dated = p.DateTime(),
                        Remarks = p.String(),
                        RepItemId = p.Int(),
                        RepPlaceId = p.Int(),
                        Type = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[AdvanceItemRecords]
                      SET [Qty] = @Qty, [Dated] = @Dated, [Remarks] = @Remarks, [RepItemId] = @RepItemId, [RepPlaceId] = @RepPlaceId, [Type] = @Type
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.AdvanceItemRecord_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[AdvanceItemRecords]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RepItem_Insert",
                p => new
                    {
                        Name = p.String(),
                        NameUrdu = p.String(),
                        SKU = p.Decimal(precision: 16, scale: 4),
                        USCount = p.Decimal(precision: 16, scale: 4),
                        SACount = p.Decimal(precision: 16, scale: 4),
                        UR = p.Decimal(precision: 16, scale: 4),
                        Adv = p.Decimal(precision: 16, scale: 4),
                        UnitValue = p.Decimal(precision: 16, scale: 4),
                        TotalValue = p.Decimal(precision: 16, scale: 4),
                        LocationId = p.Int(),
                        Weight = p.Decimal(precision: 16, scale: 4),
                        ItemCategoryId = p.Int(),
                        StoreLocationId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[RepItems]([Name], [NameUrdu], [SKU], [USCount], [SACount], [UR], [Adv], [UnitValue], [TotalValue], [LocationId], [Weight], [ItemCategoryId], [StoreLocationId])
                      VALUES (@Name, @NameUrdu, @SKU, @USCount, @SACount, @UR, @Adv, @UnitValue, @TotalValue, @LocationId, @Weight, @ItemCategoryId, @StoreLocationId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[RepItems]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[RepItems] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.RepItem_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        SKU = p.Decimal(precision: 16, scale: 4),
                        USCount = p.Decimal(precision: 16, scale: 4),
                        SACount = p.Decimal(precision: 16, scale: 4),
                        UR = p.Decimal(precision: 16, scale: 4),
                        Adv = p.Decimal(precision: 16, scale: 4),
                        UnitValue = p.Decimal(precision: 16, scale: 4),
                        TotalValue = p.Decimal(precision: 16, scale: 4),
                        LocationId = p.Int(),
                        Weight = p.Decimal(precision: 16, scale: 4),
                        ItemCategoryId = p.Int(),
                        StoreLocationId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[RepItems]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu, [SKU] = @SKU, [USCount] = @USCount, [SACount] = @SACount, [UR] = @UR, [Adv] = @Adv, [UnitValue] = @UnitValue, [TotalValue] = @TotalValue, [LocationId] = @LocationId, [Weight] = @Weight, [ItemCategoryId] = @ItemCategoryId, [StoreLocationId] = @StoreLocationId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RepItem_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[RepItems]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ItemCategory_Insert",
                p => new
                    {
                        Title = p.String(),
                        TitleUrdu = p.String(),
                    },
                body:
                    @"INSERT [dbo].[ItemCategories]([Title], [TitleUrdu])
                      VALUES (@Title, @TitleUrdu)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ItemCategories]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ItemCategories] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ItemCategory_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        TitleUrdu = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[ItemCategories]
                      SET [Title] = @Title, [TitleUrdu] = @TitleUrdu
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ItemCategory_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ItemCategories]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RepItemDamageRecord_Insert",
                p => new
                    {
                        RepItemId = p.Int(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        Dated = p.DateTime(),
                        Remarks = p.String(),
                    },
                body:
                    @"INSERT [dbo].[RepItemDamageRecords]([RepItemId], [Qty], [Dated], [Remarks])
                      VALUES (@RepItemId, @Qty, @Dated, @Remarks)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[RepItemDamageRecords]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[RepItemDamageRecords] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.RepItemDamageRecord_Update",
                p => new
                    {
                        Id = p.Int(),
                        RepItemId = p.Int(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        Dated = p.DateTime(),
                        Remarks = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[RepItemDamageRecords]
                      SET [RepItemId] = @RepItemId, [Qty] = @Qty, [Dated] = @Dated, [Remarks] = @Remarks
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RepItemDamageRecord_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[RepItemDamageRecords]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ItemPurchaseEntry_Insert",
                p => new
                    {
                        Qty = p.Decimal(precision: 16, scale: 4),
                        TotalPrice = p.Decimal(precision: 16, scale: 4),
                        UnitPrice = p.Decimal(precision: 16, scale: 4),
                        PurchaseInvoiceRecordId = p.Int(),
                        RepItemId = p.Int(),
                        Remarks = p.String(),
                    },
                body:
                    @"INSERT [dbo].[ItemPurchaseEntries]([Qty], [TotalPrice], [UnitPrice], [PurchaseInvoiceRecordId], [RepItemId], [Remarks])
                      VALUES (@Qty, @TotalPrice, @UnitPrice, @PurchaseInvoiceRecordId, @RepItemId, @Remarks)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ItemPurchaseEntries]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ItemPurchaseEntries] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ItemPurchaseEntry_Update",
                p => new
                    {
                        Id = p.Int(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        TotalPrice = p.Decimal(precision: 16, scale: 4),
                        UnitPrice = p.Decimal(precision: 16, scale: 4),
                        PurchaseInvoiceRecordId = p.Int(),
                        RepItemId = p.Int(),
                        Remarks = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[ItemPurchaseEntries]
                      SET [Qty] = @Qty, [TotalPrice] = @TotalPrice, [UnitPrice] = @UnitPrice, [PurchaseInvoiceRecordId] = @PurchaseInvoiceRecordId, [RepItemId] = @RepItemId, [Remarks] = @Remarks
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ItemPurchaseEntry_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ItemPurchaseEntries]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.PurchaseInvoiceRecord_Insert",
                p => new
                    {
                        BillId = p.String(),
                        TotalItems = p.Decimal(precision: 16, scale: 4),
                        TotalPrice = p.Decimal(precision: 16, scale: 4),
                        PurchaseDate = p.DateTime(),
                        InDate = p.DateTime(),
                        ItemSupplierId = p.Int(),
                        Unum = p.String(),
                        Remarks = p.String(),
                        DaybookId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[PurchaseInvoiceRecords]([BillId], [TotalItems], [TotalPrice], [PurchaseDate], [InDate], [ItemSupplierId], [Unum], [Remarks], [DaybookId])
                      VALUES (@BillId, @TotalItems, @TotalPrice, @PurchaseDate, @InDate, @ItemSupplierId, @Unum, @Remarks, @DaybookId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[PurchaseInvoiceRecords]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[PurchaseInvoiceRecords] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.PurchaseInvoiceRecord_Update",
                p => new
                    {
                        Id = p.Int(),
                        BillId = p.String(),
                        TotalItems = p.Decimal(precision: 16, scale: 4),
                        TotalPrice = p.Decimal(precision: 16, scale: 4),
                        PurchaseDate = p.DateTime(),
                        InDate = p.DateTime(),
                        ItemSupplierId = p.Int(),
                        Unum = p.String(),
                        Remarks = p.String(),
                        DaybookId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[PurchaseInvoiceRecords]
                      SET [BillId] = @BillId, [TotalItems] = @TotalItems, [TotalPrice] = @TotalPrice, [PurchaseDate] = @PurchaseDate, [InDate] = @InDate, [ItemSupplierId] = @ItemSupplierId, [Unum] = @Unum, [Remarks] = @Remarks, [DaybookId] = @DaybookId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.PurchaseInvoiceRecord_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[PurchaseInvoiceRecords]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ItemSupplier_Insert",
                p => new
                    {
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        AddressUrdu = p.String(),
                        Phone = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[ItemSuppliers]([Name], [NameUrdu], [Address], [AddressUrdu], [Phone], [GeneralAccountId])
                      VALUES (@Name, @NameUrdu, @Address, @AddressUrdu, @Phone, @GeneralAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ItemSuppliers]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ItemSuppliers] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ItemSupplier_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        AddressUrdu = p.String(),
                        Phone = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[ItemSuppliers]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu, [Address] = @Address, [AddressUrdu] = @AddressUrdu, [Phone] = @Phone, [GeneralAccountId] = @GeneralAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ItemSupplier_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ItemSuppliers]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Location_Insert",
                p => new
                    {
                        Name = p.String(),
                        NameUrdu = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Locations]([Name], [NameUrdu])
                      VALUES (@Name, @NameUrdu)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Locations]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Locations] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Location_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Locations]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Location_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Locations]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RepEntry_Insert",
                p => new
                    {
                        RepItemId = p.Int(),
                        DispatchQty = p.Decimal(precision: 16, scale: 4),
                        ReceivedQty = p.Decimal(precision: 16, scale: 4),
                        Remarks = p.String(),
                        RepairDispatchRecordId = p.Int(),
                        RepairReceiveRecordId = p.Int(),
                        Direction = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[RepEntries]([RepItemId], [DispatchQty], [ReceivedQty], [Remarks], [RepairDispatchRecordId], [RepairReceiveRecordId], [Direction])
                      VALUES (@RepItemId, @DispatchQty, @ReceivedQty, @Remarks, @RepairDispatchRecordId, @RepairReceiveRecordId, @Direction)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[RepEntries]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[RepEntries] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.RepEntry_Update",
                p => new
                    {
                        Id = p.Int(),
                        RepItemId = p.Int(),
                        DispatchQty = p.Decimal(precision: 16, scale: 4),
                        ReceivedQty = p.Decimal(precision: 16, scale: 4),
                        Remarks = p.String(),
                        RepairDispatchRecordId = p.Int(),
                        RepairReceiveRecordId = p.Int(),
                        Direction = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[RepEntries]
                      SET [RepItemId] = @RepItemId, [DispatchQty] = @DispatchQty, [ReceivedQty] = @ReceivedQty, [Remarks] = @Remarks, [RepairDispatchRecordId] = @RepairDispatchRecordId, [RepairReceiveRecordId] = @RepairReceiveRecordId, [Direction] = @Direction
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RepEntry_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[RepEntries]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RepairDispatchRecord_Insert",
                p => new
                    {
                        TotalItems = p.Decimal(precision: 16, scale: 4),
                        BillNo = p.String(),
                        Date = p.DateTime(),
                        RepPlaceId = p.Int(),
                        TOPerson = p.String(),
                        ReceivedItems = p.Decimal(precision: 16, scale: 4),
                        RemainingItems = p.Decimal(precision: 16, scale: 4),
                        BillPaid = p.Decimal(precision: 16, scale: 4),
                        Unum = p.String(),
                        Status = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[RepairDispatchRecords]([TotalItems], [BillNo], [Date], [RepPlaceId], [TOPerson], [ReceivedItems], [RemainingItems], [BillPaid], [Unum], [Status])
                      VALUES (@TotalItems, @BillNo, @Date, @RepPlaceId, @TOPerson, @ReceivedItems, @RemainingItems, @BillPaid, @Unum, @Status)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[RepairDispatchRecords]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[RepairDispatchRecords] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.RepairDispatchRecord_Update",
                p => new
                    {
                        Id = p.Int(),
                        TotalItems = p.Decimal(precision: 16, scale: 4),
                        BillNo = p.String(),
                        Date = p.DateTime(),
                        RepPlaceId = p.Int(),
                        TOPerson = p.String(),
                        ReceivedItems = p.Decimal(precision: 16, scale: 4),
                        RemainingItems = p.Decimal(precision: 16, scale: 4),
                        BillPaid = p.Decimal(precision: 16, scale: 4),
                        Unum = p.String(),
                        Status = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[RepairDispatchRecords]
                      SET [TotalItems] = @TotalItems, [BillNo] = @BillNo, [Date] = @Date, [RepPlaceId] = @RepPlaceId, [TOPerson] = @TOPerson, [ReceivedItems] = @ReceivedItems, [RemainingItems] = @RemainingItems, [BillPaid] = @BillPaid, [Unum] = @Unum, [Status] = @Status
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RepairDispatchRecord_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[RepairDispatchRecords]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RepPlace_Insert",
                p => new
                    {
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        AddressUrdu = p.String(),
                        PhoneNo = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[RepPlaces]([Name], [NameUrdu], [Address], [AddressUrdu], [PhoneNo], [GeneralAccountId])
                      VALUES (@Name, @NameUrdu, @Address, @AddressUrdu, @PhoneNo, @GeneralAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[RepPlaces]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[RepPlaces] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.RepPlace_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Address = p.String(),
                        AddressUrdu = p.String(),
                        PhoneNo = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[RepPlaces]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu, [Address] = @Address, [AddressUrdu] = @AddressUrdu, [PhoneNo] = @PhoneNo, [GeneralAccountId] = @GeneralAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RepPlace_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[RepPlaces]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RepairReceiveRecord_Insert",
                p => new
                    {
                        BillAmount = p.Decimal(precision: 16, scale: 4),
                        BillId = p.String(),
                        ReceivedDate = p.DateTime(),
                        InDate = p.DateTime(),
                        DispatchId = p.Int(),
                        Unum = p.String(),
                        RepPlaceId = p.Int(),
                        ReceivingPerson = p.String(),
                        QtyReceived = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[RepairReceiveRecords]([BillAmount], [BillId], [ReceivedDate], [InDate], [DispatchId], [Unum], [RepPlaceId], [ReceivingPerson], [QtyReceived])
                      VALUES (@BillAmount, @BillId, @ReceivedDate, @InDate, @DispatchId, @Unum, @RepPlaceId, @ReceivingPerson, @QtyReceived)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[RepairReceiveRecords]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[RepairReceiveRecords] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.RepairReceiveRecord_Update",
                p => new
                    {
                        Id = p.Int(),
                        BillAmount = p.Decimal(precision: 16, scale: 4),
                        BillId = p.String(),
                        ReceivedDate = p.DateTime(),
                        InDate = p.DateTime(),
                        DispatchId = p.Int(),
                        Unum = p.String(),
                        RepPlaceId = p.Int(),
                        ReceivingPerson = p.String(),
                        QtyReceived = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[RepairReceiveRecords]
                      SET [BillAmount] = @BillAmount, [BillId] = @BillId, [ReceivedDate] = @ReceivedDate, [InDate] = @InDate, [DispatchId] = @DispatchId, [Unum] = @Unum, [RepPlaceId] = @RepPlaceId, [ReceivingPerson] = @ReceivingPerson, [QtyReceived] = @QtyReceived
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RepairReceiveRecord_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[RepairReceiveRecords]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RepItemConsumptionRecord_Insert",
                p => new
                    {
                        RepItemId = p.Int(),
                        QtyConsumed = p.Decimal(precision: 16, scale: 4),
                        Price = p.Decimal(precision: 16, scale: 4),
                        Dated = p.DateTime(),
                        Remarks = p.String(),
                        DaybookId = p.Int(),
                        AccountId = p.String(),
                    },
                body:
                    @"INSERT [dbo].[RepItemConsumptionRecords]([RepItemId], [QtyConsumed], [Price], [Dated], [Remarks], [DaybookId], [AccountId])
                      VALUES (@RepItemId, @QtyConsumed, @Price, @Dated, @Remarks, @DaybookId, @AccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[RepItemConsumptionRecords]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[RepItemConsumptionRecords] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.RepItemConsumptionRecord_Update",
                p => new
                    {
                        Id = p.Int(),
                        RepItemId = p.Int(),
                        QtyConsumed = p.Decimal(precision: 16, scale: 4),
                        Price = p.Decimal(precision: 16, scale: 4),
                        Dated = p.DateTime(),
                        Remarks = p.String(),
                        DaybookId = p.Int(),
                        AccountId = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[RepItemConsumptionRecords]
                      SET [RepItemId] = @RepItemId, [QtyConsumed] = @QtyConsumed, [Price] = @Price, [Dated] = @Dated, [Remarks] = @Remarks, [DaybookId] = @DaybookId, [AccountId] = @AccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RepItemConsumptionRecord_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[RepItemConsumptionRecords]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RepItemPreAddRecord_Insert",
                p => new
                    {
                        RepItemId = p.Int(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        Dated = p.DateTime(),
                        Remarks = p.String(),
                    },
                body:
                    @"INSERT [dbo].[RepItemPreAddRecords]([RepItemId], [Qty], [Dated], [Remarks])
                      VALUES (@RepItemId, @Qty, @Dated, @Remarks)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[RepItemPreAddRecords]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[RepItemPreAddRecords] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.RepItemPreAddRecord_Update",
                p => new
                    {
                        Id = p.Int(),
                        RepItemId = p.Int(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        Dated = p.DateTime(),
                        Remarks = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[RepItemPreAddRecords]
                      SET [RepItemId] = @RepItemId, [Qty] = @Qty, [Dated] = @Dated, [Remarks] = @Remarks
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RepItemPreAddRecord_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[RepItemPreAddRecords]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.StoreLocation_Insert",
                p => new
                    {
                        LocationName = p.String(),
                        LocationNameUrdu = p.String(),
                    },
                body:
                    @"INSERT [dbo].[StoreLocations]([LocationName], [LocationNameUrdu])
                      VALUES (@LocationName, @LocationNameUrdu)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[StoreLocations]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[StoreLocations] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.StoreLocation_Update",
                p => new
                    {
                        Id = p.Int(),
                        LocationName = p.String(),
                        LocationNameUrdu = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[StoreLocations]
                      SET [LocationName] = @LocationName, [LocationNameUrdu] = @LocationNameUrdu
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.StoreLocation_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[StoreLocations]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Rahzam_Insert",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        ItheyRakh = p.String(),
                        ChalJanDey = p.String(),
                        HunAramEe = p.String(),
                        ChalBasKerYar = p.String(),
                        ChangaPhir = p.String(),
                        RabRakha = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Rahzams]([Id], [ItheyRakh], [ChalJanDey], [HunAramEe], [ChalBasKerYar], [ChangaPhir], [RabRakha])
                      VALUES (@Id, @ItheyRakh, @ChalJanDey, @HunAramEe, @ChalBasKerYar, @ChangaPhir, @RabRakha)"
            );
            
            CreateStoredProcedure(
                "dbo.Rahzam_Update",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        ItheyRakh = p.String(),
                        ChalJanDey = p.String(),
                        HunAramEe = p.String(),
                        ChalBasKerYar = p.String(),
                        ChangaPhir = p.String(),
                        RabRakha = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Rahzams]
                      SET [ItheyRakh] = @ItheyRakh, [ChalJanDey] = @ChalJanDey, [HunAramEe] = @HunAramEe, [ChalBasKerYar] = @ChalBasKerYar, [ChangaPhir] = @ChangaPhir, [RabRakha] = @RabRakha
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Rahzam_Delete",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                    },
                body:
                    @"DELETE [dbo].[Rahzams]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.AppMessageState_Insert",
                p => new
                    {
                        ShopMessageId = p.Int(),
                        SmsSubsriberId = p.Int(),
                        State = p.Boolean(),
                        TimeStamp = p.DateTime(),
                    },
                body:
                    @"INSERT [dbo].[AppMessageStates]([ShopMessageId], [SmsSubsriberId], [State], [TimeStamp])
                      VALUES (@ShopMessageId, @SmsSubsriberId, @State, @TimeStamp)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[AppMessageStates]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[AppMessageStates] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.AppMessageState_Update",
                p => new
                    {
                        Id = p.Int(),
                        ShopMessageId = p.Int(),
                        SmsSubsriberId = p.Int(),
                        State = p.Boolean(),
                        TimeStamp = p.DateTime(),
                    },
                body:
                    @"UPDATE [dbo].[AppMessageStates]
                      SET [ShopMessageId] = @ShopMessageId, [SmsSubsriberId] = @SmsSubsriberId, [State] = @State, [TimeStamp] = @TimeStamp
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.AppMessageState_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[AppMessageStates]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ShopMessage_Insert",
                p => new
                    {
                        Message = p.String(),
                        TimeStamp = p.DateTime(),
                    },
                body:
                    @"INSERT [dbo].[ShopMessages]([Message], [TimeStamp])
                      VALUES (@Message, @TimeStamp)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ShopMessages]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ShopMessages] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ShopMessage_Update",
                p => new
                    {
                        Id = p.Int(),
                        Message = p.String(),
                        TimeStamp = p.DateTime(),
                    },
                body:
                    @"UPDATE [dbo].[ShopMessages]
                      SET [Message] = @Message, [TimeStamp] = @TimeStamp
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ShopMessage_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ShopMessages]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.SmsSubscriber_Insert",
                p => new
                    {
                        Name = p.String(),
                        CellNo = p.String(),
                        IsActive = p.Boolean(),
                        ShouldReceiveCloseReportAlert = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[SmsSubscribers]([Name], [CellNo], [IsActive], [ShouldReceiveCloseReportAlert])
                      VALUES (@Name, @CellNo, @IsActive, @ShouldReceiveCloseReportAlert)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[SmsSubscribers]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[SmsSubscribers] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.SmsSubscriber_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        CellNo = p.String(),
                        IsActive = p.Boolean(),
                        ShouldReceiveCloseReportAlert = p.Boolean(),
                    },
                body:
                    @"UPDATE [dbo].[SmsSubscribers]
                      SET [Name] = @Name, [CellNo] = @CellNo, [IsActive] = @IsActive, [ShouldReceiveCloseReportAlert] = @ShouldReceiveCloseReportAlert
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.SmsSubscriber_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[SmsSubscribers]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.AppSettings_Insert",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        PhoneNo = p.String(),
                        Address = p.String(),
                        AddressUrdu = p.String(),
                        MainPrinter = p.String(),
                        ThermalPrinter = p.String(),
                        DaysAlertBeforeReady = p.Int(),
                        Logo = p.Binary(),
                        StartDate = p.DateTime(),
                        EndDate = p.DateTime(),
                        OilCakeBrokerSharePercentage = p.Decimal(precision: 16, scale: 4),
                        AllowToChargeOilCakeBrokerSharePercentage = p.Boolean(),
                        OilBrokerSharePercentage = p.Decimal(precision: 16, scale: 4),
                        AllowToChangeOilBrokerSharePercentage = p.Boolean(),
                        MailBrokerSharePercentage = p.Decimal(precision: 16, scale: 4),
                        AllowToChangeMailBrokerSharePercentage = p.Boolean(),
                        SalesTaxPercentage = p.Decimal(precision: 16, scale: 4),
                        OfferDiscountPercentage = p.Decimal(precision: 16, scale: 4),
                        ServiceCharges = p.Decimal(precision: 16, scale: 4),
                        EnableDiscounts = p.Boolean(),
                        MasterPassword = p.String(),
                        SecurityPassword = p.String(),
                        ApplyRetailOilCakeLaborExpense = p.Boolean(),
                        ApplayWholeSaleOilCakeLaborExpense = p.Boolean(),
                        ApplyCrudeOilLaborExpense = p.Boolean(),
                        ApplyOilDirtLaborExpense = p.Boolean(),
                        ApplyPurchasedItemLaborExpense = p.Boolean(),
                        ApplyBardanaExpenseForRetailOilCake = p.Boolean(),
                        ApplyBardanaExpenseForWholeSaleOilCake = p.Boolean(),
                        bool1 = p.Boolean(),
                        bool2 = p.Boolean(),
                        bool3 = p.Boolean(),
                        PrintFinancialTransactions = p.Boolean(),
                        bool5 = p.Boolean(),
                        string1 = p.String(),
                        string2 = p.String(),
                        string3 = p.String(),
                        string4 = p.String(),
                        string5 = p.String(),
                        decimal1 = p.Decimal(precision: 16, scale: 4),
                        decimal2 = p.Decimal(precision: 16, scale: 4),
                        decimal3 = p.Decimal(precision: 16, scale: 4),
                        decimal4 = p.Decimal(precision: 16, scale: 4),
                        decimal5 = p.Decimal(precision: 16, scale: 4),
                        GatePassCopies = p.Int(),
                        FloorScaleCopies = p.Int(),
                        CustomerCopies = p.Int(),
                        OfficeCopies = p.Int(),
                        int5 = p.Int(),
                        int1 = p.Int(),
                        int2 = p.Int(),
                        dt1 = p.DateTime(),
                        dt2 = p.DateTime(),
                        dt3 = p.DateTime(),
                        dt4 = p.DateTime(),
                        bin1 = p.Binary(),
                        bin2 = p.Binary(),
                    },
                body:
                    @"INSERT [dbo].[AppSettings]([Id], [Name], [NameUrdu], [PhoneNo], [Address], [AddressUrdu], [MainPrinter], [ThermalPrinter], [DaysAlertBeforeReady], [Logo], [StartDate], [EndDate], [OilCakeBrokerSharePercentage], [AllowToChargeOilCakeBrokerSharePercentage], [OilBrokerSharePercentage], [AllowToChangeOilBrokerSharePercentage], [MailBrokerSharePercentage], [AllowToChangeMailBrokerSharePercentage], [SalesTaxPercentage], [OfferDiscountPercentage], [ServiceCharges], [EnableDiscounts], [MasterPassword], [SecurityPassword], [ApplyRetailOilCakeLaborExpense], [ApplayWholeSaleOilCakeLaborExpense], [ApplyCrudeOilLaborExpense], [ApplyOilDirtLaborExpense], [ApplyPurchasedItemLaborExpense], [ApplyBardanaExpenseForRetailOilCake], [ApplyBardanaExpenseForWholeSaleOilCake], [bool1], [bool2], [bool3], [PrintFinancialTransactions], [bool5], [string1], [string2], [string3], [string4], [string5], [decimal1], [decimal2], [decimal3], [decimal4], [decimal5], [GatePassCopies], [FloorScaleCopies], [CustomerCopies], [OfficeCopies], [int5], [int1], [int2], [dt1], [dt2], [dt3], [dt4], [bin1], [bin2])
                      VALUES (@Id, @Name, @NameUrdu, @PhoneNo, @Address, @AddressUrdu, @MainPrinter, @ThermalPrinter, @DaysAlertBeforeReady, @Logo, @StartDate, @EndDate, @OilCakeBrokerSharePercentage, @AllowToChargeOilCakeBrokerSharePercentage, @OilBrokerSharePercentage, @AllowToChangeOilBrokerSharePercentage, @MailBrokerSharePercentage, @AllowToChangeMailBrokerSharePercentage, @SalesTaxPercentage, @OfferDiscountPercentage, @ServiceCharges, @EnableDiscounts, @MasterPassword, @SecurityPassword, @ApplyRetailOilCakeLaborExpense, @ApplayWholeSaleOilCakeLaborExpense, @ApplyCrudeOilLaborExpense, @ApplyOilDirtLaborExpense, @ApplyPurchasedItemLaborExpense, @ApplyBardanaExpenseForRetailOilCake, @ApplyBardanaExpenseForWholeSaleOilCake, @bool1, @bool2, @bool3, @PrintFinancialTransactions, @bool5, @string1, @string2, @string3, @string4, @string5, @decimal1, @decimal2, @decimal3, @decimal4, @decimal5, @GatePassCopies, @FloorScaleCopies, @CustomerCopies, @OfficeCopies, @int5, @int1, @int2, @dt1, @dt2, @dt3, @dt4, @bin1, @bin2)"
            );
            
            CreateStoredProcedure(
                "dbo.AppSettings_Update",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        PhoneNo = p.String(),
                        Address = p.String(),
                        AddressUrdu = p.String(),
                        MainPrinter = p.String(),
                        ThermalPrinter = p.String(),
                        DaysAlertBeforeReady = p.Int(),
                        Logo = p.Binary(),
                        StartDate = p.DateTime(),
                        EndDate = p.DateTime(),
                        OilCakeBrokerSharePercentage = p.Decimal(precision: 16, scale: 4),
                        AllowToChargeOilCakeBrokerSharePercentage = p.Boolean(),
                        OilBrokerSharePercentage = p.Decimal(precision: 16, scale: 4),
                        AllowToChangeOilBrokerSharePercentage = p.Boolean(),
                        MailBrokerSharePercentage = p.Decimal(precision: 16, scale: 4),
                        AllowToChangeMailBrokerSharePercentage = p.Boolean(),
                        SalesTaxPercentage = p.Decimal(precision: 16, scale: 4),
                        OfferDiscountPercentage = p.Decimal(precision: 16, scale: 4),
                        ServiceCharges = p.Decimal(precision: 16, scale: 4),
                        EnableDiscounts = p.Boolean(),
                        MasterPassword = p.String(),
                        SecurityPassword = p.String(),
                        ApplyRetailOilCakeLaborExpense = p.Boolean(),
                        ApplayWholeSaleOilCakeLaborExpense = p.Boolean(),
                        ApplyCrudeOilLaborExpense = p.Boolean(),
                        ApplyOilDirtLaborExpense = p.Boolean(),
                        ApplyPurchasedItemLaborExpense = p.Boolean(),
                        ApplyBardanaExpenseForRetailOilCake = p.Boolean(),
                        ApplyBardanaExpenseForWholeSaleOilCake = p.Boolean(),
                        bool1 = p.Boolean(),
                        bool2 = p.Boolean(),
                        bool3 = p.Boolean(),
                        PrintFinancialTransactions = p.Boolean(),
                        bool5 = p.Boolean(),
                        string1 = p.String(),
                        string2 = p.String(),
                        string3 = p.String(),
                        string4 = p.String(),
                        string5 = p.String(),
                        decimal1 = p.Decimal(precision: 16, scale: 4),
                        decimal2 = p.Decimal(precision: 16, scale: 4),
                        decimal3 = p.Decimal(precision: 16, scale: 4),
                        decimal4 = p.Decimal(precision: 16, scale: 4),
                        decimal5 = p.Decimal(precision: 16, scale: 4),
                        GatePassCopies = p.Int(),
                        FloorScaleCopies = p.Int(),
                        CustomerCopies = p.Int(),
                        OfficeCopies = p.Int(),
                        int5 = p.Int(),
                        int1 = p.Int(),
                        int2 = p.Int(),
                        dt1 = p.DateTime(),
                        dt2 = p.DateTime(),
                        dt3 = p.DateTime(),
                        dt4 = p.DateTime(),
                        bin1 = p.Binary(),
                        bin2 = p.Binary(),
                    },
                body:
                    @"UPDATE [dbo].[AppSettings]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu, [PhoneNo] = @PhoneNo, [Address] = @Address, [AddressUrdu] = @AddressUrdu, [MainPrinter] = @MainPrinter, [ThermalPrinter] = @ThermalPrinter, [DaysAlertBeforeReady] = @DaysAlertBeforeReady, [Logo] = @Logo, [StartDate] = @StartDate, [EndDate] = @EndDate, [OilCakeBrokerSharePercentage] = @OilCakeBrokerSharePercentage, [AllowToChargeOilCakeBrokerSharePercentage] = @AllowToChargeOilCakeBrokerSharePercentage, [OilBrokerSharePercentage] = @OilBrokerSharePercentage, [AllowToChangeOilBrokerSharePercentage] = @AllowToChangeOilBrokerSharePercentage, [MailBrokerSharePercentage] = @MailBrokerSharePercentage, [AllowToChangeMailBrokerSharePercentage] = @AllowToChangeMailBrokerSharePercentage, [SalesTaxPercentage] = @SalesTaxPercentage, [OfferDiscountPercentage] = @OfferDiscountPercentage, [ServiceCharges] = @ServiceCharges, [EnableDiscounts] = @EnableDiscounts, [MasterPassword] = @MasterPassword, [SecurityPassword] = @SecurityPassword, [ApplyRetailOilCakeLaborExpense] = @ApplyRetailOilCakeLaborExpense, [ApplayWholeSaleOilCakeLaborExpense] = @ApplayWholeSaleOilCakeLaborExpense, [ApplyCrudeOilLaborExpense] = @ApplyCrudeOilLaborExpense, [ApplyOilDirtLaborExpense] = @ApplyOilDirtLaborExpense, [ApplyPurchasedItemLaborExpense] = @ApplyPurchasedItemLaborExpense, [ApplyBardanaExpenseForRetailOilCake] = @ApplyBardanaExpenseForRetailOilCake, [ApplyBardanaExpenseForWholeSaleOilCake] = @ApplyBardanaExpenseForWholeSaleOilCake, [bool1] = @bool1, [bool2] = @bool2, [bool3] = @bool3, [PrintFinancialTransactions] = @PrintFinancialTransactions, [bool5] = @bool5, [string1] = @string1, [string2] = @string2, [string3] = @string3, [string4] = @string4, [string5] = @string5, [decimal1] = @decimal1, [decimal2] = @decimal2, [decimal3] = @decimal3, [decimal4] = @decimal4, [decimal5] = @decimal5, [GatePassCopies] = @GatePassCopies, [FloorScaleCopies] = @FloorScaleCopies, [CustomerCopies] = @CustomerCopies, [OfficeCopies] = @OfficeCopies, [int5] = @int5, [int1] = @int1, [int2] = @int2, [dt1] = @dt1, [dt2] = @dt2, [dt3] = @dt3, [dt4] = @dt4, [bin1] = @bin1, [bin2] = @bin2
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.AppSettings_Delete",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                    },
                body:
                    @"DELETE [dbo].[AppSettings]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.DealSegment_Insert",
                p => new
                    {
                        TradeUnits = p.Decimal(precision: 16, scale: 4),
                        PackingUnits = p.Decimal(precision: 16, scale: 4),
                        Price = p.Decimal(precision: 16, scale: 4),
                        TradeUnitTitle = p.String(),
                        PackingUnitTitle = p.String(),
                        ReadyDate = p.DateTime(),
                        ArrivalDate = p.DateTime(),
                        IsArrived = p.Boolean(),
                        OmeDealId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[DealSegments]([TradeUnits], [PackingUnits], [Price], [TradeUnitTitle], [PackingUnitTitle], [ReadyDate], [ArrivalDate], [IsArrived], [OmeDealId])
                      VALUES (@TradeUnits, @PackingUnits, @Price, @TradeUnitTitle, @PackingUnitTitle, @ReadyDate, @ArrivalDate, @IsArrived, @OmeDealId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[DealSegments]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[DealSegments] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.DealSegment_Update",
                p => new
                    {
                        Id = p.Int(),
                        TradeUnits = p.Decimal(precision: 16, scale: 4),
                        PackingUnits = p.Decimal(precision: 16, scale: 4),
                        Price = p.Decimal(precision: 16, scale: 4),
                        TradeUnitTitle = p.String(),
                        PackingUnitTitle = p.String(),
                        ReadyDate = p.DateTime(),
                        ArrivalDate = p.DateTime(),
                        IsArrived = p.Boolean(),
                        OmeDealId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[DealSegments]
                      SET [TradeUnits] = @TradeUnits, [PackingUnits] = @PackingUnits, [Price] = @Price, [TradeUnitTitle] = @TradeUnitTitle, [PackingUnitTitle] = @PackingUnitTitle, [ReadyDate] = @ReadyDate, [ArrivalDate] = @ArrivalDate, [IsArrived] = @IsArrived, [OmeDealId] = @OmeDealId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.DealSegment_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[DealSegments]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.DEEntry_Insert",
                p => new
                    {
                        Dated = p.DateTime(),
                        StartDateTime = p.DateTime(),
                        EndDateTime = p.DateTime(),
                        SaleAmount = p.Decimal(precision: 16, scale: 4),
                        SaleQty = p.Decimal(precision: 16, scale: 4),
                        Description = p.String(),
                    },
                body:
                    @"INSERT [dbo].[DEEntries]([Dated], [StartDateTime], [EndDateTime], [SaleAmount], [SaleQty], [Description])
                      VALUES (@Dated, @StartDateTime, @EndDateTime, @SaleAmount, @SaleQty, @Description)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[DEEntries]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[DEEntries] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.DEEntry_Update",
                p => new
                    {
                        Id = p.Int(),
                        Dated = p.DateTime(),
                        StartDateTime = p.DateTime(),
                        EndDateTime = p.DateTime(),
                        SaleAmount = p.Decimal(precision: 16, scale: 4),
                        SaleQty = p.Decimal(precision: 16, scale: 4),
                        Description = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[DEEntries]
                      SET [Dated] = @Dated, [StartDateTime] = @StartDateTime, [EndDateTime] = @EndDateTime, [SaleAmount] = @SaleAmount, [SaleQty] = @SaleQty, [Description] = @Description
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.DEEntry_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[DEEntries]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.EFCategory_Insert",
                p => new
                    {
                        Title = p.String(),
                    },
                body:
                    @"INSERT [dbo].[EFCategories]([Title])
                      VALUES (@Title)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[EFCategories]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[EFCategories] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.EFCategory_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[EFCategories]
                      SET [Title] = @Title
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.EFCategory_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[EFCategories]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.EFile_Insert",
                p => new
                    {
                        Title = p.String(),
                        DateAdded = p.DateTime(),
                        Description = p.String(),
                        EFCategoryId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[EFiles]([Title], [DateAdded], [Description], [EFCategoryId])
                      VALUES (@Title, @DateAdded, @Description, @EFCategoryId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[EFiles]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[EFiles] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.EFile_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        DateAdded = p.DateTime(),
                        Description = p.String(),
                        EFCategoryId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[EFiles]
                      SET [Title] = @Title, [DateAdded] = @DateAdded, [Description] = @Description, [EFCategoryId] = @EFCategoryId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.EFile_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[EFiles]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.EFileImage_Insert",
                p => new
                    {
                        Title = p.String(),
                        DateAdded = p.DateTime(),
                        MimeType = p.String(),
                        PicData = p.Binary(),
                        EFileId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[EFileImages]([Title], [DateAdded], [MimeType], [PicData], [EFileId])
                      VALUES (@Title, @DateAdded, @MimeType, @PicData, @EFileId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[EFileImages]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[EFileImages] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.EFileImage_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        DateAdded = p.DateTime(),
                        MimeType = p.String(),
                        PicData = p.Binary(),
                        EFileId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[EFileImages]
                      SET [Title] = @Title, [DateAdded] = @DateAdded, [MimeType] = @MimeType, [PicData] = @PicData, [EFileId] = @EFileId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.EFileImage_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[EFileImages]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.EntItemEntry_Insert",
                p => new
                    {
                        EntItemId = p.Int(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        EntPurchaseId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[EntItemEntries]([EntItemId], [Qty], [EntPurchaseId])
                      VALUES (@EntItemId, @Qty, @EntPurchaseId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[EntItemEntries]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[EntItemEntries] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.EntItemEntry_Update",
                p => new
                    {
                        Id = p.Int(),
                        EntItemId = p.Int(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        EntPurchaseId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[EntItemEntries]
                      SET [EntItemId] = @EntItemId, [Qty] = @Qty, [EntPurchaseId] = @EntPurchaseId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.EntItemEntry_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[EntItemEntries]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.EntItem_Insert",
                p => new
                    {
                        Title = p.String(),
                        NameUrdu = p.String(),
                        QtyConsumed = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[EntItems]([Title], [NameUrdu], [QtyConsumed])
                      VALUES (@Title, @NameUrdu, @QtyConsumed)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[EntItems]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[EntItems] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.EntItem_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        NameUrdu = p.String(),
                        QtyConsumed = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[EntItems]
                      SET [Title] = @Title, [NameUrdu] = @NameUrdu, [QtyConsumed] = @QtyConsumed
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.EntItem_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[EntItems]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.EntPurchase_Insert",
                p => new
                    {
                        Qty = p.Decimal(precision: 16, scale: 4),
                        Remarks = p.String(),
                        Dated = p.DateTime(),
                        Unum = p.String(),
                        Operator = p.String(),
                    },
                body:
                    @"INSERT [dbo].[EntPurchases]([Qty], [Remarks], [Dated], [Unum], [Operator])
                      VALUES (@Qty, @Remarks, @Dated, @Unum, @Operator)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[EntPurchases]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[EntPurchases] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.EntPurchase_Update",
                p => new
                    {
                        Id = p.Int(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        Remarks = p.String(),
                        Dated = p.DateTime(),
                        Unum = p.String(),
                        Operator = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[EntPurchases]
                      SET [Qty] = @Qty, [Remarks] = @Remarks, [Dated] = @Dated, [Unum] = @Unum, [Operator] = @Operator
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.EntPurchase_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[EntPurchases]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.EntZeroItemConsumption_Insert",
                p => new
                    {
                        Remarks = p.String(),
                        Dated = p.DateTime(),
                        Operator = p.String(),
                        Amount = p.Decimal(precision: 16, scale: 4),
                        QtyConsumed = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[EntZeroItemConsumptions]([Remarks], [Dated], [Operator], [Amount], [QtyConsumed])
                      VALUES (@Remarks, @Dated, @Operator, @Amount, @QtyConsumed)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[EntZeroItemConsumptions]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[EntZeroItemConsumptions] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.EntZeroItemConsumption_Update",
                p => new
                    {
                        Id = p.Int(),
                        Remarks = p.String(),
                        Dated = p.DateTime(),
                        Operator = p.String(),
                        Amount = p.Decimal(precision: 16, scale: 4),
                        QtyConsumed = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[EntZeroItemConsumptions]
                      SET [Remarks] = @Remarks, [Dated] = @Dated, [Operator] = @Operator, [Amount] = @Amount, [QtyConsumed] = @QtyConsumed
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.EntZeroItemConsumption_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[EntZeroItemConsumptions]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ExpenseItemEntry_Insert",
                p => new
                    {
                        ExpenseItemId = p.Int(),
                        ExpenseAmount = p.Decimal(precision: 16, scale: 4),
                        ExpenseDate = p.DateTime(),
                        Remarks = p.String(),
                        Description = p.String(),
                        ExpenseType = p.Int(),
                        DayBookId = p.Int(),
                        CreditAccountId = p.String(),
                    },
                body:
                    @"INSERT [dbo].[ExpenseItemEntries]([ExpenseItemId], [ExpenseAmount], [ExpenseDate], [Remarks], [Description], [ExpenseType], [DayBookId], [CreditAccountId])
                      VALUES (@ExpenseItemId, @ExpenseAmount, @ExpenseDate, @Remarks, @Description, @ExpenseType, @DayBookId, @CreditAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ExpenseItemEntries]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ExpenseItemEntries] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ExpenseItemEntry_Update",
                p => new
                    {
                        Id = p.Int(),
                        ExpenseItemId = p.Int(),
                        ExpenseAmount = p.Decimal(precision: 16, scale: 4),
                        ExpenseDate = p.DateTime(),
                        Remarks = p.String(),
                        Description = p.String(),
                        ExpenseType = p.Int(),
                        DayBookId = p.Int(),
                        CreditAccountId = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[ExpenseItemEntries]
                      SET [ExpenseItemId] = @ExpenseItemId, [ExpenseAmount] = @ExpenseAmount, [ExpenseDate] = @ExpenseDate, [Remarks] = @Remarks, [Description] = @Description, [ExpenseType] = @ExpenseType, [DayBookId] = @DayBookId, [CreditAccountId] = @CreditAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ExpenseItemEntry_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ExpenseItemEntries]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ExpenseItem_Insert",
                p => new
                    {
                        Title = p.String(),
                        ExpenseType = p.Int(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[ExpenseItems]([Title], [ExpenseType], [GeneralAccountId])
                      VALUES (@Title, @ExpenseType, @GeneralAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ExpenseItems]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ExpenseItems] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ExpenseItem_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        ExpenseType = p.Int(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[ExpenseItems]
                      SET [Title] = @Title, [ExpenseType] = @ExpenseType, [GeneralAccountId] = @GeneralAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ExpenseItem_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ExpenseItems]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.GeneralReceipt_Insert",
                p => new
                    {
                        Description = p.String(),
                        DescriptionUrdu = p.String(),
                        Dated = p.DateTime(),
                        Amount = p.Decimal(precision: 16, scale: 4),
                        Unum = p.String(),
                        Status = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[GeneralReceipts]([Description], [DescriptionUrdu], [Dated], [Amount], [Unum], [Status])
                      VALUES (@Description, @DescriptionUrdu, @Dated, @Amount, @Unum, @Status)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[GeneralReceipts]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[GeneralReceipts] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.GeneralReceipt_Update",
                p => new
                    {
                        Id = p.Int(),
                        Description = p.String(),
                        DescriptionUrdu = p.String(),
                        Dated = p.DateTime(),
                        Amount = p.Decimal(precision: 16, scale: 4),
                        Unum = p.String(),
                        Status = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[GeneralReceipts]
                      SET [Description] = @Description, [DescriptionUrdu] = @DescriptionUrdu, [Dated] = @Dated, [Amount] = @Amount, [Unum] = @Unum, [Status] = @Status
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.GeneralReceipt_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[GeneralReceipts]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ItemPlaceSKU_Insert",
                p => new
                    {
                        RepItemId = p.Int(),
                        RepPlaceId = p.Int(),
                        SKU = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[ItemPlaceSKUs]([RepItemId], [RepPlaceId], [SKU])
                      VALUES (@RepItemId, @RepPlaceId, @SKU)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ItemPlaceSKUs]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ItemPlaceSKUs] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ItemPlaceSKU_Update",
                p => new
                    {
                        Id = p.Int(),
                        RepItemId = p.Int(),
                        RepPlaceId = p.Int(),
                        SKU = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[ItemPlaceSKUs]
                      SET [RepItemId] = @RepItemId, [RepPlaceId] = @RepPlaceId, [SKU] = @SKU
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ItemPlaceSKU_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ItemPlaceSKUs]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.KeyInfo_Insert",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        Key = p.String(),
                        DateAdded = p.DateTime(),
                        IsValid = p.String(),
                        WahJi = p.String(),
                    },
                body:
                    @"INSERT [dbo].[KeyInfoes]([Id], [Key], [DateAdded], [IsValid], [WahJi])
                      VALUES (@Id, @Key, @DateAdded, @IsValid, @WahJi)"
            );
            
            CreateStoredProcedure(
                "dbo.KeyInfo_Update",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        Key = p.String(),
                        DateAdded = p.DateTime(),
                        IsValid = p.String(),
                        WahJi = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[KeyInfoes]
                      SET [Key] = @Key, [DateAdded] = @DateAdded, [IsValid] = @IsValid, [WahJi] = @WahJi
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.KeyInfo_Delete",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                    },
                body:
                    @"DELETE [dbo].[KeyInfoes]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.LongTermAssetItem_Insert",
                p => new
                    {
                        Title = p.String(),
                        InitialPrice = p.Decimal(precision: 16, scale: 4),
                        CurrentPrice = p.Decimal(precision: 16, scale: 4),
                        DateAdded = p.DateTime(),
                        Description = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[LongTermAssetItems]([Title], [InitialPrice], [CurrentPrice], [DateAdded], [Description], [GeneralAccountId])
                      VALUES (@Title, @InitialPrice, @CurrentPrice, @DateAdded, @Description, @GeneralAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[LongTermAssetItems]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[LongTermAssetItems] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.LongTermAssetItem_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        InitialPrice = p.Decimal(precision: 16, scale: 4),
                        CurrentPrice = p.Decimal(precision: 16, scale: 4),
                        DateAdded = p.DateTime(),
                        Description = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[LongTermAssetItems]
                      SET [Title] = @Title, [InitialPrice] = @InitialPrice, [CurrentPrice] = @CurrentPrice, [DateAdded] = @DateAdded, [Description] = @Description, [GeneralAccountId] = @GeneralAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.LongTermAssetItem_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[LongTermAssetItems]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.MiscTransaction_Insert",
                p => new
                    {
                        debitAccountId = p.String(maxLength: 128),
                        creditAccountId = p.String(maxLength: 128),
                        Amount = p.Decimal(precision: 16, scale: 4),
                        Dated = p.DateTime(),
                        Description = p.String(),
                    },
                body:
                    @"INSERT [dbo].[MiscTransactions]([debitAccountId], [creditAccountId], [Amount], [Dated], [Description])
                      VALUES (@debitAccountId, @creditAccountId, @Amount, @Dated, @Description)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[MiscTransactions]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[MiscTransactions] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.MiscTransaction_Update",
                p => new
                    {
                        Id = p.Int(),
                        debitAccountId = p.String(maxLength: 128),
                        creditAccountId = p.String(maxLength: 128),
                        Amount = p.Decimal(precision: 16, scale: 4),
                        Dated = p.DateTime(),
                        Description = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[MiscTransactions]
                      SET [debitAccountId] = @debitAccountId, [creditAccountId] = @creditAccountId, [Amount] = @Amount, [Dated] = @Dated, [Description] = @Description
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.MiscTransaction_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[MiscTransactions]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDealAlarm_Insert",
                p => new
                    {
                        AlarmText = p.String(),
                        AlarmReadyDate = p.DateTime(),
                        GenerateDate = p.DateTime(),
                        EndDate = p.DateTime(),
                        IsActive = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[OilDealAlarms]([AlarmText], [AlarmReadyDate], [GenerateDate], [EndDate], [IsActive])
                      VALUES (@AlarmText, @AlarmReadyDate, @GenerateDate, @EndDate, @IsActive)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[OilDealAlarms]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[OilDealAlarms] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.OilDealAlarm_Update",
                p => new
                    {
                        Id = p.Int(),
                        AlarmText = p.String(),
                        AlarmReadyDate = p.DateTime(),
                        GenerateDate = p.DateTime(),
                        EndDate = p.DateTime(),
                        IsActive = p.Boolean(),
                    },
                body:
                    @"UPDATE [dbo].[OilDealAlarms]
                      SET [AlarmText] = @AlarmText, [AlarmReadyDate] = @AlarmReadyDate, [GenerateDate] = @GenerateDate, [EndDate] = @EndDate, [IsActive] = @IsActive
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDealAlarm_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[OilDealAlarms]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtAlarm_Insert",
                p => new
                    {
                        Description = p.String(),
                        GenerateDate = p.DateTime(),
                        EndDate = p.DateTime(),
                        ActiveDate = p.DateTime(),
                        IsActive = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[OilDirtAlarms]([Description], [GenerateDate], [EndDate], [ActiveDate], [IsActive])
                      VALUES (@Description, @GenerateDate, @EndDate, @ActiveDate, @IsActive)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[OilDirtAlarms]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[OilDirtAlarms] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtAlarm_Update",
                p => new
                    {
                        Id = p.Int(),
                        Description = p.String(),
                        GenerateDate = p.DateTime(),
                        EndDate = p.DateTime(),
                        ActiveDate = p.DateTime(),
                        IsActive = p.Boolean(),
                    },
                body:
                    @"UPDATE [dbo].[OilDirtAlarms]
                      SET [Description] = @Description, [GenerateDate] = @GenerateDate, [EndDate] = @EndDate, [ActiveDate] = @ActiveDate, [IsActive] = @IsActive
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.OilDirtAlarm_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[OilDirtAlarms]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ProductEntryRecord_Insert",
                p => new
                    {
                        ProductId = p.Int(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        UnitPrice = p.Decimal(precision: 16, scale: 4),
                        TotalPrice = p.Decimal(precision: 16, scale: 4),
                        Remarks = p.String(),
                        AddedDate = p.DateTime(),
                    },
                body:
                    @"INSERT [dbo].[ProductEntryRecords]([ProductId], [Qty], [UnitPrice], [TotalPrice], [Remarks], [AddedDate])
                      VALUES (@ProductId, @Qty, @UnitPrice, @TotalPrice, @Remarks, @AddedDate)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ProductEntryRecords]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ProductEntryRecords] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ProductEntryRecord_Update",
                p => new
                    {
                        Id = p.Int(),
                        ProductId = p.Int(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        UnitPrice = p.Decimal(precision: 16, scale: 4),
                        TotalPrice = p.Decimal(precision: 16, scale: 4),
                        Remarks = p.String(),
                        AddedDate = p.DateTime(),
                    },
                body:
                    @"UPDATE [dbo].[ProductEntryRecords]
                      SET [ProductId] = @ProductId, [Qty] = @Qty, [UnitPrice] = @UnitPrice, [TotalPrice] = @TotalPrice, [Remarks] = @Remarks, [AddedDate] = @AddedDate
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ProductEntryRecord_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ProductEntryRecords]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ReadyScheduleAlarm_Insert",
                p => new
                    {
                        AlarmText = p.String(),
                        AlarmReadyDate = p.DateTime(),
                        GenerateDate = p.DateTime(),
                        EndDate = p.DateTime(),
                        IsActive = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[ReadyScheduleAlarms]([AlarmText], [AlarmReadyDate], [GenerateDate], [EndDate], [IsActive])
                      VALUES (@AlarmText, @AlarmReadyDate, @GenerateDate, @EndDate, @IsActive)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ReadyScheduleAlarms]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ReadyScheduleAlarms] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ReadyScheduleAlarm_Update",
                p => new
                    {
                        Id = p.Int(),
                        AlarmText = p.String(),
                        AlarmReadyDate = p.DateTime(),
                        GenerateDate = p.DateTime(),
                        EndDate = p.DateTime(),
                        IsActive = p.Boolean(),
                    },
                body:
                    @"UPDATE [dbo].[ReadyScheduleAlarms]
                      SET [AlarmText] = @AlarmText, [AlarmReadyDate] = @AlarmReadyDate, [GenerateDate] = @GenerateDate, [EndDate] = @EndDate, [IsActive] = @IsActive
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ReadyScheduleAlarm_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ReadyScheduleAlarms]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RetailBardanaItemEntry_Insert",
                p => new
                    {
                        RetailBardanaItemId = p.Int(),
                        RetailBardanaSupplierId = p.Int(),
                        QtyEntered = p.Decimal(precision: 16, scale: 4),
                        UnitPrice = p.Decimal(precision: 16, scale: 4),
                        TotalPrice = p.Decimal(precision: 16, scale: 4),
                        Remarks = p.String(),
                        Dated = p.DateTime(),
                        PurchasePrice = p.Decimal(precision: 16, scale: 4),
                        StockPrice = p.Decimal(precision: 16, scale: 4),
                        RetailPrice = p.Decimal(precision: 16, scale: 4),
                        CustomerPrice = p.Decimal(precision: 16, scale: 4),
                        DayBookId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[RetailBardanaItemEntries]([RetailBardanaItemId], [RetailBardanaSupplierId], [QtyEntered], [UnitPrice], [TotalPrice], [Remarks], [Dated], [PurchasePrice], [StockPrice], [RetailPrice], [CustomerPrice], [DayBookId])
                      VALUES (@RetailBardanaItemId, @RetailBardanaSupplierId, @QtyEntered, @UnitPrice, @TotalPrice, @Remarks, @Dated, @PurchasePrice, @StockPrice, @RetailPrice, @CustomerPrice, @DayBookId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[RetailBardanaItemEntries]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[RetailBardanaItemEntries] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.RetailBardanaItemEntry_Update",
                p => new
                    {
                        Id = p.Int(),
                        RetailBardanaItemId = p.Int(),
                        RetailBardanaSupplierId = p.Int(),
                        QtyEntered = p.Decimal(precision: 16, scale: 4),
                        UnitPrice = p.Decimal(precision: 16, scale: 4),
                        TotalPrice = p.Decimal(precision: 16, scale: 4),
                        Remarks = p.String(),
                        Dated = p.DateTime(),
                        PurchasePrice = p.Decimal(precision: 16, scale: 4),
                        StockPrice = p.Decimal(precision: 16, scale: 4),
                        RetailPrice = p.Decimal(precision: 16, scale: 4),
                        CustomerPrice = p.Decimal(precision: 16, scale: 4),
                        DayBookId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[RetailBardanaItemEntries]
                      SET [RetailBardanaItemId] = @RetailBardanaItemId, [RetailBardanaSupplierId] = @RetailBardanaSupplierId, [QtyEntered] = @QtyEntered, [UnitPrice] = @UnitPrice, [TotalPrice] = @TotalPrice, [Remarks] = @Remarks, [Dated] = @Dated, [PurchasePrice] = @PurchasePrice, [StockPrice] = @StockPrice, [RetailPrice] = @RetailPrice, [CustomerPrice] = @CustomerPrice, [DayBookId] = @DayBookId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RetailBardanaItemEntry_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[RetailBardanaItemEntries]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RetailBardanaItem_Insert",
                p => new
                    {
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        SKU = p.Decimal(precision: 16, scale: 4),
                        UnitPrice = p.Decimal(precision: 16, scale: 4),
                        UnitLaborPriceRetail = p.Decimal(precision: 16, scale: 4),
                        UnitLaborPriceWholeSale = p.Decimal(precision: 16, scale: 4),
                        GeneralAccountId = p.String(maxLength: 128),
                        PurchasePrice = p.Decimal(precision: 16, scale: 4),
                        RetailPrice = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[RetailBardanaItems]([Title], [TitleUrdu], [SKU], [UnitPrice], [UnitLaborPriceRetail], [UnitLaborPriceWholeSale], [GeneralAccountId], [PurchasePrice], [RetailPrice])
                      VALUES (@Title, @TitleUrdu, @SKU, @UnitPrice, @UnitLaborPriceRetail, @UnitLaborPriceWholeSale, @GeneralAccountId, @PurchasePrice, @RetailPrice)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[RetailBardanaItems]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[RetailBardanaItems] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.RetailBardanaItem_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        TitleUrdu = p.String(),
                        SKU = p.Decimal(precision: 16, scale: 4),
                        UnitPrice = p.Decimal(precision: 16, scale: 4),
                        UnitLaborPriceRetail = p.Decimal(precision: 16, scale: 4),
                        UnitLaborPriceWholeSale = p.Decimal(precision: 16, scale: 4),
                        GeneralAccountId = p.String(maxLength: 128),
                        PurchasePrice = p.Decimal(precision: 16, scale: 4),
                        RetailPrice = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[RetailBardanaItems]
                      SET [Title] = @Title, [TitleUrdu] = @TitleUrdu, [SKU] = @SKU, [UnitPrice] = @UnitPrice, [UnitLaborPriceRetail] = @UnitLaborPriceRetail, [UnitLaborPriceWholeSale] = @UnitLaborPriceWholeSale, [GeneralAccountId] = @GeneralAccountId, [PurchasePrice] = @PurchasePrice, [RetailPrice] = @RetailPrice
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RetailBardanaItem_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[RetailBardanaItems]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RetailBardanaSupplier_Insert",
                p => new
                    {
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Contact = p.String(),
                        Address = p.String(),
                        AddressUrdu = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Remarks = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[RetailBardanaSuppliers]([Name], [NameUrdu], [Contact], [Address], [AddressUrdu], [IsActive], [DateAdded], [Remarks], [GeneralAccountId])
                      VALUES (@Name, @NameUrdu, @Contact, @Address, @AddressUrdu, @IsActive, @DateAdded, @Remarks, @GeneralAccountId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[RetailBardanaSuppliers]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[RetailBardanaSuppliers] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.RetailBardanaSupplier_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        NameUrdu = p.String(),
                        Contact = p.String(),
                        Address = p.String(),
                        AddressUrdu = p.String(),
                        IsActive = p.Boolean(),
                        DateAdded = p.DateTime(),
                        Remarks = p.String(),
                        GeneralAccountId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[RetailBardanaSuppliers]
                      SET [Name] = @Name, [NameUrdu] = @NameUrdu, [Contact] = @Contact, [Address] = @Address, [AddressUrdu] = @AddressUrdu, [IsActive] = @IsActive, [DateAdded] = @DateAdded, [Remarks] = @Remarks, [GeneralAccountId] = @GeneralAccountId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.RetailBardanaSupplier_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[RetailBardanaSuppliers]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ReturnRecord_Insert",
                p => new
                    {
                        ProductId = p.Int(),
                        Item = p.String(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        Amount = p.Decimal(precision: 16, scale: 4),
                        AddedBy = p.String(),
                        ReturnDate = p.DateTime(),
                        InDate = p.DateTime(),
                    },
                body:
                    @"INSERT [dbo].[ReturnRecords]([ProductId], [Item], [Qty], [Amount], [AddedBy], [ReturnDate], [InDate])
                      VALUES (@ProductId, @Item, @Qty, @Amount, @AddedBy, @ReturnDate, @InDate)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ReturnRecords]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ReturnRecords] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ReturnRecord_Update",
                p => new
                    {
                        Id = p.Int(),
                        ProductId = p.Int(),
                        Item = p.String(),
                        Qty = p.Decimal(precision: 16, scale: 4),
                        Amount = p.Decimal(precision: 16, scale: 4),
                        AddedBy = p.String(),
                        ReturnDate = p.DateTime(),
                        InDate = p.DateTime(),
                    },
                body:
                    @"UPDATE [dbo].[ReturnRecords]
                      SET [ProductId] = @ProductId, [Item] = @Item, [Qty] = @Qty, [Amount] = @Amount, [AddedBy] = @AddedBy, [ReturnDate] = @ReturnDate, [InDate] = @InDate
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ReturnRecord_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ReturnRecords]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.SmsAlertSettings_Insert",
                p => new
                    {
                        Options = p.String(),
                    },
                body:
                    @"INSERT [dbo].[SmsAlertSettings]([Options])
                      VALUES (@Options)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[SmsAlertSettings]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[SmsAlertSettings] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.SmsAlertSettings_Update",
                p => new
                    {
                        Id = p.Int(),
                        Options = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[SmsAlertSettings]
                      SET [Options] = @Options
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.SmsAlertSettings_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[SmsAlertSettings]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.SmsConfigObj_Insert",
                p => new
                    {
                        UserName = p.String(),
                        Password = p.String(),
                        DateAdded = p.DateTime(),
                        string1 = p.String(),
                        string2 = p.String(),
                        int1 = p.Int(),
                        int2 = p.Int(),
                        double1 = p.Double(),
                        double2 = p.Double(),
                        decimal1 = p.Decimal(precision: 16, scale: 4),
                        decimal2 = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"INSERT [dbo].[SmsConfigObjs]([UserName], [Password], [DateAdded], [string1], [string2], [int1], [int2], [double1], [double2], [decimal1], [decimal2])
                      VALUES (@UserName, @Password, @DateAdded, @string1, @string2, @int1, @int2, @double1, @double2, @decimal1, @decimal2)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[SmsConfigObjs]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[SmsConfigObjs] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.SmsConfigObj_Update",
                p => new
                    {
                        Id = p.Int(),
                        UserName = p.String(),
                        Password = p.String(),
                        DateAdded = p.DateTime(),
                        string1 = p.String(),
                        string2 = p.String(),
                        int1 = p.Int(),
                        int2 = p.Int(),
                        double1 = p.Double(),
                        double2 = p.Double(),
                        decimal1 = p.Decimal(precision: 16, scale: 4),
                        decimal2 = p.Decimal(precision: 16, scale: 4),
                    },
                body:
                    @"UPDATE [dbo].[SmsConfigObjs]
                      SET [UserName] = @UserName, [Password] = @Password, [DateAdded] = @DateAdded, [string1] = @string1, [string2] = @string2, [int1] = @int1, [int2] = @int2, [double1] = @double1, [double2] = @double2, [decimal1] = @decimal1, [decimal2] = @decimal2
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.SmsConfigObj_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[SmsConfigObjs]
                      WHERE ([Id] = @Id)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.SmsConfigObj_Delete");
            DropStoredProcedure("dbo.SmsConfigObj_Update");
            DropStoredProcedure("dbo.SmsConfigObj_Insert");
            DropStoredProcedure("dbo.SmsAlertSettings_Delete");
            DropStoredProcedure("dbo.SmsAlertSettings_Update");
            DropStoredProcedure("dbo.SmsAlertSettings_Insert");
            DropStoredProcedure("dbo.ReturnRecord_Delete");
            DropStoredProcedure("dbo.ReturnRecord_Update");
            DropStoredProcedure("dbo.ReturnRecord_Insert");
            DropStoredProcedure("dbo.RetailBardanaSupplier_Delete");
            DropStoredProcedure("dbo.RetailBardanaSupplier_Update");
            DropStoredProcedure("dbo.RetailBardanaSupplier_Insert");
            DropStoredProcedure("dbo.RetailBardanaItem_Delete");
            DropStoredProcedure("dbo.RetailBardanaItem_Update");
            DropStoredProcedure("dbo.RetailBardanaItem_Insert");
            DropStoredProcedure("dbo.RetailBardanaItemEntry_Delete");
            DropStoredProcedure("dbo.RetailBardanaItemEntry_Update");
            DropStoredProcedure("dbo.RetailBardanaItemEntry_Insert");
            DropStoredProcedure("dbo.ReadyScheduleAlarm_Delete");
            DropStoredProcedure("dbo.ReadyScheduleAlarm_Update");
            DropStoredProcedure("dbo.ReadyScheduleAlarm_Insert");
            DropStoredProcedure("dbo.ProductEntryRecord_Delete");
            DropStoredProcedure("dbo.ProductEntryRecord_Update");
            DropStoredProcedure("dbo.ProductEntryRecord_Insert");
            DropStoredProcedure("dbo.OilDirtAlarm_Delete");
            DropStoredProcedure("dbo.OilDirtAlarm_Update");
            DropStoredProcedure("dbo.OilDirtAlarm_Insert");
            DropStoredProcedure("dbo.OilDealAlarm_Delete");
            DropStoredProcedure("dbo.OilDealAlarm_Update");
            DropStoredProcedure("dbo.OilDealAlarm_Insert");
            DropStoredProcedure("dbo.MiscTransaction_Delete");
            DropStoredProcedure("dbo.MiscTransaction_Update");
            DropStoredProcedure("dbo.MiscTransaction_Insert");
            DropStoredProcedure("dbo.LongTermAssetItem_Delete");
            DropStoredProcedure("dbo.LongTermAssetItem_Update");
            DropStoredProcedure("dbo.LongTermAssetItem_Insert");
            DropStoredProcedure("dbo.KeyInfo_Delete");
            DropStoredProcedure("dbo.KeyInfo_Update");
            DropStoredProcedure("dbo.KeyInfo_Insert");
            DropStoredProcedure("dbo.ItemPlaceSKU_Delete");
            DropStoredProcedure("dbo.ItemPlaceSKU_Update");
            DropStoredProcedure("dbo.ItemPlaceSKU_Insert");
            DropStoredProcedure("dbo.GeneralReceipt_Delete");
            DropStoredProcedure("dbo.GeneralReceipt_Update");
            DropStoredProcedure("dbo.GeneralReceipt_Insert");
            DropStoredProcedure("dbo.ExpenseItem_Delete");
            DropStoredProcedure("dbo.ExpenseItem_Update");
            DropStoredProcedure("dbo.ExpenseItem_Insert");
            DropStoredProcedure("dbo.ExpenseItemEntry_Delete");
            DropStoredProcedure("dbo.ExpenseItemEntry_Update");
            DropStoredProcedure("dbo.ExpenseItemEntry_Insert");
            DropStoredProcedure("dbo.EntZeroItemConsumption_Delete");
            DropStoredProcedure("dbo.EntZeroItemConsumption_Update");
            DropStoredProcedure("dbo.EntZeroItemConsumption_Insert");
            DropStoredProcedure("dbo.EntPurchase_Delete");
            DropStoredProcedure("dbo.EntPurchase_Update");
            DropStoredProcedure("dbo.EntPurchase_Insert");
            DropStoredProcedure("dbo.EntItem_Delete");
            DropStoredProcedure("dbo.EntItem_Update");
            DropStoredProcedure("dbo.EntItem_Insert");
            DropStoredProcedure("dbo.EntItemEntry_Delete");
            DropStoredProcedure("dbo.EntItemEntry_Update");
            DropStoredProcedure("dbo.EntItemEntry_Insert");
            DropStoredProcedure("dbo.EFileImage_Delete");
            DropStoredProcedure("dbo.EFileImage_Update");
            DropStoredProcedure("dbo.EFileImage_Insert");
            DropStoredProcedure("dbo.EFile_Delete");
            DropStoredProcedure("dbo.EFile_Update");
            DropStoredProcedure("dbo.EFile_Insert");
            DropStoredProcedure("dbo.EFCategory_Delete");
            DropStoredProcedure("dbo.EFCategory_Update");
            DropStoredProcedure("dbo.EFCategory_Insert");
            DropStoredProcedure("dbo.DEEntry_Delete");
            DropStoredProcedure("dbo.DEEntry_Update");
            DropStoredProcedure("dbo.DEEntry_Insert");
            DropStoredProcedure("dbo.DealSegment_Delete");
            DropStoredProcedure("dbo.DealSegment_Update");
            DropStoredProcedure("dbo.DealSegment_Insert");
            DropStoredProcedure("dbo.AppSettings_Delete");
            DropStoredProcedure("dbo.AppSettings_Update");
            DropStoredProcedure("dbo.AppSettings_Insert");
            DropStoredProcedure("dbo.SmsSubscriber_Delete");
            DropStoredProcedure("dbo.SmsSubscriber_Update");
            DropStoredProcedure("dbo.SmsSubscriber_Insert");
            DropStoredProcedure("dbo.ShopMessage_Delete");
            DropStoredProcedure("dbo.ShopMessage_Update");
            DropStoredProcedure("dbo.ShopMessage_Insert");
            DropStoredProcedure("dbo.AppMessageState_Delete");
            DropStoredProcedure("dbo.AppMessageState_Update");
            DropStoredProcedure("dbo.AppMessageState_Insert");
            DropStoredProcedure("dbo.Rahzam_Delete");
            DropStoredProcedure("dbo.Rahzam_Update");
            DropStoredProcedure("dbo.Rahzam_Insert");
            DropStoredProcedure("dbo.StoreLocation_Delete");
            DropStoredProcedure("dbo.StoreLocation_Update");
            DropStoredProcedure("dbo.StoreLocation_Insert");
            DropStoredProcedure("dbo.RepItemPreAddRecord_Delete");
            DropStoredProcedure("dbo.RepItemPreAddRecord_Update");
            DropStoredProcedure("dbo.RepItemPreAddRecord_Insert");
            DropStoredProcedure("dbo.RepItemConsumptionRecord_Delete");
            DropStoredProcedure("dbo.RepItemConsumptionRecord_Update");
            DropStoredProcedure("dbo.RepItemConsumptionRecord_Insert");
            DropStoredProcedure("dbo.RepairReceiveRecord_Delete");
            DropStoredProcedure("dbo.RepairReceiveRecord_Update");
            DropStoredProcedure("dbo.RepairReceiveRecord_Insert");
            DropStoredProcedure("dbo.RepPlace_Delete");
            DropStoredProcedure("dbo.RepPlace_Update");
            DropStoredProcedure("dbo.RepPlace_Insert");
            DropStoredProcedure("dbo.RepairDispatchRecord_Delete");
            DropStoredProcedure("dbo.RepairDispatchRecord_Update");
            DropStoredProcedure("dbo.RepairDispatchRecord_Insert");
            DropStoredProcedure("dbo.RepEntry_Delete");
            DropStoredProcedure("dbo.RepEntry_Update");
            DropStoredProcedure("dbo.RepEntry_Insert");
            DropStoredProcedure("dbo.Location_Delete");
            DropStoredProcedure("dbo.Location_Update");
            DropStoredProcedure("dbo.Location_Insert");
            DropStoredProcedure("dbo.ItemSupplier_Delete");
            DropStoredProcedure("dbo.ItemSupplier_Update");
            DropStoredProcedure("dbo.ItemSupplier_Insert");
            DropStoredProcedure("dbo.PurchaseInvoiceRecord_Delete");
            DropStoredProcedure("dbo.PurchaseInvoiceRecord_Update");
            DropStoredProcedure("dbo.PurchaseInvoiceRecord_Insert");
            DropStoredProcedure("dbo.ItemPurchaseEntry_Delete");
            DropStoredProcedure("dbo.ItemPurchaseEntry_Update");
            DropStoredProcedure("dbo.ItemPurchaseEntry_Insert");
            DropStoredProcedure("dbo.RepItemDamageRecord_Delete");
            DropStoredProcedure("dbo.RepItemDamageRecord_Update");
            DropStoredProcedure("dbo.RepItemDamageRecord_Insert");
            DropStoredProcedure("dbo.ItemCategory_Delete");
            DropStoredProcedure("dbo.ItemCategory_Update");
            DropStoredProcedure("dbo.ItemCategory_Insert");
            DropStoredProcedure("dbo.RepItem_Delete");
            DropStoredProcedure("dbo.RepItem_Update");
            DropStoredProcedure("dbo.RepItem_Insert");
            DropStoredProcedure("dbo.AdvanceItemRecord_Delete");
            DropStoredProcedure("dbo.AdvanceItemRecord_Update");
            DropStoredProcedure("dbo.AdvanceItemRecord_Insert");
            DropStoredProcedure("dbo.AccruedExpenseItem_Delete");
            DropStoredProcedure("dbo.AccruedExpenseItem_Update");
            DropStoredProcedure("dbo.AccruedExpenseItem_Insert");
            DropStoredProcedure("dbo.RawBrokerShareType_Delete");
            DropStoredProcedure("dbo.RawBrokerShareType_Update");
            DropStoredProcedure("dbo.RawBrokerShareType_Insert");
            DropStoredProcedure("dbo.AppUserOptions_Delete");
            DropStoredProcedure("dbo.AppUserOptions_Update");
            DropStoredProcedure("dbo.AppUserOptions_Insert");
            DropStoredProcedure("dbo.CancelOrderLine_Delete");
            DropStoredProcedure("dbo.CancelOrderLine_Update");
            DropStoredProcedure("dbo.CancelOrderLine_Insert");
            DropStoredProcedure("dbo.CancelSaleOrder_Delete");
            DropStoredProcedure("dbo.CancelSaleOrder_Update");
            DropStoredProcedure("dbo.CancelSaleOrder_Insert");
            DropStoredProcedure("dbo.ArchiveOrderLine_Delete");
            DropStoredProcedure("dbo.ArchiveOrderLine_Update");
            DropStoredProcedure("dbo.ArchiveOrderLine_Insert");
            DropStoredProcedure("dbo.SIEntry_Delete");
            DropStoredProcedure("dbo.SIEntry_Update");
            DropStoredProcedure("dbo.SIEntry_Insert");
            DropStoredProcedure("dbo.StockItem_Delete");
            DropStoredProcedure("dbo.StockItem_Update");
            DropStoredProcedure("dbo.StockItem_Insert");
            DropStoredProcedure("dbo.ProductSize_Delete");
            DropStoredProcedure("dbo.ProductSize_Update");
            DropStoredProcedure("dbo.ProductSize_Insert");
            DropStoredProcedure("dbo.ProductCategory_Delete");
            DropStoredProcedure("dbo.ProductCategory_Update");
            DropStoredProcedure("dbo.ProductCategory_Insert");
            DropStoredProcedure("dbo.Product_Delete");
            DropStoredProcedure("dbo.Product_Update");
            DropStoredProcedure("dbo.Product_Insert");
            DropStoredProcedure("dbo.SaleOrderLine_Delete");
            DropStoredProcedure("dbo.SaleOrderLine_Update");
            DropStoredProcedure("dbo.SaleOrderLine_Insert");
            DropStoredProcedure("dbo.ReadySelector_Delete");
            DropStoredProcedure("dbo.ReadySelector_Update");
            DropStoredProcedure("dbo.ReadySelector_Insert");
            DropStoredProcedure("dbo.ReadyTradeUnit_Delete");
            DropStoredProcedure("dbo.ReadyTradeUnit_Update");
            DropStoredProcedure("dbo.ReadyTradeUnit_Insert");
            DropStoredProcedure("dbo.ReadyItem_Delete");
            DropStoredProcedure("dbo.ReadyItem_Update");
            DropStoredProcedure("dbo.ReadyItem_Insert");
            DropStoredProcedure("dbo.ReadyBroker_Delete");
            DropStoredProcedure("dbo.ReadyBroker_Update");
            DropStoredProcedure("dbo.ReadyBroker_Insert");
            DropStoredProcedure("dbo.ReadyDeal_Delete");
            DropStoredProcedure("dbo.ReadyDeal_Update");
            DropStoredProcedure("dbo.ReadyDeal_Insert");
            DropStoredProcedure("dbo.ReadyDriver_Delete");
            DropStoredProcedure("dbo.ReadyDriver_Update");
            DropStoredProcedure("dbo.ReadyDriver_Insert");
            DropStoredProcedure("dbo.BharthiType_Delete");
            DropStoredProcedure("dbo.BharthiType_Update");
            DropStoredProcedure("dbo.BharthiType_Insert");
            DropStoredProcedure("dbo.Bharthi_Delete");
            DropStoredProcedure("dbo.Bharthi_Update");
            DropStoredProcedure("dbo.Bharthi_Insert");
            DropStoredProcedure("dbo.ReadySchedule_Delete");
            DropStoredProcedure("dbo.ReadySchedule_Update");
            DropStoredProcedure("dbo.ReadySchedule_Insert");
            DropStoredProcedure("dbo.OilDirtSelector_Delete");
            DropStoredProcedure("dbo.OilDirtSelector_Update");
            DropStoredProcedure("dbo.OilDirtSelector_Insert");
            DropStoredProcedure("dbo.OilDirtDriver_Delete");
            DropStoredProcedure("dbo.OilDirtDriver_Update");
            DropStoredProcedure("dbo.OilDirtDriver_Insert");
            DropStoredProcedure("dbo.OilDirtTradeUnit_Delete");
            DropStoredProcedure("dbo.OilDirtTradeUnit_Update");
            DropStoredProcedure("dbo.OilDirtTradeUnit_Insert");
            DropStoredProcedure("dbo.OilDirtItem_Delete");
            DropStoredProcedure("dbo.OilDirtItem_Update");
            DropStoredProcedure("dbo.OilDirtItem_Insert");
            DropStoredProcedure("dbo.OilDirtBroker_Delete");
            DropStoredProcedure("dbo.OilDirtBroker_Update");
            DropStoredProcedure("dbo.OilDirtBroker_Insert");
            DropStoredProcedure("dbo.OilDirtDeal_Delete");
            DropStoredProcedure("dbo.OilDirtDeal_Update");
            DropStoredProcedure("dbo.OilDirtDeal_Insert");
            DropStoredProcedure("dbo.OilDirtSchedule_Delete");
            DropStoredProcedure("dbo.OilDirtSchedule_Update");
            DropStoredProcedure("dbo.OilDirtSchedule_Insert");
            DropStoredProcedure("dbo.OilTradeUnit_Delete");
            DropStoredProcedure("dbo.OilTradeUnit_Update");
            DropStoredProcedure("dbo.OilTradeUnit_Insert");
            DropStoredProcedure("dbo.OilDealSelector_Delete");
            DropStoredProcedure("dbo.OilDealSelector_Update");
            DropStoredProcedure("dbo.OilDealSelector_Insert");
            DropStoredProcedure("dbo.OilDealItem_Delete");
            DropStoredProcedure("dbo.OilDealItem_Update");
            DropStoredProcedure("dbo.OilDealItem_Insert");
            DropStoredProcedure("dbo.OilDealDriver_Delete");
            DropStoredProcedure("dbo.OilDealDriver_Update");
            DropStoredProcedure("dbo.OilDealDriver_Insert");
            DropStoredProcedure("dbo.OilDealBroker_Delete");
            DropStoredProcedure("dbo.OilDealBroker_Update");
            DropStoredProcedure("dbo.OilDealBroker_Insert");
            DropStoredProcedure("dbo.OilDeal_Delete");
            DropStoredProcedure("dbo.OilDeal_Update");
            DropStoredProcedure("dbo.OilDeal_Insert");
            DropStoredProcedure("dbo.Vehicle_Delete");
            DropStoredProcedure("dbo.Vehicle_Update");
            DropStoredProcedure("dbo.Vehicle_Insert");
            DropStoredProcedure("dbo.Selector_Delete");
            DropStoredProcedure("dbo.Selector_Update");
            DropStoredProcedure("dbo.Selector_Insert");
            DropStoredProcedure("dbo.WeighBridge_Delete");
            DropStoredProcedure("dbo.WeighBridge_Update");
            DropStoredProcedure("dbo.WeighBridge_Insert");
            DropStoredProcedure("dbo.ScheduleWeighBridge_Delete");
            DropStoredProcedure("dbo.ScheduleWeighBridge_Update");
            DropStoredProcedure("dbo.ScheduleWeighBridge_Insert");
            DropStoredProcedure("dbo.ScheduleLoadPacking_Delete");
            DropStoredProcedure("dbo.ScheduleLoadPacking_Update");
            DropStoredProcedure("dbo.ScheduleLoadPacking_Insert");
            DropStoredProcedure("dbo.PackingStock_Delete");
            DropStoredProcedure("dbo.PackingStock_Update");
            DropStoredProcedure("dbo.PackingStock_Insert");
            DropStoredProcedure("dbo.TradeUnit_Delete");
            DropStoredProcedure("dbo.TradeUnit_Update");
            DropStoredProcedure("dbo.TradeUnit_Insert");
            DropStoredProcedure("dbo.PackingUnit_Delete");
            DropStoredProcedure("dbo.PackingUnit_Update");
            DropStoredProcedure("dbo.PackingUnit_Insert");
            DropStoredProcedure("dbo.DealItem_Delete");
            DropStoredProcedure("dbo.DealItem_Update");
            DropStoredProcedure("dbo.DealItem_Insert");
            DropStoredProcedure("dbo.Company_Delete");
            DropStoredProcedure("dbo.Company_Update");
            DropStoredProcedure("dbo.Company_Insert");
            DropStoredProcedure("dbo.OmeDeal_Delete");
            DropStoredProcedure("dbo.OmeDeal_Update");
            DropStoredProcedure("dbo.OmeDeal_Insert");
            DropStoredProcedure("dbo.FactoryPackingStock_Delete");
            DropStoredProcedure("dbo.FactoryPackingStock_Update");
            DropStoredProcedure("dbo.FactoryPackingStock_Insert");
            DropStoredProcedure("dbo.FactoryPackingStockIssueRecord_Delete");
            DropStoredProcedure("dbo.FactoryPackingStockIssueRecord_Update");
            DropStoredProcedure("dbo.FactoryPackingStockIssueRecord_Insert");
            DropStoredProcedure("dbo.DealPackingSupplier_Delete");
            DropStoredProcedure("dbo.DealPackingSupplier_Update");
            DropStoredProcedure("dbo.DealPackingSupplier_Insert");
            DropStoredProcedure("dbo.FactoryPackingStockAddedRecord_Delete");
            DropStoredProcedure("dbo.FactoryPackingStockAddedRecord_Update");
            DropStoredProcedure("dbo.FactoryPackingStockAddedRecord_Insert");
            DropStoredProcedure("dbo.DealPacking_Delete");
            DropStoredProcedure("dbo.DealPacking_Update");
            DropStoredProcedure("dbo.DealPacking_Insert");
            DropStoredProcedure("dbo.PackingIssueReceiveRecord_Delete");
            DropStoredProcedure("dbo.PackingIssueReceiveRecord_Update");
            DropStoredProcedure("dbo.PackingIssueReceiveRecord_Insert");
            DropStoredProcedure("dbo.GoodCompany_Delete");
            DropStoredProcedure("dbo.GoodCompany_Update");
            DropStoredProcedure("dbo.GoodCompany_Insert");
            DropStoredProcedure("dbo.SalaryDeduction_Delete");
            DropStoredProcedure("dbo.SalaryDeduction_Update");
            DropStoredProcedure("dbo.SalaryDeduction_Insert");
            DropStoredProcedure("dbo.EmployeeSalaryEntryDeduction_Delete");
            DropStoredProcedure("dbo.EmployeeSalaryEntryDeduction_Update");
            DropStoredProcedure("dbo.EmployeeSalaryEntryDeduction_Insert");
            DropStoredProcedure("dbo.SalaryAllowance_Delete");
            DropStoredProcedure("dbo.SalaryAllowance_Update");
            DropStoredProcedure("dbo.SalaryAllowance_Insert");
            DropStoredProcedure("dbo.EmployeeSalaryEntryAllowance_Delete");
            DropStoredProcedure("dbo.EmployeeSalaryEntryAllowance_Update");
            DropStoredProcedure("dbo.EmployeeSalaryEntryAllowance_Insert");
            DropStoredProcedure("dbo.EmployPayRollEntry_Delete");
            DropStoredProcedure("dbo.EmployPayRollEntry_Update");
            DropStoredProcedure("dbo.EmployPayRollEntry_Insert");
            DropStoredProcedure("dbo.CreditEntry_Delete");
            DropStoredProcedure("dbo.CreditEntry_Update");
            DropStoredProcedure("dbo.CreditEntry_Insert");
            DropStoredProcedure("dbo.AttendanceRecord_Delete");
            DropStoredProcedure("dbo.AttendanceRecord_Update");
            DropStoredProcedure("dbo.AttendanceRecord_Insert");
            DropStoredProcedure("dbo.Employee_Delete");
            DropStoredProcedure("dbo.Employee_Update");
            DropStoredProcedure("dbo.Employee_Insert");
            DropStoredProcedure("dbo.Driver_Delete");
            DropStoredProcedure("dbo.Driver_Update");
            DropStoredProcedure("dbo.Driver_Insert");
            DropStoredProcedure("dbo.ScheduleAlarm_Delete");
            DropStoredProcedure("dbo.ScheduleAlarm_Update");
            DropStoredProcedure("dbo.ScheduleAlarm_Insert");
            DropStoredProcedure("dbo.DealSchedule_Delete");
            DropStoredProcedure("dbo.DealSchedule_Update");
            DropStoredProcedure("dbo.DealSchedule_Insert");
            DropStoredProcedure("dbo.DayBook_Delete");
            DropStoredProcedure("dbo.DayBook_Update");
            DropStoredProcedure("dbo.DayBook_Insert");
            DropStoredProcedure("dbo.SaleOrder_Delete");
            DropStoredProcedure("dbo.SaleOrder_Update");
            DropStoredProcedure("dbo.SaleOrder_Insert");
            DropStoredProcedure("dbo.CustomerCategory_Delete");
            DropStoredProcedure("dbo.CustomerCategory_Update");
            DropStoredProcedure("dbo.CustomerCategory_Insert");
            DropStoredProcedure("dbo.Customer_Delete");
            DropStoredProcedure("dbo.Customer_Update");
            DropStoredProcedure("dbo.Customer_Insert");
            DropStoredProcedure("dbo.ArchiveSaleOrder_Delete");
            DropStoredProcedure("dbo.ArchiveSaleOrder_Update");
            DropStoredProcedure("dbo.ArchiveSaleOrder_Insert");
            DropStoredProcedure("dbo.AppUser_Delete");
            DropStoredProcedure("dbo.AppUser_Update");
            DropStoredProcedure("dbo.AppUser_Insert");
            DropStoredProcedure("dbo.AppDeal_Delete");
            DropStoredProcedure("dbo.AppDeal_Update");
            DropStoredProcedure("dbo.AppDeal_Insert");
            DropStoredProcedure("dbo.Broker_Delete");
            DropStoredProcedure("dbo.Broker_Update");
            DropStoredProcedure("dbo.Broker_Insert");
            DropStoredProcedure("dbo.AccountTransaction_Delete");
            DropStoredProcedure("dbo.AccountTransaction_Update");
            DropStoredProcedure("dbo.AccountTransaction_Insert");
            DropStoredProcedure("dbo.HeadAccount_Delete");
            DropStoredProcedure("dbo.HeadAccount_Update");
            DropStoredProcedure("dbo.HeadAccount_Insert");
            DropStoredProcedure("dbo.TopHeadAccount_Delete");
            DropStoredProcedure("dbo.TopHeadAccount_Update");
            DropStoredProcedure("dbo.TopHeadAccount_Insert");
            DropStoredProcedure("dbo.SubHeadAccount_Delete");
            DropStoredProcedure("dbo.SubHeadAccount_Update");
            DropStoredProcedure("dbo.SubHeadAccount_Insert");
            DropStoredProcedure("dbo.GeneralAccount_Delete");
            DropStoredProcedure("dbo.GeneralAccount_Update");
            DropStoredProcedure("dbo.GeneralAccount_Insert");
            DropStoredProcedure("dbo.AccountBase_Delete");
            DropStoredProcedure("dbo.AccountBase_Update");
            DropStoredProcedure("dbo.AccountBase_Insert");
            DropForeignKey("dbo.ReturnRecords", "ProductId", "dbo.Products");
            DropForeignKey("dbo.RetailBardanaItemEntries", "RetailBardanaSupplierId", "dbo.RetailBardanaSuppliers");
            DropForeignKey("dbo.RetailBardanaSuppliers", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.RetailBardanaItemEntries", "RetailBardanaItemId", "dbo.RetailBardanaItems");
            DropForeignKey("dbo.RetailBardanaItems", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.ProductEntryRecords", "ProductId", "dbo.Products");
            DropForeignKey("dbo.MiscTransactions", "debitAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.MiscTransactions", "creditAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.LongTermAssetItems", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.ExpenseItemEntries", "ExpenseItemId", "dbo.ExpenseItems");
            DropForeignKey("dbo.ExpenseItems", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.EntItemEntries", "EntPurchaseId", "dbo.EntPurchases");
            DropForeignKey("dbo.EntItemEntries", "EntItemId", "dbo.EntItems");
            DropForeignKey("dbo.EFileImages", "EFileId", "dbo.EFiles");
            DropForeignKey("dbo.EFiles", "EFCategoryId", "dbo.EFCategories");
            DropForeignKey("dbo.DealSegments", "OmeDealId", "dbo.OmeDeals");
            DropForeignKey("dbo.AppMessageStates", "SmsSubsriberId", "dbo.SmsSubscribers");
            DropForeignKey("dbo.AppMessageStates", "ShopMessageId", "dbo.ShopMessages");
            DropForeignKey("dbo.RepItems", "StoreLocationId", "dbo.StoreLocations");
            DropForeignKey("dbo.RepItemPreAddRecords", "RepItemId", "dbo.RepItems");
            DropForeignKey("dbo.RepItemConsumptionRecords", "RepItemId", "dbo.RepItems");
            DropForeignKey("dbo.RepEntries", "RepItemId", "dbo.RepItems");
            DropForeignKey("dbo.RepEntries", "RepairDispatchRecordId", "dbo.RepairDispatchRecords");
            DropForeignKey("dbo.RepairReceiveRecords", "RepPlaceId", "dbo.RepPlaces");
            DropForeignKey("dbo.RepEntries", "RepairReceiveRecordId", "dbo.RepairReceiveRecords");
            DropForeignKey("dbo.RepairDispatchRecords", "RepPlaceId", "dbo.RepPlaces");
            DropForeignKey("dbo.RepPlaces", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.AdvanceItemRecords", "RepPlaceId", "dbo.RepPlaces");
            DropForeignKey("dbo.RepItems", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.ItemPurchaseEntries", "RepItemId", "dbo.RepItems");
            DropForeignKey("dbo.PurchaseInvoiceRecords", "ItemSupplierId", "dbo.ItemSuppliers");
            DropForeignKey("dbo.ItemSuppliers", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.ItemPurchaseEntries", "PurchaseInvoiceRecordId", "dbo.PurchaseInvoiceRecords");
            DropForeignKey("dbo.RepItemDamageRecords", "RepItemId", "dbo.RepItems");
            DropForeignKey("dbo.RepItems", "ItemCategoryId", "dbo.ItemCategories");
            DropForeignKey("dbo.AdvanceItemRecords", "RepItemId", "dbo.RepItems");
            DropForeignKey("dbo.AccruedExpenseItems", "DebitAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.AccruedExpenseItems", "CreditAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.AccountBases", "TopHeadAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.AccountBases", "HeadAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.AccountBases", "SubHeadAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.AccountBases", "ParentAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.AppDeals", "RawBrokerShareTypeId", "dbo.RawBrokerShareTypes");
            DropForeignKey("dbo.AppDeals", "PackingUnitId", "dbo.PackingUnits");
            DropForeignKey("dbo.AppDeals", "DealPackingId", "dbo.DealPackings");
            DropForeignKey("dbo.AppDeals", "BrokerId", "dbo.Brokers");
            DropForeignKey("dbo.AppUserOptions", "AppUserId", "dbo.AppUsers");
            DropForeignKey("dbo.AppDeals", "AppUserId", "dbo.AppUsers");
            DropForeignKey("dbo.CancelOrderLines", "CancelSaleOrderId", "dbo.CancelSaleOrders");
            DropForeignKey("dbo.CancelOrderLines", "ProductId", "dbo.Products");
            DropForeignKey("dbo.DayBooks", "CancelSaleOrder_Id", "dbo.CancelSaleOrders");
            DropForeignKey("dbo.CancelSaleOrders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CancelSaleOrders", "AppUserId", "dbo.AppUsers");
            DropForeignKey("dbo.ArchiveOrderLines", "ArchiveSaleOrderId", "dbo.ArchiveSaleOrders");
            DropForeignKey("dbo.ArchiveOrderLines", "ProductId", "dbo.Products");
            DropForeignKey("dbo.DayBooks", "ArchiveSaleOrder_Id", "dbo.ArchiveSaleOrders");
            DropForeignKey("dbo.ArchiveSaleOrders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.SaleOrderLines", "SaleOrderId", "dbo.SaleOrders");
            DropForeignKey("dbo.Products", "StockItemId", "dbo.StockItems");
            DropForeignKey("dbo.SIEntries", "StockItemId", "dbo.StockItems");
            DropForeignKey("dbo.SaleOrderLines", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductSizeId", "dbo.ProductSizes");
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.Products", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.DayBooks", "SaleOrderId", "dbo.SaleOrders");
            DropForeignKey("dbo.ReadySchedules", "WeighBridgeId", "dbo.WeighBridges");
            DropForeignKey("dbo.ReadySchedules", "ReadySelectorId", "dbo.ReadySelectors");
            DropForeignKey("dbo.ReadySelectors", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.ReadyDeals", "ReadyTradeUnitId", "dbo.ReadyTradeUnits");
            DropForeignKey("dbo.ReadySchedules", "ReadyDealId", "dbo.ReadyDeals");
            DropForeignKey("dbo.ReadyDeals", "ReadyItemId", "dbo.ReadyItems");
            DropForeignKey("dbo.ReadyItems", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.ReadyDeals", "ReadyBrokerId", "dbo.ReadyBrokers");
            DropForeignKey("dbo.ReadyBrokers", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.ReadyDeals", "AppUserId", "dbo.AppUsers");
            DropForeignKey("dbo.ReadySchedules", "ReadyDriverId", "dbo.ReadyDrivers");
            DropForeignKey("dbo.DayBooks", "ReadyScheduleId", "dbo.ReadySchedules");
            DropForeignKey("dbo.Bharthis", "ReadyScheduleId", "dbo.ReadySchedules");
            DropForeignKey("dbo.Bharthis", "BharthiTypeId", "dbo.BharthiTypes");
            DropForeignKey("dbo.OilDirtSchedules", "WeighBridgeId", "dbo.WeighBridges");
            DropForeignKey("dbo.OilDirtSchedules", "OilDirtSelectorId", "dbo.OilDirtSelectors");
            DropForeignKey("dbo.OilDirtSelectors", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.OilDirtSchedules", "OilDirtDriverId", "dbo.OilDirtDrivers");
            DropForeignKey("dbo.OilDirtDeals", "OilDirtTradeUnitId", "dbo.OilDirtTradeUnits");
            DropForeignKey("dbo.OilDirtSchedules", "OilDirtDealId", "dbo.OilDirtDeals");
            DropForeignKey("dbo.OilDirtDeals", "OilDirtItemId", "dbo.OilDirtItems");
            DropForeignKey("dbo.OilDirtItems", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.OilDirtDeals", "OilDirtBrokerId", "dbo.OilDirtBrokers");
            DropForeignKey("dbo.OilDirtBrokers", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.OilDirtDeals", "AppUserId", "dbo.AppUsers");
            DropForeignKey("dbo.DayBooks", "OilDirtScheduleId", "dbo.OilDirtSchedules");
            DropForeignKey("dbo.OilDeals", "WeighBridgeId", "dbo.WeighBridges");
            DropForeignKey("dbo.OilDeals", "OilTradeUnitId", "dbo.OilTradeUnits");
            DropForeignKey("dbo.OilDeals", "OilDealSelectorId", "dbo.OilDealSelectors");
            DropForeignKey("dbo.OilDealSelectors", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.OilDeals", "OilDealItemId", "dbo.OilDealItems");
            DropForeignKey("dbo.OilDealItems", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.OilDeals", "OilDealDriverId", "dbo.OilDealDrivers");
            DropForeignKey("dbo.DayBooks", "OilDealId", "dbo.OilDeals");
            DropForeignKey("dbo.OilDeals", "OilDealBrokerId", "dbo.OilDealBrokers");
            DropForeignKey("dbo.OilDealBrokers", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.OilDeals", "AppUserId", "dbo.AppUsers");
            DropForeignKey("dbo.DealSchedules", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.DealSchedules", "SelectorId", "dbo.Selectors");
            DropForeignKey("dbo.OmeDeals", "Selector_Id", "dbo.Selectors");
            DropForeignKey("dbo.Selectors", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.ScheduleWeighBridges", "WeighBridgeId", "dbo.WeighBridges");
            DropForeignKey("dbo.ScheduleWeighBridges", "DealScheduleId", "dbo.DealSchedules");
            DropForeignKey("dbo.DealSchedules", "GoodCompanyId", "dbo.GoodCompanies");
            DropForeignKey("dbo.PackingIssueReceiveRecords", "GoodCompanyId", "dbo.GoodCompanies");
            DropForeignKey("dbo.ScheduleLoadPackings", "DealScheduleId", "dbo.DealSchedules");
            DropForeignKey("dbo.ScheduleLoadPackings", "DealPackingId", "dbo.DealPackings");
            DropForeignKey("dbo.PackingStocks", "GoodCompanyId", "dbo.GoodCompanies");
            DropForeignKey("dbo.PackingStocks", "DealPackingId", "dbo.DealPackings");
            DropForeignKey("dbo.PackingIssueReceiveRecords", "DealPackingId", "dbo.DealPackings");
            DropForeignKey("dbo.OmeDeals", "TradeUnitId", "dbo.TradeUnits");
            DropForeignKey("dbo.AppDeals", "TradeUnitId", "dbo.TradeUnits");
            DropForeignKey("dbo.OmeDeals", "PackingUnitId", "dbo.PackingUnits");
            DropForeignKey("dbo.OmeDeals", "DealPackingId", "dbo.DealPackings");
            DropForeignKey("dbo.DealSchedules", "OmeDealId", "dbo.OmeDeals");
            DropForeignKey("dbo.OmeDeals", "DealItemId", "dbo.DealItems");
            DropForeignKey("dbo.AppDeals", "DealItemId", "dbo.DealItems");
            DropForeignKey("dbo.DealItems", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.OmeDeals", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.AppDeals", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Companies", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.OmeDeals", "BrokerId", "dbo.Brokers");
            DropForeignKey("dbo.FactoryPackingStocks", "DealPackingId", "dbo.DealPackings");
            DropForeignKey("dbo.FactoryPackingStockIssueRecords", "DealPackingId", "dbo.DealPackings");
            DropForeignKey("dbo.FactoryPackingStockAddedRecords", "DealPackingSupplierId", "dbo.DealPackingSuppliers");
            DropForeignKey("dbo.DealPackingSuppliers", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.FactoryPackingStockAddedRecords", "DealPackingId", "dbo.DealPackings");
            DropForeignKey("dbo.DealPackings", "Account_Id", "dbo.AccountBases");
            DropForeignKey("dbo.GoodCompanies", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.EmployeeSalaryEntryDeductions", "SalaryDeductionId", "dbo.SalaryDeductions");
            DropForeignKey("dbo.EmployeeSalaryEntryDeductions", "EmployPayRollEntryId", "dbo.EmployPayRollEntries");
            DropForeignKey("dbo.EmployeeSalaryEntryAllowances", "SalaryAllowanceId", "dbo.SalaryAllowances");
            DropForeignKey("dbo.EmployeeSalaryEntryAllowances", "EmployPayRollEntryId", "dbo.EmployPayRollEntries");
            DropForeignKey("dbo.EmployPayRollEntries", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.DealSchedules", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.CreditEntries", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.AttendanceRecords", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.Employees", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.DealSchedules", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.DayBooks", "DealScheduleId", "dbo.DealSchedules");
            DropForeignKey("dbo.DealSchedules", "AppDealId", "dbo.AppDeals");
            DropForeignKey("dbo.ScheduleAlarms", "Id", "dbo.DealSchedules");
            DropForeignKey("dbo.SaleOrders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.SaleOrders", "AppUserId", "dbo.AppUsers");
            DropForeignKey("dbo.Customers", "CustomerCategoryId", "dbo.CustomerCategories");
            DropForeignKey("dbo.Customers", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.ArchiveSaleOrders", "AppUserId", "dbo.AppUsers");
            DropForeignKey("dbo.Brokers", "GeneralAccountId", "dbo.AccountBases");
            DropForeignKey("dbo.AccountTransactions", "GeneralAccountId", "dbo.AccountBases");
            DropIndex("dbo.ReturnRecords", new[] { "ProductId" });
            DropIndex("dbo.RetailBardanaSuppliers", new[] { "GeneralAccountId" });
            DropIndex("dbo.RetailBardanaItems", new[] { "GeneralAccountId" });
            DropIndex("dbo.RetailBardanaItemEntries", new[] { "RetailBardanaSupplierId" });
            DropIndex("dbo.RetailBardanaItemEntries", new[] { "RetailBardanaItemId" });
            DropIndex("dbo.ProductEntryRecords", new[] { "ProductId" });
            DropIndex("dbo.MiscTransactions", new[] { "creditAccountId" });
            DropIndex("dbo.MiscTransactions", new[] { "debitAccountId" });
            DropIndex("dbo.LongTermAssetItems", new[] { "GeneralAccountId" });
            DropIndex("dbo.ExpenseItems", new[] { "GeneralAccountId" });
            DropIndex("dbo.ExpenseItemEntries", new[] { "ExpenseItemId" });
            DropIndex("dbo.EntItemEntries", new[] { "EntPurchaseId" });
            DropIndex("dbo.EntItemEntries", new[] { "EntItemId" });
            DropIndex("dbo.EFileImages", new[] { "EFileId" });
            DropIndex("dbo.EFiles", new[] { "EFCategoryId" });
            DropIndex("dbo.DealSegments", new[] { "OmeDealId" });
            DropIndex("dbo.AppMessageStates", new[] { "SmsSubsriberId" });
            DropIndex("dbo.AppMessageStates", new[] { "ShopMessageId" });
            DropIndex("dbo.RepItemPreAddRecords", new[] { "RepItemId" });
            DropIndex("dbo.RepItemConsumptionRecords", new[] { "RepItemId" });
            DropIndex("dbo.RepairReceiveRecords", new[] { "RepPlaceId" });
            DropIndex("dbo.RepPlaces", new[] { "GeneralAccountId" });
            DropIndex("dbo.RepairDispatchRecords", new[] { "RepPlaceId" });
            DropIndex("dbo.RepEntries", new[] { "RepairReceiveRecordId" });
            DropIndex("dbo.RepEntries", new[] { "RepairDispatchRecordId" });
            DropIndex("dbo.RepEntries", new[] { "RepItemId" });
            DropIndex("dbo.ItemSuppliers", new[] { "GeneralAccountId" });
            DropIndex("dbo.PurchaseInvoiceRecords", new[] { "ItemSupplierId" });
            DropIndex("dbo.ItemPurchaseEntries", new[] { "RepItemId" });
            DropIndex("dbo.ItemPurchaseEntries", new[] { "PurchaseInvoiceRecordId" });
            DropIndex("dbo.RepItemDamageRecords", new[] { "RepItemId" });
            DropIndex("dbo.RepItems", new[] { "StoreLocationId" });
            DropIndex("dbo.RepItems", new[] { "ItemCategoryId" });
            DropIndex("dbo.RepItems", new[] { "LocationId" });
            DropIndex("dbo.AdvanceItemRecords", new[] { "RepPlaceId" });
            DropIndex("dbo.AdvanceItemRecords", new[] { "RepItemId" });
            DropIndex("dbo.AccruedExpenseItems", new[] { "CreditAccountId" });
            DropIndex("dbo.AccruedExpenseItems", new[] { "DebitAccountId" });
            DropIndex("dbo.AppUserOptions", new[] { "AppUserId" });
            DropIndex("dbo.CancelOrderLines", new[] { "CancelSaleOrderId" });
            DropIndex("dbo.CancelOrderLines", new[] { "ProductId" });
            DropIndex("dbo.CancelSaleOrders", new[] { "AppUserId" });
            DropIndex("dbo.CancelSaleOrders", new[] { "CustomerId" });
            DropIndex("dbo.ArchiveOrderLines", new[] { "ArchiveSaleOrderId" });
            DropIndex("dbo.ArchiveOrderLines", new[] { "ProductId" });
            DropIndex("dbo.SIEntries", new[] { "StockItemId" });
            DropIndex("dbo.Products", new[] { "StockItemId" });
            DropIndex("dbo.Products", new[] { "GeneralAccountId" });
            DropIndex("dbo.Products", new[] { "ProductSizeId" });
            DropIndex("dbo.Products", new[] { "ProductCategoryId" });
            DropIndex("dbo.SaleOrderLines", new[] { "SaleOrderId" });
            DropIndex("dbo.SaleOrderLines", new[] { "ProductId" });
            DropIndex("dbo.ReadySelectors", new[] { "GeneralAccountId" });
            DropIndex("dbo.ReadyItems", new[] { "GeneralAccountId" });
            DropIndex("dbo.ReadyBrokers", new[] { "GeneralAccountId" });
            DropIndex("dbo.ReadyDeals", new[] { "AppUserId" });
            DropIndex("dbo.ReadyDeals", new[] { "ReadyTradeUnitId" });
            DropIndex("dbo.ReadyDeals", new[] { "ReadyItemId" });
            DropIndex("dbo.ReadyDeals", new[] { "ReadyBrokerId" });
            DropIndex("dbo.Bharthis", new[] { "ReadyScheduleId" });
            DropIndex("dbo.Bharthis", new[] { "BharthiTypeId" });
            DropIndex("dbo.ReadySchedules", new[] { "ReadySelectorId" });
            DropIndex("dbo.ReadySchedules", new[] { "ReadyDealId" });
            DropIndex("dbo.ReadySchedules", new[] { "WeighBridgeId" });
            DropIndex("dbo.ReadySchedules", new[] { "ReadyDriverId" });
            DropIndex("dbo.OilDirtSelectors", new[] { "GeneralAccountId" });
            DropIndex("dbo.OilDirtItems", new[] { "GeneralAccountId" });
            DropIndex("dbo.OilDirtBrokers", new[] { "GeneralAccountId" });
            DropIndex("dbo.OilDirtDeals", new[] { "AppUserId" });
            DropIndex("dbo.OilDirtDeals", new[] { "OilDirtTradeUnitId" });
            DropIndex("dbo.OilDirtDeals", new[] { "OilDirtItemId" });
            DropIndex("dbo.OilDirtDeals", new[] { "OilDirtBrokerId" });
            DropIndex("dbo.OilDirtSchedules", new[] { "OilDirtDealId" });
            DropIndex("dbo.OilDirtSchedules", new[] { "WeighBridgeId" });
            DropIndex("dbo.OilDirtSchedules", new[] { "OilDirtDriverId" });
            DropIndex("dbo.OilDirtSchedules", new[] { "OilDirtSelectorId" });
            DropIndex("dbo.OilDealSelectors", new[] { "GeneralAccountId" });
            DropIndex("dbo.OilDealItems", new[] { "GeneralAccountId" });
            DropIndex("dbo.OilDealBrokers", new[] { "GeneralAccountId" });
            DropIndex("dbo.OilDeals", new[] { "WeighBridgeId" });
            DropIndex("dbo.OilDeals", new[] { "AppUserId" });
            DropIndex("dbo.OilDeals", new[] { "OilTradeUnitId" });
            DropIndex("dbo.OilDeals", new[] { "OilDealDriverId" });
            DropIndex("dbo.OilDeals", new[] { "OilDealSelectorId" });
            DropIndex("dbo.OilDeals", new[] { "OilDealItemId" });
            DropIndex("dbo.OilDeals", new[] { "OilDealBrokerId" });
            DropIndex("dbo.Selectors", new[] { "GeneralAccountId" });
            DropIndex("dbo.ScheduleWeighBridges", new[] { "WeighBridgeId" });
            DropIndex("dbo.ScheduleWeighBridges", new[] { "DealScheduleId" });
            DropIndex("dbo.ScheduleLoadPackings", new[] { "DealPackingId" });
            DropIndex("dbo.ScheduleLoadPackings", new[] { "DealScheduleId" });
            DropIndex("dbo.PackingStocks", new[] { "DealPackingId" });
            DropIndex("dbo.PackingStocks", new[] { "GoodCompanyId" });
            DropIndex("dbo.DealItems", new[] { "GeneralAccountId" });
            DropIndex("dbo.Companies", new[] { "GeneralAccountId" });
            DropIndex("dbo.OmeDeals", new[] { "Selector_Id" });
            DropIndex("dbo.OmeDeals", new[] { "PackingUnitId" });
            DropIndex("dbo.OmeDeals", new[] { "TradeUnitId" });
            DropIndex("dbo.OmeDeals", new[] { "DealPackingId" });
            DropIndex("dbo.OmeDeals", new[] { "DealItemId" });
            DropIndex("dbo.OmeDeals", new[] { "BrokerId" });
            DropIndex("dbo.OmeDeals", new[] { "CompanyId" });
            DropIndex("dbo.FactoryPackingStocks", new[] { "DealPackingId" });
            DropIndex("dbo.FactoryPackingStockIssueRecords", new[] { "DealPackingId" });
            DropIndex("dbo.DealPackingSuppliers", new[] { "GeneralAccountId" });
            DropIndex("dbo.FactoryPackingStockAddedRecords", new[] { "DealPackingSupplierId" });
            DropIndex("dbo.FactoryPackingStockAddedRecords", new[] { "DealPackingId" });
            DropIndex("dbo.DealPackings", new[] { "Account_Id" });
            DropIndex("dbo.PackingIssueReceiveRecords", new[] { "DealPackingId" });
            DropIndex("dbo.PackingIssueReceiveRecords", new[] { "GoodCompanyId" });
            DropIndex("dbo.GoodCompanies", new[] { "GeneralAccountId" });
            DropIndex("dbo.EmployeeSalaryEntryDeductions", new[] { "SalaryDeductionId" });
            DropIndex("dbo.EmployeeSalaryEntryDeductions", new[] { "EmployPayRollEntryId" });
            DropIndex("dbo.EmployeeSalaryEntryAllowances", new[] { "SalaryAllowanceId" });
            DropIndex("dbo.EmployeeSalaryEntryAllowances", new[] { "EmployPayRollEntryId" });
            DropIndex("dbo.EmployPayRollEntries", new[] { "EmployeeId" });
            DropIndex("dbo.CreditEntries", new[] { "EmployeeId" });
            DropIndex("dbo.AttendanceRecords", new[] { "Employee_Id" });
            DropIndex("dbo.Employees", new[] { "GeneralAccountId" });
            DropIndex("dbo.ScheduleAlarms", new[] { "Id" });
            DropIndex("dbo.DealSchedules", new[] { "AppDealId" });
            DropIndex("dbo.DealSchedules", new[] { "OmeDealId" });
            DropIndex("dbo.DealSchedules", new[] { "GoodCompanyId" });
            DropIndex("dbo.DealSchedules", new[] { "EmployeeId" });
            DropIndex("dbo.DealSchedules", new[] { "SelectorId" });
            DropIndex("dbo.DealSchedules", new[] { "VehicleId" });
            DropIndex("dbo.DealSchedules", new[] { "DriverId" });
            DropIndex("dbo.DayBooks", new[] { "CancelSaleOrder_Id" });
            DropIndex("dbo.DayBooks", new[] { "ArchiveSaleOrder_Id" });
            DropIndex("dbo.DayBooks", new[] { "ReadyScheduleId" });
            DropIndex("dbo.DayBooks", new[] { "OilDealId" });
            DropIndex("dbo.DayBooks", new[] { "OilDirtScheduleId" });
            DropIndex("dbo.DayBooks", new[] { "DealScheduleId" });
            DropIndex("dbo.DayBooks", new[] { "SaleOrderId" });
            DropIndex("dbo.SaleOrders", new[] { "AppUserId" });
            DropIndex("dbo.SaleOrders", new[] { "CustomerId" });
            DropIndex("dbo.Customers", new[] { "GeneralAccountId" });
            DropIndex("dbo.Customers", new[] { "CustomerCategoryId" });
            DropIndex("dbo.ArchiveSaleOrders", new[] { "AppUserId" });
            DropIndex("dbo.ArchiveSaleOrders", new[] { "CustomerId" });
            DropIndex("dbo.AppDeals", new[] { "AppUserId" });
            DropIndex("dbo.AppDeals", new[] { "RawBrokerShareTypeId" });
            DropIndex("dbo.AppDeals", new[] { "PackingUnitId" });
            DropIndex("dbo.AppDeals", new[] { "TradeUnitId" });
            DropIndex("dbo.AppDeals", new[] { "DealPackingId" });
            DropIndex("dbo.AppDeals", new[] { "DealItemId" });
            DropIndex("dbo.AppDeals", new[] { "BrokerId" });
            DropIndex("dbo.AppDeals", new[] { "CompanyId" });
            DropIndex("dbo.Brokers", new[] { "GeneralAccountId" });
            DropIndex("dbo.AccountTransactions", new[] { "GeneralAccountId" });
            DropIndex("dbo.AccountBases", new[] { "HeadAccountId" });
            DropIndex("dbo.AccountBases", new[] { "TopHeadAccountId" });
            DropIndex("dbo.AccountBases", new[] { "ParentAccountId" });
            DropIndex("dbo.AccountBases", new[] { "SubHeadAccountId" });
            DropTable("dbo.SmsConfigObjs");
            DropTable("dbo.SmsAlertSettings");
            DropTable("dbo.ReturnRecords");
            DropTable("dbo.RetailBardanaSuppliers");
            DropTable("dbo.RetailBardanaItems");
            DropTable("dbo.RetailBardanaItemEntries");
            DropTable("dbo.ReadyScheduleAlarms");
            DropTable("dbo.ProductEntryRecords");
            DropTable("dbo.OilDirtAlarms");
            DropTable("dbo.OilDealAlarms");
            DropTable("dbo.MiscTransactions");
            DropTable("dbo.LongTermAssetItems");
            DropTable("dbo.KeyInfoes");
            DropTable("dbo.ItemPlaceSKUs");
            DropTable("dbo.GeneralReceipts");
            DropTable("dbo.ExpenseItems");
            DropTable("dbo.ExpenseItemEntries");
            DropTable("dbo.EntZeroItemConsumptions");
            DropTable("dbo.EntPurchases");
            DropTable("dbo.EntItems");
            DropTable("dbo.EntItemEntries");
            DropTable("dbo.EFileImages");
            DropTable("dbo.EFiles");
            DropTable("dbo.EFCategories");
            DropTable("dbo.DEEntries");
            DropTable("dbo.DealSegments");
            DropTable("dbo.AppSettings");
            DropTable("dbo.SmsSubscribers");
            DropTable("dbo.ShopMessages");
            DropTable("dbo.AppMessageStates");
            DropTable("dbo.Rahzams");
            DropTable("dbo.StoreLocations");
            DropTable("dbo.RepItemPreAddRecords");
            DropTable("dbo.RepItemConsumptionRecords");
            DropTable("dbo.RepairReceiveRecords");
            DropTable("dbo.RepPlaces");
            DropTable("dbo.RepairDispatchRecords");
            DropTable("dbo.RepEntries");
            DropTable("dbo.Locations");
            DropTable("dbo.ItemSuppliers");
            DropTable("dbo.PurchaseInvoiceRecords");
            DropTable("dbo.ItemPurchaseEntries");
            DropTable("dbo.RepItemDamageRecords");
            DropTable("dbo.ItemCategories");
            DropTable("dbo.RepItems");
            DropTable("dbo.AdvanceItemRecords");
            DropTable("dbo.AccruedExpenseItems");
            DropTable("dbo.RawBrokerShareTypes");
            DropTable("dbo.AppUserOptions");
            DropTable("dbo.CancelOrderLines");
            DropTable("dbo.CancelSaleOrders");
            DropTable("dbo.ArchiveOrderLines");
            DropTable("dbo.SIEntries");
            DropTable("dbo.StockItems");
            DropTable("dbo.ProductSizes");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Products");
            DropTable("dbo.SaleOrderLines");
            DropTable("dbo.ReadySelectors");
            DropTable("dbo.ReadyTradeUnits");
            DropTable("dbo.ReadyItems");
            DropTable("dbo.ReadyBrokers");
            DropTable("dbo.ReadyDeals");
            DropTable("dbo.ReadyDrivers");
            DropTable("dbo.BharthiTypes");
            DropTable("dbo.Bharthis");
            DropTable("dbo.ReadySchedules");
            DropTable("dbo.OilDirtSelectors");
            DropTable("dbo.OilDirtDrivers");
            DropTable("dbo.OilDirtTradeUnits");
            DropTable("dbo.OilDirtItems");
            DropTable("dbo.OilDirtBrokers");
            DropTable("dbo.OilDirtDeals");
            DropTable("dbo.OilDirtSchedules");
            DropTable("dbo.OilTradeUnits");
            DropTable("dbo.OilDealSelectors");
            DropTable("dbo.OilDealItems");
            DropTable("dbo.OilDealDrivers");
            DropTable("dbo.OilDealBrokers");
            DropTable("dbo.OilDeals");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Selectors");
            DropTable("dbo.WeighBridges");
            DropTable("dbo.ScheduleWeighBridges");
            DropTable("dbo.ScheduleLoadPackings");
            DropTable("dbo.PackingStocks");
            DropTable("dbo.TradeUnits");
            DropTable("dbo.PackingUnits");
            DropTable("dbo.DealItems");
            DropTable("dbo.Companies");
            DropTable("dbo.OmeDeals");
            DropTable("dbo.FactoryPackingStocks");
            DropTable("dbo.FactoryPackingStockIssueRecords");
            DropTable("dbo.DealPackingSuppliers");
            DropTable("dbo.FactoryPackingStockAddedRecords");
            DropTable("dbo.DealPackings");
            DropTable("dbo.PackingIssueReceiveRecords");
            DropTable("dbo.GoodCompanies");
            DropTable("dbo.SalaryDeductions");
            DropTable("dbo.EmployeeSalaryEntryDeductions");
            DropTable("dbo.SalaryAllowances");
            DropTable("dbo.EmployeeSalaryEntryAllowances");
            DropTable("dbo.EmployPayRollEntries");
            DropTable("dbo.CreditEntries");
            DropTable("dbo.AttendanceRecords");
            DropTable("dbo.Employees");
            DropTable("dbo.Drivers");
            DropTable("dbo.ScheduleAlarms");
            DropTable("dbo.DealSchedules");
            DropTable("dbo.DayBooks");
            DropTable("dbo.SaleOrders");
            DropTable("dbo.CustomerCategories");
            DropTable("dbo.Customers");
            DropTable("dbo.ArchiveSaleOrders");
            DropTable("dbo.AppUsers");
            DropTable("dbo.AppDeals");
            DropTable("dbo.Brokers");
            DropTable("dbo.AccountTransactions");
            DropTable("dbo.AccountBases");
        }
    }
}
