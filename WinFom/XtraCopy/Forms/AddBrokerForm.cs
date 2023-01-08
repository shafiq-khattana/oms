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
    public partial class AddBrokerForm : Form
    {
        public int BrokerId = 0;
        public AddBrokerForm()
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
                Broker comp = new Broker
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
                    var dbObj = db.Brokers.ToList().FirstOrDefault(a => a.Equals(comp));
                    if(dbObj != null)
                    {
                        throw new Exception(string.Format("Broker ({0}), with address ({1}) with contact ({2}) already exists in database. ", dbObj.Name, dbObj.Address, dbObj.Contact));
                    }
                    comp = db.Brokers.Add(comp);
                    db.SaveChanges();
                    BrokerId = comp.Id;
                    Gujjar.InfoMsg(string.Format("Broker with name ({0}) added in database successfully", comp.Name));
                    Close();
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

        private void AddBrokerForm_Load(object sender, EventArgs e)
        {
            Gujjar.TB4(panel2);
        }
    }
}
