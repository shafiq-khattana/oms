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
    public partial class AddCapitalAccountForm : Form
    {
        public bool IsDone = false;
        public AddCapitalAccountForm()
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
                using (Context db = new Context())
                {
                    string title = tbAccountTitle.Text;
                    if(string.IsNullOrEmpty(title))
                    {
                        throw new Exception("Please enter account title");
                    }

                    title = string.Format("{0} (capital) account", title);
                    var obj = db.Accounts.OfType<GeneralAccount>()
                        .FirstOrDefault(a => a.Title == title);
                    if(obj != null)
                    {
                        throw new Exception("Account already exists in database");
                    }

                    GeneralAccount capAccount = new GeneralAccount
                    {
                        Id = Guid.NewGuid().ToString(),
                        AccountNature = AccountNature.Credit,
                        AccountNo = "N/A",
                        AccountTransactions = null,
                        Address = "N/A",
                        Balance = 0,
                        BankName = "N/A",
                        Description = title,
                        Title = title,
                        ExplicitilyCreated = false,
                        SubHeadAccountId = Properties.Resources.CapitalAccountSubHead
                    };
                    db.Accounts.Add(capAccount);
                    db.SaveChanges();
                    Gujjar.InfoMsg("Account is added in database successfully");
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
