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
using System.Data.Entity;
using Model.Repair.ViewModel;
using Model.Financials.Model;

namespace WinFom.RepairUI.Forms
{
    public partial class RepItemMultiSelectForm : Form
    {
        private List<RepItem> itemList = null;
        public List<MultiRepItemVM> SelectedItemList = null;
        public RepItemMultiSelectForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void LoadItems()
        {
            try
            {
                if(itemList != null)
                {
                    itemList.Clear();
                    itemList = null;
                }
                using (Context db = new Context())
                {
                    itemList = db.RepItems.Include(a => a.Location).Include(a => a.ItemCategory)
                        .AsParallel().ToList().OrderBy(a => a.ItemCategory.Title).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
                
            }
        }

        private void BindDataDgv()
        {
            try
            {
                foreach (var item in itemList)
                {
                    MultiRepItemVM vm = new MultiRepItemVM
                    {
                        Id = item.Id,
                        Qty = 0,
                        Selected = false,
                        Remarks = "N/A",
                        Title = string.Format("{0}-{1}-{2}", item.ItemCategory.Title, item.Name, item.Location.Name)
                    };
                    multiRepItemVMBindingSource.List.Add(vm);
                }
                tbTotalEntries.Text = itemList.Count.ToString();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void UpdateDgv()
        {
            try
            {
                foreach (var item in itemList)
                {

                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait1 = new WaitForm(LoadItems);
                wait1.ShowDialog();

                BindDataDgv();
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

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.RowIndex == dgv.NewRowIndex || e.ColumnIndex != 2)
                return;

            int id = dgv.Rows[e.RowIndex].Cells[0].Value.ToInt();

            MultiRepItemVM model = multiRepItemVMBindingSource.List.OfType<MultiRepItemVM>()
                .FirstOrDefault(a => a.Id == id);
            if(model.Qty <= 0)
            {
                model.Qty = 0;
                model.Selected = false;
            }
            else
            {
                model.Selected = true;
            }
            int slectedCount = multiRepItemVMBindingSource.List.OfType<MultiRepItemVM>()
                .Count(a => a.Selected);
            tbTotalAmount.Text = slectedCount.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult confirm = Gujjar.ConfirmYesNo("Are you confirmed?");
                if (confirm == DialogResult.No)
                    return;

                SelectedItemList = multiRepItemVMBindingSource.List.OfType<MultiRepItemVM>()
                    .Where(a => a.Selected).ToList();
                Close();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
