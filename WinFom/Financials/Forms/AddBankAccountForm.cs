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

namespace WinFom.Financials.Forms
{
    public partial class AddBankAccountForm : Form
    {
        public bool IsDone = false;
        public AddBankAccountForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                Gujjar.TB4(pMain);
                tbDescription.Text = "N/A";
                Gujjar.TBOptional(tbDescription);
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
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }

                GeneralAccount acct = new GeneralAccount
                {
                    Id = Guid.NewGuid().ToString(),
                    AccountNature = AccountNature.Debit,
                    AccountNo = tbAccountNo.Text,
                    AccountTransactions = null,
                    Address = tbBankAddress.Text,
                    Balance = 0,
                    BankName = tbBankName.Text,
                    SubHeadAccount = null,
                    Description = tbDescription.Text,
                    ExplicitilyCreated = false,
                    Title = string.Format("{0}-{1} ({2})",  tbBankName.Text, tbAccountNo.Text, tbAccountTitle.Text),
                    SubHeadAccountId = Properties.Resources.Banks
                };

                using (Context db = new Context())
                {
                    var obj = db.Accounts.OfType<GeneralAccount>()
                        .FirstOrDefault(a => a.Title == acct.Title && a.SubHeadAccountId == acct.SubHeadAccountId && a.AccountNature == acct.AccountNature);
                    if(obj != null)
                    {
                        throw new Exception("Same account already exists");
                    }

                    db.Accounts.Add(acct);
                    db.SaveChanges();
                    Gujjar.InfoMsg("Bank account is added successfully");
                    IsDone = true;
                    Close();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
