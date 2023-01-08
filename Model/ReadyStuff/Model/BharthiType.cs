using System.Collections.Generic;

namespace Model.ReadyStuff.Model
{
    public class BharthiType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal UnitQty { get; set; }

        public List<Bharthi> Bharthies{ get; set; }
        public BharthiType()
        {
            Bharthies = new List<Bharthi>();
        }
    }
}