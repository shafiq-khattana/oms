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

namespace WinFom.ReadyStuff.Forms
{
    public partial class AddBharthiType : Form
    {
        public int BharthiTypeId = 0;
        public AddBharthiType()
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
                Gujjar.NumbersOnly(tbUnitQty);
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

                BharthiType btype = new BharthiType
                {
                    Title = tbTitle.Text,
                    UnitQty = tbUnitQty.Text.ToDecimal()
                };

                using (Context db = new Context())
                {
                    var obj = db.BharthiTypes.ToList().FirstOrDefault(a => a.Title.Equals(btype.Title, StringComparison.OrdinalIgnoreCase));
                    if(obj != null)
                    {
                        throw new Exception("Packing type already exists in database.");
                    }

                    btype = db.BharthiTypes.Add(btype);
                    db.SaveChanges();
                    Gujjar.InfoMsg("Packing type is added in database");
                    BharthiTypeId = btype.Id;
                    Close();
                }
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
    }
}
