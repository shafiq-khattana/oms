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
using Model.Financials.ViewModel;
using Model.Employees.Model;
using Model.Employees.ViewModel;

namespace WinFom.Financials.Forms
{
    public partial class LongTermAssetsItemsForm : Form
    {
        private List<LongTermAssetItem> itemList = null;
        private AppSettings AppSett = Helper.AppSet;
        private string dgvbtnledger = "dgvbtnledger";
        private string dgvbtnpay = "dgvbtnpay";
        public LongTermAssetsItemsForm()
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
                if(itemList != null)
                {
                    itemList.Clear();
                    itemList = null;
                }
               
                using (Context db = new Context())
                {
                    itemList = db.LongTermAssetItems.OrderBy(a => a.DateAdded).ToList();
                    foreach (var item in itemList)
                    {
                        var obj = db.AccountTransactions.Where(a => a.GeneralAccountId == item.GeneralAccountId)
                            .OrderByDescending(a => a.Id).FirstOrDefault();
                        
                        if(obj != null)
                        {
                            item.CurrentPrice = obj.Balance;
                        }
                    }
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
                Gujjar.AddDatagridviewButton(dgv, dgvbtnpay, "Add Dep Exp", "Add Dep Exp", 120);
                Gujjar.AddDatagridviewButton(dgv, dgvbtnledger, "Ledger", "Ledger", 120);
                WaitForm wait1 = new WaitForm(LoadItems);
                wait1.ShowDialog();
                UpdateDgv();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void UpdateDgv()
        {
            try
            {
                longTermAssetItemVMBindingSource.List.Clear();
                foreach (var item in itemList)
                {
                    LongTermAssetItemVM vm = new LongTermAssetItemVM
                    {
                        Id = item.Id,
                        DateAdded = item.DateAdded.ToShortDateString(),
                        CurrentPrice = item.CurrentPrice,
                        Description = item.Description,
                        Item = item.Title,
                        PurchasedPrice = item.InitialPrice
                    };

                    longTermAssetItemVMBindingSource.List.Add(vm);
                }

                tbItemsCount.Text = itemList.Count.ToString();
                totalCurrentPrice.Text = itemList.Sum(a => a.CurrentPrice).ToString("n2");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                int ci = e.ColumnIndex;

                if (ri == -1 || ri == dgv.NewRowIndex)
                    return;
                
                if(dgv.Columns[dgvbtnledger].Index == ci)
                {
                    int empId = dgv.Rows[ri].Cells[0].Value.ToInt();
                    using (Context db = new Context())
                    {
                        string acctId = db.LongTermAssetItems.Find(empId).GeneralAccountId;
                        AccountTransactionForm form = new AccountTransactionForm(acctId);
                        form.ShowDialog();
                    }
                }
                
                if(dgv.Columns[dgvbtnpay].Index == ci)
                {
                    int empId = dgv.Rows[ri].Cells[0].Value.ToInt();
                    using (Context db = new Context())
                    {
                        string acctId = db.LongTermAssetItems.Find(empId).GeneralAccountId;
                        DepExpAddLongTermItemForm form = new DepExpAddLongTermItemForm(acctId, empId);
                        form.ShowDialog();

                        if(form.IsDone)
                        {
                            WaitForm wait1 = new WaitForm(LoadItems);
                            wait1.ShowDialog();
                            UpdateDgv();
                        }
                    }
                }   
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
                AddLongTermAssetItem form = new AddLongTermAssetItem();
                form.ShowDialog();
                if(form.IsDone)
                {
                    WaitForm wait = new WaitForm(LoadItems);
                    wait.ShowDialog();
                    UpdateDgv();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
