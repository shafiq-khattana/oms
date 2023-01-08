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
    public partial class AddSelectorForm : Form
    {
        public int SelectorId = 0;
        public AddSelectorForm()
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
        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Gujjar.IsValidForm(panel2))
                {
                    throw new Exception("Please fill all text boxes");
                }

                //Selector2 selector = new Selector2
                //{
                //    Address = tbAddress.Text,
                //    Contact = tbContact.Text,
                //    DateAdded = DateTime.Now,
                //    Extra = tbMisc.Text,
                //    IsActive = true,
                //    Name = tbCompany.Text,
                //    Remarks = tbRemarks.Text
                //};

                //using (Context db = new Context())
                //{
                //    var dbObj = db.Selector2.ToList().FirstOrDefault(a => a.Equals(selector));
                //    if(dbObj != null)
                //    {
                //        throw new Exception(string.Format("Selector ({0}), with address ({1}) with contact ({2}) already exists in database. ", dbObj.Name, dbObj.Address, dbObj.Contact));
                //    }
                //    selector = db.Selector2.Add(selector);
                //    db.SaveChanges();
                //    SelectorId = selector.Id;
                //    Gujjar.InfoMsg(string.Format("Selector with name ({0}) added in database successfully", selector.Name));
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
