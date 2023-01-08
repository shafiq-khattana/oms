using Model.Deal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.CommonModel
{
    public class RawBrokerShareType
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<AppDeal> Deals { get; set; }
        public RawBrokerShareType()
        {
            Deals = new List<AppDeal>();
        }
    }
}
