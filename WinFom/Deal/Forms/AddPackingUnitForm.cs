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

namespace WinFom.Deal.Forms
{
    public partial class AddPackingUnitForm : Form
    {
        public int PackingId = 0;
        public AddPackingUnitForm()
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

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string packingName = tbPackingName.Text;
                if (string.IsNullOrEmpty(packingName))
                {
                    tbPackingName.BackColor = Color.Pink;
                    tbPackingName.Focus();
                    throw new Exception("Please enter packing name");
                }

                PackingUnit packing = new PackingUnit
                {
                    Name = packingName
                };
                using (Context db = new Context())
                {
                    var obj = db.PackingUnits.FirstOrDefault(a => a.Name.ToLower().Equals(packing.Name.ToLower()));
                    if (obj != null)
                    {
                        throw new Exception("Packing unit already exists in database");
                    }

                    packing = db.PackingUnits.Add(packing);
                    db.SaveChanges();

                    Gujjar.InfoMsg(string.Format("Packing {0} added in database successfully", packing.Name));
                    PackingId = packing.Id;
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
