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
using Model.ReadyStuff.ViewModel;

namespace WinFom.ReadyStuff.Forms
{
    public partial class AddBharthiForm : Form
    {
        BharthiType bharthiType = null;
        private List<BharthiType> bharthiTypes = null;
        public BharthiVM bharthiVM = null;
        public AddBharthiForm()
        {
            InitializeComponent();
        }

        private void LoadBharthiTypes()
        {
            try
            {
                using (Context db = new Context())
                {
                    bharthiTypes = db.BharthiTypes.ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void BindCBharthiTypes()
        {
            cbBharthiTypes.DataSource = bharthiTypes;
            cbBharthiTypes.DisplayMember = "Title";
            cbBharthiTypes.ValueMember = "Id";
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait2 = new WaitForm(LoadBharthiTypes);
                wait2.ShowDialog();
                BindCBharthiTypes();

                Gujjar.TB4(pMain);
                Gujjar.NumbersOnly(tbNoOfPackings);
                
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void cbBharthiTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBharthiTypes.SelectedIndex == -1)
                return;

            bharthiType = cbBharthiTypes.SelectedItem as BharthiType;
            label1.Text = string.Format("Per {0} qty:", bharthiType.Title);
            tbBharthi.Text = bharthiType.UnitQty.ToString("n1");
            label3.Text = string.Format("No of {0}:", bharthiType.Title);
            label4.Text = string.Format("Total {0}-(s):", bharthiType.Title);
        }

        private void btnAddBharthiType_Click(object sender, EventArgs e)
        {
            try
            {
                AddBharthiType form = new AddBharthiType();
                form.ShowDialog();
                int bid = form.BharthiTypeId;
                if(bid != 0)
                {
                    WaitForm wait = new WaitForm(LoadBharthiTypes);
                    wait.ShowDialog();
                    BindCBharthiTypes();

                    cbBharthiTypes.SelectedItem = cbBharthiTypes.Items.OfType<BharthiType>()
                        .FirstOrDefault(a => a.Id == bid);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbNoOfPackings_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal packings = 0;
                if(string.IsNullOrEmpty(tbNoOfPackings.Text))
                {
                    packings = 0;
                }
                else
                {
                    packings = tbNoOfPackings.Text.ToDecimal();
                }
                decimal total = packings * bharthiType.UnitQty;
                tbTotalPackings.Text = total.ToString("n2");
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

                if(bharthiType == null)
                {
                    throw new Exception("Please select packing type");
                }
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }

                DialogResult res = Gujjar.ConfirmYesNo("Are you sured?");
                if(res == DialogResult.No)
                {
                    return;
                }
                bharthiVM = new BharthiVM
                {
                    Id = bharthiType.Id,
                    Qty = tbNoOfPackings.Text.ToDecimal(),
                    Total = tbTotalPackings.Text.ToDecimal(),
                    Type = string.Format("{0}-({1} Kgs)", bharthiType.Title, bharthiType.UnitQty.ToString("n1"))
                };
                Close();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
