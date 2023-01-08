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
    public partial class AddRepairEntryDispatchForm : Form
    {       
        public RepairEntryVMDispatch EntryVM = null;
        public AddRepairEntryDispatchForm()
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
                
                LoadFormData();
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
                tbRemarks.Text = "N/A";
                Gujjar.TBOptional(tbRemarks);
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

        

        private void BindCbPlaces()
        {
            //cbRepairPlace.DataSource = repPlaces;
            //cbRepairPlace.DisplayMember = "Name";
            //cbRepairPlace.ValueMember = "Id";
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

                EntryVM = new RepairEntryVMDispatch
                {
                    Id = repItem.Id,
                    DispatchingQty = tbQty.Text.ToDecimal(),
                    Category = repItem.ItemCategory.Title,
                    RepItem = repItem.Name,
                    WorkingPlace = repItem.Location.Name,
                    Remarks = tbRemarks.Text
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
