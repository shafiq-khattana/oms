using Khattana.Common;
using Khattana.Secrecy;
using Model.Admin.Model;
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
using WinFom.Common.Forms;


namespace WinFom.Admin.Forms
{
    public partial class TrialForm : Form
    {

        public TrialForm()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new SampleControls().ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        Rahzam rahzam = null;
        private void LoadData()
        {
            try
            {
                using (Context db = new Context())
                {
                    rahzam = db.Anattakh.First();
                }
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
                KeyForm form = new KeyForm();
                form.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void TrialForm_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait = new WaitForm(LoadData);
                wait.ShowDialog();

                string stDate = MsrCipher.Decrypt(rahzam.ItheyRakh);
                string endDt = MsrCipher.Decrypt(rahzam.ChalBasKerYar);

                DateTime startDate = Convert.ToDateTime(stDate);
                DateTime endDate = Convert.ToDateTime(endDt);

                lblDtStart.Text = startDate.ToShortDateString();
                lblDtEnd.Text = endDate.ToShortDateString();

                int days = (endDate - DateTime.Now).Days;
                label1.Text = string.Format("Days left ({0})", days);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
