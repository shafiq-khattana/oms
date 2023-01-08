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
    public partial class GeneralExpenseForm : Form
    {
        private AppSettings AppSett = Helper.AppSet;
        private List<ExpenseItemEntry> expenseEntries = null;
        
        private bool isDated = false;
        DateTime dtFrom = DateTime.Now;
        DateTime dtTo = DateTime.Now;
        private bool isAll = false;
        public GeneralExpenseForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void LoadEntries()
        {
            try
            {
                if(expenseEntries != null)
                {
                    
                    expenseEntries.Clear();
                    expenseEntries = null;

                }

                using (Context db = new Context())
                {
                    if(isDated)
                    {
                        expenseEntries = db.ExpenseItemEntries.Where(a => a.ExpenseType == ExpenseType.General)
                            .AsParallel().ToList()
                            .Where(a => a.ExpenseDate.Date >= dtFrom && a.ExpenseDate.Date <= dtTo)
                            .ToList();
                    }
                    else if(isAll)
                    {
                        expenseEntries = db.ExpenseItemEntries.Where(a => a.ExpenseType == ExpenseType.General)
                            .AsParallel().ToList()
                            .ToList();
                    }
                    else
                    {
                        expenseEntries = db.ExpenseItemEntries.Where(a => a.ExpenseType == ExpenseType.General)
                            .AsParallel().ToList()
                            .Where(a => a.ExpenseDate.Date == DateTime.Now.Date)
                            .ToList();
                    }
                    foreach (var item in expenseEntries)
                    {
                        item.ExpenseItem = db.ExpenseItems.Find(item.ExpenseItemId);
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void UpdateDgv()
        {
            decimal balance = 0;
            expenseEntryVMBindingSource.List.Clear();
            if(expenseEntries.Count > 0)
            {
                balance = expenseEntries.Sum(a => a.ExpenseAmount);
            }
            tbAccountBalance.Text = balance.ToString("n2");
            tbEntriesCount.Text = expenseEntries.Count.ToString();
            try
            {
                foreach (var item in expenseEntries)
                {
                    ExpenseEntryVM vm = new ExpenseEntryVM
                    {
                        Id = item.Id,
                        ExpenseAmount = item.ExpenseAmount.ToString("n2"),
                        DayBookId = item.DayBookId,
                        Description = item.Description,
                        ExpenseDate = item.ExpenseDate.ToString(),
                        ExpenseItem = item.ExpenseItem.Title,
                        Remarks = item.Remarks
                    };

                    expenseEntryVMBindingSource.List.Add(vm);
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
                isAll = false;
                isDated = false;
                WaitForm wait = new WaitForm(LoadEntries);
                wait.ShowDialog();

                UpdateDgv();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAllRecords_Click(object sender, EventArgs e)
        {
            try
            {
                isAll = true;
                isDated = false;
                WaitForm wait = new WaitForm(LoadEntries);
                wait.ShowDialog();

                UpdateDgv();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnTodayOnly_Click(object sender, EventArgs e)
        {
            try
            {
                isAll = false;
                isDated = false;

                WaitForm wait = new WaitForm(LoadEntries);
                wait.ShowDialog();

                UpdateDgv();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                dtFrom = dtpFrom.Value.Date;
                dtTo = dtpTo.Value.Date;
                isDated = true;
                isAll = false;

                WaitForm wait = new WaitForm(LoadEntries);
                wait.ShowDialog();

                UpdateDgv();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddExpense_Click(object sender, EventArgs e)
        {
            try
            {
                AddFinancialExpenseEntryForm form = new AddFinancialExpenseEntryForm(ExpenseType.General);
                form.ShowDialog();

                if(form.IsDone)
                {
                    isAll = false;
                    isDated = false;

                    WaitForm wait = new WaitForm(LoadEntries);
                    wait.ShowDialog();

                    UpdateDgv();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
