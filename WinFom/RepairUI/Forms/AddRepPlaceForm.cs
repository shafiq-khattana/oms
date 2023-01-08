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
using Model.Retail.Model;
using Model.Repair.Model;
using Model.Financials.Model;

namespace WinFom.RepairUI.Forms
{
    public partial class AddRepPlaceForm : Form
    {
        public int PlaceId = 0;
        public AddRepPlaceForm()
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
                tbPhone.Text = "N/A";
                tbAddress.Text = "N/A";
                Gujjar.TB4(pMain);
                Gujjar.TBOptional(tbPhone);
                Gujjar.TBOptional(tbAddress);
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
                    throw new Exception("Please fill all text fields");
                }
                DialogResult res = Gujjar.ConfirmYesNo("Please confirm... !!\n");
                if (res == DialogResult.No)
                    return;

                string itemName = tbName.Text;
                string phoneNo = tbPhone.Text;
                string address = tbAddress.Text;
                using (Context db = new Context())
                {
                    var dbObj = db.RepPlaces.ToList().FirstOrDefault(a => a.Name.ToLower().Equals(itemName.ToLower()));
                    if(dbObj != null)
                    {
                        throw new Exception("Repairing place already added in database");
                    }
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            string acctTitle = string.Format("{0} payable account", itemName);
                            GeneralAccount account = new GeneralAccount
                            {
                                Id = Guid.NewGuid().ToString(),
                                AccountNature = AccountNature.Credit,
                                Title = acctTitle,
                                AccountNo = "N/A",
                                Description = acctTitle,
                                Address = "N/A",
                                SubHeadAccountId = Properties.Resources.RepairExpensesPayableSubHead,
                                ExplicitilyCreated = true,
                                SubHeadAccount = null,
                                CrDrType = CrDrType.Creditor
                            };
                            db.Accounts.Add(account);
                            db.SaveChanges();

                            RepPlace place = new RepPlace
                            {
                                Id = 0,
                                Address = address,
                                GeneralAccount = null,
                                GeneralAccountId = account.Id,
                                Name = itemName,
                                PhoneNo = phoneNo,
                                //RepEntries = null
                            };

                            place = db.RepPlaces.Add(place);
                            db.SaveChanges();

                            trans.Commit();

                            PlaceId = place.Id;
                            Gujjar.InfoMsg("Repairing place is added in database successfully");
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
