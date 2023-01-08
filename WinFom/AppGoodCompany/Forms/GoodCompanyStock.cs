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
    public partial class GoodCompanyStock : Form
    {
        private int _goodCompanyId = 0;
        private GoodCompany goodCompany = null;
        private List<PackingStock> stock = new List<PackingStock>();
        public GoodCompanyStock(int goodCompanyId)
        {
            InitializeComponent();
            _goodCompanyId = goodCompanyId;
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
                    goodCompany = db.GoodCompanies.Find(_goodCompanyId);
                    stock = db.PackingStocks.Where(a => a.GoodCompanyId == _goodCompanyId).ToList();
                    foreach (var item in stock)
                    {
                        item.DealPacking = db.DealPackings.Find(item.DealPackingId);
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

                label1.Text = goodCompany.Name;

                foreach (var item in stock)
                {
                    GoodCompanyStockVM vm = new GoodCompanyStockVM
                    {
                        Packing = item.DealPacking.Name,
                        Qty = item.Balance.ToString("n1"),
                        Value = (item.Balance * item.DealPacking.UnitPrice)
                    };

                    goodCompanyStockVMBindingSource.List.Add(vm);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
