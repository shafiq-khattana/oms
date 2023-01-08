using Model.Deal.Model;
using Model.OilDirtStuff.Model;
using Model.ReadyStuff.Model;
using Model.Retail.Model;
using Model.RetailBardanaManaged.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Financials.Model
{
    public class DayBook
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string DebitTrace { get; set; }
        public string CreditTrace { get; set; }
        public decimal Amount { get; set; }
        public bool IsReversed { get; set; }

        [Browsable(false)]
        public string DebitAccountId { get; set; }

        [Browsable(false)]
        public string CreditAccountId { get; set; }

        [Browsable(false)]
        public bool CanRollBack { get; set; }
        public int? SaleOrderId { get; set; }
        public SaleOrder SaleOrder { get; set; }
        public DateTime InDate { get; set; }
        public int? DealScheduleId { get; set; }
        public DealSchedule DealSchedule { get; set; }
        public int? OilDirtScheduleId { get; set; }
        public OilDirtSchedule OilDirtSchedule { get; set; }

        public int? OilDealId { get; set; }
        public OilDeal OilDeal { get; set; }

        public int? ReadyScheduleId { get; set; }
        public ReadySchedule ReadySchedule { get; set; }        
    }
    
}
