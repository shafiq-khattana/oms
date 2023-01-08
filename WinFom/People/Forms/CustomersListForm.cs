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
using Model.Admin.ViewModel;
using WinFom.Admin.Forms;
using Khattana.Secrecy;
using WinFom.People.Forms;
using Model.Retail.Model;
using Model.Retail.ViewModel;

namespace WinFom.RepairUI.Forms
{
    public partial class CustomersListForm : Form
    {
        //private string dgvbtnupdate = "dgvbtnupdate121";
       
        private List<Customer> customers = null;
        private AppSettings appSett = Helper.AppSet;
        private List<CustomerVM2> customerVMList = null;
        public CustomersListForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadCustomers()
        {
            try
            {
                if(customers != null)
                {
                    customers.Clear();
                    customers = null;
                }
                if(customerVMList != null)
                {
                    customerVMList.Clear();
                    customerVMList = null;
                }
                customerVMList = new List<CustomerVM2>();

                using (Context db = new Context())
                {
                    customers = db.Customers.AsParallel().ToList();
                    foreach (var item in customers)
                    {
                        int inCount = db.SaleOrders.Count(a => a.CustomerId == item.Id && a.OrderDate >= appSett.StartDate && a.OrderDate <= appSett.EndDate);
                        CustomerVM2 vm = new CustomerVM2
                        {
                            Address = item.Address,
                            Contact = item.Contact,
                            Id = item.Id,
                            InvoiceCount = inCount,
                            Name = item.Name,
                            Remarks = item.Remarks
                        };
                        customerVMList.Add(vm);
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
                WaitForm wait = new WaitForm(LoadCustomers);
                wait.ShowDialog();

                foreach (var item in customerVMList)
                {
                    customerVM2BindingSource.List.Add(item);
                }
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
                {
                    return;
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
                
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
