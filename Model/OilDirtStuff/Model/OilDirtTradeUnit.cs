using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model.OilDirtStuff.Model
{
    public class OilDirtTradeUnit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal UnitQty { get; set; }
        public List<OilDirtDeal> Deals { get; set; }
        public OilDirtTradeUnit()
        {
            Deals = new List<OilDirtDeal>();
        }
    }
}