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
    public partial class AccountEditForm : Form
    {
        private string acctId = "";
        public bool IsDone { get; set; }
        public string AccountTitle { get; set; }
        public AccountEditForm(string accid)
        {
            InitializeComponent();
            acctId = accid;
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
                    tbOldTitle.Text = (db.Accounts.Find(acctId) as GeneralAccount).Title;
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
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }

                if(!Helper.ConfirmAdminPassword())
                {
                    return;
                }
                DialogResult rest = Gujjar.ConfirmYesNo("Please confirm, are you sure?");
                if (rest == DialogResult.No)
                    return;

                using (Context db = new Context())
                {
                    var obj = db.Accounts.Find(acctId) as GeneralAccount;
                    obj.Title = tbNewTitle.Text;
                    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    AccountTitle = tbNewTitle.Text;
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
