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
using Model.Admin.Model;
using WinFom.Common.Model;
using WinFom.Common.Forms;
using Model.RetailBardanaManaged.Model;
using WinFom.RetailBardanaManagedUI.Forms;

namespace WinFom.Financials.Forms
{
    public partial class AURetailBardanaItem : Form
    {
        private RetailBardanaItem bardanaItem = null;
        public AURetailBardanaItem()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadBardanaItem()
        {
            try
            {
                using (Context db = new Context())
                {
                    bardanaItem = db.RetailBardanaItems.FirstOrDefault();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait = new WaitForm(LoadBardanaItem);
                wait.ShowDialog();

                if(bardanaItem == null)
                {
                    btnAddEntry.Enabled = false;
                }
                else
                {
                    btnAddEntry.Enabled = true;
                }
                if(bardanaItem != null)
                {
                    btnAdd.Text = "Update Me!";
                    tbSKU.Text = bardanaItem.SKU.ToString("n1");
                    tbTitle.Text = bardanaItem.Title;
                    tbUnitPriceBardana.Text = bardanaItem.UnitPrice.ToString("n1");
                    tbUnitPriceLabor.Text = bardanaItem.UnitLaborPriceRetail.ToString("n1");
                }
                else
                {
                    btnAdd.Text = "Add Me!";
                    tbSKU.Text = "0.0";
                    tbUnitPriceBardana.Text = "0.0";
                }

                Gujjar.NumbersOnly(tbUnitPriceLabor);
                Gujjar.NumbersOnly(tbUnitPriceBardana);
                Gujjar.NumbersOnly(tbSKU);
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
                DialogResult res = Gujjar.ConfirmYesNo("Are you sure, you want to add/update bardana item");
                if (res == DialogResult.No)
                    return;

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            if(!Gujjar.IsValidForm(pMain))
                            {
                                throw new Exception("Please fill all text fields");
                            }

                            if(!Helper.ConfirmUserPassword())
                            {
                                return;
                            }

                            string title = string.Format("{0} (retail bardana) account", tbTitle.Text);

                            if (bardanaItem == null)
                            {
                                
                                GeneralAccount account = new GeneralAccount
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    AccountNature = AccountNature.Debit,
                                    AccountNo = "N/A",
                                    Description = title,
                                    Title = title,
                                    SubHeadAccountId = Properties.Resources.StockPurchases,
                                    Address = "N/A",
                                    Balance = 0,
                                    BankName = "N/A",
                                    ExplicitilyCreated = false,
                                    AccountTransactions = null
                                };

                                db.Accounts.Add(account);
                                db.SaveChanges();

                                bardanaItem = new RetailBardanaItem
                                {
                                    Id = 0,
                                    Account = null,
                                    Entries = null,
                                    GeneralAccountId = account.Id,
                                    SKU = tbSKU.Text.ToDecimal(),
                                    Title = tbTitle.Text,
                                    UnitLaborPriceRetail = tbUnitPriceLabor.Text.ToDecimal(),
                                    UnitLaborPriceWholeSale = tbUnitPriceLabor.Text.ToDecimal(),
                                    UnitPrice = tbUnitPriceBardana.Text.ToDecimal()
                                };
                                db.RetailBardanaItems.Add(bardanaItem);
                                db.SaveChanges();

                                trans.Commit();

                                Gujjar.InfoMsg("Bardana item has been added in database successfully");
                                btnAddEntry.Enabled = true;
                            }
                            else
                            {
                                
                                GeneralAccount account = db.Accounts.Find(bardanaItem.GeneralAccountId) as GeneralAccount;
                                account.Title = title;
                                db.Entry(account).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();

                                var dbItem = db.RetailBardanaItems.Find(bardanaItem.Id);
                                dbItem.Title = tbTitle.Text;
                                dbItem.UnitLaborPriceRetail = tbUnitPriceLabor.Text.ToDecimal();
                                dbItem.UnitLaborPriceWholeSale = tbUnitPriceLabor.Text.ToDecimal();
                                dbItem.UnitPrice = tbUnitPriceBardana.Text.ToDecimal();
                                dbItem.GeneralAccountId = account.Id;
                                dbItem.SKU = bardanaItem.SKU;

                                db.Entry(dbItem).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();

                                trans.Commit();

                                Gujjar.InfoMsg("Bardana item is updated successfully");
                                
                            }
                        }
                        catch (Exception ep2)
                        {
                            trans.Rollback();
                            throw ep2;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbUnitPriceBardana_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal unitPrice = 0;
                string unitStr = tbUnitPriceBardana.Text;
                if(!string.IsNullOrEmpty(unitStr))
                {
                    unitPrice = unitStr.ToDecimal();
                }
                decimal sku = 0;
                string skustr = tbSKU.Text;
                if(!string.IsNullOrEmpty(skustr))
                {
                    sku = skustr.ToDecimal();
                }

                decimal total = sku * unitPrice;
                tbTotalPrice.Text = total.ToString("n2");
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

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            try
            {
                RetailBardanaItemEntriesForm form = new RetailBardanaItemEntriesForm(bardanaItem);
                form.ShowDialog();

                if(form.IsDone)
                {
                    using (Context db = new Context())
                    {
                        WaitForm wait = new WaitForm(LoadBardanaItem);
                        wait.ShowDialog();

                        if (bardanaItem == null)
                        {
                            btnAddEntry.Enabled = false;
                        }
                        else
                        {
                            btnAddEntry.Enabled = true;
                        }
                        if (bardanaItem != null)
                        {
                            btnAdd.Text = "Update Me!";
                            tbSKU.Text = bardanaItem.SKU.ToString("n1");
                            tbTitle.Text = bardanaItem.Title;
                            tbUnitPriceBardana.Text = bardanaItem.UnitPrice.ToString("n1");
                            tbUnitPriceLabor.Text = bardanaItem.UnitLaborPriceRetail.ToString("n1");
                        }
                        else
                        {
                            btnAdd.Text = "Add Me!";
                            tbSKU.Text = "0.0";
                            tbUnitPriceBardana.Text = "0.0";
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
