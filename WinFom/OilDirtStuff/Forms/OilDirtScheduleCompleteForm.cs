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
using Model.ReadyStuff.ViewModel;
using Model.ReadyStuff.Model;
using WinFom.Deal.Forms;
using Model.Admin.Model;
using WinFom.Common.Model;
using WinFom.OilDirtStuff.ViewModel;
using Model.OilDirtStuff.Model;
using Model.Financials.Model;

namespace WinFom.OilDirtStuff.Forms
{
    public partial class OilDirtScheduleCompleteForm : Form
    {
        private OilDirtTradeUnit tradeUnit = null;
        private List<OilDirtTradeUnit> tradeUnits = null;
        private OilDirtSchCompTransfer schCompTransfer = null;
        private OilDirtSchedule readySchedule = null;
        private List<WeighBridge> weighBridges = null;
        public bool IsDone = false;
        private OilDirtDeal readyDeal = null;
        private AppSettings AppSett = Helper.AppSet;
        private AppUser AppUser = SingleTon.LoginForm.appUser;
        public OilDirtScheduleCompleteForm(OilDirtSchCompTransfer sct, OilDirtSchedule rs)
        {
            InitializeComponent();
            schCompTransfer = sct;
            readySchedule = rs;
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
                    tradeUnits = db.OilDirtTradeUnits.ToList();
                    readyDeal = db.OilDirtDeals.Find(readySchedule.OilDirtDealId);
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
                WaitForm form = new WaitForm(LoadTradeUnits);
                form.ShowDialog();

                BindCBTradeUnits();

                WaitForm form2 = new WaitForm(LoadWeighBridges);
                form2.ShowDialog();
                BindCBWeighBridges();
                Helper.IsOkApplied();
                Gujjar.NumbersOnly(tbBrokerSharePercentage);
                Gujjar.NumbersOnly(tbPerTradeUnitPrice);
                Gujjar.NumbersOnly(tbWeighBridgeWeight);
                Gujjar.NumbersOnly(tbFullVehicleWeight);

                tbBroker.Text = schCompTransfer.Broker;
                tbDealItem.Text = schCompTransfer.Item;
                tbDriver.Text = schCompTransfer.Driver;
                tbReadyDate.Text = schCompTransfer.ReadyDate;
                tbScheduleNo.Text = schCompTransfer.ScheduleNo;
                tbVehicleNo.Text = schCompTransfer.VehicleNo;


                tbEmptyVehicleWeight.Text = readySchedule.VehicleWeightEmpty.ToString("n4");
                tbPerTradeUnitPrice.Text = readySchedule.PerTradeUnitPrice.ToString("n3");
                tbGrossWeight.Text = readySchedule.LoadedQty.ToString("n4");
                tbBrokerSharePercentage.Enabled = AppSett.AllowToChangeMailBrokerSharePercentage;
                tbBrokerSharePercentage.Text = AppSett.MailBrokerSharePercentage.ToString("n3");
                tbRate.Text = readyDeal.PerTradeUnitPrice.ToString("n3");
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
            cbTradeUnits.SelectedItem = cbTradeUnits.Items.OfType<OilDirtTradeUnit>()
                .FirstOrDefault(a => a.Id == readyDeal.OilDirtTradeUnitId);
            cbTradeUnits.Enabled = false;
        }
        private void cbTradeUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTradeUnits.SelectedIndex == -1)
                return;

            tradeUnit = cbTradeUnits.SelectedItem as OilDirtTradeUnit;
            string title = tradeUnit.Title;
            decimal qty = tradeUnit.UnitQty;
            if (qty == 0)
            {
                qty = 1;
            }
            label11.Text = string.Format("Per {0} qty:", title);
            label12.Text = string.Format("Total {0}(s):", title);
            label13.Text = string.Format("Per {0} price:", title);
            tbPerTradeUnitQty.Text = qty.ToString("n4");

