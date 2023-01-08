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
    public partial class AddExpenseItemForm : Form
    {
        private ExpenseType expenseType = ExpenseType.Financial;
        public int ItemId = 0;
        public AddExpenseItemForm(ExpenseType expType)
        {
            InitializeComponent();
            expenseType = expType;
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
                string title = tbExpenseItem.Text;
                if(string.IsNullOrEmpty(title))
                {
                    throw new Exception("Please add expense title");
                }

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var expItemDb = db.ExpenseItems.FirstOrDefault(a => a.Title == title && expenseType == a.ExpenseType);
                            if(expItemDb != null)
                            {
                                throw new Exception("Expense Item is already added");
                            }

                           
                            string subHeadId = Properties.Resources.GeneralExpenseHead;
                            string eType = "General Expense";
                            if(expenseType == ExpenseType.Financial)
                            {
                                subHeadId = Properties.Resources.FinancialExpenseSubHead;
                                eType = "Financial Expense";
                            }

                            GeneralAccount expAccount = new GeneralAccount
                            {
                                Title = string.Format("{0} ({1} expense) account", title, eType),
                                AccountNature = AccountNature.Debit,
                                AccountNo = "N/A",
                                Description = string.Format("{0} ({1} expense) account", title, eType),
                                Address = "N/A",
                                Balance = 0,
                                BankName = "N/A",
                                ExplicitilyCreated = true,
                                SubHeadAccountId = subHeadId,
                                Id = Guid.NewGuid().ToString()
                            };

                            db.Accounts.Add(expAccount);
                            db.SaveChanges();

                            ExpenseItem expItem = new ExpenseItem
                            {
                                Account = null,
                                ExpenseType = expenseType,
                                ExpenseEntries = null,
                                GeneralAccountId = expAccount.Id,
                                Id = 0,
                                Title = title
                            };
                            expItem = db.ExpenseItems.Add(expItem);
                            db.SaveChanges();

                            trans.Commit();

                            Gujjar.InfoMsg(string.Format("Expense Item ({0}) has been added in database successfully", expItem.Title));
                            ItemId = expItem.Id;
                            Close();
                        }
                        catch (Exception exp2)
                        {
                            trans.Rollback();
                            throw exp2;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
