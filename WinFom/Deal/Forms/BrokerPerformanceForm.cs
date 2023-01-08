using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model.Admin.Model;
using Khattana.Common;
using WinFom.Admin.Database;
using Model.Deal.Model;
using WinFom.Common.Forms;
using Model.Deal.Common;
using WinFom.Common.Model;

namespace WinFom.Deal.Forms
{
    public partial class BrokerPerformanceForm : Form
    {
        private int brokerId = 0;
        private int effi = 0;
        private Broker broker = null;
        private AppSettings appSett = Helper.AppSet;
        public BrokerPerformanceForm(int brokerId, int effici)
        {
            InitializeComponent();
            this.brokerId = brokerId;
            effi = effici;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(panel1.Visible == false)
            {
                bunifuTransition1.ShowSync(panel1);
            }
            else
            {
                bunifuTransition1.HideSync(panel1);
            }
        }

        

        private void CompanyPerformanceForm_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait = new WaitForm(LoadData);
                wait.ShowDialog();

                int totalDeals = broker.AppDeals.Count;
                tbTotalDeals.Text = totalDeals.ToString();
                int dealCompleted = broker.AppDeals.Count(a => a.DealStatus == AppDealStatus.Completed);
                tbDealsCompleted.Text = dealCompleted.ToString();
                //float dealCompleteEfficiency = dealCompleted / (float)totalDeals;
                bcpDealCompletionEfficiency.MaxValue = totalDeals;
                bcpDealCompletionEfficiency.Value = dealCompleted;

                tbPartialDeals.Text = broker.AppDeals.Count(a => a.DealStatus == AppDealStatus.Partial).ToString();
                tbPendingDeals.Text = broker.AppDeals.Count(a => a.DealStatus == AppDealStatus.Scheduled).ToString();

                int schsCompleted = broker.AppDeals.Sum(a => a.DealSchedules.Count(b => b.IsArrived));
                tbScheduleCompleted.Text = schsCompleted.ToString();
                

               
                tbScheduleDispatched.Text = broker.AppDeals.Sum(a => a.DealSchedules.Count(b => b.IsDispatched && !b.IsLoaded && !b.IsArrived)).ToString();
                int totalSchs = broker.AppDeals.Sum(a => a.DealSchedules.Count);
                bcpScheduleCompletion.MaxValue = totalSchs;
                bcpScheduleCompletion.Value = schsCompleted;

                tbTotalSchedules.Text = totalSchs.ToString();
                tbScheduleLoaded.Text = broker.AppDeals.Sum(a => a.DealSchedules.Count(b => b.IsLoaded && !b.IsArrived)).ToString();
                tbSchedulePending.Text = broker.AppDeals.Sum(a => a.DealSchedules.Count(b => !b.IsLoaded && !b.IsDispatched && b.Status == ScheduleStatus.Scheduled && !b.IsArrived)).ToString();
                tbCompany.Text = string.Format("{0} ({1})", broker.Name, broker.Address);

                bcpOverallEfficiency.Value = effi;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadData()
        {
            try
            {
                using (Context db = new Context())
                {
                    broker = db.Brokers.Find(brokerId);
                    broker.AppDeals = db.AppDeals.Where(a => a.CompanyId == brokerId).ToList();
                    foreach (var item in broker.AppDeals)
                    {
                        item.DealSchedules = db.DealSchedules.Where(a => a.AppDealId == item.Id).ToList()
                            .Where(a => { DateTime dt = a.AddedDate.Date; return dt >= appSett.StartDate && dt <= appSett.EndDate; }).ToList();
                        ;
                    }
                }
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }
    }
}
