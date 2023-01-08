using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.ViewModel
{
    public class ItemVM
    {
        public int Id { get; set; }        
        public string Name { get; set; }

        [DisplayName("Store Location")]
        public string StoreLoc { get; set; }

        [DisplayName("Work Location")]
        public string WorkLoc { get; set; }

        public decimal SKU { get; set; }

        [DisplayName("Pre added")]
        public decimal PreAdded { get; set; }

        [DisplayName("Adv Items")]
        public decimal AdvanceCount { get; set; }
        [DisplayName("U/S")]
        public decimal USCount { get; set; }

        [DisplayName("U/R")]
        public decimal UR { get; set; }

        [DisplayName("Unit Price")]
        public decimal UnitValue { get; set; }

        [DisplayName("Total Price")]
        public decimal TotalValue { get; set; }        
    }
}
