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
using Model.AppCompany.ViewModel;
using WinFom.Common.Forms;
using Model.AppDriver.ViewModel;

namespace WinFom.AppDriver.Forms
{
    public partial class VehicleDispatchForm : Form
    {
        List<VehicleDispatchVM> _data = null;
        private string btndgvview = "axuawer2341";
        public VehicleDispatchForm(List<VehicleDispatchVM> data)
        {
            InitializeComponent();
            _data = data;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                Gujjar.AddDatagridviewButton(dgv, btndgvview, "Description", "Description", 80);
                dgv.DataSource = _data;
                tbCompanies.Text = _data.Count.ToString();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int ri = e.RowIndex;
            if (ri == -1 || ri == dgv.NewRowIndex)
                return;

            if(dgv.Columns[btndgvview].Index == e.ColumnIndex)
            {
                string des = dgv.Rows[ri].Cells[2].Value.ToString();

                ShowAlarmForm form = new ShowAlarmForm(des, "Description");
                form.ShowDialog();
            }
        }
    }
}
