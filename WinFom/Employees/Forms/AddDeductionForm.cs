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
using Model.Employees.Model;

namespace WinFom.Employees.Forms
{
    public partial class AddDeductionForm : Form
    {
        public int DeductionId = 0;
        public AddDeductionForm()
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
                Gujjar.NumbersOnly(tbAmount);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }

                using (Context db = new Context())
                {
                    string title = tbDeductionTitle.Text;
                    decimal amount = tbAmount.Text.ToDecimal();

                    var obj = db.SalaryDeductions.ToList().FirstOrDefault(a => a.Title.ToLower().Equals(title.ToLower()));
                    if(obj != null)
                    {
                        throw new Exception("Deduction title already added in database");
                    }

                    obj = new SalaryDeduction
                    {
                        Id = 0,
                        Amount = amount,
                        Title = title
                    };

                    obj = db.SalaryDeductions.Add(obj);
                    db.SaveChanges();
                    DeductionId = obj.Id;
                    Gujjar.InfoMsg("Deduciton is added in database");
                    Close();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
