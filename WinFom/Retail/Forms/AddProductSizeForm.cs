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
using Model.Retail.Model;
namespace WinFom.Retail.Forms
{
    public partial class AddProductSizeForm : Form
    {
        public int ProductSizeId = 0;
        public AddProductSizeForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                Gujjar.TB4(pMain);              

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }

                ProductSize pSize = new ProductSize
                {
                    Title = tbTitle.Text,                    
                    
                };

               
                using (Context db = new Context())
                {
                    var dbObj = db.ProductSizes.FirstOrDefault(a => a.Title.ToLower() == pSize.Title.ToLower());
                    if(dbObj != null)
                    {
                        throw new Exception(string.Format("Product size with this name ({0}) already exists in database", dbObj.Title));
                    }

                    pSize = db.ProductSizes.Add(pSize);
                    db.SaveChanges();

                    Gujjar.InfoMsg("Product size is added in database successfully");
                    ProductSizeId = pSize.Id;
                    Close();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
