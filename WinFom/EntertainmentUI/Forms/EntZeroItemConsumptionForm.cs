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
using Model.Entertainment.Model;

namespace WinFom.EntertainmentUI.Forms
{
    public partial class EntZeroItemConsumptionForm : Form
    {
        private List<EntItem> items = null;
        private AppUser appUser = SingleTon.LoginForm.appUser;
        private AppSettings AppSett = Helper.AppSet;
        public bool IsDone { get; set; }
        public EntZeroItemConsumptionForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadItems()
        {
            try
            {
                using (Context db = new Context())
                {
                    items = db.EntItems.ToList();
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
                Gujjar.TB4(pMain);
                Gujjar.NumbersOnly(tbBillAmount);
                tbRemarks.Text = "N/A";
                Gujjar.TBOptional(tbRemarks);

                WaitForm wait = new WaitForm(LoadItems);
                wait.ShowDialog();

                decimal qty = items.Sum(a => a.QtyConsumed);
                if(qty == 0)
                {
                    btnAdd.Enabled = false;
                }

                tbQtyConsumed.Text = qty.ToString("n1");
                dtp.MinDate = AppSett.StartDate;
                dtp.MaxDate = AppSett.EndDate;
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
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }

                if(!Helper.ConfirmAdminPassword())
                {
                    return;
                }
                DialogResult res = Gujjar.ConfirmYesNo("Please confirm");
                if (res == DialogResult.No)
                    return;

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        EntZeroItemConsumption record = new EntZeroItemConsumption
                        {
                            Amount = tbBillAmount.Text.ToDecimal(),
                            Dated = dtp.Value,
                            Id = 0,
                            Operator = appUser.Id,
                            QtyConsumed = tbQtyConsumed.Text.ToDecimal(),
                            Remarks = tbRemarks.Text
                        };

                        db.EntZeroItemConsumptions.Add(record);
                        db.SaveChanges();

                        var items = db.EntItems.ToList();
                        foreach (var item in items)
                        {
                            if(item.QtyConsumed > 0)
                            {
                                item.QtyConsumed = 0;
                                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            }
                        }
                        db.SaveChanges();
                        trans.Commit();

                        IsDone = true;
                        Gujjar.InfoMsg("Item consumption is set to zero");
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
