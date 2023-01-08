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
using Model.Financials.Model;

namespace WinFom.ReadyStuff.Forms
{
    public partial class AddReadyDealItemForm : Form
    {
        public int ItemId = 0;
        public AddReadyDealItemForm()
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
                string itemName = tbDealItem.Text;
                if (string.IsNullOrEmpty(itemName))
                {
                    tbDealItem.BackColor = Color.Pink;
                    throw new Exception("Please enter item name");
                }

                using (Context db = new Context())
                {
                    var obj = db.ReadyItems.ToList().FirstOrDefault(a => a.Title.ToLower().Equals(itemName.ToLower()));
                    if (obj != null)
                    {
                        throw new Exception("Item already exists in database.");
                    }
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            string acctStr = string.Format("{0} (whole sale) account", itemName);
                            GeneralAccount itemAccount = new GeneralAccount
                            {
                                Id = Guid.NewGuid().ToString(),
                                AccountNature = AccountNature.Credit,
                                AccountNo = "N/A",
                                AccountTransactions = null,
                                ExplicitilyCreated = false,
                                Title = acctStr,
                                Description = acctStr,
                                SubHeadAccountId = Properties.Resources.OilCakeSalesSubHead
                            };

                            db.Accounts.Add(itemAccount);
                            db.SaveChanges();

                            obj = new ReadyItem
                            {
                                Title = itemName,
                                StockQty = 0,
                                GeneralAccountId = itemAccount.Id
                            };

                            obj = db.ReadyItems.Add(obj);
                            db.SaveChanges();
                            trans.Commit();

                            ItemId = obj.Id;
                            Gujjar.InfoMsg(string.Format("Item with Id ({0}) added in database.", obj.Id));
                            Close();
                        }
                        catch (Exception exp2)
                        {

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
