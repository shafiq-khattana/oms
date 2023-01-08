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
using Model.Repair.Model;
using System.Data.Entity;
using Model.Repair.ViewModel;
using Model.Financials.Model;

namespace WinFom.RepairUI.Forms
{
    public partial class PurchasingListForm : Form
    {
        private List<PurchaseInvoiceRecord> purchaseRecords = null;
        private string dgvbtncancel = "dgvbtncancel";
        public PurchasingListForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadPurchaseRecords()
        {
            try
            {
                if(purchaseRecords != null)
                {
                    purchaseRecords.Clear();
                    purchaseRecords = null;
                }
                using (Context db = new Context())
                {
                    purchaseRecords = db.PurchaseInvoiceRecords.Include(a => a.Supplier).AsParallel().ToList();
                }
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
                purchaseVMBindingSource.List.Clear();
                foreach (var item in purchaseRecords)
                {
                    PurchaseVM vm = new PurchaseVM
                    {
                        Id = item.Id,
                        BillId = item.BillId,
                        Supplier = item.Supplier.Name,
                        PurchaseDate = item.PurchaseDate.ToShortDateString(),
                        TotalItems = item.TotalItems,
                        TotalPrice = item.TotalPrice,
                        Remarks = item.Remarks
                    };
                    purchaseVMBindingSource.List.Add(vm);
                }

                tbTotalAmount.Text = purchaseRecords.Sum(a => a.TotalPrice).ToString("n2");
                tbTotalEntries.Text = purchaseRecords.Count.ToString();
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
                WaitForm wait = new WaitForm(LoadPurchaseRecords);
                wait.ShowDialog();

                UpdateDgv();

                Gujjar.AddDatagridviewButton(dgv, dgvbtncancel, "Cancel", "Cancel", 70);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddPurchasingInvoiceForm form = new AddPurchasingInvoiceForm();
                form.ShowDialog();
                
                if(form.IsDone)
                {
                    WaitForm wait = new WaitForm(LoadPurchaseRecords);
                    wait.ShowDialog();

                    UpdateDgv();
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

                if(dgv.Columns[dgvbtncancel].Index == ci)
                {
                    if (!Helper.ConfirmAdminPassword())
                        return;

                    DialogResult res = Gujjar.ConfirmYesNo("Please confirm before to proceed on");
                    if (res == DialogResult.No)
                        return;

                    int id = dgv.Rows[ri].Cells[0].Value.ToInt();
                    using (Context db = new Context())
                    {
                        var purObj = db.PurchaseInvoiceRecords.Find(id);
                        DayBook daybook = null;
                        if(purObj.DaybookId != 0)
                            daybook = db.DayBooks.Find(purObj.DaybookId);

                        var purEntries = db.ItemPurchaseEntries.Where(a => a.PurchaseInvoiceRecordId == id).ToList();
                        foreach (var item in purEntries)
                        {
                            var repitem = db.RepItems.Find(item.RepItemId);
                            repitem.SKU -= item.Qty;
                            if(repitem.SKU == 0)
                            {
                                repitem.UnitValue = 0;
                                repitem.TotalValue = 0;
                            }
                            else
                            {
                                repitem.TotalValue -= item.TotalPrice;
                                repitem.UnitValue = repitem.TotalValue / repitem.SKU;
                            }
                            db.Entry(repitem);

                            db.ItemPurchaseEntries.Remove(item);
                        }
                        db.PurchaseInvoiceRecords.Remove(purObj);
                        db.SaveChanges();

                        if(daybook != null)
                            Helper.ReverseTransaction(daybook);

                        Gujjar.InfoMsg("Purchasing entry is cancelled suuccessfully");

                        WaitForm wait = new WaitForm(LoadPurchaseRecords);
                        wait.ShowDialog();
                        UpdateDgv();

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
