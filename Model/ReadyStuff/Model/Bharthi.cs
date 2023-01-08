namespace Model.ReadyStuff.Model
{
    public class Bharthi
    {
        public int Id { get; set; }
        public BharthiType BharthiType { get; set; }
        public int BharthiTypeId { get; set; }
        public decimal Qty { get; set; }
        public decimal Total { get; set; }

        public ReadySchedule ReadySchedule { get; set; }
        public int ReadyScheduleId { get; set; }
    }
}