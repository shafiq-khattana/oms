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
using Model.ReadyStuff.Model;
using WinFom.Common.Forms;
using Model.Admin.Model;
using WinFom.Common.Model;
using Model.Entertainment.Model;
using Model.Entertainment.ViewModel;
using WinFom.EntertainmentUI.Reports.Model;
using Zen.Barcode;
using DevExpress.XtraReports.UI;
using WinFom.EntertainmentUI.Reports.XtraReports;

namespace WinFom.EntertainmentUI.Forms
{
    public partial class AddEPurshaseForm : Form
    {
        private List<EntItem> entItems = null;
        private AppSettings AppSett = Helper.AppSet;
        private AppUser appuser = SingleTon.LoginForm.appUser;
        private string dgvbtndel = "dgvbtndel12314";
        private EntPurchase entPurchase = null;
        public bool IsDone { get; set; }
        public AddEPurshaseForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadEntItems()
        {
            try
            {
                using (Context db = new Context())
                {
                    entItems = db.EntItems.ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait = new WaitForm(LoadEntItems);
                wait.ShowDialog();
                tbRemarks.Text = "نوریمارکس";
                Gujjar.TBOptionalUrdu(tbRemarks);
                Gujjar.UrduFont(tbRemarks);
                bswUrdu.Value = true;

                Gujjar.TB4(panel1);
                Gujjar.AddDatagridviewButton(dgv, dgvbtndel, "Delete", "Delete", 80);
                IsDone = false;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddEItemVMForm form = new AddEItemVMForm(entItems);
                form.ShowDialog();

                var obj = form.eItemVM;
                if(obj != null)
                {
                    var list = ePurchaseItemVMBindingSource.List.OfType<ePurchaseItemVM>().ToList();

                    var obj2 = list.FirstOrDefault(a => a.Id == obj.Id);
                    if(obj2 != null)
                    {
                        obj2.Qty += obj.Qty;
                    }
                    else
                    {
                        ePurchaseItemVMBindingSource.List.Add(obj);
                    }
                    dgv.Refresh();
                }
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
                int ci = e.ColumnIndex;

                if (ri == -1 || ri == dgv.NewRowIndex)
                    return;

                if(dgv.Columns[dgvbtndel].Index == ci)
                {
                    DialogResult res = Gujjar.ConfirmYesNo("Please confirm ..!! before to delete?");
                    if (res == DialogResult.No)
                        return;

                    int id = dgv.Rows[ri].Cells[0].Value.ToInt();
                    var obj = ePurchaseItemVMBindingSource.List.OfType<ePurchaseItemVM>()
                        .FirstOrDefault(a => a.Id == id);
                    ePurchaseItemVMBindingSource.List.Remove(obj);

                    dgv.Refresh();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var list = ePurchaseItemVMBindingSource.List.OfType<ePurchaseItemVM>()
                    .ToList();
                if(list.Count == 0)
                {
                    throw new Exception("Please add some items before to proceed on");
                }

                if(!Gujjar.IsValidForm(panel1))
                {
                    throw new Exception("Please fill all text fields");
                }

                if(!Helper.ConfirmUserPassword())
                {
                    return;
                }

                DialogResult res = Gujjar.ConfirmYesNo("Please confirm all information is correct!!");
                if (res == DialogResult.No)
                    return;

                using (Context db = new Context())
                {
                    
                    using (var trans = db.Database.BeginTransaction())
                    {
                        EntPurchase purchase = new EntPurchase
                        {
                            Dated = dtp.Value,
                            Entries = null,
                            Id = 0,
                            Qty = list.Sum(a => a.Qty),
                            Remarks = tbRemarks.Text,
                            Unum = Helper.Unum,
                            Operator = appuser.Id
                        };

                        purchase = db.EntPurchases.Add(purchase);
                        db.SaveChanges();

                        if (entPurchase != null)
                        {
                            entPurchase = null;
                            entPurchase.Entries.Clear();
                        }
                        entPurchase = new EntPurchase
                        {
                            Dated = purchase.Dated,
                            Entries = new List<EntItemEntry>(),
                            Id = purchase.Id,
                            Qty = purchase.Qty,
                            Remarks = purchase.Remarks,
                            Unum = purchase.Unum
                        };
                        
                        foreach (var item in list)
                        {
                            EntItemEntry eEntry = new EntItemEntry
                            {
                                EntItemId = item.Id,
                                Id = 0,
                                EntPurchaseId = purchase.Id,
                                Item = null,
                                Purchase = null,
                                Qty = item.Qty
                            };
                            db.EntItemEntries.Add(eEntry);
                            entPurchase.Entries.Add(eEntry);

                            var itemDb = db.EntItems.Find(item.Id);
                            itemDb.QtyConsumed += item.Qty;
                            db.Entry(itemDb).State = System.Data.Entity.EntityState.Modified;
                        }
                        db.SaveChanges();
                        trans.Commit();

                        if(bswPrinter.Value)
                        {
                            for (int i = 0; i < numUpDown.Value; i++)
                            {
                                if(bswUrdu.Value)
                                {
                                    PrintInvoiceUrdu();
                                }
                                else
                                {
                                    PrintInvoiceEng();
                                }
                            }
                        }
                        Gujjar.InfoMsg("Record is added and saved successfully");
                        IsDone = true;
                        Close();
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private List<ECompRVM> ecomprvmList = null;
        private List<EPurEntry> epurentryList = null;
        private List<EPurRVM> epurrvmList = null;
        private void PrintInvoiceEng()
        {
            try
            {
                if(ecomprvmList != null)
                {
                    ecomprvmList.Clear();
                    ecomprvmList = null;
                }
                if(epurentryList != null)
                {
                    epurentryList.Clear();
                    epurentryList = null;
                }
                if(epurrvmList != null)
                {
                    epurrvmList.Clear();
                    epurrvmList = new List<EPurRVM>();
                }

                ecomprvmList = new List<ECompRVM>();
                epurrvmList = new List<EPurRVM>();
                epurentryList = new List<EPurEntry>();

                EPurRVM epurRVM = new EPurRVM
                {
                    Id = entPurchase.Id,
                    Dated = entPurchase.Dated.ToString(),
                    TQty = entPurchase.Entries.Sum(a => a.Qty),
                    Unum = entPurchase.Unum
                };
                epurrvmList.Add(epurRVM);
                int cnt = 1;
                foreach (var item in entPurchase.Entries)
                {
                    EPurEntry entry = new EPurEntry
                    {
                        ItemEng = item.Item.Title,
                        ItemUrdu = item.Item.NameUrdu,
                        Qty = item.Qty.ToString("n1"),
                        SrNo = cnt++
                    };
                    epurentryList.Add(entry);
                }
                ECompRVM ecompRvm = new ECompRVM
                {
                    Address = AppSett.Address,
                    Barcode = null,
                    Logo = AppSett.Logo,
                    Name = AppSett.Name,
                    Phone = AppSett.PhoneNo,
                    Qrcode = null,
                    RepDate = entPurchase.Dated.ToString(),
                    RepTitle = "ePurchase"
                };
                Code128BarcodeDraw draw = BarcodeDrawFactory.Code128WithChecksum;
                var image1 = draw.Draw(entPurchase.Unum, 50);
                ecompRvm.Barcode = Gujjar.GetByteArrayFromImage(image1);

                CodeQrBarcodeDraw draw2 = BarcodeDrawFactory.CodeQr;
                var image2 = draw2.Draw("GBS Solutions, Burewala. 0333-9372084", 50);
                ecompRvm.Qrcode = Gujjar.GetByteArrayFromImage(image2);

                ecomprvmList.Add(ecompRvm);

                EPurReport rep = new EPurReport();
                DetailReportBand band1 = rep.Bands["DetailReport"] as DetailReportBand;
                DetailReportBand band2 = rep.Bands["DetailReport1"] as DetailReportBand;
                DetailReportBand band3 = rep.Bands["DetailReport2"] as DetailReportBand;

                band1.DataSource = ecomprvmList;
                band2.DataSource = epurrvmList;
                band3.DataSource = epurentryList;

                var op = rep.Parameters["Op"];
                op.Visible = false;
                op.Value = appuser.Id;
                var lengendsParam = rep.Parameters["Legends"];
                lengendsParam.Visible = false;
                lengendsParam.Value = Helper.Legends();
                rep.ShowPrintMarginsWarning = false;
                rep.ShowPrintStatusDialog = false;
                rep.Print(AppSett.ThermalPrinter);

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void PrintInvoiceUrdu()
        {
            try
            {
                if (ecomprvmList != null)
                {
                    ecomprvmList.Clear();
                    ecomprvmList = null;
                }
                if (epurentryList != null)
                {
                    epurentryList.Clear();
                    epurentryList = null;
                }
                if (epurrvmList != null)
                {
                    epurrvmList.Clear();
                    epurrvmList = new List<EPurRVM>();
                }

                ecomprvmList = new List<ECompRVM>();
                epurrvmList = new List<EPurRVM>();
                epurentryList = new List<EPurEntry>();

                EPurRVM epurRVM = new EPurRVM
                {
                    Id = entPurchase.Id,
                    Dated = entPurchase.Dated.ToString(),
                    TQty = entPurchase.Entries.Sum(a => a.Qty),
                    Unum = entPurchase.Unum
                };
                epurrvmList.Add(epurRVM);
                int cnt = 1;
                foreach (var item in entPurchase.Entries)
                {
                    EPurEntry entry = new EPurEntry
                    {
                        ItemEng = item.Item.Title,
                        ItemUrdu = item.Item.NameUrdu,
                        Qty = item.Qty.ToString("n1"),
                        SrNo = cnt++
                    };
                    epurentryList.Add(entry);
                }
                ECompRVM ecompRvm = new ECompRVM
                {
                    Address = AppSett.AddressUrdu,
                    Barcode = null,
                    Logo = AppSett.Logo,
                    Name = AppSett.NameUrdu,
                    Phone = AppSett.PhoneNo,
                    Qrcode = null,
                    RepDate = entPurchase.Dated.ToString(),
                    RepTitle ="چاہےپانی"
                };
                Code128BarcodeDraw draw = BarcodeDrawFactory.Code128WithChecksum;
                var image1 = draw.Draw(entPurchase.Unum, 50);
                ecompRvm.Barcode = Gujjar.GetByteArrayFromImage(image1);

                CodeQrBarcodeDraw draw2 = BarcodeDrawFactory.CodeQr;
                var image2 = draw2.Draw("GBS Solutions, Burewala. 0333-9372084", 50);
                ecompRvm.Qrcode = Gujjar.GetByteArrayFromImage(image2);

                ecomprvmList.Add(ecompRvm);

                EPurReportUrdu rep = new EPurReportUrdu();
                DetailReportBand band1 = rep.Bands["DetailReport"] as DetailReportBand;
                DetailReportBand band2 = rep.Bands["DetailReport1"] as DetailReportBand;
                DetailReportBand band3 = rep.Bands["DetailReport2"] as DetailReportBand;

                band1.DataSource = ecomprvmList;
                band2.DataSource = epurrvmList;
                band3.DataSource = epurentryList;

                var op = rep.Parameters["Op"];
                op.Visible = false;
                op.Value = appuser.Id;
                var lengendsParam = rep.Parameters["Legends"];
                lengendsParam.Visible = false;
                lengendsParam.Value = Helper.Legends();
                rep.ShowPrintMarginsWarning = false;
                rep.ShowPrintStatusDialog = false;
                rep.Print(AppSett.ThermalPrinter);

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void bswUrdu_Click(object sender, EventArgs e)
        {
            if(bswUrdu.Value)
            {
                Gujjar.NotTBOptional(tbRemarks);
                Gujjar.NotUrduFont(tbRemarks);
                Gujjar.NotTbOptionalUrdu(tbRemarks);

                Gujjar.TBOptionalUrdu(tbRemarks);
                Gujjar.UrduFont(tbRemarks);
                tbRemarks.Text = "نوریمارکس";
            }
            else
            {
                Gujjar.NotTBOptional(tbRemarks);
                Gujjar.NotUrduFont(tbRemarks);
                Gujjar.NotTbOptionalUrdu(tbRemarks);

                Gujjar.TBOptional(tbRemarks);
                Gujjar.TahomaFont(tbRemarks);
                tbRemarks.Text = "N/A";
            }
        }
    }
}
