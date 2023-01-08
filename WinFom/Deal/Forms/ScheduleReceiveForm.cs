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
using Model.Deal.ViewModel;
using WinFom.Common.Forms;
using Model.Deal.Common;
using WinFom.Common.Model;
using Model.Admin.Model;
using Model.Financials.Model;
using Model.Employees.Model;
using System.Data.Entity;

namespace WinFom.Deal.Forms
{
    public partial class ScheduleReceiveForm : Form
    {
        private int _scheduleId = 0;
        private DealSchedule dealSchedule = null;
        private AppDeal _deal = null;
        //private List<Employee> selectors = new List<Employee>();
        public bool IsDone = false;
        private List<WeighBridge> weighBridges = new List<WeighBridge>();
        private AppSettings appSett = Helper.AppSet;
        private AppUser appUser = SingleTon.LoginForm.appUser;
        private List<DayBook> pDaybooks = new List<DayBook>();
        //private string btndgvrec = "asd2341asdf11234asdkaser";

        //private decimal tradeUnitsDifference = 0;
        // decimal packingsDifference = 0;
        //private decimal totalQtyDifferenc = 0;
        //private decimal priceDifference = 0;

        //private decimal allTradeUnitsDifference = 0;
        // decimal packingsDifference = 0;
        //private decimal allTotalQtyDifferenc = 0;
        //private decimal allTPriceDifference = 0;
        public ScheduleReceiveForm(int scheduleId)
        {
            InitializeComponent();
            _scheduleId = scheduleId;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        
        private void LoadWeighBridges()
        {
            try
            {
                using (Context db = new Context())
                {
                    weighBridges = db.WeighBridges.Where(a => a.WeighBrideType == WeighBridgeType.Receiving)
                        .OrderBy(a => a.Name).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindCBWeighBridges()
        {
            cbWeighBridges.DataSource = weighBridges;
            cbWeighBridges.DisplayMember = "Name";
            cbWeighBridges.ValueMember = "Id";
        }
        private void LoadDealSchedule()
        {
            try
            {
                using (Context db = new Context())
                {
                    dealSchedule = db.DealSchedules.FirstOrDefault(a => a.Id == _scheduleId);
                    dealSchedule.Employee = db.Employees.Find(dealSchedule.EmployeeId);
                    dealSchedule.Vehicle = db.Vehicles.Find(dealSchedule.VehicleId);
                    dealSchedule.ScheduleLoadPackings = db.ScheduleLoadPackings.Where(a => a.DealScheduleId == dealSchedule.Id).ToList();
                    dealSchedule.GoodCompany = db.GoodCompanies.Find(dealSchedule.GoodCompanyId);
                    foreach (var item in dealSchedule.ScheduleLoadPackings)
                    {
                        item.DealPacking = db.DealPackings.Find(item.DealPackingId);
                    }
                    _deal = db.AppDeals.Find(dealSchedule.AppDealId);
                    _deal.Company = db.Companies.Find(_deal.CompanyId);
                    _deal.Broker = db.Brokers.Find(_deal.BrokerId);
                    _deal.DealItem = db.DealItems.Find(_deal.DealItemId);
                    _deal.Packing = db.DealPackings.Find(_deal.DealPackingId);
                    _deal.TradeUnit = db.TradeUnits.Find(_deal.TradeUnitId);
                    _deal.PackingUnit = db.PackingUnits.Find(_deal.PackingUnitId);
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

                WaitForm wait2 = new WaitForm(LoadDealSchedule);
                wait2.ShowDialog();

                WaitForm wait3 = new WaitForm(LoadWeighBridges);
                wait3.ShowDialog();

                tbBroker.Text = _deal.Broker.Name;
                tbCompany.Text = _deal.Company.Name;
                tbDealDate.Text = _deal.DealDate.ToShortDateString();
                tbDealItem.Text = _deal.DealItem.Name;
                tbDealNo.Text = _deal.Id.ToString();

                tbScheduledTradeUnits.Text = dealSchedule.ScheduledTradeUnits.ToString("n2");
                tbSchedulePackings.Text = dealSchedule.ScheduledPackingsUnits.ToString("n2");
                tbSchedulePrice.Text = dealSchedule.ScheduledPrice.ToString("n2");
                tbScheduleTotalQty.Text = dealSchedule.ScheduledSubTradeUnits.ToString("n2");
                tbSelector.Text = dealSchedule.Employee.Name;
                //dtpDealDate.Value = dealSchedule.AddedDate;
                dtpScheduleDate.Value = dealSchedule.DispatchedDate.Value;

                lblPackings.Text = lblPackings2.Text = lblReceivePackings.Text = _deal.Packing.Name;
                lblTradeUnit.Text = lblTradeUnits2.Text = lblReceivedTradeUnits.Text = dealSchedule.TradeUnitTitle;
                lblTotalQty.Text = lblTotalQty2.Text = lblReceivedTotalQty.Text = dealSchedule.PackingUnitTitle;

                Helper.IsOkApplied();

                tbLoadedPackings.Text = tbReceivedPackings.Text = dealSchedule.LoadedPackingsUnits.ToString("n2");
                tbLoadedPrice.Text = dealSchedule.LoadedPrice.ToString("n2");
                tbLoadedTotalQty.Text = dealSchedule.LoadedSubTradeUnits.ToString("n2");
                tbLoadedTradeUnits.Text = dealSchedule.LoadedTradeUnits.ToString("n2");
                dtpLoadedDate.Value = dealSchedule.LoadedDate.Value;
                tbDealUnitPrice.Text = _deal.TradeUnitPrice.ToString("n2");
                tbReceivedTotalQty.Text = dealSchedule.LoadedSubTradeUnits.ToString("n2");

                StringBuilder sb = new StringBuilder();
                foreach (var item in dealSchedule.ScheduleLoadPackings)
                {
                    sb.AppendFormat("{0}, ", item.DealPacking.Name);
                    PackingReceiveNonEdit vm = new PackingReceiveNonEdit
                    {
                        Packing = item.DealPacking.Name,
                        Qty = item.LoadQty
                    };
                    packingReceiveNonEditBindingSource.List.Add(vm);
                }
                string packs = "N/A";
                if(sb.Length > 3)
                {
                    packs = sb.ToString().Remove(sb.ToString().LastIndexOf(','), 1);
                }

                lblPackings2.Text = lblReceivePackings.Text = packs;

                //Gujjar.AddDatagridviewButton(dgvReceived, btndgvrec, "Receive", "Receive", 80);

                foreach (var item in dealSchedule.ScheduleLoadPackings)
                {
                    PackingReceiveEditAble vm = new PackingReceiveEditAble
                    {
                        Id = item.Id,
                        Packing = item.DealPacking.Name,
                        PackingId = item.DealPackingId,
                        QtyLoaded = item.LoadQty,
                        QtyReceived = item.LoadQty
                    };
                    packingReceiveEditAbleBindingSource.List.Add(vm);
                }

                BindCBWeighBridges();


                //SLD
                sldTbPackings.Text = dealSchedule.DispatchLoadPackingDifference.ToString("n2");
                //decimal schPrice = dealSchedule.ScheduledTradeUnits * _deal.TradeUnitPrice;
                //decimal sldPrice = dealSchedule.LoadedTradeUnits * _deal.TradeUnitPrice;
                //decimal pDiff = schPrice - sldPrice;
                sldTbPrice.Text = dealSchedule.DispatchLoadPriceDifference.ToString("n2");
                sldTbTotalQty.Text = dealSchedule.DispatchLoadSubTradeUnitDifference.ToString("n2");
                sldTbTradeUnits.Text = dealSchedule.DispatchLoadTradeUnitDifference.ToString("n2");

                //RLD
                Gujjar.NumbersOnly(tbFareAmount);
                tbFareAmount.Text = dealSchedule.FareDealAmount.ToString("n2");
                dtpReceivedDate.MinDate = appSett.StartDate;
                dtpReceivedDate.MaxDate = appSett.EndDate;
                Gujjar.TBOptional(tbLaborChargesDescription);
                Gujjar.NumbersOnly(tbLaborCharges);
                Gujjar.TB4(panel2);
                Gujjar.NumbersOnly(tbPerBagTracktorLabor);
                tbTracktorLaborAmount.Text = "0";
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbLoadedPackings_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string packingStr = tbLoadedPackings.Text;
                if (string.IsNullOrEmpty(packingStr))
                {
                    tbLoadedPackings.Focus();
                    tbLoadedPackings.BackColor = Color.Pink;
                    return;
                    //throw new Exception(string.Format("Please enter no of {0}", _packing));
                }

                decimal packingAmnt = packingStr.ToDecimal();
                //if (packingAmnt > _remainingPackingUnits)
                //{
                //    string msg = string.Format("Remaining {0} {1}-(s) and now scheduled {2} {3}-(s). Please re-check. Thanks", _remainingPackingUnits,
                //        _packing, packingAmnt, _packing);
                //    throw new Exception(msg);
                //}
                decimal totalKgs = packingAmnt * _deal.PerPackingQty;
                decimal totalTradeUnits = totalKgs / _deal.TradeUnit.Qty;
                decimal price = totalTradeUnits * _deal.TradeUnitPrice;
                //decimal totalTradeSubUnits = totalKgs;
                tbLoadedTradeUnits.Text = totalTradeUnits.ToString("n2");
                tbLoadedTotalQty.Text = totalKgs.ToString("n2");
                tbLoadedPrice.Text = price.ToString("n2");

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
                //if(!Gujjar.IsValidForm(pMain))
                //{
                //    throw new Exception("Please fill all text fields");
                //}
                if(string.IsNullOrEmpty(tbReceivedTotalQty.Text))
                {
                    tbReceivedTotalQty.BackColor = Color.Pink;
                    tbReceivedTotalQty.Focus();
                    throw new Exception("Please enter total received qty");
                }
                if(string.IsNullOrEmpty(tbLaborCharges.Text))
                {
                    tbLaborCharges.BackColor = Color.Pink;
                    tbLaborCharges.Focus();
                    throw new Exception("Please enter labor charges");
                }
                decimal laborChargesAmmount = tbTotalLaborCharges.Text.ToDecimal();

                if (string.IsNullOrEmpty(tbLaborChargesDescription.Text))
                {
                    tbLaborChargesDescription.BackColor = Color.Pink;
                    tbLaborChargesDescription.Focus();
                    throw new Exception("Please enter labor charges description");
                }
                string laborChargesDescription = tbLaborChargesDescription.Text;

                if (cbWeighBridges.SelectedIndex == -1 || cbWeighBridges.Text == "N/A")
                {
                    throw new Exception("Please select receiving weigh bridge information");
                }

                if (!Helper.ConfirmUserPassword())
                    return;

                DialogResult res = Gujjar.ConfirmYesNo("Please confirm.. All information is correct!!");
                if (res == DialogResult.No)
                    return;
                WeighBridge weighBridge = cbWeighBridges.SelectedItem as WeighBridge;
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            DealSchedule sch2 = db.DealSchedules.Find(_scheduleId);

                            sch2.Vehicle = db.Vehicles.Find(sch2.VehicleId);
                            sch2.Driver = db.Drivers.Find(sch2.DriverId);
                            var gComp = db.GoodCompanies.Find(sch2.GoodCompanyId);

                            sch2.ReceivedPackingsUnits = dealSchedule.ReceivedPackingsUnits;
                            sch2.ReceivedPrice = dealSchedule.ReceivedPrice;
                            sch2.ReceivedSubTradeUnits = dealSchedule.ReceivedSubTradeUnits;
                            sch2.ReceivedTradeUnits = dealSchedule.ReceivedTradeUnits;

                            sch2.DiffLoadedPackingUnits = dealSchedule.DiffLoadedPackingUnits;
                            sch2.DiffLoadedPrice = dealSchedule.DiffLoadedPrice;
                            sch2.DiffLoadedSubTradeUnits = dealSchedule.DiffLoadedSubTradeUnits;
                            sch2.DiffLoadedTradeUnits = dealSchedule.DiffLoadedTradeUnits;
                            sch2.TracktorLaborAmount = tbTracktorLaborAmount.Text.ToDecimal();
                            sch2.IsArrived = true;
                            sch2.ArrivalDate = DateTime.Now;
                            sch2.ReceiveRemarks = tbRemarks.Text;
                            ScheduleStatus oldStatus = sch2.Status;
                            sch2.Status = ScheduleStatus.Arrived;
                            sch2.FareActualPaid = tbFareAmount.Text.ToDecimal();

                            db.Entry(sch2).State = System.Data.Entity.EntityState.Modified;

                            var vehicleObj = db.Vehicles.Find(sch2.VehicleId);
                            vehicleObj.Status = VehicleStatus.Available;
                            db.Entry(vehicleObj).State = System.Data.Entity.EntityState.Modified;

                            var driverObj = db.Drivers.Find(sch2.DriverId);
                            driverObj.IsAvailable = true;
                            db.Entry(driverObj).State = System.Data.Entity.EntityState.Modified;

                            db.SaveChanges();

                            ScheduleWeighBridge swb = new ScheduleWeighBridge
                            {
                                DealScheduleId = sch2.Id,
                                WeighBridgeId = weighBridge.Id,
                                Type = ScheduleWeighBridgeType.Receive
                            };

                            db.ScheduleWeighBridges.Add(swb);
                            db.SaveChanges();

                            var deal1 = db.AppDeals.Find(sch2.AppDealId);
                            deal1.DealSchedules = db.DealSchedules.Where(a => a.AppDealId == deal1.Id).ToList();

                            deal1.IsCompleted = !deal1.DealSchedules.Any(a => !a.IsArrived);
                            db.Entry(deal1).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            foreach (var item in packingReceiveEditAbleBindingSource.List.OfType<PackingReceiveEditAble>())
                            {
                                var dobj = db.ScheduleLoadPackings.Find(item.Id);
                                dobj.ReceiveQty = item.QtyReceived;
                                db.Entry(dobj).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();

                                FactoryPackingStockAddedRecord fpsr = new FactoryPackingStockAddedRecord
                                {
                                    AddedDate = dtpReceivedDate.Value,
                                    DealPackingId = item.PackingId,
                                    QtyAdded = item.QtyReceived,
                                    Description = string.Format("{0} {1}-(s) received in schedule no: {2}, deal: {3}, good company: {4}, driver: {5}, vehicle: {6}. Date time: {7}",
                                    item.QtyReceived, item.Packing, sch2.Id, _deal.Id, gComp.Name, sch2.Driver.Name, string.Format("{0}-{1}", sch2.Vehicle.VehicleType, sch2.Vehicle.No),
                                    dtpReceivedDate.Value)
                                };

                                db.FactoryPackingStockAddedRecords.Add(fpsr);
                                db.SaveChanges();

                                var dobj2 = db.FactoryPackingStocks.FirstOrDefault(a => a.DealPackingId == item.PackingId);
                                if(dobj2 == null)
                                {
                                    dobj2 = new FactoryPackingStock
                                    {
                                        DealPackingId = item.PackingId,
                                        Quantity = item.QtyReceived
                                    };
                                    db.FactoryPackingStocks.Add(dobj2);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    dobj2.Quantity += item.QtyReceived;
                                    db.Entry(dobj2).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();
                                }
                                

                                PackingIssueReceiveRecord pirr = new PackingIssueReceiveRecord
                                {
                                    DateTime = dtpReceivedDate.Value,
                                    DealPackingId = item.PackingId,
                                    GoodCompanyId = gComp.Id,
                                    Qty = item.QtyReceived,
                                    RecordType = RecordType.Receive,
                                    Description = "N/A",
                                    Remarks = string.Format("{0} {1}-(s) received in schedule no: {2}, deal: {3}, good company: {4}, driver: {5}, vehicle: {6}. Date time: {7}",
                                    item.QtyReceived, item.Packing, sch2.Id, _deal.Id, gComp.Name, sch2.Driver.Name, string.Format("{0}-{1}", sch2.Vehicle.VehicleType, sch2.Vehicle.No),
                                    dtpReceivedDate.Value),
                                    Unum = Helper.Unum
                                };
                                db.PackingIssueReceiveRecords.Add(pirr);
                                db.SaveChanges();

                                PackingStock dobj4 = db.PackingStocks.FirstOrDefault(a => a.DealPackingId == item.PackingId && a.GoodCompanyId == gComp.Id);
                                if(dobj4 == null)
                                {
                                    dobj4 = new PackingStock
                                    {
                                        Balance = -item.QtyReceived,
                                        DealPackingId = item.PackingId,
                                        GoodCompanyId = gComp.Id
                                    };
                                    db.PackingStocks.Add(dobj4);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    dobj4.Balance -= item.QtyReceived;

                                    db.Entry(dobj4).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();
                                }
                                
                            }

                            db.SaveChanges();

                            #region "Financial Account"
                            string financialMessage = string.Format("Accrued freight expense for ({0}) payable to goods company ({1}), amount ({2}), vehicle no ({3}).",
                                string.Format("Deal {0}, Schedule {1}", _deal.Id, dealSchedule.Id), dealSchedule.GoodCompany.Name, tbFareAmount.Text, dealSchedule.Vehicle.No);

                            DayBook dayBookEntry = new DayBook
                            {
                                Id = 0,
                                CreditTrace = "",
                                DebitTrace = "",
                                Date = dtpReceivedDate.Value,
                                Description = financialMessage,
                                Amount = tbFareAmount.Text.ToDecimal(),
                                CanRollBack = false,
                                DealScheduleId = sch2.Id,
                                InDate = DateTime.Now.Date
                            };
                            db.DayBooks.Add(dayBookEntry);
                            db.SaveChanges();

                            AccountTransaction debitTransaction = new AccountTransaction
                            {
                                Id = 0,
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Debit,
                                Balance = 0,
                                CreditAmount = 0,
                                Date = dtpReceivedDate.Value,
                                DayBookId = dayBookEntry.Id,
                                DebitAmount = tbFareAmount.Text.ToDecimal(),
                                GeneralAccountId = Properties.Resources.FreightExpenseAccount,
                                Description = financialMessage
                            };

                            debitTransaction.Balance = debitTransaction.DebitAmount;
                            var freightDbObj = db.AccountTransactions
                                .Where(a => a.GeneralAccountId == Properties.Resources.FreightExpenseAccount)
                                .OrderByDescending(a => a.Id).FirstOrDefault();
                            if (freightDbObj != null)
                            {
                                debitTransaction.Balance += freightDbObj.Balance;
                            }
                            debitTransaction = db.AccountTransactions.Add(debitTransaction);
                            db.SaveChanges();

                            decimal payAmount = tbFareAmount.Text.ToDecimal();
                            AccountTransaction creditTransaction = new AccountTransaction
                            {
                                Id = 0,
                                Balance = payAmount,
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Credit,
                                CreditAmount = payAmount,
                                Date = dtpReceivedDate.Value,
                                DayBookId = dayBookEntry.Id,
                                DebitAmount = 0,
                                GeneralAccountId = dealSchedule.GoodCompany.GeneralAccountId,
                                Description = financialMessage
                            };

                            var dbcredittrans = db.AccountTransactions.Where(a => a.GeneralAccountId == creditTransaction.GeneralAccountId)
                                .OrderByDescending(a => a.Id).FirstOrDefault();
                            if (dbcredittrans != null)
                            {
                                creditTransaction.Balance += dbcredittrans.Balance;
                            }
                            creditTransaction = db.AccountTransactions.Add(creditTransaction);
                            db.SaveChanges();

                            var dayBoodDb = db.DayBooks.Find(dayBookEntry.Id);

                            var cAcct = db.Accounts.OfType<GeneralAccount>().FirstOrDefault(a => a.Id == dealSchedule.GoodCompany.GeneralAccountId);
                            var dAcct = db.Accounts.OfType<GeneralAccount>().FirstOrDefault(a => a.Id == Properties.Resources.FreightExpenseAccount);
                            dayBoodDb.CreditTrace = string.Format("{0}. Trans Id: {1}", cAcct.Title, creditTransaction.Id);
                            dayBoodDb.DebitTrace = string.Format("{0}. Trans Id: {1}", dAcct.Title, debitTransaction.Id);
                            dayBoodDb.DebitAccountId = dAcct.Id;
                            dayBoodDb.CreditAccountId = cAcct.Id;

                            db.Entry(dayBoodDb).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            pDaybooks.Add(dayBoodDb);

                            #region Loss in item due to less qty
                            if(dealSchedule.ReceivedSubTradeUnits < dealSchedule.LoadedSubTradeUnits)
                            {
                                decimal diff = dealSchedule.DiffLoadedSubTradeUnits;
                                decimal amountDiff = dealSchedule.DiffLoadedPrice;

                                string finMsg = string.Format("Item ({0}) shortage loss, amount ({1}), qty loss ({2}-/) in (Deal No. {3}, Schedule No. {4}), Vehicle No ({5}), from ({6})",
                                    _deal.DealItem.Name, amountDiff.ToString("n2"), diff.ToString("n2"), _deal.Id, dealSchedule.Id, dealSchedule.Vehicle.No, _deal.Company.Name);

                                // credit: item account
                                // debit: goods loss account

                                DayBook daybook2 = new DayBook
                                {
                                    Id = 0,
                                    Date = dtpReceivedDate.Value,
                                    Description = finMsg,
                                    Amount = amountDiff,
                                    CanRollBack = false,
                                    DealScheduleId  = sch2.Id,
                                    InDate = DateTime.Now.Date
                                };

                                daybook2 = db.DayBooks.Add(daybook2);
                                db.SaveChanges();

                                string itemLossAccountId = Properties.Resources.GoodsLossDueToShortQty;
                                AccountTransaction debitItemLoss = new AccountTransaction
                                {
                                    Id = 0,
                                    Account = null,
                                    AccountTransactionType = AccountTransactionType.Debit,
                                    CreditAmount = 0,
                                    Balance = amountDiff,
                                    Date = dtpReceivedDate.Value,
                                    DayBookId = daybook2.Id,
                                    DebitAmount = amountDiff,
                                    Description = finMsg,
                                    GeneralAccountId = itemLossAccountId
                                };

                                var debitObj = db.AccountTransactions.Where(a => a.GeneralAccountId == itemLossAccountId).AsParallel()
                                    .ToList()
                                    .Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date)
                                    .OrderByDescending(a => a.Id).FirstOrDefault();
                                if(debitObj != null)
                                {
                                    debitItemLoss.Balance += debitObj.Balance;
                                }

                                debitItemLoss = db.AccountTransactions.Add(debitItemLoss);
                                db.SaveChanges();

                                AccountTransaction creditItemEntry = new AccountTransaction
                                {
                                    Id = 0,
                                    Balance = amountDiff,
                                    Account = null,
                                    AccountTransactionType = AccountTransactionType.Credit,
                                    CreditAmount = amountDiff,
                                    Date = dtpReceivedDate.Value,
                                    DayBookId = daybook2.Id,
                                    DebitAmount = 0,
                                    GeneralAccountId = _deal.DealItem.GeneralAccountId,
                                    Description = finMsg
                                };

                                var creditObj = db.AccountTransactions.Where(a => a.GeneralAccountId == _deal.DealItem.GeneralAccountId).AsParallel()
                                    .ToList()
                                    .Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date)
                                    .OrderByDescending(a => a.Id).FirstOrDefault();
                                if (creditObj != null)
                                {
                                    creditItemEntry.Balance = creditObj.Balance - creditItemEntry.Balance;
                                }

                                creditItemEntry = db.AccountTransactions.Add(creditItemEntry);
                                db.SaveChanges();

                                GeneralAccount cAcct2 = db.Accounts.OfType<GeneralAccount>().FirstOrDefault(a => a.Id == _deal.DealItem.GeneralAccountId);
                                GeneralAccount dAcct2 = db.Accounts.OfType<GeneralAccount>().FirstOrDefault(a => a.Id == itemLossAccountId);

                                var dayBookdb = db.DayBooks.Find(daybook2.Id);
                                dayBookdb.CreditTrace = string.Format("{0}. Trans Id: {1}", cAcct2.Title, creditItemEntry.Id);
                                dayBookdb.DebitTrace = string.Format("{0}. Trans Id: {1}", dAcct2.Title, debitItemLoss.Id);
                                dayBookdb.DebitAccountId = dAcct2.Id;
                                dayBookdb.CreditAccountId = cAcct2.Id;

                                db.Entry(dayBookdb).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                pDaybooks.Add(dayBookdb);
                                
                            }
                            #endregion
                            #endregion

                            #region Labor Expenses
                            #region "Labor expense accounting"
                            decimal laboramount = laborChargesAmmount;
                            if (laboramount > 0)
                            {



                                string finMessage = string.Format("Labor charges amount ({0}) for cotton seed receiving for ({1}). Dated ({2}). Description ({3}) by ({4})", laboramount,
                                    string.Format("Deal: {0}, Schedule: {1}", _deal.Id, dealSchedule.Id), dtpReceivedDate.Value, tbLaborChargesDescription.Text, appUser.Name);

                                DayBook daybookEntry = new DayBook
                                {
                                    Id = 0,
                                    Amount = laboramount,
                                    Date = dtpReceivedDate.Value,
                                    Description = finMessage,
                                    CanRollBack = false,
                                    DealScheduleId = sch2.Id,
                                    InDate = DateTime.Now.Date
                                };
                                daybookEntry = db.DayBooks.Add(daybookEntry);
                                db.SaveChanges();



                                #region "Credit entry"
                                GeneralAccount creditAccount1 = db.Accounts.Find(Properties.Resources.CottonSeedLaborExpensePayable) as GeneralAccount;
                                GeneralAccount debitAccount1 = db.Accounts.Find(Properties.Resources.CottonSeedLaborExpenseAccount) as GeneralAccount;

                                AccountTransaction creditTrans1 = new AccountTransaction
                                {
                                    Account = null,
                                    Description = finMessage,
                                    AccountTransactionType = AccountTransactionType.Credit,
                                    CreditAmount = laboramount,
                                    Balance = laboramount,
                                    Date = dtpReceivedDate.Value,
                                    DayBookId = daybookEntry.Id,
                                    DebitAmount = 0,
                                    Id = 0,
                                    GeneralAccountId = creditAccount1.Id
                                };

                                var dbProdEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == creditAccount1.Id)
                                    .AsParallel().ToList().Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date)
                                    .OrderByDescending(a => a.Id)
                                    .FirstOrDefault();
                                if (dbProdEntry != null)
                                {
                                    creditTrans1.Balance += dbProdEntry.Balance;
                                }

                                creditTrans1 = db.AccountTransactions.Add(creditTrans1);
                                db.SaveChanges();
                                #endregion

                                #region "Debit transaction"
                                AccountTransaction debitTrans = new AccountTransaction
                                {
                                    Id = 0,
                                    GeneralAccountId = debitAccount1.Id,
                                    Account = null,
                                    AccountTransactionType = AccountTransactionType.Debit,
                                    CreditAmount = 0,
                                    Balance = laboramount,
                                    Date = dtpReceivedDate.Value,
                                    DayBookId = daybookEntry.Id,
                                    DebitAmount = laboramount,
                                    Description = finMessage
                                };

                                var dbDebitEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == debitAccount1.Id)
                                    .AsParallel().ToList().Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date)
                                    .OrderByDescending(a => a.Id)
                                    .FirstOrDefault();
                                if (dbDebitEntry != null)
                                {
                                    debitTrans.Balance += dbDebitEntry.Balance;
                                }
                                debitTrans = db.AccountTransactions.Add(debitTrans);
                                db.SaveChanges();


                                DayBook dbDayBook = db.DayBooks.Find(daybookEntry.Id);
                                dbDayBook.DebitTrace = string.Format("{0}. Trans Id: {1}", debitAccount1.Title, debitTrans.Id);
                                dbDayBook.CreditTrace = string.Format("{0}. Trans Id: {1}", creditAccount1.Title, creditTrans1.Id);
                                dbDayBook.CreditAccountId = creditAccount1.Id;
                                dbDayBook.DebitAccountId = debitAccount1.Id;
                                db.Entry(dbDayBook).State = EntityState.Modified;
                                db.SaveChanges();
                                #endregion
                                pDaybooks.Add(dbDayBook);

                            }
                            #endregion
                            #endregion

                            decimal tracktorLabor = tbTracktorLaborAmount.Text.ToDecimal();
                            #region "Tracktor Labor"
                            if (tracktorLabor > 0)
                            {
                                string finMessage = string.Format("Tracktor Labor charges amount ({0}) for cotton seed receiving for ({1}). Dated ({2}). Description ({3}) by ({4})", tracktorLabor,
                                    string.Format("Deal: {0}, Schedule: {1}", _deal.Id, dealSchedule.Id), dtpReceivedDate.Value, tbLaborChargesDescription.Text, appUser.Name);

                                DayBook daybookEntry = new DayBook
                                {
                                    Id = 0,
                                    Amount = tracktorLabor,
                                    Date = dtpReceivedDate.Value,
                                    Description = finMessage,
                                    CanRollBack = false,
                                    DealScheduleId = sch2.Id,
                                    InDate = DateTime.Now.Date
                                };
                                daybookEntry = db.DayBooks.Add(daybookEntry);
                                db.SaveChanges();



                                #region "Credit entry"
                                GeneralAccount creditAccount1 = db.Accounts.Find(Properties.Resources.TracktorLaborPayableAccount) as GeneralAccount;
                                GeneralAccount debitAccount1 = db.Accounts.Find(Properties.Resources.TracktorLaborExpenseAccount) as GeneralAccount;

                                AccountTransaction creditTrans1 = new AccountTransaction
                                {
                                    Account = null,
                                    Description = finMessage,
                                    AccountTransactionType = AccountTransactionType.Credit,
                                    CreditAmount = tracktorLabor,
                                    Balance = tracktorLabor,
                                    Date = dtpReceivedDate.Value,
                                    DayBookId = daybookEntry.Id,
                                    DebitAmount = 0,
                                    Id = 0,
                                    GeneralAccountId = creditAccount1.Id
                                };

                                var dbProdEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == creditAccount1.Id)
                                    .AsParallel().ToList().Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date)
                                    .OrderByDescending(a => a.Id)
                                    .FirstOrDefault();
                                if (dbProdEntry != null)
                                {
                                    creditTrans1.Balance += dbProdEntry.Balance;
                                }

