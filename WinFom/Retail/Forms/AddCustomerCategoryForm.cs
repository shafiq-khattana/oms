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
    public partial class AddCustomerCategoryForm : Form
    {
        public int CateId = 0;
        public AddCustomerCategoryForm()
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
                Gujjar.NumbersOnly(tbDiscPercentage);
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

                CustomerCategory cat = new CustomerCategory
                {
                    Id = 0,
                    Customers = null,
                    Discount = tbDiscPercentage.Text.ToDecimal(),
                    Title = tbTitle.Text
                };

               
                using (Context db = new Context())
                {
                    var dbObj = db.CustomerCategories.FirstOrDefault(a => a.Title.ToLower() == cat.Title.ToLower());
                    if(dbObj != null)
                    {
                        throw new Exception(string.Format("Customer Category with this name ({0}) already exists in database", dbObj.Title));
                    }

                    cat = db.CustomerCategories.Add(cat);
                    db.SaveChanges();

                    Gujjar.InfoMsg("Category is added in database successfully");
                    CateId = cat.Id;
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
