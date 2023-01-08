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
using WinFom.Deal.Forms;
using WinFom.Common.Forms;
using WinFom.Common.Model;
using WinFom.AppGoodCompany.Reports.Model;
using WinFom.Reports.Model;
using Model.Admin.Model;
using Zen.Barcode;
using WinFom.AppGoodCompany.Reports.Report;
using DevExpress.XtraReports.UI;

namespace WinFom.AppGoodCompany.Forms
{
    public partial class IssuePackingCompanyForm : Form
    {
        private List<DealPacking> dealPackings = new List<DealPacking>();
        private int gCompId = 0;
        public bool IsDone = false;
        private GoodCompany goodCompany = null;
        private AppSettings sett = null;
        private IssuePackingRVM issuePackingRVM = null;
        private string unum = "";
        public IssuePackingCompanyForm(int goodCompanyId)
        {
            InitializeComponent();
            gCompId = goodCompanyId;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadGoodCompany()
        {
            try
            {
                using (Context db = new Context())
                {
                    goodCompany = db.GoodCompanies.Find(gCompId);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void LoadSettings()
        {
            try
            {
                using (Context db = new Context())
                {
                    sett = db.AppSettings.First();
                }
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait2 = new WaitForm(LoadPackings);
                wait2.ShowDialog();
                BindCBPackings();

                WaitForm wati3 = new WaitForm(LoadGoodCompany);
                wati3.ShowDialog();

                tbCompany.Text = goodCompany.Name;

                Gujjar.NumbersOnly(tbQty);

                Gujjar.TB4(pMain);

                WaitForm wait4 = new WaitForm(LoadSettings);
                wait4.ShowDialog();
                dtpIssue.MinDate = sett.StartDate;
                dtpIssue.MaxDate = sett.EndDate;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadPackings()
        {
            if (dealPackings.Count > 0)
                dealPackings.Clear();
            try
            {
                using (Context db = new Context())
                {
                    dealPackings = db.DealPackings
                        .OrderBy(a => a.Name).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindCBPackings()
        {
            cbPackings.DataSource = dealPackings;
            cbPackings.DisplayMember = "Name";
            cbPackings.ValueMember = "Id";

            cbPackings.SelectedItem = cbPackings.Items.OfType<DealPacking>()
                .FirstOrDefault(a => a.Name == "N/A");
        }
        private void btnAddPacking_Click(object sender, EventArgs e)
        {
            try
            {
                AddPackingForm form = new AddPackingForm();
                form.ShowDialog();

                int pid = form.PackingId;
                if (pid != 0)
                {
                    WaitForm w5 = new WaitForm(LoadPackings, "");
                    w5.ShowDialog();

                    BindCBPackings();

                    cbPackings.SelectedItem = cbPackings.Items.OfType<DealPacking>()
                        .FirstOrDefault(a => a.Id == pid);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            try
            {
                
                
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }

                if(cbPackings.SelectedIndex == -1)
                {
                    throw new Exception("Please select packing");
                }
                decimal factoryStock = tbFactoryStockQty.Text.ToDecimal();
                decimal compStock = tbQty.Text.ToDecimal();

                if(factoryStock < compStock)
                {
                    throw new Exception(string.Format("Facotory stock of packing {0} is low as compared to your entered stock amount", cbPackings.Text));
                }
                DealPacking dealPacking = cbPackings.SelectedItem as DealPacking;
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            PackingIssueReceiveRecord issueRec = new PackingIssueReceiveRecord
                            {
                                DateTime = dtpIssue.Value,
                                Qty = tbQty.Text.ToDecimal(),
                                RecordType = RecordType.Issue,
                                DealPackingId = dealPacking.Id,
                                GoodCompanyId = gCompId,
                                Remarks = string.Format("Issuance of {0} {1}-(s) to good company ({2}), dated: {3}.\nDescription: {4}",
                                tbQty.Text, dealPacking.Name, goodCompany.Name, dtpIssue.Value, tbDescription.Text),
                                Description = tbDescription.Text,
                                Unum = Helper.Unum
                            };

                            unum = issueRec.Unum;
                            db.PackingIssueReceiveRecords.Add(issueRec);
                            db.SaveChanges();
                            
                            Code128BarcodeDraw code2 = BarcodeDrawFactory.Code128WithChecksum;
                            Image img2 = code2.Draw(unum, 80);
                            byte[] barBytes = Gujjar.GetByteArrayFromImage(img2);

                            issuePackingRVM = new IssuePackingRVM
                            {
                                Company = goodCompany.Name,
                                Description = issueRec.Description,
                                IssueDateTime = issueRec.DateTime.ToString(),
                                IssuePackings = string.Format("{0} {1}-(s)", issueRec.Qty.ToString("n1"), dealPacking.Name),
                                Barcode = barBytes
                            };

                            FactoryPackingStockIssueRecord fpsir = new FactoryPackingStockIssueRecord
                            {
                                DealPackingId = dealPacking.Id,
                                Description = string.Format("Issuance of {0} {1}-(s) to good company ({2}), dated: {3}.\nDescription: {4}",
                                tbQty.Text, dealPacking.Name, goodCompany.Name, dtpIssue.Value, tbDescription.Text),
                                IssuedDate = dtpIssue.Value,
                                QtyIssued = tbQty.Text.ToDecimal()
                            };

                            db.FactoryPackingStockIssueRecords.Add(fpsir);
                            db.SaveChanges();
                            
                            var stObj = db.PackingStocks.FirstOrDefault(a => a.GoodCompanyId == gCompId && a.DealPackingId == dealPacking.Id);
                            if(stObj != null)
                            {
                                stObj.Balance += issueRec.Qty;

                                db.Entry(stObj).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                            else
                            {
                                PackingStock pStock = new PackingStock
                                {
                                    Balance = issueRec.Qty,
                                    DealPackingId = dealPacking.Id,
                                    GoodCompanyId = gCompId
                                };

                                db.PackingStocks.Add(pStock);
                                db.SaveChanges();
                            }
                            var factObj = db.FactoryPackingStocks.FirstOrDefault(a => a.DealPackingId == dealPacking.Id);
                            factObj.Quantity -= issueRec.Qty;
                            db.Entry(factObj).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            if(bsPrinterState.Value)
                            {
                                for (int i = 0; i < (int)numUpDown.Value; i++)
                                {
                                    PrintReport();
                                }
                            }
                           
                            trans.Commit();
                            IsDone = true;

                            Gujjar.InfoMsg(string.Format("{0} ({1}-(s)) are issued to {2} and record is saved in database", 
                                issueRec.Qty, dealPacking.Name, tbCompany.Text));
                            
                            Close();
                        }
                        catch (Exception exp2)
                        {
                            trans.Rollback();
                            throw exp2;
                        }
                    }
                }
                
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void PrintReport()
        {
            try
            {
                IssuePackingRVM obj = new IssuePackingRVM();
                FactoryRVM factObj = new FactoryRVM
                {
                    Address = sett.Address,
                    Dated = DateTime.Now.ToString(),
                    LogoImg = sett.Logo,
                    Name = sett.Name,
                    Phone = sett.PhoneNo
                };

                Code128BarcodeDraw draw = BarcodeDrawFactory.Code128WithChecksum;
                Image img = draw.Draw(unum, 50);
                factObj.Barcode = Gujjar.GetByteArrayFromImage(img);

                IssuePackingReport report = new IssuePackingReport();

                DetailReportBand band1 = report.Bands["DetailReport"] as DetailReportBand;
                band1.DataSource = new List<FactoryRVM> { factObj };

                DetailReportBand band2 = report.Bands["DetailReport1"] as DetailReportBand;
                band2.DataSource = new List<IssuePackingRVM> { issuePackingRVM };
                report.ShowPrintMarginsWarning = false;
                report.ShowPrintStatusDialog = false;
                report.Print(sett.ThermalPrinter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbPackings_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbPackings.SelectedIndex == -1)
                    return;

                DealPacking pack = cbPackings.SelectedItem as DealPacking;

                if (pack.Name == "N/A")
                {
                    tbStockBalance.Text = "0";
                    label6.Text = label5.Text = "";
                    return;
                }
                label6.Text = string.Format("Factory Stock Balance of {0}-(s)", pack.Name);
                label5.Text = string.Format("{0}-(s) Company Balance", pack.Name);
                using (Context db = new Context())
                {
                    var obj = db.PackingStocks.FirstOrDefault(a => a.GoodCompanyId == gCompId && a.DealPackingId == pack.Id);
                    if(obj == null)
                    {
                        tbStockBalance.Text = "0";
                    }
                    else
                    {
                        tbStockBalance.Text = obj.Balance.ToString("n1");
                    }
                    var obj2 = db.FactoryPackingStocks.FirstOrDefault(a => a.DealPackingId == pack.Id);
                    if(obj2 == null)
                    {
                        tbFactoryStockQty.Text = "0";
                    }
                    else
                    {
                        tbFactoryStockQty.Text = obj2.Quantity.ToString("n1");
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string qtyStr = tbQty.Text;
                if (string.IsNullOrEmpty(qtyStr))
                {
                    tbQty.Clear();
                    return;
                }
                else
                {
                    decimal facStockQty = tbFactoryStockQty.Text.ToDecimal();
                    decimal qtyNow = qtyStr.ToDecimal();

                    if (qtyNow > facStockQty)
                    {
                        tbQty.Focus();
                        tbQty.BackColor = Color.Pink;
                        throw new Exception(string.Format("Factory stock of ({0}) is {1} and you entered qty {2}.", cbPackings.Text, tbFactoryStockQty.Text, tbQty.Text));
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
            
        }

        private void bsPrinterState_Click(object sender, EventArgs e)
        {
            numUpDown.Enabled = bsPrinterState.Value;
        }
    }
}
