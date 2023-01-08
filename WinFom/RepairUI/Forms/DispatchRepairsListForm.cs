using Khattana.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinFom.Admin.Database;
using WinFom.Common.Forms;
using Model.Repair.Model;
using System.Data.Entity;
using Model.Repair.ViewModel;
using WinFom.RepairUI.Reports.Model;
using Model.Admin.Model;
using WinFom.RepairUI.Reports.XtraReport;
using Zen.Barcode;
using System.Drawing;
using DevExpress.XtraReports.UI;
using WinFom.Common.Model;

namespace WinFom.RepairUI.Forms
{
    public partial class DispatchRepairsListForm : Form
    {
        private List<RepairDispatchRecord> dispatchRecords = null;
        private string btndgvreport = "dgvbtnreport";
        private string btndgvupdatebillid = "btndgvupdatebillid";
        private string btndgvreceiveitems = "btndgvreceiveitems";
        private string btndgvdetailrep = "btndgvdetailreport";
        public DispatchRepairsListForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadDispatchRecords()
        {
            try
            {
                if (dispatchRecords != null)
                {
                    dispatchRecords.Clear();
                    dispatchRecords = null;
                }
                using (Context db = new Context())
                {
                    dispatchRecords = db.RepairDispatchRecords.Include(a => a.Place).AsParallel().ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void UpdateDgv()
        {
            try
            {
                dispatchVMBindingSource.List.Clear();
                foreach (var item in dispatchRecords)
                {
                    DispatchVM vm = new DispatchVM
                    {
                        BillNo = item.BillNo,
                        BillPaid = item.BillPaid,
                        Date = item.Date.ToShortDateString(),
                        Id = item.Id,
                        Place = item.Place.Name,
                        ReceivedItems = item.ReceivedItems,
                        RemainingItems = item.RemainingItems,
                        TOPerson = item.TOPerson,
                        TotalItems = item.TotalItems,
                        PlaceId = item.RepPlaceId
                    };
                    dispatchVMBindingSource.List.Add(vm);
                }
                tbTotalDispatch.Text = dispatchRecords.Sum(a => a.TotalItems).ToString("n1");
                tbTotalEntries.Text = dispatchRecords.Count.ToString();
                tbTotalReceived.Text = dispatchRecords.Sum(a => a.ReceivedItems).ToString("n1");
                tbTotalRemaining.Text = dispatchRecords.Sum(a => a.RemainingItems).ToString("n1");
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
                WaitForm wait = new WaitForm(LoadDispatchRecords);
                wait.ShowDialog();
                Gujjar.AddDatagridviewButton(dgv, btndgvupdatebillid, "Update Bill Id", "Update Bill Id", 120);
                Gujjar.AddDatagridviewButton(dgv, btndgvreport, "Dispatch Report", "Dispatch Report", 120);
                Gujjar.AddDatagridviewButton(dgv, btndgvreceiveitems, "Receive Items", "Receive Items", 120);
                Gujjar.AddDatagridviewButton(dgv, btndgvdetailrep, "Detail Report", "Detail Report", 120);
                UpdateDgv();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                RepairEntriesForm form = new RepairEntriesForm();
                form.ShowDialog();

                if (form.IsDone)
                {
                    WaitForm wait = new WaitForm(LoadDispatchRecords);
                    wait.ShowDialog();

                    UpdateDgv();
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
                {
                    return;
                }
                if (dgv.Columns[btndgvreceiveitems].Index == ci)
                {
                    DialogResult res = Gujjar.ConfirmYesNo("Are you sured");
                    if (res == DialogResult.No)
                        return;

                    int id = dgv.Rows[ri].Cells[0].Value.ToInt();
                    ReceiveRepairItemsForm form = new ReceiveRepairItemsForm(id);
                    form.ShowDialog();

                    if (form.IsDone)
                    {
                        WaitForm wait = new WaitForm(LoadDispatchRecords);
                        wait.ShowDialog();

                        UpdateDgv();
                    }
                }
                if (dgv.Columns[btndgvupdatebillid].Index == ci)
                {
                    if (!Helper.ConfirmAdminPassword())
                        return;

                    DialogResult res = Gujjar.ConfirmYesNo("Are you sure to update bill Id");
                    if (res == DialogResult.No)
                        return;

                    int id = dgv.Rows[ri].Cells[0].Value.ToInt();
                    var obj = dispatchVMBindingSource.List.OfType<DispatchVM>().FirstOrDefault(a => a.Id == id);

                    BillTransVM vm = new BillTransVM
                    {
                        BillId = obj.BillNo,
                        Place = obj.Place,
                        PlaceId = obj.PlaceId
                    };

                    UpdateBillIdForm form = new UpdateBillIdForm(vm);
                    form.ShowDialog();

                    string billId2 = form.BillId;
                    if (!string.IsNullOrEmpty(billId2))
                    {
                        obj.BillNo = billId2;
                        dgv.Refresh();
                        using (Context db = new Context())
                        {
                            var ddobj = db.RepairDispatchRecords.Find(id);
                            ddobj.BillNo = billId2;
                            db.Entry(ddobj).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        Gujjar.InfoMsg("Bill Id is updated");
                    }
                }
                if (dgv.Columns[btndgvreport].Index == ci)
                {
                    int did = dgv.Rows[ri].Cells[0].Value.ToInt();

                    using (Context db = new Context())
                    {
                        var disObj = db.RepairDispatchRecords.Find(did);
                        disObj.RepEntries = db.RepEntries.Where(a => a.RepairDispatchRecordId == did && a.Direction == RepEntryDirection.Dispatched).ToList();
                        disObj.Place = db.RepPlaces.Find(disObj.RepPlaceId);

                        List<DispatchCompanyVM> companyListVM = new List<DispatchCompanyVM>();
                        AppSettings appSett = db.AppSettings.First();
                        DispatchCompanyVM compVM = new DispatchCompanyVM
                        {
                            Address = appSett.Address,
                            Barcode = null,
                            BillId = disObj.BillNo,
                            Dated = disObj.Date.ToShortDateString(),
                            Logo = appSett.Logo,
                            Name = appSett.Name,
                            Phone = appSett.PhoneNo,
                            Qrcode = null,
                            RepairShop = string.Format("{0} ({1})", disObj.Place.Name, disObj.Place.Address),
                            ReportTitle = "Items dispatch for repairing report",
                            TOPerson = disObj.TOPerson,
                            RepId = disObj.Id
                        };
                        if (compVM.BillId == "N/A")
                        {
                            compVM.BillId = "";
                        }
                        Code128BarcodeDraw bcode = BarcodeDrawFactory.Code128WithChecksum;
                        Image img = bcode.Draw(disObj.Unum, 50);
                        byte[] imgBytes = Gujjar.GetByteArrayFromImage(img);
                        compVM.Barcode = imgBytes;

                        CodeQrBarcodeDraw qrCode = BarcodeDrawFactory.CodeQr;
                        Image img2 = qrCode.Draw("GBS Solutions, Burewala. 0323-9372084", 50);
                        byte[] qrBytes = Gujjar.GetByteArrayFromImage(img2);
                        compVM.Qrcode = qrBytes;


                        companyListVM.Add(compVM);
                        List<DispatchItemDetail> dispatchItemDetailList = new List<DispatchItemDetail>();
                        int n = 1;
                        foreach (var item in disObj.RepEntries)
                        {
                            item.RepItem = db.RepItems.Find(item.RepItemId);
                            item.RepItem.Location = db.Locations.Find(item.RepItem.LocationId);
                            item.RepItem.ItemCategory = db.ItemCategories.Find(item.RepItem.ItemCategoryId);

                            DispatchItemDetail vm = new DispatchItemDetail
                            {
                                Item = string.Format("{0}-{1}-{2}", item.RepItem.ItemCategory.Title, item.RepItem.Name, item.RepItem.Location.Name),
                                Qty = item.DispatchQty,
                                Remarks = item.Remarks,
                                SrNo = n++
                            };
                            dispatchItemDetailList.Add(vm);
                        }

                        DispatchItemsReport rep = new DispatchItemsReport();

                        DetailReportBand band1 = rep.Bands["DetailReport"] as DetailReportBand;
                        DetailReportBand band2 = rep.Bands["DetailReport1"] as DetailReportBand;

                        band1.DataSource = companyListVM;
                        band2.DataSource = dispatchItemDetailList;

                        var p1 = rep.Parameters["totalItems"];
                        p1.Visible = false;
                        p1.Value = disObj.RepEntries.Sum(a => a.DispatchQty).ToString("n1");

                        var p2 = rep.Parameters["billPaid"];
                        p2.Visible = false;
                        p2.Value = disObj.BillPaid.ToString("n1");

                        var p3 = rep.Parameters["received"];
                        p3.Visible = false;
                        p3.Value = disObj.ReceivedItems.ToString("n1");

                        var p4 = rep.Parameters["ur"];
                        p4.Visible = false;
                        p4.Value = disObj.RemainingItems.ToString("n1");

                        var lengendsParam = rep.Parameters["Legends"];
                        lengendsParam.Visible = false;
                        lengendsParam.Value = Helper.Legends();

                        rep.ShowPrintMarginsWarning = false;
                        rep.ShowPrintStatusDialog = false;
                        rep.ShowRibbonPreview();
                    }
                }

                if (dgv.Columns[btndgvdetailrep].Index == ci)
                {
                    int did = dgv.Rows[ri].Cells[0].Value.ToInt();

                    using (Context db = new Context())
                    {
                        var disObj = db.RepairDispatchRecords.Find(did);
                        List<RepEntry> dispEntries = db.RepEntries
                            .Where(a => a.RepairDispatchRecordId == did && a.Direction == RepEntryDirection.Dispatched).ToList();
                        disObj.Place = db.RepPlaces.Find(disObj.RepPlaceId);

                        List<RepairReceiveRecord> receRecords = db.RepairReceivedRecords.Where(a => a.DispatchId == did).ToList();
                        foreach (var item in receRecords)
                        {
                            item.Entries = db.RepEntries.Where(a => a.RepairReceiveRecordId == item.Id).ToList();
                            foreach (var item2 in item.Entries)
                            {
                                if (item2.RepItem == null)
                                    item2.RepItem = db.RepItems.Find(item2.RepItemId);
                                if (item2.RepItem.Location == null)
                                    item2.RepItem.Location = db.Locations.Find(item2.RepItem.LocationId);
                            }
                        }

                        List<DispatchCompanyVM> companyListVM = new List<DispatchCompanyVM>();
                        AppSettings appSett = db.AppSettings.First();
                        DispatchCompanyVM compVM = new DispatchCompanyVM
                        {
                            Address = appSett.Address,
                            Barcode = null,
                            BillId = disObj.BillNo,
                            Dated = disObj.Date.ToShortDateString(),
                            Logo = appSett.Logo,
                            Name = appSett.Name,
                            Phone = appSett.PhoneNo,
                            Qrcode = null,
                            RepairShop = string.Format("{0} ({1})", disObj.Place.Name, disObj.Place.Address),
                            ReportTitle = "Items dispatch for repairing report",
                            TOPerson = disObj.TOPerson,
                            RepId = disObj.Id
                        };
                        if (compVM.BillId == "N/A")
                        {
                            compVM.BillId = "";
                        }
                        Code128BarcodeDraw bcode = BarcodeDrawFactory.Code128WithChecksum;
                        Image img = bcode.Draw(disObj.Unum, 50);
                        byte[] imgBytes = Gujjar.GetByteArrayFromImage(img);
                        compVM.Barcode = imgBytes;

                        CodeQrBarcodeDraw qrCode = BarcodeDrawFactory.CodeQr;
                        Image img2 = qrCode.Draw("GBS Solutions, Burewala. 0323-9372084", 50);
                        byte[] qrBytes = Gujjar.GetByteArrayFromImage(img2);
                        compVM.Qrcode = qrBytes;


                        companyListVM.Add(compVM);
                        List<DispatchItemDetail> dispatchItemDetailList = new List<DispatchItemDetail>();
                        int n = 1;
                        foreach (var item in dispEntries)
                        {
                            item.RepItem = db.RepItems.Find(item.RepItemId);
                            item.RepItem.Location = db.Locations.Find(item.RepItem.LocationId);
                            item.RepItem.ItemCategory = db.ItemCategories.Find(item.RepItem.ItemCategoryId);

                            DispatchItemDetail vm = new DispatchItemDetail
                            {
                                Item = string.Format("{0}-{1}-{2}", item.RepItem.ItemCategory.Title, item.RepItem.Name, item.RepItem.Location.Name),
                                Qty = item.DispatchQty,
                                Remarks = item.Remarks,
                                SrNo = n++
                            };
                            dispatchItemDetailList.Add(vm);
                        }

                        List<ReceItemsVM> receItemsVMList = new List<ReceItemsVM>();
                        foreach (var item in receRecords)
                        {
                            ReceItemsVM vm = new ReceItemsVM
                            {
                                BillAmount = item.BillAmount,
                                Dated = item.ReceivedDate.ToShortDateString(),
                                Id = item.Id,
                                Items = item.Entries.Select(a => new ReceItem { Item = string.Format("{0}-{1}", a.RepItem.Name, a.RepItem.Location.Name), Qty = a.ReceivedQty, Remarks = a.Remarks }).ToList(),
                                RecePerson = item.ReceivingPerson,
                                TotalReceItems = item.Entries.Sum(a => a.ReceivedQty)
                            };
                            receItemsVMList.Add(vm);
                        }

                        DispatchReceiveItemsReport rep = new DispatchReceiveItemsReport();

                        DetailReportBand band1 = rep.Bands["DetailReport"] as DetailReportBand;
                        DetailReportBand band2 = rep.Bands["DetailReport1"] as DetailReportBand;
                        DetailReportBand band3 = rep.Bands["DetailReport2"] as DetailReportBand;

                        band1.DataSource = companyListVM;
                        band2.DataSource = dispatchItemDetailList;
                        band3.DataSource = receItemsVMList;

                        var p1 = rep.Parameters["totalItems"];
                        p1.Visible = false;
                        p1.Value = disObj.RepEntries.Sum(a => a.DispatchQty).ToString("n1");

                        var p2 = rep.Parameters["billPaid"];
                        p2.Visible = false;
                        p2.Value = disObj.BillPaid.ToString("n1");

                        var p3 = rep.Parameters["received"];
                        p3.Visible = false;
                        p3.Value = disObj.ReceivedItems.ToString("n1");

                        var p4 = rep.Parameters["ur"];
                        p4.Visible = false;
                        p4.Value = disObj.RemainingItems.ToString("n1");

                        var lengendsParam = rep.Parameters["Legends"];
                        lengendsParam.Visible = false;
                        lengendsParam.Value = Helper.Legends();


                        rep.ShowPrintMarginsWarning = false;
                        rep.ShowPrintStatusDialog = false;
                        rep.ShowRibbonPreview();
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
