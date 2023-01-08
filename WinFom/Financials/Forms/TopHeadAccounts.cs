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
    public partial class TopHeadAccounts : Form
    {
        private List<TopHeadAccount> topHeadAccounts = null;
        private HeadAccount headAccount = null;
        private string headAccountId = "";
        //private string btnAdd = "asfsfasasfasfasdfs";
        private string btnView = "shafiqasdfwera";
        public bool IsDone = false;
        public TopHeadAccounts(string headId)
        {
            InitializeComponent();
            headAccountId = headId;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadData()
        {
            try
            {
                if(topHeadAccounts != null)
                {
                    topHeadAccounts.Clear();
                    topHeadAccounts = null;
                }

                using (Context db = new Context())
                {
                    headAccount = db.Accounts.OfType<HeadAccount>().FirstOrDefault(a => a.Id == headAccountId);

                    topHeadAccounts = db.Accounts.OfType<TopHeadAccount>().Where(a => a.HeadAccountId == headAccountId)
                        .ToList();
                    foreach (var item in topHeadAccounts)
                    {
                        item.SubHeadAccounts = db.Accounts.OfType<SubHeadAccount>().Where(a => a.TopHeadAccountId == item.Id).ToList();
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
                lblHeading.Text = string.Format("Top heads of acount : {0}", headAccount.Title);
                //Gujjar.AddDatagridviewButton(dgv, btnAdd, "Add Top Head", "Add Top Head", 120);
                Gujjar.AddDatagridviewButton(dgv, btnView, "View Sub Heads", "View Sub Heads", 120);
                foreach (var item in topHeadAccounts)
                {
                    HeadAccountVM vm = new HeadAccountVM
                    {
                        Id = item.Id,
                        Description = item.Description,
                        SubAccountCounts = item.SubHeadAccounts.Count,
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
                if(ri == -1 || ri == dgv.NewRowIndex)
                {
                    return;
                }

                if(dgv.Rows[ri].Cells[btnView].ColumnIndex == ci)
                {
                    string accid = dgv.Rows[ri].Cells[0].Value.ToString();

                    SubHeadAccountsForm form = new SubHeadAccountsForm(accid);
                    form.ShowDialog();
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
                AddTopHeadAccountForm form = new AddTopHeadAccountForm(headAccountId);
                form.ShowDialog();

                if(form.IsDone)
                {
                    IsDone = true;
                    WaitForm wait1 = new WaitForm(LoadData);
                    wait1.ShowDialog();

                    headAccountVMBindingSource.List.Clear();
                    foreach (var item in topHeadAccounts)
                    {
                        HeadAccountVM vm = new HeadAccountVM
                        {
                            Id = item.Id,
                            Description = item.Description,
                            SubAccountCounts = item.SubHeadAccounts.Count,
                            Title = item.Title,
                            Type = item.AccountNature
                        };
                        headAccountVMBindingSource.List.Add(vm);
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
