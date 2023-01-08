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
    public partial class PackingStockForm : Form
    {
        private List<PackingStock> stock = new List<PackingStock>();
        private List<PackingStockVM> vmList = new List<PackingStockVM>();
        private List<GoodCompanyStockVM> packingStockVm = new List<GoodCompanyStockVM>();
        public PackingStockForm()
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
                    stock = db.PackingStocks.ToList();
                    foreach (var item in stock)
                    {
                        var pack = db.DealPackings.Find(item.DealPackingId);
                        var comp = db.GoodCompanies.Find(item.GoodCompanyId);

                        PackingStockVM vm = new PackingStockVM
                        {
                            Company = comp.Name,
                            Packing = pack.Name,
                            Qty = item.Balance.ToString("n1"),
                            Value
                            = item.Balance * pack.UnitPrice
                        };

                        vmList.Add(vm);

                        var obj = packingStockVm.FirstOrDefault(a => a.Packing.Equals(pack.Name));
                        if(obj != null)
                        {
                            decimal qty2 = obj.Qty.ToDecimal();
                            obj.Qty = (qty2 + item.Balance).ToString("n1");
                            obj.Value = qty2 * pack.UnitPrice;
                        }
                        else
                        {
                            GoodCompanyStockVM vm2 = new GoodCompanyStockVM
                            {
                                Packing = pack.Name,
                                Value = pack.UnitPrice * item.Balance,
                                Qty = item.Balance.ToString("n1")
                            };
                            packingStockVm.Add(vm2);
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

                
                foreach (var item in vmList.OrderBy(a => a.Company))
                {
                    packingStockVMBindingSource.List.Add(item);
                }
                foreach (var item in packingStockVm.OrderBy(a => a.Packing))
                {
                    goodCompanyStockVMBindingSource.List.Add(item);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
