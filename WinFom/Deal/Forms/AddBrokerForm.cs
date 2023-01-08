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
using Model.Financials.Model;

namespace WinFom.Deal.Forms
{
    public partial class AddBrokerForm : Form
    {
        public int BrokerId = 0;
        public AddBrokerForm()
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
                Broker comp = new Broker
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
                    var dbObj = db.Brokers.ToList().FirstOrDefault(a => a.Equals(comp));
                    if (dbObj != null)
                    {
                        throw new Exception(string.Format("Broker ({0}), with address ({1}) with contact ({2}) already exists in database. ", dbObj.Name, dbObj.Address, dbObj.Contact));
                    }
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            GeneralAccount brokerAct = new GeneralAccount
                            {
                                Id = Guid.NewGuid().ToString(),
                                AccountNature = AccountNature.Credit,
                                SubHeadAccountId = Properties.Resources.Creditors,
                                Title = string.Format("{0} -Broker account", comp.Name),
                                AccountNo = "N/A",
                                AccountTransactions = null,
                                Address = "N/A",
                                Brokers = null,
                                Companies = null,
                                Customers = null,
                                SubHeadAccount = null,
                                DealItems = null,
                                Description = "Broker Account",
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
                            db.Accounts.Add(brokerAct);
                            db.SaveChanges();
                            comp.GeneralAccountId = brokerAct.Id;


                            comp = db.Brokers.Add(comp);
                            db.SaveChanges();
                            trans.Commit();
                            BrokerId = comp.Id;
                            Gujjar.InfoMsg(string.Format("Broker with name ({0}) added in database successfully", comp.Name));
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
    }
}