            string totalWeightStr = tbWeighBridgeWeight.Text;
            if (!string.IsNullOrEmpty(totalWeightStr))
            {
                decimal totalWeight = totalWeightStr.ToDecimal();
                decimal totalTradeUnits = totalWeight / qty;

                tbTotalTradeUnits.Text = totalTradeUnits.ToString("n4");
            }
            else
            {
                tbTotalTradeUnits.Text = "0";
            }
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

        private void tbWeighBridgeWeight_TextChanged(object sender, EventArgs e)
        {
            decimal wweight = 0;
            if(!string.IsNullOrEmpty(tbWeighBridgeWeight.Text))
            {
                wweight = tbWeighBridgeWeight.Text.ToDecimal();
            }

            decimal total = 0;
            if(!string.IsNullOrEmpty(tbGrossWeight.Text))
            {
                total = tbGrossWeight.Text.ToDecimal();
            }

            decimal diff = total - wweight;
            tbWeightDifference.Text = diff.ToString("n4");

            decimal perTradeUnitQty = 0;
            string txt = tbPerTradeUnitQty.Text;
            if(!string.IsNullOrEmpty(txt))
            {
                perTradeUnitQty = txt.ToDecimal();
            }
            if (perTradeUnitQty == 0)
                perTradeUnitQty = 1;
            decimal totalTradeUnits = wweight / perTradeUnitQty;

            tbTotalTradeUnits.Text = totalTradeUnits.ToString("n4");

            decimal perTradeUnitPrice = 0;
            string txt2 = tbPerTradeUnitPrice.Text;
            if(!string.IsNullOrEmpty(txt2))
            {
                perTradeUnitPrice = txt2.ToDecimal();
            }
            decimal totalGrossPrice = totalTradeUnits * perTradeUnitPrice;
            tbTotalPrice.Text = totalGrossPrice.ToString("n4");

            decimal brokerSharePercentage = 0;
            string txt3 = tbBrokerSharePercentage.Text;
            if(!string.IsNullOrEmpty(txt3))
            {
                brokerSharePercentage = txt3.ToDecimal();
            }
            decimal brokerShareAmount = totalGrossPrice * brokerSharePercentage / 100;
            decimal netprice = totalGrossPrice - brokerShareAmount;
            tbBrokerShare.Text = brokerShareAmount.ToString("n4");
            tbNetPrice.Text = netprice.ToString("n4");
        }

        private void tbPerTradeUnitPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal totalTradeUnits = 0;
                if (!string.IsNullOrEmpty(tbTotalTradeUnits.Text))
                {
                    totalTradeUnits = tbTotalTradeUnits.Text.ToDecimal();
                }
                decimal perPrice = 0;
                if (!string.IsNullOrEmpty(tbPerTradeUnitPrice.Text))
                {
                    perPrice = tbPerTradeUnitPrice.Text.ToDecimal();
                }
                decimal totalPrice = totalTradeUnits * perPrice;

                tbTotalPrice.Text = totalPrice.ToString("n4");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbBrokerSharePercentage_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal totalPrice = 0;
                if (!string.IsNullOrEmpty(tbTotalPrice.Text))
                {
                    totalPrice = tbTotalPrice.Text.ToDecimal();
                }
                decimal bshper = 0;
                if (!string.IsNullOrEmpty(tbBrokerSharePercentage.Text))
                {
                    bshper = tbBrokerSharePercentage.Text.ToDecimal();

                }

