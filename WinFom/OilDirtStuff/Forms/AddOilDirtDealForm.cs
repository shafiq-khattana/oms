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
using Model.OilDirtStuff.Model;
using WinFom.OilDirtStuff.Forms;
using Model.ReadyStuff.ViewModel;
using WinFom.ReadyStuff.Forms;

namespace WinFom.OilDealManaged.Forms
{
    public partial class AddOilDirtDealForm : Form
    {
        private List<OilDirtTradeUnit> tradeUnits = null;
        private OilDirtTradeUnit tradeUnit = null;
        private List<OilDirtItem> oilDirtItems = null;
        private OilDirtItem oilDirtItem = null;
        private List<OilDirtBroker> brokers = null;
        private OilDirtBroker oilDirtBroker = null;
        private AppSettings AppSett = Helper.AppSet;
        private string dgvdeletebtn = "dgvdeletebtn";
        private AppUser appUser = SingleTon.LoginForm.appUser;
        public AddOilDirtDealForm()
        {
            InitializeComponent();
        }
        private void LoadBrokers()
        {
            try
            {
                brokers = null;
                using (Context db = new Context())
                {
                    brokers = db.OilDirtBrokers.Where(a => a.IsActive)
                        .OrderBy(a => a.Name).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void LoadOilDirtItems()
        {
            try
            {
                using (Context db = new Context())
                {
                    oilDirtItems = db.OilDirtItems.ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadOilDirtTradeUnits()
        {
            try
            {
                using (Context db = new Context())
                {
                    tradeUnits = db.OilDirtTradeUnits.ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void BindCbBrokers()
        {
            cbBrokers.DataSource = brokers;
            cbBrokers.DisplayMember = "Name";
            cbBrokers.ValueMember = "Id";

            cbBrokers.SelectedItem = cbBrokers.Items.OfType<OilDirtBroker>()
                .FirstOrDefault(a => a.Name == "N/A");
        }
        private void BindCbOilDirtItems()
        {
            cbDealItems.DataSource = oilDirtItems;
            cbDealItems.DisplayMember = "Title";
            cbDealItems.ValueMember = "Id";

            cbDealItems.SelectedItem = cbDealItems.Items.OfType<OilDirtItem>()
                .FirstOrDefault(a => a.Title == "Oil Dirt");
        }
        private void BindCbTradeUnits()
        {
            cbTradeUnits.DataSource = tradeUnits;
            cbTradeUnits.DisplayMember = "Title";
            cbTradeUnits.ValueMember = "Id";
            cbTradeUnits.SelectedItem = cbTradeUnits.Items.OfType<TradeUnit>()
                .FirstOrDefault(a => a.Title == "N/A");
        }

        private void LoadAllCombos()
        {
            LoadBrokers();
            LoadOilDirtTradeUnits();
            LoadOilDirtItems();
        }
        private void BindAllCombo()
        {
            BindCbBrokers();
            BindCbOilDirtItems();
            BindCbTradeUnits();
        }
        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait1 = new WaitForm(LoadAllCombos);
                wait1.ShowDialog();
                BindAllCombo();
                Helper.IsOkApplied();
                Gujjar.NumbersOnly(tbVehiclesCount);
                Gujjar.NumbersOnly(tbPerTradeUnitPrice);

                dtpDealDate.MinDate = dtpReadyDate.MinDate = AppSett.StartDate;
                dtpDealDate.MaxDate = dtpReadyDate.MaxDate = AppSett.EndDate;

                btnAdd.Enabled = false;

                Gujjar.AddDatagridviewButton(dgv, dgvdeletebtn, "Delete", "Delete", 80);
                Gujjar.TB4(pMain);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void pMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbTradeUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbTradeUnits.SelectedIndex == -1)
                    return;

                tradeUnit = cbTradeUnits.SelectedItem as OilDirtTradeUnit;
                string title = tradeUnit.Title;

                label11.Text = string.Format("Per {0} qty:", title);
                label7.Text = string.Format("Per {0} price:", title);
                tbPerTradeUnitQty.Text = tradeUnit.UnitQty.ToString("n4");
            }
            catch (Exception ex)
            {
                Gujjar.ErrMsg(ex);
            }
        }

        private void btnAddBroker_Click(object sender, EventArgs e)
        {
            try
            {
                AddOilDirtBrokerForm form = new AddOilDirtBrokerForm();
                form.ShowDialog();

                int did = form.BrokerId;
                if(did != 0)
                {
                    LoadBrokers();
                    BindCbBrokers();

                    cbBrokers.SelectedItem = cbBrokers.Items.OfType<OilDirtBroker>()
                        .FirstOrDefault(a => a.Id == did);
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

        private void btnAddDealItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddOilDirtItemForm form = new AddOilDirtItemForm();
                form.ShowDialog();

                int did = form.ItemId;
                if(did != 0)
                {
                    LoadOilDirtItems();
                    BindCbOilDirtItems();

                    cbDealItems.SelectedItem = cbDealItems.Items.OfType<OilDirtItem>()
                        .FirstOrDefault(a => a.Id == did);
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
                AddOilDirtTrageUnit form = new AddOilDirtTrageUnit();
                form.ShowDialog();

                int did = form.TradeUnitId;
                if(did != 0)
                {
                    LoadOilDirtTradeUnits();
                    BindCbTradeUnits();

                    cbTradeUnits.SelectedItem = cbTradeUnits.Items.OfType<OilDirtTradeUnit>()
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
                return;

            oilDirtBroker = cbBrokers.SelectedItem as OilDirtBroker;
        }

        private void cbDealItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDealItems.SelectedIndex == -1)
                return;

            oilDirtItem = cbDealItems.SelectedItem as OilDirtItem;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }

                if(tradeUnit == null || oilDirtBroker == null || oilDirtItem == null)
                {
                    throw new Exception("Please choose dropdown items");
                }
                if(cbBrokers.SelectedIndex == -1)
                {
                    throw new Exception("Please select broker");
                }
                if(cbDealItems.SelectedIndex == -1)
                {
                    throw new Exception("Please select deal item");
                }
                if(cbTradeUnits.SelectedIndex == -1)
                {
                    throw new Exception("Please select trade unit");
                }

                //DialogResult res = Gujjar.ConfirmYesNo("Please confirm")
                DialogResult res = Gujjar.ConfirmYesNo("Are you confirmed before to add deal?");
                if (res == DialogResult.No)
                    return;
                var schs = schVMBindingSource.List.OfType<SchVM>();
                
                using (Context db = new Context())
                {
                    var sett = db.AppSettings.First();
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            OilDirtDeal deal = new OilDirtDeal
                            {
                                CompletionStatus = string.Format("0/{0}", tbVehiclesCount.Text),
                                TradeUnit = null,
                                PerTradeUnitPrice = tbPerTradeUnitPrice.Text.ToDecimal(),
                                Broker = null,
                                Description = tbDescription.Text,
                                DateCompletion = null,
                                GenerateDate = DateTime.Now,
                                Status = OilDirtStatus.Scheduled,
                                Id = 0,
                                Item = null,
                                OilDirtBrokerId = oilDirtBroker.Id,
                                OilDirtItemId = oilDirtItem.Id,
                                OilDirtTradeUnitId = tradeUnit.Id,
                                ReadyDate = dtpReadyDate.Value,
                                Schedules = null,
                                NoOfVehicles = tbVehiclesCount.Text.ToInt(),
                                AppUserId = appUser.Id
                            };

                            deal = db.OilDirtDeals.Add(deal);
                            db.SaveChanges();

                            int num = 0;
                            foreach (var item in schs)
                            {
                                num++;
                                char ch = 'A';
                                for (int i = 0; i < item.Vehicles; i++)
                                {
                                    OilDirtSchedule oilDirtSchedule = new OilDirtSchedule
                                    {
                                        BrokerShareAmount = 0,
                                        ScheduleNo = string.Format("{0}-{1}/{2}", num, ch, item.Vehicles),
                                        BrokerSharePercentage = 0,
                                        PerTradeUnitPrice = deal.PerTradeUnitPrice,
                                        ReadyDate = Convert.ToDateTime(item.ReadyDate),
                                        OilDirtDriverId = null,
                                        ScheduleDate = DateTime.Now,
                                        WeighBridge = null,
                                        WeighBridgeId = null,
                                        Driver = null,
                                        WeighBridgeWeight = 0,
                                        VehicleNo = "N/A",
                                        Unum = Helper.Unum,
                                        Selector = null,
                                        TotalActualAmount = 0,
                                        Deal = null,
                                        DealQty = 0,
                                        Id = 0,
                                        OilDirtDealId = deal.Id,
                                        OilDirtSelectorId = null,
                                        TotalExpectedAmount = 0,
                                        TotalNetAmount = 0,
                                        VehicleWeightEmpty = 0,
                                        VehicleWeightFull = 0,
                                        Status = OilDirtScheduleStatus.Scheduled,
                                        CompleteDate = null,
                                        LoadedQty = 0,
                                        TotalTradeUnits = 0, LaborExpenses = 0, LaborExpensesDescription = "N/A"
                                    };
                                    ch++;
                                    oilDirtSchedule = db.OilDirtSchedules.Add(oilDirtSchedule);
                                    db.SaveChanges();

                                }

                                string msg = string.Format("Oil dirt Schedule Alarm. \nDeal No. {0}, Schedule No. {1}.\nBroker: {2}, Deal Item: {3}.\nSchedule time: {4}, ready time: {5}\nschedule for no of vehicles: {6}",
                                        deal.Id, num, oilDirtBroker.Name, oilDirtItem.Title, DateTime.Now.ToShortDateString(), item.ReadyDate, item.Vehicles);

                                OilDirtAlarm alarm = new OilDirtAlarm
                                {
                                    EndDate = Convert.ToDateTime(item.ReadyDate),
                                    ActiveDate = Convert.ToDateTime(item.ReadyDate).AddDays(-sett.DaysAlertBeforeReady),
                                    GenerateDate = DateTime.Now,
                                    IsActive = true,
                                    Description = msg
                                };
                                db.OilDirtAlarms.Add(alarm);
                            }

                            db.SaveChanges();
                            trans.Commit();

                            Gujjar.InfoMsg(string.Format("Deal No. {0} added in database successfully", deal.Id));
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

        private void tbReadyAfterDays_TextChanged(object sender, EventArgs e)
        {
            string txt = tbReadyAfterDays.Text;
            int days = 0;
            if(!string.IsNullOrEmpty(txt))
            {
                days = txt.ToInt();
            }

            dtpReadyDate.Value = dtpDealDate.Value.AddDays(days);
        }

        private void btnAddSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbBrokers.SelectedIndex == -1 || cbDealItems.SelectedIndex == -1)
                {
                    throw new Exception("Please choose broker and item");
                }
                if (cbDealItems.Text == "N/A" || cbBrokers.Text == "N/A")
                {
                    throw new Exception("Please choose broker and item.");
                }

                string vehCount = tbVehiclesCount.Text;
                if (string.IsNullOrEmpty(vehCount))
                {
                    throw new Exception("Please enter number of vehicles");
                }

                int vehCount2 = vehCount.ToInt();
                if (vehCount2 == 0)
                {
                    throw new Exception("Please enter number of vehicles at least 1");
                }

                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }

                if (schVMBindingSource.List.Count == 0)
                {
                    int total = tbVehiclesCount.Text.ToInt();
                    int remain = total;
                    int done = 0;

                    AddScheduleForm form4 = new AddScheduleForm(total, done, remain, dtpDealDate.Value);
                    form4.ShowDialog();
                    if (form4.vm != null)
                    {
                        schVMBindingSource.Add(form4.vm);
                        dgv.Refresh();
                    }

                    int done2 = schVMBindingSource.List.OfType<SchVM>().Sum(a => a.Vehicles);
                    int rem = total - done2;
                    tbRemainingVehiclesToSchedule.Text = rem.ToString();
                }
                else
                {
                    int total = tbVehiclesCount.Text.ToInt();
                    int done = schVMBindingSource.List.OfType<SchVM>().Sum(a => a.Vehicles);
                    int rem = total - done;
                    if (rem == 0)
                    {
                        throw new Exception("Schedules are completed");
                    }
                    DateTime lastDate = schVMBindingSource.List.OfType<SchVM>().Select(a => Convert.ToDateTime(a.ReadyDate)).ToList().OrderByDescending(a => a).FirstOrDefault();
                    AddScheduleForm form = new AddScheduleForm(total, done, rem, lastDate);
                    form.ShowDialog();

                    var obj = form.vm;
                    if (obj != null)
                    {
                        schVMBindingSource.List.Add(obj);
                        dgv.Refresh();
                    }
                    int done2 = schVMBindingSource.List.OfType<SchVM>().Sum(a => a.Vehicles);
                    rem = total - done2;
                    tbRemainingVehiclesToSchedule.Text = rem.ToString();
                }

                var list = schVMBindingSource.List.OfType<SchVM>();
                if (list.Count() > 0)
                {
                    var total2 = list.Sum(a => a.Vehicles);
                    int totalStr = tbVehiclesCount.Text.ToInt();

                    if (total2 == totalStr)
                    {
                        btnAdd.Enabled = true;
                    }
                }

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbVehiclesCount_TextChanged(object sender, EventArgs e)
        {
            tbVehiclesToScheduled.Text = tbRemainingVehiclesToSchedule.Text =  tbVehiclesCount.Text;
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 || e.RowIndex == dgv.NewRowIndex)
                {
                    return;
                }
                int ri = e.RowIndex;
                if (dgv.Rows[ri].Cells[dgvdeletebtn].ColumnIndex == e.ColumnIndex)
                {
                    DialogResult res = Gujjar.ConfirmYesNo("Are you sure to delete this record");
                    if (res == DialogResult.No)
                        return;

                    string rid = dgv.Rows[ri].Cells[0].Value.ToString();
                    var obj = schVMBindingSource.List.OfType<SchVM>().FirstOrDefault(a => a.Id == rid);

                    schVMBindingSource.List.Remove(obj);
                    dgv.Refresh();

                    UpdateRemaining();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void UpdateRemaining()
        {
            int total = tbVehiclesCount.Text.ToInt();

            int done = 0;
            done = schVMBindingSource.List.OfType<SchVM>().Sum(a => a.Vehicles);
            int rem = total - done;
            tbRemainingVehiclesToSchedule.Text = rem.ToString();
        }
    }
}
