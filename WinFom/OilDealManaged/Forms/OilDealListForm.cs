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
using System.Data.Entity;
using Model.ReadyStuff.ViewModel;
using WinFom.OilDealManaged.Reports.XtraReports;
using WinFom.OilDealManaged.Reports.Model;
using Zen.Barcode;
using DevExpress.XtraReports.UI;
using Model.Financials.Model;

namespace WinFom.OilDealManaged.Forms
{
    public partial class OilDealListForm : Form
    {
        private List<OilDeal> dealList = new List<OilDeal>();
        private AppSettings appSett = Helper.AppSet;
        private string btndgvprocess = "bndagprosw3242";
        private string btndgvcomplete = "askwerwerasd232";
        private string btndgvedit = "btndgavajweaawerw23421234";
        private string btndgvprint = "btndgvprint234234";
        private string btndgvcancel = "btndgvcancel";
        public OilDealListForm()
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
                Helper.IsOkApplied();
                Gujjar.AddDatagridviewButton(dgv, btndgvprocess, "Process", "Process", 84);
                Gujjar.AddDatagridviewButton(dgv, btndgvcomplete, "Complete", "Complete", 84);
                Gujjar.AddDatagridviewButton(dgv, btndgvprint, "Report", "Report", 84);
                Gujjar.AddDatagridviewButton(dgv, btndgvedit, "Re-sch", "Re-sch", 84);
                Gujjar.AddDatagridviewButton(dgv, btndgvcancel, "Cancel", "Cancel", 84);

                WaitForm form = new WaitForm(LoadData);
                form.ShowDialog();

