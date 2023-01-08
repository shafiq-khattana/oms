using Model.Financials.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model.OilDirtStuff.Model
{
    public class OilDirtBroker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameUrdu { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }
        public string Extra { get; set; }

        public GeneralAccount Account { get; set; }
        public string GeneralAccountId { get; set; }
        public string BrokerExpenseAccountId { get; set; }
        public List<OilDirtDeal> Deals { get; set; }
        
        public OilDirtBroker()
        {
            Deals = new List<OilDirtDeal>();            
        }
        public bool Equals(OilDirtBroker other)
        {
            return (Name.ToLower().Equals(other.Name.ToLower())
             && Address.ToLower().Equals(other.Address.ToLower())
             && Contact.Equals(other.Contact));
        }
    }
}