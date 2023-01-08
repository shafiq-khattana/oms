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
using WinFom.Financials.ViewModel;

namespace WinFom.Financials.Forms
{
    public partial class IncomeAddAcctForm : Form
    {
        public GenAcctLVM AccountInfo = null;
        public IncomeAddAcctForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            AccountInfo = null;
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
            AccountInfo = null;
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

                AccountInfo = new GenAcctLVM
                {
                    Account = tbTitle.Text,
                    Balance = tbBalance.Text.ToDecimal()
                };
                Close();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
