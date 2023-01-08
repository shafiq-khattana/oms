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
using System.Data.Entity;
using Model.AppBroker.ViewModel;
using WinFom.Common.Forms;
using Model.AppSelector.ViewModel;

namespace WinFom.Deal.Forms
{
    public partial class SelectorListForm : Form
    {
        private List<Selector> dbSelector = null;
        private decimal totalCashLoss = 0;
        private decimal totalQtyLoaded2 = 0;
        private decimal totalQtyReceived2 = 0;
        public SelectorListForm()
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
                WaitForm form = new WaitForm(LoadSelectors);
                form.ShowDialog();

                tbCompanies.Text = selectorVMBindingSource.List.Count.ToString();
                decimal lossPercent = 0;

                if(totalQtyLoaded2 > 0)
                {
                    lossPercent = totalQtyReceived2 / totalQtyLoaded2 * 100;
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadSelectors()
        {
            try
            {
                using (Context db = new Context())
                {
                    dbSelector = db.Selectors.Include(a => a.Schedules)
                        .OrderBy(a => a.Id).ToList();

                    foreach (var it in dbSelector)
                    {
                        if (it.Name == "N/A")
                            continue;                        
                        SelectorVM vm = new SelectorVM
                        {
                            Id = it.Id,
                            Address = it.Address,
                            Contact = it.Contact,
                            DateAdded = it.DateAdded.ToShortDateString(),
                            IsActive = it.IsActive,
                            CollectsCount = it.Schedules.Count,
                            Extra = it.Extra,                            
                            Name = it.Name,
                            Remarks = it.Remarks
                        };

                        if (dgv.InvokeRequired)
                        {
                            dgv.Invoke(new Action(() =>
                            {
                                selectorVMBindingSource.List.Add(vm);
                            }));
                        }
                        else
                        {
                            selectorVMBindingSource.List.Add(vm);
                        }

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
