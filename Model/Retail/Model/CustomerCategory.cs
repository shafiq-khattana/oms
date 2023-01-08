using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Retail.Model
{
    public class CustomerCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleUrdu { get; set; }
        public decimal Discount { get; set; }
        public List<Customer> Customers { get; set; }

        public CustomerCategory()
        {
            Customers = new List<Customer>();
        }
    }
}
