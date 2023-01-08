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
using WinFom.Common.Forms;
using WinFom.Deal.Forms;
using WinFom.Common.Model;
using Model.Admin.Model;
using WinFom.AppGoodCompany.Reports.Model;
using WinFom.Reports.Model;
using Zen.Barcode;
using DevExpress.XtraReports.UI;
using WinFom.AppGoodCompany.Reports.Report;

namespace WinFom.AppGoodCompany.Forms
{
    public partial class XIssuePackingForm : Form
    {
        private int _issuegCompId = 0;
        private GoodCompany issueGoodCompany = new GoodCompany();
        private GoodCompany receiveGoodCompany = new GoodCompany();
        private List<GoodCompany> goodCompanies = new List<GoodCompany>();
        private List<DealPacking> packings = new List<DealPacking>();
        public bool IsDone = false;
        private AppSettings sett = null;
        private XIssuePackingRVM xIssuePacking = null;

        public XIssuePackingForm(int gCompId)
        {
            InitializeComponent();
            _issuegCompId = gCompId;
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
        private void LoadData()
        {
            try
            {
                using (Context db = new Context())
                {
                    issueGoodCompany = db.GoodCompanies.Find(_issuegCompId);
                    goodCompanies = db.GoodCompanies.Where(a => a.Id != _issuegCompId).ToList();
                    packings = db.DealPackings.Where(a => a.Name != "N/A").ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadGoodCompanies()
        {
            try
            {
                using (Context db = new Context())
                {
                    goodCompanies = db.GoodCompanies.Where(a => a.Id != _issuegCompId).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindCBPackings()
        {
            cbPackings.DataSource = packings;
            cbPackings.DisplayMember = "Name";
            cbPackings.ValueMember = "Id";
        }
        private void BindCBGoodCompanies()
        {
            cbGoodCompanies.DataSource = goodCompanies;
            cbGoodCompanies.DisplayMember = "Name";
            cbGoodCompanies.ValueMember = "Id";
        }
        private void LoadPackings()
        {
            try
            {
                using (Context db = new Context())
                {

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
                WaitForm wait = new WaitForm(LoadData);
                wait.ShowDialog();

                BindCBGoodCompanies();
                BindCBPackings();

                lblHeading.Text = string.Format("X Issue Of Packing from {0}.", issueGoodCompany.Name);

                tbIssueGoodCompany.Text = issueGoodCompany.Name;

                Gujjar.TB4(pMain);

                WaitForm wait2 = new WaitForm(LoadSettings);
                wait2.ShowDialog();

                dtp.MinDate = sett.StartDate;
                dtp.MaxDate = sett.EndDate;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void pMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbPackings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPackings.SelectedIndex == -1)
                return;

            DealPacking pack = cbPackings.SelectedItem as DealPacking;

            label9.Text = label10.Text = label11.Text = string.Format("{0}-(s)", pack.Name);

            try
            {
                using (Context db = new Context())
                {
                    var obj = db.PackingStocks.FirstOrDefault(a => a.DealPackingId == pack.Id && a.GoodCompanyId == _issuegCompId);
                    tbPackingsAtIssuanceGoodCompany.Text = "0";
                    if(obj != null)
                    {
                        tbPackingsAtIssuanceGoodCompany.Text = obj.Balance.ToString("n1");
                    }

                    decimal qty = 0;

                    if(cbGoodCompanies.SelectedIndex != -1 && cbGoodCompanies.Text != "N/A")
                    {
                        GoodCompany gCom = cbGoodCompanies.SelectedItem as GoodCompany;
                        var obj2 = db.PackingStocks.FirstOrDefault(a => a.DealPackingId == pack.Id && a.GoodCompanyId == gCom.Id);
                        if(obj2 != null)
                        {
                            qty = obj2.Balance;
                        }
                    }

                    tbPackingsAtReceivingGoodCompany.Text = qty.ToString("n1");
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }

        }

        private void cbGoodCompanies_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cbGoodCompanies.SelectedIndex == -1 || cbPackings.SelectedIndex == -1)
                return;

            try
            {
                using (Context db = new Context())
                {
                    DealPacking pack = cbPackings.SelectedItem as DealPacking;
                    GoodCompany gComp = cbGoodCompanies.SelectedItem as GoodCompany;

                    decimal qty = 0;
                    var obj = db.PackingStocks.FirstOrDefault(a => a.DealPackingId == pack.Id && a.GoodCompanyId == gComp.Id);
                    if(obj != null)
                    {
                        qty = obj.Balance;
                    }

                    tbPackingsAtReceivingGoodCompany.Text = qty.ToString("n1");

                    decimal qty2 = 0;
                    var obj2 = db.PackingStocks.FirstOrDefault(a => a.DealPackingId == pack.Id && a.GoodCompanyId == _issuegCompId);
                    if(obj2 != null)
                    {
                        qty2 = obj2.Balance;
                    }
                    tbPackingsAtIssuanceGoodCompany.Text = qty2.ToString("n1");
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddGoodCompany_Click(object sender, EventArgs e)
        {
            try
            {
                AddGoodsCompanyForm form = new AddGoodsCompanyForm();
                form.ShowDialog();

                int cid = form.GoodCompanyId;
                if (cid != 0)
                {
                    WaitForm wait1 = new WaitForm(LoadGoodCompanies);
                    wait1.ShowDialog();
                    BindCBGoodCompanies();

                    cbGoodCompanies.SelectedItem = cbGoodCompanies.Items.OfType<GoodCompany>().FirstOrDefault(a => a.Id == cid);
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
                
                FactoryRVM factObj = new FactoryRVM
                {
                    Address = sett.Address,
                    Dated = DateTime.Now.ToString(),
                    LogoImg = sett.Logo,
                    Name = sett.Name,
                    Phone = sett.PhoneNo
                };

                
                

                XIssuePackingReport report = new XIssuePackingReport();

                DetailReportBand band1 = report.Bands["DetailReport"] as DetailReportBand;
                band1.DataSource = new List<FactoryRVM> { factObj };

                DetailReportBand band2 = report.Bands["DetailReport1"] as DetailReportBand;
                band2.DataSource = new List<XIssuePackingRVM> { xIssuePacking };
                report.ShowPrintMarginsWarning = false;
                report.ShowPrintStatusDialog = false;
                report.Print(sett.ThermalPrinter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = Gujjar.ConfirmYesNo("Please confirm..!!");
                if (result == DialogResult.No)
                    return;


                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }

                if(cbGoodCompanies.SelectedIndex == -1 || cbPackings.SelectedIndex == -1)
                {
                    throw new Exception("Please choose good company and packing.");
                }

                DealPacking pack = cbPackings.SelectedItem as DealPacking;
                GoodCompany gComp = cbGoodCompanies.SelectedItem as GoodCompany;

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            decimal qty = tbNowPackingsToIssue.Text.ToDecimal();
                            var obj1 = db.PackingStocks.FirstOrDefault(a => a.DealPackingId == pack.Id && a.GoodCompanyId == _issuegCompId);

                            obj1.Balance -= qty;
                            db.Entry(obj1).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            var obj2 = db.PackingStocks.FirstOrDefault(a => a.DealPackingId == pack.Id && a.GoodCompanyId == gComp.Id);
                            if(obj2 != null)
                            {
                                obj2.Balance += qty;
                                db.Entry(obj2).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                            else
                            {
                                var obj3 = new PackingStock
                                {
                                    Balance = qty,
                                    DealPackingId = pack.Id,
                                    GoodCompanyId = gComp.Id
                                };
                                db.PackingStocks.Add(obj3);
                                db.SaveChanges();
                            }

                            string xmsg = string.Format("X-issue/receive of {0} {1}-(s). From Good Company ({2}) to Good Company ({3}). Dated: {4}.\nDescription: {5}",
                                qty.ToString("n1"), pack.Name, issueGoodCompany.Name, gComp.Name, dtp.Value, tbDescription.Text);

                            PackingIssueReceiveRecord record1 = new PackingIssueReceiveRecord
                            {
                                DateTime = dtp.Value,
                                DealPackingId = pack.Id,
                                GoodCompanyId = _issuegCompId,
                                Description = tbDescription.Text,
                                Qty = qty,
                                RecordType = RecordType.Receive,
                                Remarks = xmsg,
                                Unum = Helper.Unum
                            };
                            db.PackingIssueReceiveRecords.Add(record1);

                            PackingIssueReceiveRecord record2 = new PackingIssueReceiveRecord
                            {
                                DateTime = dtp.Value,
                                DealPackingId = pack.Id,
                                GoodCompanyId = gComp.Id,
                                Qty = qty,
                                RecordType = RecordType.Issue,
                                Description = tbDescription.Text,
                                Remarks = xmsg,
                                Unum = Helper.Unum
                            };

                            db.PackingIssueReceiveRecords.Add(record2);
                            
                            xIssuePacking = new XIssuePackingRVM
                            {
                                Description = tbDescription.Text,
                                FromCompany = issueGoodCompany.Name,
                                ToCompany = gComp.Name,
                                IssueDate = record2.DateTime.ToString(),
                                IssuePackings = string.Format("{0} {1}-(s)", record2.Qty.ToString("n1"), pack.Name)
                            };

                            Code128BarcodeDraw bcode = BarcodeDrawFactory.Code128WithChecksum;
                            Image img = bcode.Draw(record2.Unum, 50);
                            byte[] imgBytes = Gujjar.GetByteArrayFromImage(img);
                            xIssuePacking.Barcode = imgBytes;

                            if(bsPrinterState.Value)
                            {
                                for (int i = 0; i < (int)numUpDown.Value; i++)
                                {
                                    PrintReport();
                                }
                            }
                            
                            db.SaveChanges();
                            trans.Commit();

                            Gujjar.InfoMsg(string.Format("{0}\n\nCompleted..!!", xmsg));
                            IsDone = true;
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

        private void tbNowPackingsToIssue_TextChanged(object sender, EventArgs e)
        {
            string qtyStr = tbNowPackingsToIssue.Text;
            if(string.IsNullOrEmpty(qtyStr))
            {
                return;
            }
            try
            {
                decimal qty = qtyStr.ToDecimal();
                decimal qtyissue = tbPackingsAtIssuanceGoodCompany.Text.ToDecimal();

                if(qty > qtyissue)
                {
                    tbNowPackingsToIssue.Clear();
                    tbNowPackingsToIssue.Focus();
                    tbNowPackingsToIssue.BackColor = Color.Pink;
                    throw new Exception(string.Format("Qty at issuance side: {0}, is less than your desirious qty: {1}.", qty, qtyissue));
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
