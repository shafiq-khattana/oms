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
using Model.Deal.Model;
using WinFom.Admin.Database;
using WinFom.Common.Forms;
using WinFom.Common.Model;

namespace WinFom.XtraCopy.Forms
{
    public partial class AddDealForm : Form
    {
        private List<Company> companies = new List<Company>();
        private List<Broker> brokers = new List<Broker>();
        private List<Selector> selectors = new List<Selector>();
        private List<DealItem> dealItems = new List<DealItem>();
        private List<DealPacking> dealPackings = new List<DealPacking>();
        private List<PackingUnit> packingUnits = new List<PackingUnit>();

        #region "Load Combo Data"
        private void LoadPackingUnits()
        {
            try
            {
                if(packingUnits.Count > 0)
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
            //try
            //{
                //using (Context db = new Context())
                //{
                //    selectors = db.Selectors.Where(a => a.IsActive)
                //        .OrderBy(a => a.Name).ToList();
                //}
            //}
            //catch (Exception exp)
            //{
            //    Gujjar.ErrMsg(exp);
            //}
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
        }
        #endregion
        public AddDealForm()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Gujjar.IsValidForm(panel2))
                {
                    throw new Exception("Please fill text fields");
                }

                //foreach (var ctrl in panel2.Controls)
                //{
                //    if (ctrl is ComboBox)
                //    {
                //        ComboBox cb = ctrl as ComboBox;
                //        if (cb.Text == "N/A")
                //        {
                //            throw new Exception("Please choose valid item from dropdown lists");
                //        }
                //    }
                //}
                tbPerPackingQty_TextChanged(null, null);
                tbDealPrice_Leave(null, null);
                tbBrokerAmount_Leave(null, null);
                tbFareAmount_Leave(null, null);

                Company comp = cbCompanies.SelectedItem as Company;
                Broker broker = cbBrokers.SelectedItem as Broker;
                //Selector2 selector = cbSelectors.SelectedItem as Selector2;
                DealPacking packing = cbPackings.SelectedItem as DealPacking;
                DealItem item = cbDealItems.SelectedItem as DealItem;
                PackingUnit packingUnit = cbPackingUnits.SelectedItem as PackingUnit;

