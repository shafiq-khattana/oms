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
    public partial class DashBoardForm : Form
    {
        private List<AppDeal> deals = null;
        private List<DealSchedule> schedules = null;
        private AppSettings appSett = Helper.AppSet;
        public DashBoardForm()
        {
            InitializeComponent();
            
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

                int totalDeals = deals.Count;
                tbTotalDeals.Text = totalDeals.ToString();
                int dealCompleted = deals.Count(a => a.DealStatus == AppDealStatus.Completed);
                tbDealsCompleted.Text = dealCompleted.ToString();
                //float dealCompleteEfficiency = dealCompleted / (float)totalDeals;
                bcpDealCompletionEfficiency.MaxValue = totalDeals;
                bcpDealCompletionEfficiency.Value = dealCompleted;

                tbPartialDeals.Text = deals.Count(a => a.DealStatus == AppDealStatus.Partial).ToString();
                tbPendingDeals.Text = deals.Count(a => a.DealStatus == AppDealStatus.Scheduled).ToString();

                int schsCompleted = schedules.Count(b => b.IsArrived);
                tbScheduleCompleted.Text = schsCompleted.ToString();
                

               
                tbScheduleDispatched.Text = schedules.Count(b => b.IsDispatched && !b.IsLoaded && !b.IsArrived).ToString();
                int totalSchs = schedules.Count;
                bcpScheduleCompletion.MaxValue = totalSchs;
                bcpScheduleCompletion.Value = schsCompleted;

                tbTotalSchedules.Text = totalSchs.ToString();
                tbScheduleLoaded.Text = schedules.Count(b => b.IsLoaded && !b.IsArrived).ToString();
                tbSchedulePending.Text = schedules.Count(b => !b.IsLoaded && !b.IsDispatched && 
                    b.Status == ScheduleStatus.Scheduled && !b.IsArrived).ToString();
                decimal allReceived = schedules.Where(a => a.IsArrived).Sum(a => a.ReceivedSubTradeUnits);
                decimal allLoaded = schedules.Where(a => a.IsArrived).Sum(a => a.LoadedSubTradeUnits);
                bcpOverallEfficiency.MaxValue = (int)Math.Ceiling(allLoaded);
                bcpOverallEfficiency.Value = (int)Math.Ceiling(allReceived);
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
                    deals = db.AppDeals.AsParallel().ToList()
                        .Where(a => { DateTime dt = a.DealDate.Date; return dt >= appSett.StartDate && dt <= appSett.EndDate; }).ToList();
                    
                    schedules = db.DealSchedules.AsParallel().ToList()
                        .Where(a => { DateTime dt = a.AddedDate.Date; return dt >= appSett.StartDate && dt <= appSett.EndDate; }).ToList();

                }
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }
    }
}
