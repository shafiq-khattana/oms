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
using Model.Admin.ViewModel;
using WinFom.Common.Forms;

namespace WinFom.Deal.Forms
{
    public partial class ScheduleAlarmsList : Form
    {
        private string dgvbtnalarmdescription = "dgvbtnalarmdescriptionasdf";
        private List<AlarmVm> enabledAlarmList = new List<AlarmVm>();
        private List<ScheduleAlarm> alarmList = new List<ScheduleAlarm>();
        public ScheduleAlarmsList()
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
                Gujjar.AddDatagridviewButton(dgv, dgvbtnalarmdescription, "Alarm", "Alarm", 80);
                WaitForm wait = new WaitForm(LoadEnabledAlarms);
                wait.ShowDialog();
                BindEnabledAlarms();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void LoadEnabledAlarms()
        {
            try
            {
                using (Context db = new Context())
                {
                    alarmList = db.ScheduleAlarms.Where(a => a.Status).OrderBy(a => a.ActiveDate).ToList();
                    foreach (var item in alarmList)
                    {
                        AlarmVm vm = new AlarmVm
                        {
                            Id = item.Id,
                            ActiveDate = item.ActiveDate.ToShortDateString(),
                            EndTime = item.EndTime.ToShortDateString(),
                            GenerateTime = item.GenerateTime,
                            Status = item.Status,
                            Title = item.Title
                        };
                        enabledAlarmList.Add(vm);  
                    }
                }
            }
            catch (Exception exp)
            {

                Gujjar.ErrMsg(exp);
            }
        }
        private void DgvRefresh()
        {
            try
            {
                DateTime nowDate = DateTime.Now.Date;
                if (dgv.Rows.Count > 0)
                {
                    foreach (DataGridViewRow item in dgv.Rows)
                    {
                        int id = item.Cells[0].Value.ToInt();
                        var chVm = alarmVmBindingSource.List.OfType<AlarmVm>().FirstOrDefault(a => a.Id == id);
                        DateTime endDate = Convert.ToDateTime(chVm.EndTime).Date;
                        DateTime readyDate = Convert.ToDateTime(chVm.ActiveDate).Date;
                        if (endDate < nowDate)
                        {
                            item.DefaultCellStyle.BackColor = Color.Red;
                        }
                        if(nowDate == readyDate)
                        {
                            item.DefaultCellStyle.BackColor = Color.Pink;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindEnabledAlarms()
        {
            foreach (var vm in enabledAlarmList)
            {
                alarmVmBindingSource.List.Add(vm);
            }
            SetCount();
            DgvRefresh();
        }
        private void SetCount()
        {
            label4.Text = alarmVmBindingSource.List.Count.ToString();
        }
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int ri = e.RowIndex;
            int ci = e.ColumnIndex;

            if (ri == -1 || ri == dgv.NewRowIndex)
                return;

            if(dgv.Columns[dgvbtnalarmdescription].Index == ci)
            {
                int id = dgv.Rows[ri].Cells[0].Value.ToInt();

                var item = alarmVmBindingSource.List.OfType<AlarmVm>().FirstOrDefault(a => a.Id == id);

                ShowAlarmForm form = new ShowAlarmForm(item.Title);
                form.ShowDialog();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
