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
using Model.Entertainment.ViewModel;

namespace WinFom.EntertainmentUI.Forms
{
    public partial class EItemListForm : Form
    {
        private List<EntItem> entItems = null;
        public EItemListForm()
        {
            InitializeComponent();
        }

        private void LoadEntItems()
        {
            try
            {
                using (Context db = new Context())
                {
                    if(entItems != null)
                    {
                        entItems.Clear();
                        entItems = null;
                    }
                    entItems = db.EntItems.ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindItems()
        {
            entItemVMBindingSource.List.Clear();
            foreach (var item in entItems)
            {
                EntItemVM vm = new EntItemVM
                {
                    Id = item.Id,
                    NameEng = item.Title,
                    NameUrdu = item.NameUrdu,
                    QtyConsumed = item.QtyConsumed
                };
                entItemVMBindingSource.List.Add(vm);
            }
            dgv.Refresh();
        }

        private void LoadAndBind()
        {
            WaitForm wait = new WaitForm(LoadEntItems);
            wait.ShowDialog();
            BindItems();
        }
        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AddEItemForm form = new AddEItemForm();
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

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnZeroConsume_Click(object sender, EventArgs e)
        {
            try
            {
                EntZeroItemConsumptionForm form = new EntZeroItemConsumptionForm();
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
    }
}
