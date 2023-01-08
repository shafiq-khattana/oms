using DevExpress.XtraReports.UI;
using Khattana.Common;
using Model.ReadyStuff.Model;
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
using WinFom.Common.Model;
using WinFom.ReadyStuff.Report;
using WinFom.ReadyStuff.ReportViewModel;
using Zen.Barcode;

namespace WinFom.ReadyStuff.Forms
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                List<PackingRVN> packingList = new List<PackingRVN>();
                List<TradeBroker> tradeBrokerList = new List<TradeBroker>();
                using (Context db = new Context())
                {
                    var sett = db.AppSettings.First();
                    ReadySchedule schedule = db.ReadySchedules.Find(24);
                    //schedule.TradeUnit = db.ReadyTradeUnits.Find(schedule.ReadyTradeUnitId);
                    schedule.WeighBridge = db.WeighBridges.Find(schedule.WeighBridgeId);
                    ReadyDeal deal = db.ReadyDeals.Find(schedule.ReadyDealId);
                    ReadyBroker broker = db.ReadyBrokers.Find(deal.ReadyBrokerId);
                    ReadyItem readyItem = db.ReadyItems.Find(deal.ReadyItemId);
                    var packings = db.Bharthies.Where(a => a.ReadyScheduleId == schedule.Id).ToList();
                    List<TopHeader> topHeaderList = new List<TopHeader>();
                    TopHeader header = new TopHeader
                    {
                        CompanyAddress = sett.Address,
                        CompanyName = sett.Name,
                        CompanyPhone = sett.PhoneNo,
                        Logo = sett.Logo,
                        Dated = DateTime.Now.ToString(),
                        Item = readyItem.Title
                    };

                    
                    TradeBroker tradeBroker = new TradeBroker
                    {
                        BrokerShareAmount = schedule.BrokerShareAmount,
                        BrokerSharePercentage = schedule.BrokerSharePercentage,
                        NetPrice = schedule.NetScheduleAmount,
                        PerTradeUnitPrice = schedule.PerTradeUnitPrice,
                        TotalTradeUnits = schedule.TotalTradeUnits,
                        //TradeUnitName = schedule.TradeUnit.Title,
                        //TradeUnitQty = schedule.TradeUnit.UnitQty.ToString(),
                        WeighBridge = schedule.WeighBridge.Name,
                        WeighBridgeWeight = schedule.WeighBridgeWeight, 
                        
                    };
                    tradeBrokerList.Add(tradeBroker);
                    
                    foreach (var item in packings)
                    {
                        item.BharthiType = db.BharthiTypes.Find(item.BharthiTypeId);

                        PackingRVN pack = new PackingRVN
                        {
                            Packing = string.Format("{0}-({1})", item.BharthiType.Title, item.BharthiType.UnitQty.ToString("n1")),
                            Qty = item.Qty,
                            Total = item.Total
                        };
                        packingList.Add(pack);
                    }

                    header.Broker = broker.Name;
                    byte[] driverPic = null;
                    byte[] driverThumb = null;
                    byte[] selectorPic = null;
                    byte[] selectorThum = null;
                    string driverName = "N/A";
                    string selectorName = "N/A";
                    string dealSchedule = string.Format("Deal No. {0}, Schedule No. {1}", deal.Id, schedule.ScheduleNo);
                    if(schedule.ReadyDriverId != null)
                    {
                        var drv = db.ReadyDrivers.Find(schedule.ReadyDriverId);
                        if(drv.PicData != null)
                        {
                            driverPic = drv.PicData;
                        }
                        if(drv.ThumbPicData != null)
                        {
                            driverThumb = drv.ThumbPicData;
                        }
                        driverName = drv.Name;
                    }
                    if(schedule.ReadySelectorId != null)
                    {
                        var slc = db.ReadySelectors.Find(schedule.ReadySelectorId);
                        if(slc.PicData != null)
                        {
                            selectorPic = slc.PicData;
                            
                        }
                        if(slc.ThumbPicData != null)
                        {
                            selectorThum = slc.ThumbPicData;
                        }
                        selectorName = slc.Name;
                    }
                    Image picImg = Properties.Resources.atomix_user31;
                    Image thumbImg = Properties.Resources.Fingerprint_96px;
                    if(driverPic == null)
                    {
                        driverPic = Gujjar.GetByteArrayFromImage(picImg);
                    }
                    if(driverThumb == null)
                    {
                        driverThumb = Gujjar.GetByteArrayFromImage(thumbImg);
                    }
                    if(selectorPic == null)
                    {
                        selectorPic = Gujjar.GetByteArrayFromImage(picImg);
                    }
                    if(selectorThum == null)
                    {
                        selectorThum = Gujjar.GetByteArrayFromImage(thumbImg);
                    }
                    Persons persons = new Persons
                    {
                        Dated = schedule.DispatchedDate.ToString(),
                        DealSchedule = dealSchedule,
                        DriverName = driverName,
                        DriverPic = driverPic,
                        DriverThumb = driverThumb,
                        SelectorName = selectorName,
                        SelectorPic = selectorPic,
                        SelectorThumb = selectorThum
                    };

                    List<Persons> personsList = new List<Persons>();
                    personsList.Add(persons);
                    
                    Code128BarcodeDraw draw = BarcodeDrawFactory.Code128WithChecksum;
                    var image1 = draw.Draw(Helper.Unum, 50);
                    header.Barcode = Gujjar.GetByteArrayFromImage(image1);

                    CodeQrBarcodeDraw draw2 = BarcodeDrawFactory.CodeQr;
                    var image2 = draw2.Draw("GBS Solutions, Burewala. 0333-9372084", 50);
                    header.QrCode = Gujjar.GetByteArrayFromImage(image2);

                    topHeaderList.Add(header);
                    SCSReport rep = new SCSReport();

                    DetailReportBand topBand = rep.Bands["DetailReport"] as DetailReportBand;
                    topBand.DataSource = topHeaderList;
                    DetailReportBand pBand = rep.Bands["DetailReport1"] as DetailReportBand;
                    pBand.DataSource = personsList;
                    DetailReportBand packBand = rep.Bands["DetailReport2"] as DetailReportBand;
                    packBand.DataSource = packingList;
                    DetailReportBand bBand = rep.Bands["DetailReport3"] as DetailReportBand;
                    bBand.DataSource = tradeBrokerList;

                    var param = rep.Parameters["appUser"];
                    param.Visible = false;
                    param.Value = "admin1";
                    rep.ShowPrintMarginsWarning = false;
                    rep.ShowPrintStatusDialog = false;

                    rep.ShowPreview();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
