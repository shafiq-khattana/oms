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
    public partial class AddAllowanceForm : Form
    {
        public int AllowanceId = 0;
        public AddAllowanceForm()
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

                    var obj = db.SalaryAllowances.ToList().FirstOrDefault(a => a.Title.ToLower().Equals(title.ToLower()));
                    if(obj != null)
                    {
                        throw new Exception("Allowance title already added in database");
                    }

                    obj = new SalaryAllowance
                    {
                        Id = 0,
                        Amount = amount,
                        Title = title
                    };

                    obj = db.SalaryAllowances.Add(obj);
                    db.SaveChanges();
                    AllowanceId = obj.Id;
                    Gujjar.InfoMsg("Allowance is added in database");
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
