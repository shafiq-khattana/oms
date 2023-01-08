using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.Model
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameUrdu { get; set; }
        public List<RepItem> Items { get; set; }
        public Location()
        {
            Items = new List<RepItem>();
        }
    }
}
