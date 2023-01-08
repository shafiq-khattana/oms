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

using Model.Deal.Model;
using WinFom.Admin.Database;

namespace WinFom.Deal.Forms
{
    public partial class AddVehicleForm : Form
    {
        public int VehicleId = 0;
        //private int companyId = 0;
        //private string companyName = "";
        public AddVehicleForm(/*int _compId, string compName*/)
        {
            InitializeComponent();
            //companyId = _compId;
            //companyName = compName;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                //tbCompany.Text = companyName;
                cbVehicles.DataSource = Enum.GetNames(typeof(VehicleType));
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
                if (!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text boxes");
                }
                Vehicle vehicle = new Vehicle
                {                    
                    No = tbVehicleNo.Text,
                    VehicleType = (VehicleType)Enum.Parse(typeof(VehicleType), cbVehicles.Text),
                    Status = VehicleStatus.Available
                };
                using (Context db = new Context())
                {
                    var dbObj = db.Vehicles.ToList().FirstOrDefault(a => a.Equals(vehicle));
                    if (dbObj != null)
                    {
                        throw new Exception(string.Format("Vehicle ({0}), with No ({1}) already exists in database. ", dbObj.VehicleType, dbObj.No));
                    }
                    vehicle = db.Vehicles.Add(vehicle);
                    db.SaveChanges();
                    VehicleId = vehicle.Id;
                    Gujjar.InfoMsg(string.Format("Vehicle ({0}-{1}) added in database successfully", vehicle.VehicleType, vehicle.No));
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
