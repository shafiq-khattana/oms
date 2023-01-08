using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Deal.Model
{
    public class ScheduleLoadPacking
    {
        public int Id { get; set; }
        public DealSchedule DealSchedule { get; set; }
        public int DealScheduleId { get; set; }
        public DealPacking DealPacking { get; set; }
        public int DealPackingId { get; set; }
        public decimal LoadQty { get; set; }
        public decimal ReceiveQty { get; set; }
    }
}
