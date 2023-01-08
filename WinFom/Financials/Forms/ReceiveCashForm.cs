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
using Model.Employees.Model;

namespace WinFom.Financials.Forms
{

    public partial class ReceiveCashForm : Form
    {
        private List<GeneralAccount> banks = null;
        private List<BankTVM> bankVMList = new List<BankTVM>();
        private AppSettings AppSett = Helper.AppSet;
        public bool IsDone = false;
        private string debitAccountId = "";
        private GeneralAccount debitAccount = null;
        public ReceiveCashForm(string acctId)
        {
            InitializeComponent();
            debitAccountId = acctId;
        }

       
        private void LoadBanks()
        {
            try
            {
                if (banks != null)
                {
                    banks.Clear();
                    banks = null;
                }
                using (Context db = new Context())
                {
                    banks = db.Accounts.OfType<GeneralAccount>().Where(a => a.SubHeadAccountId == Properties.Resources.Banks).ToList();
                }

                if (bankVMList.Count > 0)
                {
                    bankVMList.Clear();
                }
                foreach (var item in banks)
                {
                    BankTVM tvm = new BankTVM
                    {
                        Id = item.Id,
                        Title = string.Format("{0}", item.Title)
                    };

                    bankVMList.Add(tvm);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadAccount()
        {
            try
            {
                using (Context db = new Context())
                {
                    debitAccount = db.Accounts.Find(debitAccountId) as GeneralAccount;
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadBoth()
        {
            LoadAccount();   
            LoadBanks();
        }
        
        private void BindCbBanks()
        {
            cbBanks.DataSource = bankVMList;
            cbBanks.DisplayMember = "Title";
            cbBanks.ValueMember = "Id";
        }

        private void BindBoth()
        {
            BindCbBanks();
            
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
                Gujjar.NumbersOnly(tbExpenseAmount);

                Gujjar.TBOptional(tbRemarks);
                Gujjar.TBOptional(tbDescription);

                WaitForm wait1 = new WaitForm(LoadBoth);
                wait1.ShowDialog();
                tbEmployee.Text = debitAccount.Title;
                lblHeading.Text = tbEmployee.Text;
                BindBoth();
                bswIsBank.Value = false;
                cbBanks.Enabled = false;
                tbDescription.Enabled = false;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void bswIsBank_Click(object sender, EventArgs e)
        {
            bool bval = bswIsBank.Value;

            cbBanks.Enabled = tbDescription.Enabled = bval;
            if (!bval)
            {
                tbDescription.Text = "N/A";
            }
        }

        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        if (!Gujjar.IsValidForm(pMain))
                        {
                            throw new Exception("Please fill text fields");
                        }
                       
                        GeneralAccount creditAccount = null;

                       
                        string tmsg = "";
                        if (bswIsBank.Value)
                        {
                            if (cbBanks.SelectedIndex == -1)
                            {
                                throw new Exception("Please select bank from the list");
                            }

                            BankTVM bankObj = cbBanks.SelectedItem as BankTVM;
                            creditAccount = db.Accounts.Find(bankObj.Id) as GeneralAccount;
                            tmsg = string.Format("Receive through bank account ({0}). Description ({1})", creditAccount.Title, tbDescription.Text);
                        }
                        else
                        {
                            creditAccount = db.Accounts.Find(Properties.Resources.CashInHand) as GeneralAccount;
                            tmsg = string.Format("Receive through cash in hand account");
                        }

                        var user = SingleTon.LoginForm.appUser;
                        decimal amount = tbExpenseAmount.Text.ToDecimal();

                        string finMsg = string.Format("Cash receive: ({0}). Amount ({1}). Remarks ({2}), ({3}). By ({4})", debitAccount.Title, 
                            amount.ToString("n1"), tbRemarks.Text, tmsg, user.Name);



                        #region "Financials"
                        DayBook daybookEntry = new DayBook
                        {
                            Id = 0,
                            Amount = amount,
                            Date = dtp.Value,
                            Description = finMsg,
                            CanRollBack = true,
                            InDate = DateTime.Now.Date
                        };

                        daybookEntry = db.DayBooks.Add(daybookEntry);
                        db.SaveChanges();

                        #region "Debit transaction"

                        AccountTransaction debitItemTrans = new AccountTransaction
                        {
                            Id = 0,
                            Account = null,
                            AccountTransactionType = AccountTransactionType.Debit,
                            Balance = amount,
                            CreditAmount = 0,
                            Date = DateTime.Now,
                            DayBookId = daybookEntry.Id,
                            DebitAmount = amount,
                            GeneralAccountId = creditAccount.Id,
                            Description = finMsg
                        };

                        var debitDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == creditAccount.Id).AsParallel()
                            .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                            .OrderByDescending(a => a.Id).FirstOrDefault();

                        if (debitDbEntry != null)
                        {
                            debitItemTrans.Balance += debitDbEntry.Balance;
                        }

                        debitItemTrans = db.AccountTransactions.Add(debitItemTrans);
                        db.SaveChanges();



                        #endregion

                        #region  "Credit transaction"
                        AccountTransaction creditItemTrans = new AccountTransaction
                        {
                            Id = 0,
                            Account = null,
                            AccountTransactionType = AccountTransactionType.Credit,
                            Balance = -amount,
                            CreditAmount = amount,
                            Date = DateTime.Now,
                            DayBookId = daybookEntry.Id,
                            DebitAmount = 0,
                            GeneralAccountId = debitAccount.Id,
                            Description = finMsg
                        };

                        var creditDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == debitAccount.Id).AsParallel()
                            .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                            .OrderByDescending(a => a.Id).FirstOrDefault();

                        if (creditDbEntry != null)
                        {
                            creditItemTrans.Balance += creditDbEntry.Balance;
                        }

                        creditItemTrans = db.AccountTransactions.Add(creditItemTrans);
                        db.SaveChanges();
                        #endregion

                        var daybookdb = db.DayBooks.Find(daybookEntry.Id);
                        daybookdb.DebitTrace = string.Format("({0}). Trans Id: {1}", creditAccount.Title, creditItemTrans.Id);
                        daybookdb.CreditTrace = string.Format("({0}). Trans Id: {1}", debitAccount.Title, debitItemTrans.Id);
                        daybookdb.CreditAccountId = debitAccount.Id;
                        daybookdb.DebitAccountId = creditAccount.Id;

                        db.Entry(daybookdb).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        #endregion

                       

                        trans.Commit();

                        if (AppSett.PrintFinancialTransactions)
                        {
                            Helper.PrintTransactions(new List<DayBook> { daybookdb });
                        }

                        Gujjar.InfoMsg("Cash is received record is stored in database successfully");
                        IsDone = true;
                        Close();
                        

                    }
                }
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
    }

   
}
