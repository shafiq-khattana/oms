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
using Model.Financials.Model;
using Model.Admin.Model;
using WinFom.Common.Model;
using WinFom.Common.Forms;
using Model.RetailBardanaManaged.Model;
using Model.RetailBardanaManaged.ViewModel;

namespace WinFom.RetailBardanaManagedUI.Forms
{
    public partial class RetailBardanaItemEntriesForm : Form
    {
        private List<RetailBardanaItemEntry> itemEntries = null;
        private AppSettings AppSet = Helper.AppSet;
        RetailBardanaItem bardanaItem = null;
        public bool IsDone = false;
        private string dgvbtncancel = "dgvbtncancel";
        public RetailBardanaItemEntriesForm(RetailBardanaItem item)
        {
            InitializeComponent();
            bardanaItem = item;
        }

        private void LoadItemEntries()
        {
            try
            {
                if(itemEntries != null && itemEntries.Count > 0)
                {
                    itemEntries.Clear();
                    itemEntries = null;
                }
                using (Context db = new Context())
                {
                    itemEntries = db.RetailBardanaItemEntries.ToList()
                        .Where(a => a.Dated.Date >= AppSet.StartDate.Date && a.Dated.Date <= AppSet.EndDate.Date)
                        .OrderBy(a => a.Id).ToList();
                    foreach (var item in itemEntries)
                    {
                        item.RetailBardanaSupplier = db.RetailBardanaSuppliers.Find(item.RetailBardanaSupplierId);
                        item.RetailBardanaItem = bardanaItem;
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait = new WaitForm(LoadItemEntries);
                wait.ShowDialog();

                UpdateDgv();
                Gujjar.AddDatagridviewButton(dgv, dgvbtncancel, "Cancel", "Cancel", 80);
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
                bardanaItemEntryBindingSource.List.Clear();

                foreach (var item in itemEntries)
                {
                    BardanaItemEntry vm = new BardanaItemEntry
                    {
                        Id = item.Id,
                        Dated = item.Dated.ToString(),
                        QtyEntered = item.QtyEntered.ToString("n1"),
                        Remarks = item.Remarks,
                        RetailBardanaItem = item.RetailBardanaItem.Title,
                        RetailBardanaSupplier = item.RetailBardanaSupplier.Name,
                        TotalPrice = item.TotalPrice.ToString("n2"),
                        UnitPrice = item.UnitPrice.ToString("n2"),
                        DaybookId = item.DayBookId
                    };
                    bardanaItemEntryBindingSource.List.Add(vm);
                }
            }
            catch (Exception ep)
            {
                Gujjar.ErrMsg(ep);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AddBardanaItemEntry form = new AddBardanaItemEntry(bardanaItem.Id);
                form.ShowDialog();
                if(form.IsDone)
                {
                    WaitForm wait = new WaitForm(LoadItemEntries);
                    wait.ShowDialog();
                    IsDone = true;
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

                if(ri == -1 || ri == dgv.NewRowIndex)
                {
                    return;
                }

                if(!Helper.ConfirmAdminPassword())
                {
                    return;
                }
                DialogResult res = Gujjar.ConfirmYesNo("Please confirm, before to delete purchasing entry");
                if (res == DialogResult.No)
                    return;

                int id = dgv.Rows[ri].Cells[0].Value.ToInt();
                var vmObj = bardanaItemEntryBindingSource.List.OfType<BardanaItemEntry>()
                    .FirstOrDefault(a => a.Id == id);

                DayBook daybook = null;
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var dbEntry = db.RetailBardanaItemEntries.Find(id);

                            var item = db.RetailBardanaItems.Find(dbEntry.RetailBardanaItemId);
                            item.SKU -= dbEntry.QtyEntered;
                            if(item.SKU == 0)
                            {
                                item.UnitPrice = 0;
                                
                            }
                            daybook = db.DayBooks.Find(dbEntry.DayBookId);
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            db.Entry(dbEntry).State = System.Data.Entity.EntityState.Deleted;
                            db.SaveChanges();

                            trans.Commit();
                            if (daybook != null)
                            {
                                Helper.ReverseTransaction(daybook);
                            }

                            WaitForm wait = new WaitForm(LoadItemEntries);
                            wait.ShowDialog();
                            IsDone = true;
                            UpdateDgv();
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
