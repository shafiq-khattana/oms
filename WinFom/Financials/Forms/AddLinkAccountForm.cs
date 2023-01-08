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
    public partial class AddLinkAccountForm : Form
    {
        private string acctId = null;
        private GeneralAccount account = null;
        private List<GeneralAccount> linkAccounts = null;
        private string dgvbtndel = "dgvbtndelete";
        public bool IsDone = false;
        public AddLinkAccountForm(string acid)
        {
            InitializeComponent();
            acctId = acid;
        }

        private void LoadLinksAccounts()
        {
            try
            {
                if(linkAccounts != null)
                {
                    linkAccounts.Clear();
                    linkAccounts = null;
                }
                using (Context db = new Context())
                {
                    linkAccounts = db.Accounts.OfType<GeneralAccount>()
                        .Where(a => a.ParentAccountId == acctId).ToList();

                    foreach (var item in linkAccounts)
                    {
                        item.Balance = 0;
                        var trans = db.AccountTransactions.Where(a => a.GeneralAccountId == item.Id).OrderByDescending(a => a.Id)
                            .FirstOrDefault();
                        if(trans != null)
                        {
                            item.Balance = trans.Balance;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadAccount()
        {
            try
            {
                using (Context db = new Context())
                {
                    account = db.Accounts.Find(acctId) as GeneralAccount;
                    var trans = db.AccountTransactions.Where(a => a.GeneralAccountId == acctId).OrderByDescending(a => a.Id)
                        .FirstOrDefault();
                    account.Balance = 0;
                    if(trans != null)
                    {
                        account.Balance = trans.Balance;
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

        private void DgvUpdate()
        {
            linkAcctVMBindingSource.List.Clear();
            foreach (var item in linkAccounts)
            {
                LinkAcctVM vm = new LinkAcctVM
                {
                    Balance = item.Balance,
                    Id = item.Id,
                    Title = item.Title,
                    Type = item.AccountNature.ToString()
                };

                linkAcctVMBindingSource.List.Add(vm);
            }
        }

        private void CalculateBalance()
        {
            decimal balane = account.Balance;
            decimal plusBal = linkAccounts.Where(a => a.AccountNature == account.AccountNature).Sum(a => a.Balance);
            decimal minBal = linkAccounts.Where(a => a.AccountNature != account.AccountNature).Sum(a => a.Balance);

            decimal newBal = balane + plusBal - minBal;
            tbEffectiveBalance.Text = newBal.ToString("n2");
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                Gujjar.AddDatagridviewButton(dgv, dgvbtndel, "Delete", "Delete", 80);

                WaitForm wait = new WaitForm(LoadAccount);
                wait.ShowDialog();

                WaitForm wait2 = new WaitForm(LoadLinksAccounts);
                wait2.ShowDialog();

                tbAccountTitle.Text = account.Title;
                tbBalance.Text = account.Balance.ToString("n2");
                tbType.Text = account.AccountNature.ToString();
                
                DgvUpdate();
                CalculateBalance();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AddLinkAccountForm4 form = new AddLinkAccountForm4(account, linkAccounts);
                form.ShowDialog();

                if(form.IsDone)
                {
                    WaitForm wait1 = new WaitForm(LoadLinksAccounts);
                    wait1.ShowDialog();

                    DgvUpdate();
                    CalculateBalance();
                    IsDone = true;
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

                if(dgv.Columns[dgvbtndel].Index == ci)
                {
                    if (!Helper.ConfirmAdminPassword())
                        return;

                    DialogResult rest = Gujjar.ConfirmYesNo("Are you sured to delete this account");
                    if (rest == DialogResult.No)
                        return;

                    string id = dgv.Rows[ri].Cells[0].Value.ToString();
                    //var obj = linkAcctVMBindingSource.List.OfType<LinkAcctVM>().FirstOrDefault(a => a.Id == id);
                    //linkAcctVMBindingSource.List.Remove(obj);
                    using (Context db = new Context())
                    {
                        var obj2 = db.Accounts.Find(id) as GeneralAccount;
                        obj2.ParentAccountId = null;
                        db.Entry(obj2).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        Gujjar.InfoMsg("Link account is removed");

                        WaitForm wait = new WaitForm(LoadLinksAccounts);
                        wait.ShowDialog();

                        DgvUpdate();
                        CalculateBalance();
                        IsDone = true;
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
