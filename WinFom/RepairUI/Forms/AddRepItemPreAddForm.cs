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
    public partial class AddRepItemPreAddForm : Form
    {
        private int itemId = 0;
        private string itemName = "";
        public bool IsDone { get; set; }
        public AddRepItemPreAddForm(int repItemId, string itemName)
        {
            InitializeComponent();
            itemId = repItemId;
            this.itemName = itemName;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                tbItem.Text = itemName;
                Gujjar.TB4(pMain);
                tbRemarks.Text = "N/A";
                Gujjar.TBOptional(tbRemarks);
                Gujjar.NumbersOnly(tbQty);
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

                decimal qty = tbQty.Text.ToDecimal();
                if (qty == 0)
                {
                    throw new Exception("Qty is zero");
                }
                if (!Helper.ConfirmUserPassword())
                {
                    return;
                }

                DialogResult res = Gujjar.ConfirmYesNo("Please confirm..!! ");
                if (res == DialogResult.No)
                    return;

                using (Context db = new Context())
                {
                    RepItemPreAddRecord record = new RepItemPreAddRecord
                    {
                        Dated = dtp.Value,
                        Id = 0,
                        Item = null,
                        Qty = qty,
                        Remarks = tbRemarks.Text,
                        RepItemId = itemId
                    };
                    db.RepItemPreAddRecords.Add(record);
                    db.SaveChanges();
                    IsDone = true;
                    Gujjar.InfoMsg("Record is inserted and saved in database successfully");
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
