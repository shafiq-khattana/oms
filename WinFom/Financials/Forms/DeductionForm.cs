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
using Model.Financials.Model;
using Model.Admin.Model;
using WinFom.Common.Model;
using WinFom.Common.Forms;

namespace WinFom.Financials.Forms
{
    public partial class DeductionForm : Form
    {
        public float PercentageValue = 0;
        public DeductionForm()
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
                Gujjar.NumbersOnly(tbPercent);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                string txt = tbPercent.Text;
                if(string.IsNullOrEmpty(txt))
                {
                    throw new Exception("Enter percentage value");
                }
                PercentageValue = (float)txt.ToDecimal();
                if(PercentageValue < 0 || PercentageValue > 100)
                {
                    throw new Exception("Invalid value, enter (0 to 100)");
                }
                Close();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
