using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.Model
{
    public class ItemPlaceSKU
    {
        public int Id { get; set; }
        public int RepItemId { get; set; }
        public int RepPlaceId { get; set; }
        public decimal SKU { get; set; }
    }
}
