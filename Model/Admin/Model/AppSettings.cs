using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Admin.Model
{
    public class AppSettings
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NameUrdu { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string AddressUrdu { get; set; }
        public string MainPrinter { get; set; }
        public string ThermalPrinter { get; set; }
        public int DaysAlertBeforeReady { get; set; }
        public byte[] Logo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal OilCakeBrokerSharePercentage { get; set; }
        public bool AllowToChargeOilCakeBrokerSharePercentage { get; set; }
        public decimal OilBrokerSharePercentage { get; set; }
        public bool AllowToChangeOilBrokerSharePercentage { get; set; }
        public decimal MailBrokerSharePercentage { get; set; }
        public bool AllowToChangeMailBrokerSharePercentage { get; set; }
        public decimal SalesTaxPercentage { get; set; }
        public decimal OfferDiscountPercentage { get; set; }
        public decimal ServiceCharges { get; set; }
        public bool EnableDiscounts { get; set; }
        public string MasterPassword { get; set; }
        public string SecurityPassword { get; set; }
        public bool ApplyRetailOilCakeLaborExpense { get; set; }
        public bool ApplayWholeSaleOilCakeLaborExpense { get; set; }
        public bool ApplyCrudeOilLaborExpense { get; set; }
        public bool ApplyOilDirtLaborExpense { get; set; }
        public bool ApplyPurchasedItemLaborExpense { get; set; }
        public bool ApplyBardanaExpenseForRetailOilCake { get; set; }
        public bool ApplyBardanaExpenseForWholeSaleOilCake { get; set; }

       
        // Is legtimate
        public bool bool1 { get; set; }
        public bool bool2 { get; set; }
        public bool bool3 { get; set; }

        // Print transactions
        public bool PrintFinancialTransactions { get; set; }
        public bool bool5 { get; set; }

        public string string1 { get; set; }
        public string string2 { get; set; }
        public string string3 { get; set; }
        public string string4 { get; set; }
        public string string5 { get; set; }
        public decimal decimal1 { get; set; }
        public decimal decimal2 { get; set; }
        public decimal decimal3 { get; set; }
        public decimal decimal4 { get; set; }
        public decimal decimal5 { get; set; }

        #region Retail receipts
        // Gate pass no of copies
        public int GatePassCopies { get; set; }

        // Floor scale no of copies
        public int FloorScaleCopies { get; set; }

        // Customer no of copies
        public int CustomerCopies { get; set; }

        // Office no of copies
        public int OfficeCopies { get; set; }

        #endregion
        public int int5 { get; set; }
        public int int1 { get; set; }
        public int int2 { get; set; }

        public DateTime dt1 { get; set; }
        public DateTime dt2 { get; set; }
        public DateTime dt3 { get; set; }
        public DateTime dt4 { get; set; }

        public byte[] bin1 { get; set; }
        public byte[] bin2 { get; set; }
    }
}
