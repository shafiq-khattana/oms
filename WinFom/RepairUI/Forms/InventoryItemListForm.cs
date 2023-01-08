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

namespace WinFom.RepairUI.Forms
{
    public partial class InventoryItemListForm : Form
    {
        private List<RepItem> items = null;
        private string dgvitemid = "dgvitemid";
        private string dgvitemedit = "dgvitemeditmode1231";
        public InventoryItemListForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadRepItems()
        {
            try
            {
                if (items != null)
                {
                    items.Clear();
                    items = null;
                }
                using (Context db = new Context())
                {
                    items = db.RepItems
                        .Include(a => a.ItemCategory)
                        .Include(a => a.Location)
                        .Include(a => a.StoreLocation)
                        .Include(a => a.AdvaneItemRecords)
                        .Include(a => a.ItemDamageRecords)
                        .Include(a => a.RepItemPreAddRecords)
                        .AsParallel().ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private ItemVM GetItemVM(RepItem item)
        {
            ItemVM vm = new ItemVM
            {
                AdvanceCount = item.AdvaneItemRecords.Where(a => a.Type == AdvanceItemRecordType.Received).Sum(a => a.Qty) - item.AdvaneItemRecords.Where(a => a.Type == AdvanceItemRecordType.Dispatched).Sum(a => a.Qty),
                Id = item.Id,
                WorkLoc = item.Location.Name,
                StoreLoc = item.StoreLocation.LocationName,
                Name = string.Format("{0}-{1}", item.ItemCategory.Title, item.Name),
                SKU = item.SKU,
                TotalValue = item.TotalValue,
                UnitValue = item.UnitValue,
                UR = item.UR,
                USCount = item.ItemDamageRecords.Sum(a => a.Qty),
                PreAdded = item.RepItemPreAddRecords.Sum(a => a.Qty)
            };
            vm.SKU += vm.AdvanceCount;
            vm.SKU += vm.PreAdded;
            vm.TotalValue = vm.SKU * vm.UnitValue;
            return vm;
        }
        private void UpdateDgv()
        {
            try
            {
                itemVMBindingSource.List.Clear();
                foreach (var item in items)
                {
                    var vm = GetItemVM(item);
                    itemVMBindingSource.List.Add(vm);
                }

                //tbSACount.Text = items.Sum(a => a.SACount).ToString("n0");
                List<ItemVM> items2 = itemVMBindingSource.List.OfType<ItemVM>().ToList();
                tbStockValue.Text = items2.Sum(a => a.TotalValue).ToString("n2");
                tbTotalItems.Text = (items2.Sum(a => a.SKU) + items2.Sum(a => a.USCount) + items2.Sum(a => a.UR)).ToString("n1");
                tbURCount.Text = items2.Sum(a => a.UR).ToString("n1");
                tbUSCount.Text = items2.Sum(a => a.USCount).ToString("n1");
                tbSKU.Text = items2.Sum(a => a.SKU).ToString("n1");
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
                WaitForm wait = new WaitForm(LoadRepItems);
                wait.ShowDialog();
                Gujjar.AddDatagridviewButton(dgv, dgvitemedit, "Edit", "Edit");
                Gujjar.AddDatagridviewButton(dgv, dgvitemid, "Extra", "Extra", 80);
                UpdateDgv();
                Helper.IsOkApplied();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddRepItemForm form = new AddRepItemForm();
                form.ShowDialog();
                if (form.ItemId != 0)
                {
                    WaitForm wait = new WaitForm(LoadRepItems);
                    wait.ShowDialog();

                    UpdateDgv();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                int ci = e.ColumnIndex;

                if (ri == -1 || ri == dgv.NewRowIndex)
                {
                    return;
                }

                if(dgv.Columns[dgvitemedit].Index == ci)
                {
                    DialogResult res = Gujjar.ConfirmYesNo("Are you sured");
                    if (res == DialogResult.No)
                        return;
                    int itemId = dgv.Rows[ri].Cells[0].Value.ToInt();

                    var obj = itemVMBindingSource.List.OfType<ItemVM>()
                        .FirstOrDefault(a => a.Id == itemId);

                    RepItemEditForm form = new RepItemEditForm(obj.Id);
                    form.ShowDialog();

                    if(form.IsDone)
                    {
                        WaitForm wait = new WaitForm(LoadRepItems);
                        wait.ShowDialog();

                        UpdateDgv();
                    }
                }
                if (dgv.Columns[dgvitemid].Index == ci)
                {
                    int itemId = dgv.Rows[ri].Cells[0].Value.ToInt();
                    AdvanceItemPlaceListForm form = new AdvanceItemPlaceListForm(itemId);
                    form.ShowDialog();

                    if (form.IsDone)
                    {
                        var obj = itemVMBindingSource.List.OfType<ItemVM>().FirstOrDefault(a => a.Id == itemId);
                        int index = itemVMBindingSource.IndexOf(obj);
                        itemVMBindingSource.Remove(obj);
                        RepItem repItem = null;
                        using (Context db = new Context())
                        {
                            repItem = db.RepItems
                                .Include(a => a.ItemCategory)
                                .Include(a => a.Location)
                                .Include(a => a.StoreLocation)
                                .Include(a => a.AdvaneItemRecords)
                                .Include(a => a.ItemDamageRecords)
                                .Include(a => a.RepItemPreAddRecords)
                                .AsParallel().FirstOrDefault(a => a.Id == itemId);
                        }
                        var vm = GetItemVM(repItem);
                        itemVMBindingSource.Insert(index, vm);

                        dgv.Refresh();
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
