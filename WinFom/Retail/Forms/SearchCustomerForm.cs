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
using Model.Retail.Model;
using Model.Retail.ViewModel;

namespace WinFom.Retail.Forms
{
    public partial class SearchCustomerForm : Form
    {
        private List<Customer> customers = null;
        List<CustomerVM> customerList = null;
        public int CustomerId = 0;
        public SearchCustomerForm(List<Customer> custs)
        {
            InitializeComponent();
            customers = custs;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                customerList = new List<CustomerVM>();
                Gujjar.DGVDesign(dgv);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbCustomerName_TextChanged(object sender, EventArgs e)
        {
            string txt = tbCustomerName.Text;
            if(string.IsNullOrEmpty(txt))
            {
                customerList.Clear();
            }
            else
            {
                txt = txt.ToLower();
                customerList = customers.Where(a => a.Name.ToLower().Contains(txt))
                .Select(a => new CustomerVM { Address = a.Address, CellNo = a.Contact, Id = a.Id, Name = a.Name })
                .OrderBy(a => Name).ToList();
               
            }
            UpdateDgv();
            dgv.ClearSelection();
        }

        private void UpdateDgv()
        {
            customerVMBindingSource.List.Clear();
            foreach (var item in customerList)
            {
                customerVMBindingSource.List.Add(item);
            }
        }

        private void tbCustomerCellNo_TextChanged(object sender, EventArgs e)
        {
            string txt = tbCustomerCellNo.Text;
            if (string.IsNullOrEmpty(txt))
            {
                customerList.Clear();
            }
            else
            {
                customerList = customers.Where(a => a.Contact.Contains(txt))
               .Select(a => new CustomerVM { Address = a.Address, CellNo = a.Contact, Id = a.Id, Name = a.Name })
               .OrderBy(a => Name).ToList();
                
            }
            UpdateDgv();
            dgv.ClearSelection();
        }

        private void tbCustomerAddress_TextChanged(object sender, EventArgs e)
        {
            string txt = tbCustomerAddress.Text;
            if (string.IsNullOrEmpty(txt))
            {
                customerList.Clear();
            }
            else
            {
                txt = txt.ToLower();
                customerList = customers.Where(a => a.Address.ToLower().Contains(txt))
                .Select(a => new CustomerVM { Address = a.Address, CellNo = a.Contact, Id = a.Id, Name = a.Name })
                .OrderBy(a => Name).ToList();                
            }
            UpdateDgv();
            dgv.ClearSelection();
        }

        private void tbCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgv.Focus();
                dgv.TabIndex = 0;
                if (dgv.Rows.Count > 0)
                {
                    dgv.Rows[0].Selected = true;
                }
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 || dgv.NewRowIndex == e.RowIndex)
                {
                    return;
                }
                CustomerId = dgv.Rows[e.RowIndex].Cells[0].Value.ToInt();
                Close();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgv.Rows.Count > 0)
                {
                    CustomerId = dgv.SelectedRows[0].Cells[0].Value.ToInt();
                }
                Close();
            }
        }

        private void dgv_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void SearchCustomerForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
        }

        private void SearchCustomerForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                CustomerId = 0;
                Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
