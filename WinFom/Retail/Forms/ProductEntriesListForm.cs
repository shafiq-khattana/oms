using Khattana.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFom.Admin.Database;
using Model.Deal.Model;
using Model.ReadyStuff.Model;
using WinFom.Common.Forms;
using Model.Admin.Model;
using WinFom.Common.Model;
using Model.Retail.Model;
using Model.Retail.ViewModel;

namespace WinFom.Retail.Forms
{
    public partial class ProductEntriesListForm : Form
    {
        private int prodId = 0;
        private Product prod = null;
        private List<ProductEntryRecord> entries = null;
        public ProductEntriesListForm(int pid)
        {
            InitializeComponent();
            prodId = pid;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadProduct()
        {
            try
            {
                using (Context db = new Context())
                {
                    prod = db.Products.Find(prodId);
                    prod.ProductCategory = db.ProductCategories.Find(prod.ProductCategoryId);
                    prod.ProductSize = db.ProductSizes.Find(prod.ProductSizeId);
                    entries = db.ProductEntryRecords.Where(a => a.ProductId == prod.Id).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait = new WaitForm(LoadProduct);
                wait.ShowDialog();

                tbProdCategory.Text = prod.ProductCategory.Title;
                tbProduct.Text = string.Format("{0}-{1}", prod.Title, prod.ProductSize.Title);

                foreach (var item in entries)
                {
                    ProductEntryRecordViewModel mod = new ProductEntryRecordViewModel
                    {
                        DateAdded = item.AddedDate.ToShortDateString(),
                        Id = item.Id,
                        QtyAdded = item.Qty.ToString("n2"),
                        Remarks = item.Remarks,
                        TotalPrice = item.TotalPrice.ToString("n2"),
                        UnitPrice = item.UnitPrice.ToString("n2")
                    };
                    productEntryRecordViewModelBindingSource.List.Add(mod);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
