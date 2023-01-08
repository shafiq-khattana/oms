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

using Model.Deal.Model;
using WinFom.Admin.Database;

namespace WinFom.Deal.Forms
{
    public partial class AddDriverForm : Form
    {
        public int DriverId = 0;
        //private int companyId = 0;
        //private string companyName = "";
        public AddDriverForm(/*int _compId, string compName*/)
        {
            InitializeComponent();
            //companyId = _compId;
            //companyName = compName;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                //tbCompany.Text = companyName;
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
                if (!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text boxes");
                }
                Driver comp = new Driver
                {
                    Address = tbAddress.Text,
                    Contact = tbContact.Text,
                    DateAdded = DateTime.Now,
                    Extra = tbMisc.Text,
                    IsActive = true,
                    IsAvailable = true,
                    Name = tbName.Text,
                    Remarks = tbRemarks.Text,
                    
                };
                using (Context db = new Context())
                {
                    var dbObj = db.Drivers.ToList().FirstOrDefault(a => a.Equals(comp));
                    if (dbObj != null)
                    {
                        throw new Exception(string.Format("Driver ({0}), with address ({1}) with contact ({2}) already exists in database. ", dbObj.Name, dbObj.Address, dbObj.Contact));
                    }
                    comp = db.Drivers.Add(comp);
                    db.SaveChanges();
                    DriverId = comp.Id;
                    Gujjar.InfoMsg(string.Format("Driver with name ({0}) added in database successfully", comp.Name));
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
