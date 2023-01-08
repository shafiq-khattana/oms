using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Deal.Model
{
    public class TradeUnit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleUrdu { get; set; }
        public decimal Qty { get; set; }
        

        public List<OmeDeal> Deals { get; set; }
        public List<AppDeal> AppDeals { get; set; }
        public TradeUnit()
        {
            Deals = new List<OmeDeal>();
            AppDeals = new List<AppDeal>();
        }
    }
}
