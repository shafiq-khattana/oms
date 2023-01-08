using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ReadyStuff.ViewModel
{
    public class SchVM2
    {
        public int Id { get; set; }

        [DisplayName("Sch No.")]
        public string ScheduleNo { get; set; }
        [DisplayName("Sch Date")]
        public string ScheduleDate { get; set; }

        [DisplayName("Ready Date")]
        public string ReadyDate { get; set; }

        [DisplayName("Completed")]
        public bool IsCompleted { get; set; }

        [DisplayName("Vehicles")]
        public int NoOfVehicles { get; set; }

        [DisplayName("Disp Date")]
        public string DispatchedDate { get; set; }

        [DisplayName("Driver")]
        public string Driver { get; set; }

        [DisplayName("Selector")]
        public string Selector { get; set; }

        [DisplayName("Weight")]
        public decimal ScheduleWeight { get; set; }

        [DisplayName("WB Weight")]
        public decimal WeighBridgeWeight { get; set; }

        [DisplayName("Weigh Bridge")]
        public string WeighBridge { get; set; }

        [DisplayName("Status")]
        public string ScheduleStatus { get; set; }

        [Browsable(false)]
        public string TradeUnit { get; set; }

        [Browsable(false)]
        public decimal TotalTradeUnits { get; set; }

        [Browsable(false)]
        public decimal PerTradeUnitPrice { get; set; }

        [Browsable(false)]
        public decimal GrossPrice { get; set; }

        [Browsable(false)]
        public decimal BrokerSharePercentage { get; set; }

        [Browsable(false)]
        public decimal BrokerShareAmount { get; set; }

        [Browsable(false)]
        public decimal NetScheduleAmount { get; set; }

        [Browsable(false)]
        public int ReadyDealId { get; set; }

        [Browsable(false)]
        public int Bharthies { get; set; }

        [Browsable(false)]
        public decimal VehicleEmptyWeight { get; set; }
    }
}
