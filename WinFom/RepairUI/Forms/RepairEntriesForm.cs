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
using WinFom.RepairUI.Forms;

namespace WinFom.RepairUI.Forms
{
    public partial class RepairEntriesForm : Form
    {
        private string btndgvdel = "btndgvdelete";
        public bool IsDone = false;
        private RepPlace place = null;
        private List<RepPlace> places = null;
        private int repairId = 0;
        public RepairEntriesForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadLocations()
        {
            try
            {
                using (Context db = new Context())
                {
                    if (places != null)
                    {
                        places.Clear();
                        places = null;
                    }
                    places = db.RepPlaces.OrderBy(a => a.Name).ToList();

                    var obj = db.RepairDispatchRecords.OrderByDescending(a => a.Id).FirstOrDefault();
                    if (obj != null)
                    {
                        repairId = obj.Id + 1;
                    }
                    else
                    {
                        repairId = 1;
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindCbLocations()
        {
            cbLocations.DataSource = places;
            cbLocations.DisplayMember = "Name";
            cbLocations.ValueMember = "Id";
        }
        private void LoadEntries()
        {
            try
            {

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
                var list = repairEntryVMDispatchBindingSource.List.OfType<RepairEntryVMDispatch>().ToList();
                tbTotalCount.Text = list.Sum(a => a.DispatchingQty).ToString("n1");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void LoadAndUpdate()
        {
            try
            {
                WaitForm wait = new WaitForm(LoadLocations);
                wait.ShowDialog();
                BindCbLocations();
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
                Gujjar.AddDatagridviewButton(dgv, btndgvdel, "Delete", "Delete", 80);
                LoadAndUpdate();
                tbRepairId.Text = repairId.ToString();
                Helper.IsOkApplied();
                Gujjar.TB4(panel1);
                tbBillId.Text = "N/A";
                Gujjar.TBOptional(tbBillId);
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
                //AddRepairEntryDispatchForm form = new AddRepairEntryDispatchForm();
                //form.ShowDialog();

                //if (form.EntryVM != null)
                //{
                //    //LoadAndUpdate();
                //    var list = repairEntryVMDispatchBindingSource.List.OfType<RepairEntryVMDispatch>().ToList();
                //    var obj = form.EntryVM;

                //    var obj2 = list.FirstOrDefault(a => a.Id == obj.Id);
                //    if (obj2 != null)
                //    {
                //        obj2.DispatchingQty += obj.DispatchingQty;
                //    }
                //    else
                //    {
                //        repairEntryVMDispatchBindingSource.List.Add(obj);
                //    }
                //    dgv.Refresh();
                //    UpdateDgv();
                //}

                RepItemMultiSelectForm form = new RepItemMultiSelectForm();
                form.ShowDialog();

                List<MultiRepItemVM> itemList = form.SelectedItemList;
                if (itemList != null && itemList.Count > 0)
                {
                    foreach (var item in itemList)
                    {
                        string[] tokens = item.Title.Split('-');
                        RepairEntryVMDispatch vm = new RepairEntryVMDispatch
                        {
                            Category = tokens[0],
                            DispatchingQty = (decimal)item.Qty,
                            Id = item.Id,
                            PlaceId = 0,
                            Remarks = item.Remarks,
                            RepItem = tokens[1],
                            WorkingPlace = tokens[2]
                        };

                        var objItem = repairEntryVMDispatchBindingSource.List.OfType<RepairEntryVMDispatch>()
                            .FirstOrDefault(a => a.Id == item.Id);
                        if (objItem != null)
                        {
                            objItem.DispatchingQty += (decimal)item.Qty;
                        }
                        else
                            repairEntryVMDispatchBindingSource.List.Add(vm);
                    }

                    dgv.Refresh();
                    UpdateDgv();
                }
            }
            catch (Exception ep)
            {
                Gujjar.ErrMsg(ep);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int ri = e.RowIndex;
            int ci = e.ColumnIndex;

            try
            {
                if (ri == -1 || ri == dgv.NewRowIndex)
                    return;

                if (dgv.Columns[btndgvdel].Index == ci)
                {
                    DialogResult res = Gujjar.ConfirmYesNo("Are you sured to delete this record?");
                    if (res == DialogResult.No)
                        return;

                    int id = dgv.Rows[ri].Cells[0].Value.ToInt();

                    var obj = repairEntryVMDispatchBindingSource.List.OfType<RepairEntryVMDispatch>()
                        .FirstOrDefault(a => a.Id == id);

                    repairEntryVMDispatchBindingSource.List.Remove(obj);
                    dgv.Refresh();

                    UpdateDgv();
                }

                //if(dgv.Columns[btndgvreceive].Index == ci)
                //{
                //    int id = dgv.Rows[ri].Cells[0].Value.ToInt();
                //    var obj = repairEntryVMBindingSource.List.OfType<RepairEntryVM>()
                //        .FirstOrDefault(a => a.Id == id);
                //    if(obj.Status == ReceiveStatus.Completed || obj.Status == ReceiveStatus.Partial)
                //    {
                //        throw new Exception("Entry already has been received");
                //    }

                //    DialogResult res = Gujjar.ConfirmYesNo("Are you sured to update this record?");
                //    if (res == DialogResult.No)
                //        return;




                //    ReceiveDispatchedEntryForm form = new ReceiveDispatchedEntryForm(id);
                //    form.ShowDialog();

                //    if(form.IsDone)
                //    {
                //        LoadAndUpdate();
                //    }
                //}
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddLocation_Click(object sender, EventArgs e)
        {
            try
            {
                AddRepPlaceForm form = new AddRepPlaceForm();
                form.ShowDialog();
                int id = form.PlaceId;
                if (id != 0)
                {
                    WaitForm wait = new WaitForm(LoadLocations);
                    wait.ShowDialog();

                    BindCbLocations();
                    cbLocations.SelectedItem = cbLocations.Items.OfType<RepPlace>()
                        .FirstOrDefault(a => a.Id == id);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddEntry_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Gujjar.IsValidForm(panel1))
                {
                    throw new Exception("Please fill all text fields");
                }
                string toperson = tbTOPerson.Text;
                if (string.IsNullOrEmpty(toperson))
                {
                    tbTOPerson.BackColor = Color.Pink;
                    tbTOPerson.Focus();
                    throw new Exception("Please enter taking out person");
                }
                if (place == null)
                {
                    throw new Exception("Please specify repairing workshop information");
                }

                if (!Helper.ConfirmAdminPassword())
                {
                    return;
                }
                using (Context db = new Context())
                {
                    if (tbBillId.Text != "N/A")
                    {
                        var obj2 = db.RepairDispatchRecords.FirstOrDefault(a => a.BillNo == tbBillId.Text && a.RepPlaceId == place.Id);
                        if (obj2 != null)
                        {
                            throw new Exception(string.Format("Bill no ({0}) with repairing shop ({1}) already added in database",
                                tbBillId.Text, place.Name));
                        }
                    }

                    var list = repairEntryVMDispatchBindingSource.List.OfType<RepairEntryVMDispatch>().ToList();

                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            RepairDispatchRecord dispatchRecord = new RepairDispatchRecord
                            {
                                BillNo = tbBillId.Text,
                                BillPaid = 0,
                                Date = dtp.Value,
                                Id = 0,
                                Place = null,
                                ReceivedItems = 0,
                                RemainingItems = list.Sum(a => a.DispatchingQty),
                                RepEntries = null,
                                RepPlaceId = place.Id,
                                TOPerson = tbTOPerson.Text,
                                TotalItems = list.Sum(a => a.DispatchingQty),
                                Unum = Helper.Unum,
                                Status = RepairDispatchStatus.DispatchedOnly
                            };
                            db.RepairDispatchRecords.Add(dispatchRecord);
                            db.SaveChanges();

                            foreach (var item in list)
                            {
                                RepEntry entry = new RepEntry
                                {
                                    Id = 0,
                                    Direction = RepEntryDirection.Dispatched,
                                    DispatchQty = item.DispatchingQty,
                                    ReceivedQty = 0,
                                    Remarks = item.Remarks,
                                    RepItem = null,
                                    RepairDispatchRecord = null,
                                    RepairDispatchRecordId = dispatchRecord.Id,
                                    RepairReceiveRecord = null,
                                    RepairReceiveRecordId = null,
                                    RepItemId = item.Id
                                };

                                db.RepEntries.Add(entry);

                                RepItem dbItem = db.RepItems.Find(item.Id);
                                dbItem.SKU -= entry.DispatchQty;
                                dbItem.UR += entry.DispatchQty;

                                db.Entry(dbItem).State = EntityState.Modified;
                            }
                            db.SaveChanges();

                            trans.Commit();

                            Gujjar.InfoMsg("Entry is added and saved in database successfully");
                            IsDone = true;
                            Close();
                        }
                        catch (Exception exp2)
                        {
                            trans.Rollback();
                            throw exp2;
                        }
                    }

                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void cbLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLocations.SelectedIndex == -1)
            {
                place = null;
                return;
            }
            place = cbLocations.SelectedItem as RepPlace;
        }
    }
}
