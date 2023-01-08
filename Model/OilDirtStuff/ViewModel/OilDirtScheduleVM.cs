using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.OilDirtStuff.ViewModel
{
    public class OilDirtScheduleVM
    {
        public int Id { get; set; }

        [DisplayName("Sch No")]
        public string ScheduleNo { get; set; }


        [DisplayName("Sch Date")]
        public string ScheduleDate { get; set; }

        [DisplayName("Ready Date")]
        public string ReadyDate { get; set; }

        [DisplayName("Comp Date")]
        public string CompleteDate { get; set; }
        [DisplayName("Rate")]
        public string PerTradeUnitPrice { get; set; }

        [DisplayName("Vehicle#")]
        public string VehicleNo { get; set; }

        public string Selector { get; set; }
        public string Driver { get; set; }

        public string Status { get; set; }



        [Browsable(false)]
        [DisplayName("Veh Empty Weight")]
        public decimal VehicleWeightEmpty { get; set; }

        [DisplayName("Veh Full Weight")]
        [Browsable(false)]
        public decimal VehicleWeightFull { get; set; }

        
        [DisplayName("Weigh Bridge Weight")]
        [Browsable(false)]
        public decimal WeighBridgeWeight { get; set; }

        [DisplayName("Weigh Bridge")]
        [Browsable(false)]
        public string WeighBridge { get; set; }

        [DisplayName("Broker Share %")]
        [Browsable(false)]
        public decimal BrokerSharePercentage { get; set; }

        [Browsable(false)]
        public decimal BrokerShareAmount { get; set; }

        [Browsable(false)]
        public decimal TotalExpectedAmount { get; set; }

        [Browsable(false)]
        public decimal TotalActualAmount { get; set; }

        [Browsable(false)]
        public decimal TotalNetAmount { get; set; }

        
    }
}
