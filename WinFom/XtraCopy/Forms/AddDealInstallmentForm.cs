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

using WinFom.Common.Forms;
using Model.Deal.Model;
using WinFom.Admin.Database;
using System.Data.Entity;
using WinFom.Common.Model;

namespace WinFom.XtraCopy.Forms
{
    public partial class AddDealInstallmentForm : Form
    {
        int dealId = 0;
        private Model.Deal.Model.OmeDeal deal = null;
        //private List<Driver> driverList = new List<Driver>();
        public AddDealInstallmentForm(int dealId)
        {
            InitializeComponent();
            this.dealId = dealId;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }

       
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string qtyLoaded = tbLoadedQty.Text;
                string qtyReceived = tbReceivedQty.Text;
                string vehicleNo = tbVehicleNo.Text;
                string vehicleType = cbVehicleTypes.Text;

                if(string.IsNullOrEmpty(qtyLoaded))
                {
                    tbLoadedQty.BackColor = Color.Pink;
                    tbLoadedQty.Focus();
                    throw new Exception("Please enter source loaded qty");
                }
                if (string.IsNullOrEmpty(qtyReceived))
                {
                    tbReceivedQty.BackColor = Color.Pink;
                    tbReceivedQty.Focus();
                    throw new Exception("Please enter destination receiving qty");
                }
                if(string.IsNullOrEmpty(vehicleNo))
                {
                    tbVehicleNo.BackColor = Color.Pink;
                    tbVehicleNo.Focus();
                    throw new Exception("Please enter vehicle no");
                }
                if(string.IsNullOrEmpty(vehicleType))
                {
                    throw new Exception("Please select vehicle type form drop down list");
                }
                tbReceivedQty_Leave(null, null);
                //Driver2 driver = cbDrivers.SelectedItem as Driver2;

                //decimal remainingQty = tbRemainingQty.Text.ToDecimal();
                //decimal qtyReceived = tb
                //DealInstallment2 dealInstallment = new DealInstallment2
                //{
                //    DealId = dealId,
                //    LoadedQty = qtyLoaded.ToDecimal(),
                //    QtyReceived = qtyReceived.ToDecimal(),
                //    ReceiveDateTime = DateTime.Now,
                //    DriverId = driver.Id,
                //    ReceivedBy = SingleTon.LoginForm.appUser.UserId,
                //    VehicleNo = vehicleNo,
                //    VehicleType = vehicleType
                //};

                DialogResult res = Gujjar.ConfirmYesNo("Please confirm\nAll entries are correct?");
                if(res == DialogResult.Yes)
                {
                    using (Context db = new Context())
                    {
                        //db.DealInstallment2.Add(dealInstallment);
                        db.SaveChanges();

                        //decimal totalReceived = deal.DealInstallments.Sum(a => a.LoadedQty);
                        //if(totalReceived >= deal.PackingQty)
                        //{
                        //    string msg = string.Format("Deal No ({0}) has received qty ({1}) from its deal qty ({2}).\nNow would you like to complete this deal?", dealId, totalReceived, deal.PackingQty);
                        //    DialogResult res2 = Gujjar.ConfirmYesNo(msg);
                        //    if(res2 == DialogResult.Yes)
                        //    {
                        //        var deal2 = db.Deals2.Find(dealId);
                        //        deal2.IsCompleted = true;
                        //        db.Entry(deal2).State = EntityState.Modified;
                        //        db.SaveChanges();
                        //    }
                        //}
                        //Gujjar.InfoMsg("Deal installment added in database");
                        //Close();
                    }
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

        private void AddBrokerForm_Load(object sender, EventArgs e)
        {
            Gujjar.TB4(panel2);
        }

        private void AddDealInstallmentForm_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait = new WaitForm(LoadDealObj, "");
                wait.ShowDialog();
                if(deal != null)
                {
                    BindControls();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadDealObj()
        {
            try
            {
                using (Context db = new Context())
                {
                    //deal = db.Deals.Include(a => a.Broker)
                    //    .Include(a => a.Selector)
                    //    .Include(a => a.Company)
                    //    .Include(a => a.DealItem)
                    //    .Include(a => a.Packing)
                    //    .Include(a => a.DealInstallments)
                    //    .FirstOrDefault(a => a.Id == dealId);

                    //if(deal == null)
                    //{
                    //    throw new Exception("Deal not foud with no " + dealId.ToString());
                    //}

                    //LoadDrivers();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void BindControls()
        {
            tbDealNo.Text = deal.Id.ToString();
            tbItemName.Text = deal.DealItem.Name;
            tbPackingName.Text = deal.Packing.Name;
            tbCompanyName.Text = deal.Company.Name;
            tbBrokerName.Text = deal.Broker.Name;
            //tbSelectorName.Text = deal.Selector.Name;
            //tbDealDate.Text = Gujjar.LocalDate(deal.DealDate);
            //tbTotalQty.Text = deal.PackingQty.ToString();
            //decimal totalReceived = deal.DealInstallments.Sum(a => a.LoadedQty);
            //if(deal.PackingQty == totalReceived)
            //{
            //    label28.BackColor = Color.Lime;
            //}
            //else
            //{
            //    label28.BackColor = Color.Red;
            //}
            //tbTotalReceivedQty.Text = totalReceived.ToString();
            //decimal diff = deal.DealInstallments.Sum(a => a.QtyDifference);
            //tbTotalQtyDifference.Text = diff.ToString();

            //label14.Text = label15.Text = label16.Text = label17.Text = label18.Text = label19.Text = label25.Text = label27.Text = deal.Packing.Name;
            //tbActualQty.Text = deal.DealInstallments.Sum(a => a.QtyReceived).ToString();
            //decimal remaining = deal.PackingQty - totalReceived;
            //tbRemainingQty.Text = remaining.ToString();
            //BindCbDrivers();
        }

        private void tbReceivedQty_Leave(object sender, EventArgs e)
        {
            string loadedStr = tbLoadedQty.Text;
            string receivedStr = tbReceivedQty.Text;

            if (string.IsNullOrEmpty(loadedStr) || string.IsNullOrEmpty(receivedStr))
                return;

            decimal loaded = loadedStr.ToDecimal();
            decimal received = receivedStr.ToDecimal();

            decimal diff = loaded - received;
            tbQtyDiff.Text = diff.ToString();
        }

        private void btnAddDriver_Click(object sender, EventArgs e)
        {
            try
            {
                AddDriverForm form = new AddDriverForm();
                form.ShowDialog();

                int did = form.DriverId;
                if(did != 0)
                {
                    WaitForm wait = new WaitForm(LoadDrivers, "");
                    wait.ShowDialog();

                    BindCbDrivers();

                    //cbDrivers.SelectedItem = cbDrivers.Items.OfType<Driver2>()
                    //    .FirstOrDefault(a => a.Id == did);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void BindCbDrivers()
        {
            //cbDrivers.DataSource = driverList;
            //cbDrivers.DisplayMember = "Name";
            //cbDrivers.ValueMember = "Id";
        }

        private void LoadDrivers()
        {
            try
            {
                //using (Context db = new Context())
                //{
                //    driverList = db.Drivers2.ToList();
                //}
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
