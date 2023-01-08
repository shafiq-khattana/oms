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

using WinFom.Common.Forms;
using Model.Deal.Model;
using WinFom.Admin.Database;


namespace WinFom.XtraCopy.Forms
{
    public partial class AddCompanyForm : Form
    {
        public int CompId = 0;
        public AddCompanyForm()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void AddCompanyForm_Load(object sender, EventArgs e)
        {
            Gujjar.TB4(panel2);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Gujjar.IsValidForm(panel2))
                {
                    throw new Exception("Please fill all text boxes");
                }

                Company comp = new Company
                {
                    Address = tbAddress.Text,
                    Contact = tbContact.Text,
                    DateAdded = DateTime.Now,
                    Extra = tbMisc.Text,
                    IsActive = true,
                    Name = tbCompany.Text,
                    Remarks = tbRemarks.Text
                };

                using (Context db = new Context())
                {
                    comp = db.Companies.Add(comp);
                    db.SaveChanges();
                    CompId = comp.Id;
                    Gujjar.InfoMsg(string.Format("Company with name ({0}) added in database successfully", comp.Name));
                    Close();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbCompany_Leave(object sender, EventArgs e)
        {
            try
            {
                string comp = tbCompany.Text;
                if(!string.IsNullOrEmpty(comp))
                {
                    using (Context db = new Context())
                    {
                        var obj = db.Companies.ToList().FirstOrDefault(a => a.Name.ToLower().Equals(comp.ToLower()));
                        if(obj != null)
                        {
                            tbCompany.BackColor = Color.Pink;
                            tbCompany.Focus();
                            throw new Exception(string.Format("Company {0} already exists", comp));
                        }
                    }
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
    }
}
