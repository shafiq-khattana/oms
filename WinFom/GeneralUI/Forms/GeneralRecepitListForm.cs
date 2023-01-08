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
using Model.General.Model;
using Model.General.ViewModel;

namespace WinFom.GeneralUI.Forms
{
    public partial class GeneralRecepitListForm : Form
    {
        private List<GeneralReceipt> generalReceipts = null;
        public GeneralRecepitListForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadReceipts()
        {
            try
            {
                if(generalReceipts != null)
                {
                    generalReceipts.Clear();
                    generalReceipts = null;
                }

                using (Context db = new Context())
                {
                    generalReceipts = db.GeneralReceipts.OrderByDescending(a => a.Id).Take(40).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindReceipts()
        {
            grVmBindingSource.List.Clear();
            foreach (var item in generalReceipts)
            {
                GrVm grvm = new GrVm
                {
                    Amount = item.Amount,
                    Dated = item.Dated,
                    Description = item.Description,
                    Id = item.Id,
                    Unum = item.Unum
                };
                grVmBindingSource.List.Add(grvm);
            }
        }
        private void LoadAndBind()
        {
            WaitForm wait = new WaitForm(LoadReceipts);
            wait.ShowDialog();
            BindReceipts();
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
                AddGeneralReceiptForm form = new AddGeneralReceiptForm();
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
