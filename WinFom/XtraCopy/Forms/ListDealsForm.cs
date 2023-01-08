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

using WinFom.Common.Forms;

using System.Data.Entity;

namespace WinFom.XtraCopy.Forms
{
    public partial class ListDealsForm : Form
    {
        public int ItemId = 0;
        string dgveditbtn = "dgveditbtn";
        string dgvaddinstallment = "dgvaddinstallment";
        string dgvviewinstallments = "dgvviewinstallments";
        string dgvDetails = "dgvdetailsbtn";

        
        public ListDealsForm()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddDealItemForm_Load(object sender, EventArgs e)
        {
            Gujjar.TB4(panel2);
        }
        private void AddDgvButtons()
        {
            Gujjar.AddDatagridviewButton(dgv, dgveditbtn, "Edit", "Edit", 80);
            Gujjar.AddDatagridviewButton(dgv, dgvaddinstallment, "Add Installment", "Add Installment", 130);
            Gujjar.AddDatagridviewButton(dgv, dgvviewinstallments, "View Installmetns", "View Installments", 130);
            Gujjar.AddDatagridviewButton(dgv, dgvDetails, "Details", "Details", 80);
        }
        private void ListDealsForm_Load(object sender, EventArgs e)
        {
            try
            {
                Gujjar.DGVDesign(dgv);
                AddDgvButtons();
                WaitForm wait2 = new WaitForm(LoadDealVMFromDB, "");
                wait2.ShowDialog();

                BindDealVMlist(false);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadDealVMFromDB()
        {
            try
            {
                
                //using (Context db = new Context())
                //{
                //    var deals = db.Deals2.Include(a => a.Broker)
                //        .Include(a => a.Company)
                //        .Include(a => a.DealItem)
                //        .Include(a => a.Packing)
                //        .Include(a => a.Selector)
                //        .ToList();
                //    foreach (var d in deals)
                //    {
                //        DealVM2 vm = new DealVM2
                //        {
                //            Id = d.Id,
                //            Selector = d.Selector.Name,
                //            Packing = d.Packing.Name,
                //            DealItem = d.DealItem.Name,
                //            AddedBy = d.AddedBy,
                //            Broker = d.Broker.Name,
                //            BrokerAmount = d.BrokerAmount,
                //            BrokerAmountPercentage = d.BrokerAmountPercentage,
                //            FareAmount = d.FareAmount,
                //            Company = d.Company.Name,
                //            DealDate = Gujjar.LocalDate(d.DealDate),
                //            DealPrice = d.DealPrice,
                //            DealReadyDate = Gujjar.LocalDate(d.DealReadyDate),
                //            Qty = d.PackingQty,
                //            Tax = d.Tax,
                //            TotalPrice = d.TotalPrice,
                //            TotalUnitPrice = d.TotalUnitPrice,
                //            UnitPrice = d.UnitPrice,
                //            UpdatedBy = d.UpdatedBy,
                //            IsCompleted = d.IsCompleted
                //        };

                //        dealVMList.Add(vm);
                //    }
               //}
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindDealVMlist(bool isCompleted)
        {
            
        }

        private void btnInCompleted_Click(object sender, EventArgs e)
        {
            WaitForm wait2 = new WaitForm(LoadDealVMFromDB, "");
            wait2.ShowDialog();
            BindDealVMlist(false);
        }

        private void btnCompleted_Click(object sender, EventArgs e)
        {
            WaitForm wait2 = new WaitForm(LoadDealVMFromDB, "");
            wait2.ShowDialog();
            BindDealVMlist(true);
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                int ci = e.ColumnIndex;

                if (ri == dgv.NewRowIndex || ri == -1)
                    return;
                int dealId = dgv.Rows[ri].Cells[0].Value.ToString().ToInt();
                if (dgv.Columns[dgvaddinstallment].Index == ci)
                {
                    AddDealInstallmentForm form = new AddDealInstallmentForm(dealId);
                    form.ShowDialog();
                }
                if(dgv.Columns[dgvviewinstallments].Index == ci)
                {
                    ListDealInstallments form = new ListDealInstallments(dealId);
                    form.ShowDialog();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
