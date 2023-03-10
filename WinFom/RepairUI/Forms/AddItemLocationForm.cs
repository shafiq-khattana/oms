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
    public partial class AddItemLocationForm : Form
    {
        public int LocationId = 0;
        public AddItemLocationForm()
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

                Location cate = new Location
                {
                    Name = tbTitle.Text,                    
                    Id = 0
                };

               
                using (Context db = new Context())
                {
                    var dbObj = db.Locations.FirstOrDefault(a => a.Name.ToLower() == cate.Name.ToLower());
                    if (dbObj != null)
                    {
                        throw new Exception(string.Format("Location with this name ({0}) already exists in database", dbObj.Name));
                    }

                    cate = db.Locations.Add(cate);
                    db.SaveChanges();

                    Gujjar.InfoMsg("Location is added in database successfully");
                    LocationId = cate.Id;
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
