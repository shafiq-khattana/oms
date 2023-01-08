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



namespace WinFom.XtraCopy.Forms
{
    public partial class AddDriverForm : Form
    {
        public int DriverId = 0;
        public AddDriverForm()
        {
            InitializeComponent();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }

       
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Gujjar.IsValidForm(panel2))
                {
                    throw new Exception("Please fill all text boxes");
                }
                //Driver driver = new Driver
                //{
                //    Address = tbAddress.Text,
                //    CellNo = tbContact.Text,
                //    Name = tbCompany.Text
                //};
                //using (Context db = new Context())
                //{
                //    var dbObj = db.Drivers2.FirstOrDefault(a => a.CellNo == driver.CellNo);
                //    if(dbObj != null)
                //    {
                //        throw new Exception(string.Format("Driver ({0}), with address ({1}) with contact ({2}) already exists in database. ", 
                //            dbObj.Name, dbObj.Address, dbObj.CellNo));
                //    }
                //    driver = db.Drivers2.Add(driver);
                //    db.SaveChanges();
                //    DriverId = driver.Id;
                //    Gujjar.InfoMsg(string.Format("Driver with name ({0}) added in database successfully", driver.Name));
                //    Close();
                //}
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

        private void AddBrokerForm_Load(object sender, EventArgs e)
        {
            Gujjar.TB4(panel2);
        }
    }
}
