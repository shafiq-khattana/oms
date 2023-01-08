using Model.Financials.Model;
using System;
using System.Collections.Generic;

namespace Model.ReadyStuff.Model
{
    public class ReadyBroker : IEquatable<ReadyBroker>
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
        public List<ReadyDeal> Deals { get; set; }
        

        public ReadyBroker()
        {
            Deals = new List<ReadyDeal>();
           
        }

        public bool Equals(ReadyBroker other)
        {
            return (Name.ToLower().Equals(other.Name.ToLower())
             && Address.ToLower().Equals(other.Address.ToLower())
             && Contact.Equals(other.Contact));
        }
    }
}