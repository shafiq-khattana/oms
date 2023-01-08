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

namespace WinFom.Financials.Forms
{
    public partial class DebitorsAccountsForm : Form
    {
        private string btndgvtrans = "btndgvtrans23424";
        private string btndgvReceive = "btndgvreceive1234";        
        private string dgvbtnmindebitor = "dgvbtnmindebitor";        
        private List<DrCrVM> vmList = null;
        private AppSettings AppSett = Helper.AppSet;
        public DebitorsAccountsForm()
        {
            InitializeComponent();
        }
        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadDebitorAccounts()
        {
            try
            {                
                using (Context db = new Context())
                {
                    var emps = db.Customers.Where(a => a.IsActive && a.Id != 1 && !a.IsEmployee).ToList();
                    var wholeSaleBrokers = db.ReadyBrokers.Where(a => a.IsActive && a.Id != 1).ToList();
                    var oilBrokers = db.OilDealBrokers.Where(a => a.IsActive).ToList();
                    var oilDirtBrokers = db.OilDirtBrokers.Where(a => a.IsActive).ToList();

                    if(vmList != null)
                    {
                        vmList.Clear();
                        vmList = null;
                    }
                    vmList = new List<DrCrVM>();

                    
                    foreach (var item in emps)
                    {
                        decimal balance = 0;
                        var obj = db.AccountTransactions.Where(a => a.GeneralAccountId == item.GeneralAccountId).AsParallel()
                            .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                            .OrderByDescending(a => a.Id)
                            .FirstOrDefault();
                        var acct = db.Accounts.Find(item.GeneralAccountId) as GeneralAccount;
                        if(obj != null)
                        {
                            balance = obj.Balance;
                        }
                        DrCrVM vm = new DrCrVM
                        {
                            Balance = balance,
                            Id = item.GeneralAccountId,
                            Title = acct.Title,
                            Type = "Retailer"
                        };
                        vmList.Add(vm);
                    }

                    foreach (var item in wholeSaleBrokers)
                    {
                        decimal balance = 0;
                        var obj = db.AccountTransactions.Where(a => a.GeneralAccountId == item.GeneralAccountId).AsParallel()
                            .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                            .OrderByDescending(a => a.Id)
                            .FirstOrDefault();
                        var acct = db.Accounts.Find(item.GeneralAccountId) as GeneralAccount;
                        if (obj != null)
                        {
                            balance = obj.Balance;
                        }
                        DrCrVM vm = new DrCrVM
                        {
                            Balance = balance,
                            Id = item.GeneralAccountId,
                            Title = acct.Title,
                            Type = "Wholesale Broker"
                        };
                        vmList.Add(vm);
                    }

                    foreach (var item in oilBrokers)
                    {
                        decimal balance = 0;
                        var obj = db.AccountTransactions.Where(a => a.GeneralAccountId == item.GeneralAccountId).AsParallel()
                            .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                            .OrderByDescending(a => a.Id)
                            .FirstOrDefault();
                        var acct = db.Accounts.Find(item.GeneralAccountId) as GeneralAccount;
                        if (obj != null)
                        {
                            balance = obj.Balance;
                        }
                        DrCrVM vm = new DrCrVM
                        {
                            Balance = balance,
                            Id = item.GeneralAccountId,
                            Title = acct.Title,
                            Type = "Crude Oil Broker"
                        };
                        vmList.Add(vm);
                    }

                    foreach (var item in oilDirtBrokers)
                    {
                        decimal balance = 0;
                        var obj = db.AccountTransactions.Where(a => a.GeneralAccountId == item.GeneralAccountId).AsParallel()
                            .ToList().Where(a => a.Date.Date >= AppSett.StartDate.Date && a.Date.Date <= AppSett.EndDate.Date)
                            .OrderByDescending(a => a.Id)
                            .FirstOrDefault();
                        var acct = db.Accounts.Find(item.GeneralAccountId) as GeneralAccount;
                        if (obj != null)
                        {
                            balance = obj.Balance;
                        }
                        DrCrVM vm = new DrCrVM
                        {
                            Balance = balance,
                            Id = item.GeneralAccountId,
                            Title = acct.Title,
                            Type = "Oil Dirt Broker"
                        };
                        vmList.Add(vm);
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
                string receiveBalance = "Receive Amount";
                string ledger = "Ledger";
                //string prevReceive = "Prev Receive";
                //string prevPay = "Prev Pay";

                //Gujjar.AddDatagridviewButton(dgv, btndgvprevreceive, prevReceive, prevReceive, 120);
                //Gujjar.AddDatagridviewButton(dgv, btndgvprevpay, prevPay, prevPay, 80);

                Gujjar.AddDatagridviewButton(dgv, btndgvReceive, receiveBalance, receiveBalance, 120);
                Gujjar.AddDatagridviewButton(dgv, btndgvtrans, ledger,ledger, 80);
                Gujjar.AddDatagridviewButton(dgv, dgvbtnmindebitor, "Remove", "Remove", 80);
                WaitForm wait1 = new WaitForm(LoadDebitorAccounts);
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
            tbTotalAccounts.Text = vmList.Count.ToString();
            tbTotalBalance.Text = vmList.Sum(a => a.Balance).ToString("n2");

            drCrVMBindingSource.List.Clear();
            foreach (var item in vmList)
            {
                drCrVMBindingSource.List.Add(item);
            }
        }        
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                int ci = e.ColumnIndex;

                if(ri == -1 || ri == dgv.NewRowIndex)
                {
                    return;
                }

                if(dgv.Columns[btndgvtrans].Index == ci)
                {
                    string acctId = dgv.Rows[ri].Cells[0].Value.ToString();
                    AccountTransactionForm form = new AccountTransactionForm(acctId);
                    form.ShowDialog();
                }
                if(dgv.Columns[btndgvReceive].Index == ci)
                {
                    if (!Helper.ConfirmAdminPassword())
                        return;

                    string acctId = dgv.Rows[ri].Cells[0].Value.ToString();
                    ReceiveCashForm form = new ReceiveCashForm(acctId);
                    form.ShowDialog();

                    if(form.IsDone)
                    {
                        WaitForm wait1 = new WaitForm(LoadDebitorAccounts);
                        wait1.ShowDialog();

                        UpdateDgv();
                    }
                }
                /*
                if(dgv.Columns[btndgvprevpay].Index == ci)
                {
                    if (!Helper.ConfirmAdminPassword())
                        return;

                    string acctId = dgv.Rows[ri].Cells[0].Value.ToString();
                    DebitorPrevPayForm form = new DebitorPrevPayForm(acctId);
                    form.ShowDialog();

                    if (form.IsDone)
                    {
                        WaitForm wait1 = new WaitForm(LoadDebitorAccounts);
                        wait1.ShowDialog();

                        UpdateDgv(debitorsAccounts);
                    }
                }
                if(dgv.Columns[btndgvprevreceive].Index == ci)
                {
                    if (!Helper.ConfirmAdminPassword())
                        return;

                    string acctId = dgv.Rows[ri].Cells[0].Value.ToString();
                    DebitorPrevReceiveForm form = new DebitorPrevReceiveForm(acctId);
                    form.ShowDialog();

                    if (form.IsDone)
                    {
                        WaitForm wait1 = new WaitForm(LoadDebitorAccounts);
                        wait1.ShowDialog();

                        UpdateDgv(debitorsAccounts);
                    }
                }
                */
                if(dgv.Columns[dgvbtnmindebitor].Index == ci)
                {
                    if (!Helper.ConfirmAdminPassword())
                        return;

                    DialogResult rest = Gujjar.ConfirmYesNo("Are you sured please...??");
                    if (rest == DialogResult.No)
                        return;

                    string id = dgv.Rows[ri].Cells[0].Value.ToString();
                    using (Context db = new Context())
                    {
                        var obj = db.Accounts.Find(id) as GeneralAccount;
                        obj.CrDrType = CrDrType.General;
                        db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        WaitForm wait1 = new WaitForm(LoadDebitorAccounts);
                        wait1.ShowDialog();
                        UpdateDgv();
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string term = tbSearch.Text;
            if(string.IsNullOrEmpty(term))
            {
                UpdateDgv();
            }
            else
            {
                var accts = vmList.Where(a => a.Title.ToLower().Contains(term.ToLower())).ToList();

                drCrVMBindingSource.List.Clear();
                foreach (var item in accts)
                {
                    drCrVMBindingSource.List.Add(item);
                }
                tbTotalAccounts.Text = accts.Count.ToString();
                tbTotalBalance.Text = accts.Sum(a => a.Balance).ToString("n2");
                dgv.Refresh();
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AddDrCrForm form = new AddDrCrForm(CrDrType.Debitor);
                form.ShowDialog();

                if (form.IsDone)
                {
                    WaitForm wait1 = new WaitForm(LoadDebitorAccounts);
                    wait1.ShowDialog();

                    UpdateDgv();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
