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
    public partial class SearchAccountForm : Form
    {
        private List<AccountSearchVM> accounts = null;
        public string AccountId { get; set; }
        public SearchAccountForm(List<AccountSearchVM> accts)
        {
            InitializeComponent();
            accounts = accts;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                AccountId = null;
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
                //accountSearchVMBindingSource.List.Clear();
                accountSearchVMBindingSource.DataSource = null;
            }
            else
            {
                accountSearchVMBindingSource.DataSource = accounts.Where(a => a.Title.ToLower().Contains(term.ToLower())).ToList();
                dgv.Refresh();
            }
            
            dgv.ClearSelection();
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                dgv.Select();
                if(dgv.Rows.Count > 0)
                {
                    dgv.Rows[0].Selected = true;
                }
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int ri = e.RowIndex;

            if (ri == -1 || ri == dgv.NewRowIndex)
                return;

            AccountId = dgv.Rows[ri].Cells[0].Value.ToString();
            Close();
        }

        private void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            if(dgv.Rows.Count > 0 && e.KeyCode == Keys.Enter)
            {
                AccountId = dgv.CurrentRow.Cells[0].Value.ToString();
                Close();
            }
        }
    }

    
}
