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

namespace WinFom.Retail.Forms
{
    public partial class AddProductEntryForm : Form
    {
        private int productId = 0;
        private Product prod = null;
        public bool IsDone = false;
        public AddProductEntryForm(int prodId)
        {
            InitializeComponent();
            productId = prodId;
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
                    prod = db.Products.Find(productId);
                    prod.ProductSize = db.ProductSizes.Find(prod.ProductSizeId);
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

                tbProduct.Text = string.Format("{0}-{1}", prod.Title, prod.ProductSize.Title);

                Gujjar.NumbersOnly(tbQty);
                Gujjar.NumbersOnly(tbTotalPrice);
                Gujjar.NumbersOnly(tbUnitPrice);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }

                ProductEntryRecord record = new ProductEntryRecord
                {
                    Id = 0,
                    AddedDate = DateTime.Now,
                    Product = null,
                    ProductId = productId,
                    Qty = tbQty.Text.ToDecimal(),
                    Remarks = tbRemarks.Text,
                    TotalPrice = tbTotalPrice.Text.ToDecimal(),
                    UnitPrice = tbUnitPrice.Text.ToDecimal()
                };
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var dbProduct = db.Products.Find(productId);
                            dbProduct.SKU += record.Qty;
                            dbProduct.CostPrice = record.UnitPrice;

                            db.Entry(dbProduct).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            db.ProductEntryRecords.Add(record);
                            db.SaveChanges();
                            trans.Commit();

                            Gujjar.InfoMsg("Product entry is added in database");
                            IsDone = true;
                            Close();
                        }
                        catch (Exception ep)
                        {

                            trans.Rollback();
                            throw ep;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbUnitPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal qty = 0;
                string txt = tbQty.Text;
                if(!string.IsNullOrEmpty(txt))
                {
                    qty = txt.ToDecimal();
                }
                decimal unit = 0;
                string txt2 = tbUnitPrice.Text;
                if(!string.IsNullOrEmpty(txt2))
                {
                    unit = txt2.ToDecimal();
                }

                decimal total = unit * qty;
                tbTotalPrice.Text = total.ToString("n2");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
