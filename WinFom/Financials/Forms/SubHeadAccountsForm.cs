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
    public partial class SubHeadAccountsForm : Form
    {
        private List<SubHeadAccount> subHeadAccounts = null;
        private TopHeadAccount topHead = null;
        private string headAccountId = "";
        //private string btnAdd = "asfsfasasfasfasdfs";
        private string btnView = "shafiqasdfwera";
        public SubHeadAccountsForm(string headId)
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
                if(subHeadAccounts != null)
                {
                    subHeadAccounts.Clear();
                    subHeadAccounts = null;
                }
                using (Context db = new Context())
                {
                    topHead = db.Accounts.OfType<TopHeadAccount>().FirstOrDefault(a => a.Id == headAccountId);

                    subHeadAccounts = db.Accounts.OfType<SubHeadAccount>().Where(a => a.TopHeadAccountId == headAccountId)
                        .ToList();
                    foreach (var item in subHeadAccounts)
                    {
                        item.Accounts = db.Accounts.OfType<GeneralAccount>().Where(a => a.SubHeadAccountId == item.Id).ToList();
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
                lblHeading.Text = string.Format("Sub heads of acount : {0}", topHead.Title);
                //Gujjar.AddDatagridviewButton(dgv, btnAdd, "Add Top Head", "Add Top Head", 120);
                Gujjar.AddDatagridviewButton(dgv, btnView, "View Accounts", "View Accounts", 120);
                foreach (var item in subHeadAccounts)
                {
                    HeadAccountVM vm = new HeadAccountVM
                    {
                        Id = item.Id,
                        Description = item.Description,
                        SubAccountCounts = item.Accounts.Count,
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
            try
            {
                int ri = e.RowIndex;
                int ci = e.ColumnIndex;

                if (ri == dgv.NewRowIndex || ri == -1)
                    return;

                if(dgv.Columns[btnView].Index == ci)
                {
                    string accid = dgv.Rows[ri].Cells[0].Value.ToString();

                    GeneralAccountsForm form = new GeneralAccountsForm(accid);
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
                AddHeadAccountForm form = new AddHeadAccountForm(topHead.Id);
                form.ShowDialog();

                if(form.IsDone)
                {
                    WaitForm wait = new WaitForm(LoadData);
                    wait.ShowDialog();

                    headAccountVMBindingSource.List.Clear();

                    foreach (var item in subHeadAccounts)
                    {
                        HeadAccountVM vm = new HeadAccountVM
                        {
                            Id = item.Id,
                            Description = item.Description,
                            SubAccountCounts = item.Accounts.Count,
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
