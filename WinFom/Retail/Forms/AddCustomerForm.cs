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
using Model.ReadyStuff.Model;
using Model.OilDirtStuff.Model;
using Model.Retail.Model;
using WinFom.Common.Forms;
using WinFom.Common.Model;
using Model.Financials.Model;

namespace WinFom.Retail.Forms
{
    public partial class AddCustomerForm : Form
    {
        private List<CustomerCategory> custCategories = null;
        public int CustomerId = 0;
        public AddCustomerForm()
        {
            InitializeComponent();
        }
        private void LoadCustCats()
        {
            try
            {
                using (Context db = new Context())
                {
                    custCategories = db.CustomerCategories.ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindCBCustCats()
        {
            cbCustCategories.DataSource = custCategories;
            cbCustCategories.DisplayMember = "Title";
            cbCustCategories.ValueMember = "Id";

            cbCustCategories.SelectedItem = cbCustCategories.Items.OfType<CustomerCategory>()
                .FirstOrDefault(a => a.Id == 1);
        }
        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                Gujjar.TB4(pMain);
                Gujjar.NumbersOnly(tbYears);

                WaitForm wait1 = new WaitForm(LoadCustCats);
                wait1.ShowDialog();
                BindCBCustCats();
                tbCardNo.Text = "N/A";
                Gujjar.TBOptional(tbCardNo);

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
                if (!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text boxes");
                }
                if(cbCustCategories.SelectedIndex == -1)
                {
                    throw new Exception("Please select customer category");
                }

                DialogResult res = Gujjar.ConfirmYesNo("Are you sure before to add the customer");
                if (res == DialogResult.No)
                    return;
                CustomerCategory cat = cbCustCategories.SelectedItem as CustomerCategory;

                Customer customer = new Customer
                {
                    Address = tbAddress.Text,
                    ApplyCardDiscount = bswApplyCardDiscount.Value,
                    CardEndDate = dtpExpirayDate.Value.Date,
                    DateAdded = DateTime.Now,
                    CardNo = tbCardNo.Text,
                    CardStartDate = dtpStart.Value.Date,
                    CNIC = tbCNIC.Text,
                    Contact = tbContact.Text,
                    IsActive = true,
                    CustomerCategory = null,
                    CustomerCategoryId = cat.Id,
                    Id = 0,
                    DiscountPercentage = cat.Discount,
                    Name = tbName.Text,
                    Points = 0,
                    Remarks = tbRemarks.Text,
                    SaleOrders = null,
                    IsEmployee = false
                };

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var obj = db.Customers.AsParallel().ToList().FirstOrDefault(a => a.Equals(customer));
                            if (obj != null)
                            {
                                throw new Exception("Same Customer already exists in database");
                            }
                            string acctTitle = string.Format("{0} (customer) account", customer.Name);
                            GeneralAccount custAccount = new GeneralAccount
                            {
                                AccountNature = AccountNature.Debit,
                                AccountNo = "N/A",
                                Address = "N/A",
                                BankName = "N/A",
                                Description = acctTitle,
                                ExplicitilyCreated = false,
                                Id = Guid.NewGuid().ToString(),
                                SubHeadAccountId = Properties.Resources.Debtors,
                                Title = acctTitle,
                                SubHeadAccount = null,
                                CrDrType = CrDrType.Debitor
                            };

                            db.Accounts.Add(custAccount);
                            db.SaveChanges();

                            customer.GeneralAccountId = custAccount.Id;
                            customer = db.Customers.Add(customer);

                            db.SaveChanges();
                            trans.Commit();

                            SingleTon.LoginForm.AppCustomers.Add(customer);
                            Gujjar.InfoMsg("Customer iformation is added in database");
                            CustomerId = customer.Id;
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

        private void bswApplyCardDiscount_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = bswApplyCardDiscount.Value;
            if(!bswApplyCardDiscount.Value)
            {
                tbYears.Text = "0";
            }
        }

        private void tbYears_TextChanged(object sender, EventArgs e)
        {
            string txt = tbYears.Text;
            if(string.IsNullOrEmpty(txt))
            {
                dtpExpirayDate.Value = dtpStart.Value;
            }
            else
            {
                int years = txt.ToInt();
                dtpExpirayDate.Value = dtpStart.Value.AddYears(years);
            }
        }

        private void cbCustCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCustCategories.SelectedIndex == -1)
                return;

            CustomerCategory cat = cbCustCategories.SelectedItem as CustomerCategory;
            tbCustDiscount.Text = string.Format("{0}-%", cat.Discount.ToString("n2"));
        }

        private void btnAddCustCategory_Click(object sender, EventArgs e)
        {
            try
            {
                AddCustomerCategoryForm form = new AddCustomerCategoryForm();
                form.ShowDialog();
                int id = form.CateId;
                if(id != 0)
                {
                    LoadCustCats();
                    BindCBCustCats();

                    cbCustCategories.SelectedItem = cbCustCategories.Items.OfType<CustomerCategory>()
                        .FirstOrDefault(a => a.Id == id);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
