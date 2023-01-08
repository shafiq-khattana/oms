using System.Collections.Generic;

namespace Model.ReadyStuff.Model
{
    public class OilTradeUnit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal UnitQty { get; set; }

        public List<OilDeal> Deals { get; set; }

        public OilTradeUnit()
        {
            Deals = new List<OilDeal>();
        }
    }
}