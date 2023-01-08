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
using System.Globalization;
using Model.Deal.ViewModel;
using WinFom.Admin.Forms;
using Model.Deal.Common;
using Model.Admin.Model;
using WinFom.Common.Model;
using Model.CommonModel;

namespace WinFom.Deal.Forms
{
    public partial class AddDealForm : Form
    {
        private List<Company> companies = new List<Company>();
        private List<Broker> brokers = new List<Broker>();
        private List<DealItem> dealItems = new List<DealItem>();
        private List<DealPacking> dealPackings = new List<DealPacking>();
        private List<PackingUnit> packingUnits = new List<PackingUnit>();
        private List<TradeUnit> tradeUnits = new List<TradeUnit>();
        private string btndgvdelete = "btndgvdel";
        private AppSettings appSettings = null;
        private RawBrokerShareType rawBrokerShareType = null;
        private List<RawBrokerShareType> rawBrokerShareTypes = null;
        private decimal brokerSharePerPackPercentage = 0;
        private AppUser appUser = SingleTon.LoginForm.appUser;
        #region "Load Combo Data"
        private void LoadTradeUnits()
        {
            try
            {
                using (Context db = new Context())
                {
                    tradeUnits = db.TradeUnits.ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadPackingUnits()
        {
            try
            {
                if (packingUnits.Count > 0)
                {
                    packingUnits.Clear();
                }
                using (Context db = new Context())
                {
                    packingUnits = db.PackingUnits.ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadAllComboData()
        {
            LoadPackings();
            LoadDealItems();
            LoadSelectors();
            LoadBrokers();
            LoadCompanies();
            LoadPackingUnits();
            LoadTradeUnits();
            LoadRawBrokerTypes();
        }
        private void LoadPackings()
        {
            if (dealPackings.Count > 0)
                dealPackings.Clear();
            try
            {
                using (Context db = new Context())
                {
                    dealPackings = db.DealPackings
                        .OrderBy(a => a.Name).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadDealItems()
        {
            if (dealItems.Count > 0)
                dealItems.Clear();
            try
            {
                using (Context db = new Context())
                {
                    dealItems = db.DealItems
                        .OrderBy(a => a.Name).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadSelectors()
        {
            //if (selectors.Count > 0)
            //    selectors.Clear();
            try
            {
                //using (Context db = new Context())
                //{
                //    selectors = db.Selectors.Where(a => a.IsActive)
                //        .OrderBy(a => a.Name).ToList();
                //}
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadBrokers()
        {
            if (brokers.Count > 0)
                brokers.Clear();
            try
            {
                using (Context db = new Context())
                {
                    brokers = db.Brokers.Where(a => a.IsActive)
                        .OrderBy(a => a.Name).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadCompanies()
        {
            if (companies.Count > 0)
                companies.Clear();
            try
            {
                using (Context db = new Context())
                {
                    companies = db.Companies.Where(a => a.IsActive)
                        .OrderBy(a => a.Name).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        #endregion

        #region "Bind Data With Combo boxes"

        private void BindTradeUnits()
        {
            cbTradeUnits.DataSource = tradeUnits;
            cbTradeUnits.ValueMember = "Id";
            cbTradeUnits.DisplayMember = "Title";

            cbTradeUnits.SelectedItem = cbTradeUnits.Items.OfType<TradeUnit>()
                .FirstOrDefault(a => a.Title == "N/A");
        }
        private void BindPackingUnits()
        {
            cbPackingUnits.DataSource = packingUnits;
            cbPackingUnits.ValueMember = "Id";
            cbPackingUnits.DisplayMember = "Name";
        }
        private void BindCBCompanies()
        {
            cbCompanies.DataSource = companies;
            cbCompanies.ValueMember = "Id";
            cbCompanies.DisplayMember = "Name";

            cbCompanies.SelectedItem = cbCompanies.Items.OfType<Company>()
                .FirstOrDefault(a => a.Name == "N/A");
        }
        private void BindCBBrokers()
        {
            cbBrokers.DataSource = brokers;
            cbBrokers.ValueMember = "Id";
            cbBrokers.DisplayMember = "Name";

            cbBrokers.SelectedItem = cbBrokers.Items.OfType<Broker>()
                .FirstOrDefault(a => a.Name == "N/A");
        }
        private void BindCBSelectors()
        {
            //cbSelectors.DataSource = selectors;
            //cbSelectors.ValueMember = "Id";
            //cbSelectors.DisplayMember = "Name";

            //cbSelectors.SelectedItem = cbSelectors.Items.OfType<Selector2>()
            //    .FirstOrDefault(a => a.Name == "N/A");
        }

        private void BindCBDealItems()
        {
            cbDealItems.DataSource = dealItems;
            cbDealItems.DisplayMember = "Name";
            cbDealItems.ValueMember = "Id";

            cbDealItems.SelectedItem = cbDealItems.Items.OfType<DealItem>()
                .FirstOrDefault(a => a.Name == "N/A");
        }
        private void BindCBPackings()
        {
            cbPackings.DataSource = dealPackings;
            cbPackings.DisplayMember = "Name";
            cbPackings.ValueMember = "Id";

            cbPackings.SelectedItem = cbPackings.Items.OfType<DealPacking>()
                .FirstOrDefault(a => a.Name == "N/A");
        }
        private void BindAllCBs()
        {
            BindCBPackings();
            BindCBDealItems();
            BindCBSelectors();
            BindCBBrokers();
            BindCBCompanies();
            BindPackingUnits();
            BindTradeUnits();
            BindCbRawBrokerShareTypes();
        }
        #endregion
        public AddDealForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadAppSettings()
        {
            try
            {
                using (Context db = new Context())
                {
                    appSettings = db.AppSettings.First();
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
                Helper.IsOkApplied();
                WaitForm w5 = new WaitForm(LoadAppSettings);
                w5.ShowDialog();
                WaitForm w3 = new WaitForm(LoadAllComboData);
                w3.ShowDialog();

                BindAllCBs();
                Gujjar.TB4(pMain);
                NumsOnly();
                Gujjar.TBOptional(tbRemarks);
                tbRemarks.Text = "N/A";
                Gujjar.AddDatagridviewButton(dgv, btndgvdelete, "Delete", "Delete", 70);
                btnAdd.Enabled = false;

                dtpDealDate.MinDate = appSettings.StartDate;
                dtpDealDate.MaxDate = appSettings.EndDate;

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void NumsOnly()
        {
            Gujjar.NumbersOnly(tbPackingQty);
            Gujjar.NumbersOnly(tbDealPrice);
        }

        private void btnAddCompany_Click(object sender, EventArgs e)
        {
            try
            {
                AddCompanyForm form = new AddCompanyForm();
                form.ShowDialog();

                if (form.CompId != 0)
                {
                    WaitForm w1 = new WaitForm(LoadCompanies, "Loading Companies");
                    w1.ShowDialog();

                    BindCBCompanies();

                    cbCompanies.SelectedItem = cbCompanies.Items.OfType<Company>()
                        .FirstOrDefault(a => a.Id == form.CompId);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddBroker_Click(object sender, EventArgs e)
        {
            try
            {
                AddBrokerForm form = new AddBrokerForm();
                form.ShowDialog();

                if (form.BrokerId != 0)
                {
                    WaitForm w1 = new WaitForm(LoadBrokers, "Loading brokers");
                    w1.ShowDialog();
                    BindCBBrokers();

                    cbBrokers.SelectedItem = cbBrokers.Items.OfType<Broker>()
                        .FirstOrDefault(a => a.Id == form.BrokerId);
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
                AddDealItemForm form = new AddDealItemForm();
                form.ShowDialog();

                if (form.ItemId != 0)
                {
                    WaitForm w4 = new WaitForm(LoadDealItems, "");
                    w4.ShowDialog();

                    BindCBDealItems();
                    cbDealItems.SelectedItem = cbDealItems.Items.OfType<DealItem>()
                        .FirstOrDefault(a => a.Id == form.ItemId);
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
                AddPackingForm form = new AddPackingForm();
                form.ShowDialog();

                int pid = form.PackingId;
                if (pid != 0)
                {
                    WaitForm w5 = new WaitForm(LoadPackings, "");
                    w5.ShowDialog();

                    BindCBPackings();

                    cbPackings.SelectedItem = cbPackings.Items.OfType<DealPacking>()
                        .FirstOrDefault(a => a.Id == pid);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void cbPackings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPackings.SelectedIndex == -1)
                return;

            DealPacking packing = cbPackings.SelectedItem as DealPacking;
            label7.Text = string.Format("No of {0}(s):", packing.Name);
            label14.Text = string.Format("{0}(s)", packing.Name);
            label19.Text = string.Format("Per {0} Qty:", packing.Name);
        }

        private void btnAddUnit_Click(object sender, EventArgs e)
        {
            try
            {
                AddPackingUnitForm form = new AddPackingUnitForm();
                form.ShowDialog();
                int puid = form.PackingId;
                if (puid != 0)
                {
                    WaitForm wait = new WaitForm(LoadPackingUnits, "");
                    wait.ShowDialog();

                    BindPackingUnits();

                    cbPackingUnits.SelectedItem = cbPackingUnits.Items.OfType<PackingUnit>()
                        .FirstOrDefault(a => a.Id == puid);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void cbPackingUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPackingUnits.SelectedIndex == -1)
                return;

            PackingUnit pUnit = cbPackingUnits.SelectedItem as PackingUnit;
            label1.Text = label21.Text = label10.Text = string.Format("{0}(s)", pUnit.Name);
        }

        private void Packing_Leave(object sender, EventArgs e)
        {
            string packStr = tbPackingQty.Text;
            string perPackStr = tbPerPackingQty.Text;

            if (string.IsNullOrEmpty(packStr) || string.IsNullOrEmpty(perPackStr))
            {
                //tbPerPackingQty.Clear();
                //tbPackingQty.Clear();
                return;
            }
            else
            {
                decimal packs = packStr.ToDecimal();
                decimal perPack = perPackStr.ToDecimal();

                decimal totalQty = packs * perPack;
                tbTotalQty.Text = totalQty.ToString("n1");
            }
        }

        private void btnAddTradeUnit_Click(object sender, EventArgs e)
        {
            try
            {
                AddTradeUnit form = new AddTradeUnit();
                form.ShowDialog();

                int tid = form.TradeUnitId;
                if (tid != 0)
                {
                    WaitForm wait = new WaitForm(LoadTradeUnits);
                    wait.ShowDialog();

                    BindTradeUnits();

                    cbTradeUnits.SelectedItem = cbTradeUnits.Items.OfType<TradeUnit>()
                        .FirstOrDefault(a => a.Id == tid);
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
            try
            {

                CultureInfo cinfo = new CultureInfo("ur-PK");
                TradeUnit tradeUnit = cbTradeUnits.SelectedItem as TradeUnit;
                if (tradeUnit.Title == "N/A")
                {
                    tradeUnit.Qty = 1;

                }
                label11.Text = string.Format("Per {0} qty:", tradeUnit.Title);
                label13.Text = string.Format("Per {0} price:", tradeUnit.Title);

                tbPerTradeUnitQty.Text = tradeUnit.Qty.ToString();
                //tbPerTradeUnitPrice.Text = tradeUnit.Price.ToString("c", cinfo);

                label17.Text = string.Format("No of {0}(s):", tradeUnit.Title);
                label16.Text = string.Format("{0}(s)", tradeUnit.Title);

                string totalQtyStr = tbTotalQty.Text;
                if (string.IsNullOrEmpty(totalQtyStr))
                {
                    return;
                }

                decimal totalQty = totalQtyStr.ToDecimal();

                string perTradeUnitQtyStr = tbPerTradeUnitQty.Text;
                if (string.IsNullOrEmpty(perTradeUnitQtyStr))
                {
                    return;
                }

                decimal perTradeUnitQty = perTradeUnitQtyStr.ToDecimal();

                decimal totalTradeUnits = totalQty / perTradeUnitQty;

                tbToralTradeUnits.Text = totalTradeUnits.ToString("n4");
                //decimal dealPrice = (totalTradeUnits * tradeUnit.Price);
                //tbDealPrice.Text = dealPrice.ToString("c", cinfo);
                //tbDealPrice.Text = dealPrice.ToString("n4");
                //label15.Text = Gujjar.ConvertCurrencyToWords(dealPrice);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }

        }

        DateTime scheduleDate = DateTime.Now;
        private decimal GetAdddedTradeUnits()
        {
            decimal added = 0;

            if (dgv.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    decimal tus = Convert.ToDecimal(row.Cells[1].Value.ToString().Split('-')[0]);
                    added += tus;
                }
                string strDt = dgv.Rows[dgv.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells[4].Value.ToString();
                scheduleDate = Convert.ToDateTime(strDt);
            }
            return added;
        }
        private decimal GetAddedPackingUnits()
        {
            decimal added = 0;

            if (dgv.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    decimal tus = Convert.ToDecimal(row.Cells[2].Value.ToString().Split('-')[0]);
                    added += tus;
                }
                //string strDt = dgv.Rows[dgv.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells[4].Value.ToString();
                //scheduleDate = Convert.ToDateTime(strDt);
            }
            return added;
        }
        private void btnAddSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbTradeUnits.SelectedIndex == -1)
                {
                    throw new Exception("Please select a trade unit");
                }
                if (cbPackings.SelectedIndex == -1)
                {
                    throw new Exception("Please select packings");
                }

                if (!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill text fields, before to add any schedule");
                }
                TradeUnit tradeUnit = cbTradeUnits.SelectedItem as TradeUnit;
                string packing = cbPackings.Text;
                decimal totalTradeUnits = tbToralTradeUnits.Text.ToDecimal();
                decimal totalPackings = tbPackingQty.Text.ToDecimal();
                decimal perPackQty = tbPerPackingQty.Text.ToDecimal();
                string packingUnit = cbPackingUnits.Text;
                decimal addedTradeUnits = GetAdddedTradeUnits();
                decimal tradeUnitPrice = tbPerTradeUnitPrice.Text.ToDecimal();
                decimal addedPackingUnits = GetAddedPackingUnits();
                decimal remainingPackingUnits = totalPackings - addedPackingUnits;
                if (remainingPackingUnits == 0)
                {
                    string msg = string.Format("You can't add more schedules. Because there is no {0}-(s) to schedule. Thanks ", packing);
                    throw new Exception(msg);
                }
                AddScheduleForm form = new AddScheduleForm(tradeUnit, totalTradeUnits, packing, addedTradeUnits, totalPackings, perPackQty, packingUnit, scheduleDate, tradeUnitPrice, addedPackingUnits, remainingPackingUnits);
                form.ShowDialog();
                if (form.scheduleVM != null)
                {
                    scheduleVMBindingSource.List.Add(form.scheduleVM);

                    dgv.Refresh();
                    numUpDown.Value = scheduleVMBindingSource.List.Count;
                }
                decimal addedPackingUnits2 = GetAddedPackingUnits();
                decimal remainingPackingUnits2 = totalPackings - addedPackingUnits2;

                if (remainingPackingUnits2 == 0)
                {
                    label23.BackColor = Color.Lime;
                    btnAdd.Enabled = true;
                }
                else
                {
                    label23.BackColor = Color.Red;
                    btnAdd.Enabled = false;
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                int ci = e.ColumnIndex;

                if (ri == -1 || ri == dgv.NewRowIndex)
                    return;

                if (dgv.Columns[btndgvdelete].Index == ci)
                {
                    string sid = dgv.Rows[ri].Cells[0].Value.ToString();

                    var schvm = scheduleVMBindingSource.List.OfType<ScheduleVM>().FirstOrDefault(a => a.Id == sid);
                    scheduleVMBindingSource.List.Remove(schvm);



                    numUpDown.Value = scheduleVMBindingSource.List.Count;
                    Gujjar.InfoMsg("Schedule information is deleted.");
                    dgv.Refresh();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //OmeDeal deal = new OmeDeal();

                #region Test
                Company company2 = cbCompanies.SelectedItem as Company;
                Broker broker2 = cbBrokers.SelectedItem as Broker;
                DealItem dealItem2 = cbDealItems.SelectedItem as DealItem;
                DealPacking dealPacking2 = cbPackings.SelectedItem as DealPacking;
                PackingUnit packingUnit2 = cbPackingUnits.SelectedItem as PackingUnit;
                TradeUnit tradeUnit2 = cbTradeUnits.SelectedItem as TradeUnit;
                List<ScheduleVM> scheduleVMList2 = scheduleVMBindingSource.List.OfType<ScheduleVM>().ToList();
                decimal addedTradeUnits2 = GetAdddedTradeUnits();
                decimal totalTradeUnits2 = tbToralTradeUnits.Text.ToDecimal();
                decimal remainingTradeUnits2 = totalTradeUnits2 - addedTradeUnits2;

                AppDeal deal3 = new AppDeal();
                deal3.BrokerId = broker2.Id;
                deal3.IsCompleted = false;
                deal3.CompanyId = company2.Id;
                deal3.DealDate = DateTime.Now;
                deal3.DealItemId = dealItem2.Id;
                //deal3.PackingQty = (decimal)Convert.ToDouble(tbPackingQty.Text);
                deal3.PackingQty = tbPackingQty.Text.ToDecimal();
                deal3.FareAmount = 0;
                deal3.DealPackingId = dealPacking2.Id;
                deal3.PackingUnitId = packingUnit2.Id;
                //deal3.PerPackingQty = (decimal)Convert.ToDouble(tbPerPackingQty.Text);
                deal3.PerPackingQty = tbPerPackingQty.Text.ToDecimal();
                deal3.Remarks = tbRemarks.Text;
                deal3.AddedBy = "admin1";
                //deal3.DealPrice = (decimal)Convert.ToDouble(tbDealPrice.Text);
                deal3.DealPrice = tbDealPrice.Text.ToDecimal();
                deal3.SchedulesCount = (int)numUpDown.Value;
                deal3.Tax = 0;
                //deal3.TotalQty = (decimal)Convert.ToDouble(tbTotalQty.Text);
                //deal3.TotalTradeUnits = (decimal)Convert.ToDouble(tbToralTradeUnits.Text);

                deal3.TotalQty = tbTotalQty.Text.ToDecimal();
                deal3.TotalTradeUnits = tbToralTradeUnits.Text.ToDecimal();
                deal3.TradeUnitId = tradeUnit2.Id;
                deal3.UpdatedBy = "N/A";
                using (Context db = new Context())
                {
                    var xyz = db.AppDeals.Add(deal3);

                    db.Database.Log = (s) => { System.Diagnostics.Debug.WriteLine(s); };
                    db.SaveChanges();
                    Gujjar.InfoMsg(xyz.Id.ToString());
                    Gujjar.InfoMsg("Data is inserted");
                }
                #endregion

                if (!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill required fields");
                }
                if (scheduleVMBindingSource.List.Count == 0)
                {
                    throw new Exception("Please add at least one schedule in list");
                }

                //DialogResult result = Gujjar.ConfirmYesNo("Please confirm..!! all information is correct? before to save.");
                //if (result == DialogResult.No)
                //    return;

                //foreach (var ctrl in pMain.Controls)
                //{
                //    if (ctrl is ComboBox)
                //    {
                //        ComboBox cb = ctrl as ComboBox;

                //        if (cb.SelectedIndex == -1 || cb.Text == "N/A")
                //        {
                //            cb.Select();
                //            throw new Exception("Please select an appropriate option from drop down list. Thanks");
                //        }
                //    }
                //}


                Company company = cbCompanies.SelectedItem as Company;
                Broker broker = cbBrokers.SelectedItem as Broker;
                DealItem dealItem = cbDealItems.SelectedItem as DealItem;
                DealPacking dealPacking = cbPackings.SelectedItem as DealPacking;
                PackingUnit packingUnit = cbPackingUnits.SelectedItem as PackingUnit;
                TradeUnit tradeUnit = cbTradeUnits.SelectedItem as TradeUnit;
                List<ScheduleVM> scheduleVMList = scheduleVMBindingSource.List.OfType<ScheduleVM>().ToList();
                decimal addedTradeUnits = GetAdddedTradeUnits();
                decimal totalTradeUnits = tbToralTradeUnits.Text.ToDecimal();
                decimal remainingTradeUnits = totalTradeUnits - addedTradeUnits;

                #region "Confirm message"
                if (remainingTradeUnits != 0)
                {
                    string tu = tradeUnit.Title;
                    string msg = string.Format("Toral {0}-(s): \t\t{1}\nScheduled: \t\t{2}\nRemaining: \t\t{3}. \nPlease compete all schedules. Thanks",
                        tu, totalTradeUnits.ToString("n4"), addedTradeUnits.ToString("n4"), remainingTradeUnits.ToString("n4"));

                    throw new Exception(msg);
                }
                //StringBuilder sb = new StringBuilder();
                //sb.AppendFormat("Please confirm following parameters before to add deal. Thanks\n\n");
                //sb.AppendFormat("Company: \t\t{0}\n", company.Name);
                //sb.AppendFormat("Broker: \t\t\t{0}\n", broker.Name);
                //sb.AppendFormat("Deal Item: \t\t{0}\n", dealItem.Name);
                //sb.AppendFormat("No of {0}-(s): {1}\n", dealPacking.Name, tbPackingQty.Text);
                //sb.AppendFormat("Each {0} contains {1}-{2}\nTotal {3}(s): {4}\n", dealPacking.Name, 
                //    tbPerPackingQty.Text, packingUnit.Name, packingUnit.Name, tbTotalQty.Text);

                //sb.AppendFormat("Trade in {0}\nEach {1} contains {2}-{3}(s)\nEach {4} price: {5}\n", tradeUnit.Title, 
                //    tradeUnit.Title, tradeUnit.Qty.ToString("n3"),
                //    packingUnit.Name, tradeUnit.Title, tradeUnit.Price.ToString("n4"));

                //sb.AppendFormat("Deal price: {0}\n{1}\n", tbDealPrice.Text, label15.Text);
                //sb.AppendFormat("No of schedules: {0}\n", scheduleVMList.Count);
                //sb.AppendFormat("----------------------------------------------------------------------------------------------------\n");
                //int count = 1;
                //foreach (var item in scheduleVMList)
                //{
                //    sb.AppendFormat("Schedule No {0}. Scheduled {1}, price: {2}, expected date of arrival: {3}.\n",
                //        count++, item.TradeUnits, item.Price, item.ExpectedDate);
                //}
                //sb.AppendFormat("----------------------------------------------------------------------------------------------------\n");
                //sb.AppendFormat("\n\nAll information is correct? please confirm !!");

                //DialogResult result2 = Gujjar.ConfirmYesNo(sb.ToString());
                //if (result2 == DialogResult.No)
                //{
                //    return;
                //}
                #endregion
                //Model.Deal.Model.OmeDeal deal3 = new OmeDeal();
                AppDeal deal31 = new AppDeal();
                deal31.BrokerId = broker.Id;
                deal31.IsCompleted = false;
                deal31.CompanyId = company.Id;
                deal31.DealDate = DateTime.Now;
                deal31.DealItemId = dealItem.Id;
                //deal3.PackingQty = (decimal)Convert.ToDouble(tbPackingQty.Text);
                deal31.PackingQty = tbPackingQty.Text.ToDecimal();
                deal31.FareAmount = 0;
                deal31.DealPackingId = dealPacking.Id;
                deal31.PackingUnitId = packingUnit.Id;
                //deal3.PerPackingQty = (decimal)Convert.ToDouble(tbPerPackingQty.Text);
                deal31.PerPackingQty = tbPerPackingQty.Text.ToDecimal();
                deal31.Remarks = tbRemarks.Text;
                deal31.AddedBy = "admin1";
                //deal3.DealPrice = (decimal)Convert.ToDouble(tbDealPrice.Text);
                deal31.DealPrice = tbDealPrice.Text.ToDecimal();
                deal31.SchedulesCount = (int)numUpDown.Value;
                deal31.Tax = 0;
                //deal3.TotalQty = (decimal)Convert.ToDouble(tbTotalQty.Text);
                //deal3.TotalTradeUnits = (decimal)Convert.ToDouble(tbToralTradeUnits.Text);

                deal31.TotalQty = tbTotalQty.Text.ToDecimal();
                deal31.TotalTradeUnits = tbToralTradeUnits.Text.ToDecimal();
                deal31.TradeUnitId = tradeUnit.Id;
                deal31.UpdatedBy = "N/A";
                using (Context db = new Context())
                {
                    //OmeDeal deal4 = new OmeDeal();
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {


                            //var dbItem = db.OmeDeals.FirstOrDefault(a => a.Equals(deal31));
                            //if (dbItem != null)
                            //{
                            //    throw new Exception(string.Format("Deal already added in database"));
                            //}



                            var dbItem1 = db.AppDeals.FirstOrDefault(a => a.Equals(deal31));
                            if (dbItem1 != null)
                            {
                                throw new Exception(string.Format("Deal already added in database"));
                            }
                            deal31 = db.AppDeals.Add(deal31);
                            db.SaveChanges();

                            foreach (ScheduleVM sch in scheduleVMBindingSource.List.OfType<ScheduleVM>())
                            {
                                DealSchedule schedule = new DealSchedule
                                {
                                    DriverId = null,
                                    ArrivalDate = Convert.ToDateTime(sch.ExpectedDate),
                                    IsArrived = false,
                                    OmeDealId = deal31.Id,
                                    ScheduledPackingsUnits = sch.PackingUnits.ToDecimal(),
                                    PackingUnitTitle = packingUnit.Name,
                                    ScheduledPrice = sch.Price.ToDecimal(),
                                    ReadyDate = Convert.ToDateTime(sch.ExpectedDate),
                                    ReceivedTradeUnits = 0,
                                    Remarks = "N/A",
                                    ScheduledTradeUnits = sch.TradeUnits.ToDecimal(),
                                    TradeUnitTitle = tradeUnit.Title,

                                };

                                db.DealSchedules.Add(schedule);
                            }
                            db.SaveChanges();

                            trans.Commit();

                            Gujjar.InfoMsg(string.Format("Deal with ID {0} saved successfully.", deal31.Id));
                            Close();
                        }
                        catch (Exception exp)
                        {
                            trans.Rollback();
                            throw exp;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (Context db = new Context())
                {

                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            Company company = cbCompanies.SelectedItem as Company;
                            Broker broker = cbBrokers.SelectedItem as Broker;
                            DealItem dealItem = cbDealItems.SelectedItem as DealItem;
                            DealPacking dealPacking = cbPackings.SelectedItem as DealPacking;
                            PackingUnit packingUnit = cbPackingUnits.SelectedItem as PackingUnit;
                            TradeUnit tradeUnit = cbTradeUnits.SelectedItem as TradeUnit;
                            List<ScheduleVM> scheduleVMList = scheduleVMBindingSource.List.OfType<ScheduleVM>().ToList();
                            //decimal addedTradeUnits = GetAdddedTradeUnits();
                            //decimal totalTradeUnits = tbToralTradeUnits.Text.ToDecimal();
                            //decimal remainingTradeUnits = totalTradeUnits - addedTradeUnits;
                            decimal addedPackingUnits = GetAddedPackingUnits();
                            decimal remainingPackingUnits = tbPackingQty.Text.ToDecimal() - addedPackingUnits;
                            #region "Confirm message"
                            if (remainingPackingUnits > 0)
                            {
                                string tu = dealPacking.Name;
                                string msg = string.Format("Toral {0}-(s): \t\t{1}\nScheduled: \t\t{2}\nRemaining: \t\t{3}. \nPlease compete all schedules. Thanks",
                                    tu, tbPackingQty.Text, addedPackingUnits.ToString("n2"), remainingPackingUnits.ToString("n2"));

                                throw new Exception(msg);
                            }
                            StringBuilder sb = new StringBuilder();
                            sb.AppendFormat("Please confirm following parameters before to add deal. Thanks\n\n");
                            sb.AppendFormat("Company: \t\t{0}\n", company.Name);
                            sb.AppendFormat("Broker: \t\t\t{0}\n", broker.Name);
                            sb.AppendFormat("Deal Item: \t\t{0}\n", dealItem.Name);
                            sb.AppendFormat("No of {0}-(s): {1}\n", dealPacking.Name, tbPackingQty.Text);
                            sb.AppendFormat("Each {0} contains {1}-{2}\nTotal {3}(s): {4}\n", dealPacking.Name,
                                tbPerPackingQty.Text, packingUnit.Name, packingUnit.Name, tbTotalQty.Text);

                            sb.AppendFormat("Trade in {0}\nEach {1} contains {2}-{3}(s)\nEach {4} price: {5}\n", tradeUnit.Title,
                                tradeUnit.Title, tradeUnit.Qty.ToString("n3"),
                                packingUnit.Name, tradeUnit.Title, tbPerTradeUnitPrice.Text);

                            sb.AppendFormat("Deal price: {0}\n{1}\n", tbDealPrice.Text, label15.Text);
                            sb.AppendFormat("No of schedules: {0}\n", scheduleVMList.Count);
                            sb.AppendFormat("----------------------------------------------------------------------------------------------------\n");
                            int count = 1;
                            foreach (var item in scheduleVMList)
                            {
                                sb.AppendFormat("Schedule No {0}. Scheduled {1}, price: {2}, expected date of arrival: {3}. Remarks: {4}\n",
                                    count++, item.TradeUnits, item.Price, item.ExpectedDate, item.Remarks);
                            }
                            sb.AppendFormat("----------------------------------------------------------------------------------------------------\n");
                            sb.AppendFormat("\n\nAll information is correct? please confirm !!");

                            DialogResult result2 = Gujjar.ConfirmYesNo(sb.ToString());
                            if (result2 == DialogResult.No)
                            {
                                return;
                            }
                            #endregion
                            AppDeal deal31 = new AppDeal();
                            deal31.BrokerId = broker.Id;
                            deal31.IsCompleted = false;
                            deal31.CompanyId = company.Id;
                            deal31.DealItemId = dealItem.Id;
                            deal31.PackingQty = tbPackingQty.Text.ToDecimal();
                            deal31.FareAmount = 0;
                            deal31.DealPackingId = dealPacking.Id;
                            deal31.PackingUnitId = packingUnit.Id;
                            deal31.PerPackingQty = tbPerPackingQty.Text.ToDecimal();
                            deal31.Remarks = tbRemarks.Text;
                            deal31.AddedBy = "admin1";
                            deal31.DealPrice = tbDealPrice.Text.ToDecimal();
                            deal31.SchedulesCount = (int)numUpDown.Value;
                            deal31.Tax = 0;
                            deal31.TotalQty = tbTotalQty.Text.ToDecimal();
                            deal31.TotalTradeUnits = tbToralTradeUnits.Text.ToDecimal();
                            deal31.TradeUnitId = tradeUnit.Id;
                            deal31.UpdatedBy = "N/A";
                            deal31.DealDate = dtpDealDate.Value.Date;
                            deal31.TradeUnitPrice = tbPerTradeUnitPrice.Text.ToDecimal();
                            deal31.Unum = Helper.Unum;
                            deal31.CompletionStatus = string.Format("0/{0}", deal31.SchedulesCount);
                            deal31.DealStatus = AppDealStatus.Scheduled;
                            deal31.AppUserId = appUser.Id;
                            deal31.RawBrokerShareTypeId = rawBrokerShareType.Id;
                            switch (rawBrokerShareType.Title)
                            {
                                case "Per Packing":
                                    brokerSharePerPackPercentage = tbBrokerShare.Text.ToDecimal();
                                    break;
                                case "Percetange":
                                    brokerSharePerPackPercentage = tbBrokerShare.Text.ToDecimal() / 100;
                                    break;
                                case "None":
                                    brokerSharePerPackPercentage = 0;
                                    break;
                            }
                            deal31.BrokerSharePerPackPercentage = brokerSharePerPackPercentage;
                            deal31.BrokerShareAmount = 0;


                            var dbObj = db.AppDeals.ToList().FirstOrDefault(a => a.Equals(deal31));
                            if (dbObj != null)
                            {
                                throw new Exception("Same deal already exists in database");
                            }
                            db.AppDeals.Add(deal31);
                            db.SaveChanges();

                            foreach (ScheduleVM sch in scheduleVMList)
                            {
                                DealSchedule schedule = new DealSchedule
                                {
                                    DriverId = null,
                                    ArrivalDate = Convert.ToDateTime(sch.ExpectedDate),
                                    LaborExpenses = 0,
                                    LaborExpensesDescription = "N/A",
                                    IsArrived = false,
                                    AppDealId = deal31.Id,
                                    ScheduledPackingsUnits = sch.PackingUnits.Split('-')[0].ToDecimal(),
                                    ScheduledSubTradeUnits = sch.TotalSubUnits.Split('-')[0].ToDecimal(),
                                    PackingUnitTitle = packingUnit.Name,
                                    ScheduledPrice = sch.Price.ToDecimal(),
                                    ReadyDate = Convert.ToDateTime(sch.ExpectedDate).AddDays(-appSettings.DaysAlertBeforeReady).Date,
                                    ReceivedTradeUnits = 0,
                                    Remarks = "N/A",
                                    ScheduledTradeUnits = sch.TradeUnits.Split('-')[0].ToDecimal(),
                                    TradeUnitTitle = tradeUnit.Title,
                                    Status = ScheduleStatus.Scheduled,
                                    AddedBy = "admin1",
                                    AddedDate = DateTime.Now,
                                    AppDeal = null,
                                    Driver = null,
                                    LoadedPackingsUnits = 0,
                                    LoadedPrice = 0,
                                    LoadedSubTradeUnits = 0,
                                    LoadedTradeUnits = 0,
                                    OmeDeal = null,
                                    OmeDealId = null,
                                    ReceivedPackingsUnits = 0,
                                    ReceivedPrice = 0,
                                    ReceivedSubTradeUnits = 0,
                                    Selector = null,
                                    SelectorId = null,
                                    UpdatedBy = "N/A",
                                    DispatchedDate = null,
                                    IsDispatched = false,
                                    IsLoaded = false,
                                    LoadedDate = null,
                                    VehicleId = null,
                                    Unum = Helper.Unum,
                                    ScheduleRemarks = sch.Remarks,
                                    DispatchRemarks = "N/A",
                                    GoodCompanyId = null,
                                    LoadRemarks = "N/A",
                                    ReceiveRemarks = "N/A",
                                    TransId = 0,
                                };

                                schedule = db.DealSchedules.Add(schedule);
                                db.SaveChanges();
                                ScheduleAlarm salarm = new ScheduleAlarm
                                {
                                    Id = schedule.Id,
                                    ActiveDate = schedule.ReadyDate,
                                    EndTime = schedule.ArrivalDate.Value,
                                    GenerateTime = DateTime.Now,
                                    Status = true,
                                    Title = string.Format("Schedule Alarm. Deal No. {0}, \nCompany: {1}, \nBroker: {2}, \nDeal Item: {3}, \nItem Total Packing Qty: {4}, \nTotal Trade Units: {5}. \n\nNow Alarm for {6}, \n{7}, \n{8}, \nPrice: {9}, \nSchedule Generate time {10}, \nReady Date {11}.",
                                              deal31.Id, company.Name, broker.Name, dealItem.Name, (deal31.PackingQty.ToString() + " " + dealPacking.Name),
                                              (deal31.TotalTradeUnits.ToString() + " " + tradeUnit.Title), sch.PackingUnits, sch.TradeUnits, sch.TotalSubUnits, sch.Price,
                                              DateTime.Now, schedule.ArrivalDate.Value.ToShortDateString()),

                                };

                                db.ScheduleAlarms.Add(salarm);
                            }

                            db.SaveChanges();

                            trans.Commit();

                            Gujjar.InfoMsg(string.Format("Deal No. ({0}) has been added in database", deal31.Id));
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
        private void LoadRawBrokerTypes()
        {
            try
            {
                using (Context db = new Context())
                {
                    rawBrokerShareTypes = db.RawBrokerShares.ToList();
                }
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }
        private void BindCbRawBrokerShareTypes()
        {
            cbBrokerShareType.DataSource = rawBrokerShareTypes;
            cbBrokerShareType.DisplayMember = "Title";
            cbBrokerShareType.ValueMember = "Id";
        }
        private void tbPerTradeUnitPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                string tradeUnitPriceStr = tbPerTradeUnitPrice.Text;
                string totalTradeUnitsStr = tbToralTradeUnits.Text;

                if (string.IsNullOrEmpty(tradeUnitPriceStr) || string.IsNullOrEmpty(totalTradeUnitsStr))
                {
                    return;
                }
                decimal tradeUnitPrice = tradeUnitPriceStr.ToDecimal();
                decimal totalTradeUnits = totalTradeUnitsStr.ToDecimal();

                decimal dealPrice = tradeUnitPrice * totalTradeUnits;

                tbDealPrice.Text = dealPrice.ToString("n4");
            }
            catch (Exception exp)
            {
                tbPerTradeUnitPrice.Focus();
                Gujjar.ErrMsg(exp);
            }
        }

        private void cbBrokerShareType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBrokerShareType.SelectedIndex == -1)
                return;
            rawBrokerShareType = cbBrokerShareType.SelectedItem as RawBrokerShareType;
            string label = "N/A";
            bool state = false;
            switch (rawBrokerShareType.Title)
            {
                case "Per Packing":
                    state = true;
                    label = "Per packing price:";                    
                    break;
                case "Percetange":
                    state = true;
                    label = string.Format("Share in percentage:");
                    break;
                case "None":
                    label = "No broker share";
                    state = false;
                    break;
            }
            label26.Text = label;
            tbBrokerShare.Enabled = state;
            tbBrokerShare.Text = "0";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
