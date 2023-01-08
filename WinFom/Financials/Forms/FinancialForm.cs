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
using DevExpress.XtraEditors;
using WinFom.ReadyStuff.Forms;
using WinFom.Financials.Reports.Model;
using Model.Admin.Model;
using Zen.Barcode;
using WinFom.Common.Model;
using Model.Financials.Model;
using WinFom.Common.Forms;
using DevExpress.XtraReports.UI;
using WinFom.Financials.Reports.XtraReports;

namespace WinFom.Financials.Forms
{
    public partial class FinancialForm : Form
    {
        public FinancialForm()
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
                Helper.IsOkApplied();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnDayBook_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                DayBookForm form = new DayBookForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnTransactions_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                TransactionForms form = new TransactionForms();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
        {

        }

        private void btnAccountHeadBalance_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                AccountHeadsBalanceForm form = new AccountHeadsBalanceForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAccountDetailsTreeView_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                AccountsDetailsTreeViewForm form = new AccountsDetailsTreeViewForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAccounts_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                HeadAccounts form = new HeadAccounts();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnCapitalAccount_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                CapitalAccountsForm form = new CapitalAccountsForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnCashAccount_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                AccountTransactionForm form = new AccountTransactionForm(Properties.Resources.CashInHand);
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnBanks_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                BankListForm form = new BankListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnFinancialExpense_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                FinancialExpenseForm form = new FinancialExpenseForm();
                form.ShowDialog();

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnGeneralExpense_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                GeneralExpenseForm form = new GeneralExpenseForm();
                form.ShowDialog();

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnCreditors_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                CreditorsAccountsForm form = new CreditorsAccountsForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tileItem10_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                DebitorsAccountsForm form = new DebitorsAccountsForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnOpeningBalance_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                OpeningBalanceAccountForm form = new OpeningBalanceAccountForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnLaborPayableAccounts_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                LaborPayableAccountsForm form = new LaborPayableAccountsForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAccruedExpenses_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                AccruedExpensesForm form = new AccruedExpensesForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnEmployeePayments_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                EmployeesAccountsForm form = new EmployeesAccountsForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnLongTermAssets_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                LongTermAssetsItemsForm form = new LongTermAssetsItemsForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnZakatAccount_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                ZakatAccountForm form = new ZakatAccountForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        List<FCompRVM> fcompRvmList = null;
        List<FHeadRVM> fHeadRvmList = null;
        TrailBalanceReport1 rep = null;
        List<FSummaryRVM> fsummaryList = null;
        private void btnTrailBalances1_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                WaitForm wait = new WaitForm(LoadData);
                wait.ShowDialog();


                DetailReportBand band1 = rep.Bands["DetailReport"] as DetailReportBand;
                DetailReportBand band2 = rep.Bands["DetailReport1"] as DetailReportBand;
                DetailReportBand band3 = rep.Bands["DetailReport2"] as DetailReportBand;

                band1.DataSource = fcompRvmList;
                band2.DataSource = fHeadRvmList;
                band3.DataSource = fsummaryList;

                var lengendsParam = rep.Parameters["Legends"];
                lengendsParam.Visible = false;
                lengendsParam.Value = Helper.Legends();

