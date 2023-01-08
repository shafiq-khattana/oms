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
    public partial class GeneralAccountsForm : Form
    {
        private List<GeneralAccount> generalAccounts = null;
        private SubHeadAccount subHead = null;
        private string headAccountId = "";
        //private string btnAdd = "asfsfasasfasfasdfs";
        //private string btnView = "shafiqasdfwera";
        private string dgvbtnedittitle = "dgvbtnedittitle";
        private string dgvbtndelete = "dgvbtndelete";
        public GeneralAccountsForm(string headId)
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
                if(generalAccounts != null)
                {
                    generalAccounts.Clear();
                    generalAccounts = null;
                }
                using (Context db = new Context())
                {
                    subHead = db.Accounts.OfType<SubHeadAccount>().FirstOrDefault(a => a.Id == headAccountId);

                    generalAccounts = db.Accounts.OfType<GeneralAccount>().Where(a => a.SubHeadAccountId == headAccountId)
                        .ToList();
                    foreach (var item in generalAccounts)
                    {
                        var obj = db.AccountTransactions.Where(a => a.GeneralAccountId == item.Id).ToList()
                            .OrderByDescending(a => a.Id).FirstOrDefault();
                        decimal bal = 0;
                        if(obj != null)
                        {
                            bal = obj.Balance;
                        }
                        item.Balance = bal;
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
                accountVMBindingSource.List.Clear();

                WaitForm wait1 = new WaitForm(LoadData);
                wait1.ShowDialog();
                lblHeading.Text = string.Format("Accounts of sub head acount : {0}", subHead.Title);
                //Gujjar.AddDatagridviewButton(dgv, btnAdd, "Add Top Head", "Add Top Head", 120);
                //Gujjar.AddDatagridviewButton(dgv, btnView, "View Top Heads", "View Top Heads", 120);
                Gujjar.AddDatagridviewButton(dgv, dgvbtnedittitle, "Edit", "Edit", 80);
                Gujjar.AddDatagridviewButton(dgv, dgvbtndelete, "Delete", "Delete", 80);
                foreach (var item in generalAccounts)
                {
                    AccountVM vm = new AccountVM
                    {
                        Id = item.Id,
                        Description = item.Description,
                        AcctNo = item.AccountNo,
                        Title = item.Title,
                        Balance = item.Balance,
                        Type = item.AccountNature
                    };
                    accountVMBindingSource.List.Add(vm);
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
                AddGeneralAccountForm form = new AddGeneralAccountForm(subHead.Id);
                form.ShowDialog();

                if(form.IsDone)
                {
                    accountVMBindingSource.List.Clear();
                    WaitForm wait = new WaitForm(LoadData);
                    wait.ShowDialog();

                    foreach (var item in generalAccounts)
                    {
                        AccountVM vm = new AccountVM
                        {
                            Id = item.Id,
                            Description = item.Description,
                            AcctNo = item.AccountNo,
                            Title = item.Title,
                            Balance = item.Balance,
                            Type = item.AccountNature
                        };
                        accountVMBindingSource.List.Add(vm);
                    }
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

                if (ri == -1 || ri == dgv.NewRowIndex)
                    return;

                if(dgv.Columns[dgvbtndelete].Index == ci)
                {
                    if(!Helper.ConfirmAdminPassword())
                    {
                        return;
                    }

                    DialogResult res = Gujjar.ConfirmYesNo("Are you sure plz");
                    if (res == DialogResult.No)
                        return;

                    string id = dgv.Rows[ri].Cells[0].Value.ToString();
                    using (Context db = new Context())
                    {
                        var account = db.Accounts.Find(id) as GeneralAccount;
                        if(account.ExplicitilyCreated)
                        {
                            throw new Exception("This account can't be deleted, because it is system generated");
                        }
                        var trans = db.AccountTransactions.Where(a => a.GeneralAccountId == account.Id).ToList();
                        if(trans.Count == 0)
                        {
                            db.Accounts.Remove(account);
                            db.SaveChanges();

                            Gujjar.InfoMsg("Account is deleted successfully");

                            accountVMBindingSource.List.Clear();
                            WaitForm wait = new WaitForm(LoadData);
                            wait.ShowDialog();

                            foreach (var item in generalAccounts)
                            {
                                AccountVM vm = new AccountVM
                                {
                                    Id = item.Id,
                                    Description = item.Description,
                                    AcctNo = item.AccountNo,
                                    Title = item.Title,
                                    Balance = item.Balance,
                                    Type = item.AccountNature
                                };
                                accountVMBindingSource.List.Add(vm);
                            }
                        }
                        else
                        {
                            throw new Exception(string.Format("It can't be deleted, it has ({0}) transactions", trans.Count));
                        }
                    }
                }

                if(dgv.Columns[dgvbtnedittitle].Index == ci)
                {
                    string id = dgv.Rows[ri].Cells[0].Value.ToString();
                    AccountEditForm form = new AccountEditForm(id);
                    form.ShowDialog();

                    if(form.IsDone)
                    {
                        accountVMBindingSource.List.Clear();
                        WaitForm wait = new WaitForm(LoadData);
                        wait.ShowDialog();

                        foreach (var item in generalAccounts)
                        {
                            AccountVM vm = new AccountVM
                            {
                                Id = item.Id,
                                Description = item.Description,
                                AcctNo = item.AccountNo,
                                Title = item.Title,
                                Balance = item.Balance,
                                Type = item.AccountNature
                            };
                            accountVMBindingSource.List.Add(vm);
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
