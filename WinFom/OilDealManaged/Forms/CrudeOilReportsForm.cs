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
using WinFom.ReadyStuff.ReportViewModel;
using Model.Admin.Model;
using WinFom.Common.Model;
using Zen.Barcode;
using WinFom.Common.Forms;
using WinFom.ReadyStuff.Report;
using DevExpress.XtraReports.UI;
using Model.ReadyStuff.Model;

namespace WinFom.OilDealManaged.Forms
{
    public partial class CrudeOilReportsForm : Form
    {
        private AppSettings appSett = Helper.AppSet;
        public CrudeOilReportsForm()
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

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        
        
        
        #region "Broker trial balance report"
        List<OilDealBroker> brokers = null;
        ReadyBrokerTrailBalanceReport trialRep = null;
        List<BrokerTrialRVM> dataDatasource = null;
        private void LoadTrialData()
        {
            trialRep = new ReadyBrokerTrailBalanceReport();
            dataDatasource = new List<BrokerTrialRVM>();
            using (Context db = new Context())
            {
                brokers = db.OilDealBrokers.Where(a => !a.Name.Equals("N/A")).OrderBy(a => a.Name).ToList();
                foreach (var item in brokers)
                {
                    decimal balance = 0;
                    var trans = db.AccountTransactions.Where(a => a.GeneralAccountId == item.GeneralAccountId)
                        .OrderByDescending(a => a.Id).FirstOrDefault();
                    if (trans != null)
                    {
                        balance = trans.Balance;
                    }

                    BrokerTrialRVM trialvm = new BrokerTrialRVM
                    {
                        Balance = balance,
                        Broker = item.Name,
                        CellNo = item.Contact
                    };

                    dataDatasource.Add(trialvm);
                }
            }
        }
        private void btnTrialBalance_Click(object sender, EventArgs e)
        {
            try
            {
                List<RCompRVM> compDatasource = new List<RCompRVM>();
                WaitForm wait = new WaitForm(LoadTrialData);
                wait.ShowDialog();
                if (dataDatasource.Count == 0)
                {
                    throw new Exception("There is no broker's data to show in report");
                }
                RCompRVM compvm = new RCompRVM
                {
                    Address = appSett.Address,
                    Barcode = null,
                    Logo = appSett.Logo,
                    Name = appSett.Name,
                    Phone = "Phone No. " + appSett.PhoneNo,
                    Qrcode = null,
                    RepDate = string.Format("Report date time {0}", DateTime.Now.ToString()),
                    RepTitle = "Crude Oil brokers trial balances"
                };
                compDatasource.Add(compvm);

                DetailReportBand compBand = trialRep.Bands["DetailReport"] as DetailReportBand;
                DetailReportBand dataBand = trialRep.Bands["DetailReport1"] as DetailReportBand;

                compBand.DataSource = compDatasource;
                dataBand.DataSource = dataDatasource;

                var tbal = trialRep.Parameters["TotalBalance"];
                tbal.Value = dataDatasource.Sum(a => a.Balance).ToString("n1");
                tbal.Visible = false;
                var lengendsParam = trialRep.Parameters["Legends"];
                lengendsParam.Visible = false;
                lengendsParam.Value = Helper.Legends();
                trialRep.ShowPrintMarginsWarning = false;
                trialRep.ShowPrintStatusDialog = false;

                trialRep.ShowPreview();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        #endregion
    }
}
