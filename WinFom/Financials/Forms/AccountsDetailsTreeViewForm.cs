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
    public partial class AccountsDetailsTreeViewForm : Form
    {
        //private string assetId = Properties.Resources.AssetsHead;
        //private string liabilityId = Properties.Resources.LiabilitiesHead;
        //private string expenseId = Properties.Resources.ExpensesHead;
        //private string incomeId = Properties.Resources.IncomeHead;
        //private HeadAccount assetHead = null;
        //private HeadAccount liabilityHead = null;
        //private HeadAccount expenseHead = null;
        //private HeadAccount incomeHead = null;

        private AppSettings AppSet = Helper.AppSet;

        private decimal assetBalance = 0;
        private decimal expenseBalance = 0;
        private decimal liabilityBalance = 0;
        private decimal incomeBalance = 0;

        private List<TreeNode> nodes = new List<TreeNode>
        {
            new TreeNode {Name = "asset" },
            new TreeNode { Name = "liability" },
            new TreeNode { Name = "expense" },
            new TreeNode { Name = "income" }
        };

        ImageList imgList = new ImageList();
        
        private void LoadData()
        {
            try
            {
                
                using (Context db = new Context())
                {
                    #region "Assets Balance Calculations"
                    var topAssetHeads = db.Accounts.OfType<TopHeadAccount>().Where(a => a.HeadAccountId == Properties.Resources.AssetsHead).ToList();
                    foreach (var item in topAssetHeads)
                    {
                        decimal topHeadBalance = 0;
                        TreeNode topHeadNode = new TreeNode();
                        nodes[0].Nodes.Add(topHeadNode);

                        var subHeads = db.Accounts.OfType<SubHeadAccount>().Where(a => a.TopHeadAccountId == item.Id).ToList();
                        foreach (var item2 in subHeads)
                        {
                            decimal subHeadBalance = 0;
                            TreeNode subHeadNode = new TreeNode();
                            topHeadNode.Nodes.Add(subHeadNode);
                            var accounts = db.Accounts.OfType<GeneralAccount>().Where(a => a.SubHeadAccountId == item2.Id).ToList();

                            foreach (var item3 in accounts)
                            {
                                decimal accountBalance = 0;
                                TreeNode accountNode = new TreeNode();
                                subHeadNode.Nodes.Add(accountNode);
                                accountNode.ToolTipText = item3.Title;
                                //accountNode.ImageKey = "asset";

                                var trans = db.AccountTransactions.Where(a => a.GeneralAccountId == item3.Id).AsParallel().ToList()
                                    .Where(a =>
                                    a.Date.Date >= AppSet.StartDate.Date && a.Date.Date <= AppSet.EndDate.Date)
                                    .OrderByDescending(a => a.Id)
                                    .FirstOrDefault();

                                if (trans != null)
                                {
                                    assetBalance += trans.Balance;
                                    topHeadBalance += trans.Balance;
                                    subHeadBalance += trans.Balance;
                                    accountBalance = trans.Balance;
                                }



                                accountNode.Text = string.Format("{0} ({1})", item3.Title, accountBalance.ToString("n4"));
                            }
                            subHeadNode.Text = string.Format("{0} ({1})", item2.Title, subHeadBalance.ToString("n4"));
                        }

                        topHeadNode.Text = string.Format("{0} ({1})", item.Title, topHeadBalance.ToString("n4"));
                    }
                    #endregion

                    #region "Liability Balance Calculations"
                    var topLiabilityHeads = db.Accounts.OfType<TopHeadAccount>()
                        .Where(a => a.HeadAccountId == Properties.Resources.LiabilitiesHead).ToList();
                    foreach (var item in topLiabilityHeads)
                    {
                        decimal topHeadBalance = 0;
                        TreeNode topHeadNode = new TreeNode();
                        nodes[1].Nodes.Add(topHeadNode);

                        var subHeads = db.Accounts.OfType<SubHeadAccount>().Where(a => a.TopHeadAccountId == item.Id).ToList();
                        foreach (var item2 in subHeads)
                        {
                            decimal subHeadBalance = 0;
                            TreeNode subHeadNode = new TreeNode();
                            topHeadNode.Nodes.Add(subHeadNode);
                            var accounts = db.Accounts.OfType<GeneralAccount>().Where(a => a.SubHeadAccountId == item2.Id).ToList();

                            foreach (var item3 in accounts)
                            {
                                decimal accountBalance = 0;
                                TreeNode accountNode = new TreeNode();
                                subHeadNode.Nodes.Add(accountNode);

                                var trans = db.AccountTransactions.Where(a => a.GeneralAccountId == item3.Id).AsParallel().ToList()
                                    .Where(a =>
                                    a.Date.Date >= AppSet.StartDate.Date && a.Date.Date <= AppSet.EndDate.Date)
                                    .OrderByDescending(a => a.Id)
                                    .FirstOrDefault();

                                if (trans != null)
                                {
                                    liabilityBalance += trans.Balance;
                                    topHeadBalance += trans.Balance;
                                    subHeadBalance += trans.Balance;
                                    accountBalance = trans.Balance;
                                }



                                accountNode.Text = string.Format("{0} ({1})", item3.Title, accountBalance.ToString("n4"));
                            }
                            subHeadNode.Text = string.Format("{0} ({1})", item2.Title, subHeadBalance.ToString("n4"));
                        }
                        topHeadNode.Text = string.Format("{0} ({1})", item.Title, topHeadBalance.ToString("n4"));
                    }
                    #endregion

                    #region "Expense Balance Calculations"
                    var topExpenseHeads = db.Accounts.OfType<TopHeadAccount>()
                        .Where(a => a.HeadAccountId == Properties.Resources.ExpensesHead).ToList();
                    foreach (var item in topExpenseHeads)
                    {
                        decimal topHeadBalance = 0;
                        TreeNode topHeadNode = new TreeNode();
                        nodes[2].Nodes.Add(topHeadNode);

                        var subHeads = db.Accounts.OfType<SubHeadAccount>().Where(a => a.TopHeadAccountId == item.Id).ToList();
                        foreach (var item2 in subHeads)
                        {
                            decimal subHeadBalance = 0;
                            TreeNode subHeadNode = new TreeNode();
                            topHeadNode.Nodes.Add(subHeadNode);
                            var accounts = db.Accounts.OfType<GeneralAccount>().Where(a => a.SubHeadAccountId == item2.Id).ToList();

                            foreach (var item3 in accounts)
                            {
                                decimal accountBalance = 0;
                                TreeNode accountNode = new TreeNode();
                                subHeadNode.Nodes.Add(accountNode);



                                var trans = db.AccountTransactions.Where(a => a.GeneralAccountId == item3.Id).AsParallel().ToList()
                                    .Where(a =>
                                    a.Date.Date >= AppSet.StartDate.Date && a.Date.Date <= AppSet.EndDate.Date)
                                    .OrderByDescending(a => a.Id)
                                    .FirstOrDefault();

                                if (trans != null)
                                {
                                    expenseBalance += trans.Balance;
                                    topHeadBalance += trans.Balance;
                                    subHeadBalance += trans.Balance;
                                    accountBalance = trans.Balance;
                                }



                                accountNode.Text = string.Format("{0} ({1})", item3.Title, accountBalance.ToString("n4"));
                            }
                            subHeadNode.Text = string.Format("{0} ({1})", item2.Title, subHeadBalance.ToString("n4"));
                        }
                        topHeadNode.Text = string.Format("{0} ({1})", item.Title, topHeadBalance.ToString("n4"));
                    }
                    #endregion


                    #region "Income Balance Calculations"
                    var incomeTopHeads = db.Accounts.OfType<TopHeadAccount>()
                        .Where(a => a.HeadAccountId == Properties.Resources.IncomeHead).ToList();
                    foreach (var item in incomeTopHeads)
                    {
                        decimal topHeadBalance = 0;
                        TreeNode topHeadNode = new TreeNode();
                        nodes[3].Nodes.Add(topHeadNode);

                        var subHeads = db.Accounts.OfType<SubHeadAccount>().Where(a => a.TopHeadAccountId == item.Id).ToList();
                        foreach (var item2 in subHeads)
                        {
                            decimal subHeadBalance = 0;
                            TreeNode subHeadNode = new TreeNode();
                            topHeadNode.Nodes.Add(subHeadNode);
                            var accounts = db.Accounts.OfType<GeneralAccount>().Where(a => a.SubHeadAccountId == item2.Id).ToList();

                            foreach (var item3 in accounts)
                            {
                                decimal accountBalance = 0;
                                TreeNode accountNode = new TreeNode();
                                subHeadNode.Nodes.Add(accountNode);

                                var trans = db.AccountTransactions.Where(a => a.GeneralAccountId == item3.Id).AsParallel().ToList()
                                    .Where(a =>
                                    a.Date.Date >= AppSet.StartDate.Date && a.Date.Date <= AppSet.EndDate.Date)
                                    .OrderByDescending(a => a.Id)
                                    .FirstOrDefault();

                                if (trans != null)
                                {
                                    incomeBalance += trans.Balance;
                                    topHeadBalance += trans.Balance;
                                    subHeadBalance += trans.Balance;
                                    accountBalance = trans.Balance;
                                }






                                accountNode.Text = string.Format("{0} ({1})", item3.Title, accountBalance.ToString("n4"));
                            }
                            subHeadNode.Text = string.Format("{0} ({1})", item2.Title, subHeadBalance.ToString("n4"));
                        }
                        topHeadNode.Text = string.Format("{0} ({1})", item.Title, topHeadBalance.ToString("n4"));
                    }
                    #endregion

                    assetBal = string.Format("Assets ({0})", assetBalance.ToString("n4"));
                    liabBal = string.Format("Liabilities ({0})", liabilityBalance.ToString("n4"));
                    expenseBal = string.Format("Expenses ({0})", expenseBalance.ToString("n4"));
                    incomeBal = string.Format("Income ({0})", incomeBalance.ToString("n4"));
                    nodes[0].Text = assetBal;
                    nodes[1].Text = liabBal;
                    nodes[2].Text = expenseBal;
                    nodes[3].Text = incomeBal;
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private string assetBal = "";
        private string liabBal = "";
        private string expenseBal = "";
        private string incomeBal = "";
        public AccountsDetailsTreeViewForm()
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
                //imgList.Images.Add("asset", Properties.Resources.accounts);
                //imgList.Images.Add("expense", Properties.Resources.expenseBalance);
                //tw.ImageList = imgList;

                WaitForm wait1 = new WaitForm(LoadData);
                wait1.ShowDialog();

                tw.Nodes.AddRange(nodes.ToArray());

                decimal debit = assetBalance + expenseBalance;
                decimal credit = liabilityBalance + incomeBalance;

                tbCredit.Text = credit.ToString("n4");
                tbDebit.Text = debit.ToString("n4");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
