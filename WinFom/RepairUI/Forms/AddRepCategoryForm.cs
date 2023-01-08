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
using Model.Repair.Model;

namespace WinFom.RepairUI.Forms
{
    public partial class AddRepCategoryForm : Form
    {
        public int CategoryId = 0;
        public AddRepCategoryForm()
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

                ItemCategory cate = new ItemCategory
                {
                    Title = tbTitle.Text,                    
                    
                };

               
                using (Context db = new Context())
                {
                    var dbObj = db.ItemCategories.FirstOrDefault(a => a.Title.ToLower() == cate.Title.ToLower());
                    if (dbObj != null)
                    {
                        throw new Exception(string.Format("Category with this name ({0}) already exists in database", dbObj.Title));
                    }

                    cate = db.ItemCategories.Add(cate);
                    db.SaveChanges();

                    Gujjar.InfoMsg("Category is added in database successfully");
                    CategoryId = cate.Id;
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
