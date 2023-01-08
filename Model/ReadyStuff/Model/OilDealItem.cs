using Model.Financials.Model;
using System.Collections.Generic;

namespace Model.ReadyStuff.Model
{
    public class OilDealItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleUrdu { get; set; }
        public List<OilDeal> OilDeals { get; set; }

        public GeneralAccount Account { get; set; }
        public string GeneralAccountId { get; set; }

        public OilDealItem()
        {
            OilDeals = new List<OilDeal>();
        }
    }
}