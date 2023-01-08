using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.Model
{
    public class StoreLocation
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string LocationNameUrdu { get; set; }
        public List<RepItem> RepItems { get; set; }

    }
}
