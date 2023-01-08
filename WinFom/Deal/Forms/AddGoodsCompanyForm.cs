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
    public partial class AddGoodsCompanyForm : Form
    {
        public int GoodCompanyId = 0;
        public AddGoodsCompanyForm()
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
                GoodCompany comp = new GoodCompany
                {
                    Address = tbAddress.Text,
                    Phone = tbContact.Text,
                    DateAdded = DateTime.Now,
                    Extra = tbMisc.Text,
                    IsActive = true,
                    Name = tbCompany.Text,
                    Remarks = tbRemarks.Text,
                    Owner = tbOwner.Text,
                    OwnerContact = tbOwnerContact.Text
                };
                using (Context db = new Context())
                {
                    var dbObj = db.GoodCompanies.ToList().FirstOrDefault(a => a.Equals(comp));
                    if (dbObj != null)
                    {
                        throw new Exception(string.Format("Good Company ({0}), with address ({1}) with contact ({2}) already exists in database. ", dbObj.Name, dbObj.Address, dbObj.Phone));
                    }
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            GeneralAccount account = new GeneralAccount
                            {
                                Id = Guid.NewGuid().ToString(),
                                AccountNature = AccountNature.Credit,
                                AccountNo = "N/A",
                                AccountTransactions = null,
                                Address = "N/A",
                                SubHeadAccountId = Properties.Resources.Creditors,
                                Title = string.Format("{0} (Goods Company) account", comp.Name),
                                ExplicitilyCreated = false,
                                Description = string.Format("Goods Company ({0}) payable account", comp.Name),
                                CrDrType = CrDrType.Creditor
                            };
                            db.Accounts.Add(account);
                            db.SaveChanges();

                            comp.GeneralAccountId = account.Id;
                            comp = db.GoodCompanies.Add(comp);
                            db.SaveChanges();
                            GoodCompanyId = comp.Id;
                            trans.Commit();

                            Gujjar.InfoMsg(string.Format("Goods Company with name ({0}) added in database successfully", comp.Name));
                            Close();
                        }
                        catch (Exception ased)
                        {
                            trans.Rollback();
                            throw ased;
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
