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
using Model.AppWeighBridge.ViewModel;
using WinFom.Common.Forms;

namespace WinFom.AppWeighBridge.Forms
{
    public partial class WeighBridgeListForm : Form
    {
        public WeighBridgeListForm()
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
                WaitForm form = new WaitForm(LoadData);
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void LoadData()
        {
            try
            {
                using (Context db = new Context())
                {
                    var wbs = db.WeighBridges.Where(a => a.Name != "N/A").OrderBy(a => a.WeighBrideType).ToList();

                    foreach (var wb in wbs)
                    {
                        decimal loadedQty = 0;
                        decimal recQty = 0;
                        decimal effi = 0;

                        var swbs = db.ScheduleWeighBridges.Where(a => a.WeighBridgeId == wb.Id).ToList();
                        foreach (var swb in swbs)
                        {
                            var schs = db.DealSchedules.Find(swb.DealScheduleId);
                            if (schs.IsArrived)
                            {
                                loadedQty += schs.LoadedSubTradeUnits;
                                recQty += schs.ReceivedSubTradeUnits;
                            }
                        }
                        if (loadedQty > 0)
                        {
                            effi = recQty / loadedQty * 100;
                        }

                        WeighBridgeVM vm = new WeighBridgeVM
                        {
                            Id = wb.Id,
                            Address = wb.Address,
                            Efficiency = string.Format("{0} %", effi.ToString("n2")),
                            Name = wb.Name,
                            Phone = wb.Phone,
                            Schedules = swbs.Count,
                            WeighBrideType = wb.WeighBrideType.ToString()
                        };

                        if (dgv.InvokeRequired)
                        {
                            dgv.Invoke(new Action(() =>
                            {
                                weighBridgeVMBindingSource.List.Add(vm);
                            }));
                        }
                        else
                        {
                            weighBridgeVMBindingSource.List.Add(vm);
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
