using Khattana.Common;
using Model.Deal.Model;
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
using WinFom.Reports.XtraReports;
using WinFom.Reports.Model;
using Model.Admin.Model;
using Zen.Barcode;
using DevExpress.XtraReports.UI;
using WebCam_Capture;
using System.IO;
using Model.Retail.Model;
using WinFom.Common.Forms;
using System.Reflection;
using DevExpress.XtraEditors.Controls;

namespace WinFom.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        #region 
        private void button2_Click(object sender, EventArgs e)
        {
            decimal desi = GetDesi();
            double doub = GetDouble();

            double diff = (double)desi - doub;
            textBox1.Text = desi.ToString("n2");
            textBox2.Text = doub.ToString("n2");
            textBox3.Text = diff.ToString("n2");
        }

        private double GetDouble()
        {
            double bori = 6300;
            double perBori = 44;
            double totalKg = bori * perBori;
            double trade = 37.324;
            double manns = totalKg / trade;
            double perMann = 1350;
            double totalPrice = manns * perMann;

            return totalPrice;
        }

        private decimal GetDesi()
        {
            decimal bori = 6300;
            decimal perBori = 44;
            decimal totalKg = bori * perBori;
            decimal trade = 37.324m;
            decimal manns = totalKg / trade;
            decimal perMann = 1350;
            decimal totalPrice = manns * perMann;

            return totalPrice;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (Context db = new Context())
                {
                    var appDeal = db.AppDeals.FirstOrDefault();

                    if (appDeal != null)
                    {
                        MessageBox.Show(appDeal.Remarks);
                        MessageBox.Show("Data is added");

                        appDeal = new Model.Deal.Model.AppDeal();
                        appDeal.DealDate = DateTime.Now;
                        appDeal.BrokerId = 1;
                        appDeal.PackingUnitId = 1;
                        appDeal.TradeUnitId = 1;
                        appDeal.DealPackingId = 1;
                        appDeal.DealItemId = 1;
                        appDeal.CompanyId = 1;
                        appDeal.Remarks = "Remarks";
                        db.AppDeals.Add(appDeal);
                        db.SaveChanges();

                        AppDeal deal3 = new AppDeal();
                        deal3.BrokerId = 1;
                        deal3.IsCompleted = false;
                        deal3.CompanyId = 1;
                        deal3.DealDate = DateTime.Now;
                        deal3.DealItemId = 1;
                        //deal3.PackingQty = (decimal)Convert.ToDouble(tbPackingQty.Text);
                        deal3.PackingQty = 21;
                        deal3.FareAmount = 0;
                        deal3.DealPackingId = 1;
                        deal3.PackingUnitId = 1;
                        //deal3.PerPackingQty = (decimal)Convert.ToDouble(tbPerPackingQty.Text);
                        deal3.PerPackingQty = 2341;
                        deal3.Remarks = "xyz remarks";
                        deal3.AddedBy = "admin1";
                        //deal3.DealPrice = (decimal)Convert.ToDouble(tbDealPrice.Text);
                        deal3.DealPrice = 234234;
                        deal3.SchedulesCount = 3;
                        deal3.Tax = 0;
                        //deal3.TotalQty = (decimal)Convert.ToDouble(tbTotalQty.Text);
                        //deal3.TotalTradeUnits = (decimal)Convert.ToDouble(tbToralTradeUnits.Text);

                        deal3.TotalQty = 23;
                        deal3.TotalTradeUnits = 11;
                        deal3.TradeUnitId = 1;
                        deal3.UpdatedBy = "N/A";

                        db.AppDeals.Add(deal3);
                        db.SaveChanges();

                        MessageBox.Show(deal3.Remarks);
                    }
                    else
                    {
                        MessageBox.Show("Object is null");
                        appDeal = new Model.Deal.Model.AppDeal();
                        appDeal.DealDate = DateTime.Now;
                        appDeal.BrokerId = 1;
                        appDeal.PackingUnitId = 1;
                        appDeal.TradeUnitId = 1;
                        appDeal.DealPackingId = 1;
                        appDeal.DealItemId = 1;
                        appDeal.CompanyId = 1;
                        appDeal.Remarks = "Remarks";
                        db.AppDeals.Add(appDeal);
                        db.SaveChanges();

                        AppDeal deal3 = new AppDeal();
                        deal3.BrokerId = 1;
                        deal3.IsCompleted = false;
                        deal3.CompanyId = 1;
                        deal3.DealDate = DateTime.Now;
                        deal3.DealItemId = 1;
                        //deal3.PackingQty = (decimal)Convert.ToDouble(tbPackingQty.Text);
                        deal3.PackingQty = 21;
                        deal3.FareAmount = 0;
                        deal3.DealPackingId = 1;
                        deal3.PackingUnitId = 1;
                        //deal3.PerPackingQty = (decimal)Convert.ToDouble(tbPerPackingQty.Text);
                        deal3.PerPackingQty = 2341;
                        deal3.Remarks = "xyz remarks";
                        deal3.AddedBy = "admin1";
                        //deal3.DealPrice = (decimal)Convert.ToDouble(tbDealPrice.Text);
                        deal3.DealPrice = 234234;
                        deal3.SchedulesCount = 3;
                        deal3.Tax = 0;
                        //deal3.TotalQty = (decimal)Convert.ToDouble(tbTotalQty.Text);
                        //deal3.TotalTradeUnits = (decimal)Convert.ToDouble(tbToralTradeUnits.Text);

                        deal3.TotalQty = 23;
                        deal3.TotalTradeUnits = 11;
                        deal3.TradeUnitId = 1;
                        deal3.UpdatedBy = "N/A";

                        db.AppDeals.Add(deal3);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                AppSettings sett = null;
                List<DealSchedule> schedules = null;
                List<ScheduleRVM> scheduleRvms = new List<ScheduleRVM>();
                Random rnd = new Random();
                using (Context db = new Context())
                {
                    sett = db.AppSettings.First();
                    schedules = db.DealSchedules.ToList();
                    int tid = 101;
                    char ch = 'A';

                    int dateDay = 1;
                    foreach (var item in schedules)
                    {
                        int n = rnd.Next(6) + 1;
                        ScheduleRVM rvm = new ScheduleRVM
                        {
                            TransId = tid++,
                            Company = string.Format("Company {0}", ch++),
                            Date = "",
                            Price = "",
                            Qty = ""
                        };
                        if (rnd.Next(24) % 3 == 0)
                        {
                            dateDay++;
                        }
                        rvm.Date = string.Format("{0}/4/18", dateDay);
                        decimal qty = rnd.Next(800);
                        decimal price = qty * 1350;
                        rvm.Qty = qty.ToString("n4");
                        rvm.Price = price.ToString("n4");

                        scheduleRvms.Add(rvm);
                    }
                }

                FactoryRVM obj = new FactoryRVM
                {
                    Address = sett.Address,
                    Dated = DateTime.Now.ToString(),
                    LogoImg = sett.Logo,
                    Name = sett.Name,
                    Phone = "234234"
                };
                CodeQrBarcodeDraw qrObj = BarcodeDrawFactory.CodeQr;
                obj.QrImg = Gujjar.GetByteArrayFromImage(qrObj.Draw("App Developed By GBS Solutions, 0323-9372084", 50));

                MaalReport report = new MaalReport();
                DetailReportBand band1 = report.Bands["DetailReport"] as DetailReportBand;
                band1.DataSource = new List<FactoryRVM> { obj };

                DetailReportBand band2 = report.Bands["DetailReport1"] as DetailReportBand;
                band2.DataSource = scheduleRvms;

                report.ShowPrintMarginsWarning = false;
                report.ShowPrintStatusDialog = false;
                var param1 = report.Parameters["DevBy"];
                param1.Value = "Developed by: GBS Solutions 0323-9372084";
                param1.Visible = false;
                report.ShowRibbonPreview();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        #endregion
        private void button5_Click(object sender, EventArgs e)
        {
            WebCamCapture capture = new WebCamCapture();

        }

        private void button7_Click(object sender, EventArgs e)
        {

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 40; i++)
            {
                sb.AppendLine(Guid.NewGuid().ToString());
            }
            File.WriteAllText("C:\\Users\\Zarrar\\Desktop\\Notes\\guids.txt", sb.ToString());

            Gujjar.InfoMsg("40 Guids Done");
            Close();
        }
        private void DoOps()
        {
            using (Context db = new Context())
            {
                Random rnd = new Random();
                for (int i = 0; i < 9000; i++)
                {
                    Product p = new Product
                    {
                        Id = 0,
                        Account = null,
                        AlertOnSale = false,
                        ApplyLaborExpense = false,
                        GeneralAccountId = Properties.Resources.CurrentAssets,
                        Barcode = Guid.NewGuid().ToString(),
                        IsAvailable = true,
                        CostPrice = rnd.Next(10000),
                        DeductBardanaPacking = false,
                        IsDicountable = false,
                        ProductCategoryId = 1,
                        ProductDiscPercentage = 0,
                        ProductPoints = 0,
                        ProductCategory = null,
                        ProductNetUnitPriceWholeSale = rnd.Next(10000),
                        ProductSize = null,
                        ProductSizeId = 1,
                        ProductTotalUnitPrice = rnd.Next(1000),
                        SaleOrderLines = null,
                        SKU = rnd.Next(100)
                        ,
                        StockItem = null,
                        StockItemId = null,
                        Title = Guid.NewGuid().ToString(),
                        Weight = rnd.Next(100)
                    };
                    db.Products.Add(p);
                    if (i % 100 == 0)
                    {
                        db.SaveChanges();
                    }
                }
                db.SaveChanges();
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait = new WaitForm(DoOps);
                wait.ShowDialog();

                MessageBox.Show("Done");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                string path = @"F:\Docs\input.txt";
                string path2 = @"F:\Docs\out2.txt";
                string[] lines = File.ReadAllLines(path);
                StringBuilder sb = new StringBuilder();

                foreach (var item in lines)
                {
                    if (item.Length > 5)
                        sb.AppendLine(string.Format("{0} = true,", item));
                }

                File.WriteAllText(path2, sb.ToString());
                Gujjar.InfoMsg("done");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Gujjar.SendRequest("http://localhost:14350/Home/GetRepData", new { Name = "Shafiq Hussain", Age = 33, Profession = "Lecturer" });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Type[] allTypes = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type curType in allTypes)
                if (curType.BaseType == typeof(XtraReport))
                {
                    checkedListBoxControl1.Items.Add(curType.ToString());
                }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (checkedListBoxControl1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Select at least one report from the list");
                return;
            }
            try
            {
                //if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                foreach (CheckedListBoxItem item in checkedListBoxControl1.CheckedItems)
                {
                    Type reportType = Assembly.GetExecutingAssembly().GetType(item.Value.ToString());
                    XtraReport report = Activator.CreateInstance(reportType) as XtraReport;
                    //report.ExportToPdf(folderBrowserDialog1.SelectedPath + "\\" + reportType.Name + ".pdf");
                    MaalReport rep = new MaalReport();
                    rep.CreateDocument();
                    documentViewer1.DocumentSource = rep;
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }

        }
    }
}
