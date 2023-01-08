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
    public partial class AddPackingUnitForm : Form
    {
        public int PackingUnitId = 0;
        public AddPackingUnitForm()
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
                string packingName = tbPackingName.Text;
                if(string.IsNullOrEmpty(packingName))
                {
                    tbPackingName.BackColor = Color.Pink;
                    tbPackingName.Focus();
                    throw new Exception("Please enter packing name");
                }

                //PackingUnit2 packing = new PackingUnit2
                //{
                //    Name = packingName
                //};
                //using (Context db = new Context())
                //{
                //    var obj = db.PackingUnit2.FirstOrDefault(a => a.Name.ToLower().Equals(packing.Name.ToLower()));
                //    if(obj != null)
                //    {
                //        throw new Exception("Packing unit already exists in database");
                //    }

                //    packing = db.PackingUnit2.Add(packing);
                //    db.SaveChanges();

                //    Gujjar.InfoMsg(string.Format("Packing {0} added in database successfully", packing.Name));
                //    PackingUnitId = packing.Id;
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
