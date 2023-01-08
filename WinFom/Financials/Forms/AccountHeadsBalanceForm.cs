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
    public partial class AccountHeadsBalanceForm : Form
    {
        private string assetBal = "0", liabBal = "0", expenseBal = "0", incomeBal = "0";
        private AppSettings AppSet = Helper.AppSet;
        public AccountHeadsBalanceForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        decimal assetBalance = 0;
        decimal expenseBalance = 0;
        decimal liabilityBalance = 0;
        decimal incomeBalance = 0;
        decimal debitBal = 0;

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        decimal creditBal = 0;
        private void LoadData()
        {
            try
            {

                using (Context db = new Context())
                {
                    #region "Assets Balance Calculations"
                    var topAssetHeads = db.Accounts.OfType<TopHeadAccount>()
                        .Where(a => a.HeadAccountId == Properties.Resources.AssetsHead).ToList();
                    foreach (var item in topAssetHeads)
                    {
                        var subHeads = db.Accounts.OfType<SubHeadAccount>().Where(a => a.TopHeadAccountId == item.Id).ToList();
                        foreach (var item2 in subHeads)
                        {
                            var accounts = db.Accounts.OfType<GeneralAccount>().Where(a => a.SubHeadAccountId == item2.Id).ToList();

                            foreach (var item3 in accounts)
                            {
                                var trans = db.AccountTransactions.Where(a => a.GeneralAccountId == item3.Id).AsParallel().ToList()
                                    .Where(a => a.Date.Date >= AppSet.StartDate.Date && a.Date.Date <= AppSet.EndDate.Date)
                                    .OrderByDescending(a => a.Id).FirstOrDefault();
                                if (trans != null)
                                    assetBalance += trans.Balance;
                            }
                        }
                    }
                    #endregion

                    #region Expense Balance Calculations"
                    var topExpenseHead = db.Accounts.OfType<TopHeadAccount>()
                        .Where(a => a.HeadAccountId == Properties.Resources.ExpensesHead).ToList();
                    foreach (var item in topExpenseHead)
                    {
                        var subHeads = db.Accounts.OfType<SubHeadAccount>().Where(a => a.TopHeadAccountId == item.Id).ToList();
                        foreach (var item2 in subHeads)
                        {
                            var accounts = db.Accounts.OfType<GeneralAccount>().Where(a => a.SubHeadAccountId == item2.Id).ToList();

                            foreach (var item3 in accounts)
                            {
                                var trans = db.AccountTransactions.Where(a => a.GeneralAccountId == item3.Id).AsParallel().ToList()
                                    .Where(a => a.Date.Date >= AppSet.StartDate.Date && a.Date.Date <= AppSet.EndDate.Date)
                                    .OrderByDescending(a => a.Id).FirstOrDefault();

                                if (trans != null)
                                    expenseBalance += trans.Balance;
                            }
                        }
                    }
                    #endregion

                    #region Liability Balance Calculations"
                    var topLiabilityHeads = db.Accounts.OfType<TopHeadAccount>()
                        .Where(a => a.HeadAccountId == Properties.Resources.LiabilitiesHead).ToList();
                    foreach (var item in topLiabilityHeads)
                    {
                        var subHeads = db.Accounts.OfType<SubHeadAccount>().Where(a => a.TopHeadAccountId == item.Id).ToList();
                        foreach (var item2 in subHeads)
                        {
                            var accounts = db.Accounts.OfType<GeneralAccount>().Where(a => a.SubHeadAccountId == item2.Id).ToList();

                            foreach (var item3 in accounts)
                            {
                                var trans = db.AccountTransactions.Where(a => a.GeneralAccountId == item3.Id).AsParallel().ToList()
                                    .Where(a => a.Date.Date >= AppSet.StartDate.Date && a.Date.Date <= AppSet.EndDate.Date)
                                    .OrderByDescending(a => a.Id).FirstOrDefault();
                                if (trans != null)
                                    liabilityBalance += trans.Balance;
                            }
                        }
                    }
                    #endregion

                    #region "Income Balance Calculations"
                    var incomeTopHeads = db.Accounts.OfType<TopHeadAccount>()
                        .Where(a => a.HeadAccountId == Properties.Resources.IncomeHead).ToList();
                    foreach (var item in incomeTopHeads)
                    {
                        var subHeads = db.Accounts.OfType<SubHeadAccount>().Where(a => a.TopHeadAccountId == item.Id).ToList();
                        foreach (var item2 in subHeads)
                        {
                            var accounts = db.Accounts.OfType<GeneralAccount>().Where(a => a.SubHeadAccountId == item2.Id).ToList();

                            foreach (var item3 in accounts)
                            {
                                var trans = db.AccountTransactions.Where(a => a.GeneralAccountId == item3.Id).AsParallel().ToList()
                                    .Where(a => a.Date.Date >= AppSet.StartDate.Date && a.Date.Date <= AppSet.EndDate.Date)
                                    .OrderByDescending(a => a.Id).FirstOrDefault();
                                if (trans != null)
                                    incomeBalance += trans.Balance;
                            }
                        }
                    }
                    #endregion

                    assetBal = string.Format("Assets ({0})", assetBalance.ToString("n2"));
                    liabBal = string.Format("Liabilities ({0})", liabilityBalance.ToString("n2"));
                    expenseBal = string.Format("Expenses ({0})", expenseBalance.ToString("n2"));
                    incomeBal = string.Format("Income ({0})", incomeBalance.ToString("n2"));
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
                Helper.IsOkApplied();
                WaitForm form = new WaitForm(LoadData);
                form.ShowDialog();

                assetsLabel.Text = assetBal;
                labelExpense.Text = expenseBal;
                labelIncome.Text = incomeBal;
                labelLiability.Text = liabBal;

                debitBal = assetBalance + expenseBalance;
                creditBal = liabilityBalance + incomeBalance;

                tbCredit.Text = creditBal.ToString("n2");
                tbDebit.Text = debitBal.ToString("n2");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
