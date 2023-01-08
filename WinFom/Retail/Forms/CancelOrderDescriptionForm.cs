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

namespace WinFom.Retail.Forms
{
    public partial class CancelOrderDescriptionForm : Form
    {
        public bool IsDone = false;
        public string Description = "N/A";
        public CancelOrderDescriptionForm()
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
                Gujjar.TB4(pMain);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
            IsDone = false;
            Description = "N/A";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string des = tbLaborChargesDescription.Text;
                if(string.IsNullOrEmpty(des))
                {
                    throw new Exception("Please give description about cancellation of order");
                }
                Description = des;
                IsDone = true;
                Close();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
