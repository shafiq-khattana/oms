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
    public partial class AddTopHeadAccountForm : Form
    {
        private string headId = "";
        private HeadAccount headAccount = null;
        public bool IsDone = false;
        public AddTopHeadAccountForm(string headId)
        {
            InitializeComponent();
            this.headId = headId;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                using (Context db = new Context())
                {
                    headAccount = db.Accounts.OfType<HeadAccount>().FirstOrDefault(a => a.Id == headId);
                    
                }

                Gujjar.TB4(pMain);
               
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
                if (!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }

                TopHeadAccount acct = new TopHeadAccount
                {
                    Id = Guid.NewGuid().ToString(),
                    AccountNature = headAccount.AccountNature,
                    Title = tbAccountTitle.Text,
                    AccountNo = "N/A",
                    Address = "N/A",
                    Description = tbAccountDescription.Text,
                    ExplicitilyCreated = false,
                    HeadAccountId = headAccount.Id,
                    HeadAccount = null,
                    SubHeadAccounts = null
                };

                using (Context db = new Context())
                {
                    var obj = db.Accounts.OfType<TopHeadAccount>()
                        .Where(a => a.Title.Equals(acct.Title) && a.HeadAccountId == headAccount.Id)
                        .FirstOrDefault();
                    if (obj != null)
                    {
                        throw new Exception("Account already created");
                    }

                    db.Accounts.Add(acct);
                    db.SaveChanges();

                    Gujjar.InfoMsg("Account information is added in database successfully");
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
