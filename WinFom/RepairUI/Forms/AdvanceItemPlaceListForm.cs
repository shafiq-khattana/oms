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
    public partial class AdvanceItemPlaceListForm : Form
    {
        private int itemId = 0;
        private RepItem repItem = null;
        private List<ItemPlaceSKU> itemPlaceSKUs = null;
        private List<AdvanceItemRecord> advanceItemRecords = null;
        private List<AdvanceItemRecord> dispatchItemRecords = null;
        private List<RepItemConsumptionRecord> repItemConsumptionRecords = null;
        private string dgvconsumecancel = "dgvconsumecancel";
        public bool IsDone { get; set; }
        public AdvanceItemPlaceListForm(int repItemId)
        {
            InitializeComponent();
            itemId = repItemId;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadCommonData()
        {
            try
            {
                using (Context db = new Context())
                {
                    repItem = db.RepItems.Find(itemId);
                    repItem.Location = db.Locations.Find(repItem.LocationId);
                    itemPlaceSKUs = db.ItemPlaceSKUs.Where(a => a.RepItemId == itemId).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void LoadItemPlaceSKU()
        {
            try
            {
                using (Context db = new Context())
                {
                    if(itemPlaceSKUs != null)
                    {
                        itemPlaceSKUs.Clear();
                        itemPlaceSKUs = null;
                    }
                    itemPlaceSKUs = db.ItemPlaceSKUs.Where(a => a.RepItemId == itemId).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadAdvanceReceiveData()
        {
            try
            {
                if(advanceItemRecords != null)
                {
                    advanceItemRecords.Clear();
                    advanceItemRecords = null;
                }
                using (Context db = new Context())
                {
                    advanceItemRecords = db.AdvanceItemRecords.Include(a => a.Place)
                        .Where(a => a.RepItemId == itemId && a.Type == AdvanceItemRecordType.Received).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }

        }

        private void LoadAndBindRepItemConsumptionRecords()
        {
            try
            {
                WaitForm wait = new WaitForm(LoadRepItemConsumptionRecords);
                wait.ShowDialog();

                UpdateDgvRepItemConsumptionRecords();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void UpdateDgvRepItemConsumptionRecords()
        {
            repItemConsumptionRecordVMBindingSource.List.Clear();
            foreach (var item in repItemConsumptionRecords)
            {
                RepItemConsumptionRecordVM vm = new RepItemConsumptionRecordVM
                {
                    Id = item.Id,
                    AccountId = item.AccountId,
                    Dated = item.Dated.ToShortDateString(),
                    DaybookId = item.DaybookId,
                    Item = item.Item.Name,
                    Price = item.Price,
                    QtyConsumed = item.QtyConsumed,
                    Remarks = item.Remarks,
                    RepItemId = item.RepItemId
                };
                repItemConsumptionRecordVMBindingSource.List.Add(vm);
            }
        }
        private void LoadRepItemConsumptionRecords ()
        {
            if(repItemConsumptionRecords != null)
            {
                repItemConsumptionRecords.Clear();
                repItemConsumptionRecords = null;
            }
            using (Context db = new Context())
            {
                repItemConsumptionRecords = db.RepItemConsumptionRecords.Where(a => a.RepItemId == itemId).ToList();
                foreach (var item in repItemConsumptionRecords)
                {
                    item.Item = db.RepItems.Find(item.RepItemId);
                }
            }
        }
        private void LoadAdvanceDispatchData()
        {
            try
            {
                if(dispatchItemRecords != null)
                {
                    dispatchItemRecords.Clear();
                    dispatchItemRecords = null;
                }
                using (Context db = new Context())
                {
                    dispatchItemRecords = db.AdvanceItemRecords.Include(a => a.Place)
                        .Where(a => a.RepItemId == itemId && a.Type == AdvanceItemRecordType.Dispatched).ToList();

                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }

        }
        private void LoadAllData()
        {
            try
            {
                WaitForm wait1 = new WaitForm(LoadAdvanceReceiveData);
                wait1.ShowDialog();
                WaitForm wait2 = new WaitForm(LoadAdvanceDispatchData);
                wait2.ShowDialog();
                WaitForm wait3 = new WaitForm(LoadCommonData);
                wait3.ShowDialog();
                WaitForm wait4 = new WaitForm(LoadDamageItems);
                wait4.ShowDialog();

                WaitForm wait5 = new WaitForm(LoadPreItemRecords);
                wait5.ShowDialog();

                
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void UpdateAdvanceDgvItems()
        {
            try
            {
                advanceItemRecordVMBindingSource.List.Clear();
                foreach (var item in advanceItemRecords)
                {
                    AdvanceItemRecordVM vm = new AdvanceItemRecordVM
                    {
                        Dated = item.Dated.ToString(),
                        Id = item.Id,
                        Place = item.Place.Name,
                        Qty = item.Qty,
                        Remarks = item.Remarks,
                        RepPlaceId = item.RepPlaceId
                    };
                    advanceItemRecordVMBindingSource.List.Add(vm);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void UpdateDispatchDgvItems()
        {
            try
            {
                dispatchItemRecordVMBindingSource.List.Clear();
                foreach (var item in dispatchItemRecords)
                {
                    DispatchItemRecordVM vm = new DispatchItemRecordVM
                    {
                        Dated = item.Dated.ToString(),
                        Id = item.Id,
                        Place = item.Place.Name,
                        Qty = item.Qty,
                        Remarks = item.Remarks,
                        RepPlaceId = item.RepPlaceId
                    };
                    dispatchItemRecordVMBindingSource.List.Add(vm);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadAndBindCommonAndAllData()
        {
            try
            {
                LoadAllData();
                BindAllData();
                decimal advanceSKU = 0;
                if (itemPlaceSKUs.Count > 0)
                    advanceSKU = itemPlaceSKUs.Sum(a => a.SKU);

                tbAdvanceSKU.Text = advanceSKU.ToString("n1");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private List<RepItemDamageRecord> repItemDamageRecords = null;
        private void LoadDamageItems()
        {
            try
            {
                if(repItemDamageRecords != null)
                {
                    repItemDamageRecords.Clear();
                    repItemDamageRecords = null;
                }
                using (Context db = new Context())
                {
                    repItemDamageRecords = db.RepItemDamageRecords.Where(a => a.RepItemId == itemId).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindAllData()
        {
            UpdateAdvanceDgvItems();
            UpdateDispatchDgvItems();
            UpdateDamageDgvItems();
            UpdatePreAddedItems();
            LoadAndBindRepItemConsumptionRecords();
        }
        private List<RepItemPreAddRecord> repItemPreAddRecords = null;
        private void LoadPreItemRecords()
        {
            try
            {
                if(repItemPreAddRecords != null)
                {
                    repItemPreAddRecords.Clear();
                    repItemPreAddRecords = null;
                }
                using (Context db = new Context())
                {
                    repItemPreAddRecords = db.RepItemPreAddRecords.Where(a => a.RepItemId == itemId).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void UpdatePreAddedItems()
        {
            try
            {
                repItemPreAddRecordVMBindingSource.List.Clear();
                foreach (var item in repItemPreAddRecords)
                {
                    RepItemPreAddRecordVM vm = new RepItemPreAddRecordVM
                    {
                        Dated = item.Dated.ToShortDateString(),
                        Id = item.Id,
                        Qty = item.Qty,
                        Remarks = item.Remarks
                    };
                    repItemPreAddRecordVMBindingSource.List.Add(vm);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void UpdateDamageDgvItems()
        {
            try
            {
                repItemDamageRecordVMBindingSource.List.Clear();
                foreach (var item in repItemDamageRecords)
                {
                    RepItemDamageRecordVM vm = new RepItemDamageRecordVM
                    {
                        Dated = item.Dated.ToShortDateString(),
                        Id = item.Id,
                        Qty = item.Qty,
                        Remarks = item.Remarks
                    };
                    repItemDamageRecordVMBindingSource.List.Add(vm);
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
                LoadAndBindCommonAndAllData();
                tbItem.Text = string.Format("{0} -{1}", repItem.Name, repItem.Location.Name);
                IsDone = false;
                Gujjar.AddDatagridviewButton(dgvItemConsumption, dgvconsumecancel, "Cancel", "Cancel", 80);
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
                AddAdvanceItemForm form = new AddAdvanceItemForm(itemId, tbItem.Text);
                form.ShowDialog();

                if(form.IsDone)
                {
                    WaitForm wait = new WaitForm(LoadAdvanceReceiveData);
                    wait.ShowDialog();
                    UpdateAdvanceDgvItems();

                    WaitForm wait2 = new WaitForm(LoadItemPlaceSKU);
                    wait2.ShowDialog();
                    tbAdvanceSKU.Text = itemPlaceSKUs.Sum(a => a.SKU).ToString("n1");
                    IsDone = true;
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnDispatch_Click(object sender, EventArgs e)
        {
            try
            {
                AddDispatchItemForm form = new AddDispatchItemForm(itemId, tbItem.Text, itemPlaceSKUs);
                form.ShowDialog();

                if(form.IsDone)
                {
                    WaitForm wait1 = new WaitForm(LoadAdvanceDispatchData);
                    wait1.ShowDialog();
                    UpdateDispatchDgvItems();

                    WaitForm wait2 = new WaitForm(LoadItemPlaceSKU);
                    wait2.ShowDialog();
                    tbAdvanceSKU.Text = itemPlaceSKUs.Sum(a => a.SKU).ToString("n1");
                    IsDone = true;
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddDamage_Click(object sender, EventArgs e)
        {
            try
            {
                AddItemDamageRecordForm form = new AddItemDamageRecordForm(itemId, tbItem.Text);
                form.ShowDialog();

                if(form.IsDone)
                {
                    WaitForm wait1 = new WaitForm(LoadDamageItems);
                    wait1.ShowDialog();
                    UpdateDamageDgvItems();
                    IsDone = true;
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddPre_Click(object sender, EventArgs e)
        {
            try
            {
                AddRepItemPreAddForm form = new AddRepItemPreAddForm(itemId, tbItem.Text);
                form.ShowDialog();

                if(form.IsDone)
                {
                    WaitForm wait = new WaitForm(LoadPreItemRecords);
                    wait.ShowDialog();
                    UpdatePreAddedItems();
                    IsDone = true;
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnItemConsumption_Click(object sender, EventArgs e)
        {
            try
            {
                RepItemConsumptionForm form = new RepItemConsumptionForm(itemId);
                form.ShowDialog();

                if(form.IsDone)
                {
                    LoadAndBindRepItemConsumptionRecords();
                    IsDone = true;
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dgvItemConsumption_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                int ci = e.ColumnIndex;

                if(ri == -1 || ri == dgvItemConsumption.NewRowIndex)
                {
                    return;
                }

                if(dgvItemConsumption.Columns[dgvconsumecancel].Index == ci)
                {
                    int id = dgvItemConsumption.Rows[ri].Cells[0].Value.ToInt();
                    
                    if(!Helper.ConfirmAdminPassword())
                    {
                        return;
                    }
                    DialogResult rest = Gujjar.ConfirmYesNo("Please confirm before to proceed. Are you really want to delete this consumption record?");
                    if (rest == DialogResult.No)
                        return;

                    using (Context db = new Context())
                    {
                        var cRec = db.RepItemConsumptionRecords.Find(id);
                        var rItem = db.RepItems.Find(cRec.RepItemId);
                        var daybook = db.DayBooks.Find(cRec.DaybookId);
                        rItem.SKU += cRec.QtyConsumed;
                        rItem.TotalValue += cRec.Price;
                        rItem.UnitValue = rItem.TotalValue / rItem.SKU;

                        db.Entry(rItem).State = EntityState.Modified;
                        db.Entry(cRec).State = EntityState.Deleted;
                        db.SaveChanges();

                        Helper.ReverseTransaction(daybook);

                        Gujjar.InfoMsg("Consumption record is deleted and financial entries are reversed");
                        IsDone = true;

                        LoadAndBindRepItemConsumptionRecords();
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
