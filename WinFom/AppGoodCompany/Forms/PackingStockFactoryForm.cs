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
using WinFom.Common.Forms;
using Model.AppGoodCompany.ViewModel;

namespace WinFom.AppGoodCompany.Forms
{
    public partial class PackingStockFactoryForm : Form
    {
        public PackingStockFactoryForm()
        {
            InitializeComponent();
            
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadData()
        {
            try
            {
                using (Context db = new Context())
                {
                    var stock = db.FactoryPackingStocks.ToList();
                    foreach (var item in stock)
                    {
                        decimal issQty = 0;
                        var objs = db.PackingIssueReceiveRecords
                            .Where(a => a.RecordType == RecordType.Issue && a.DealPackingId == item.DealPackingId).ToList();
                        if(objs.Count > 0)
                        {
                            issQty = objs.Sum(a => a.Qty);
                        }
                        item.DealPacking = db.DealPackings.Find(item.DealPackingId);

                        PackingStockFacotoryVM vm = new PackingStockFacotoryVM
                        {
                            Id = item.Id,
                            Packing = item.DealPacking.Name,
                            PackingId = item.DealPackingId,
                            StockQty = item.Quantity,
                            IssueQty = issQty
                        };

                        if(dgv.InvokeRequired)
                        {
                            dgv.Invoke(new Action(() => {

                                packingStockFacotoryVMBindingSource.List.Add(vm);
                            }));
                        }
                        else
                        {
                            packingStockFacotoryVMBindingSource.List.Add(vm);
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
                WaitForm form = new WaitForm(LoadData);
                form.ShowDialog();

                
                
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
                AddFactoryPackingStock form = new AddFactoryPackingStock();
                form.ShowDialog();

                if(form.IsDone)
                {
                    packingStockFacotoryVMBindingSource.List.Clear();
                    WaitForm wait = new WaitForm(LoadData);
                    wait.ShowDialog();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
