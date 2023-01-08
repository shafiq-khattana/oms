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
using WinFom.Deal.Forms;
using Model.Admin.Model;
using WinFom.Common.Model;

namespace WinFom.AppCompany.Forms
{
    public partial class CompaniesListForm : Form
    {
        private List<Company> dbCompanies = null;
        private decimal totalCashLoss = 0;
        private decimal totalQtyLoaded2 = 0;
        private decimal totalQtyReceived2 = 0;
        private string dgvbtndetails = "dsgabwerasdrq2341";
        private AppSettings appSet = Helper.AppSet;
        public CompaniesListForm()
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
                appSet = Helper.AppSet;
                tbCompanies.Text = companyVMBindingSource.List.Count.ToString();
                tbLossInCash.Text = totalCashLoss.ToString("n2");
                decimal lossPercent = 0;

                if(totalQtyLoaded2 > 0)
                {
                    lossPercent = totalQtyReceived2 / totalQtyLoaded2 * 100;
                }
                tbTotalLossPercentage.Text = lossPercent.ToString("n2");
                Gujjar.AddDatagridviewButton(dgv, dgvbtndetails, "Details", "Details", 80);
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
                    dbCompanies = db.Companies
                        .OrderBy(a => a.Id).ToList();

                    foreach (var it in dbCompanies)
                    {
                        if (it.Name == "N/A")
                            continue;
                        it.AppDeals = db.AppDeals.Where(a => a.CompanyId == it.Id).ToList()
                            .Where(a => { DateTime dt = a.DealDate.Date; return dt >= appSet.StartDate && dt <= appSet.EndDate; }).ToList(); ;
                        decimal lossInCash = 0;
                        float lossPercentage = 0;
                        float efficiency = 0;

                        decimal totalQtyLoaded = 0;
                        decimal totalQtyReceived = 0;
                        foreach (var item in it.AppDeals)
                        {
                            var tradeUnit = db.TradeUnits.Find(item.TradeUnitId);
                            item.DealSchedules = db.DealSchedules.Where(a => a.AppDealId == item.Id).ToList();
                            lossInCash += item.DealSchedules.Where(a => a.IsArrived).Sum(a => a.DiffLoadedTradeUnits * item.TradeUnitPrice);
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

                        CompanyVM vm = new CompanyVM
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
                                companyVMBindingSource.List.Add(vm);
                            }));
                        }
                        else
                        {
                            companyVMBindingSource.List.Add(vm);
                        }

                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int ri = e.RowIndex;
            if (ri == -1 || ri == dgv.NewRowIndex)
                return;

            if(dgv.Columns[dgvbtndetails].Index == e.ColumnIndex)
            {
                int compid = dgv.Rows[ri].Cells[0].Value.ToInt();
                int effi = (int)Convert.ToSingle(dgv.Rows[ri].Cells[11].Value.ToString());
                CompanyPerformanceForm compPerform = new CompanyPerformanceForm(compid, effi);
                compPerform.ShowDialog();
            }
        }
    }
}
