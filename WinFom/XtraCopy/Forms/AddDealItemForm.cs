using Khattana.Common;
using WinFom.Admin.Database;
using Model.Deal.Model;
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
    public partial class AddDealItemForm : Form
    {
        public int ItemId = 0;
        public AddDealItemForm()
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
                string dealItem = tbDealItem.Text;
                if(string.IsNullOrEmpty(dealItem))
                {
                    tbDealItem.BackColor = Color.Pink;
                    tbDealItem.Focus();
                    throw new Exception("Please enter item name");
                }

                DealItem item = new DealItem
                {
                    Name = dealItem
                };
                using (Context db = new Context())
                {
                    var obj = db.DealItems.ToList().FirstOrDefault(a => a.Equals(item));
                    if(obj != null)
                    {
                        throw new Exception("Deal item already exists in database");
                    }

                    item = db.DealItems.Add(item);
                    db.SaveChanges();

                    Gujjar.InfoMsg(string.Format("Item {0} added in database successfully", item.Name));
                    ItemId = item.Id;
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

        private void AddDealItemForm_Load(object sender, EventArgs e)
        {
            Gujjar.TB4(panel2);
        }
    }
}
