using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace WinFom.Financials.Reports.XtraReports
{
    public partial class AccountTransactionsReport : DevExpress.XtraReports.UI.XtraReport
    {
        public AccountTransactionsReport()
        {
            InitializeComponent();
        }

        private void DetailReport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DetailReportBand detailReport = sender as DetailReportBand;
            detailReport.ReportPrintOptions.PrintOnEmptyDataSource = false;
        }
    }
}
