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
using Model.RetailBardanaManaged.Model;
using Model.Deal.Model;
using WinFom.Admin.Database;
using Model.ReadyStuff.Model;
using Model.Financials.Model;

namespace WinFom.RetailBardanaManagedUI.Forms
{
    public partial class AddRetailBardanaSupplier : Form
    {
        public int SupplierId = 0;
        public AddRetailBardanaSupplier()
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
                tbRemarks.Text = "N/A";
                Gujjar.TBOptional(tbRemarks);
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
                RetailBardanaSupplier comp = new RetailBardanaSupplier
                {
                    Address = tbAddress.Text,
                    Contact = tbContact.Text,
                    DateAdded = DateTime.Now,
                    IsActive = true,
                    Name = tbCompany.Text,
                    Remarks = tbRemarks.Text
                };
                using (Context db = new Context())
                {
                    var dbObj = db.RetailBardanaSuppliers.ToList().FirstOrDefault(a => a.Equals(comp));
                    if (dbObj != null)
                    {
                        throw new Exception(string.Format("Supplier ({0}), with address ({1}) with contact ({2}) already exists in database. ", dbObj.Name, dbObj.Address, dbObj.Contact));
                    }
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            string na = "N/A";
                            string acctTitle = string.Format("{0} (retail bardana supplier) account", comp.Name);
                            GeneralAccount supplierAccount = new GeneralAccount
                            {
                                Id = Guid.NewGuid().ToString(),
                                AccountNature = AccountNature.Credit,
                                AccountNo = "N/A",
                                Address = na,
                                AccountTransactions = null,
                                Balance = 0,
                                BankName = na,
                                ExplicitilyCreated = false,
                                Description = acctTitle,
                                Title = acctTitle,
                                SubHeadAccountId = Properties.Resources.Creditors,
                                CrDrType = CrDrType.Creditor
                            };

                            db.Accounts.Add(supplierAccount);
                            db.SaveChanges();
                            
                            comp.GeneralAccountId = supplierAccount.Id;

                            comp = db.RetailBardanaSuppliers.Add(comp);
                            db.SaveChanges();

                            trans.Commit();
                            SupplierId = comp.Id;

                            Gujjar.InfoMsg(string.Format("Supplier with name ({0}) added in database successfully", comp.Name));
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
