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
using Model.Repair.Model;

namespace WinFom.RepairUI.Forms
{
    public partial class RepItemEditForm : Form
    {
        public bool IsDone { get; set; }
        private int repItemId = 0;
        private RepItem repItem = null;
        public RepItemEditForm(int itemId)
        {
            InitializeComponent();
            repItemId = itemId;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                using (Context db = new Context())
                {
                    repItem = db.RepItems.Find(repItemId);
                }

                tbPrevUnitPrice.Text = repItem.UnitValue.ToString("n1");
                Gujjar.TB4(pMain);
                Gujjar.NumbersOnly(tbNewUnitPrice);
                IsDone = false;
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
                    throw new Exception("Please fill all text fields");
                }

                DialogResult res = Gujjar.ConfirmYesNo("Are you sured");
                if (res == DialogResult.No)
                    return;

                using (Context db = new Context())
                {
                    var itemdb = db.RepItems.Find(repItemId);
                    itemdb.UnitValue = tbNewUnitPrice.Text.ToDecimal();
                    itemdb.TotalValue = itemdb.UnitValue * itemdb.SKU;

                    db.Entry(itemdb).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                IsDone = true;
                Close();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
