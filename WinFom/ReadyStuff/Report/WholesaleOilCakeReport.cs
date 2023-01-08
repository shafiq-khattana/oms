using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;

namespace WinFom.ReadyStuff.Report
{
    public partial class WholesaleOilCakeReport : XtraReport
    {
        public WholesaleOilCakeReport()
        {
            InitializeComponent();
        }

        private void DetailReport3_BeforePrint(object sender, PrintEventArgs e)
        {
            DetailReportBand detailReport = sender as DetailReportBand;
            detailReport.ReportPrintOptions.PrintOnEmptyDataSource = false;
            //e.Cancel = true;
           
        }

        private void DetailReport1_BeforePrint(object sender, PrintEventArgs e)
        {
            DetailReportBand detailReport = sender as DetailReportBand;
            detailReport.ReportPrintOptions.PrintOnEmptyDataSource = false;
            //e.Cancel = true;
        }
    }
}
