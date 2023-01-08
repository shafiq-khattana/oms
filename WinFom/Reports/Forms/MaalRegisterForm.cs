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
using WinFom.Common.Model;
using Model.Admin.Model;
using WinFom.Common.Forms;
using WinFom.Reports.Model;
using Zen.Barcode;
using WinFom.Reports.XtraReports;
using DevExpress.XtraReports.UI;

namespace WinFom.Reports.Forms
{
    public partial class MaalRegisterForm : Form
    {
        private List<DealSchedule> schedules = new List<DealSchedule>();
        private List<ScheduleRVM> scheduleVMList = new List<ScheduleRVM>();
        private AppSettings sett = null;
        private int month = 0;
        private int year = 0;
        private string appDated = "";
        public MaalRegisterForm()
        {
            InitializeComponent();
        }
        private void LoadSettings()
        {
            try
            {
                using (Context db = new Context())
                {
                    sett = db.AppSettings.FirstOrDefault();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
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
                WaitForm wait1 = new WaitForm(LoadSettings);
                wait1.ShowDialog();

                cbMonths.DataSource = Enum.GetNames(typeof(GBSMonths));
                List<int> years = new List<int>();
                int year = DateTime.Now.Year;
                for (int i = year; i >= year - 10; i--)
                {
                    years.Add(i);
                }

                cbYears.DataSource = years;
                cbYears.SelectedItem = cbYears.Items.OfType<int>().FirstOrDefault(a => a == year);
                GBSMonths month = (GBSMonths)DateTime.Now.Month;
                cbMonths.SelectedItem = cbMonths.Items.OfType<GBSMonths>().FirstOrDefault(a => a == month);
                cbMonths.Text = month.ToString();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void LoadSchedule1()
        {
            try
            {
                scheduleVMList.Clear();
                using (Context db = new Context())
                {
                    schedules = db.DealSchedules.AsParallel().ToList().Where(a => { if (a.ArrivalDate == null) { return false; } var date = a.ArrivalDate.Value; return a.IsArrived && (date.Month == month && date.Year == year); }).ToList();

                    foreach (var item in schedules)
                    {
                        var deal = db.AppDeals.Find(item.AppDealId);
                        var comp = db.Companies.Find(deal.CompanyId);
                        ScheduleRVM vm = new ScheduleRVM
                        {
                            Company = string.Format("{0} ({1})", comp.Name, comp.Address),
                            Date = item.ArrivalDate.Value.ToShortDateString(),
                            Price = item.ReceivedPrice.ToString("n2"),
                            Qty = item.ReceivedSubTradeUnits.ToString("n2"),
                            TransId = item.TransId
                        };

                        scheduleVMList.Add(vm);
                    }
                }


            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void LoadSchedule2()
        {
            try
            {
                scheduleVMList.Clear();
                using (Context db = new Context())
                {
                    schedules = db.DealSchedules.AsParallel().ToList()
                        .Where(a => { if (a.ArrivalDate == null) { return false; }
                            var date = a.ArrivalDate.Value.Date; return a.IsArrived && (date >= dtFrom && date <= dtTo); }).ToList();

                    foreach (var item in schedules)
                    {
                        var deal = db.AppDeals.Find(item.AppDealId);
                        var comp = db.Companies.Find(deal.CompanyId);
                        ScheduleRVM vm = new ScheduleRVM
                        {
                            Company = string.Format("{0} ({1})", comp.Name, comp.Address),
                            Date = item.ArrivalDate.Value.ToShortDateString(),
                            Price = item.ReceivedPrice.ToString("n4"),
                            Qty = item.ReceivedSubTradeUnits.ToString("n4"),
                            TransId = item.TransId
                        };

                        scheduleVMList.Add(vm);
                    }
                }


            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnView1_Click(object sender, EventArgs e)
        {
            try
            {
                month = (int)(GBSMonths)Enum.Parse(typeof(GBSMonths), cbMonths.Text);
                year = cbYears.Text.ToInt();
                appDated = string.Format("{0}, {1}", (GBSMonths)month, year);
                WaitForm wait = new WaitForm(LoadSchedule1);
                wait.ShowDialog();
                if (scheduleVMList.Count == 0)
                {
                    throw new Exception(string.Format("There is no arrived schedule in {0}", appDated));
                }
                ShowReport();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void ShowReport()
        {
            try
            {
                FactoryRVM obj = new FactoryRVM
                {
                    Address = sett.Address,
                    Dated = appDated,
                    LogoImg = sett.Logo,
                    Name = sett.Name,
                    Phone = sett.PhoneNo
                };

                CodeQrBarcodeDraw qrObj = BarcodeDrawFactory.CodeQr;
                obj.QrImg = Gujjar.GetByteArrayFromImage(qrObj.Draw("App Developed By GBS Solutions, 0323-9372084", 50));

                MaalReport report = new MaalReport();
                DetailReportBand band1 = report.Bands["DetailReport"] as DetailReportBand;
                band1.DataSource = new List<FactoryRVM> { obj };

                DetailReportBand band2 = report.Bands["DetailReport1"] as DetailReportBand;
                band2.DataSource = scheduleVMList;

                report.ShowPrintMarginsWarning = false;
                report.ShowPrintStatusDialog = false;
                var param1 = report.Parameters["DevBy"];
                param1.Value = Helper.Legends();
                param1.Visible = false;
                report.ShowRibbonPreview();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        DateTime dtFrom = DateTime.Now;
        DateTime dtTo = DateTime.Now;
        private void btnView2_Click(object sender, EventArgs e)
        {
            try
            {
                dtFrom = dtpFrom.Value.Date;
                dtTo = dtpTo.Value.Date;
                appDated = string.Format("From: {0}, To: {1}", dtFrom.ToShortDateString(), dtTo.ToShortDateString());

                WaitForm wait = new WaitForm(LoadSchedule2);
                wait.ShowDialog();
                if (scheduleVMList.Count == 0)
                {
                    throw new Exception(string.Format("There is no arrived schedule in {0}", appDated));
                }
                ShowReport();

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
