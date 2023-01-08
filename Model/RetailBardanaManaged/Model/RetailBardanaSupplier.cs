using Model.Financials.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.RetailBardanaManaged.Model
{
    public class RetailBardanaSupplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameUrdu { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string AddressUrdu { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }
        public string Remarks { get; set; }
        public GeneralAccount Account { get; set; }
        public string GeneralAccountId { get; set; }
        public List<RetailBardanaItemEntry> Entries { get; set; }

        public RetailBardanaSupplier()
        {
            Entries = new List<RetailBardanaItemEntry>();
        }
    }
}
