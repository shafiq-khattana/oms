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
    public partial class AddAccruedExpenseForm : Form
    {
        public bool IsDone { get; set; }
        public AddAccruedExpenseForm()
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
                tbDescription.Text = "N/A";
                Gujjar.TBOptional(tbDescription);
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
                string itemTitle = tbItem.Text;
                if(string.IsNullOrEmpty(itemTitle))
                {
                    throw new Exception("Please add expense title");
                }
                
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var itemObj = db.AccruedExpenseItems.ToList().FirstOrDefault(a => a.Title.ToLower().Equals(itemTitle.ToLower()));
                            if(itemObj != null)
                            {
                                throw new Exception("Item and its accounts already created");
                            }

                           
                            string subHeadId = Properties.Resources.AccruedExpensesSubHead;
                            
                            GeneralAccount expAccount = new GeneralAccount
                            {
                                Title = tbDebitAccount.Text,
                                AccountNature = AccountNature.Debit,
                                AccountNo = "N/A",
                                Description = string.Format("{0} (accrued expense) account", tbItem.Text),
                                Address = "N/A",
                                Balance = 0,
                                BankName = "N/A",
                                ExplicitilyCreated = true,
                                SubHeadAccountId = subHeadId,
                                Id = Guid.NewGuid().ToString()
                            };

                            db.Accounts.Add(expAccount);
                            db.SaveChanges();

                            string subHeadPayable = Properties.Resources.AccruedPayablesSubHead;

                            GeneralAccount payableAccount = new GeneralAccount
                            {
                                Title = tbCreditAccount.Text,
                                AccountNature = AccountNature.Credit,
                                AccountNo = "N/A",
                                Description = string.Format("{0} (payable) account", tbItem.Text),
                                Address = "N/A",
                                Balance = 0,
                                BankName = "N/A",
                                ExplicitilyCreated = true,
                                SubHeadAccountId = subHeadPayable,
                                Id = Guid.NewGuid().ToString()
                            };

                            db.Accounts.Add(payableAccount);
                            db.SaveChanges();

                            AccruedExpenseItem aitem = new AccruedExpenseItem
                            {
                                CreditAccount = null,
                                CreditAccountId = payableAccount.Id,
                                Id = 0,
                                DebitAccount = null,
                                DebitAccountId = expAccount.Id,
                                Title = tbItem.Text
                            };

                            db.AccruedExpenseItems.Add(aitem);
                            db.SaveChanges();

                            trans.Commit();

                            Gujjar.InfoMsg(string.Format("Accounts created expense ({0}) and payable ({1}) successfully and saved in database.", expAccount.Title, payableAccount.Title));
                            IsDone = true;
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

        private void tbItem_TextChanged(object sender, EventArgs e)
        {
            string itemText = tbItem.Text;
            string dtxt = "";
            string ctxt = "";
            if(string.IsNullOrEmpty(itemText))
            {
                dtxt = "";
                ctxt = "";
            }
            else
            {
                dtxt = string.Format("{0} (expense) account", itemText);
                ctxt = string.Format("{0} (payable) account", itemText);
            }
            tbCreditAccount.Text = ctxt;
            tbDebitAccount.Text = dtxt;
        }
    }
}
