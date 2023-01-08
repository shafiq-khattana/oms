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
    public partial class AmountDepositWithdrawBankForm : Form
    {
        private AmountDepositWithdrawFormType type = AmountDepositWithdrawFormType.Deposit;
        public BankTransactionTransferVM BankTransactionTranfer = null;
        private GeneralAccount bankAccount = null;
        private string acctId = "";

        public AmountDepositWithdrawBankForm(AmountDepositWithdrawFormType amntType, string accountId)
        {
            InitializeComponent();
            type = amntType;
            acctId = accountId;
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
                    bankAccount = db.Accounts.Find(acctId) as GeneralAccount;
                    label1.Text = bankAccount.Title;
                }

                Gujjar.TB4(pMain);
                Gujjar.NumbersOnly(tbAmount);
                Gujjar.TBOptional(tbDescription);
                tbDescription.Text = "N/A";
                string msg = "";

                switch (type)
                {
                    case AmountDepositWithdrawFormType.Deposit:
                        msg = "Please enter deposit amount";
                        break;
                    case AmountDepositWithdrawFormType.Withdraw:
                        msg = "Please enter withdraw amount";
                        break;
                }

                label1.Text = string.Format("{0}\nBank: {1}",msg, bankAccount.Title);
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
            string description = tbDescription.Text;
            try
            {
                if (string.IsNullOrEmpty(txt))
                {
                    throw new Exception("Please enter some amount in textbox");
                }
                if (string.IsNullOrEmpty(description))
                {
                    throw new Exception("Please enter description in textbox");
                }
                BankTransactionTranfer = new BankTransactionTransferVM
                {
                    Amount = txt.ToDecimal(),
                    Description = description
                };

                if (BankTransactionTranfer.Amount <= 0)
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
    public class BankTransactionTransferVM
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }

}
