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
    public partial class AddGeneralAccountForm : Form
    {
        private string subHeadId = "";
        private SubHeadAccount subHead = null;
        public bool IsDone = false;
        public AddGeneralAccountForm(string subheadid)
        {
            InitializeComponent();
            subHeadId = subheadid;
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
                    subHead = db.Accounts.OfType<SubHeadAccount>().FirstOrDefault(a => a.Id == subHeadId);
                    
                }

                Gujjar.TB4(pMain);
                Gujjar.TBOptional(tbAccountNo);
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

                GeneralAccount acct2 = new GeneralAccount
                {
                    Id = Guid.NewGuid().ToString(),
                    AccountNature = subHead.AccountNature,
                    Title = tbAccountTitle.Text,
                    AccountNo = tbAccountNo.Text,
                    Address = "N/A",
                    Balance = 0,
                    Description = tbAccountDescription.Text,
                    ExplicitilyCreated = false,
                    SubHeadAccountId = subHead.Id, AccountTransactions = null, SubHeadAccount = null

                };

                using (Context db = new Context())
                {
                    var obj = db.Accounts.OfType<GeneralAccount>()
                        .Where(a => a.Title.Equals(acct2.Title) && a.SubHeadAccountId == subHead.Id)
                        .FirstOrDefault();
                    if (obj != null)
                    {
                        throw new Exception("Account already created");
                    }

                    db.Accounts.Add(acct2);
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
