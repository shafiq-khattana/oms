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
    public partial class AddPackingForm : Form
    {
        public int PackingId = 0;
        public AddPackingForm()
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
                string packingName = tbPackingName.Text;
                if(string.IsNullOrEmpty(packingName))
                {
                    tbPackingName.BackColor = Color.Pink;
                    tbPackingName.Focus();
                    throw new Exception("Please enter packing name");
                }

                //DealPacking2 packing = new DealPacking2
                //{
                //    Name = packingName
                //};
                //using (Context db = new Context())
                //{
                //    var obj = db.DealPackings2.ToList().FirstOrDefault(a => a.Equals(packing));
                //    if(obj != null)
                //    {
                //        throw new Exception("Packing already exists in database");
                //    }

                //    packing = db.DealPackings2.Add(packing);
                //    db.SaveChanges();

                //    Gujjar.InfoMsg(string.Format("Packing {0} added in database successfully", packing.Name));
                //    PackingId = packing.Id;
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

        private void AddDealItemForm_Load(object sender, EventArgs e)
        {
            Gujjar.TB4(panel2);
        }
    }
}
