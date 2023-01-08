using Model.Financials.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model.OilDirtStuff.Model
{
    public class OilDirtItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleUrdu { get; set; }
        public List<OilDirtDeal> Deals { get; set; }

        public GeneralAccount Account { get; set; }
        public string GeneralAccountId { get; set; }
        public OilDirtItem()
        {
            Deals = new List<OilDirtDeal>();
        }
    }
}