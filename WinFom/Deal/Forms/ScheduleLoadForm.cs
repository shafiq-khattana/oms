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
using Model.Admin.Model;
using WinFom.Common.Model;
using Model.Financials.Model;
using Model.Employees.Model;
using WinFom.Employees.Forms;

namespace WinFom.Deal.Forms
{
    public partial class ScheduleLoadForm : Form
    {
        private int _scheduleId = 0;
        private DealSchedule dealSchedule = null;
        private AppDeal _deal = null;
        private List<Employee> selectors = new List<Employee>();
        private List<WeighBridge> weighBridges = new List<WeighBridge>();
        public bool IsDone = false;
        private string btndgvdel = "asdf2342342";
        private List<DealPacking> dealPackings = new List<DealPacking>();
        private AppSettings appSett = Helper.AppSet;
        private List<DayBook> printDaybooks = new List<DayBook>();
        public ScheduleLoadForm(int scheduleId)
        {
            InitializeComponent();
            _scheduleId = scheduleId;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadSelectors()
        {
            try
            {
                using (Context db = new Context())
                {
                    selectors = db.Employees.Where(a => a.EmployeeType == EmployeeType.ScheduleSelector)
                        .OrderBy(a => a.Name).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadWeighBridges()
        {
            try
            {
                using (Context db = new Context())
                {
                    weighBridges = db.WeighBridges.Where(a => a.WeighBrideType == WeighBridgeType.Loading).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindWeighBridges()
        {
            cbWeighBridges.DataSource = weighBridges;
            cbWeighBridges.DisplayMember = "Name";
            cbWeighBridges.ValueMember = "Id";
        }
        private void BindSelectors()
        {
            cbSelectors.DataSource = selectors;
            cbSelectors.DisplayMember = "Name";
            cbSelectors.ValueMember = "Id";
        }

        private void LoadDealSchedule()
        {
            try
            {
                using (Context db = new Context())
                {
                    dealSchedule = db.DealSchedules.FirstOrDefault(a => a.Id == _scheduleId);
                    _deal = db.AppDeals.Find(dealSchedule.AppDealId);
                    _deal.Company = db.Companies.Find(_deal.CompanyId);
                    _deal.Broker = db.Brokers.Find(_deal.BrokerId);
                    _deal.DealItem = db.DealItems.Find(_deal.DealItemId);
                    _deal.Packing = db.DealPackings.Find(_deal.DealPackingId);
                    _deal.TradeUnit = db.TradeUnits.Find(_deal.TradeUnitId);
                    _deal.RawBrokerShareType = db.RawBrokerShares.Find(_deal.RawBrokerShareTypeId);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void LoadPackings()
        {
            try
            {
                using (Context db = new Context())
                {
                    dealPackings = db.DealPackings.OrderBy(a => a.Name).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindPackings()
        {
            cbPackings.DataSource = dealPackings;
            cbPackings.DisplayMember = "Name";
            cbPackings.ValueMember = "Id";
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait = new WaitForm(LoadSelectors);
                wait.ShowDialog();

                BindSelectors();
                WaitForm wait3 = new WaitForm(LoadWeighBridges);
                wait3.ShowDialog();
                BindWeighBridges();

                WaitForm wait2 = new WaitForm(LoadDealSchedule);
                wait2.ShowDialog();

                WaitForm wait4 = new WaitForm(LoadPackings);
                wait4.ShowDialog();
                BindPackings();
                Helper.IsOkApplied();
                tbBroker.Text = _deal.Broker.Name;
                tbCompany.Text = _deal.Company.Name;
                tbDealDate.Text = _deal.DealDate.ToShortDateString();
                tbDealItem.Text = _deal.DealItem.Name;
                tbDealNo.Text = _deal.Id.ToString();
                tbScheduledTradeUnits.Text = dealSchedule.ScheduledTradeUnits.ToString("n2");
                tbSchedulePackings.Text = dealSchedule.ScheduledPackingsUnits.ToString("n2");
                tbSchedulePrice.Text = dealSchedule.ScheduledPrice.ToString("n2");
                tbScheduleTotalQty.Text = dealSchedule.ScheduledSubTradeUnits.ToString("n2");

                lblPackings.Text = label22.Text =  _deal.Packing.Name;
                lblTradeUnit.Text = lblTradeUnits2.Text = label20.Text = dealSchedule.TradeUnitTitle;
                lblTotalQty.Text = lblTotalQty2.Text = label24.Text = dealSchedule.PackingUnitTitle;

                //tbLoadedPackings.Text = dealSchedule.ScheduledPackingsUnits.ToString("n2");
                Gujjar.AddDatagridviewButton(dgv, btndgvdel, "Delete", "Delete", 70);

                AddPackingVM vm = new AddPackingVM
                {
                    Id = Guid.NewGuid().ToString(),
                    Packing = _deal.Packing.Name,
                    PackingId = _deal.Packing.Id,
                    Qty = dealSchedule.ScheduledPackingsUnits
                };

                addPackingVMBindingSource.List.Add(vm);
                dgv.Refresh();
                tbTotalPacking.Text = dealSchedule.ScheduledPackingsUnits.ToString("n2");
                label18.Text = _deal.Packing.Name;
                dtp.MinDate = appSett.StartDate;
                dtp.MaxDate = appSett.EndDate;

                tbBrokerPerPackPercentage.Text = _deal.BrokerSharePerPackPercentage.ToString();
                string lblMsg = "N/A";
                switch(_deal.RawBrokerShareType.Title)
                {
                    case "Per Packing":
                        lblMsg = "Per packing rate:";
                        break;
                    case "Percetange":
                        lblMsg = "Share percentage (%):";
                        break;
                    case "None":
                        lblMsg = "No broker share";
                        break;
                }
                label27.Text = lblMsg;
                tbBrokerShareType.Text = _deal.RawBrokerShareType.Title;
                tbDealUnitprice.Text = _deal.TradeUnitPrice.ToString("n2");
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
                //string packingStr = tbLoadedPackings.Text;
                //if (string.IsNullOrEmpty(packingStr))
                //{
                //    tbLoadedPackings.Focus();
                //    tbLoadedPackings.BackColor = Color.Pink;
                //    return;
                //    //throw new Exception(string.Format("Please enter no of {0}", _packing));
                //}

                //decimal packingAmnt = packingStr.ToDecimal();
                //if (packingAmnt > _remainingPackingUnits)
                //{
                //    string msg = string.Format("Remaining {0} {1}-(s) and now scheduled {2} {3}-(s). Please re-check. Thanks", _remainingPackingUnits,
                //        _packing, packingAmnt, _packing);
                //    throw new Exception(msg);
                //}
               // decimal totalKgs = packingAmnt * _deal.PerPackingQty;
                //decimal totalTradeUnits = totalKgs / _deal.TradeUnit.Qty;
                //decimal price = totalTradeUnits * _deal.TradeUnitPrice;
                ////decimal totalTradeSubUnits = totalKgs;
                //tbLoadedTradeUnits.Text = totalTradeUnits.ToString("n4");
                //tbLoadedTotalQty.Text = totalKgs.ToString("n4");
                //tbLoadedPrice.Text = price.ToString("n4");

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddSelector_Click(object sender, EventArgs e)
        {
            try
            {
                AddEmployeeForm form = new AddEmployeeForm(EmployeeType.ScheduleSelector);
                form.ShowDialog();

                if(form.EmployeeId != 0)
                {
                    int sid = form.EmployeeId;
                    LoadSelectors();
                    BindSelectors();

                    cbSelectors.SelectedItem = cbSelectors.Items.OfType<Employee>()
                        .FirstOrDefault(a => a.Id == sid);
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
                if(!Gujjar.IsValidForm(panel2))
                {
                    throw new Exception("Please fill all text fields");
                }
                if(cbSelectors.SelectedIndex == -1)
                {
                    throw new Exception("Please select any selector form the list");
                }
                if(cbWeighBridges.SelectedIndex == -1 || cbWeighBridges.Text == "N/A")
                {
                    throw new Exception("Please select any weigh bridge");
                }

                if (!Helper.ConfirmUserPassword())
                    return;

                DialogResult res = Gujjar.ConfirmYesNo("Please confirm, all information is correct..!!");
                if (res == DialogResult.No)
                    return;

                Employee selector = cbSelectors.SelectedItem as Employee;
                WeighBridge weighBridge = cbWeighBridges.SelectedItem as WeighBridge;

                
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                           

                            DealSchedule deal2 = db.DealSchedules.Find(_scheduleId);

                            //GeneralAccount itemAccount = db.Accounts.OfType<GeneralAccount>()
                            //    .FirstOrDefault(a => a.Id == _deal.DealItem.GeneralAccountId);
                            //GeneralAccount compAccount = db.Accounts.OfType<GeneralAccount>()
                            //    .FirstOrDefault(a => a.Id == _deal.Company.GeneralAccountId);
                            string description = string.Format("Purchase of item ({0}), qty ({1}-{2}) in amount: {3}-/ from ({4}).",
                                _deal.DealItem.Name, tbLoadedTotalQty.Text.ToString(), lblTotalQty2.Text.ToString(), 
                                tbLoadedPrice.Text.ToString(), _deal.Company.Name);

                            #region Trans btw Purchased item (debit) and creditor (credit)
                            DayBook daybook = new DayBook
                            {
                                Id = 0,
                                Date = dtp.Value,
                                CreditTrace = "N/A",
                                DebitTrace = "N/A",
                                Description = description,
                                Amount = tbLoadedPrice.Text.ToDecimal(),
                                CanRollBack = false,
                                DealScheduleId = deal2.Id,
                                InDate = DateTime.Now.Date
                            };
                            daybook = db.DayBooks.Add(daybook);
                            db.SaveChanges();
                            AccountTransaction debitTransaction = new AccountTransaction
                            {
                                Account = null,
                                GeneralAccountId = _deal.DealItem.GeneralAccountId,
                                Id = 0,
                                AccountTransactionType = AccountTransactionType.Debit,
                                Balance = tbLoadedPrice.Text.ToDecimal(),
                                CreditAmount = 0,
                                DebitAmount = tbLoadedPrice.Text.ToDecimal(),
                                Date = dtp.Value,
                                DayBookId = daybook.Id,
                                Description = description
                            };
                            var trans1 = db.AccountTransactions
                                .Where(a => a.GeneralAccountId == _deal.DealItem.GeneralAccountId)
                                .OrderByDescending(a => a.Id).FirstOrDefault();
                            
                            if (trans1 != null)
                            {
                                debitTransaction.Balance += trans1.Balance;
                            }
                            debitTransaction = db.AccountTransactions.Add(debitTransaction);
                            db.SaveChanges();

                            AccountTransaction creditTrans = new AccountTransaction
                            {
                                Account = null,
                                GeneralAccountId = _deal.Company.GeneralAccountId,
                                Id = 0,
                                AccountTransactionType = AccountTransactionType.Credit,
                                Balance = tbLoadedPrice.Text.ToDecimal(),
                                CreditAmount = tbLoadedPrice.Text.ToDecimal(),
                                DebitAmount = 0,
                                Date = dtp.Value,
                                DayBookId = daybook.Id,
                                Description = description
                            };
                            var trans2 = db.AccountTransactions.Where(a => a.GeneralAccountId == _deal.Company.GeneralAccountId)
                                .OrderByDescending(a => a.Id).FirstOrDefault();
                            if (trans2 != null)
                            {
                                creditTrans.Balance += trans2.Balance;
                            }
                            creditTrans = db.AccountTransactions.Add(creditTrans);
                            db.SaveChanges();

                            var cAcct1 = db.Accounts.OfType<GeneralAccount>().FirstOrDefault(a => a.Id == _deal.Company.GeneralAccountId);
                            var dAcct1 = db.Accounts.OfType<GeneralAccount>().FirstOrDefault(a => a.Id == _deal.DealItem.GeneralAccountId);
                            var daybook2 = db.DayBooks.Find(daybook.Id);
                            daybook2.DebitTrace = string.Format("({0}). Trans Id {1}", dAcct1.Title, debitTransaction.Id);
                            daybook2.CreditTrace = string.Format("({0}). Trans Id {1}", cAcct1.Title, creditTrans.Id);
                            daybook2.DebitAccountId = dAcct1.Id;
                            daybook2.CreditAccountId = cAcct1.Id;

                            db.Entry(daybook2).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            printDaybooks.Add(daybook2);
                            #endregion

                            #region Trans btw Broker expense (debit) and Broker (credit)
                            string shareAmountStr = tbBrokerShareAmount.Text;
                            decimal shareAmount = 0;
                            if(!string.IsNullOrEmpty(shareAmountStr))
                            {
                                shareAmount = shareAmountStr.ToDecimal();
                            }
                            if(_deal.BrokerShareAmount <= 0)
                            {
                                _deal.BrokerShareAmount = shareAmount;
                            }

                            if(_deal.RawBrokerShareTypeId != 3 && _deal.BrokerId != 1 && _deal.BrokerShareAmount > 0)
                            {
                                decimal brokeryExpense = tbBrokerShareAmount.Text.ToDecimal();

                                string financialMessage = string.Format("Accrued brokery expense for ({0}), Share type ({1}), total packings ({2}), total price ({3}), broker share amount ({4}), broker name ({5}).",
                                string.Format("Deal: {0}, Schedule: {1}", _deal.Id, dealSchedule.Id), _deal.RawBrokerShareType.Title, tbTotalPacking.Text, tbLoadedPrice.Text, tbBrokerShareAmount.Text, tbBroker.Text);
                                DayBook daybook3 = new DayBook
                                {
                                    Id = 0,
                                    CreditTrace = "",
                                    DebitTrace = "",
                                    Description = financialMessage,
                                    Date = dtp.Value,
                                    Amount = brokeryExpense,
                                    CanRollBack = false,
                                    DealScheduleId = deal2.Id,
                                    InDate = DateTime.Now.Date
                                };
                                daybook3 = db.DayBooks.Add(daybook3);
                                db.SaveChanges();

                                
                                var brokeryExpenseDebit = new AccountTransaction
                                {
                                    Id = 0,
                                    Account = null,
                                    AccountTransactionType = AccountTransactionType.Debit,
                                    CreditAmount = 0,
                                    Balance = brokeryExpense,
                                    Date = dtp.Value,
                                    DayBookId = daybook3.Id,
                                    DebitAmount = brokeryExpense,
                                    Description = financialMessage,
                                    GeneralAccountId = Properties.Resources.BrokeryExpenseAccount
                                };
                                var dbBrokeryExpense = db.AccountTransactions.
                                    Where(a => a.GeneralAccountId == Properties.Resources.BrokeryExpenseAccount).
                                    OrderByDescending(a => a.Id).FirstOrDefault();
                                if (dbBrokeryExpense != null)
                                {
                                    brokeryExpenseDebit.Balance += dbBrokeryExpense.Balance;
                                }
                                brokeryExpenseDebit = db.AccountTransactions.Add(brokeryExpenseDebit);
                                db.SaveChanges();

                                AccountTransaction brokerCreditTransaction = new AccountTransaction
                                {
                                    Id = 0,
                                    Balance = brokeryExpense,
                                    Account = null,
                                    AccountTransactionType = AccountTransactionType.Credit,
                                    CreditAmount = brokeryExpense,
                                    Date = dtp.Value,
                                    DayBookId = daybook3.Id,
                                    DebitAmount = 0,
                                    Description = financialMessage,
                                    GeneralAccountId = _deal.Broker.GeneralAccountId
                                };

                                var brokerCreditDbEntry = db.AccountTransactions
                                    .Where(a => a.GeneralAccountId == _deal.Broker.GeneralAccountId)
                                    .OrderByDescending(a => a.Id).FirstOrDefault();

                                if (brokerCreditDbEntry != null)
                                {
                                    brokerCreditTransaction.Balance += brokerCreditDbEntry.Balance;
                                }
                                brokerCreditTransaction = db.AccountTransactions.Add(brokerCreditTransaction);
                                db.SaveChanges();

                                var cAcct = db.Accounts.OfType<GeneralAccount>().FirstOrDefault(a => a.Id == _deal.Broker.GeneralAccountId);
                                var dAcct = db.Accounts.OfType<GeneralAccount>().FirstOrDefault(a => a.Id == Properties.Resources.BrokeryExpenseAccount);

                                var dbDayBook = db.DayBooks.Find(daybook3.Id);
                                dbDayBook.CreditTrace = string.Format("{0}. Trans Id: {1}", cAcct.Title, brokerCreditTransaction.Id);
                                dbDayBook.DebitTrace = string.Format("{0}. Trans Id: {1}", dAcct.Title, brokeryExpenseDebit.Id);
                                dbDayBook.DebitAccountId = dAcct.Id;
                                dbDayBook.CreditAccountId = cAcct.Id;

                                db.Entry(dbDayBook).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();

                                printDaybooks.Add(dbDayBook);
                            }
                           
                            #endregion
                            deal2.LoadedDate = dtp.Value;
                            deal2.EmployeeId = selector.Id;
                            
                            
                            deal2.LoadedPackingsUnits = tbTotalPacking.Text.ToDecimal();
                            deal2.LoadedPrice = tbLoadedPrice.Text.ToDecimal();
                            deal2.LoadedSubTradeUnits = tbLoadedTotalQty.Text.ToDecimal();
                            deal2.LoadedTradeUnits = tbLoadedTradeUnits.Text.ToDecimal();

                            deal2.DispatchLoadPackingDifference = tbPackingDifference.Text.ToDecimal();
                            deal2.DispatchLoadPriceDifference = tbTradeUnitsDifference.Text.ToDecimal() * _deal.TradeUnitPrice;
                            deal2.DispatchLoadSubTradeUnitDifference = tbTotalDifference.Text.ToDecimal();
                            deal2.DispatchLoadTradeUnitDifference = tbTradeUnitsDifference.Text.ToDecimal();

                            ScheduleStatus oldStatus = deal2.Status;
                            deal2.Status = ScheduleStatus.Loaded;
                            deal2.IsLoaded = true;
                            deal2.LoadRemarks = tbRemarks.Text;
                            db.Entry(deal2).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            var vehicleObj = db.Vehicles.Find(deal2.VehicleId);
                            vehicleObj.Status = VehicleStatus.Loaded;
                            db.Entry(vehicleObj).State = System.Data.Entity.EntityState.Modified;


                            var schDbObj = db.DealSchedules.Find(deal2.Id);
                            schDbObj.TransId = daybook.Id;
                            db.Entry(schDbObj).State = System.Data.Entity.EntityState.Modified;

                            db.SaveChanges();

                            ScheduleWeighBridge swb = new ScheduleWeighBridge
                            {
                                DealScheduleId = dealSchedule.Id,
                                WeighBridgeId = weighBridge.Id,
                                Type = ScheduleWeighBridgeType.Load
                            };

                            db.ScheduleWeighBridges.Add(swb);
                            db.SaveChanges();

                            var packObjs = db.ScheduleLoadPackings.Where(a => a.DealScheduleId == dealSchedule.Id).ToList();
                            foreach (var item in packObjs)
                            {
                                db.ScheduleLoadPackings.Remove(item);

                            }
                            db.SaveChanges();
                            foreach (var item in addPackingVMBindingSource.List.OfType<AddPackingVM>())
                            {
                                ScheduleLoadPacking slp = new ScheduleLoadPacking
                                {
                                    DealPackingId = item.PackingId,
                                    DealScheduleId = dealSchedule.Id,
                                    LoadQty = item.Qty,
                                    ReceiveQty = item.Qty
                                };

                                db.ScheduleLoadPackings.Add(slp);
                            }
                            db.SaveChanges();

                            trans.Commit();
                            if(appSett.PrintFinancialTransactions)
                            {
                                Helper.PrintTransactions(printDaybooks);
                            }
                            Gujjar.InfoMsg(string.Format("Schedule status is changed to ({0}) to ({1}).", oldStatus, deal2.Status));
                            IsDone = true;
                            Close();
                        }
                        catch (Exception abcd)
                        {
                            trans.Rollback();
                            throw abcd;
                        }
                    } 
                }
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
                AddWeighBridgeForm form = new AddWeighBridgeForm(WeighBridgeType.Loading);
                form.ShowDialog();
                int id = form.weighBridgeId;
                if(id != 0)
                {
                    WaitForm form2 = new WaitForm(LoadWeighBridges);
                    form2.ShowDialog();

                    BindWeighBridges();

                    cbWeighBridges.SelectedItem = cbWeighBridges.Items.OfType<WeighBridge>().FirstOrDefault(a => a.Id == id);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddPacking_Click(object sender, EventArgs e)
        {
            try
            {
                if(cbPackings.SelectedIndex == -1)
                {
                    throw new Exception("Please select packing first");
                }
                string qtyStr = tbQty.Text;
                if(string.IsNullOrEmpty(qtyStr))
                {
                    tbQty.BackColor = Color.Pink;
                    tbQty.Focus();

                    throw new Exception("Please enter packing quantity");
                }
                decimal qty = qtyStr.ToDecimal();
                DealPacking pack = cbPackings.SelectedItem as DealPacking;

                AddPackingVM vm = new AddPackingVM
                {
                    Id = Guid.NewGuid().ToString(),
                    Packing = pack.Name,
                    PackingId = pack.Id,
                    Qty = qty
                };

                var obj = addPackingVMBindingSource.List.OfType<AddPackingVM>().FirstOrDefault(a => a.PackingId == pack.Id);
                if(obj != null)
                {
                    obj.Qty += qty;
                    dgv.Refresh();
                }
                else
                {
                    addPackingVMBindingSource.List.Add(vm);
                    dgv.Refresh();
                }

                tbTotalPacking.Text = addPackingVMBindingSource.List.OfType<AddPackingVM>().Sum(a => a.Qty).ToString("n2");
                tbQty.Clear();
                UpdateLoadPackingLabel();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void UpdateLoadPackingLabel()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in addPackingVMBindingSource.List.OfType<AddPackingVM>())
            {
                sb.AppendFormat(string.Format("{0}, ", item.Packing));
            }
            if(sb.Length > 0)
            {
                string packings = sb.ToString().Remove(sb.ToString().LastIndexOf(','), 1);
                label18.Text = packings;
            }
           else
            {
                label18.Text = "N/A";
            }
        }
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int ri = e.RowIndex;
            int ci = e.ColumnIndex;

            if (ri == -1 || ri == dgv.NewRowIndex)
                return;

            string id = dgv.Rows[ri].Cells[0].Value.ToString();

            if(dgv.Columns[btndgvdel].Index == ci)
            {
                var obj = addPackingVMBindingSource.List.OfType<AddPackingVM>().FirstOrDefault(a => a.Id == id);
                addPackingVMBindingSource.List.Remove(obj);
                dgv.Refresh();
            }

            tbTotalPacking.Text = addPackingVMBindingSource.List.OfType<AddPackingVM>().Sum(a => a.Qty).ToString("n2");
            UpdateLoadPackingLabel();
        }

        private void tbTotalPacking_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string packingStr = tbTotalPacking.Text;
                if (string.IsNullOrEmpty(packingStr))
                {
                    
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

                decimal diffPacks = dealSchedule.ScheduledPackingsUnits - packingAmnt;
                decimal diffTradeUnits = dealSchedule.ScheduledTradeUnits - totalTradeUnits;
                decimal diffTotalQty = dealSchedule.ScheduledSubTradeUnits - totalKgs;

                tbTradeUnitsDifference.Text = diffTradeUnits.ToString("n2");
                tbPackingDifference.Text = diffPacks.ToString("n2");
                tbTotalDifference.Text = diffTotalQty.ToString("n2");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbLoadedTotalQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(tbLoadedTotalQty.Text))
                {
                    tbLoadedTradeUnits.Clear();
                    tbLoadedPrice.Clear();
                    return;
                }
                decimal qtyInKg = tbLoadedTotalQty.Text.ToDecimal();
                decimal qtyInTradeUnits = qtyInKg / _deal.TradeUnit.Qty;
                decimal loadedPrice = qtyInTradeUnits * _deal.TradeUnitPrice;

                tbLoadedTradeUnits.Text = qtyInTradeUnits.ToString("n4");
                tbLoadedPrice.Text = loadedPrice.ToString("n4");

                tbTradeUnitsDifference.Text = (dealSchedule.ScheduledTradeUnits - qtyInTradeUnits).ToString("n4");
                tbTotalDifference.Text = (dealSchedule.ScheduledSubTradeUnits - qtyInKg).ToString("n4");

                decimal brokerAmount = 0;
                decimal packings = 0;
                if(!string.IsNullOrEmpty(tbTotalPacking.Text))
                {
                    packings = tbTotalPacking.Text.ToDecimal();
                }

                switch(_deal.RawBrokerShareType.Title)
                {
                    case "Per Packing":
                        brokerAmount = packings * _deal.BrokerSharePerPackPercentage; 
                        break;
                    case "Percetange":
                        brokerAmount = loadedPrice * _deal.BrokerSharePerPackPercentage;
                        break;
                    case "None":
                        brokerAmount = 0;
                        break;
                }
                tbBrokerShareAmount.Text = brokerAmount.ToString("n4");
                _deal.BrokerShareAmount = brokerAmount;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void lblTradeUnit_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void cbSelectors_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