                rep.ShowPrintMarginsWarning = false;
                rep.ShowPrintStatusDialog = false;
                rep.ShowRibbonPreview();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadData()
        {
            try
            {
                if (fcompRvmList != null)
                {
                    fcompRvmList.Clear();
                    fcompRvmList = null;
                }
                if (fHeadRvmList != null)
                {
                    fHeadRvmList.Clear();
                    fHeadRvmList = null;
                }
                if (rep != null)
                {
                    rep.Dispose();
                    rep = null;
                }
                rep = new TrailBalanceReport1();
                if (fsummaryList != null)
                {
                    fsummaryList.Clear();
                    fsummaryList = null;
                }
                fsummaryList = new List<FSummaryRVM>();
                fcompRvmList = new List<FCompRVM>();
                fHeadRvmList = new List<FHeadRVM>();

                using (Context db = new Context())
                {
                    AppSettings appSett = db.AppSettings.First();
                    FCompRVM fcompVM = new FCompRVM
                    {
                        Address = appSett.Address,
                        Barcode = null,
                        Logo = appSett.Logo,
                        Name = appSett.Name,
                        Phone = appSett.PhoneNo,
                        Qrcode = null,
                        RepDate = string.Format("Dated: {0}", DateTime.Now.ToString()),
                        RepTitle = "Account Balances Report"
                    };
                    Code128BarcodeDraw bcode = BarcodeDrawFactory.Code128WithChecksum;
                    Image img = bcode.Draw(Helper.Unum, 50);
                    byte[] imgBytes = Gujjar.GetByteArrayFromImage(img);
                    fcompVM.Barcode = imgBytes;

                    CodeQrBarcodeDraw qrCode = BarcodeDrawFactory.CodeQr;
                    Image img2 = qrCode.Draw("GBS Solutions, Burewala. 0323-9372084", 50);
                    byte[] qrBytes = Gujjar.GetByteArrayFromImage(img2);
                    fcompVM.Qrcode = qrBytes;

                    fcompRvmList.Add(fcompVM);

                    FHeadRVM assets = new FHeadRVM();
                    assets.HeadTitle = "Assets Head";
                    assets.Accounts = new List<FAccountRVM>();
                    var topHeads = db.Accounts.OfType<TopHeadAccount>()
                        .Where(a => a.HeadAccountId == Properties.Resources.AssetsHead).ToList();
                    foreach (var topHead in topHeads)
                    {
                        var subHeads = db.Accounts.OfType<SubHeadAccount>()
                            .Where(a => a.TopHeadAccountId == topHead.Id).ToList();

                        foreach (var subHead in subHeads)
                        {
                            var genAccts = db.Accounts.OfType<GeneralAccount>()
                                .Where(a => a.SubHeadAccountId == subHead.Id).OrderBy(a => a.Title).ToList();
                            foreach (var item in genAccts)
                            {
                                item.AccountTransactions = db.AccountTransactions.Where(a => a.GeneralAccountId == item.Id).AsParallel().ToList()
                                    .Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date).ToList();
                                assets.AccountCount++;
                                item.Balance = 0;
                                if (item.AccountTransactions.Count > 0)
                                {
                                    item.Balance = item.AccountTransactions.Last().Balance;
                                }
                                assets.Balance += item.Balance;
                                assets.TransCount += item.AccountTransactions.Count;

                                FAccountRVM factrvm = new FAccountRVM
                                {
                                    Balance = item.Balance,
                                    TransCount = item.AccountTransactions.Count,
                                    Title = item.Title
                                };
                                if(factrvm.Balance != 0)
                                    assets.Accounts.Add(factrvm);
                            }
                        }
                    }
                    fHeadRvmList.Add(assets);

                    #region Liability
                    FHeadRVM liability = new FHeadRVM();
                    liability.HeadTitle = "Liabilities Head";
                    liability.Accounts = new List<FAccountRVM>();
                    var topHeadsLibs = db.Accounts.OfType<TopHeadAccount>()
                        .Where(a => a.HeadAccountId == Properties.Resources.LiabilitiesHead).ToList();
                    foreach (var topHead in topHeadsLibs)
                    {
                        var subHeads = db.Accounts.OfType<SubHeadAccount>()
                            .Where(a => a.TopHeadAccountId == topHead.Id).ToList();

                        foreach (var subHead in subHeads)
                        {
                            var genAccts = db.Accounts.OfType<GeneralAccount>()
                                .Where(a => a.SubHeadAccountId == subHead.Id).OrderBy(a => a.Title).ToList();
                            foreach (var item in genAccts)
                            {
                                item.AccountTransactions = db.AccountTransactions.Where(a => a.GeneralAccountId == item.Id).AsParallel().ToList()
                                   .Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date).ToList();
                                liability.AccountCount++;
                                item.Balance = 0;
                                if (item.AccountTransactions.Count > 0)
                                {
                                    item.Balance = item.AccountTransactions.Last().Balance;
                                }
                                liability.Balance += item.Balance;
                                liability.TransCount += item.AccountTransactions.Count;

                                FAccountRVM factrvm = new FAccountRVM
                                {
                                    Balance = item.Balance,
                                    TransCount = item.AccountTransactions.Count,
                                    Title = item.Title
                                };
                                if (factrvm.Balance != 0)
                                    liability.Accounts.Add(factrvm);
                            }
                        }
                    }
                    fHeadRvmList.Add(liability);
                    #endregion

                    #region Expenses
                    FHeadRVM expense = new FHeadRVM();
                    expense.HeadTitle = "Expenses Head";
                    expense.Accounts = new List<FAccountRVM>();
                    var topHeadExps = db.Accounts.OfType<TopHeadAccount>()
                        .Where(a => a.HeadAccountId == Properties.Resources.ExpensesHead).ToList();
                    foreach (var topHead in topHeadExps)
                    {
                        var subHeads = db.Accounts.OfType<SubHeadAccount>()
                            .Where(a => a.TopHeadAccountId == topHead.Id).ToList();

                        foreach (var subHead in subHeads)
                        {
                            var genAccts = db.Accounts.OfType<GeneralAccount>()
                                .Where(a => a.SubHeadAccountId == subHead.Id).OrderBy(a => a.Title).ToList();
                            foreach (var item in genAccts)
                            {
                                item.AccountTransactions = db.AccountTransactions.Where(a => a.GeneralAccountId == item.Id).AsParallel().ToList()
                                   .Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date).ToList();
                                expense.AccountCount++;
                                item.Balance = 0;
                                if (item.AccountTransactions.Count > 0)
                                {
                                    item.Balance = item.AccountTransactions.Last().Balance;
                                }
                                expense.Balance += item.Balance;
                                expense.TransCount += item.AccountTransactions.Count;

                                FAccountRVM factrvm = new FAccountRVM
                                {
                                    Balance = item.Balance,
                                    TransCount = item.AccountTransactions.Count,
                                    Title = item.Title
                                };
                                if (factrvm.Balance != 0)
                                    expense.Accounts.Add(factrvm);
                            }
                        }
                    }
                    fHeadRvmList.Add(expense);
                    #endregion

