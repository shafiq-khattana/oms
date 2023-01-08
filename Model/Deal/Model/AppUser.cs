using Model.Admin.Model;
using Model.OilDirtStuff.Model;
using Model.ReadyStuff.Model;
using Model.Retail.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Deal.Model
{
    public class AppUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public bool IsActive { get; set; }
        public string Options { get; set; }
        public AppUserOptions UserOptions { get; set; }
        public List<SaleOrder> SaleOrders { get; set; }
        public List<CancelSaleOrder> CancelSaleOrders { get; set; }
        public List<ArchiveSaleOrder> ArchiveSaleOrders { get; set; }

        public List<AppDeal> Deals { get; set; }
        public List<OilDeal> OilDeals { get; set; }
        public List<ReadyDeal> ReadyDeals { get; set; }
        public List<OilDirtDeal> OilDirtDeals { get; set; }
        public AppUser()
        {
            SaleOrders = new List<SaleOrder>();
            //CancelSaleOrders = new List<CancelSaleOrder>();
            //ArchiveSaleOrders = new List<ArchiveSaleOrder>();
            //Deals = new List<AppDeal>();
            //OilDe
        }
    }
}
