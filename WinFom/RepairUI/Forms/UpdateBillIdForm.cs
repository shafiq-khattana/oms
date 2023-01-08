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
using Model.ReadyStuff.Model;
using WinFom.Common.Forms;
using Model.Admin.Model;
using WinFom.Common.Model;

namespace WinFom.RepairUI.Forms
{
    public partial class UpdateBillIdForm : Form
    {
        private BillTransVM billTrans = null;
        public string BillId { get; set; }
        public UpdateBillIdForm(BillTransVM vm)
        {
            InitializeComponent();
            billTrans = vm;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                tbRepairingshop.Text = billTrans.Place;
                tbBillId.Text = billTrans.BillId;
                tbBillId.Select(0, tbBillId.Text.Length);
                Gujjar.TB4(pMain);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbBillId.Text))
                {
                    tbBillId.BackColor = Color.Pink;
                    tbBillId.Focus();
                    throw new Exception("Please provide bill no");

                }

                using (Context db = new Context())
                {
                    var obj = db.RepairDispatchRecords.FirstOrDefault(a => a.BillNo.Equals(tbBillId.Text) && a.RepPlaceId == billTrans.PlaceId);
                    if(obj != null)
                    {
                        throw new Exception(string.Format("A record with bill id ({0}) and repairing shop ({1}) already added in database", billTrans.BillId, billTrans.Place));
                    }
                }
                BillId = tbBillId.Text;
                Close();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }

    public class BillTransVM
    {
        public string BillId { get; set; }
        public string Place { get; set; }
        public int PlaceId { get; set; }
    }
}
