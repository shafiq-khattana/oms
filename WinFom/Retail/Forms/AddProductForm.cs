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
    public partial class AddProductForm : Form
    {
        private List<ProductSize> productSizes = null;
        private List<ProductCategory> productCategories = null;

        private ProductSize productSize = null;
        private ProductCategory productCategory = null;
        public AddProductForm()
        {
            InitializeComponent();
        }
        private void LoadProductSizes()
        {
            try
            {
                using (Context db = new Context())
                {
                    productSizes = db.ProductSizes.ToList();
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
        }
        private void BindCbProductCategories()
        {
            cbCategories.DataSource = productCategories;
            cbCategories.DisplayMember = "Title";
            cbCategories.ValueMember = "Id";
            var item = cbCategories.Items.OfType<ProductCategory>().FirstOrDefault(a => a.Id == 1);
            if (item != null)
            {
                cbCategories.SelectedItem = item;
                cbCategories.Enabled = btnAddCategory.Enabled = false;
            } 
            else
            {
                cbCategories.Enabled = btnAddCategory.Enabled =  true;
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
                if(id != 0)
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
                if(id != 0)
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
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill text fields");
                }
                if(cbCategories.SelectedIndex == -1)
                {
                    throw new Exception("Please select product category");
                }
                if(cbProductSize.SelectedIndex == -1)
                {
                    throw new Exception("Please select product size");
                }

                Product product = new Product
                {
                    Id = 0,
                    ProductSize = null,
                    AlertOnSale = bswAlertOnSale.Value,
                    Barcode = tbBarcode.Text,
                    CostPrice = 0,
                    ProductTotalUnitPrice = tbCustomerPrice.Text.ToDecimal(),
                    IsAvailable = true,
                    IsDicountable = bswIsDiscountable.Value,
                    ProductCategory = null,
                    ProductCategoryId = productCategory.Id,
                    ProductSizeId = productSize.Id,
                    SaleOrderLines = null,
                    SKU = 0,
                    Title = tbProductTitle.Text,
                    ProductDiscPercentage = tbDiscountPercentage.Text.ToDecimal(),
                    ProductPoints = tbPoints.Text.ToDecimal(),
                    Weight = tbWeight.Text.ToDecimal(),
                    ProductNetUnitPriceWholeSale = tbWholeSalePrice.Text.ToDecimal(),
                    ApplyLaborExpense = bswApplyLaborExpense.Value,
                    DeductBardanaPacking = bswDeductBardana.Value
                };

                using (Context db = new Context())
                {                  
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var obj2 = db.Products.FirstOrDefault(a => a.Barcode == product.Barcode);
                            if (obj2 != null)
                            {
                                string msg = string.Format("Product with barcode ({0}) already exists in database", obj2.Barcode);
                                throw new Exception(msg);
                            }
                            var obj = db.Products.FirstOrDefault(a => a.ProductCategoryId == product.ProductCategoryId && a.ProductSizeId == product.ProductSizeId && a.Title.ToLower().Equals(product.Title.ToLower()));
                            if (obj != null)
                            {
                                string msg = String.Format("Product ({0}), with category ({1}), with size ({2}) already exists in database", obj.Title, productCategory.Title, productSize.Title);
                                throw new Exception(msg);
                            }

                            string acctTitle = string.Format("{0}-{1} {2} (product) account", productCategory.Title, product.Title, productSize.Title == "N/A" ? "" : productSize.Title);

                            //GeneralAccount prodAccount = new GeneralAccount
                            //{
                            //    Id = Guid.NewGuid().ToString(),
                            //    Title = acctTitle,
                            //    AccountNature = AccountNature.Credit,
                            //    Address = "N/A",
                            //    ExplicitilyCreated = false,
                            //    Description = acctTitle,
                            //    SubHeadAccountId = Properties.Resources.IncomeSubHead,
                            //    BankName = "N/A",
                            //    SubHeadAccount = null,
                            //};

                            //db.Accounts.Add(prodAccount);
                            //db.SaveChanges();

                            //product.GeneralAccountId = prodAccount.Id;
                            db.Products.Add(product);
                            db.SaveChanges();


                            trans.Commit();
                            product.ProductSize = productSize;
                            SingleTon.LoginForm.AppProducts.Add(product);
                            string confirmMsg = string.Format("Product ({0}) has been added in database successfully.", product.Title);
                            Gujjar.InfoMsg(confirmMsg);
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
                if(!string.IsNullOrEmpty(txt))
                {
                    discPer = txt.ToDecimal();
                }
                decimal totalPrice = 0;
                string txt2 = tbCustomerPrice.Text;
                if(!string.IsNullOrEmpty(txt2))
                {
                    totalPrice = txt2.ToDecimal();
                }

                decimal disc = totalPrice * discPer / 100;
                decimal net = totalPrice - disc;
                tbProductDiscount.Text = disc.ToString("n2");
                tbProductNetPrice.Text = net.ToString("n2");
            }
            catch(FormatException)
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
            tbProductNetPrice.Text = tbWholeSalePrice.Text = tbCustomerPrice.Text;
        }

        private void tbWeight_Leave(object sender, EventArgs e)
        {
            string txt = tbWeight.Text;
            if(string.IsNullOrEmpty(txt))
            {
                tbWeight.Text = "0.0";
            }
        }
    }
}
