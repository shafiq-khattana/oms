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
using Model.Financials.Model;

namespace WinFom.Retail.Forms
{
    public partial class EditProductForm : Form
    {
        private List<ProductSize> productSizes = null;
        private List<ProductCategory> productCategories = null;
        private Product product = null;
        private int productId = 0;
        private ProductSize productSize = null;
        private ProductCategory productCategory = null;
        public bool IsDone = false;
        public EditProductForm(int prodId)
        {
            InitializeComponent();
            productId = prodId;
        }
        private void LoadProductSizes()
        {
            try
            {
                using (Context db = new Context())
                {
                    productSizes = db.ProductSizes.ToList();
                    product = db.Products.Find(productId);
                    product.ProductCategory = db.ProductCategories.Find(product.ProductCategoryId);
                    product.ProductSize = db.ProductSizes.Find(product.ProductSizeId);
                    product.StockItem = db.StockItems.Find(product.StockItemId);

                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadProductCategories()
        {
            try
            {
                using (Context db = new Context())
                {
                    productCategories = db.ProductCategories.ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadBoth()
        {
            LoadProductCategories();
            LoadProductSizes();
        }
        private void BindCbProductSizes()
        {
            cbProductSize.DataSource = productSizes;
            cbProductSize.DisplayMember = "Title";
            cbProductSize.ValueMember = "Id";

            cbProductSize.SelectedItem = cbProductSize.Items.OfType<ProductSize>()
                .FirstOrDefault(a => a.Id == product.ProductSizeId);
            
        }
        private void BindCbProductCategories()
        {
            cbCategories.DataSource = productCategories;
            cbCategories.DisplayMember = "Title";
            cbCategories.ValueMember = "Id";
            var item = cbCategories.Items.OfType<ProductCategory>().FirstOrDefault(a => a.Id == product.ProductCategoryId);
            if (item != null)
            {
                cbCategories.SelectedItem = item;
                cbCategories.Enabled = btnAddCategory.Enabled = false;
            }
            else
            {
                cbCategories.Enabled = btnAddCategory.Enabled = true;
            }
        }
        private void BindBoth()
        {
            BindCbProductCategories();
            BindCbProductSizes();
        }
        private void picBtnClose_Click(object sender, EventArgs e)
        {
            CloseMe();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait = new WaitForm(LoadBoth);
                wait.ShowDialog();
                BindBoth();

                Gujjar.TB4(pMain);
                Gujjar.NumbersOnly(tbBarcode);
                Gujjar.NumbersOnly(tbCustomerPrice);
                Gujjar.NumbersOnly(tbPoints);
                Gujjar.NumbersOnly(tbDiscountPercentage);
                Gujjar.NumbersOnly(tbWholeSalePrice);

                tbDiscountPercentage.Text = "0";
                tbPoints.Text = "0";

                tbProductTitle.Text = product.Title;
                tbCustomerPrice.Text = product.ProductTotalUnitPrice.ToString("n2");
                bswIsDiscountable.Value = product.IsDicountable;
                bswAlertOnSale.Value = product.AlertOnSale;
                bswProductStatus.Value = product.IsAvailable;
                tbBarcode.Text = product.Barcode;
                tbDiscountPercentage.Text = product.ProductDiscPercentage.ToString("n2");
                tbProductDiscount.Text = product.ProductDiscount.ToString("n2");
                tbProductNetPrice.Text = product.ProductNetUnitPrice.ToString("n2");
                tbPoints.Text = product.ProductPoints.ToString("n2");
                tbWeight.Text = product.Weight.ToString("n2");
                tbWholeSalePrice.Text = product.ProductNetUnitPriceWholeSale.ToString("n2");
                bswDeductBardana.Value = product.DeductBardanaPacking;
                bswApplyLaborExpense.Value = product.ApplyLaborExpense;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseMe();
        }
        public void CloseMe()
        {
            DialogResult res = Gujjar.ConfirmYesNo("Are you sured to close?");
            if (res == DialogResult.No)
                return;
            Close();
        }
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            try
            {
                AddProductCategoryForm form = new AddProductCategoryForm();
                form.ShowDialog();
                int id = form.CategoryId;
                if (id != 0)
                {
                    LoadProductCategories();
                    BindCbProductCategories();

                    cbCategories.SelectedItem = cbCategories.Items.OfType<ProductCategory>()
                        .FirstOrDefault(a => a.Id == id);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddProductsize_Click(object sender, EventArgs e)
        {
            try
            {
                AddProductSizeForm form = new AddProductSizeForm();
                form.ShowDialog();

                int id = form.ProductSizeId;
                if (id != 0)
                {
                    LoadProductSizes();
                    BindCbProductSizes();

                    cbProductSize.SelectedItem = cbProductSize.Items.OfType<ProductSize>()
                        .FirstOrDefault(a => a.Id == id);
                }
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
                if (!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill text fields");
                }
                if (cbCategories.SelectedIndex == -1)
                {
                    throw new Exception("Please select product category");
                }
                if (cbProductSize.SelectedIndex == -1)
                {
                    throw new Exception("Please select product size");
                }

                if (!Helper.ConfirmUserPassword())
                {
                    return;
                }
                ProductCategory prodCategory = cbCategories.SelectedItem as ProductCategory;
                ProductSize prodSize = cbProductSize.SelectedItem as ProductSize;

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var productDb = db.Products.Find(productId);

                            productDb.Title = tbProductTitle.Text;
                            productDb.ProductTotalUnitPrice = tbCustomerPrice.Text.ToDecimal();
                            productDb.IsDicountable = bswIsDiscountable.Value;
                            productDb.AlertOnSale = bswAlertOnSale.Value;
                            productDb.IsAvailable = bswProductStatus.Value;
                            productDb.ProductDiscPercentage = tbProductDiscount.Text.ToDecimal();
                            productDb.Barcode = tbBarcode.Text;
                            productDb.ProductPoints = tbPoints.Text.ToDecimal();
                            productDb.Weight = tbWeight.Text.ToDecimal();
                            productDb.ProductPoints = tbPoints.Text.ToDecimal();
                            productDb.ProductSizeId = prodSize.Id;
                            productDb.ProductCategoryId = prodCategory.Id;
                            productDb.ProductNetUnitPriceWholeSale = tbWholeSalePrice.Text.ToDecimal();
                            productDb.ApplyLaborExpense = bswApplyLaborExpense.Value;
                            productDb.DeductBardanaPacking = bswDeductBardana.Value;

                            db.Entry(productDb).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            trans.Commit();

                            productDb.ProductSize = prodSize;

                            var items = SingleTon.LoginForm.AppProducts;
                            var objdel = items.FirstOrDefault(a => a.Id == productDb.Id);
                            if(objdel != null)
                            {
                                items.Remove(objdel);
                                items.Add(productDb);
                            }
                            string confirmMsg = string.Format("Product ({0}) has been updated in database successfully.", productDb.Title);
                            Gujjar.InfoMsg(confirmMsg);
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

        private void cbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCategories.SelectedIndex == -1)
                return;

            productCategory = cbCategories.SelectedItem as ProductCategory;
        }

        private void cbProductSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProductSize.SelectedIndex == -1)
                return;

            productSize = cbProductSize.SelectedItem as ProductSize;
        }

        private void tbDiscountPercentage_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal discPer = 0;
                string txt = tbDiscountPercentage.Text;
                if (!string.IsNullOrEmpty(txt))
                {
                    discPer = txt.ToDecimal();
                }
                decimal totalPrice = 0;
                string txt2 = tbCustomerPrice.Text;
                if (!string.IsNullOrEmpty(txt2))
                {
                    totalPrice = txt2.ToDecimal();
                }

                decimal disc = totalPrice * discPer / 100;
                decimal net = totalPrice - disc;
                tbProductDiscount.Text = disc.ToString("n2");
                tbProductNetPrice.Text = net.ToString("n2");
            }
            catch (FormatException)
            {
                //ignore
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void bswIsDiscountable_Click(object sender, EventArgs e)
        {
            bool value = bswIsDiscountable.Value;
            tbDiscountPercentage.ReadOnly = !value;
            tbDiscountPercentage.Enabled = value;
        }

        private void tbCustomerPrice_TextChanged(object sender, EventArgs e)
        {
            tbDiscountPercentage.Text = "0";
            tbProductDiscount.Text = "0";
            tbProductNetPrice.Text = tbCustomerPrice.Text;
        }

        private void tbWeight_Leave(object sender, EventArgs e)
        {
            string txt = tbWeight.Text;
            if (string.IsNullOrEmpty(txt))
            {
                tbWeight.Text = "0.0";
            }
        }

        private void pMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
