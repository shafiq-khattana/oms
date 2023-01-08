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
using Model.ReadyStuff.Model;
using WinFom.Common.Forms;
using Model.Admin.Model;
using WinFom.Common.Model;
using Model.Financials.Model;
using Model.Repair.Model;

namespace WinFom.RepairUI.Forms
{
    public partial class AddItemSupplierForm : Form
    {
        public int SupplierId { get; set; }
        public AddItemSupplierForm()
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
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please enter all related data");
                }
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            string supplierName = tbName.Text;
                            string phone = tbPhone.Text;

                            var dobj = db.ItemSuppliers.ToList().FirstOrDefault(a => a.Name.ToLower().Equals(supplierName.ToLower()) && a.Phone.Equals(phone));
                            if(dobj != null)
                            {
                                throw new Exception("Supplier already added in database");
                            }

                            string actTitle = string.Format("{0} (item supplier) account", supplierName);
                            GeneralAccount acct = new GeneralAccount
                            {
                                Id = Guid.NewGuid().ToString(),
                                AccountNature = AccountNature.Credit,
                                AccountNo = "N/A",
                                AccountTransactions = null,
                                Address = "N/A",
                                Title = actTitle,
                                Description = actTitle,
                                ExplicitilyCreated = false,
                                SubHeadAccountId = Properties.Resources.Creditors,
                                CrDrType = CrDrType.Creditor
                            };
                            db.Accounts.Add(acct);
                            db.SaveChanges();

                            ItemSupplier supp = new ItemSupplier
                            {
                                Id = 0,
                                Address = phone,
                                Name = supplierName,
                                Phone = phone,
                                PurchaseInvoiceRecords = null,
                                Account = null,
                                GeneralAccountId = acct.Id
                            };
                            supp = db.ItemSuppliers.Add(supp);
                            db.SaveChanges();
                            trans.Commit();
                            SupplierId = supp.Id;
                            Gujjar.InfoMsg("Supplier is added in database successfully");
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
