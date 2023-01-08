using Model.Deal.Model;
using Model.Employees.Model;
using Model.OilDirtStuff.Model;
using Model.ReadyStuff.Model;
using Model.Retail.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Financials.Model
{
    public class GeneralAccount : AccountBase
    {
        public string SubHeadAccountId { get; set; }
        public SubHeadAccount SubHeadAccount { get; set; }

        public List<AccountTransaction> AccountTransactions { get; set; }

        public string BankName { get; set; }

        public List<Company>  Companies { get; set; }
        //public Company Company { get; set; }
        //public int? CompanyId { get; set; }

        public List<Broker> Brokers { get; set; }
        //public Broker Broker { get; set; }
        //public int? BrokerId { get; set; }

        public List<Selector> Selectors { get; set; }
        //public Selector Selector { get; set; }
        //public int? SelectorId { get; set; }

        public List<GoodCompany> GoodCompanies { get; set; }

        //public GoodCompany GoodCompany { get; set; }
        //public int? GoodCompanyId { get; set; }

        public List<DealItem> DealItems { get; set; }
        //public DealItem DealItem { get; set; }
        //public int? DealItemId { get; set; }

        public decimal Balance { get; set; }


        public List<OilDealBroker>   OilDealBrokers { get; set; }
        //public OilDealBroker OilDealBroker { get; set; }
        //public int? OilDealBrokerId { get; set; }

        public List<OilDealSelector> OilDealSelectors { get; set; }
        //public OilDealSelector OilDealSelector { get; set; }
        //public int? OilDealSelectorId { get; set; }

        public List<ReadyBroker> ReadyBrokers { get; set; }
        //public ReadyBroker ReadyBroker { get; set; }
        //public int? ReadyBrokerId { get; set; }



        public List<ReadySelector> ReadySelectors { get; set; }
        //public ReadySelector ReadySelector { get; set; }
        //public int? ReadySelectorId { get; set; }

        public List<OilDealItem> OilDealItems { get; set; }
        //public OilDealItem OilDealItem { get; set; }
        //public int? OilDealItemId { get; set; }

        public List<ReadyItem> ReadyItems { get; set; }
        //public ReadyItem ReadyItem { get; set; }
        //public int? ReadyItemId { get; set; }




        public List<OilDirtBroker> OilDirtBrokers { get; set; }
        //public OilDirtBroker OilDirtBroker { get; set; }
        //public int? OilDirtBrokerId { get; set; }

        public List<OilDirtSelector> OilDirtSelectors { get; set; }
        //public OilDirtSelector OilDirtSelector { get; set; }
        //public int? OilDirtSelectorId { get; set; }

        public List<OilDirtItem> OilDirtItems { get; set; }
        //public OilDirtItem OilDirtItem { get; set; }
        //public int? OilDirtItemId { get; set; }
        public List<Employee>    Employees { get; set; }
        //public Employee { get; set; }
        //public int? EmployeeId { get; set; }

        public List<Customer>   Customers { get; set; }
        //public Customer Customer { get; set; }
        //public int? CustomerId { get; set; }

        public CrDrType? CrDrType { get; set; }

        public string ParentAccountId { get; set; }

        [ForeignKey("ParentAccountId")]
        public GeneralAccount ParentAccount { get; set; }

        public List<GeneralAccount> LinkAccounts { get; set; }

        public GeneralAccount()
        {
            Companies = new List<Company>();
            Brokers = new List<Broker>();
            Selectors = new List<Selector>();
            GoodCompanies = new List<GoodCompany>();
            DealItems = new List<DealItem>();


            OilDealBrokers = new List<OilDealBroker>();
            OilDealSelectors = new List<OilDealSelector>();
            ReadyBrokers = new List<ReadyBroker>();
            ReadySelectors = new List<ReadySelector>();
            OilDealItems = new List<OilDealItem>();
            ReadyItems = new List<ReadyItem>();


            OilDirtSelectors = new List<OilDirtSelector>();
            OilDirtBrokers = new List<OilDirtBroker>();
            OilDirtItems = new List<OilDirtItem>();


            Employees = new List<Employee>();

            Customers = new List<Customer>();
            AccountTransactions = new List<AccountTransaction>();
            LinkAccounts = new List<GeneralAccount>();
        }
    }

    public enum CrDrType
    {
        General,
        Debitor,
        Creditor
    }
}
