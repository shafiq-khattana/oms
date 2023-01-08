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
    public partial class ListDealInstallments : Form
    {
        public int dealId = 0;
        string dgvdeletebtn = "dgvdeletebtn";
        
        //Deal2 deal = null;
       
        public ListDealInstallments(int _dealId)
        {
            InitializeComponent();
            dealId = _dealId;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }
        

        private void AddDealItemForm_Load(object sender, EventArgs e)
        {
            Gujjar.TB4(panel2);
        }
        private void AddDgvButtons()
        {
            Gujjar.AddDatagridviewButton(dgv, dgvdeletebtn, "Edit", "Edit", 80);
            
        }
        
        

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                int ci = e.ColumnIndex;

                if (ri == dgv.NewRowIndex || ri == -1)
                    return;
                
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void LoadDealObj()
        {
            try
            {
                //using (Context db = new Context())
                //{
                //    deal = db.Deals2.Include(a => a.Broker)
                //        .Include(a => a.Selector)
                //        .Include(a => a.Company)
                //        .Include(a => a.DealItem)
                //        .Include(a => a.Packing)
                //        .Include(a => a.DealInstallments)
                //        .FirstOrDefault(a => a.Id == dealId);

                //    if (deal == null)
                //    {
                //        throw new Exception("Deal not foud with no " + dealId.ToString());
                //    }

                    
                //}
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void ListDealInstallments_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait2 = new WaitForm(LoadDealObj, "");
                wait2.ShowDialog();
                WaitForm wait = new WaitForm(LoadInstallments, "");
                wait.ShowDialog();
                BindControls();
                BindInstallments();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void BindControls()
        {
            try
            {
                //tbDealCount.Text = deal.Id.ToString();
                //tbCompany.Text = deal.Company.Name;
                //tbBroker.Text = deal.Broker.Name;
                //tbDealDate.Text = Gujjar.LocalDate(deal.DealDate);
                //tbSelector.Text = deal.Selector.Name;
                //tbInstallmentsCount.Text = deal.DealInstallments.Count.ToString();
                //tbDealQty.Text = deal.PackingQty.ToString();
                //decimal receivedQty = deal.DealInstallments.Sum(a => a.QtyReceived);
                //tbReceivedQty.Text = receivedQty.ToString();
                //tbRemainingQty.Text = (deal.PackingQty - receivedQty).ToString();
                //tbPacking.Text = deal.Packing.Name;
                //tbQtyLoss.Text = deal.DealInstallments.Sum(a => a.QtyDifference).ToString();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void BindInstallments()
        {
            //dgv.DataSource = dealInstallmentList;
        }

        private void LoadInstallments()
        {
            try
            {
               

                
                //using (Context db = new Context())
                //{
                //    var installments = db.DealInstallment2.Where(a => a.DealId == dealId).Include(a => a.Driver)
                //        .ToList();

                //    foreach (var d in installments)
                //    {
                //        DealInstallmentVM2 vm = new DealInstallmentVM2()
                //        {
                //            Driver = d.Driver.Name,
                //            Id = d.Id,
                //            LoadedQty = d.LoadedQty,
                //            QtyReceived = d.QtyReceived,
                //            ReceiveDateTime = Gujjar.LocalDate(d.ReceiveDateTime),
                //            ReceivedBy = d.ReceivedBy,
                //            VehicleNo = d.VehicleNo,
                //            VehicleType = d.VehicleType
                //        };

                //        dealInstallmentList.Add(vm);
                //    }
                //}
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
