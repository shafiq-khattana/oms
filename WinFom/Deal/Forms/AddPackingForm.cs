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
    public partial class AddPackingForm : Form
    {
        public int PackingId = 0;
        public AddPackingForm()
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
                Gujjar.NumbersOnly(tbUnitPrice);
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
                string unitPriceStr = tbUnitPrice.Text;
                if (string.IsNullOrEmpty(packingName))
                {
                    tbPackingName.BackColor = Color.Pink;
                    tbPackingName.Focus();
                    throw new Exception("Please enter packing name");
                }
                if(string.IsNullOrEmpty(unitPriceStr))
                {
                    tbUnitPrice.BackColor = Color.Pink;
                    tbUnitPrice.Focus();
                    throw new Exception("Please enter packing unit price");
                }
                DealPacking packing = new DealPacking
                {
                    Name = packingName,
                    UnitPrice = unitPriceStr.ToDecimal()
                };
                using (Context db = new Context())
                {
                    var obj = db.DealPackings.ToList().FirstOrDefault(a => a.Equals(packing));
                    if (obj != null)
                    {
                        throw new Exception("Packing already exists in database");
                    }

                    packing = db.DealPackings.Add(packing);
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
