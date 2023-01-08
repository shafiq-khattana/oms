using Model.Financials.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Retail.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string CardNo { get; set; }
        public string Name { get; set; }
        public string NameUrdu { get; set; }
        public string AddressUrdu { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }
        
        public int CustomerCategoryId { get; set; }
        public CustomerCategory CustomerCategory { get; set; }
        public string CNIC { get; set; }
        public List<SaleOrder> SaleOrders { get; set; }
        public decimal Points { get; set; }
        public bool ApplyCardDiscount { get; set; }
        public DateTime CardStartDate { get; set; }
        public DateTime CardEndDate { get; set; }
        public decimal DiscountPercentage { get; set; }
        public bool IsEmployee { get; set; }
        public GeneralAccount Account { get; set; }
        public string GeneralAccountId { get; set; }
        public int? EmpId { get; set; }
        public Customer()
        {

            SaleOrders = new List<SaleOrder>();
        }

        public bool Equals(Customer other)
        {
            return (Name.ToLower().Equals(other.Name.ToLower()) && Address.ToLower().Equals(other.Address.ToLower()) && Contact.Equals(other.Contact) && CNIC.Equals(other.CNIC));
        }
    }
}
