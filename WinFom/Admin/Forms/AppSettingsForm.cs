using Khattana.Common;
using Model.Admin.Model;
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
using WinFom.Common.Forms;
using WinFom.Common.Model;

namespace WinFom.Admin.Forms
{
    public partial class AppSettingsForm : Form
    {
        private byte[] picData = null;
        private AppSettings appSettings = null;
        public AppSettingsForm()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
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
        private void button1_Click(object sender, EventArgs e)
        {
            new SampleControls().ShowDialog();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dial = new OpenFileDialog())
            {
                try
                {
                    dial.Filter = "JPG Files (*.jpg) | *.jpg";
                    DialogResult result = dial.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        pictureBox3.Image = Image.FromFile(dial.FileName);
                        Image img = pictureBox3.Image;
                        picData = Gujjar.GetByteArrayFromImage(img);
                    }
                }
                catch (Exception exp)
                {
                    Gujjar.ErrMsg(exp);
                }
            }
        }

        private void AppSettingsForm_Load(object sender, EventArgs e)
        {
            try
            {
                Helper.IsOkApplied();
                WaitForm wait = new WaitForm(LoadAppSettings);
                wait.ShowDialog();
                Gujjar.NumbersOnly(tbMailBrokerSharePercentage);
                Gujjar.NumbersOnly(tbOilBrokerSharePercentage);
                Gujjar.NumbersOnly(tbOilCakeBrokerSharePercentage);

                Gujjar.UrduFont(tbFactoryNameUrdu);
                Gujjar.UrduFont(tbFactoryAddressUrdu);

                tbFactoryAddressUrdu.Text = appSettings.AddressUrdu;
                tbFactoryNameUrdu.Text = appSettings.NameUrdu;
                tbOilCakeBrokerSharePercentage.Text = appSettings.OilCakeBrokerSharePercentage.ToString("n2");
                tbOilBrokerSharePercentage.Text = appSettings.OilBrokerSharePercentage.ToString("n2");
                tbMailBrokerSharePercentage.Text = appSettings.MailBrokerSharePercentage.ToString("n2");
                bsAllowMailBrokerPercentage.Value = appSettings.AllowToChangeMailBrokerSharePercentage;
                bsAllowOilBrokerPercentage.Value = appSettings.AllowToChangeOilBrokerSharePercentage;
                bsAllowOilCakeBrokerPercentage.Value = appSettings.AllowToChargeOilCakeBrokerSharePercentage;

                tbMainPrinter.Text = appSettings.MainPrinter;
                tbName.Text = appSettings.Name;
                tbPhone.Text = appSettings.PhoneNo;
                tbTokenPrinter.Text = appSettings.ThermalPrinter;
                pictureBox3.Image = Gujjar.GetImageFromByteArray(appSettings.Logo);
                numUpDown.Value = appSettings.DaysAlertBeforeReady;
                picData = appSettings.Logo;
                tbAddress.Text = appSettings.Address;
                dtpEndDate.Value = appSettings.EndDate;
                dtpStartDate.Value = appSettings.StartDate;
                bswEnableDiscounts.Value = appSettings.EnableDiscounts;
                tbSalesTax.Text = appSettings.SalesTaxPercentage.ToString("n2");
                tbServiceCharges.Text = appSettings.ServiceCharges.ToString("n2");
                tbOfferDiscount.Text = appSettings.OfferDiscountPercentage.ToString("n2");
                SetDifference();
                tbMasterPass.Text = appSettings.MasterPassword;
                tbSecurityPass.Text = appSettings.SecurityPassword;
                numGatePass.Value = appSettings.GatePassCopies;
                numFloorScale.Value = appSettings.FloorScaleCopies;
                numCustomer.Value = appSettings.CustomerCopies;
                numOffice.Value = appSettings.OfficeCopies;
                bswPrintFianancialTransactions.Value = appSettings.PrintFinancialTransactions;
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
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date;

                if(endDate <= startDate)
                {
                    throw new Exception("End date can't be smaller than start date");
                }
                if(endDate <= DateTime.Now.Date)
                {
                    throw new Exception("End date can't be less than today's date");
                }
                using (Context db = new Context())
                {
                    var set = db.AppSettings.First();

                    set.Address = tbAddress.Text;
                    set.DaysAlertBeforeReady = (int)numUpDown.Value;
                    set.Logo = picData;
                    set.MainPrinter = tbMainPrinter.Text;
                    set.Name = tbName.Text;
                    set.PhoneNo = tbPhone.Text;
                    set.ThermalPrinter = tbTokenPrinter.Text;
                    set.StartDate = startDate;
                    set.EndDate = endDate;

                    set.OilCakeBrokerSharePercentage = tbOilCakeBrokerSharePercentage.Text.ToDecimal();
                    set.OilBrokerSharePercentage  = tbOilBrokerSharePercentage.Text.ToDecimal();
                    set.MailBrokerSharePercentage = tbMailBrokerSharePercentage.Text.ToDecimal();
                    set.AllowToChangeMailBrokerSharePercentage = bsAllowMailBrokerPercentage.Value;
                    set.AllowToChangeOilBrokerSharePercentage = bsAllowOilBrokerPercentage.Value;
                    set.AllowToChargeOilCakeBrokerSharePercentage = bsAllowOilCakeBrokerPercentage.Value;

                    set.EnableDiscounts = bswEnableDiscounts.Value;
                    set.ServiceCharges = tbServiceCharges.Text.ToDecimal();
                    set.SalesTaxPercentage = tbSalesTax.Text.ToDecimal();
                    set.OfferDiscountPercentage = tbOfferDiscount.Text.ToDecimal();
                    set.MasterPassword = tbMasterPass.Text;
                    set.SecurityPassword = tbSecurityPass.Text;
                    set.GatePassCopies = (int)numGatePass.Value;
                    set.FloorScaleCopies = (int)numFloorScale.Value;
                    set.CustomerCopies = (int)numCustomer.Value;
                    set.OfficeCopies = (int)numOffice.Value;
                    set.PrintFinancialTransactions = bswPrintFianancialTransactions.Value;
                    set.NameUrdu = tbFactoryNameUrdu.Text;
                    set.AddressUrdu = tbFactoryAddressUrdu.Text;

                    db.Entry(set).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    Gujjar.InfoMsg("App Settings are updated successfully");
                    Close();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            SetDifference();
        }
        private void SetDifference()
        {
            tbDifference.Text = (dtpEndDate.Value - dtpStartDate.Value).Days.ToString() + " Days";
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            SetDifference();
        }

        private void chbSecurity_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            switch(box.CheckState)
            {
                case CheckState.Checked:
                    tbSecurityPass.UseSystemPasswordChar = false;
                    //tbMasterPass.UseSystemPasswordChar = false;
                    break;
                default:
                    tbSecurityPass.UseSystemPasswordChar = true;
                    //tbMasterPass.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void chbMaster_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            switch (box.CheckState)
            {
                case CheckState.Checked:
                    //tbSecurityPass.UseSystemPasswordChar = false;
                    tbMasterPass.UseSystemPasswordChar = false;
                    break;
                default:
                    //tbSecurityPass.UseSystemPasswordChar = true;
                    tbMasterPass.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tbDifference_TextChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnLicenseInfo_Click(object sender, EventArgs e)
        {
            try
            {
                using (Context db = new Context())
                {
                    var abc = db.Anattakh.FirstOrDefault();
                    if(abc != null)
                    {
                        TrialForm form = new TrialForm();
                        form.ShowDialog();
                    }
                    else
                    {
                        KeyInfoForm form = new KeyInfoForm();
                        form.ShowDialog();
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnZerozero_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Helper.ConfirmAdminPassword())
                    return;

                for (int i = 0; i < 3; i++)
                {
                    DialogResult rest = Gujjar.ConfirmYesNo(string.Format("All account transaction records and day book entry records will be deleted, and all account balances will be zero. Are you confirmed?. You will be asked 3 times. This is {0} time", i + 1));
                    if (rest == DialogResult.No)
                        return;
                }
                DialogResult rest2 = Gujjar.ConfirmYesNo("Once again, at very very last time, are you sured to delete all account data?");
                if (rest2 == DialogResult.No)
                    return;

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var atrans = db.AccountTransactions.ToList();
                            foreach (var item in atrans)
                            {
                                db.AccountTransactions.Remove(item);
                            }
                            db.SaveChanges();

                            var dbentries = db.DayBooks.ToList();
                            foreach (var item in dbentries)
                            {
                                db.DayBooks.Remove(item);
                            }
                            db.SaveChanges();

                            trans.Commit();
                            Gujjar.InfoMsg("All accounts data is set to zero balance");
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
    }
}
