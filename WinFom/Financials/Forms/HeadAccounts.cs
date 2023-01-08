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
using Model.Financials.ViewModel;

namespace WinFom.Financials.Forms
{
    public partial class HeadAccounts : Form
    {
        private List<HeadAccount> headAccounts = null;
        //private string btnAdd = "asfsfasasfasfasdfs";
        private string btnView = "shafiqasdfwera";
        public HeadAccounts()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadData()
        {
            try
            {
                if(headAccounts != null)
                {
                    headAccounts.Clear();
                    headAccounts = null;
                }
                using (Context db = new Context())
                {
                    headAccounts = db.Accounts.OfType<HeadAccount>().ToList();
                    foreach (var item in headAccounts)
                    {
                        item.TopHeadAccounts = db.Accounts.OfType<TopHeadAccount>().Where(a => a.HeadAccountId == item.Id).ToList();
                    }
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
                WaitForm wait1 = new WaitForm(LoadData);
                wait1.ShowDialog();
                //Gujjar.AddDatagridviewButton(dgv, btnAdd, "Add Top Head", "Add Top Head", 120);
                Gujjar.AddDatagridviewButton(dgv, btnView, "View Top Heads", "View Top Heads", 120);
                foreach (var item in headAccounts)
                {
                    HeadAccountVM vm = new HeadAccountVM
                    {
                        Id = item.Id,
                        Description = item.Description,
                        SubAccountCounts = item.TopHeadAccounts.Count,
                        Title = item.Title,
                        Type = item.AccountNature
                    };
                    headAccountVMBindingSource.List.Add(vm);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int ri = e.RowIndex;
            int ci = e.ColumnIndex;

            try
            {
                if(ri == dgv.NewRowIndex || ri == -1)
                {
                    return;
                }

                if(dgv.Columns[btnView].Index == ci)
                {
                    string acctId = dgv.Rows[ri].Cells[0].Value.ToString();

                    TopHeadAccounts form = new TopHeadAccounts(acctId);
                    form.ShowDialog();
                    if(form.IsDone)
                    {
                        WaitForm wait = new WaitForm(LoadData);
                        wait.ShowDialog();

                        headAccountVMBindingSource.List.Clear();
                        foreach (var item in headAccounts)
                        {
                            HeadAccountVM vm = new HeadAccountVM
                            {
                                Id = item.Id,
                                Description = item.Description,
                                SubAccountCounts = item.TopHeadAccounts.Count,
                                Title = item.Title,
                                Type = item.AccountNature
                            };
                            headAccountVMBindingSource.List.Add(vm);
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
