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
using Khattana.Display;
using Khattana.Model;
using Model.Financials.ViewModel;

namespace WinFom.Financials.Forms
{
    public partial class ZakatAccountForm : Form
    {
        private AppSettings AppSett = Helper.AppSet;
        private List<AccountTransaction> transactions = null;
        private List<AccountTransaction> tempTrans = null;
        private decimal balance = 0;
        public ZakatAccountForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PopulateTempTrans(List<AccountTransaction> trans)
        {
            if(tempTrans != null)
            {
                tempTrans.Clear();
                tempTrans = null;
            }
            tempTrans = new List<AccountTransaction>();
            foreach (var item in trans)
            {
                tempTrans.Add(item);
            }
        }
        private void LoadTransactions()
        {
            try
            {
                if(transactions != null)
                {
                    transactions.Clear();
                    transactions = null;
                }
                using (Context db = new Context())
                {
                    transactions = db.AccountTransactions.Where(a => a.GeneralAccountId == Properties.Resources.ZakatPayableAccount)
                        .AsParallel().ToList()
                        .Where(a => a.Date.Date >= AppSett.StartDate && a.Date.Date <= AppSett.EndDate).ToList();
                    

                    PopulateTempTrans(transactions);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void UpdateDgv(List<AccountTransaction> trans)
        {
            
            
            transactionsVMBindingSource.List.Clear();
            tbEntriesCount.Text = trans.Count.ToString();

            decimal debit = 0;
            decimal credit = 0;
            if(trans.Count > 0)
            {
                debit = trans.Where(a => a.AccountTransactionType == AccountTransactionType.Debit).Sum(a => a.DebitAmount);
                credit = trans.Where(a => a.AccountTransactionType == AccountTransactionType.Credit).Sum(a => a.CreditAmount);
            }

            tbDebit.Text = debit.ToString("n4");
            tbCredit.Text = credit.ToString("n4");
            try
            {
                foreach (var item in trans)
                {
                    TransactionsVM vm = new TransactionsVM
                    {
                        Id = item.Id,                        
                        Balance = item.Balance.ToString("n3"),
                        CreditAmount = item.CreditAmount.ToString("n3"),
                        Date = item.Date.ToString(),
                        DayBookId = item.DayBookId,
                        DebitAmount = item.DebitAmount.ToString("n3"),
                        Description = item.Description
                    };
                    transactionsVMBindingSource.List.Add(vm);
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
                WaitForm wait = new WaitForm(LoadTransactions);
                wait.ShowDialog();

                UpdateDgv(transactions);

                var obj = transactions.OrderByDescending(a => a.Id).FirstOrDefault();
                if (obj != null)
                {
                    balance = obj.Balance;
                }
                else
                {
                    balance = 0;
                }
                tbAccountBalance.Text = balance.ToString("n4");
                cbTransTypes.Text = "Both";
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

       

        private void btnAccountView_Click(object sender, EventArgs e)
        {
            try
            {
                string type = cbTransTypes.Text;
                switch(type)
                {
                    case "Both":
                        UpdateDgv(tempTrans);
                        break;

                    case "Debit":
                        UpdateDgv(tempTrans.Where(a => a.AccountTransactionType == AccountTransactionType.Debit).ToList());
                        break;

                    case "Credit":
                        UpdateDgv(tempTrans.Where(a => a.AccountTransactionType == AccountTransactionType.Credit).ToList());
                        break;
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            tempTrans = transactions.Where(a => a.Date.Date >= dtpFrom.Value.Date && a.Date.Date <= dtpTo.Value.Date).ToList();
            UpdateDgv(tempTrans);
        }

        private void btnAllRecords_Click(object sender, EventArgs e)
        {
            PopulateTempTrans(transactions);
            UpdateDgv(tempTrans);
        }

        private void btnTodayOnly_Click(object sender, EventArgs e)
        {
            tempTrans = tempTrans.Where(a => a.Date.Date == DateTime.Now.Date).ToList();
            UpdateDgv(tempTrans);
        }

        private void cbTransTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTransTypes.SelectedIndex == -1)
                return;
            try
            {
                string type = cbTransTypes.Text;
                switch (type)
                {
                    case "Both":
                        UpdateDgv(tempTrans);
                        break;

                    case "Debit":
                        UpdateDgv(tempTrans.Where(a => a.AccountTransactionType == AccountTransactionType.Debit).ToList());
                        break;

                    case "Credit":
                        UpdateDgv(tempTrans.Where(a => a.AccountTransactionType == AccountTransactionType.Credit).ToList());
                        break;
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
            
        }

        private void btnAddZakat_Click(object sender, EventArgs e)
        {
            try
            {
                DeductZakatForm form = new DeductZakatForm();
                form.ShowDialog();

                if(form.IsDone)
                {
                    WaitForm wait = new WaitForm(LoadTransactions);
                    wait.ShowDialog();

                    UpdateDgv(transactions);

                    var obj = transactions.OrderByDescending(a => a.Id).FirstOrDefault();
                    if (obj != null)
                    {
                        balance = obj.Balance;
                    }
                    else
                    {
                        balance = 0;
                    }
                    tbAccountBalance.Text = balance.ToString("n4");
                    cbTransTypes.Text = "Both";
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnPayZakat_Click(object sender, EventArgs e)
        {
            try
            {
                PayZakatForm form = new PayZakatForm();
                form.ShowDialog();
                if(form.IsDone)
                {
                    WaitForm wait = new WaitForm(LoadTransactions);
                    wait.ShowDialog();

                    UpdateDgv(transactions);

                    var obj = transactions.OrderByDescending(a => a.Id).FirstOrDefault();
                    if (obj != null)
                    {
                        balance = obj.Balance;
                    }
                    else
                    {
                        balance = 0;
                    }
                    tbAccountBalance.Text = balance.ToString("n4");
                    cbTransTypes.Text = "Both";
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
