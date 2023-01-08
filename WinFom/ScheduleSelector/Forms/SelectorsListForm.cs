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
using Model.AppCompany.ViewModel;
using WinFom.Common.Forms;
using Model.ScheduleSelector.ViewModel;
using Model.Admin.Model;
using WinFom.Common.Model;
using Model.Employees.Model;

namespace WinFom.ScheduleSelector.Forms
{
    public partial class SelectorsListForm : Form
    {
        private decimal totalCashLoss = 0;
        private decimal totalQtyLoaded2 = 0;
        private decimal totalQtyReceived2 = 0;
        private AppSettings appSett = Helper.AppSet;
        public SelectorsListForm()
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
                WaitForm form = new WaitForm(LoadSelectors);
                form.ShowDialog();


                tbLossInCash.Text = totalCashLoss.ToString("n2");
                decimal lossPercent = 0;

                if (totalQtyLoaded2 > 0)
                {
                    lossPercent = totalQtyReceived2 / totalQtyLoaded2 * 100;
                }
                tbTotalLossPercentage.Text = lossPercent.ToString("n2");

                tbCompanies.Text = selectorVMBindingSource.List.Count.ToString();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadSelectors()
        {
            try
            {
                using (Context db = new Context())
                {
                    var selectors = db.Employees.Where(a => a.EmployeeType == EmployeeType.ScheduleSelector)
                        .OrderBy(a => a.Id).ToList();

                    foreach (var it in selectors)
                    {
                        if (it.Name == "N/A")
                            continue;
                        var schedules = db.DealSchedules.Where(a => a.IsArrived && a.EmployeeId == it.Id).ToList()
                            .Where(a => { DateTime dt = a.AddedDate.Date; return dt >= appSett.StartDate && dt <= appSett.EndDate; }).ToList();

                        decimal lossInCash = 0;
                        float lossPercentage = 0;
                        float efficiency = 0;

                        decimal totalQtyLoaded = 0;
                        decimal totalQtyReceived = 0;

                        lossInCash = schedules.Where(a => a.IsArrived).Sum(a => a.DiffLoadedPrice);
                        totalQtyLoaded = schedules.Where(a => a.IsArrived).Sum(a => a.LoadedSubTradeUnits);
                        totalQtyReceived = schedules.Where(a => a.IsArrived).Sum(a => a.ReceivedSubTradeUnits);




                        decimal diffQty = totalQtyLoaded - totalQtyReceived;
                        if (totalQtyLoaded > 0)
                            efficiency = (float)(totalQtyReceived / totalQtyLoaded * 100);

                        if (totalQtyReceived > 0)
                            lossPercentage = (float)(diffQty / totalQtyReceived * 100);

                        totalCashLoss += lossInCash;
                        totalQtyLoaded2 += totalQtyLoaded;
                        totalQtyReceived2 += totalQtyReceived;

                        SelectorVM vm = new SelectorVM
                        {
                            Id = it.Id,
                            Address = it.Address,
                            Contact = it.Contact,
                            DateAdded = it.DateAdded.ToShortDateString(),
                            IsActive = it.IsActive,
                            SchedulesCount = schedules.Count,
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
                                selectorVMBindingSource.List.Add(vm);
                            }));
                        }
                        else
                        {
                            selectorVMBindingSource.List.Add(vm);
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
