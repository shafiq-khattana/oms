using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.Model
{
    public class ItemCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleUrdu { get; set; }
        public List<RepItem> RepItems { get; set; }

        public ItemCategory()
        {
            RepItems = new List<RepItem>();
        }
    }
}
