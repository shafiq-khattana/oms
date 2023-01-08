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
using Model.Financials.Model;

namespace WinFom.Deal.Forms
{
    public partial class AddCompanyForm : Form
    {
        public int CompId = 0;
        public AddCompanyForm()
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
                Gujjar.TB4(pMain);
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
                if (!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text boxes");
                }

                Company comp = new Company
                {
                    Address = tbAddress.Text,
                    Contact = tbContact.Text,
                    DateAdded = DateTime.Now,
                    Extra = tbMisc.Text,
                    IsActive = true,
                    Name = tbCompany.Text,
                    Remarks = tbRemarks.Text
                };

                using (Context db = new Context())
                {
                    var obj3 = db.Companies.ToList().FirstOrDefault(a => a.Equals(comp));
                    if(obj3 != null)
                    {
                        throw new Exception("Company already exists in database");
                    }

                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            GeneralAccount companyAct = new GeneralAccount
                            {
                                Id = Guid.NewGuid().ToString(),
                                AccountNature = AccountNature.Credit,
                                SubHeadAccountId = Properties.Resources.Creditors,
                                Title = string.Format("{0} account", comp.Name),
                                AccountNo = "N/A",
                                AccountTransactions = null,
                                Address = "N/A",
                                Brokers = null,
                                Companies = null,
                                Customers = null,
                                SubHeadAccount = null,
                                DealItems = null,
                                Description = "Company Account",
                                Employees = null,
                                ExplicitilyCreated = false,
                                GoodCompanies = null,
                                OilDealBrokers = null,
                                OilDealItems = null,
                                OilDealSelectors = null,
                                OilDirtBrokers = null,
                                OilDirtItems = null,
                                OilDirtSelectors = null,
                                ReadyBrokers = null,
                                ReadyItems = null,
                                ReadySelectors = null,
                                Selectors = null,
                                CrDrType = CrDrType.Creditor
                            };

                            db.Accounts.Add(companyAct);
                            db.SaveChanges();
                            comp.GeneralAccountId = companyAct.Id;
                            comp = db.Companies.Add(comp);
                            db.SaveChanges();
                            trans.Commit();
                            CompId = comp.Id;
                            Gujjar.InfoMsg(string.Format("Company with name ({0}) added in database successfully", comp.Name));
                            Close();
                        }
                        catch (Exception pe)
                        {
                            trans.Rollback();
                            throw pe;
                        }
                    }

                        
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbCompany_Leave(object sender, EventArgs e)
        {
            try
            {
                string comp = tbCompany.Text;
                if (!string.IsNullOrEmpty(comp))
                {
                    using (Context db = new Context())
                    {
                        var obj = db.Companies.ToList().FirstOrDefault(a => a.Name.ToLower().Equals(comp.ToLower()));
                        if (obj != null)
                        {
                            tbCompany.BackColor = Color.Pink;
                            tbCompany.Focus();
                            throw new Exception(string.Format("Company {0} already exists", comp));
                        }
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
    }
}
