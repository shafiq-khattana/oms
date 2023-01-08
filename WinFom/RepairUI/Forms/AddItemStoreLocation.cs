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
    public partial class AddItemStoreLocation : Form
    {
        public int LocationId = 0;
        public AddItemStoreLocation()
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

                StoreLocation cate = new StoreLocation
                {
                    LocationName = tbName.Text,                    
                    Id = 0,
                    LocationNameUrdu = ""
                };

               
                using (Context db = new Context())
                {
                    var dbObj = db.StoreLocations.FirstOrDefault(a => a.LocationName.ToLower() == cate.LocationName.ToLower());
                    if (dbObj != null)
                    {
                        throw new Exception(string.Format("Storing Location with this name ({0}) already exists in database", dbObj.LocationName));
                    }

                    cate = db.StoreLocations.Add(cate);
                    db.SaveChanges();

                    Gujjar.InfoMsg("Storing Location is added in database successfully");
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
