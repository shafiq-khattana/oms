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
using Model.Entertainment.Model;
using Model.Entertainment.ViewModel;

namespace WinFom.EntertainmentUI.Forms
{
    public partial class AddEItemVMForm : Form
    {
        private List<EntItem> entItems = null;
        private EntItem entItem = null;
        public ePurchaseItemVM eItemVM = null;

        public AddEItemVMForm(List<EntItem> items)
        {
            InitializeComponent();
            entItems = items;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                cbEItems.DataSource = entItems;
                cbEItems.DisplayMember = "Title";
                cbEItems.ValueMember = "Id";

                Gujjar.TB4(pMain);
                Gujjar.NumbersOnly(tbQty);
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

        private void cbEItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEItems.SelectedIndex == -1)
            {
                entItem = null;
                return;
            }
            entItem = cbEItems.SelectedItem as EntItem;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }
                if(entItem == null)
                {
                    throw new Exception("Please select Ent Item");
                }
                DialogResult res = Gujjar.ConfirmYesNo("Are you sure to add this item");
                if (res == DialogResult.No)
                    return;

                eItemVM = new ePurchaseItemVM
                {
                    Id = entItem.Id,
                    Qty = tbQty.Text.ToDecimal(),
                    Item = entItem.Title
                };
                Close();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
