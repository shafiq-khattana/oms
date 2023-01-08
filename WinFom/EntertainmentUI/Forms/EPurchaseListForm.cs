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
using System.Data.Entity;
using Model.Entertainment.ViewModel;

namespace WinFom.EntertainmentUI.Forms
{
    public partial class EPurchaseListForm : Form
    {
        private List<EntPurchase> entPurchases = null;
        public EPurchaseListForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadEntPurchases()
        {
            try
            {
                if(entPurchases != null)
                {
                    entPurchases.Clear();
                    entPurchases = null;
                }
                using (Context db = new Context())
                {
                    entPurchases = db.EntPurchases.Include(a => a.Entries)
                        .OrderByDescending(a => a.Id).Take(60).ToList();
                    foreach (var item in entPurchases)
                    {
                        foreach (var item2 in item.Entries)
                        {
                            item2.Item = db.EntItems.Find(item2.EntItemId);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void LoadAndBind()
        {
            try
            {
                WaitForm wait = new WaitForm(LoadEntPurchases);
                wait.ShowDialog();

                DgvUpdate();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void DgvUpdate()
        {
            try
            {
                ePurchaseVMBindingSource.Clear();
                foreach (var item in entPurchases)
                {
                    EPurchaseVM vm = new EPurchaseVM
                    {
                        Dated = item.Dated.ToString(),
                        Id = item.Id,
                        Operator = item.Operator,
                        Qty = item.Qty,
                        Remarks = item.Remarks
                    };
                    vm.Entries = new List<ePurEntry>();
                    foreach (var itemw in item.Entries)
                    {
                        ePurEntry ent = new ePurEntry
                        {
                            Id = itemw.Id,
                            Item = itemw.Item.Title,
                            Qty = itemw.Qty,
                            ItemUrdu = itemw.Item.NameUrdu
                        };
                        vm.Entries.Add(ent);
                    }
                    ePurchaseVMBindingSource.List.Add(vm);
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
                LoadAndBind();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void pMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEItemForm form = new AddEItemForm();
            form.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnItemsList_Click(object sender, EventArgs e)
        {
            try
            {
                EItemListForm form = new EItemListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddPurchase_Click(object sender, EventArgs e)
        {
            try
            {
                AddEPurshaseForm form = new AddEPurshaseForm();
                form.ShowDialog();
                if(form.IsDone)
                {
                    LoadAndBind();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int ri = e.RowIndex;
            if(ri == -1 || ri == dgv.NewRowIndex)
            {
                return;
            }
            try
            {
                int id = dgv.Rows[ri].Cells[0].Value.ToInt();

                var obj = ePurchaseVMBindingSource.List.OfType<EPurchaseVM>()
                    .FirstOrDefault(a => a.Id == id);

                ePurEntryBindingSource.List.Clear();
                foreach (var item in obj.Entries)
                {
                    ePurEntry entry = new ePurEntry
                    {
                        Id = item.Id,
                        Item = item.Item,
                        Qty = item.Qty
                    };
                    ePurEntryBindingSource.List.Add(entry);
                }

                dgvEnts.Refresh();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
