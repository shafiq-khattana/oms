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
using DevExpress.XtraEditors;
using WinFom.ReadyStuff.Forms;
using WinFom.Financials.Forms;

namespace WinFom.RepairUI.Forms
{
    public partial class RepairMainForm : Form
    {
        public RepairMainForm()
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

        private void btnDayBook_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                DispatchRepairsListForm form = new DispatchRepairsListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnTransactions_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                TransactionForms form = new TransactionForms();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
        {

        }

        private void btnAccountHeadBalance_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                AccountHeadsBalanceForm form = new AccountHeadsBalanceForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAccountDetailsTreeView_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                AccountsDetailsTreeViewForm form = new AccountsDetailsTreeViewForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAccounts_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                HeadAccounts form = new HeadAccounts();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnCapitalAccount_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                CapitalAccountsForm form = new CapitalAccountsForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnCashAccount_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                CashAccountForm form = new CashAccountForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnBanks_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                BankListForm form = new BankListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnFinancialExpense_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                FinancialExpenseForm form = new FinancialExpenseForm();
                form.ShowDialog();

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnGeneralExpense_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                PurchasingListForm form = new PurchasingListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnCreditors_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                CreditorsAccountsForm form = new CreditorsAccountsForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tileItem10_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                DebitorsAccountsForm form = new DebitorsAccountsForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnOpeningBalance_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                OpeningBalanceAccountForm form = new OpeningBalanceAccountForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnLaborPayableAccounts_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                LaborPayableAccountsForm form = new LaborPayableAccountsForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAccruedExpenses_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                AccruedExpensesForm form = new AccruedExpensesForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnEmployeePayments_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                EmployeesAccountsForm form = new EmployeesAccountsForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnLongTermAssets_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                LongTermAssetsItemsForm form = new LongTermAssetsItemsForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnInventoryItems_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                InventoryItemListForm form = new InventoryItemListForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tileItem15_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
