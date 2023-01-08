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
using System.Data.Entity;

namespace WinFom.Retail.Forms
{
    public partial class ProductListForm : Form
    {
        private List<Product> productList = null;
        private string dgvbtneditbtn = "dgvtnaedit23423";
        private string dgvbtnaddentry = "dgvbtnaddentyasd234";
        private string dgvbtnentrieslist = "dgvbtnentrieslist";
        public ProductListForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadAllProducts()
        {
            try
            {
                if(productList != null && productList.Count > 0)
                {
                    productList.Clear();
                    productList = null;
                }
                using (Context db = new Context())
                {
                    productList = db.Products.Include(a => a.ProductCategory).Include(b => b.ProductSize).AsParallel().ToList();
                }
            }
            catch (Exception ep)
            {
                Gujjar.ErrMsg(ep);
            }
        }

        private void BindData(List<Product> list)
        {
            try
            {
                productViewModelBindingSource.List.Clear();
                foreach (var item in list)
                {
                    ProductViewModel model = new ProductViewModel
                    {
                        Id = item.Id,
                        AlertOnSale = item.AlertOnSale,
                        Code = item.Barcode,
                        CostPrice = item.CostPrice.ToString("n2"),
                        CustomerPrice = item.ProductTotalUnitPrice.ToString("n2"),
                        IsAvailable = item.IsAvailable,
                        IsDicountable = item.IsDicountable,
                        ProductCategory = item.ProductCategory.Title,
                        Title = item.Title,
                        ProductSize = item.ProductSize.Title,
                        SKU = item.SKU.ToString("n2")
                    };
                    productViewModelBindingSource.List.Add(model);
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
                WaitForm wait1 = new WaitForm(LoadAllProducts);
                wait1.ShowDialog();

                BindData(productList);
                Helper.IsOkApplied();
                Gujjar.AddDatagridviewButton(dgv, dgvbtneditbtn, "Edit", "Edit", 80);
                Gujjar.AddDatagridviewButton(dgv, dgvbtnaddentry, "+ Entry", "+ Entry", 80);
                Gujjar.AddDatagridviewButton(dgv, dgvbtnentrieslist, "Entries", "Entries", 80);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                int ci = e.ColumnIndex;

                if (ri == dgv.NewRowIndex || ri == -1)
                    return;
                
                if(dgv.Columns[dgvbtnaddentry].Index == ci)
                {
                    int pid = dgv.Rows[ri].Cells[0].Value.ToInt();
                    AddProductEntryForm form = new AddProductEntryForm(pid);
                    form.ShowDialog();

                    if(form.IsDone)
                    {
                        WaitForm wait1 = new WaitForm(LoadAllProducts);
                        wait1.ShowDialog();

                        BindData(productList);
                    }
                }
                if(dgv.Rows[ri].Cells[dgvbtnentrieslist].ColumnIndex == ci)
                {
                    int pid = dgv.Rows[ri].Cells[0].Value.ToInt();
                    ProductEntriesListForm form = new ProductEntriesListForm(pid);
                    form.ShowDialog();
                }
                if(dgv.Columns[dgvbtneditbtn].Index == ci)
                {
                    int pid = dgv.Rows[ri].Cells[0].Value.ToInt();
                    EditProductForm form = new EditProductForm(pid);
                    form.ShowDialog();

                    if(form.IsDone)
                    {
                        WaitForm wait1 = new WaitForm(LoadAllProducts);
                        wait1.ShowDialog();

                        BindData(productList);
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
