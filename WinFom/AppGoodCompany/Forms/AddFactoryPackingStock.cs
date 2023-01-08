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
using WinFom.Deal.Forms;
using WinFom.Common.Forms;
using Model.Admin.Model;
using WinFom.Common.Model;

namespace WinFom.AppGoodCompany.Forms
{
    public partial class AddFactoryPackingStock : Form
    {
        private List<DealPacking> dealPackings = new List<DealPacking>();
        public bool IsDone = false;
        AppSettings appSett = Helper.AppSet;
        public AddFactoryPackingStock()
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
                WaitForm wait = new WaitForm(LoadPackings);
                wait.ShowDialog();
                BindCBPackings();
                dtpIssue.MinDate = appSett.StartDate;
                dtpIssue.MaxDate = appSett.EndDate;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadPackings()
        {
            if (dealPackings.Count > 0)
                dealPackings.Clear();
            try
            {
                using (Context db = new Context())
                {
                    dealPackings = db.DealPackings
                        .OrderBy(a => a.Name).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindCBPackings()
        {
            cbPackings.DataSource = dealPackings;
            cbPackings.DisplayMember = "Name";
            cbPackings.ValueMember = "Id";

            cbPackings.SelectedItem = cbPackings.Items.OfType<DealPacking>()
                .FirstOrDefault(a => a.Name == "N/A");
        }
        private void btnAddPacking_Click(object sender, EventArgs e)
        {
            try
            {
                AddPackingForm form = new AddPackingForm();
                form.ShowDialog();

                int pid = form.PackingId;
                if (pid != 0)
                {
                    WaitForm w5 = new WaitForm(LoadPackings, "");
                    w5.ShowDialog();

                    BindCBPackings();

                    cbPackings.SelectedItem = cbPackings.Items.OfType<DealPacking>()
                        .FirstOrDefault(a => a.Id == pid);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void cbPackings_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbPackings.SelectedIndex == -1)
                    return;

                DealPacking pack = cbPackings.SelectedItem as DealPacking;

                if (pack.Name == "N/A")
                {
                    tbStockBalance.Text = "0";
                    return;
                }


                using (Context db = new Context())
                {
                    var obj = db.FactoryPackingStocks.FirstOrDefault(a => a.DealPackingId == pack.Id);
                    if (obj == null)
                    {
                        tbStockBalance.Text = "0";
                    }
                    else
                    {
                        tbStockBalance.Text = obj.Quantity.ToString("n1");
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            try
            {
                if(cbPackings.SelectedIndex == -1)
                {
                    throw new Exception("Please select packing");
                }
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all fields");
                }

                DealPacking dPack = cbPackings.SelectedItem as DealPacking;
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            FactoryPackingStock fps = new FactoryPackingStock
                            {
                                DealPackingId = dPack.Id,
                                Quantity = tbQty.Text.ToDecimal()
                            };

                            var obj = db.FactoryPackingStocks.FirstOrDefault(a => a.DealPackingId == fps.DealPackingId);
                            if(obj != null)
                            {
                                obj.Quantity += fps.Quantity;
                                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                            else
                            {
                                db.FactoryPackingStocks.Add(fps);
                                db.SaveChanges();
                            }

                            FactoryPackingStockAddedRecord record = new FactoryPackingStockAddedRecord
                            {
                                AddedDate = dtpIssue.Value,
                                DealPackingId = dPack.Id,
                                QtyAdded = fps.Quantity,
                                Description = string.Format("Qty: {0} of packing: {1} added in database. At datetime: {2}",
                                fps.Quantity, dPack.Name, dtpIssue.Value)
                            };

                            db.FactoryPackingStockAddedRecords.Add(record);
                            db.SaveChanges();

                            trans.Commit();

                            Gujjar.InfoMsg(string.Format("{0} {1}-(s) added in factory stock and saved in database.", fps.Quantity, dPack.Name));
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
    }
}