                label10.Text = oilDealVM1BindingSource.List.Count.ToString();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private OilDealVM1 GetVm(OilDeal item)
        {

            using (Context db = new Context())
            {
                OilDealVM1 vm = new OilDealVM1
                {
                    Broker = item.Broker.Name,
                    BrokerShareAmount = item.BrokerShareAmount.ToString("n4"),
                    BrokerSharePercentage = item.BrokerSharePercentage.ToString("n2"),
                    CompleteDate = item.CompleteDate == null ? "N/A" : item.CompleteDate.Value.ToShortDateString(),
                    DealDate = item.DealDate.ToShortDateString(),
                    DealQty = item.DealQty.ToString("n2"),
                    Id = item.Id,
                    Item = item.Item.Title,
                    NetPrice = item.NetPrice.ToString("n2"),
                    PerTradeUnitPrice = item.PerTradeUnitPrice.ToString("n2"),
                    PerTradeUnitQty = "0",
                    Driver = "N/A",
                    ReadyDate = item.ReadyDate.ToShortDateString(),
                    Selector = "N/A",
                    Status = item.Status.ToString(),
                    TotalPrice = item.TotalPrice.ToString("n2"),
                    TotalTradeUnits = item.TotalTradeUnits.ToString("n2"),
                    TradeUnit = "N/A",
                    VehicleEmptyWeight = item.VehicleEmptyWeight.ToString("n2"),
                    VehicleFullWeight = item.VehicleFullWeight.ToString("n2"),
                    VehicleNo = item.VehicleNo,
                    VehicleWeightDifference = item.VehicleWeightDifference.ToString("n2"),
                    WeighBridgeWeight = item.WeighBridgeWeight.ToString("n2"),
                    VehicleLoadedQty = item.VehicleScheduleQty.ToString("n2"),
                    
                };
                if (item.OilDealDriverId != null)
                {
                    item.Driver = db.OilDealDrivers.Find(item.OilDealDriverId);
                    vm.Driver = item.Driver.Name;
                }
                if (item.OilDealSelectorId != null)
                {
                    item.Selector = db.OilDealSelectors.Find(item.OilDealSelectorId);
                    vm.Selector = item.Selector.Name;
                }
                if (item.TradeUnit == null)
                {
                    item.TradeUnit = db.OilTradeUnits.Find(item.OilTradeUnitId);
                    vm.TradeUnit = item.TradeUnit.Title;
                }
                return vm;
            }

        }
        private void LoadData()
        {
            try
            {
                using (Context db = new Context())
                {
                    dealList = db.OilDeals.Include(a => a.Broker)
                        .Include(a => a.Item).ToList()
                        .Where(a => { DateTime dt = a.DealDate.Date; return dt >= appSett.StartDate.Date && dt <= appSett.EndDate.Date; })
                        .OrderByDescending(a => a.Id).ToList();
                    int index = 0;
                    foreach (var item in dealList)
                    {
                        var vm = GetVm(item);

                        if (dgv.InvokeRequired)
                        {
                            dgv.Invoke(new Action(() => { oilDealVM1BindingSource.List.Add(vm); }));
                        }
                        else
                        {
                            oilDealVM1BindingSource.List.Add(vm);
                        }

                        if(item.Status == OilDealStatus.Cancelled)
                        {
                            dgv.Rows[index].DefaultCellStyle.BackColor = Color.Pink;
                        }
                        if(item.Status == OilDealStatus.Completed)
                        {
                            dgv.Rows[index].DefaultCellStyle.BackColor = Color.PaleGreen;
                        }
                        index++;
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int ri = e.RowIndex;
            int ci = e.ColumnIndex;
            try
            {
                if (ri == -1 || ri == dgv.NewRowIndex)
                {
                    return;
                }

                #region "Process"
                if (dgv.Columns[btndgvprocess].Index == ci)
                {
                    
                    int vid = dgv.Rows[ri].Cells[0].Value.ToString().ToInt();
                    var vm = oilDealVM1BindingSource.List.OfType<OilDealVM1>().FirstOrDefault(a => a.Id == vid);
                    if (vm.Status == OilDealStatus.Cancelled.ToString())
                    {
                        throw new Exception("Deal is cancelled");
                    }
                    if (vm.Status != OilDealStatus.Scheduled.ToString())
                    {
                        throw new Exception("Only scheduled deals are allowed to be processed");
                    }
                    if (!Helper.ConfirmUserPassword())
                    {
                        return;
                    }

                    DialogResult rest = Gujjar.ConfirmYesNo("Are you confirmed..!!");
                    if (rest == DialogResult.No)
                        return;
                    OilDealProcessForm form = new OilDealProcessForm(vm);
                    form.ShowDialog();
                    if (form.IsDone)
                    {
                        oilDealVM1BindingSource.List.Remove(vm);
                        using (Context db = new Context())
                        {
                            var deal = db.OilDeals.Find(vid);
                            deal.Broker = db.OilDealBrokers.Find(deal.OilDealBrokerId);
                            deal.Selector = db.OilDealSelectors.Find(deal.OilDealSelectorId);
                            deal.Driver = db.OilDealDrivers.Find(deal.OilDealDriverId);
                            deal.Item = db.OilDealItems.Find(deal.OilDealItemId);

                            var vm2 = GetVm(deal);
                            oilDealVM1BindingSource.List.Add(vm2);
                            dgv.Refresh();
                        }
                    }
                }

                #endregion

                #region "Complete"
                if (dgv.Columns[btndgvcomplete].Index == ci)
                {
                    
                    int vid = dgv.Rows[ri].Cells[0].Value.ToString().ToInt();
                    var vm = oilDealVM1BindingSource.List.OfType<OilDealVM1>().FirstOrDefault(a => a.Id == vid);
                    if (vm.Status == OilDealStatus.Cancelled.ToString())
                    {
                        throw new Exception("Deal is cancelled");
                    }
                    if (vm.Status != OilDealStatus.Departed.ToString())
                    {
                        throw new Exception("Only departed deals are allowed to be processed");
                    }
                    if (!Helper.ConfirmUserPassword())
                    {
                        return;
                    }
                    DialogResult rest = Gujjar.ConfirmYesNo("Please confirm .. !!");
                    if (rest == DialogResult.No)
                        return;

                    OilDealCompleteForm form = new OilDealCompleteForm(vm);
                    form.ShowDialog();
                    if (form.IsDone)
                    {
                        oilDealVM1BindingSource.List.Remove(vm);
                        using (Context db = new Context())
                        {
                            var deal = db.OilDeals.Find(vid);
                            deal.Broker = db.OilDealBrokers.Find(deal.OilDealBrokerId);
                            deal.Selector = db.OilDealSelectors.Find(deal.OilDealSelectorId);
                            deal.Driver = db.OilDealDrivers.Find(deal.OilDealDriverId);
                            deal.Item = db.OilDealItems.Find(deal.OilDealItemId);

                            var vm2 = GetVm(deal);
                            oilDealVM1BindingSource.List.Add(vm2);
                            dgv.Refresh();
                        }
                    }
                }
                #endregion

                #region "Print"
                if (dgv.Columns[btndgvprint].Index == ci)
                {
                    int vid = dgv.Rows[ri].Cells[0].Value.ToString().ToInt();
                    rdealId = vid;
                    var vm = oilDealVM1BindingSource.List.OfType<OilDealVM1>().FirstOrDefault(a => a.Id == vid);
                    if (vm.Status == OilDealStatus.Cancelled.ToString())
                    {
                        throw new Exception("Deal is cancelled");
                    }
                    if (vm.Status != OilDealStatus.Completed.ToString())
                    {
                        throw new Exception("Only completed deals are allowed to be printed");
                    }

                    DialogResult res = Gujjar.ConfirmYesNo("Please confirm..!!");
                    if (res == DialogResult.No)
                        return;

                    isRepLoaded = false;
                    WaitForm wait1 = new WaitForm(LoadRepData);
                    wait1.ShowDialog();
                    if (isRepLoaded)
                    {
                        crudeOilReport.ShowPrintMarginsWarning = false;
                        crudeOilReport.ShowPrintStatusDialog = false;
                        var lengendsParam = crudeOilReport.Parameters["Legends"];
                        lengendsParam.Visible = false;
                        lengendsParam.Value = Helper.Legends();
                        crudeOilReport.ShowPreview();
                    }
                }
                #endregion
                #region "Re-schedule"
                if (dgv.Columns[btndgvedit].Index == ci)
                {
                    try
                    {
                        int did = dgv.Rows[ri].Cells[0].Value.ToInt();

                        var bObj = oilDealVM1BindingSource.List.OfType<OilDealVM1>()
                            .FirstOrDefault(a => a.Id == did);
                        if (bObj.Status == OilDealStatus.Cancelled.ToString())
                        {
                            throw new Exception("Deal is cancelled");
                        }
                        if (bObj.Status == OilDealStatus.Scheduled.ToString())
                        {
                            throw new Exception("Already scheduled");
                        }

                        if (!Helper.ConfirmAdminPassword())
                        {
                            return;
                        }
                        DialogResult res = Gujjar.ConfirmYesNo("Are you sure, before to re-schedule deal. All concerned financial entries will be rolled back");
                        if (res == DialogResult.No)
                            return;

                        using (Context db = new Context())
                        {
                            List<DayBook> daybooks = db.DayBooks.Where(a => a.OilDealId == did && !a.IsReversed).ToList();

                            var oilDeal = db.OilDeals.Find(did);
                            oilDeal.Status = OilDealStatus.Scheduled;
                            oilDeal.CompleteDate = null;
                            oilDeal.WeighBridgeId = null;

                            db.Entry(oilDeal).State = EntityState.Modified;
                            db.SaveChanges();

                            foreach (var item in daybooks)
                            {
                                Helper.ReverseTransaction(item);
                            }

                            Gujjar.InfoMsg("Deal schedule is re-sheduled successfully and financial entries has been reversed.");
                            oilDealVM1BindingSource.List.Clear();
                            WaitForm form = new WaitForm(LoadData);
                            form.ShowDialog();

                            label10.Text = oilDealVM1BindingSource.List.Count.ToString();
                        }
                    }
                    catch (Exception exp)
                    {
                        Gujjar.ErrMsg(exp);
                    }
                }
                #endregion

                #region "Cancel"
                if (dgv.Columns[btndgvcancel].Index == ci)
                {
                    try
                    {
                        int did = dgv.Rows[ri].Cells[0].Value.ToInt();

                        var bObj = oilDealVM1BindingSource.List.OfType<OilDealVM1>()
                            .FirstOrDefault(a => a.Id == did);

                        if (bObj.Status == OilDealStatus.Cancelled.ToString())
                        {
                            throw new Exception("Deal is cancelled");
                        }

                        if(bObj.Status != OilDealStatus.Scheduled.ToString())
                        {
                            throw new Exception("Only scheduled deals are allowed to be cancelled");
                        }

                        if (!Helper.ConfirmAdminPassword())
                        {
                            return;
                        }
                        DialogResult res = Gujjar.ConfirmYesNo("Are you sure, before to cancel deal. All concerned financial entries will be rolled back");
                        if (res == DialogResult.No)
                            return;

                        using (Context db = new Context())
                        {
                            List<DayBook> daybooks = db.DayBooks.Where(a => a.OilDealId == did).ToList();

                            var oilDeal = db.OilDeals.Find(did);
                            oilDeal.Status = OilDealStatus.Cancelled;
                            oilDeal.CompleteDate = null;
                            oilDeal.WeighBridgeId = null;
                            oilDeal.OilDealSelectorId = null;
                            oilDeal.OilDealDriverId = null;
                            oilDeal.TotalPrice = 0;

                            db.Entry(oilDeal).State = EntityState.Modified;
                            db.SaveChanges();

                            foreach (var item in daybooks.Where(a => !a.IsReversed))
                            {
                                Helper.ReverseTransaction(item);
                            }
                            foreach (var item in daybooks)
                            {
                                item.IsReversed = true;
                                item.OilDealId = null;
                                db.Entry(item).State = EntityState.Modified;
                            }
                            db.SaveChanges();

                            Gujjar.InfoMsg("Deal schedule is cancelled successfully and financial entries has been reversed.");
                            oilDealVM1BindingSource.List.Clear();
                            WaitForm form = new WaitForm(LoadData);
                            form.ShowDialog();
                            label10.Text = oilDealVM1BindingSource.List.Count.ToString();
                        }
                    }
                    catch (Exception exp)
                    {
                        Gujjar.ErrMsg(exp);
                    }
                }
                #endregion
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }

        }
        WholesaleCrudeOilReport crudeOilReport = null;
        List<RCOCompRVM> rcocompVMList = null;
        List<RCOSchRVM> rcoschVMList = null;
        private bool isRepLoaded = false;
        private int rdealId = 0;
        private void LoadRepData()
        {
            try
            {
                using (Context db = new Context())
                {
                    AppSettings appSett = db.AppSettings.First();

                    RCOCompRVM rcovm = new RCOCompRVM
                    {
                        Address = appSett.Address,
                        Barcode = null,
                        Logo = appSett.Logo,
                        Name = appSett.Name,
                        Phone = appSett.PhoneNo,
                        Qrcode = null,
                        RepDate = DateTime.Now.ToString(),
                        RepTitle = "Cruide Oil Whole Sale Report"
                    };
                    Code128BarcodeDraw draw = BarcodeDrawFactory.Code128WithChecksum;
                    var image1 = draw.Draw(Helper.Unum, 50);
                    rcovm.Barcode = Gujjar.GetByteArrayFromImage(image1);

                    CodeQrBarcodeDraw draw2 = BarcodeDrawFactory.CodeQr;
                    var image2 = draw2.Draw("GBS Solutions, Burewala. 0333-9372084", 50);
                    rcovm.Qrcode = Gujjar.GetByteArrayFromImage(image2);

                    if (rcocompVMList != null)
                    {
                        rcocompVMList.Clear();
                        rcocompVMList = null;
                    }
                    if (rcoschVMList != null)
                    {
                        rcoschVMList.Clear();
                        rcoschVMList = null;
                    }
                    rcoschVMList = new List<RCOSchRVM>();
                    rcocompVMList = new List<RCOCompRVM>();
                    rcocompVMList.Add(rcovm);

                    OilDeal oilDeal = db.OilDeals.Find(rdealId);
                    oilDeal.Broker = db.OilDealBrokers.Find(oilDeal.OilDealBrokerId);
                    if (oilDeal.OilDealDriverId != null)
                    {
                        oilDeal.Driver = db.OilDealDrivers.Find(oilDeal.OilDealDriverId.Value);
                    }
                    else
                    {
                        oilDeal.Driver = null;
                    }

                    if (oilDeal.OilDealSelectorId != null)
                    {
                        oilDeal.Selector = db.OilDealSelectors.Find(oilDeal.OilDealSelectorId.Value);
                    }
                    if (oilDeal.WeighBridgeId != null)
                    {
                        oilDeal.WeighBridge = db.WeighBridges.Find(oilDeal.WeighBridgeId.Value);
                    }

                    oilDeal.TradeUnit = db.OilTradeUnits.Find(oilDeal.OilTradeUnitId);


                    RCOSchRVM rcoSchvm = new RCOSchRVM
                    {
                        BrokerShareAmount = oilDeal.BrokerShareAmount,
                        Broker = oilDeal.Broker.Name,
                        DriverBioMet = null,
                        DriverPic = null,
                        BrokerSharePercentage = oilDeal.BrokerSharePercentage,
                        Driver = "N/A",
                        LoadedQty = oilDeal.DealQty,
                        NetPrice = oilDeal.NetPrice,
                        PerTradeUnit = oilDeal.TradeUnit.UnitQty,
                        PerTURate = oilDeal.PerTradeUnitPrice,
                        TradeUnit = oilDeal.TradeUnit.Title,
                        SchNo = oilDeal.Id.ToString(),
                        Selector = "N/A",
                        SelectorBioMet = null,
                        SelectorPic = null,
                        TotalPrice = oilDeal.TotalPrice,
                        TotalTUs = oilDeal.TotalTradeUnits,
                        Vehicle = oilDeal.VehicleNo,
                        VehicleEmptyWeight = oilDeal.VehicleEmptyWeight,
                        WeighBridge = "N/A",
                        WeighBridgeWeight = oilDeal.WeighBridgeWeight,
                        Dated = oilDeal.CompleteDate.Value.ToString(),
                        ServedBy = oilDeal.AppUserId,
                        DriverNIC = "N/A",
                        SelectorNIC = "N/A"
                    };

                    Image picImg = Properties.Resources.atomix_user31;
                    Image thumbImg = Properties.Resources.fingerprint_reader;
                    byte[] PicData = Gujjar.GetByteArrayFromImage(picImg);
                    byte[] ThumbData = Gujjar.GetByteArrayFromImage(thumbImg);
                    if (oilDeal.Selector != null)
                    {
                        rcoSchvm.Selector = oilDeal.Selector.Name;
                        rcoSchvm.SelectorNIC = oilDeal.Selector.CNIC;
                        if(oilDeal.Selector.PicData != null)
                        {
                            rcoSchvm.SelectorPic = oilDeal.Selector.PicData;                           
                        }
                        else
                        {
                            rcoSchvm.SelectorPic = PicData;
                        }
                        if(oilDeal.Selector.ThumbPicData != null)
                        {
                            rcoSchvm.SelectorBioMet = oilDeal.Selector.ThumbPicData;
                        }
                        else
                        {
                            rcoSchvm.SelectorBioMet = ThumbData;
                        }
                    }
                    else
                    {
                        rcoSchvm.SelectorPic = PicData;
                        rcoSchvm.SelectorBioMet = ThumbData;
                    }
                    if(oilDeal.Driver != null)
                    {
                        rcoSchvm.Driver = oilDeal.Driver.Name;
                        rcoSchvm.DriverNIC = oilDeal.Driver.CNIC;
                        if (oilDeal.Driver.PicData != null)
                        {
                            rcoSchvm.DriverPic = oilDeal.Driver.PicData;
                        }
                        else
                        {
                            rcoSchvm.DriverPic = PicData;
                        }
                        if (oilDeal.Driver.ThumbPicData != null)
                        {
                            rcoSchvm.DriverBioMet = oilDeal.Driver.ThumbPicData;
                        }
                        else
                        {
                            rcoSchvm.DriverBioMet = ThumbData;
                        }
                    }
                    else
                    {
                        rcoSchvm.DriverPic = PicData;
                        rcoSchvm.DriverBioMet = ThumbData;
                    }

                    if(oilDeal.WeighBridge != null)
                    {
                        rcoSchvm.WeighBridge = oilDeal.WeighBridge.Name;
                    }


                    rcoschVMList.Add(rcoSchvm);

                    crudeOilReport = new WholesaleCrudeOilReport();

                    DetailReportBand compBand = crudeOilReport.Bands["DetailReport"] as DetailReportBand;
                    DetailReportBand schBand = crudeOilReport.Bands["DetailReport1"] as DetailReportBand;
                    compBand.DataSource = rcocompVMList;
                    schBand.DataSource = rcoschVMList;
                    isRepLoaded = true;
                }
            }
            catch (Exception exp)
            {
                isRepLoaded = false;
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