                using (Context db = new Context())
                {
                    Model.Deal.Model.OmeDeal deal = new Model.Deal.Model.OmeDeal
                    {
                        //BrokerAmount = tbBrokerAmount.Text.ToDecimal(),
                        //BrokerAmountPercentage = tbBrokerAmountPercentage.Text.ToDecimal(),
                        //FareAmount = tbFareAmount.Text.ToDecimal(),
                        BrokerId = broker.Id,
                        CompanyId = comp.Id,
                        DealDate = DateTime.Now.Date,
                        DealItemId = item.Id,
                        DealPackingId = packing.Id,
                        DealPrice = tbDealPrice.Text.ToDecimal(),
                        PackingQty = tbPackingQty.Text.ToDecimal(),                       
                        Tax = tbTaxAmount.Text.ToDecimal(),
                        AddedBy = SingleTon.LoginForm.appUser.Id,
                        UpdatedBy = "N/A",
                        PackingUnitId = packingUnit.Id,
                        PerPackingQty = tbPerPackingQty.Text.ToDecimal(),
                        TotalQty = tbTotalQty.Text.ToDecimal()
                    };

                    var dbDeals = db.OmeDeals.ToList();
                    if(dbDeals.FirstOrDefault(a => a.Equals(deal)) != null)
                    {
                        throw new Exception("Same deal already exists in database");
                    }

                    string confirmMessage = string.Format("New deal confirmaton\nCompany: {0}, Broker: {1}, Selector: {2}, Item: {3}, Packing: {4}, Broker Amount: {5}," +
                        "Fare amount: {6}, Packing Qty: {7}({8}), Deal Price: {9}, Tax amount {10}, Deal total price: {11}\nPer pack qty: {12}, Total qty: {13}({14})",
                        comp.Name, broker.Name, "",item.Name, packing.Name, 0, deal.FareAmount, deal.PackingQty, packing.Name, deal.DealPrice,
                        deal.Tax, deal.PerPackingQty, deal.TotalQty, packingUnit.Name);

                    DialogResult res = Gujjar.ConfirmYesNo(confirmMessage);
                    if(res == DialogResult.Yes)
                    {
                        deal = db.OmeDeals.Add(deal);
                        db.SaveChanges();
                        Gujjar.InfoMsg(string.Format("Deal No: {0}, added in database successfully", deal.Id));
                        Close();
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void NumsOnly()
        {
            Gujjar.NumbersOnly(tbPackingQty);
            Gujjar.NumbersOnly(tbDealPrice);
            Gujjar.NumbersOnly(tbTaxAmount);
            Gujjar.NumbersOnly(tbFareAmount);
        }
        private void AddDealForm_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm w3 = new WaitForm(LoadAllComboData);
                w3.ShowDialog();

                BindAllCBs();
                Gujjar.TB4(panel2);
                NumsOnly();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
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

        private void btnAddSelector_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    AddSelectorForm form = new AddSelectorForm();
            //    form.ShowDialog();

            //    if (form.SelectorId != 0)
            //    {
            //        WaitForm w3 = new WaitForm(LoadSelectors, "");
            //        w3.ShowDialog();

            //        BindCBSelectors();
            //        cbSelectors.SelectedItem = cbSelectors.Items.OfType<Selector2>()
            //            .FirstOrDefault(a => a.Id == form.SelectorId);
            //    }
            //}
            //catch (Exception exp)
            //{
            //    Gujjar.ErrMsg(exp);
            //}
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
            label14.Text = string.Format("{0}-(s)", cbPackings.Text);
            label15.Text = string.Format("/ {0}", cbPackings.Text);
            label7.Text = string.Format("No of {0}-(s)", cbPackings.Text);
            label19.Text = string.Format("Per {0} qty:", cbPackings.Text);
        }

        private void tbDealPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                string qtyStr = tbPackingQty.Text;
                string dealPriceStr = tbDealPrice.Text;

                if (!string.IsNullOrEmpty(qtyStr) && !string.IsNullOrEmpty(dealPriceStr))
                {
                    decimal qty = qtyStr.ToDecimal();
                    if (qty == 0)
                        return;
                    decimal dealPrice = dealPriceStr.ToDecimal();

                    decimal total = dealPrice / qty;
                    tbUnitPrice.Text = total.ToString("n2");
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbFareAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbFareAmount_Leave(object sender, EventArgs e)
        {
            try
            {
                string fareAmountStr = tbFareAmount.Text;
                if (string.IsNullOrEmpty(fareAmountStr))
                    return;

                decimal fareAmount = fareAmountStr.ToDecimal();
                decimal taxAmount = 0;
                string taxStr = tbTaxAmount.Text;
                if (!string.IsNullOrEmpty(taxStr))
                {
                    taxAmount = taxStr.ToDecimal();
                }
                decimal dealAmount = 0;
                string dealAmountStr = tbDealPrice.Text;
                if (!string.IsNullOrEmpty(dealAmountStr))
                {
                    dealAmount = dealAmountStr.ToDecimal();
                }

                string brokerAmountStr = tbBrokerAmount.Text;
                decimal brokerAmount = 0;
                if (!string.IsNullOrEmpty(brokerAmountStr))
                    brokerAmount = brokerAmountStr.ToDecimal();

                decimal total = dealAmount + fareAmount + taxAmount + brokerAmount;
                tbTotalDealPrice.Text = total.ToString("n2");

                string qtyStr = tbPackingQty.Text;
                decimal qty = 0;
                if (!string.IsNullOrEmpty(qtyStr))
                {
                    qty = qtyStr.ToDecimal();
                }
                if (qty > 0)
                {
                    decimal totalUnitPrice = total / qty;
                    tbTotalDealUnitPrice.Text = totalUnitPrice.ToString("n2");
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbBrokerAmount_Leave(object sender, EventArgs e)
        {
            try
            {
                string amountStr = tbBrokerAmount.Text;
                if (string.IsNullOrEmpty(amountStr))
                    return;
                string dealPriceStr = tbDealPrice.Text;
                if (string.IsNullOrEmpty(dealPriceStr))
                    return;

                decimal brokerAmount = amountStr.ToDecimal();
                decimal dealPrice = dealPriceStr.ToDecimal();

                float share = (float)(brokerAmount / dealPrice) * 100;
                tbBrokerAmountPercentage.Text = share.ToString("n2");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbBrokerAmountPercentage_Leave(object sender, EventArgs e)
        {
            try
            {
                string amountStrPercen = tbBrokerAmountPercentage.Text;
                if (string.IsNullOrEmpty(amountStrPercen))
                    return;
                string dealPriceStr = tbDealPrice.Text;
                if (string.IsNullOrEmpty(dealPriceStr))
                    return;

                decimal brokerAmountPercen = amountStrPercen.ToDecimal();
                decimal dealPrice = dealPriceStr.ToDecimal();

                float share = (float)(brokerAmountPercen / 100 * dealPrice);
                tbBrokerAmount.Text = share.ToString("n2");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddUnit_Click(object sender, EventArgs e)
        {
            try
            {
                AddPackingUnitForm form = new AddPackingUnitForm();
                form.ShowDialog();
                int puid = form.PackingUnitId;
                if(puid != 0)
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
            label21.Text = cbPackingUnits.Text;
        }

        private void tbPerPackingQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string perPackQtyStr = tbPerPackingQty.Text;
                if (string.IsNullOrEmpty(perPackQtyStr))
                {
                    tbTotalQty.Text = "0";
                    return;
                }
                   

                string packingQtyStr = tbPackingQty.Text;
                if(string.IsNullOrEmpty(perPackQtyStr))
                {
                    tbTotalQty.Text = "0";
                    return;
                }

                decimal perPackQty = perPackQtyStr.ToDecimal();
                decimal packingQty = perPackQtyStr.ToDecimal();
                decimal totalQty = perPackQty * packingQty;

                tbTotalQty.Text = totalQty.ToString();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
