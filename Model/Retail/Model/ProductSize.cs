
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Retail.Model
{
    public class ProductSize
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleUrdu { get; set; }
        public List<Product> Products { get; set; }
        public ProductSize()
        {
            Products = new List<Product>();
        }
    }
}
