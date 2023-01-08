using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Financials.Model
{
    public class LongTermAssetItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal InitialPrice { get; set; }
        public decimal CurrentPrice { get; set; }

        public DateTime DateAdded { get; set; }
        public string Description { get; set; }
        public string GeneralAccountId { get; set; }

        [ForeignKey("GeneralAccountId")]
        public GeneralAccount Account { get; set; }
    }
}
