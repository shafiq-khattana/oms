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
using System.Data.Entity;
using Model.AppBroker.ViewModel;
using WinFom.Common.Forms;

namespace WinFom.AppBCSList.Forms
{
    public partial class AllListForm : Form
    {
        private List<Broker> dbBrokers = null;
        private decimal totalCashLoss = 0;
        private decimal totalQtyLoaded2 = 0;
        private decimal totalQtyReceived2 = 0;
        public AllListForm()
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
                WaitForm form = new WaitForm(LoadCompanies);
                form.ShowDialog();

                tbCompanies.Text = brokerVMBindingSource.List.Count.ToString();
                tbLossInCash.Text = totalCashLoss.ToString("n2");
                decimal lossPercent = 0;

                if(totalQtyLoaded2 > 0)
                {
                    lossPercent = totalQtyReceived2 / totalQtyLoaded2 * 100;
                }
                tbTotalLossPercentage.Text = lossPercent.ToString("n2");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadCompanies()
        {
            try
            {
                using (Context db = new Context())
                {
                    dbBrokers = db.Brokers.Include(a => a.AppDeals)
                        .OrderBy(a => a.Id).ToList();

                    foreach (var it in dbBrokers)
                    {
                        if (it.Name == "N/A")
                            continue;
                        //it.AppDeals = db.AppDeals.Where(a => a.CompanyId == it.Id).ToList();
                        decimal lossInCash = 0;
                        float lossPercentage = 0;
                        float efficiency = 0;

                        decimal totalQtyLoaded = 0;
                        decimal totalQtyReceived = 0;
                        foreach (var item in it.AppDeals)
                        {
                            var tradeUnit = db.TradeUnits.Find(item.TradeUnitId);
                            item.DealSchedules = db.DealSchedules.Where(a => a.AppDealId == item.Id).ToList();
                            lossInCash += item.DealSchedules.Where(a => a.IsArrived).Sum(a => a.DiffTradeUnits * item.TradeUnitPrice);
                            totalQtyLoaded += item.DealSchedules.Where(a => a.IsArrived).Sum(a => a.LoadedSubTradeUnits);
                            totalQtyReceived += item.DealSchedules.Where(a => a.IsArrived).Sum(a => a.ReceivedSubTradeUnits);

                        }

                        
                        decimal diffQty = totalQtyLoaded - totalQtyReceived;
                        if(totalQtyLoaded > 0)
                            efficiency = (float)(totalQtyReceived / totalQtyLoaded * 100);

                        if(totalQtyReceived > 0)
                            lossPercentage = (float)(diffQty / totalQtyReceived * 100);

                        totalCashLoss += lossInCash;
                        totalQtyLoaded2 += totalQtyLoaded;
                        totalQtyReceived2 += totalQtyReceived;

                        BrokerVM vm = new BrokerVM
                        {
                            Id = it.Id,
                            Address = it.Address,
                            Contact = it.Contact,
                            DateAdded = it.DateAdded.ToShortDateString(),
                            IsActive = it.IsActive,
                            DealsCount = it.AppDeals.Count,
                            Extra = it.Extra,
                            LossInCash = lossInCash.ToString("n2"),
                            Efficiency = efficiency.ToString("n2"),
                            LossPercentage = lossPercentage.ToString("n2"),
                            Name = it.Name,
                            Remarks = it.Remarks
                        };

                        if (dgv.InvokeRequired)
                        {
                            dgv.Invoke(new Action(() =>
                            {
                                brokerVMBindingSource.List.Add(vm);
                            }));
                        }
                        else
                        {
                            brokerVMBindingSource.List.Add(vm);
                        }

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
