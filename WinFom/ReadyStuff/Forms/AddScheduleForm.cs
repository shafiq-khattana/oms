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
using Model.ReadyStuff.ViewModel;
using Model.Admin.Model;
using WinFom.Common.Model;

namespace WinFom.ReadyStuff.Forms
{
    public partial class AddScheduleForm : Form
    {
        private int _total = 0;
        private int _scheduled = 0;
        private int _remaining = 0;
        private DateTime _lastDate = DateTime.Now;
        public SchVM vm = null;
        private AppSettings appSett = Helper.AppSet;
        public AddScheduleForm(int total, int scheduled, int remaining, DateTime lastDate)
        {
            InitializeComponent();
            _total = total;
            _scheduled = scheduled;
            _remaining = remaining;
            _lastDate = lastDate;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                tbAlreadyScheduled.Text = _scheduled.ToString();
                tbNowScheduling.Text = _remaining.ToString();
                tbRemaingSchedules.Text = _remaining.ToString();
                tbtotalVehicles.Text = _total.ToString();
                dtpScheduleDate.MinDate = _lastDate;

                tbNowScheduling.Focus();
                tbNowScheduling.Select(0, tbNowScheduling.Text.Length);
                dtpScheduleDate.MinDate = appSett.StartDate;
                dtpScheduleDate.MaxDate = appSett.EndDate;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbNowScheduling_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string txt = tbNowScheduling.Text;
                if(string.IsNullOrEmpty(txt))
                {
                    return;
                }
                int nowQty = txt.ToInt();

                if(nowQty > _remaining)
                {
                    tbNowScheduling.Text = _remaining.ToString();
                    nowQty = _remaining;
                    throw new Exception("Now qty is greater than remaining qty, setted to remaining qty");
                }
                //nowQty = tbNowScheduling.Text.ToInt();
                
                
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
                if(string.IsNullOrEmpty(tbNowScheduling.Text))
                {
                    throw new Exception("Please enter now scheduling vehicle qty");
                }

                int nowQty = tbNowScheduling.Text.ToInt();
                if(nowQty == 0)
                {
                    throw new Exception("You can't add zero schedule");
                }
                DialogResult res = Gujjar.ConfirmYesNo(string.Format("Are you confirmed to add {0} schedules.", nowQty));
                if(res == DialogResult.No)
                {
                    return;
                }
                vm = new SchVM
                {
                    Id = Guid.NewGuid().ToString(),
                    ReadyDate = dtpScheduleDate.Value.ToShortDateString(),
                    Vehicles = nowQty
                };
                Close();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
