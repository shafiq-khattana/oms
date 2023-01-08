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
using Model.EFiling.Model;

namespace WinFom.RepairUI.Forms
{
    public partial class AddEFileCategoryForm : Form
    {
        public int CategoryId { get; set; }
        public AddEFileCategoryForm()
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
                CategoryId = 0;
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
                string title = textBox1.Text;
                if(string.IsNullOrEmpty(title))
                {
                    textBox1.BackColor = Color.Pink;
                    textBox1.Focus();
                    throw new Exception("Please enter category title");
                }
                using (Context db = new Context())
                {
                    var obj = db.EFCategories.ToList().FirstOrDefault(a => a.Title.ToLower().Equals(title.ToLower()));
                    if(obj != null)
                    {
                        throw new Exception("Category already added in database");
                    }

                    EFCategory efCat = new EFCategory
                    {
                        Id = 0,
                        EFIles = null,
                        Title = title
                    };
                    efCat = db.EFCategories.Add(efCat);
                    db.SaveChanges();
                    CategoryId = efCat.Id;
                    Gujjar.InfoMsg("Category is added successfully");
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
