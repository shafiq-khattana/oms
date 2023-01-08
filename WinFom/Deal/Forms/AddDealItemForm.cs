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
    public partial class AddDealItemForm : Form
    {
        public int ItemId = 0;
        public AddDealItemForm()
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string dealItem = tbDealItem.Text;
                if (string.IsNullOrEmpty(dealItem))
                {
                    tbDealItem.BackColor = Color.Pink;
                    tbDealItem.Focus();
                    throw new Exception("Please enter item name");
                }

                DealItem item = new DealItem
                {
                    Name = dealItem
                };
                using (Context db = new Context())
                {
                    var obj = db.DealItems.ToList().FirstOrDefault(a => a.Equals(item));
                    if (obj != null)
                    {
                        throw new Exception("Deal item already exists in database");
                    }
                    using (var trans = db.Database.BeginTransaction())
                    {
                        GeneralAccount genAcct = new GeneralAccount
                        {
                            Id = Guid.NewGuid().ToString(),
                            AccountNature = AccountNature.Debit,
                            SubHeadAccountId = Properties.Resources.ItemPurchasedExpenseSubHead,
                            Title = string.Format("{0} expense account", item.Name),
                            AccountNo = "N/A",
                            AccountTransactions = null,
                            Address = "N/A",
                            Brokers = null,
                            Companies = null,
                            Customers = null,
                            SubHeadAccount = null,
                            DealItems = null,
                            Description = "Item purchased expense Account",
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

                        };

                        db.Accounts.Add(genAcct);
                        db.SaveChanges();
                        item.GeneralAccountId = genAcct.Id;
                        item = db.DealItems.Add(item);
                        db.SaveChanges();
                        trans.Commit();
                        Gujjar.InfoMsg(string.Format("Item {0} added in database successfully", item.Name));
                        ItemId = item.Id;
                        Close();
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
