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
    public partial class AddAdvanceItemForm : Form
    {
        private int itemId = 0;
        private string itemName = "";
        private List<RepPlace> repPlaces = null;
        private RepPlace repPlace = null;
        public bool IsDone { get; set; }
        public AddAdvanceItemForm(int repItemId, string itemName)
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
                LoadAndBindPlaces();
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

        private void LoadPlaces()
        {
            try
            {
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
        private void BindCbPlaces()
        {
            cbPlaces.DataSource = repPlaces;
            cbPlaces.DisplayMember = "Name";
            cbPlaces.ValueMember = "Id";
        }

        private void LoadAndBindPlaces()
        {
            try
            {
                WaitForm wait = new WaitForm(LoadPlaces);
                wait.ShowDialog();

                BindCbPlaces();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddWorkingPlace_Click(object sender, EventArgs e)
        {
            try
            {
                AddRepPlaceForm form = new AddRepPlaceForm();
                form.ShowDialog();

                int pid = form.PlaceId;
                if(pid != 0)
                {
                    LoadAndBindPlaces();
                    cbPlaces.SelectedItem = cbPlaces.Items.OfType<RepPlace>()
                        .FirstOrDefault(a => a.Id == pid);
                }
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

                if(repPlace == null)
                {
                    throw new Exception("Please select/choose repairing place");
                }
                decimal qty = tbQty.Text.ToDecimal();
                if(qty == 0)
                {
                    throw new Exception("Qty is zero");
                }
                if(!Helper.ConfirmUserPassword())
                {
                    return;
                }

                DialogResult res = Gujjar.ConfirmYesNo("Please confirm..!! ");
                if (res == DialogResult.No)
                    return;

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        AdvanceItemRecord aRec = new AdvanceItemRecord
                        {
                            Dated = dtp.Value,
                            Id = 0,
                            Item = null,
                            Place = null,
                            Qty = tbQty.Text.ToDecimal(),
                            Remarks = tbRemarks.Text,
                            RepItemId = itemId,
                            RepPlaceId = repPlace.Id,
                            Type = AdvanceItemRecordType.Received
                        };
                        aRec = db.AdvanceItemRecords.Add(aRec);
                        db.SaveChanges();

                        var dbObj = db.ItemPlaceSKUs.FirstOrDefault(a => a.RepItemId == itemId && a.RepPlaceId == repPlace.Id);
                        if(dbObj != null)
                        {
                            dbObj.SKU += aRec.Qty;
                            db.Entry(dbObj).State = System.Data.Entity.EntityState.Modified;

                        }
                        else
                        {
                            dbObj = new ItemPlaceSKU
                            {
                                Id = 0,
                                SKU = aRec.Qty,
                                RepItemId = itemId,
                                RepPlaceId = repPlace.Id
                            };
                            db.ItemPlaceSKUs.Add(dbObj);
                        }

                        db.SaveChanges();
                        trans.Commit();
                        IsDone = true;
                        Gujjar.InfoMsg("Record is inserted and saved in database successfully");
                        Close();
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void cbPlaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPlaces.SelectedIndex == -1)
            {
                repPlace = null;
                return;
            }
            repPlace = cbPlaces.SelectedItem as RepPlace;
        }
    }
}
