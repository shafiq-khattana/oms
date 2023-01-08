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
using WinFom.ReadyStuff.Forms;

namespace WinFom.OilDealManaged.Forms
{
    public partial class AddOilDealForm : Form
    {
        private List<OilDealBroker> brokerList = null;
        private OilDealBroker broker = null;
        private List<OilDealItem> itemList = null;
        private OilDealItem dealItem = null;
        private AppSettings appSett = Helper.AppSet;
        private List<OilTradeUnit> tradeUnits = null;
        OilTradeUnit tradeUnit = null;
        private AppUser appUser = SingleTon.LoginForm.appUser;
        public AddOilDealForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadOilBrokers()
        {
            try
            {
                using (Context db = new Context())
                {
                    brokerList = db.OilDealBrokers.Where(a => a.IsActive).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindCbOilBrokers()
        {
            cbBrokers.DataSource = brokerList;
            cbBrokers.DisplayMember = "Name";
            cbBrokers.ValueMember = "Id";
        }
        private void LoadDealItems()
        {
            try
            {
                using (Context db = new Context())
                {
                    itemList = db.OilDealItems.ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindCbDealItems()
        {
            cbDealItems.DataSource = itemList;
            cbDealItems.DisplayMember = "Title";
            cbDealItems.ValueMember = "Id";
        }
        private void LoadBoth()
        {
            LoadDealItems();
            LoadOilBrokers();
            LoadTradeUnits();
        }
        private void BindBoth()
        {
            BindCbDealItems();
            BindCbOilBrokers();
            BindCBTradeUnits();
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                Helper.IsOkApplied();
                WaitForm wait = new WaitForm(LoadBoth);
                wait.ShowDialog();

                BindBoth();

                Gujjar.NumbersOnly(tbDealQty);
                Gujjar.NumbersOnly(tbReadyAfterDays);
                Gujjar.TB4(pMain);

                dtpDealDate.MinDate = dtpReadyDate.MinDate = appSett.StartDate;
                dtpReadyDate.MaxDate = dtpDealDate.MaxDate = appSett.EndDate;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddBroker_Click(object sender, EventArgs e)
        {
            try
            {
                AddOilBrokerForm form = new AddOilBrokerForm();
                form.ShowDialog();

                int id = form.BrokerId;
                if (id != 0)
                {
                    LoadOilBrokers();
                    BindCbOilBrokers();

                    cbBrokers.SelectedItem = cbBrokers.Items.OfType<OilDealBroker>().FirstOrDefault(a => a.Id == id);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddDealItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddOilDealItemForm form = new AddOilDealItemForm();
                form.ShowDialog();

                int did = form.ItemId;
                if (did != 0)
                {
                    LoadDealItems();
                    BindCbDealItems();

                    cbDealItems.SelectedItem = cbDealItems.Items.OfType<OilDealItem>()
                        .FirstOrDefault(a => a.Id == did);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void cbBrokers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBrokers.SelectedIndex == -1)
            {
                broker = null;
                return;
            }
            broker = cbBrokers.SelectedItem as OilDealBroker;
        }

        private void cbDealItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDealItems.SelectedIndex == -1)
            {
                dealItem = null;
                return;
            }
            dealItem = cbDealItems.SelectedItem as OilDealItem;
        }

        private void tbReadyAfterDays_TextChanged(object sender, EventArgs e)
        {
            string txt = tbReadyAfterDays.Text;
            if (string.IsNullOrEmpty(txt))
            {
                dtpReadyDate.Value = dtpDealDate.Value;
            }
            else
            {
                int days = txt.ToInt();
                dtpReadyDate.Value = dtpDealDate.Value.AddDays(days);
            }
        }

        private void dtpDealDate_ValueChanged(object sender, EventArgs e)
        {
            //SetDays();
        }
        private void SetDays()
        {
            int days = (dtpReadyDate.Value - dtpDealDate.Value).Days;
            tbReadyAfterDays.Text = days.ToString();
        }

        private void dtpReadyDate_ValueChanged(object sender, EventArgs e)
        {
            //SetDays();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }
                if (broker == null || dealItem == null)
                {
                    throw new Exception("Please select broker and deal item");
                }
                if(tradeUnit == null || tradeUnit.Title == "N/A")
                {
                    throw new Exception("Trade unit is not set.");
                }
                OilDeal oilDeal = new OilDeal
                {
                    BrokerShareAmount = 0,
                    Broker = null,
                    BrokerSharePercentage = 0,
                    CompleteDate = null,
                    DealDate = dtpDealDate.Value,
                    DealQty = tbDealQty.Text.ToDecimal(),
                    Driver = null,
                    Item = null,
                    Id = 0,
                    NetPrice = 0,
                    OilDealBrokerId = broker.Id,
                    OilDealDriverId = null,
                    OilDealItemId = dealItem.Id,
                    OilDealSelectorId = null,
                    OilTradeUnitId = tradeUnit.Id,
                    PerTradeUnitPrice = tbPerTradeUnitPrice.Text.ToDecimal(),
                    ReadyDate = dtpReadyDate.Value.Date,
                    Selector = null,
                    Status = OilDealStatus.Scheduled,
                    TotalPrice = 0,
                    TotalTradeUnits = 0,
                    TradeUnit = null,
                    VehicleEmptyWeight = 0,
                    VehicleFullWeight = 0,
                    VehicleNo = "N/A",
                    WeighBridgeWeight = 0,
                    Unum = Helper.Unum,
                    VehicleScheduleQty = 0,
                    LaborExpensesDescription = "N/A",
                    LaborExpenses = 0,
                    AppUserId = appUser.Id
                };

                string confirmMessage = string.Format("Please confirm... Broker: {0}\nDeal Item: {1}\nDeal Qty: {2}\nDeal Date: {3}\nReady Date: {4}. \nAre you sure, the given information is correct.",
                    broker.Name, dealItem.Title, tbDealQty.Text, dtpDealDate.Value.ToShortDateString(), dtpReadyDate.Value.ToShortDateString());

                DialogResult res = Gujjar.ConfirmYesNo(confirmMessage);
                if (res == DialogResult.No)
                    return;

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            oilDeal = db.OilDeals.Add(oilDeal);
                            db.SaveChanges();

                            string alarmText = string.Format("Oil Deal No. {0}\nBroker: {1}\nDeal Item: {2}\nDeal Qty: {3}\nDeal Date: {4}\nReady Date: {5}.",
                                oilDeal.Id, broker.Name, dealItem.Title, tbDealQty.Text, dtpDealDate.Value.ToShortDateString(), dtpReadyDate.Value.ToShortDateString());

                            OilDealAlarm dealAlarm = new OilDealAlarm
                            {
                                AlarmText = alarmText,
                                EndDate = dtpReadyDate.Value.Date,
                                GenerateDate = DateTime.Now,
                                IsActive = true,
                                AlarmReadyDate = dtpReadyDate.Value.AddDays(-appSett.DaysAlertBeforeReady)
                            };

                            db.OilDealAlarms.Add(dealAlarm);
                            db.SaveChanges();

                            trans.Commit();

                            Gujjar.InfoMsg(string.Format("Deal No. {0} added in database successfully.", oilDeal.Id));
                            Close();
                        }
                        catch (Exception ep)
                        {
                            trans.Rollback();
                            throw ep;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void BindCBTradeUnits()
        {
            cbTradeUnits.DataSource = tradeUnits;
            cbTradeUnits.DisplayMember = "Title";
            cbTradeUnits.ValueMember = "Id";

            cbTradeUnits.SelectedItem = cbTradeUnits.Items.OfType<OilTradeUnit>()
                .FirstOrDefault(a => a.Title == "N/A");
        }
        private void LoadTradeUnits()
        {
            try
            {
                using (Context db = new Context())
                {
                    tradeUnits = db.OilTradeUnits.ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void btnAddBharthiType_Click(object sender, EventArgs e)
        {
            try
            {
                AddOilTradeUnit form = new AddOilTradeUnit();
                form.ShowDialog();
                int did = form.TradeUnitId;
                if (did != 0)
                {
                    WaitForm wait2 = new WaitForm(LoadTradeUnits);
                    wait2.ShowDialog();
                    BindCBTradeUnits();

                    cbTradeUnits.SelectedItem = cbTradeUnits.Items.OfType<OilTradeUnit>()
                        .FirstOrDefault(a => a.Id == did);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void cbTradeUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTradeUnits.SelectedIndex == -1)
            {
                return;
            }
            decimal dealQty = 0;
            string txt = tbDealQty.Text;
            if (!string.IsNullOrEmpty(txt))
            {
                dealQty = txt.ToDecimal();
            }

            tradeUnit = cbTradeUnits.SelectedItem as OilTradeUnit;
            tbPerTradeUnitQty.Text = tradeUnit.UnitQty.ToString("n4");
            //dealQty = txt.ToDecimal();
            if (tradeUnit.UnitQty == 0)
            {
                tradeUnit.UnitQty = 1;
            }
            decimal totalTradeUnits = dealQty / tradeUnit.UnitQty;
            tbTotalTradeUnits.Text = totalTradeUnits.ToString("n4");
        }

        private void tbPerTradeUnitPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string perTxt = tbPerTradeUnitPrice.Text;
                if(string.IsNullOrEmpty(perTxt))
                {
                    tbTotalPriceApprox.Text = "0";
                }

                decimal tradeUnits = 0;
                string txt = tbTotalTradeUnits.Text;
                if(!string.IsNullOrEmpty(txt))
                {
                    tradeUnits = txt.ToDecimal();
                }
                decimal perAmount = perTxt.ToDecimal();

                decimal total = perAmount * tradeUnits;
                tbTotalPriceApprox.Text = total.ToString("n4");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
