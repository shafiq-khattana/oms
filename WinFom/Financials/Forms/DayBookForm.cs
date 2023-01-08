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
using WinFom.Common.Forms;
using Model.Financials.ViewModel;
using WinFom.Financials.Reports.Model;
using Model.Admin.Model;
using WinFom.Common.Model;
using WinFom.Financials.Reports.XtraReports;
using DevExpress.XtraReports.UI;
using Zen.Barcode;

namespace WinFom.ReadyStuff.Forms
{
    public partial class DayBookForm : Form
    {
        private List<DayBook> dayBookEntries = null;
        DateTime inqueryDate = DateTime.Now.Date;
        private bool IsDated = false;
        DateTime dtFrom = DateTime.Now.Date;
        DateTime dtTo = DateTime.Now.Date;
        private string btndgvdes = "btndgvdes1111";
        private string datedStr = "N/A";
        private string dgvbtnreverse = "dgvbtnreverse13111";
        UserOps useOps = SingleTon.LoginForm.UserOptions;
        private List<GeneralAccount> accountList = null;
        public DayBookForm()
        {
            InitializeComponent();
        }
        private void LoadAccountList()
        {
            try
            {
                using (Context db = new Context())
                {
                    if(accountList != null)
                    {
                        accountList.Clear();
                        accountList = null;
                    }
                    accountList = db.Accounts.OfType<GeneralAccount>().OrderBy(a => a.Title).ToList();
                    accountList.Add(new GeneralAccount
                    {
                        Title = "All Accounts",
                        Id = "zero"
                    });
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
                
            }
        }
        private void LoadEntries()
        {
            if (dayBookEntries != null)
            {
                dayBookEntries.Clear();
                dayBookEntries = null;
            }
            using (Context db = new Context())
            {
                if (IsDated)
                {
                    dayBookEntries = db.DayBooks.Where(a => a.InDate >= dtFrom && a.InDate <= dtTo).ToList();

                }
                else
                    dayBookEntries = db.DayBooks.Where(a => a.InDate == inqueryDate).ToList();
            }
        }
        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                inqueryDate = dtpTo.Value.Date;
                IsDated = false;
                WaitForm wait1 = new WaitForm(LoadEntries);
                wait1.ShowDialog();

                BindEnteries(dayBookEntries);
                WaitForm wait2 = new WaitForm(LoadAccountList);
                wait2.ShowDialog();
                BindAccountList();
                Gujjar.AddDatagridviewButton(dgv, btndgvdes, "Desc", "Desc", 70);
                if (useOps.CanDoReverseFinancialEntries)
                    Gujjar.AddDatagridviewButton(dgv, dgvbtnreverse, "Reverse", "Reverse", 80);
                datedStr = string.Format("Dated: ({0})", DateTime.Now.ToString());
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindAccountList()
        {
            cbAccountList.DataSource = accountList;
            cbAccountList.DisplayMember = "Title";
            cbAccountList.ValueMember = "Id";
            cbAccountList.SelectedItem = cbAccountList.Items.OfType<GeneralAccount>()
                .FirstOrDefault(a => a.Id == "zero");
        }
        int dgvIndex = 0;
        private void BindEnteries(List<DayBook> entries)
        {
            tbEntriesCount.Text = entries.Count.ToString();
            try
            {
                dayBookVMBindingSource.List.Clear();
                dgvIndex = 0;
                foreach (var item in entries.OrderByDescending(a => a.Id))
                {
                    DayBookVM vm = new DayBookVM
                    {
                        CreditTrace = item.CreditTrace,
                        DebitTrace = item.DebitTrace,
                        Description = item.Description,
                        Id = item.Id,
                        Date = item.Date.ToString(),
                        Amount = item.Amount.ToString("n2"),
                        RB = item.CanRollBack,
                        IsReversed = item.IsReversed
                    };
                    dayBookVMBindingSource.List.Add(vm);
                    if (vm.RB)
                    {
                        var row = dgv.Rows[dgvIndex];
                        row.DefaultCellStyle.BackColor = Color.PaleGreen;
                    }
                    if (vm.IsReversed)
                    {
                        var row = dgv.Rows[dgvIndex];
                        row.DefaultCellStyle.BackColor = Color.Coral;
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                    dgvIndex++;
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
                tbDaybookEntry.Clear();
                IsDated = true;
                dtFrom = dtpFrom.Value.Date;
                dtTo = dtpTo.Value.Date;
                datedStr = string.Format("From date ({0}) to ({1})", dtFrom.ToShortDateString(), dtTo.ToShortDateString());
                WaitForm wait1 = new WaitForm(LoadEntries);
                wait1.ShowDialog();
                BindEnteries(dayBookEntries);
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
                tbDaybookEntry.Clear();
                IsDated = false;
                WaitForm wait1 = new WaitForm(LoadEntries);
                wait1.ShowDialog();

                BindEnteries(dayBookEntries);
                datedStr = string.Format("Dated: ({0})", DateTime.Now.ToString());
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                if (ri == -1 || ri == dgv.NewRowIndex)
                {
                    return;
                }
                if (dgv.Columns[btndgvdes].Index == e.ColumnIndex)
                {
                    string msg = dgv.Rows[ri].Cells[2].Value.ToString();
                    Gujjar.InfoMsg(msg);
                }
                if (useOps.CanDoReverseFinancialEntries)
                    if (dgv.Columns[dgvbtnreverse].Index == e.ColumnIndex)
                    {
                        int id = dgv.Rows[ri].Cells[0].Value.ToInt();
                        var daybookVM = dayBookVMBindingSource.List.OfType<DayBookVM>()
                            .FirstOrDefault(a => a.Id == id);

                        if (!daybookVM.RB)
                        {
                            throw new Exception("This transaction is system generated, it can't be roll backed.");
                        }
                        if (daybookVM.IsReversed)
                        {
                            throw new Exception("The transaction already has been rolled back.");
                        }
                        if (!Helper.ConfirmAdminPassword())
                        {
                            return;
                        }
                        DialogResult res = Gujjar.ConfirmYesNo("Please confirm. Are you sured to reverse this transaction?");
                        if (res == DialogResult.No)
                            return;

                        DayBook daybook = null;
                        using (Context db = new Context())
                        {
                            daybook = db.DayBooks.Find(daybookVM.Id);
                        }
                        if (Helper.ReverseTransaction(daybook))
                        {
                            Gujjar.InfoMsg("Transaction has been reversed successfully");

                            inqueryDate = dtpTo.Value.Date;
                            IsDated = false;
                            WaitForm wait1 = new WaitForm(LoadEntries);
                            wait1.ShowDialog();

                            BindEnteries(dayBookEntries);
                            datedStr = string.Format("Dated: ({0})", DateTime.Now.ToString());
                        }
                    }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        List<FincReportHeader> fincReportHeaderList = null;
        List<DayBookRepModel> daybookRepModelList = null;

        private void LoadReportData()
        {
            try
            {
                var items = dayBookVMBindingSource.List.OfType<DayBookVM>().ToList();
                AppSettings AppSett = Helper.AppSet;
                FincReportHeader header = new FincReportHeader
                {
                    FactoryAddress = AppSett.Address,
                    Barcode = null,
                    Dated = datedStr,
                    FactoryLogo = AppSett.Logo,
                    FactoryName = AppSett.Name,
                    FactoryPhone = AppSett.PhoneNo,
                    QrCode = null,
                    ReportTitle = "Day Book Entries",
                    Entries = items.Count
                };

                Code128BarcodeDraw bcode = BarcodeDrawFactory.Code128WithChecksum;
                Image img = bcode.Draw(Helper.Unum, 50);
                byte[] imgBytes = Gujjar.GetByteArrayFromImage(img);
                header.Barcode = imgBytes;
                CodeQrBarcodeDraw qrCode = BarcodeDrawFactory.CodeQr;
                Image img2 = qrCode.Draw("GBS Solutions, Burewala. 0323-9372084", 50);
                byte[] qrBytes = Gujjar.GetByteArrayFromImage(img2);
                header.QrCode = qrBytes;

                if (fincReportHeaderList != null && fincReportHeaderList.Count > 0)
                {
                    fincReportHeaderList.Clear();
                    fincReportHeaderList = null;
                }
                fincReportHeaderList = new List<FincReportHeader> { header };



                if (daybookRepModelList != null && daybookRepModelList.Count > 0)
                {
                    daybookRepModelList.Clear();
                    daybookRepModelList = null;
                }
                daybookRepModelList = new List<DayBookRepModel>();
                foreach (var item in items)
                {
                    DayBookRepModel mod = new DayBookRepModel
                    {
                        Amount = item.Amount.ToDecimal(),
                        CreditTrace = item.CreditTrace,
                        DebitTrace = item.DebitTrace,
                        Description = item.Description,
                        Id = item.Id
                    };
                    daybookRepModelList.Add(mod);
                }

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void btnGetReport_Click(object sender, EventArgs e)
        {
            try
            {
                tbDaybookEntry.Clear();
                var items = dayBookVMBindingSource.List.OfType<DayBookVM>().ToList();
                if (items.Count == 0)
                {
                    throw new Exception("There is no day book entry to display in report");
                }

                WaitForm wait = new WaitForm(LoadReportData);
                wait.ShowDialog();

                ReportDayBook rep = new ReportDayBook();




                DetailReportBand hBand = rep.Bands["DetailReport"] as DetailReportBand;
                DetailReportBand cBand = rep.Bands["DetailReport1"] as DetailReportBand;

                hBand.DataSource = fincReportHeaderList;
                cBand.DataSource = daybookRepModelList;

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void cbAccountList_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbDaybookEntry.Clear();
            if (cbAccountList.SelectedIndex == -1)
                return;
            if (dayBookEntries == null || dayBookEntries.Count == 0)
                return;
            
            GeneralAccount account = cbAccountList.SelectedItem as GeneralAccount;
            if(account.Id == "zero")
            {
                BindEnteries(dayBookEntries);
            }
            else
            {
                var dbe = dayBookEntries.Where(a => a.DebitAccountId == account.Id || a.CreditAccountId == account.Id)
                    .OrderByDescending(a => a.Id).ToList();
                BindEnteries(dbe);
            }
        }

        private void btnSearchDbEntry_Click(object sender, EventArgs e)
        {
            try
            {
                string term = tbDaybookEntry.Text;
                if(string.IsNullOrEmpty(term))
                {
                    throw new Exception("Enter daybook entry id");
                }
                int dbid = term.ToInt();
                using (Context db = new Context())
                {
                    var dbent = db.DayBooks.FirstOrDefault(a => a.Id == dbid);
                    if(dbent == null)
                    {
                        throw new Exception(string.Format("There is no daybook entry with this ID {0} in database.", dbid));
                    }
                    else
                    {
                        List<DayBook> dbEntries = new List<DayBook>();
                        dbEntries.Add(dbent);
                        BindEnteries(dbEntries);
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
