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
using WinFom.RepairUI.Reports.Model;
using WinFom.RepairUI.Reports.XtraReport;
using Zen.Barcode;
using DevExpress.XtraReports.UI;
using System.Data.Entity;
using Model.Repair.ViewModel;

namespace WinFom.RepairUI.Forms
{
    public partial class AddPurchasingItemEntryForm : Form
    {       
        public ItemPurchaseEntryVM PurchaseVM = null;
        public bool IsDone { get; set; }
        public AddPurchasingItemEntryForm()
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
                Gujjar.NumbersOnly(tbQty);
                Gujjar.NumbersOnly(tbTotalPrice);
                Gujjar.NumbersOnly(tbUnitPrice);

                LoadFormData();

                Gujjar.TBOptional(tbRemarks);
                tbRemarks.Text = "N/A";
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void LoadFormData()
        {
            try
            {               
                WaitForm wait = new WaitForm(LoadItems);
                wait.ShowDialog();
                BindCbItems();
                
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
        
        private List<RepItem> repItems = null;

        private void LoadItems()
        {
            try
            {
                using (Context db = new Context())
                {
                    if(repItems != null)
                    {
                        repItems.Clear();
                        repItems = null;
                    }
                    repItems = db.RepItems.Include(a => a.ItemCategory).Include(a => a.Location)
                        .OrderBy(a => a.Name).AsParallel().ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void BindCbItems()
        {
            cbItems.DataSource = repItems;
            cbItems.DisplayMember = "Name";
            cbItems.ValueMember = "Id";
        }

        

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {                
                AddRepItemForm form = new AddRepItemForm();
                form.ShowDialog();

                int id = form.ItemId;
                if(id != 0)
                {
                    LoadItems();
                    BindCbItems();

                    cbItems.SelectedItem = cbItems.Items.OfType<RepItem>()
                        .FirstOrDefault(a => a.Id == id);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        

        private List<RepPlace> repPlaces = null;
        private void BindCbPlaces()
        {
            //cbRepairPlace.DataSource = repPlaces;
            //cbRepairPlace.DisplayMember = "Name";
            //cbRepairPlace.ValueMember = "Id";
        }

        private void LoadPlaces()
        {
            try
            {
                if(repPlaces != null)
                {
                    repPlaces.Clear();
                    repPlaces = null;
                }
                using (Context db = new Context())
                {
                    repPlaces = db.RepPlaces.OrderBy(a => a.Name).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void cbItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbItems.SelectedIndex == -1)
                return;

            RepItem repItem = cbItems.SelectedItem as RepItem;
            textBox1.Text = repItem.ItemCategory.Title;
            textBox2.Text = repItem.Location.Name;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }
                
                if(cbItems.SelectedIndex == -1)
                {
                    throw new Exception("Please choose item");
                }               

                
                DialogResult res = Gujjar.ConfirmYesNo("Are you confirm..!!");
                if (res == DialogResult.No)
                    return;

                
                RepItem repItem = cbItems.SelectedItem as RepItem;

                PurchaseVM = new ItemPurchaseEntryVM
                {
                    Id = repItem.Id,
                    Qty = tbQty.Text.ToDecimal(),
                    RepItem = repItem.Name,
                    TotalPrice = tbTotalPrice.Text.ToDecimal(),
                    UnitPrice = tbUnitPrice.Text.ToDecimal(),
                    Category = repItem.ItemCategory.Title,
                    Place = repItem.Location.Name,
                    Remarks = tbRemarks.Text
                };
                Close();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbUnitPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal qty = 0;
                decimal unitPrice = 0;
                if(!string.IsNullOrEmpty(tbQty.Text))
                {
                    qty = tbQty.Text.ToDecimal();
                }
                if(!string.IsNullOrEmpty(tbUnitPrice.Text))
                {
                    unitPrice = tbUnitPrice.Text.ToDecimal();
                }
                decimal totalPrice = qty * unitPrice;
                tbTotalPrice.Text = totalPrice.ToString("n1");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbTotalPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal qty = 0;
                decimal totalPrice = 0;
                decimal unitPrice = 0;
                if (!string.IsNullOrEmpty(tbQty.Text))
                {
                    qty = tbQty.Text.ToDecimal();
                }
                if (!string.IsNullOrEmpty(tbTotalPrice.Text))
                {
                    totalPrice = tbTotalPrice.Text.ToDecimal();
                }
                if(qty > 0)
                {
                    unitPrice = totalPrice / qty;
                }
                tbUnitPrice.Text = unitPrice.ToString("n1");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
