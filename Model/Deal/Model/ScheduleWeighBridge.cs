using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Deal.Model
{
    public class ScheduleWeighBridge
    {
        public int Id { get; set; }
        public DealSchedule DealSchedule { get; set; }
        public int DealScheduleId { get; set; }

        public WeighBridge WeighBridge { get; set; }
        public int WeighBridgeId { get; set; }
        public ScheduleWeighBridgeType Type { get; set; }

    }
    public enum ScheduleWeighBridgeType
    {
        Load,
        Receive
    }
}