                    #region Income
                    FHeadRVM income = new FHeadRVM();
                    income.HeadTitle = "Income Head";
                    income.Accounts = new List<FAccountRVM>();
                    var topHeadIncome = db.Accounts.OfType<TopHeadAccount>()
                        .Where(a => a.HeadAccountId == Properties.Resources.IncomeHead).ToList();
                    foreach (var topHead in topHeadIncome)
                    {
                        var subHeads = db.Accounts.OfType<SubHeadAccount>()
                            .Where(a => a.TopHeadAccountId == topHead.Id).ToList();

                        foreach (var subHead in subHeads)
                        {
                            var genAccts = db.Accounts.OfType<GeneralAccount>()
                                .Where(a => a.SubHeadAccountId == subHead.Id).OrderBy(a => a.Title).ToList();
                            foreach (var item in genAccts)
                            {
                                item.AccountTransactions = db.AccountTransactions.Where(a => a.GeneralAccountId == item.Id).AsParallel().ToList()
                                   .Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date).ToList();
                                income.AccountCount++;
                                item.Balance = 0;
                                if (item.AccountTransactions.Count > 0)
                                {
                                    item.Balance = item.AccountTransactions.Last().Balance;
                                }
                                income.Balance += item.Balance;
                                income.TransCount += item.AccountTransactions.Count;

                                FAccountRVM factrvm = new FAccountRVM
                                {
                                    Balance = item.Balance,
                                    TransCount = item.AccountTransactions.Count,
                                    Title = item.Title
                                };
                                if (factrvm.Balance != 0)
                                    income.Accounts.Add(factrvm);
                            }
                        }
                    }
                    fHeadRvmList.Add(income);
                    #endregion
                    fHeadRvmList.ForEach((a) =>
                    {
                        a.Accounts = a.Accounts.OrderBy(n => n.Title).ToList();
                    });


                    FSummaryRVM fsvm = new FSummaryRVM
                    {
                        TotalAccount = fHeadRvmList.Sum(a => a.AccountCount),
                        TotalAssets = assets.Balance,
                        TotalExpenses = expense.Balance,
                        TotalIncome = income.Balance,
                        TotalLiability = liability.Balance,
                        TotalTrans = fHeadRvmList.Sum(a => a.TransCount),
                        FBarchRVMList = new List<FBarchRVM>
                        {
                            new FBarchRVM
                            {
                                Balance = assets.Balance,
                                Title = "Assets"
                            },
                            new FBarchRVM
                            {
                                Balance = liability.Balance,
                                Title = "Liabilities"
                            },
                            new FBarchRVM
                            {
                                Balance = expense.Balance,
                                Title = "Expenses"
                            },
                            new FBarchRVM
                            {
                                Balance = income.Balance,
                                Title = "Income"
                            }
                        }
                    };
                    fsummaryList.Add(fsvm);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnGeneralTransaction_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                JVForm form = new JVForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAccountsList_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                AccountsListForm form = new AccountsListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnIncomeStatement_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                IncomeStatementForm form = new IncomeStatementForm();
                form.ShowDialog();
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
    }
}
