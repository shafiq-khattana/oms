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
    public partial class AddLinkAccountForm4 : Form
    {
        private GeneralAccount account = null;
        List<GeneralAccount> linkAccounts = null;
        private List<GeneralAccount> accountList = null;
        public bool IsDone = false;
        private List<AccountSearchVM> accountSearchList = null;
        public AddLinkAccountForm4(GeneralAccount acct, List<GeneralAccount> accounts)
        {
            InitializeComponent();
            account = acct;
            linkAccounts = accounts;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadAccounts()
        {
            try
            {
                if(accountList != null)
                {
                    accountList.Clear();
                    accountList = null;
                }
                accountSearchList = null;
                accountSearchList = new List<AccountSearchVM>();
                using (Context db = new Context())
                {
                    accountList = db.Accounts.OfType<GeneralAccount>().ToList();
                    foreach (var item in accountList)
                    {
                        item.Balance = 0;
                        var trans = db.AccountTransactions.Where(a => a.GeneralAccountId == item.Id).OrderByDescending(a => a.Id)
                            .FirstOrDefault();
                        if(trans != null)
                        {
                            item.Balance = trans.Balance;
                        }
                        AccountSearchVM asvm = new AccountSearchVM
                        {
                            Id = item.Id,
                            Title = item.Title
                        };

                        accountSearchList.Add(asvm);
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
                tbAccountTitle.Text = account.Title;
                tbAccountBalance.Text = account.Balance.ToString("n2");

                WaitForm waitF = new WaitForm(LoadAccounts);
                waitF.ShowDialog();
                cbAccounts.DataSource = accountList;
                cbAccounts.DisplayMember = "Title";
                cbAccounts.ValueMember = "Id";
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbBalance_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAccounts.SelectedItem == null)
                return;
            tbBalance.Text = (cbAccounts.SelectedItem as GeneralAccount).Balance.ToString("n2");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Helper.ConfirmAdminPassword())
                {
                    return;
                }

                DialogResult res = Gujjar.ConfirmYesNo("Are you confirmed please ??");
                if (res == DialogResult.No)
                    return;

                if(cbAccounts.SelectedIndex == -1)
                {
                    throw new Exception("Please choose a account to link");
                }
                GeneralAccount genAccount = cbAccounts.SelectedItem as GeneralAccount;
                if(genAccount.ParentAccountId != null)
                {
                    using (Context db = new Context())
                    {
                        var acct2 = db.Accounts.Find(genAccount.ParentAccountId);
                        throw new Exception(string.Format("{0} is already a sub account of {1}", genAccount.Title, acct2.Title));
                    }
                }
                using (Context db = new Context())
                {
                    var acct4 = db.Accounts.Find(genAccount.Id) as GeneralAccount;
                    acct4.ParentAccountId = account.Id;

                    db.Entry(acct4).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    IsDone = true;

                    Gujjar.InfoMsg("Link account is added");
                    Close();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchAccountForm form = new SearchAccountForm(accountSearchList);
                form.ShowDialog();
                string id = form.AccountId;
                if (!string.IsNullOrEmpty(id))
                {
                    cbAccounts.SelectedItem = cbAccounts.Items.OfType<GeneralAccount>()
                        .FirstOrDefault(a => a.Id == id);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