                                creditTrans1 = db.AccountTransactions.Add(creditTrans1);
                                db.SaveChanges();
                                #endregion

                                #region "Debit transaction"
                                AccountTransaction debitTrans = new AccountTransaction
                                {
                                    Id = 0,
                                    GeneralAccountId = debitAccount1.Id,
                                    Account = null,
                                    AccountTransactionType = AccountTransactionType.Debit,
                                    CreditAmount = 0,
                                    Balance = tracktorLabor,
                                    Date = dtpReceivedDate.Value,
                                    DayBookId = daybookEntry.Id,
                                    DebitAmount = tracktorLabor,
                                    Description = finMessage
                                };

                                var dbDebitEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == debitAccount1.Id)
                                    .AsParallel().ToList().Where(a => a.Date.Date >= appSett.StartDate.Date && a.Date.Date <= appSett.EndDate.Date)
                                    .OrderByDescending(a => a.Id)
                                    .FirstOrDefault();
                                if (dbDebitEntry != null)
                                {
                                    debitTrans.Balance += dbDebitEntry.Balance;
                                }
                                debitTrans = db.AccountTransactions.Add(debitTrans);
                                db.SaveChanges();


                                DayBook dbDayBook = db.DayBooks.Find(daybookEntry.Id);
                                dbDayBook.DebitTrace = string.Format("{0}. Trans Id: {1}", debitAccount1.Title, debitTrans.Id);
                                dbDayBook.CreditTrace = string.Format("{0}. Trans Id: {1}", creditAccount1.Title, creditTrans1.Id);
                                dbDayBook.CreditAccountId = creditAccount1.Id;
                                dbDayBook.DebitAccountId = debitAccount1.Id;
                                db.Entry(dbDayBook).State = EntityState.Modified;
                                db.SaveChanges();
                                #endregion
                                pDaybooks.Add(dbDayBook);
                                
                            }
                            #endregion
                           
                            trans.Commit();

                            if(appSett.PrintFinancialTransactions)
                            {
                                Helper.PrintTransactions(pDaybooks);
                            }
                            Gujjar.InfoMsg(string.Format("Schedule status is changed to ({0}) to ({1}).", oldStatus, sch2.Status));
                            IsDone = true;

                            
                            Close();
                        }
                        catch (Exception exp3)
                        {
                            trans.Rollback();
                            throw exp3;
                        }
                    }

                        
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbReceivedTradeUnits_TextChanged(object sender, EventArgs e)
        {
            
        }


        decimal recTotalQty = 0;
        decimal calcTradeUnits = 0;
        //decimal calcPackings = 0;
        decimal calcPrice = 0;
        private void tbReceivedTotalQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string receivedTotalQty = tbReceivedTotalQty.Text;

                if(string.IsNullOrEmpty(receivedTotalQty))
                {
                    //totalQtyDifferenc = 0;
                    //tradeUnitsDifference = 0;
                    ////packingsDifference = 0;
                    //priceDifference = 0;

                    tbReceivedPrice.Text = "0";
                    tbReceivedTradeUnits.Text = "0";

                    //tbReceivedPrice.Text = sldTbPrice.Text;
                    //tbReceivedTradeUnits.Text = sldTbTradeUnits.Text;
                    //tbReceivedTotalQty.Text = sldTbTotalQty.Text;

                    //tbPriceDifference.Text = sldTbPrice.Text;
                    //tbTradeUnitsDifference.Text = sldTbTradeUnits.Text;
                    //tbTotalQtyDifference.Text = sldTbTotalQty.Text;

                    lrdTbTradeUnits.Text = "0";
                    lrdTbTotalQty.Text = "0";
                    lrdTbPrice.Text = "0";

                    //dealSchedule.ReceivedPackingsUnits = 0;
                    dealSchedule.ReceivedPrice = 0;
                    dealSchedule.ReceivedSubTradeUnits = 0;
                    dealSchedule.ReceivedTradeUnits = 0;
                }
                else
                {
                    recTotalQty = receivedTotalQty.ToDecimal();
                    calcTradeUnits = recTotalQty / _deal.TradeUnit.Qty;
                    //calcPackings = Math.Ceiling(recTotalQty / _deal.PerPackingQty);
                    calcPrice = calcTradeUnits * _deal.TradeUnitPrice;

                    dealSchedule.ReceivedTradeUnits = calcTradeUnits;
                    dealSchedule.ReceivedSubTradeUnits = recTotalQty;
                    dealSchedule.ReceivedPrice = calcPrice;

                    dealSchedule.DiffLoadedPrice = dealSchedule.LoadedPrice - calcPrice;
                    dealSchedule.DiffLoadedSubTradeUnits = dealSchedule.LoadedSubTradeUnits - recTotalQty;
                    dealSchedule.DiffLoadedTradeUnits = dealSchedule.LoadedTradeUnits - calcTradeUnits;

                    //totalQtyDifferenc = dealSchedule.LoadedSubTradeUnits - recTotalQty;
                    //tradeUnitsDifference = dealSchedule.LoadedTradeUnits - calcTradeUnits;
                    ////packingsDifference = dealSchedule.LoadedPackingsUnits - calcPackings;
                    //priceDifference = dealSchedule.LoadedPrice - calcPrice;

                    //allTotalQtyDifferenc = dealSchedule.ScheduledSubTradeUnits - recTotalQty;
                    //allTradeUnitsDifference = dealSchedule.ScheduledTradeUnits - calcTradeUnits;
                    ////packingsDifference = dealSchedule.LoadedPackingsUnits - calcPackings;
                    //allTPriceDifference = dealSchedule.ScheduledPrice - calcPrice;

                    tbReceivedTradeUnits.Text = calcTradeUnits.ToString("n2");
                    tbReceivedPrice.Text = calcPrice.ToString("n2");

                    //tbPriceDifference.Text = (dealSchedule.DispatchLoadPriceDifference - calcPrice).ToString("n2");
                    //tbTradeUnitsDifference.Text = (dealSchedule.DispatchLoadTradeUnitDifference - calcTradeUnits).ToString("n2");
                    //lrdTbTradeUnits.Text = calcTradeUnits.ToString("n2");
                    //lrdTbPrice.Text = calcPrice.ToString("n2");
                }

                ////tbReceivedPackings.Text = dealSchedule.LoadedPackingsUnits.ToString("n2");
                ////tbPackingDifference.Text = string.Format("{0} {1}-(s)", packingsDifference.ToString("n2"), _deal.Packing.Name);

                tbTradeUnitsDifference.Text = string.Format("{0} {1}-(s)", dealSchedule.AllTradeUnitDifference.ToString("n2"), _deal.TradeUnit.Title);
                tbPriceDifference.Text = string.Format("{0}", dealSchedule.AllPriceDifference.ToString("n2"));
                tbTotalQtyDifference.Text = string.Format("{0} {1}-(s)", dealSchedule.AllSubTradeUnitDifference.ToString("n2"), _deal.PackingUnit.Name);

                lrdTbTradeUnits.Text = string.Format("{0} {1}-(s)", dealSchedule.DiffLoadedTradeUnits.ToString("n2"), _deal.TradeUnit.Title);
                lrdTbPrice.Text = string.Format("{0}", dealSchedule.DiffLoadedPrice.ToString("n2"));
                lrdTbTotalQty.Text = string.Format("{0} {1}-(s)", dealSchedule.DiffLoadedSubTradeUnits.ToString("n2"), _deal.PackingUnit.Name);

                //tbReceivedTotalQty.Text = dealSchedule.DiffLoadedSubTradeUnits.ToString("n2");
                //tbReceivedPrice.Text = dealSchedule.DiffLoadedPrice.ToString("n2");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dgvReceived_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 || e.RowIndex == dgvReceived.NewRowIndex)
                    return;

                if(e.ColumnIndex != 3)
                {
                    dgvReceived.CurrentCell.ReadOnly = true;
                }
                int id = dgvReceived.CurrentRow.Cells[0].Value.ToInt();

                var obj = packingReceiveEditAbleBindingSource.List.OfType<PackingReceiveEditAble>().FirstOrDefault(a => a.Id == id);
                if (obj.QtyReceived > obj.QtyLoaded)
                {
                    obj.QtyReceived = obj.QtyLoaded;
                    throw new Exception("Qty received can't be greater than qty loaded.");
                }

                decimal recQty = packingReceiveEditAbleBindingSource.List.OfType<PackingReceiveEditAble>().Sum(a => a.QtyReceived);
                tbReceivedPackings.Text = recQty.ToString("n1");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }

        }

        private void btnAddWeighBridge_Click(object sender, EventArgs e)
        {
            try
            {
                AddWeighBridgeForm form = new AddWeighBridgeForm(WeighBridgeType.Receiving);
                form.ShowDialog();
                int id = form.weighBridgeId;
                if (id != 0)
                {
                    WaitForm form2 = new WaitForm(LoadWeighBridges);
                    form2.ShowDialog();

                    BindCBWeighBridges();

                    cbWeighBridges.SelectedItem = cbWeighBridges.Items.OfType<WeighBridge>().FirstOrDefault(a => a.Id == id);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dgvReceived_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex == -1 || e.RowIndex == dgvReceived.NewRowIndex)
                return;

            if (e.ColumnIndex != 3)
            {
                dgvReceived.CurrentCell.ReadOnly = true;
            }
        }

        private void tbReceivedPackings_TextChanged(object sender, EventArgs e)
        {
            decimal recPacks = 0;
            if(!string.IsNullOrEmpty(tbReceivedPackings.Text))
            {
                recPacks = tbReceivedPackings.Text.ToDecimal();
            }

            decimal loadPacks = tbSchedulePackings.Text.ToDecimal();
            dealSchedule.ReceivedPackingsUnits = recPacks;
            dealSchedule.DiffLoadedPackingUnits = dealSchedule.LoadedPackingsUnits - recPacks;
            //decimal diff = loadPacks - recPacks;
            lrdTbPackings.Text = dealSchedule.DiffLoadedPackingUnits.ToString("n1");
            tbPackingDifference.Text = dealSchedule.AllPackingDifference.ToString("n1");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void tbTradeUnitsDifference_TextChanged(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void tbLaborCharges_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal perPackLabor = 0;
                string perPackStr = tbLaborCharges.Text;
                if(!string.IsNullOrEmpty(perPackStr))
                {
                    perPackLabor = perPackStr.ToDecimal();
                }
                decimal packings = 0;
                string packStr = tbReceivedPackings.Text;
                if(!string.IsNullOrEmpty(packStr))
                {
                    packings = packStr.ToDecimal();
                }

                decimal totalLabor = perPackLabor * packings;
                tbTotalLaborCharges.Text = totalLabor.ToString("n2");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbPerBagTracktorLabor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal perPackLabor = 0;
                string perPackStr = tbPerBagTracktorLabor.Text;
                if (!string.IsNullOrEmpty(perPackStr))
                {
                    perPackLabor = perPackStr.ToDecimal();
                }
                decimal packings = 0;
                string packStr = tbReceivedPackings.Text;
                if (!string.IsNullOrEmpty(packStr))
                {
                    packings = packStr.ToDecimal();
                }

                decimal totalLabor = perPackLabor * packings;
                tbTracktorLaborAmount.Text = totalLabor.ToString("n2");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
