using Khattana.Common;
using Model.Admin.Model;
using Model.Deal.Model;
using Model.OilDirtStuff.Model;
using Model.ReadyStuff.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Retail.Model;
using Model.Financials.Model;
using Model.CommonModel;
using Model.RetailBardanaManaged.Model;
using Khattana.Secrecy;
using Khattana.Model;
using WinFom.Common.Model;
using Model.Employees.Model;

namespace WinFom.Admin.Database
{
    public class DbSeed
    {
        /// <summary>
        /// DbSeed.Seed
        /// </summary>
        /// <param name="context"></param>
        public static void Seed(Context context)
        {
            AppUser user = new AppUser
            {
                Id = "admin1",
                Contact = "contact",
                Name = "admin 1",
                Options = "",
                IsActive = true,
                //Password = MsrCipher.Encrypt("admin8")
                Password = "admin8"
            };
            UserOps allTrueOps = new UserOps
            {
                canViewSaleOrderList = true,
                canViewSaleOrder = true,
                canReprintSaleInvoiceCustomerCopy = true,
                canCancelSaleInvoice = true,
                canViewAddEmployee = true,
                canViewEmployeeList = true,
                canViewEmployeePayRolls = true,
                canViewAddNewDeal = true,
                canViewDealList = true,
                canViewScheduleAlarmsList = true,
                canViewDashboard = true,
                canViewMaalRegister = true,
                canViewAddNewProductCategory = true,
                canViewAddNewProduct = true,
                canViewCatgoryList = true,
                UserId = "admin1",
                canViewProductList = true,
                canViewRetailBardana = true,
                canViewAppSettings = true,
                canViewCompanyList = true,
                canViewBrokerList = true,
                canViewSelectorList = true,
                canViewGoodCompaniesList = true,
                canViewDriverList = true,
                canViewPackingStockCompanies = true,
                canViewPackingIssRecRecords = true,
                canViewPackingStockFactory = true,
                canViewWeighBridgeList = true,
                canViewVehicleList = true,
                canViewAddWholesaleDeal = true,
                canViewWholeSaleDealList = true,
                canViewWholeSaleScheduleAlarmList = true,
                canViewWholeSaleDashboard = true,
                canViewWholesaleReports = true,
                canViewAddNewOilDeal = true,
                canViewOilDealList = true,
                canViewOilDealSchedules = true,
                canViewOilDealDashboard = true,
                canViewAddOilDirtDeal = true,
                canViewOilDirtDealList = true,
                canViewFinancials = true,
                canViewInventoryItems = true,
                canViewEFiles = true,
                canViewAppUsers = true,
                canAddAppUser = true,
                canViewCustomersList = true,
                canViewGeneralReceipts = true,
                canViewRefreshmentReceipts = true,
                CanDoReverseFinancialEntries = true,
                canViewRetailTrialBalanceReport = true,
                canViewOilCakePurchasesReport = true,
                canViewCrudeOilSaleReports = true,
                canViewOilDirtSaleReports = true
            };
            UserOps foradmin24 = new UserOps
            {
                canViewSaleOrderList = true,
                canViewSaleOrder = true,
                canReprintSaleInvoiceCustomerCopy = true,
                canCancelSaleInvoice = true,
                canViewAddEmployee = true,
                canViewEmployeeList = true,
                canViewEmployeePayRolls = true,
                canViewAddNewDeal = true,
                canViewDealList = true,
                canViewScheduleAlarmsList = true,
                canViewDashboard = true,
                canViewMaalRegister = true,
                canViewAddNewProductCategory = true,
                canViewAddNewProduct = true,
                canViewCatgoryList = true,
                UserId = "admin24",
                canViewProductList = true,
                canViewRetailBardana = true,
                canViewAppSettings = true,
                canViewCompanyList = true,
                canViewBrokerList = true,
                canViewSelectorList = true,
                canViewGoodCompaniesList = true,
                canViewDriverList = true,
                canViewPackingStockCompanies = true,
                canViewPackingIssRecRecords = true,
                canViewPackingStockFactory = true,
                canViewWeighBridgeList = true,
                canViewVehicleList = true,
                canViewAddWholesaleDeal = true,
                canViewWholeSaleDealList = true,
                canViewWholeSaleScheduleAlarmList = true,
                canViewWholeSaleDashboard = true,
                canViewWholesaleReports = true,
                canViewAddNewOilDeal = true,
                canViewOilDealList = true,
                canViewOilDealSchedules = true,
                canViewOilDealDashboard = true,
                canViewAddOilDirtDeal = true,
                canViewOilDirtDealList = true,
                canViewFinancials = true,
                canViewInventoryItems = true,
                canViewEFiles = true,
                canAddAppUser = true,
                canViewAppUsers = true,
                canViewCustomersList = true,
                canViewGeneralReceipts = true,
                canViewRefreshmentReceipts = true,
                CanDoReverseFinancialEntries = true,
                canViewRetailTrialBalanceReport = true,
                canViewOilCakePurchasesReport = true,
                canViewCrudeOilSaleReports = true,
                canViewOilDirtSaleReports = true
            };
            AppUser user2 = new AppUser
            {
                Id = "admin24",
                Contact = "0333-9372084",
                Name = "Shafiq Hussain",
                Options = "",
                IsActive = true,
                Password = MsrCipher.Encrypt("JaneRehmat@24")
            };



            string ops = Gujjar.ToXML<UserOps>(allTrueOps);
            string opse = MsrCipher.Encrypt(ops);

            string ops24 = Gujjar.ToXML<UserOps>(foradmin24);
            string opse24 = MsrCipher.Encrypt(ops24);
            user.Options = opse;
            user2.Options = opse24;

            
            var dbUser = context.AppUsers.FirstOrDefault(a => a.Id == user.Id);
            if(dbUser == null)
            {
                context.AppUsers.AddOrUpdate(user);
            }
            context.AppUsers.AddOrUpdate(user2);




            #region Retail
            ProductCategory prodCategory = new ProductCategory
            {
                Id = 1,
                Products = null,
                Title = "Oil Cake"
            };

            context.ProductCategories.AddOrUpdate(prodCategory);
            #endregion

            RawBrokerShareType rbst1 = new RawBrokerShareType
            {
                Id = 1,
                Title = "Per Packing"
            };
            RawBrokerShareType rbst2 = new RawBrokerShareType
            {
                Id = 2,
                Title = "Percetange"
            };
            RawBrokerShareType rbst3 = new RawBrokerShareType
            {
                Id = 3,
                Title = "None"
            };

            context.RawBrokerShares.AddOrUpdate(rbst1);
            context.RawBrokerShares.AddOrUpdate(rbst2);
            context.RawBrokerShares.AddOrUpdate(rbst3);

            Broker broker = new Broker
            {
                Name = "N/A",
                IsActive = true,
                Address = "N/A",
                Remarks = "N/A",
                Extra = "N/A",
                Contact = "N/A",
                DateAdded = DateTime.Now
            };
            var dbBroker = context.Brokers.FirstOrDefault(a => a.Name == broker.Name);
            if (dbBroker == null)
            {
                context.Brokers.Add(broker);
            }

            Company comp = new Company
            {
                Name = "N/A",
                IsActive = true,
                Address = "N/A",
                Remarks = "N/A",
                Extra = "N/A",
                Contact = "N/A",
                DateAdded = DateTime.Now
            };
            var dbComp = context.Companies.FirstOrDefault(a => a.Name == comp.Name);
            if (dbComp == null)
            {
                context.Companies.Add(comp);
            }

            DealPacking packing = new DealPacking
            {
                Name = "N/A"
            };

            var dbPacking = context.DealPackings.FirstOrDefault(a => a.Name == packing.Name);
            if (dbPacking == null)
            {
                context.DealPackings.Add(packing);
            }

            DealItem dealItem = new DealItem
            {
                Name = "N/A"
            };
            var dbDealItem = context.DealItems.FirstOrDefault(a => a.Name == "N/A");
            if (dbDealItem == null)
            {
                context.DealItems.Add(dealItem);
            }
            TradeUnit tradeUnit = new TradeUnit
            {
                Id = 1,
                Title = "N/A",

                Qty = 0
            };
            context.TradeUnits.AddOrUpdate(tradeUnit);

            WeighBridge loadingWeighB = new WeighBridge
            {
                Id = 1,
                Address = "N/A",
                Name = "N/A",
                Phone = "N/A",
                WeighBrideType = WeighBridgeType.Loading
            };
            WeighBridge recWeighB = new WeighBridge
            {
                Id = 2,
                Address = "N/A",
                WeighBrideType = WeighBridgeType.Receiving,
                Phone = "N/A",
                Name = "N/A"
            };
            context.WeighBridges.AddOrUpdate(new[] { loadingWeighB, recWeighB });

            AppSettings appSettings = new AppSettings
            {
                Id = Properties.Settings.Default.AppSettings,
                Address = "Address Here",
                DaysAlertBeforeReady = 1,
                MainPrinter = "Microsoft Print to PDF",
                Name = "Name Here",
                PhoneNo = "Phone Number Here",
                ThermalPrinter = "Black Copper BC-85AC",
                Logo = Gujjar.GetByteArrayFromImage(Properties.Resources.factorylogo),
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.AddYears(1).Date,
                AllowToChangeMailBrokerSharePercentage = false,
                AllowToChangeOilBrokerSharePercentage = false,
                AllowToChargeOilCakeBrokerSharePercentage = false,
                MailBrokerSharePercentage = 0.20m,
                OilBrokerSharePercentage = 0.30m,
                OilCakeBrokerSharePercentage = 0.85m,
                EnableDiscounts = false,
                OfferDiscountPercentage = 0,
                SalesTaxPercentage = 0,
                ServiceCharges = 0,
                SecurityPassword = "abc",
                MasterPassword = "secret",
                dt1 = DateTime.Now,
                dt2 = DateTime.Now,
                dt3 = DateTime.Now,
                dt4 = DateTime.Now,
                GatePassCopies = 1,
                FloorScaleCopies = 1,
                CustomerCopies = 1,
                OfficeCopies = 1,
                AddressUrdu = "فیکڑی کا مکمل پتا",
                NameUrdu = "فیکڑی کا نام",
                bool1 = true
            };

            ReadyBroker readyBrokerNA = new ReadyBroker
            {
                Id = 1,
                Address = "N/A",
                Contact = "N/A",
                DateAdded = DateTime.Now,
                Extra = "N/A",
                IsActive = true,
                Deals = null,
                Name = "N/A",
                Remarks = "N/A"
            };
            context.ReadyBrokers.AddOrUpdate(readyBrokerNA);

            var sett = context.AppSettings.FirstOrDefault(a => a.Id == Properties.Settings.Default.AppSettings);
            if (sett == null)
            {
                context.AppSettings.AddOrUpdate(appSettings);
            }


            ReadyItem readyItem = new ReadyItem
            {
                Id = 1,
                StockQty = 0,
                Title = "N/A"
            };
            context.ReadyItems.AddOrUpdate(readyItem);

            ReadyDriver driver2 = new ReadyDriver
            {
                Address = "N/A",
                CNIC = "N/A",
                Contact = "N/A",
                DateAdded = DateTime.Now,
                Extra = "N/A",
                IsActive = true,
                Id = 1,
                IsAvailable = true,
                Name = "N/A",
                Remarks = "N/A"
            };
            driver2.PicData = Gujjar.GetByteArrayFromImage(Properties.Resources.atomix_user31);
            driver2.ThumbPicData = Gujjar.GetByteArrayFromImage(Properties.Resources.Fingerprint_96px);
            context.ReadyDrivers.AddOrUpdate(driver2);

            ReadySelector selector2 = new ReadySelector
            {
                Address = "N/A",
                CNIC = "N/A",
                Contact = "N/A",
                DateAdded = DateTime.Now,
                Extra = "N/A",
                IsActive = true,
                Id = 1,
                IsAvailable = true,
                Name = "N/A",
                Remarks = "N/A"
            };
            selector2.PicData = Gujjar.GetByteArrayFromImage(Properties.Resources.atomix_user31);
            selector2.ThumbPicData = Gujjar.GetByteArrayFromImage(Properties.Resources.Fingerprint_96px);
            context.ReadySelectors.AddOrUpdate(selector2);

            OilDealSelector oilDealSelector = new OilDealSelector
            {
                Id = 1,
                Address = "N/A",
                CNIC = "N/A",
                Contact = "N/A",
                DateAdded = DateTime.Now,
                Deals = null,
                Extra = "N/A",
                IsActive = true,
                IsAvailable = true,
                Name = "N/A",
                PicData = null,
                Remarks = "N/A",
                ThumbData = null,
                ThumbPicData = null
            };
            context.OilDealSelectors.AddOrUpdate(oilDealSelector);

            OilDealDriver oilDealDriver = new OilDealDriver
            {
                Id = 1,
                Address = "N/A",
                CNIC = "N/A",
                Contact = "N/A",
                DateAdded = DateTime.Now,
                Deals = null,
                Extra = "N/A",
                IsActive = true,
                IsAvailable = true,
                Name = "N/A",
                PicData = null,
                Remarks = "N/A",
                ThumbData = null,
                ThumbPicData = null
            };
            context.OilDealDrivers.AddOrUpdate(oilDealDriver);

            ReadyTradeUnit readyTradeUnit = new ReadyTradeUnit
            {
                Id = 1,
                Title = "N/A",
                UnitQty = 1
            };
            context.ReadyTradeUnits.AddOrUpdate(readyTradeUnit);

            OilTradeUnit oilTradeUnit = new OilTradeUnit
            {
                Id = 1,
                Title = "N/A",
                UnitQty = 1
            };
            context.OilTradeUnits.AddOrUpdate(oilTradeUnit);

            OilDirtTradeUnit oilDirtTradeUnit = new OilDirtTradeUnit
            {
                Deals = null,
                Id = 1,
                Title = "N/A",
                UnitQty = 1
            };
            context.OilDirtTradeUnits.AddOrUpdate(oilDirtTradeUnit);

            OilDirtSelector oilDirtSelector = new OilDirtSelector
            {
                Id = 1,
                Address = "N/A",
                CNIC = "N/A",
                Contact = "N/A",
                DateAdded = DateTime.Now,
                Schedules = null,
                Extra = "N/A",
                IsActive = true,
                IsAvailable = true,
                Name = "N/A",
                PicData = null,
                Remarks = "N/A",
                ThumbData = null,
                ThumbPicData = null
            };
            context.OilDirtSelectors.AddOrUpdate(oilDirtSelector);

            OilDirtDriver oilDirtDriver = new OilDirtDriver
            {
                Id = 1,
                Address = "N/A",
                CNIC = "N/A",
                Contact = "N/A",
                DateAdded = DateTime.Now,
                Schedules = null,
                Extra = "N/A",
                IsActive = true,
                IsAvailable = true,
                Name = "N/A",
                PicData = null,
                Remarks = "N/A",
                ThumbData = null,
                ThumbPicData = null
            };
            context.OilDirtDrivers.AddOrUpdate(oilDirtDriver);

            SalaryAllowance salaryAllowance = new SalaryAllowance
            {
                Id = 1,
                Amount = 0,
                Title = "N/A"
            };
            context.SalaryAllowances.Add(salaryAllowance);

            SalaryDeduction salaryDeduction = new SalaryDeduction
            {
                Id = 1,
                Title = "N/A",
                Amount = 0,

            };
            context.SalaryDeductions.Add(salaryDeduction);
            context.SaveChanges();

            #region "Retail Model"
            CustomerCategory customerCategory = new CustomerCategory
            {
                Id = 1,
                Discount = 0,
                Customers = null,
                Title = "Zero Category"
            };
            context.CustomerCategories.AddOrUpdate(customerCategory);
            context.SaveChanges();

            Customer customer = new Customer
            {
                Id = 1,
                Address = "N/A",
                CNIC = "N/A",
                Contact = "N/A",
                CustomerCategory = null,
                CustomerCategoryId = 1,
                DateAdded = DateTime.Now,
                IsActive = true,
                Name = "Walk In",
                Remarks = "N/A",
                SaleOrders = null,
                ApplyCardDiscount = false,
                CardEndDate = DateTime.Now,
                CardNo = "N/A",
                CardStartDate = DateTime.Now,
                DiscountPercentage = 0,
                Points = 0
            };

            context.Customers.AddOrUpdate(customer);

            ProductSize productSize = new ProductSize
            {
                Id = 1,
                Products = null,
                Title = "N/A"
            };
            context.ProductSizes.AddOrUpdate(productSize);
            context.SaveChanges();
            #endregion

            #region "Financial Accounting"

            #region "Asset accounts"
            HeadAccount asset = new HeadAccount
            {
                Id = Properties.Resources.AssetsHead,
                Description = "Assets",
                TopHeadAccounts = null,
                Title = "Asset Accounts",
                ExplicitilyCreated = true,
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Address = "N/A"
            };
            context.Accounts.AddOrUpdate(asset);
            context.SaveChanges();

            TopHeadAccount fixedAsset = new TopHeadAccount
            {
                Id = Properties.Resources.FixedAssets,
                Description = "Fixed Assets Accounts",
                Title = "Fixed Assets Accounts",
                SubHeadAccounts = null,
                HeadAccountId = Properties.Resources.AssetsHead,
                AccountNature = AccountNature.Debit,
                ExplicitilyCreated = true,
                AccountNo = "N/A",
                Address = "N/A",
                HeadAccount = null
            };
            context.Accounts.AddOrUpdate(fixedAsset);
            TopHeadAccount longTermAssetsTopHead = new TopHeadAccount
            {
                Id = Properties.Resources.LongTermAssetsTopHead,
                Description = "Long Term Assets Accounts",
                Title = "Long Term Assets Accounts",
                ExplicitilyCreated = true,

                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Address = "N/A",
                HeadAccount = null,
                HeadAccountId = Properties.Resources.AssetsHead,
                SubHeadAccounts = null
            };
            context.Accounts.AddOrUpdate(longTermAssetsTopHead);
            context.SaveChanges();

            SubHeadAccount longTermAssetsSubHead = new SubHeadAccount
            {
                Id = Properties.Resources.LongTermAssetsSubHead,
                Title = "Long term assets items sub head",
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Accounts = null,

                Address = "N/A",
                Description = "Long term assets items sub head",
                TopHeadAccount = null,
                ExplicitilyCreated = true,
                TopHeadAccountId = longTermAssetsTopHead.Id
            };
            context.Accounts.AddOrUpdate(longTermAssetsSubHead);

            TopHeadAccount currentAssets = new TopHeadAccount
            {
                Id = Properties.Resources.CurrentAssets,
                Description = "Current Assets Accounts",
                Title = "Current Assets Accounts",
                AccountNature = AccountNature.Debit,

                ExplicitilyCreated = true,
                AccountNo = "N/A",
                Address = "N/A",
                HeadAccount = null,
                HeadAccountId = Properties.Resources.AssetsHead,
                SubHeadAccounts = null
            };
            context.Accounts.AddOrUpdate(currentAssets);
            context.SaveChanges();

            SubHeadAccount debitors = new SubHeadAccount
            {
                Id = Properties.Resources.Debtors,
                Title = "Debitors (Receivables)",
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Accounts = null,

                Address = "N/A",
                Description = "Debitors (Receiveable Accounts)",
                TopHeadAccount = null,
                ExplicitilyCreated = true,
                TopHeadAccountId = currentAssets.Id
            };
            context.Accounts.AddOrUpdate(debitors);
            context.SaveChanges();
            SubHeadAccount cashAccount = new SubHeadAccount
            {
                Id = Properties.Resources.CashAccountSubHead,
                Title = "Cash Account Sub Head",
                Description = "Cash Account Sub Head",
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Accounts = null,

                Address = "N/A",
                ExplicitilyCreated = true,
                TopHeadAccount = null,
                TopHeadAccountId = currentAssets.Id
            };
            context.Accounts.AddOrUpdate(cashAccount);
            context.SaveChanges();



            SubHeadAccount banks = new SubHeadAccount
            {
                Id = Properties.Resources.Banks,
                Title = "Bank Accounts",
                Description = "Bank Accounts",
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Accounts = null,
                Address = "N/A",
                ExplicitilyCreated = true,
                TopHeadAccount = null,
                TopHeadAccountId = currentAssets.Id
            };
            context.Accounts.AddOrUpdate(banks);

            SubHeadAccount stockPurchases = new SubHeadAccount
            {
                Id = Properties.Resources.StockPurchases,
                Title = "Stock Purchases (asset) Accounts",
                Description = "Stock Purchases (asset) Accounts",
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Accounts = null,
                Address = "N/A",
                ExplicitilyCreated = true,
                TopHeadAccount = null,
                TopHeadAccountId = currentAssets.Id,

            };
            context.Accounts.AddOrUpdate(stockPurchases);
            GeneralAccount cashInHand = new GeneralAccount
            {
                Id = Properties.Resources.CashInHand,
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Balance = 0,
                Address = "N/A",
                ExplicitilyCreated = true,
                SubHeadAccountId = cashAccount.Id,
                Description = "Cash in hand account",
                Title = "Cash in hand Account",
                SubHeadAccount = null,
                CrDrType = CrDrType.General
            };
            context.Accounts.AddOrUpdate(cashInHand);

            GeneralAccount itemPurchasedAccount = new GeneralAccount
            {
                Id = Properties.Resources.ItemsPurchasedAccount,
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Balance = 0,
                Address = "N/A",
                ExplicitilyCreated = true,
                SubHeadAccountId = stockPurchases.Id,
                Description = "Asset Items Account",
                Title = "Asset Items Purchased Account",
                SubHeadAccount = null,
                CrDrType = CrDrType.General
            };
            context.Accounts.AddOrUpdate(itemPurchasedAccount);
            context.SaveChanges();
            #endregion

            #region "Liability accounts"
            HeadAccount liability = new HeadAccount
            {
                Id = Properties.Resources.LiabilitiesHead,
                Description = "Liability accounts",
                Title = "Liability accounts",

                AccountNature = AccountNature.Credit,
                ExplicitilyCreated = true,
                AccountNo = "N/A",
                Address = "N/A",
                TopHeadAccounts = null
            };
            context.Accounts.AddOrUpdate(liability);
            context.SaveChanges();

            TopHeadAccount capitalAccountTopHead = new TopHeadAccount
            {
                Id = Properties.Resources.CapitalAccountTopHead,
                Description = "Capital Accounts",
                Title = "Capital Accounts",
                AccountNature = AccountNature.Credit,
                ExplicitilyCreated = true,
                AccountNo = "N/A",
                Address = "N/A",
                HeadAccount = null,
                SubHeadAccounts = null,
                HeadAccountId = Properties.Resources.LiabilitiesHead
            };
            context.Accounts.AddOrUpdate(capitalAccountTopHead);
            context.SaveChanges();

            SubHeadAccount capitalSubHead = new SubHeadAccount
            {
                Id = Properties.Resources.CapitalAccountSubHead,
                AccountNature = AccountNature.Credit,
                AccountNo = "N/A",
                Title = "Capital Sub Head",
                Description = "Capital Sub Head",
                ExplicitilyCreated = true,
                TopHeadAccountId = Properties.Resources.CapitalAccountTopHead,
                Accounts = null,
                Address = "N/A",
                TopHeadAccount = null
            };
            context.Accounts.AddOrUpdate(capitalSubHead);
            context.SaveChanges();



            TopHeadAccount longTermLiability = new TopHeadAccount
            {
                Id = Properties.Resources.LongTermLiabilities,
                Description = "Long term liabilities",
                Title = "Long term liabilities",
                HeadAccount = null,
                SubHeadAccounts = null,
                HeadAccountId = Properties.Resources.LiabilitiesHead,
                AccountNature = AccountNature.Credit,
                ExplicitilyCreated = true,
                AccountNo = "N/A",
                Address = "N/A",
            };
            context.Accounts.AddOrUpdate(longTermLiability);

            TopHeadAccount currentLiabililty = new TopHeadAccount
            {
                Id = Properties.Resources.CurrentLiabilities,
                Description = "Current liabilities",
                Title = "Current liabilities",
                AccountNature = AccountNature.Credit,

                ExplicitilyCreated = true,
                AccountNo = "N/A",
                Address = "N/A",
                HeadAccount = null,
                SubHeadAccounts = null,
                HeadAccountId = Properties.Resources.LiabilitiesHead
            };
            context.Accounts.AddOrUpdate(currentLiabililty);
            context.SaveChanges();

            SubHeadAccount creditors = new SubHeadAccount
            {
                Id = Properties.Resources.Creditors,
                Title = "Creditors",
                AccountNature = AccountNature.Credit,
                AccountNo = "N/A",
                Accounts = null,

                Address = "N/A",
                Description = "Creditors (Payable Accounts)",
                TopHeadAccount = null,
                ExplicitilyCreated = true,
                TopHeadAccountId = Properties.Resources.CurrentLiabilities
            };
            context.Accounts.AddOrUpdate(creditors);

            SubHeadAccount zakatSubHead = new SubHeadAccount
            {
                Id = Properties.Resources.ZakatPayableSubHead,
                Title = "Zakat Sub Head",
                AccountNature = AccountNature.Credit,
                AccountNo = "N/A",
                Accounts = null,

                Address = "N/A",
                Description = "Zakat Sub Head",
                TopHeadAccount = null,
                ExplicitilyCreated = true,
                TopHeadAccountId = Properties.Resources.CurrentLiabilities
            };
            context.Accounts.AddOrUpdate(zakatSubHead);

            SubHeadAccount repairExpensesPayableSubHead = new SubHeadAccount
            {
                Id = Properties.Resources.RepairExpensesPayableSubHead,
                Title = "Repair Expense Payable Accounts",
                AccountNature = AccountNature.Credit,
                AccountNo = "N/A",
                Accounts = null,

                Address = "N/A",
                Description = "Repair Expense Payable Accounts",
                TopHeadAccount = null,
                ExplicitilyCreated = true,
                TopHeadAccountId = Properties.Resources.CurrentLiabilities
            };
            context.Accounts.AddOrUpdate(repairExpensesPayableSubHead);

            SubHeadAccount accruedPayablesSubHead = new SubHeadAccount
            {
                Id = Properties.Resources.AccruedPayablesSubHead,
                Title = "Accrued Payable Expenses",
                AccountNature = AccountNature.Credit,
                AccountNo = "N/A",
                Accounts = null,
                Address = "N/A",
                Description = "Accrued Expenses (Payable Accounts)",
                TopHeadAccount = null,
                ExplicitilyCreated = true,
                TopHeadAccountId = Properties.Resources.CurrentLiabilities
            };
            context.Accounts.AddOrUpdate(accruedPayablesSubHead);
            SubHeadAccount employees = new SubHeadAccount
            {
                Id = Properties.Resources.Employees,
                Title = "Employees Accounts",
                Description = "Employees Accounts",
                AccountNature = AccountNature.Credit,
                AccountNo = "N/A",
                Accounts = null,
                Address = "N/A",
                ExplicitilyCreated = true,
                TopHeadAccount = null,
                TopHeadAccountId = currentLiabililty.Id
            };
            context.Accounts.AddOrUpdate(employees);
            SubHeadAccount openingBalanceSubHead = new SubHeadAccount
            {
                Id = Properties.Resources.OpeningBalanceSubHead,
                Title = "Opening Balance Sub head",
                AccountNature = AccountNature.Credit,
                AccountNo = "N/A",
                Accounts = null,
                Address = "N/A",
                Description = "Opening Balance Sub head",
                TopHeadAccount = null,
                ExplicitilyCreated = true,
                TopHeadAccountId = Properties.Resources.CurrentLiabilities
            };
            context.Accounts.AddOrUpdate(openingBalanceSubHead);
            context.SaveChanges();

            SubHeadAccount laborPayableExpenses = new SubHeadAccount
            {
                Id = Properties.Resources.LaborExpensePayableSubHead,
                Title = "Labor expenses payables",
                AccountNature = AccountNature.Credit,
                AccountNo = "N/A",
                Accounts = null,
                Address = "N/A",
                Description = "Labor expenses payables",
                TopHeadAccount = null,
                ExplicitilyCreated = true,
                TopHeadAccountId = Properties.Resources.CurrentLiabilities
            };
            context.Accounts.AddOrUpdate(laborPayableExpenses);
            context.SaveChanges();

            GeneralAccount oilCakeRetailExpensePayableAccount = new GeneralAccount
            {
                Id = Properties.Resources.OilcakeLaborExpensePayableAccount,
                Title = "Oil Cake (retail) labor payable account",
                Description = "Oil Cake (retail) labor payable account",
                SubHeadAccountId = Properties.Resources.LaborExpensePayableSubHead,
                AccountNature = AccountNature.Credit,
                Address = "N/A",
                ExplicitilyCreated = true,
                Balance = 0,
                CrDrType = CrDrType.General
            };
            context.Accounts.AddOrUpdate(oilCakeRetailExpensePayableAccount);

            GeneralAccount accruedCottonSeedLaborExpense = new GeneralAccount
            {
                Id = Properties.Resources.CottonSeedLaborExpensePayable,
                Title = "Cotton Seed (labor expense) payable account",
                Description = "Cotton seed (labor expense) payable account",
                SubHeadAccountId = Properties.Resources.LaborExpensePayableSubHead,
                AccountNature = AccountNature.Credit,
                Address = "N/A",
                ExplicitilyCreated = true,
                Balance = 0,
                CrDrType = CrDrType.General
            };
            context.Accounts.AddOrUpdate(accruedCottonSeedLaborExpense);

            GeneralAccount tracktorLaborPayableAccount = new GeneralAccount
            {
                Id = Properties.Resources.TracktorLaborPayableAccount,
                Title = "Tracktor Labor Payable Account",
                Description = "Tracktor Labor Payable Account",
                SubHeadAccountId = Properties.Resources.LaborExpensePayableSubHead,
                AccountNature = AccountNature.Credit,
                Address = "N/A",
                ExplicitilyCreated = true,
                Balance = 0,
                CrDrType = CrDrType.General
            };
            context.Accounts.AddOrUpdate(tracktorLaborPayableAccount);
            context.SaveChanges();

            GeneralAccount openingBalanceEquity = new GeneralAccount
            {
                Id = Properties.Resources.OpeningBalanceEquityAccount,
                Title = "Opening balance equity account",
                Description = "Opening balance equity account",
                SubHeadAccountId = Properties.Resources.OpeningBalanceSubHead,
                AccountNature = AccountNature.Credit,
                Address = "N/A",
                ExplicitilyCreated = true,
                Balance = 0,
                CrDrType = CrDrType.General
            };
            context.Accounts.AddOrUpdate(openingBalanceEquity);

            GeneralAccount zakatAccount = new GeneralAccount
            {
                Id = Properties.Resources.ZakatPayableAccount,
                Title = "Zakat Account",
                Description = "Zakat Account",
                SubHeadAccountId = Properties.Resources.ZakatPayableSubHead,
                AccountNature = AccountNature.Credit,
                Address = "N/A",
                ExplicitilyCreated = true,
                Balance = 0,
                CrDrType = CrDrType.General
            };
            context.Accounts.AddOrUpdate(zakatAccount);

            context.SaveChanges();
            #endregion

            #region "Expenses accounts"
            HeadAccount expenses = new HeadAccount
            {
                Id = Properties.Resources.ExpensesHead,
                Description = "Expense Accounts",
                Title = "Expense Accounts",

                AccountNature = AccountNature.Debit,
                ExplicitilyCreated = true,
                AccountNo = "N/A",
                Address = "N/A",
                TopHeadAccounts = null
            };
            context.Accounts.AddOrUpdate(expenses);
            context.SaveChanges();
            TopHeadAccount adminExpenses = new TopHeadAccount
            {
                Id = Properties.Resources.AdminExpenses,
                Description = "Administration expenses",
                Title = "Administrations expenses",
                AccountNature = AccountNature.Debit,

                ExplicitilyCreated = true,
                AccountNo = "N/A",
                Address = "N/A",
                HeadAccount = null,
                HeadAccountId = Properties.Resources.ExpensesHead,
                SubHeadAccounts = null
            };
            context.Accounts.AddOrUpdate(adminExpenses);

            TopHeadAccount purchaseItemsTopHead = new TopHeadAccount
            {
                Id = Properties.Resources.ItemPurchasedExpenseTopHead,
                Description = "Purchase Items top head",
                Title = "Purchase Items Top Head",
                AccountNature = AccountNature.Debit,

                ExplicitilyCreated = true,
                AccountNo = "N/A",
                Address = "N/A",
                HeadAccount = null,
                HeadAccountId = Properties.Resources.ExpensesHead,
                SubHeadAccounts = null
            };
            context.Accounts.AddOrUpdate(purchaseItemsTopHead);


            TopHeadAccount sellingExpenses = new TopHeadAccount
            {
                Id = Properties.Resources.SellingExpenses,
                Description = "Selling expenses",
                Title = "Selling expenses",
                AccountNature = AccountNature.Debit,

                ExplicitilyCreated = true,
                AccountNo = "N/A",
                Address = "N/A",
                HeadAccount = null,
                HeadAccountId = Properties.Resources.ExpensesHead,
                SubHeadAccounts = null
            };
            context.Accounts.AddOrUpdate(sellingExpenses);
            context.SaveChanges();

            SubHeadAccount repairExpenseSubHead = new SubHeadAccount
            {
                Id = Properties.Resources.RepairingExpenseSubHead,
                Title = "Repair Expense Subhead",
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Accounts = null,
                Address = "N/A",
                Description = "Repair Expense Subhead",
                TopHeadAccount = null,
                ExplicitilyCreated = true,
                TopHeadAccountId = sellingExpenses.Id
            };
            context.Accounts.AddOrUpdate(repairExpenseSubHead);
            SubHeadAccount itemPurchasedExpenseSubHead = new SubHeadAccount
            {
                Id = Properties.Resources.ItemPurchasedExpenseSubHead,
                Title = "Item Purchased Items Subhead",
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Accounts = null,
                Address = "N/A",
                Description = "Item Purchased Subhead",
                TopHeadAccount = null,
                ExplicitilyCreated = true,
                TopHeadAccountId = purchaseItemsTopHead.Id
            };
            context.Accounts.AddOrUpdate(itemPurchasedExpenseSubHead);
            context.SaveChanges();

            GeneralAccount repairExpenseAccount = new GeneralAccount
            {
                Id = Properties.Resources.RepairingExpenseAccount,
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Address = "N/A",
                Description = "Repair Expenses Account",
                SubHeadAccount = null,
                ExplicitilyCreated = true,
                SubHeadAccountId = repairExpenseSubHead.Id,
                Title = "Repair Expenses Account",
                Balance = 0,
                CrDrType = CrDrType.General
            };
            context.Accounts.AddOrUpdate(repairExpenseAccount);

            SubHeadAccount brokeryExpenseSubHead = new SubHeadAccount
            {
                Id = Properties.Resources.BrokeryExpenseSubHead,
                Title = "Brokery Expense Accounts",
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Accounts = null,
                Address = "N/A",
                Description = "Brokery Expense Accounts",
                TopHeadAccount = null,
                ExplicitilyCreated = true,
                TopHeadAccountId = sellingExpenses.Id
            };
            context.Accounts.AddOrUpdate(brokeryExpenseSubHead);

            SubHeadAccount assetsExpensesSubHead = new SubHeadAccount
            {
                Id = Properties.Resources.AssetsExpensesSubHead,
                Title = "Assets expenses sub head",
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Accounts = null,
                Address = "N/A",
                Description = "Assets expenses sub head",
                TopHeadAccount = null,
                ExplicitilyCreated = true,
                TopHeadAccountId = adminExpenses.Id
            };
            context.Accounts.AddOrUpdate(assetsExpensesSubHead);
            context.SaveChanges();

            GeneralAccount longTermAssetsExpenseAccount = new GeneralAccount
            {
                Id = Properties.Resources.LongTermAssetsExpensesAccount,
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Address = "N/A",
                Description = "Long term assets expense account",
                SubHeadAccount = null,
                ExplicitilyCreated = true,
                SubHeadAccountId = assetsExpensesSubHead.Id,
                Title = "Long term assets expense account",
                Balance = 0,
                CrDrType = CrDrType.General
            };
            context.Accounts.AddOrUpdate(longTermAssetsExpenseAccount);

            SubHeadAccount miscSellingExpenses = new SubHeadAccount
            {
                Id = Properties.Resources.MiscSellingExpenses,
                Title = "Misc Selling Expenses",
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Accounts = null,

                Address = "N/A",
                Description = "Misc Selling Expenses",
                TopHeadAccount = null,
                ExplicitilyCreated = true,
                TopHeadAccountId = sellingExpenses.Id
            };
            context.Accounts.AddOrUpdate(miscSellingExpenses);

            TopHeadAccount financialExpensesTopHead = new TopHeadAccount
            {
                Id = Properties.Resources.FinancialExpensesTopHead,
                AccountNature = AccountNature.Debit,

                ExplicitilyCreated = true,
                Description = "Financial Expenses",
                Title = "Financial Expenses",
                AccountNo = "N/A",
                Address = "N/A",
                HeadAccount = null,
                HeadAccountId = Properties.Resources.ExpensesHead,
                SubHeadAccounts = null
            };
            context.Accounts.AddOrUpdate(financialExpensesTopHead);
            //TopHeadAccount zakatPaidTopHead = new TopHeadAccount
            //{
            //    Id = Properties.Resources.ZakatPaidTopHead,
            //    AccountNature = AccountNature.Debit,

            //    ExplicitilyCreated = true,
            //    Description = "Zakat Top Head",
            //    Title = "Zakat Top Head",
            //    AccountNo = "N/A",
            //    Address = "N/A",
            //    HeadAccount = null,
            //    HeadAccountId = Properties.Resources.ExpensesHead,
            //    SubHeadAccounts = null
            //};
            //context.Accounts.AddOrUpdate(zakatPaidTopHead);
            //context.SaveChanges();

            //SubHeadAccount zakatPaidSubHead = new SubHeadAccount
            //{
            //    Id = Properties.Resources.ZakatPaidSubHead,
            //    Title = "Zakat Paid Sub Head",
            //    AccountNature = AccountNature.Debit,
            //    AccountNo = "N/A",
            //    Accounts = null,

            //    Address = "N/A",
            //    Description = "Zakat Paid Sub Head",
            //    TopHeadAccount = null,
            //    ExplicitilyCreated = true,
            //    TopHeadAccountId = zakatPaidTopHead.Id
            //};
            //context.Accounts.AddOrUpdate(zakatPaidSubHead);

            SubHeadAccount financialExpenseSubHead = new SubHeadAccount
            {
                Id = Properties.Resources.FinancialExpenseSubHead,
                Title = "Financial Expenses Sub Head",
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Accounts = null,

                Address = "N/A",
                Description = "Financial Expenses Sub Head",
                TopHeadAccount = null,
                ExplicitilyCreated = true,
                TopHeadAccountId = financialExpensesTopHead.Id
            };

            context.Accounts.AddOrUpdate(financialExpenseSubHead);

            SubHeadAccount salariesSubHead = new SubHeadAccount
            {
                Id = Properties.Resources.SalariesSubHead,
                Title = "Salaries",
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Accounts = null,

                Address = "N/A",
                Description = "Salaries",
                TopHeadAccount = null,
                ExplicitilyCreated = true,
                TopHeadAccountId = adminExpenses.Id
            };
            context.Accounts.AddOrUpdate(salariesSubHead);

            SubHeadAccount generalExpenseSubhead = new SubHeadAccount
            {
                Id = Properties.Resources.GeneralExpenseHead,
                Title = "General Expenses",
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Accounts = null,

                Address = "N/A",
                Description = "General Expenses",
                TopHeadAccount = null,
                ExplicitilyCreated = true,
                TopHeadAccountId = adminExpenses.Id
            };
            context.Accounts.AddOrUpdate(generalExpenseSubhead);
            context.SaveChanges();

            SubHeadAccount accruedExpensesSubhead = new SubHeadAccount
            {
                Id = Properties.Resources.AccruedExpensesSubHead,
                Title = "Accrued Expenses Sub Head",
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Accounts = null,

                Address = "N/A",
                Description = "Accrued Expenses Sub Head",
                TopHeadAccount = null,
                ExplicitilyCreated = true,
                TopHeadAccountId = adminExpenses.Id
            };
            context.Accounts.AddOrUpdate(accruedExpensesSubhead);
            context.SaveChanges();

            SubHeadAccount laborExpensesSubHead = new SubHeadAccount
            {
                Id = Properties.Resources.LaborExpenseSubHead,
                Title = "Labor Expenses Sub Head",
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Accounts = null,
                Address = "N/A",
                Description = "Labor Expenses Sub Head Accounts",
                TopHeadAccount = null,
                ExplicitilyCreated = true,
                TopHeadAccountId = sellingExpenses.Id
            };
            context.Accounts.AddOrUpdate(laborExpensesSubHead);
            context.SaveChanges();


            GeneralAccount cottonSeedReceivingLaborExpense = new GeneralAccount
            {
                Id = Properties.Resources.CottonSeedLaborExpenseAccount,
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Address = "N/A",
                Description = "Cotton Seed Receiving labor expenses",
                SubHeadAccount = null,
                ExplicitilyCreated = true,
                SubHeadAccountId = miscSellingExpenses.Id,
                Title = "Cotton Seed Receiving labor expense account",
                Balance = 0,
                CrDrType = CrDrType.General
            };
            context.Accounts.AddOrUpdate(cottonSeedReceivingLaborExpense);

            //GeneralAccount zakatPaidAccount = new GeneralAccount
            //{
            //    Id = Properties.Resources.ZakatPaidAccount,
            //    AccountNature = AccountNature.Debit,
            //    AccountNo = "N/A",
            //    Address = "N/A",
            //    Description = "Zakat Paid Account",
            //    SubHeadAccount = null,
            //    ExplicitilyCreated = true,
            //    SubHeadAccountId = zakatPaidSubHead.Id,
            //    Title = "Zakat Paid Account",
            //    Balance = 0
            //};
            //context.Accounts.AddOrUpdate(zakatPaidAccount);

            GeneralAccount sellingBardanaExpenseAccount = new GeneralAccount
            {
                Id = Properties.Resources.BardanaSellingExpenseAccount,
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Address = "N/A",
                Description = "Selling bardana expense account",
                SubHeadAccount = null,
                ExplicitilyCreated = true,
                SubHeadAccountId = miscSellingExpenses.Id,
                Title = "Selling bardana expense account",
                Balance = 0,
                CrDrType = CrDrType.General
            };
            context.Accounts.AddOrUpdate(sellingBardanaExpenseAccount);

            GeneralAccount oilCakeWholeSaleLaborExpenseAccount = new GeneralAccount
            {
                Id = Properties.Resources.OilCakeWholeSaleLaborExpenseAccount,
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",

                Address = "N/A",
                Description = "Oil Cake Wholesale Labor Expenses",
                SubHeadAccount = null,
                ExplicitilyCreated = true,
                SubHeadAccountId = laborExpensesSubHead.Id,
                Title = "Oil Cake Wholesale Labor Expense Account",
                Balance = 0,
                CrDrType = CrDrType.General
            };
            //context.Accounts.AddOrUpdate(oilCakeWholeSaleLaborExpenseAccount);

            GeneralAccount crudeOilWholeSaleLaborExpenseAccount = new GeneralAccount
            {
                Id = Properties.Resources.CrudeOilWholeSaleLaborExpenseAccount,
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Address = "N/A",
                Description = "Crude Oil Wholesale Labor Expenses",
                SubHeadAccount = null,
                ExplicitilyCreated = true,
                SubHeadAccountId = laborExpensesSubHead.Id,
                Title = "Crude Oil Wholesale Labor Expense Account",
                Balance = 0,
                CrDrType = CrDrType.General
            };
            //context.Accounts.AddOrUpdate(crudeOilWholeSaleLaborExpenseAccount);

            GeneralAccount oilDirtWholeSaleLaborExpenseAccount = new GeneralAccount
            {
                Id = Properties.Resources.OilDirtWholeSaleLaborExpenseAccount,
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Address = "N/A",
                Description = "Oil Dirt Wholesale Labor Expenses",
                SubHeadAccount = null,
                ExplicitilyCreated = true,
                SubHeadAccountId = laborExpensesSubHead.Id,
                Title = "Oil Dirt Wholesale Labor Expense Account",
                Balance = 0,
                CrDrType = CrDrType.General
            };
            //context.Accounts.AddOrUpdate(oilDirtWholeSaleLaborExpenseAccount);

            GeneralAccount oilCakeRetailLaborExpenseAccount = new GeneralAccount
            {
                Id = Properties.Resources.OilCakeRetailLaborExpenseAccount,
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Address = "N/A",
                Description = "Oil Cake Retail Wholesale Labor Expenses",
                SubHeadAccount = null,
                ExplicitilyCreated = true,
                SubHeadAccountId = laborExpensesSubHead.Id,
                Title = "Oil Cake Retail Wholesale Labor Expense Account",
                Balance = 0,
                CrDrType = CrDrType.General
            };
            context.Accounts.AddOrUpdate(oilCakeRetailLaborExpenseAccount);

            GeneralAccount salariesAccount = new GeneralAccount
            {
                Id = Properties.Resources.SalariesAccount,
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Address = "N/A",
                Description = "Salary Account",
                SubHeadAccount = null,
                ExplicitilyCreated = true,
                SubHeadAccountId = salariesSubHead.Id,
                Title = "Salary Account",
                Balance = 0,
                CrDrType = CrDrType.General
            };
            context.Accounts.AddOrUpdate(salariesAccount);

            GeneralAccount generalExpenseAccount = new GeneralAccount
            {
                Id = Properties.Resources.GeneralExpenseAccount,
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Balance = 0,
                Address = "N/A",

                Description = "General Expense Account",
                SubHeadAccount = null,

                ExplicitilyCreated = true,

                SubHeadAccountId = generalExpenseSubhead.Id,

                Title = "General Expense Account",
                CrDrType = CrDrType.General
            };
            context.Accounts.AddOrUpdate(generalExpenseAccount);
            GeneralAccount freightAccount = new GeneralAccount
            {
                Id = Properties.Resources.FreightExpenseAccount,
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Balance = 0,
                Address = "N/A",

                Description = "Freight Expense Account",
                SubHeadAccount = null,

                ExplicitilyCreated = true,

                SubHeadAccountId = miscSellingExpenses.Id,

                Title = "Freight Expense Account",
                CrDrType = CrDrType.General
            };
            context.Accounts.AddOrUpdate(freightAccount);

            GeneralAccount goodsLossShortQty = new GeneralAccount
            {
                Id = Properties.Resources.GoodsLossDueToShortQty,
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Balance = 0,
                Address = "N/A",

                Description = "Goods loss due to shortage in qty",
                SubHeadAccount = null,

                ExplicitilyCreated = true,

                SubHeadAccountId = miscSellingExpenses.Id,

                Title = "Goods Shortage Loss Account",
                CrDrType = CrDrType.General
            };
            context.Accounts.AddOrUpdate(goodsLossShortQty);

            GeneralAccount brokeryExpenseAccount = new GeneralAccount
            {
                Id = Properties.Resources.BrokeryExpenseAccount,
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Balance = 0,
                Address = "N/A",

                Description = "Brokery Expense Account",
                SubHeadAccount = null,

                ExplicitilyCreated = true,

                SubHeadAccountId = miscSellingExpenses.Id,

                Title = "Brokery Expense Account",
                CrDrType = CrDrType.General
            };
            context.Accounts.AddOrUpdate(brokeryExpenseAccount);

            GeneralAccount inventoryItemConsumptionExpseneAccount = new GeneralAccount
            {
                Id = Properties.Resources.InventoryItemsConsumptionExpenseAccount,
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Balance = 0,
                Address = "N/A",

                Description = "Inventory Item Expense Account",
                SubHeadAccount = null,

                ExplicitilyCreated = true,

                SubHeadAccountId = miscSellingExpenses.Id,

                Title = "Inventory Items Consumption Expense Account",
                CrDrType = CrDrType.General
            };
            context.Accounts.AddOrUpdate(inventoryItemConsumptionExpseneAccount);
            GeneralAccount tracktorLaborExpenseAccount = new GeneralAccount
            {
                Id = Properties.Resources.TracktorLaborExpenseAccount,
                AccountNature = AccountNature.Debit,
                AccountNo = "N/A",
                Balance = 0,
                Address = "N/A",

                Description = "Tracktor Labor Expense Account",
                SubHeadAccount = null,

                ExplicitilyCreated = true,

                SubHeadAccountId = miscSellingExpenses.Id,

                Title = "Tracktor Labor Expense Account",
                CrDrType = CrDrType.General
            };
            context.Accounts.AddOrUpdate(tracktorLaborExpenseAccount);
            context.SaveChanges();
            #endregion

            #region "Income accounts"
            HeadAccount income = new HeadAccount
            {
                Id = Properties.Resources.IncomeHead,
                Description = "Income  Accounts top level",
                Title = "Income Accounts",
                AccountNature = AccountNature.Credit,

                ExplicitilyCreated = true,
                AccountNo = "N/A",
                Address = "N/A",
                TopHeadAccounts = null
            };

            context.Accounts.AddOrUpdate(income);
            context.SaveChanges();

            TopHeadAccount incomeTopHead = new TopHeadAccount
            {
                Id = Properties.Resources.IncomeTopHead,
                Title = "Income accounts",
                Description = "Income top head account",
                AccountNature = AccountNature.Credit,

                ExplicitilyCreated = true,
                AccountNo = "N/A",
                Address = "N/A",
                HeadAccount = null,
                SubHeadAccounts = null,
                HeadAccountId = Properties.Resources.IncomeHead
            };

            context.Accounts.AddOrUpdate(incomeTopHead);
            context.SaveChanges();

            SubHeadAccount oilCakeSalesSubHead = new SubHeadAccount
            {
                Id = Properties.Resources.OilCakeSalesSubHead,
                Title = "Oil Cake (whole) sales accounts",
                Description = "Oil Cake (whole) sales accounts",
                TopHeadAccount = null,
                TopHeadAccountId = Properties.Resources.IncomeTopHead,
                Accounts = null,
                AccountNature = AccountNature.Credit,

                ExplicitilyCreated = true,
                AccountNo = "N/A",
                Address = "N/A"
            };
            context.Accounts.AddOrUpdate(oilCakeSalesSubHead);
            context.SaveChanges();

            SubHeadAccount oilSalesSubHead = new SubHeadAccount
            {
                Id = Properties.Resources.OilSalesSubHead,
                Title = "Oil (whole) sales accounts",
                Description = "Oil (whole) sales accounts",
                TopHeadAccount = null,
                TopHeadAccountId = Properties.Resources.IncomeTopHead,
                Accounts = null,
                AccountNature = AccountNature.Credit,

                ExplicitilyCreated = true,
                AccountNo = "N/A",
                Address = "N/A"
            };
            context.Accounts.AddOrUpdate(oilSalesSubHead);
            context.SaveChanges();

            SubHeadAccount oilDirtSubHead = new SubHeadAccount
            {
                Id = Properties.Resources.OilDirtSubHead,
                Title = "Oil Dirt (whole) sales accounts",
                Description = "Oil Dirt (whole) sales accounts",
                TopHeadAccount = null,
                TopHeadAccountId = Properties.Resources.IncomeTopHead,
                Accounts = null,
                AccountNature = AccountNature.Credit,

                ExplicitilyCreated = true,
                AccountNo = "N/A",
                Address = "N/A"
            };
            context.Accounts.AddOrUpdate(oilDirtSubHead);

            SubHeadAccount oilCakeRetailSubHead = new SubHeadAccount
            {
                Id = Properties.Resources.OilCakeRetailSubHead,
                Title = "Oil Cake (retail) sales accounts",
                Description = "Oil Cake (retail) sales accounts",
                TopHeadAccount = null,
                TopHeadAccountId = Properties.Resources.IncomeTopHead,
                Accounts = null,
                AccountNature = AccountNature.Credit,

                ExplicitilyCreated = true,
                AccountNo = "N/A",
                Address = "N/A"
            };
            context.Accounts.AddOrUpdate(oilCakeRetailSubHead);
            context.SaveChanges();


            GeneralAccount oilCakeAccount = new GeneralAccount
            {
                Id = Properties.Resources.OilCakeRetailSaleAccount,
                ExplicitilyCreated = true,
                AccountNature = AccountNature.Credit,
                Balance = 0,
                SubHeadAccount = null,
                Description = "Oil Cake Retail Sales Account",
                SubHeadAccountId = oilCakeRetailSubHead.Id,
                Title = "Oil Cake Retail Sales Account",
                AccountNo = "N/A",
                Address = "N/A",
                CrDrType = CrDrType.General
            };
            context.Accounts.AddOrUpdate(oilCakeAccount);
            #endregion

            #endregion

            #region Admin
            SecOptMod secOps = Helper.GetSecOpsObj();
            string abcd = secOps.ChalJanDey;

            string dexi = MsrCipher.Encrypt(abcd);
            Rahzam gwerwaashjko = new Rahzam
            {
                Id = "akhpalkilrae",
                ChalBasKerYar = MsrCipher.Encrypt(DateTime.Now.AddDays(24).Date.ToString()),
                HunAramEe = MsrCipher.Encrypt("asd;fksi@r8234asffasdf##"),
                ItheyRakh = MsrCipher.Encrypt(DateTime.Now.ToString()),
                ChalJanDey = dexi,
                ChangaPhir = MsrCipher.Encrypt("kia;kizei;sdkf;sdfasieasf34az871##"),
                RabRakha = MsrCipher.Encrypt(DateTime.Now.ToString()),
            };
            var jkabedlra = context.Anattakh.Find(gwerwaashjko.Id);
            if (jkabedlra == null)
                context.Anattakh.AddOrUpdate(gwerwaashjko);
            #endregion

            #region "Account based Items"

            #endregion
            context.SaveChanges();
        }
    }
}
