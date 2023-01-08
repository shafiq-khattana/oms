using Model.Financials.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Retail.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleUrdu { get; set; }
        public decimal CostPrice { get; set; }
        public decimal ProductTotalUnitPrice { get; set; }
        public decimal ProductNetUnitPrice
        {
            get
            {
                return
                    ProductTotalUnitPrice - ProductDiscount;
            }
        }

        public decimal ProductNetUnitPriceWholeSale { get; set; }
        public bool IsAvailable { get; set; }
        public decimal SKU { get; set; }
        public string Barcode { get; set; }
        public bool AlertOnSale { get; set; }
        public bool IsDicountable { get; set; }
        public int ProductCategoryId { get; set; }
        public decimal ProductDiscPercentage { get; set; }
        public decimal ProductDiscount
        {
            get
            {
                return ProductTotalUnitPrice * ProductDiscPercentage / 100;
            }
        }

        public ProductCategory ProductCategory { get; set; }

        public ProductSize ProductSize { get; set; }
        public int ProductSizeId { get; set; }

        public List<SaleOrderLine> SaleOrderLines { get; set; }
        public Product()
        {
            SaleOrderLines = new List<SaleOrderLine>();
        }
        public decimal ProductPoints { get; set; }

        public GeneralAccount Account { get; set; }
        public string GeneralAccountId { get; set; }
        public int? StockItemId { get; set; }
        public StockItem StockItem { get; set; }
        public decimal Weight { get; set; }
        public bool DeductBardanaPacking { get; set; }
        public bool ApplyLaborExpense { get; set; }
    }
}