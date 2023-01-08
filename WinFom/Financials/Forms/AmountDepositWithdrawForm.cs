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
    public partial class AmountDepositWithdrawForm : Form
    {
        private AmountDepositWithdrawFormType type = AmountDepositWithdrawFormType.Deposit;
        public decimal Amount = 0;
        
        public AmountDepositWithdrawForm(AmountDepositWithdrawFormType amntType)
        {
            InitializeComponent();
            type = amntType;
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
                Gujjar.NumbersOnly(tbAmount);
                string msg = "";
                switch(type)
                {
                    case AmountDepositWithdrawFormType.Deposit:
                        msg = "Please enter deposit amount";
                        break;
                    case AmountDepositWithdrawFormType.Withdraw:
                        msg = "Please enter withdraw amount";
                        break;
                }

                label1.Text = msg;
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
            string txt = tbAmount.Text;
            try
            {
                if(string.IsNullOrEmpty(txt))
                {
                    throw new Exception("Please enter some amount in textbox");
                }
                Amount = txt.ToDecimal();
                if(Amount <= 0)
                {
                    throw new Exception("Please enter valid amount");
                }
                Close();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }

    public enum AmountDepositWithdrawFormType
    {
        Deposit,
        Withdraw
    }
}
