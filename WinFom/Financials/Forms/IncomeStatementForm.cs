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
using WinFom.Financials.Reports.XtraReports;
using WinFom.Financials.Reports.Model;
using DevExpress.XtraReports.UI;

namespace WinFom.Financials.Forms
{
    public partial class IncomeStatementForm : Form
    {
        private DateTime? startDate;
        private DateTime? endDate;
        private AppSettings appSett;
        List<IncAcctExpLVM> expenseVMList = new List<IncAcctExpLVM>();
        List<IncAcctIncLVM> incomeVMList = new List<IncAcctIncLVM>();
        List<CapAcctVM> capitalAcctList = new List<CapAcctVM>();
        private decimal netProfit = 0;
        private string expRemBtnDgv = "aasdfasdaaadaw";
        private string incRemBtnDgv = "incomedgvemo";
        public IncomeStatementForm()
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
                Gujjar.AddDatagridviewButton(capdgv, "dgvbtnadd", "Deduct", "Deduct");
                Gujjar.AddDatagridviewButton(dgvExpenses, expRemBtnDgv, "X", "X", 20);
                Gujjar.AddDatagridviewButton(dgvIncome, incRemBtnDgv, "X", "X", 20);
                WaitForm wait1 = new WaitForm(LoadSettings);
                wait1.ShowDialog();
                startDate = null;
                endDate = null;
                labelInfo.Text = string.Format("Showing all information from ({0}) to ({1})", appSett.StartDate.ToShortDateString(), appSett.EndDate.ToShortDateString());
                WaitForm wait2 = new WaitForm(LoadData);
                wait2.ShowDialog();
                UpdateForm();
                tbDeduction.Text = "0.0";
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void UpdateDeduction()
        {
            tbDeduction.Text = capitalAcctList.Sum(a => a.DeductionAmount).ToString("n1");
        }
        private void LoadData()
        {
            try
            {
                expenseVMList.Clear();
                incomeVMList.Clear();
                capitalAcctList.Clear();

                using (Context db = new Context())
                {
                    if (startDate == null || endDate == null)
                    {
                        startDate = appSett.StartDate.Date;
                        endDate = appSett.EndDate.Date;
                    }

                    #region Expenses
                    var expenseTopHeads = db.Accounts.OfType<TopHeadAccount>()
                        .Where(a => a.HeadAccountId == Properties.Resources.ExpensesHead).ToList();

                    foreach (var tophead in expenseTopHeads)
                    {
                        var subheads = db.Accounts.OfType<SubHeadAccount>()
                            .Where(a => a.TopHeadAccountId == tophead.Id).ToList();
                        foreach (var subhead in subheads)
                        {
                            var accounts = db.Accounts.OfType<GeneralAccount>()
                                .Where(a => a.SubHeadAccountId == subhead.Id)
                                .OrderBy(a => a.Title).ToList();
                            foreach (var item in accounts)
                            {
                                decimal balance = 0;
                                var lastTrans = db.AccountTransactions.Where(a => a.GeneralAccountId == item.Id).AsParallel().ToList()
                                    .Where(a => a.Date >= startDate && a.Date <= endDate)
                                    .OrderByDescending(a => a.Id).FirstOrDefault();
                                if (lastTrans != null)
                                {
                                    balance = lastTrans.Balance;

                                }
                                IncAcctExpLVM vm = new IncAcctExpLVM
                                {
                                    Account = item.Title,
                                    Balance = balance
                                };
                                if(vm.Balance != 0)
                                    expenseVMList.Add(vm);
                            }

                        }
                    }
                    #endregion

                    #region Income
                    var incomeTopHeads = db.Accounts.OfType<TopHeadAccount>()
                        .Where(a => a.HeadAccountId == Properties.Resources.IncomeHead).ToList();

                    foreach (var tophead in incomeTopHeads)
                    {
                        var subheads = db.Accounts.OfType<SubHeadAccount>()
                            .Where(a => a.TopHeadAccountId == tophead.Id).ToList();
                        foreach (var subhead in subheads)
                        {
                            var accounts = db.Accounts.OfType<GeneralAccount>()
                                .Where(a => a.SubHeadAccountId == subhead.Id)
                                .OrderBy(a => a.Title).ToList();
                            foreach (var item in accounts)
                            {
                                decimal balance = 0;
                                var lastTrans = db.AccountTransactions.Where(a => a.GeneralAccountId == item.Id).AsParallel().ToList()
                                    .Where(a => a.Date >= startDate && a.Date <= endDate)
                                    .OrderByDescending(a => a.Id).FirstOrDefault();
                                if (lastTrans != null)
                                {
                                    balance = lastTrans.Balance;


                                }
                                IncAcctIncLVM vm = new IncAcctIncLVM
                                {
                                    Account = item.Title,
                                    Balance = balance
                                };
                                if(vm.Balance != 0)
                                    incomeVMList.Add(vm);
                            }

                        }
                    }
                    #endregion

                    #region Capital accounts
                    var acctList = db.Accounts.OfType<GeneralAccount>()
                        .Where(a => a.SubHeadAccountId == Properties.Resources.CapitalAccountSubHead)
                        .OrderBy(a => a.Title)
                        .ToList();
                    
                    foreach (var item in acctList)
                    {
                        decimal balance = 0;
                        var lastTrans = db.AccountTransactions.Where(a => a.GeneralAccountId == item.Id)
                            .OrderByDescending(a => a.Id)
                            .FirstOrDefault();
                        if (lastTrans != null)
                        {
                            balance = lastTrans.Balance;
                        }
                        CapAcctVM vm = new CapAcctVM
                        {
                            AcctId = item.Id,
                            DeductionAmount = 0,
                            DeductionRatio = 0,
                            DepositedAmount = balance,
                            SharedAmount = 0,
                            SharePercentage = 0,
                            Title = item.Title,
                            
                        };
                        capitalAcctList.Add(vm);
                    }
                    decimal totalCapital = capitalAcctList.Sum(a => a.DepositedAmount);
                    netProfit = incomeVMList.Sum(a => a.Balance) - expenseVMList.Sum(a => a.Balance);
                    foreach (var item in capitalAcctList)
                    {
                        item.SharePercentage = (float)(item.DepositedAmount / totalCapital * 100);
                        item.SharedAmount = (decimal)item.SharePercentage * netProfit / 100;
                        item.TotalProfit = item.SharedAmount;
                    }
                    #endregion
                }

                expenseVMList = expenseVMList.OrderByDescending(a => a.Balance).ToList();
                incomeVMList = incomeVMList.OrderByDescending(a => a.Balance).ToList();

                
                
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        
        decimal totalExpenses = 0;
        decimal totalIncome = 0;
        private void UpdateForm()
        {
            incAcctExpLVMBindingSource.List.Clear();
            incAcctIncLVMBindingSource.List.Clear();
            capAcctVMBindingSource.List.Clear();

            foreach (var item in expenseVMList)
            {
                incAcctExpLVMBindingSource.List.Add(item);
            }
            foreach (var item in incomeVMList)
            {
                incAcctIncLVMBindingSource.List.Add(item);
            }

            foreach (var item in capitalAcctList)
            {
                capAcctVMBindingSource.List.Add(item);
            }

            dgvExpenses.Refresh();
            dgvIncome.Refresh();
            capdgv.Refresh();

            totalExpenses = expenseVMList.Sum(a => a.Balance);
            totalIncome = incomeVMList.Sum(a => a.Balance);
            netProfit = totalIncome - totalExpenses;

            tbNetProfit.Text = netProfit.ToString("n2");
            tbTotalExpenses.Text = totalExpenses.ToString("n2");
            tbTotalIncome.Text = totalIncome.ToString("n2");

            

            UpdateDeduction();
        }

        private void LoadSettings()
        {
            try
            {
                using (Context db = new Context())
                {
                    if (appSett == null)
                    {
                        appSett = db.AppSettings.FirstOrDefault();
                    }
                }
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
                labelInfo.Text = string.Format("Showing data from ({0}) to ({1})", dtpFrom.Value.ToShortDateString(), dtpTo.Value.ToShortDateString());
                startDate = dtpFrom.Value.Date;
                endDate = dtpTo.Value.Date;

                WaitForm wait1 = new WaitForm(LoadData);
                wait1.ShowDialog();

                UpdateForm();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait1 = new WaitForm(LoadSettings);
                wait1.ShowDialog();
                startDate = null;
                endDate = null;
                labelInfo.Text = string.Format("Showing all information from ({0}) to ({1})", appSett.StartDate.ToShortDateString(), appSett.EndDate.ToShortDateString());
                WaitForm wait2 = new WaitForm(LoadData);
                wait2.ShowDialog();
                UpdateForm();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void capdgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                if (ri == capdgv.NewRowIndex || ri == -1)
                    return;

                DialogResult result = Gujjar.ConfirmYesNo("Are you sured to update deduction");
                if (result == DialogResult.No)
                    return;

                string acctId = capdgv.Rows[ri].Cells[0].Value.ToString();
                DeductionForm form = new DeductionForm();
                form.ShowDialog();

                if(form.PercentageValue != 0)
                {
                    var obj = capAcctVMBindingSource.List.OfType<CapAcctVM>()
                        .First(a => a.AcctId == acctId);
                    obj.DeductionRatio = form.PercentageValue;
                    obj.DeductionAmount = ((float)obj.SharedAmount * obj.DeductionRatio / 100);
                    obj.TotalProfit = obj.SharedAmount - (decimal)obj.DeductionAmount;
                    capdgv.Refresh();
                    UpdateDeduction();
                }

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnCustExpense_Click(object sender, EventArgs e)
        {
            try
            {
                IncomeAddAcctForm form = new IncomeAddAcctForm();
                form.ShowDialog();

                GenAcctLVM genAcct = form.AccountInfo;
                if(genAcct != null)
                {
                    IncAcctExpLVM vm = new IncAcctExpLVM
                    {
                        Account = genAcct.Account,
                        Balance = genAcct.Balance
                    };
                    expenseVMList.Add(vm);
                    UpdateForm();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnCustIncome_Click(object sender, EventArgs e)
        {
            try
            {
                IncomeAddAcctForm form = new IncomeAddAcctForm();
                form.ShowDialog();

                GenAcctLVM genAcct = form.AccountInfo;
                if (genAcct != null)
                {
                    IncAcctIncLVM vm = new IncAcctIncLVM
                    {
                        Account = genAcct.Account,
                        Balance = genAcct.Balance
                    };
                    incomeVMList.Add(vm);
                    UpdateForm();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dgvExpenses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;

                if (ri == -1 || ri == dgvExpenses.NewRowIndex)
                    return;

                if(dgvExpenses.Columns[expRemBtnDgv].Index == e.ColumnIndex)
                {
                    if (!Common.Model.Helper.ConfirmAdminPassword())
                        return;

                    DialogResult rest = Gujjar.ConfirmYesNo("Are you sured to remove account information?");
                    if (rest == DialogResult.No)
                        return;

                    string title = dgvExpenses.Rows[ri].Cells[0].Value.ToString();
                    decimal balance = dgvExpenses.Rows[ri].Cells[1].Value.ToString().ToDecimal();

                    var delObj = expenseVMList.FirstOrDefault(a => a.Account.Equals(title) && a.Balance == balance);
                    if(delObj != null)
                    {
                        expenseVMList.Remove(delObj);
                        UpdateForm();
                        Gujjar.InfoMsg("Account details are removed successfully");
                    }
                    else
                    {
                        Gujjar.ErrMsg("Account information is not found");
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);

            }
        }

        private void dgvIncome_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;

                if (ri == -1 || ri == dgvIncome.NewRowIndex)
                    return;

                if (dgvIncome.Columns[incRemBtnDgv].Index == e.ColumnIndex)
                {
                    if (!Helper.ConfirmAdminPassword())
                        return;

                    DialogResult rest = Gujjar.ConfirmYesNo("Are you sured to remove account information?");
                    if (rest == DialogResult.No)
                        return;
                    
                    string title = dgvIncome.Rows[ri].Cells[0].Value.ToString();
                    decimal balance = dgvIncome.Rows[ri].Cells[1].Value.ToString().ToDecimal();

                    var delObj = incomeVMList.FirstOrDefault(a => a.Account.Equals(title) && a.Balance == balance);
                    if (delObj != null)
                    {
                        incomeVMList.Remove(delObj);
                        UpdateForm();
                        Gujjar.InfoMsg("Account details are removed successfully");
                    }
                    else
                    {
                        Gujjar.ErrMsg("Account information is not found");
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);

            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                IncomeReprt incomeReport = new IncomeReprt();
                List<IncomeRepVM> data = new List<IncomeRepVM>
                {
                    new IncomeRepVM
                    {
                        Expenses = expenseVMList, Incomes = incomeVMList
                    }
                };

                
                List<FCompRVM> compDatasource = new List<FCompRVM>();
                using (Context db = new Context())
                {
                    var appSett = db.AppSettings.First();
                    FCompRVM compvm = new FCompRVM
                    {
                        Address = appSett.Address,
                        Barcode = null,
                        Logo = appSett.Logo,
                        Name = appSett.Name,
                        Phone = appSett.PhoneNo,
                        Qrcode = null,
                        RepDate = DateTime.Now.ToString(),
                        RepTitle = string.Format("Income statement from ({0}) to ({1})", dtpFrom.Value.ToShortDateString(), dtpTo.Value.ToShortDateString())
                    };
                    compDatasource.Add(compvm);
                }
                DetailReportBand compBand = incomeReport.Bands["DetailReport"] as DetailReportBand;
                compBand.DataSource = compDatasource;

                DetailReportBand dataBand = incomeReport.Bands["DetailReport1"] as DetailReportBand;

                DetailReportBand finBand = incomeReport.Bands["DetailReport2"] as DetailReportBand;
                finBand.DataSource = capAcctVMBindingSource.List.OfType<CapAcctVM>().ToList();

                expReport expReport = new expReport();
                expReport.DataSource = data.First().Expenses;
                incReport increport = new incReport();
                increport.DataSource = data.First().Incomes;

                var sub1 = dataBand.Report.FindControl("Detail2", true);
                var ctrl1 = sub1.Controls[1] as XRSubreport;
                var ctrl2 = sub1.Controls[0] as XRSubreport;
                ctrl1.ReportSource = expReport;
                ctrl2.ReportSource = increport;

                var grossProfit = incomeReport.Parameters["GrossProfit"];
                grossProfit.Value = tbNetProfit.Text;
                grossProfit.Visible = false;

                var deduct = incomeReport.Parameters["Deduction"];
                deduct.Value = tbDeduction.Text;
                deduct.Visible = false;

                var lengendsParam = incomeReport.Parameters["Legends"];
                lengendsParam.Visible = false;
                lengendsParam.Value = Helper.Legends();


                incomeReport.ShowPrintMarginsWarning = false;
                incomeReport.ShowPrintStatusDialog = false;
                incomeReport.ShowPreview();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
