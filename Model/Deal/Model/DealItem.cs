using Model.Financials.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Deal.Model
{
    public class DealItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameUrdu { get; set; }
        public decimal SKU { get; set; }
        public List<OmeDeal> Deals { get; set; }
        public List<AppDeal> AppDeals { get; set; }

        [ForeignKey("GeneralAccountId")]
        public GeneralAccount Account { get; set; }
        public string GeneralAccountId { get; set; }
        public DealItem ()
        {
            Deals = new List<OmeDeal>();
            AppDeals = new List<AppDeal>();
        }
    }
}