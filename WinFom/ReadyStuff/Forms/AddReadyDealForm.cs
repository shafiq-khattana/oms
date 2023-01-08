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
using Model.ReadyStuff.ViewModel;
using WinFom.Common.Model;
using Model.Admin.Model;

namespace WinFom.ReadyStuff.Forms
{
    public partial class AddReadyDealForm : Form
    {
        private List<ReadyBroker> brokers = null;
        private List<ReadyItem> readyItems = null;
        private AppSettings appSettings = Helper.AppSet;
        private ReadyTradeUnit tradeUnit = null;
        private List<ReadyTradeUnit> tradeUnits = null;
        private string dgvdeletebtn = "dgvdeletebtn";
        private AppUser appUser = SingleTon.LoginForm.appUser;
        public AddReadyDealForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadTradeUnits()
        {
            try
            {
                using (Context db = new Context())
                {
                    tradeUnits = db.ReadyTradeUnits.ToList();
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
        }
        private void LoadBrokers()
        {
            try
            {
                using (Context db = new Context())
                {
                    brokers = db.ReadyBrokers.Where(a => a.IsActive).ToList();
                }
            }
            catch (Exception ep)
            {
                Gujjar.ErrMsg(ep);
            }
        }

        private void LoadReadyItems()
        {
            try
            {
                using (Context db = new Context())
                {
                    readyItems = db.ReadyItems.ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void LoadAllComboData()
        {
            LoadBrokers();
            LoadReadyItems();
            LoadTradeUnits();
        }
        private void BindBrokers()
        {
            cbBrokers.DataSource = brokers;
            cbBrokers.DisplayMember = "Name";
            cbBrokers.ValueMember = "Id";
            
        }
        private void BindDealItems()
        {
            cbDealItems.DataSource = readyItems;

            cbDealItems.DisplayMember = "Title";
            cbDealItems.ValueMember = "Id";
        }
        private void BindComboData()
        {
            BindBrokers();
            BindDealItems();
            BindCBTradeUnits();
            cbBrokers.SelectedItem = brokers.FirstOrDefault(a => a.Name == "N/A");
            cbDealItems.SelectedItem = readyItems.FirstOrDefault(a => a.Title == "N/A");
            cbTradeUnits.SelectedItem = tradeUnits.FirstOrDefault(a => a.Title == "N/A");
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait = new WaitForm(LoadAllComboData);
                wait.ShowDialog();
                Helper.IsOkApplied();
                BindComboData();
                Gujjar.NumbersOnly(tbVehiclesCount);
                Gujjar.NumbersOnly(tbPerTradeUnitPrice);
                Gujjar.TB4(pMain);
                btnAdd.Enabled = false;
                dtpDealDate.MinDate = appSettings.StartDate;
                dtpDealDate.MaxDate = appSettings.EndDate;
                Gujjar.TBOptional(tbDescription);
                Gujjar.AddDatagridviewButton(dgv, dgvdeletebtn, "Delete", "Delete", 80);
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

        private void btnAddBroker_Click(object sender, EventArgs e)
        {
            try
            {
                AddBrokerForm form = new AddBrokerForm();
                form.ShowDialog();

                int id = form.BrokerId;
                if(id != 0)
                {
                    LoadBrokers();
                    BindBrokers();

                    cbBrokers.SelectedItem = cbBrokers.Items.OfType<ReadyBroker>().FirstOrDefault(a => a.Id == id);
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
                AddReadyDealItemForm form = new AddReadyDealItemForm();
                form.ShowDialog();

                int did = form.ItemId;
                if(did != 0)
                {
                    LoadReadyItems();
                    BindDealItems();

                    cbDealItems.SelectedItem = cbDealItems.Items.OfType<ReadyItem>()
                        .FirstOrDefault(a => a.Id == did);
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
            tbRemaining.Text = rem.ToString();
        }
        private void btnAddSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                if(cbBrokers.SelectedIndex == -1 || cbDealItems.SelectedIndex == -1)
                {
                    throw new Exception("Please choose broker and item");
                }
                if(cbDealItems.Text == "N/A" || cbBrokers.Text == "N/A")
                {
                    throw new Exception("Please choose broker and item.");
                }

                string vehCount = tbVehiclesCount.Text;
                if(string.IsNullOrEmpty(vehCount))
                {
                    throw new Exception("Please enter number of vehicles");
                }

                int vehCount2 = vehCount.ToInt();
                if(vehCount2 == 0)
                {
                    throw new Exception("Please enter number of vehicles at least 1");
                }

                if(schVMBindingSource.List.Count == 0)
                {
                    int total = tbVehiclesCount.Text.ToInt();
                    int remain = total;
                    int done = 0;

                    AddScheduleForm form4 = new AddScheduleForm(total, done, remain, dtpDealDate.Value);
                    form4.ShowDialog();
                    if(form4.vm != null)
                    {
                        schVMBindingSource.Add(form4.vm);
                        dgv.Refresh();
                    }

                    int done2 = schVMBindingSource.List.OfType<SchVM>().Sum(a => a.Vehicles);
                    int rem = total - done2;
                    tbRemaining.Text = rem.ToString();
                }
                else
                {
                    int total = tbVehiclesCount.Text.ToInt();
                    int done = schVMBindingSource.List.OfType<SchVM>().Sum(a => a.Vehicles);
                    int rem = total - done;
                    if(rem == 0)
                    {
                        throw new Exception("Schedules are completed");
                    }
                    DateTime lastDate = schVMBindingSource.List.OfType<SchVM>().Select(a => Convert.ToDateTime(a.ReadyDate)).ToList().OrderByDescending(a => a).FirstOrDefault();
                    AddScheduleForm form = new AddScheduleForm(total, done, rem, lastDate);
                    form.ShowDialog();

                    var obj = form.vm;
                    if(obj != null)
                    {
                        schVMBindingSource.List.Add(obj);
                        dgv.Refresh();
                    }
                    int done2 = schVMBindingSource.List.OfType<SchVM>().Sum(a => a.Vehicles);
                    rem = total - done2;
                    tbRemaining.Text = rem.ToString();
                }

                var list = schVMBindingSource.List.OfType<SchVM>();
                if(list.Count() > 0)
                {
                    var total2 = list.Sum(a => a.Vehicles);
                    int totalStr = tbVehiclesCount.Text.ToInt();

                    if(total2 == totalStr)
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }

                if(tradeUnit == null || tradeUnit.Title == "N/A")
                {
                    throw new Exception("Please select trade unit.");
                }
                if(schVMBindingSource.List.Count == 0)
                {
                    throw new Exception("No schedule is added in the list");
                }

                DialogResult res = Gujjar.ConfirmYesNo("Are you confirmed before to add deal?");
                if (res == DialogResult.No)
                    return;
                var schs = schVMBindingSource.List.OfType<SchVM>();
                ReadyBroker broker = cbBrokers.SelectedItem as ReadyBroker;
                ReadyItem readyItem = cbDealItems.SelectedItem as ReadyItem;

                using (Context db = new Context())
                {
                    var sett = db.AppSettings.First();
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            ReadyDeal deal = new ReadyDeal
                            {
                                CompletionStatus = string.Format("0/{0}", tbVehiclesCount.Text),
                                TotalWeight = 0,
                                ReadyItemId = readyItem.Id,
                                IsCompleted = false,
                                DealDate = dtpDealDate.Value,
                                NoOfVehicles = tbVehiclesCount.Text.ToInt(),
                                ReadyBrokerId = broker.Id,
                                TradeUnit = null,
                                PerTradeUnitPrice = tbPerTradeUnitPrice.Text.ToDecimal(),
                                Broker = null,
                                DealStatus = AppDealStatus.Scheduled,
                                Description = tbDescription.Text,
                                EstimatedPrice = 0,
                                ReadyItem = null,
                                ReadySchedules = null,
                                ReadyTradeUnitId = tradeUnit.Id,
                                TotalTradeUnits = 0,
                                AppUserId = appUser.Id
                            };

                            deal = db.ReadyDeals.Add(deal);
                            db.SaveChanges();

                            int num = 0;
                            foreach (var item in schs)
                            {
                                num++;
                                char ch = 'A';
                                for (int i = 0; i < item.Vehicles; i++)
                                {
                                    ReadySchedule readySchedule = new ReadySchedule
                                    {
                                        BrokerShareAmount = 0,
                                        ScheduleNo = string.Format("{0}-{1}/{2}", num, ch, item.Vehicles),
                                        Bharthies = null,
                                        BrokerSharePercentage = 0,
                                        DispatchedDate = null,
                                        NetScheduleAmount = 0,
                                        GrossPrice = 0,
                                        IsCompleted = false,
                                        NoOfVehicles = 1,
                                        PerTradeUnitPrice = deal.PerTradeUnitPrice,
                                        ReadyDate = Convert.ToDateTime(item.ReadyDate),
                                        ReadyDriverId = null,
                                        ScheduleDate = DateTime.Now,
                                        ScheduleWeight = 0,
                                        TotalTradeUnits = 0,
                                        WeighBridge = null,
                                        WeighBridgeId = null,
                                        Driver = null,
                                        WeighBridgeWeight = 0,
                                        ReadyDealId = deal.Id,
                                        ScheduleType = ReadyScheduleType.Scheduled,
                                        ReadySelectorId = null,
                                        VehicleNo = "N/A",
                                        Unum = Helper.Unum,
                                        EmptyVehicleWeight = 0,
                                        ReadyDeal = null,
                                        Selector = null,
                                        LaborExpenses = 0,
                                        LaborExpensesDescription = "N/A"
                                    };
                                    ch++;
                                    readySchedule = db.ReadySchedules.Add(readySchedule);
                                    db.SaveChanges();
                                    
                                }

                                string msg = string.Format("Ready Schedule Alarm. \nDeal No. {0}, Schedule No. {1}.\nBroker: {2}, Deal Item: {3}.\nSchedule time: {4}, ready time: {5}\nschedule for no of vehicles: {6}",
                                        deal.Id, num, broker.Name, readyItem.Title, DateTime.Now.ToShortDateString(), item.ReadyDate, item.Vehicles);

                                ReadyScheduleAlarm alarm = new ReadyScheduleAlarm
                                {
                                    EndDate = Convert.ToDateTime(item.ReadyDate),
                                    AlarmReadyDate = Convert.ToDateTime(item.ReadyDate).AddDays(-sett.DaysAlertBeforeReady),
                                    GenerateDate = DateTime.Now,
                                    IsActive = true,
                                    AlarmText = msg
                                };
                                db.ReadyScheduleAlarms.Add(alarm);
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

        private void tbVehiclesCount_TextChanged(object sender, EventArgs e)
        {
            tbRemaining.Text = tbVehiclesCount.Text;
        }

        private void cbDealItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnAddBharthiType_Click(object sender, EventArgs e)
        {
            try
            {
                AddReadyTradeUnit form = new AddReadyTradeUnit();
                form.ShowDialog();
                int did = form.TradeUnitId;
                if (did != 0)
                {
                    WaitForm wait2 = new WaitForm(LoadTradeUnits);
                    wait2.ShowDialog();
                    BindCBTradeUnits();

                    cbTradeUnits.SelectedItem = cbTradeUnits.Items.OfType<ReadyTradeUnit>()
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
                return;

            tradeUnit = cbTradeUnits.SelectedItem as ReadyTradeUnit;
            string title = tradeUnit.Title;
            decimal qty = tradeUnit.UnitQty;
            if (qty == 0)
            {
                qty = 1;
            }
            label11.Text = string.Format("Per {0} qty:", title);            
            tbPerTradeUnitQty.Text = qty.ToString("n4");
            label7.Text = string.Format("Per {0} price: ", title);
        }

        private void dgv_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
            //dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = new DataGridViewCellStyle { BackColor = Color.Teal, ForeColor = Color.Yellow };
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(e.RowIndex == -1 || e.RowIndex == dgv.NewRowIndex)
                {
                    return;
                }
                int ri = e.RowIndex;
                if(dgv.Rows[ri].Cells[dgvdeletebtn].ColumnIndex == e.ColumnIndex)
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
    }
}
