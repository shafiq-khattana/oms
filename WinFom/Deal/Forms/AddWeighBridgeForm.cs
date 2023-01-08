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
    public partial class AddWeighBridgeForm : Form
    {
        //public bool IsDone = false;
        public int weighBridgeId = 0;
        private WeighBridgeType _type = WeighBridgeType.Loading;
        public AddWeighBridgeForm(WeighBridgeType type)
        {
            InitializeComponent();
            _type = type;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                cbTypes.DataSource = Enum.GetNames(typeof(WeighBridgeType));
                cbTypes.Text = _type.ToString();
                cbTypes.Enabled = false;
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }
                WeighBridgeType wType = (WeighBridgeType)Enum.Parse(typeof(WeighBridgeType), cbTypes.Text);
                using (Context db = new Context())
                {
                    WeighBridge wb = new WeighBridge
                    {
                        Name = tbName.Text,
                        Address = tbAddress.Text,
                        Phone = tbPhone.Text,
                        WeighBrideType = wType
                    };
                    var bridges = db.WeighBridges.ToList();
                    if(bridges.FirstOrDefault(a => a.Equals(wb)) != null)
                    {
                        throw new Exception(string.Format("Weigh Bridge: {0} already exist in database", wb.Name));
                    }

                    wb = db.WeighBridges.Add(wb);
                    db.SaveChanges();

                    Gujjar.InfoMsg(string.Format("Weigh Bridge: {0} has been added in database", wb.Name));
                    weighBridgeId = wb.Id;
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
