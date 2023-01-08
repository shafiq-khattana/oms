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
using Model.Financials.Model;

namespace WinFom.ReadyStuff.Forms
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
                Gujjar.TB4(pMain);
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
                ReadyBroker comp = new ReadyBroker
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
                            string na = "N/A";
                            string acctTitle = string.Format("{0} (oil cake broker) account", comp.Name);
                            GeneralAccount brokerAccount = new GeneralAccount
                            {
                                Id = Guid.NewGuid().ToString(),
                                AccountNature = AccountNature.Debit,
                                AccountNo = "N/A",
                                Address = na,
                                AccountTransactions = null,
                                Balance = 0,
                                BankName = na,
                                ExplicitilyCreated = false,
                                Description = acctTitle,
                                Title = acctTitle,
                                SubHeadAccountId = Properties.Resources.Debtors,
                                CrDrType = CrDrType.Debitor
                            };

                            db.Accounts.Add(brokerAccount);
                            db.SaveChanges();

                            string acctTitle2 = string.Format("{0} (oil cake brokery expense) account", comp.Name);
                            GeneralAccount brokeryExpenseAccount = new GeneralAccount
                            {
                                Id = Guid.NewGuid().ToString(),
                                AccountNature = AccountNature.Debit,
                                AccountNo = "N/A",
                                Address = na,
                                AccountTransactions = null,
                                Balance = 0,
                                BankName = na,
                                ExplicitilyCreated = false,
                                Description = acctTitle2,
                                Title = acctTitle2,
                                SubHeadAccountId = Properties.Resources.BrokeryExpenseSubHead
                            };

                            db.Accounts.Add(brokeryExpenseAccount);
                            db.SaveChanges();

                            comp.BrokerExpenseAccountId = brokeryExpenseAccount.Id;
                            comp.GeneralAccountId = brokerAccount.Id;

                            comp = db.ReadyBrokers.Add(comp);
                            db.SaveChanges();
                            trans.Commit();

                            BrokerId = comp.Id;
                            Gujjar.InfoMsg(string.Format("Broker with name ({0}) added in database successfully", comp.Name));
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
    }
}
