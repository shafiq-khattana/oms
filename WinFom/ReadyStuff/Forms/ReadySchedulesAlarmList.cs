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
using Model.ReadyStuff.ViewModel;
using System.Data.Entity;
using WinFom.Common.Forms;
using Model.Admin.Model;
using WinFom.Common.Model;

namespace WinFom.ReadyStuff.Forms
{
    public partial class ReadySchedulesAlarmList : Form
    {
        private List<ReadyScheduleAlarm> readyScheduleAlarmList = null;
        private List<ReadyScheduleAlarmVM> readyScheduleAlarmVMList = new List<ReadyScheduleAlarmVM>();
        private string btndgvdescription = "btndgvdescription1234";
        private AppSettings appSet = Helper.AppSet;
        public ReadySchedulesAlarmList()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadAlarms()
        {
            try
            {
                using (Context db = new Context())
                {
                    readyScheduleAlarmList = db.ReadyScheduleAlarms.Where(a => a.IsActive)
                        .AsParallel().OrderByDescending(a => a.Id)
                        .ToList()
                        .Where(a => { DateTime dt = a.GenerateDate.Date; return dt >= appSet.StartDate && dt <= appSet.EndDate; }).ToList();

                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void DgvUpdate(List<ReadyScheduleAlarm> listOfAlarms)
        {
            try
            {
                readyScheduleAlarmVMBindingSource.List.Clear();
                foreach (var item in listOfAlarms)
                {
                    ReadyScheduleAlarmVM vm = new ReadyScheduleAlarmVM
                    {
                        Description = item.AlarmText,
                        Id = item.Id,
                        AlarmDate = item.AlarmReadyDate,
                        ScheduleDate = item.GenerateDate,
                        ReadyDate = item.EndDate
                    };

                    readyScheduleAlarmVMBindingSource.List.Add(vm);
                }
                label10.Text = listOfAlarms.Count.ToString();
            }
            catch (Exception ep)
            {

                throw ep;
            }
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                Gujjar.AddDatagridviewButton(dgv, btndgvdescription, "Details", "Details", 80);

                WaitForm wait2 = new WaitForm(LoadAlarms);
                wait2.ShowDialog();
                DgvUpdate(readyScheduleAlarmList);
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

                if(ri == -1 || ri == dgv.NewRowIndex)
                {
                    return;
                }

                if(dgv.Columns[btndgvdescription].Index == ci)
                {
                    int id = dgv.Rows[ri].Cells[0].Value.ToString().ToInt();
                    var schAlarm = readyScheduleAlarmVMBindingSource.List.OfType<ReadyScheduleAlarmVM>().FirstOrDefault(a => a.Id == id);
                    ShowAlarmForm form = new ShowAlarmForm(schAlarm.Description, "Ready Schedule Alarm");
                    form.ShowDialog();
                }
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

        private void btnDateRangeView_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fromDate = dtpFrom.Value.Date;
                DateTime toDate = dtpTo.Value.Date;
                var date = readyScheduleAlarmList.Where(a => a.EndDate.Date >= fromDate && a.EndDate.Date <= toDate).ToList();

                DgvUpdate(date);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAllDeals_Click(object sender, EventArgs e)
        {
            DgvUpdate(readyScheduleAlarmList);
        }
    }
}