                decimal brokerShare = totalPrice * bshper / 100;
                decimal netprice = totalPrice - brokerShare;
                tbBrokerShare.Text = brokerShare.ToString("n4");
                tbNetPrice.Text = netprice.ToString("n4");

            }
            catch (Exception)
            {
                //Gujjar.ErrMsg(ep);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private List<DayBook> pDaybooks = new List<DayBook>();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Helper.ConfirmUserPassword())
                {
                    return;
                }
                DialogResult result = Gujjar.ConfirmYesNo("Are you sured? please confirm..!!");
                if (result == DialogResult.No)
                    return;

                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }
                if(cbWeighBridges.SelectedIndex == -1)
                {
                    throw new Exception("Please choose weigh bridge");
                }

                WeighBridge weighBridge = cbWeighBridges.SelectedItem as WeighBridge;

                if(tradeUnit.Title == "N/A")
                {
                    throw new Exception("Please choose appropriate trade unit");
                }
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var sch = db.OilDirtSchedules.Find(readySchedule.Id);

                            sch.WeighBridgeWeight = tbWeighBridgeWeight.Text.ToDecimal();
                            sch.WeighBridgeId = weighBridge.Id;
                            sch.TotalActualAmount = tbTotalPrice.Text.ToDecimal();
                            sch.BrokerSharePercentage = tbBrokerSharePercentage.Text.ToDecimal();
                            sch.BrokerShareAmount = tbBrokerShare.Text.ToDecimal();
                            sch.PerTradeUnitPrice = tbPerTradeUnitPrice.Text.ToDecimal();
                            sch.VehicleWeightFull = tbFullVehicleWeight.Text.ToDecimal();
                            sch.Status = OilDirtScheduleStatus.Completed;
                            sch.TotalTradeUnits = tbTotalTradeUnits.Text.ToDecimal();
                            sch.TotalNetAmount = tbNetPrice.Text.ToDecimal();
                            db.Entry(sch).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            #region "Financial transactions"
                            string oilDirtItemName = tbDealItem.Text;
                            #region Item whole sale Credit account
                            string wholeSaleFinMessage = string.Format("Whole Sale of item ({0}). Qty ({1}) Kg. Total Price ({2}). Broker ({3}). Brokery ({4}). Net Price ({5}). Schedule No ({6}). By ({7})",
                                                                                oilDirtItemName, sch.WeighBridgeWeight.ToString("n3"), sch.TotalActualAmount.ToString("n3"), schCompTransfer.Broker,
                                                                                sch.BrokerShareAmount.ToString("n3"), sch.TotalNetAmount.ToString("n3"), string.Format("D: {0}, Sch: {1}", readyDeal.Id, sch.Id), AppUser.Name);
                            decimal grossPrice = sch.TotalActualAmount;
                            DayBook wholeSaleItemDayBook = new DayBook
                            {
                                Id = 0,
                                Amount = grossPrice,
                                Date = dtp.Value,
                                Description = wholeSaleFinMessage,
                                CanRollBack = false,
                                OilDirtScheduleId = sch.Id,
                                InDate = DateTime.Now.Date
                            };
                            wholeSaleItemDayBook = db.DayBooks.Add(wholeSaleItemDayBook);
                            db.SaveChanges();

                            OilDirtItem oilDirtItem = db.OilDirtItems.Find(readyDeal.OilDirtItemId);
                            GeneralAccount wholeSaleItemAccount = db.Accounts.Find(oilDirtItem.GeneralAccountId) as GeneralAccount;

                            AccountTransaction itemCreditTrans = new AccountTransaction
                            {
                                Id = 0,
                                GeneralAccountId = wholeSaleItemAccount.Id,
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Credit,
                                CreditAmount = grossPrice,
                                Balance = grossPrice,
                                Date = dtp.Value,
                                DayBookId = wholeSaleItemDayBook.Id,
                                DebitAmount = 0,
                                Description = wholeSaleFinMessage
                            };

                            var itemDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == wholeSaleItemAccount.Id)
                                .AsParallel().ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                .OrderByDescending(a => a.Id)
                                .FirstOrDefault();
                            if (itemDbEntry != null)
                            {
                                itemCreditTrans.Balance += itemDbEntry.Balance;
                            }
                            itemCreditTrans = db.AccountTransactions.Add(itemCreditTrans);
                            db.SaveChanges();

                            var wholeSaleItemDayBookDb = db.DayBooks.Find(wholeSaleItemDayBook.Id);
                            wholeSaleItemDayBookDb.CreditTrace = string.Format("{0}. Trans Id: {1}", wholeSaleItemAccount.Title, itemCreditTrans.Id);
                            wholeSaleItemDayBookDb.DebitTrace = "N/A";
                            wholeSaleItemDayBookDb.CreditAccountId = wholeSaleItemAccount.Id;
                            wholeSaleItemDayBookDb.DebitAccountId = null;
                            db.Entry(wholeSaleItemDayBookDb).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            pDaybooks.Add(wholeSaleItemDayBookDb);


                            #endregion

                            #region Brokery expense
                            OilDirtBroker oilDirtBroker = db.OilDirtBrokers.Find(readyDeal.OilDirtBrokerId);

                            if (sch.BrokerShareAmount > 0)
                            {
                                string brokeryExpenseMessage = string.Format("Brokery expense amount ({0}). Broker share percetnage ({1}%). Broker share amount ({2}). Total Amount ({3}). Deal No ({4}). By ({5})",
                                sch.BrokerShareAmount.ToString("n3"), sch.BrokerSharePercentage.ToString("n3"), sch.BrokerShareAmount.ToString("n3"), sch.TotalActualAmount.ToString("n3"), string.Format("D: {0}, Sch: {1}", readyDeal.Id, sch.Id), AppUser.Name);

                                DayBook daybookBrokeryExpense = new DayBook
                                {
                                    Id = 0,
                                    DebitTrace = "",
                                    Date = dtp.Value,
                                    Amount = sch.BrokerShareAmount,
                                    Description = brokeryExpenseMessage,
                                    CanRollBack = false,
                                    OilDirtScheduleId = sch.Id,
                                    InDate = DateTime.Now.Date
                                };

                                daybookBrokeryExpense = db.DayBooks.Add(daybookBrokeryExpense);
                                db.SaveChanges();

                                GeneralAccount brokeryExpenseAccount = db.Accounts.Find(oilDirtBroker.BrokerExpenseAccountId) as GeneralAccount;
                                decimal brokeryExpenseAmount = sch.BrokerShareAmount;
                                AccountTransaction brokeryExpenseDebitTrans = new AccountTransaction
                                {
                                    Id = 0,
                                    Account = null,
                                    AccountTransactionType = AccountTransactionType.Debit,
                                    Balance = brokeryExpenseAmount,
                                    CreditAmount = 0,
                                    Date = DateTime.Now,
                                    DayBookId = daybookBrokeryExpense.Id,
                                    Description = brokeryExpenseMessage,
                                    DebitAmount = brokeryExpenseAmount,
                                    GeneralAccountId = brokeryExpenseAccount.Id
                                };

                                var expenseDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == brokeryExpenseAccount.Id)
                                    .AsParallel().ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                    .OrderByDescending(a => a.Id)
                                    .FirstOrDefault();
                                if (expenseDbEntry != null)
                                {
                                    brokeryExpenseDebitTrans.Balance += expenseDbEntry.Balance;
                                }
                                brokeryExpenseDebitTrans = db.AccountTransactions.Add(brokeryExpenseDebitTrans);
                                db.SaveChanges();

                                var daybookExpenseDb = db.DayBooks.Find(daybookBrokeryExpense.Id);
                                daybookExpenseDb.DebitTrace = string.Format("{0}. Trans Id: {1}", brokeryExpenseAccount.Title, brokeryExpenseDebitTrans.Id);
                                daybookExpenseDb.CreditTrace = "N/A";
                                daybookExpenseDb.DebitAccountId = brokeryExpenseAccount.Id;
                                daybookExpenseDb.CreditAccountId = null;
                                db.Entry(daybookExpenseDb).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();

                                pDaybooks.Add(wholeSaleItemDayBookDb);
                            }
                            

                            #endregion

                            #region Broker Receivable amount
                            decimal netAoumnt = sch.TotalNetAmount;
                            string brokerRxAmntStr = string.Format("Whole Sale of ({0}), total qty ({1}-kg), total price ({2}), broker ({3}), broker share ({4}), receivable amount ({5})",
                                string.Format("Deal: {0}, Sch: {1}", readyDeal.Id, sch.Id), sch.WeighBridgeWeight.ToString("n3"),
                                sch.TotalActualAmount.ToString("n3"), schCompTransfer.Broker, sch.BrokerShareAmount.ToString("n4"),
                                sch.TotalNetAmount.ToString("n4"));

                            DayBook daybookNet = new DayBook
                            {
                                Id = 0,
                                Amount = netAoumnt,
                                CreditTrace = "",
                                Date = dtp.Value,
                                DebitTrace = "",
                                Description = brokerRxAmntStr,
                                CanRollBack = false,
                                OilDirtScheduleId = sch.Id,
                                InDate = DateTime.Now.Date
                            };
                            daybookNet = db.DayBooks.Add(daybookNet);
                            db.SaveChanges();

                            GeneralAccount brokerDebitAccount = db.Accounts.Find(oilDirtBroker.GeneralAccountId) as GeneralAccount;
                            AccountTransaction netDebitTrans = new AccountTransaction
                            {
                                Account = null,
                                AccountTransactionType = AccountTransactionType.Debit,
                                GeneralAccountId = brokerDebitAccount.Id,
                                CreditAmount = 0,
                                Id = 0,
                                DebitAmount = netAoumnt,
                                Balance = netAoumnt,
                                Date = DateTime.Now,
                                DayBookId = daybookNet.Id,
                                Description = brokerRxAmntStr
                            };

                            var netDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == brokerDebitAccount.Id)
                                .AsParallel().ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                                .OrderByDescending(a => a.Id)
                                .FirstOrDefault();
                            if (netDbEntry != null)
                            {
                                netDebitTrans.Balance += netDbEntry.Balance;
                            }
                            netDebitTrans = db.AccountTransactions.Add(netDebitTrans);
                            db.SaveChanges();

                            var daybooknetdb = db.DayBooks.Find(daybookNet.Id);
                            daybooknetdb.DebitTrace = string.Format("{0}. Trans Id: {1}", brokerDebitAccount.Title, netDebitTrans.Id);
                            daybooknetdb.CreditTrace = "N/A";
                            daybooknetdb.DebitAccountId = brokerDebitAccount.Id;
                            daybooknetdb.CreditAccountId = null;
                            db.Entry(daybooknetdb).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            pDaybooks.Add(daybooknetdb);
                            trans.Commit();

                            if (AppSett.PrintFinancialTransactions)
                            {
                                Helper.PrintTransactions(pDaybooks);
                            }
                            #endregion

                            #endregion



                            Gujjar.InfoMsg("Schedule is completed. Status changed from dispatched to completed");
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

        private void btnAddBharthiType_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    AddReadyTradeUnit form = new AddReadyTradeUnit();
            //    form.ShowDialog();
            //    int did = form.TradeUnitId;
            //    if(did != 0)
            //    {
            //        WaitForm wait2 = new WaitForm(LoadTradeUnits);
            //        wait2.ShowDialog();
            //        BindCBTradeUnits();

            //        cbTradeUnits.SelectedItem = cbTradeUnits.Items.OfType<ReadyTradeUnit>()
            //            .FirstOrDefault(a => a.Id == did);
            //    }
            //}
            //catch (Exception exp)
            //{
            //    Gujjar.ErrMsg(exp);
            //}
        }

        private void cbWeighBridges_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tbFullVehicleWeight_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal emptyWeight = tbEmptyVehicleWeight.Text.ToDecimal();
                decimal fullWeight = 0;
                string txt = tbFullVehicleWeight.Text;
                if(!string.IsNullOrEmpty(txt))
                {
                    fullWeight = txt.ToDecimal();
                }
                decimal diff = fullWeight - emptyWeight;
                tbWeighBridgeWeight.Text = diff.ToString("n4");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
