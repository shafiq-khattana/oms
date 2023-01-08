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
    public partial class AddHeadAccountForm : Form
    {
        private string topHeadId = "";
        private TopHeadAccount topHead = null;
        public bool IsDone = false;
        public AddHeadAccountForm(string topHeadId)
        {
            InitializeComponent();
            this.topHeadId = topHeadId;
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
                    topHead = db.Accounts.OfType<TopHeadAccount>().FirstOrDefault(a => a.Id == topHeadId);
                    
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

                SubHeadAccount acct = new SubHeadAccount
                {
                    Id = Guid.NewGuid().ToString(),
                    AccountNature = topHead.AccountNature,
                    Title = tbAccountTitle.Text,
                    AccountNo = "N/A",
                    Address = "N/A",
                    Description = tbAccountDescription.Text,
                    ExplicitilyCreated = false,
                    TopHeadAccountId = topHead.Id,
                    Accounts = null,
                    TopHeadAccount = null
                };

                using (Context db = new Context())
                {
                    var obj = db.Accounts.OfType<SubHeadAccount>()
                        .Where(a => a.Title.Equals(acct.Title) && a.TopHeadAccountId == topHead.Id)
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
