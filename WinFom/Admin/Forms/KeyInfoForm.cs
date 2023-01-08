using Khattana.Common;
using Khattana.Secrecy;
using Model.Admin.Model;
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
using WinFom.Common.Forms;


namespace WinFom.Admin.Forms
{
    public partial class KeyInfoForm : Form
    {
        public KeyInfoForm()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new SampleControls().ShowDialog();
        }

        string macid = "";
        LicenseKeyInfo lkinfo = null;
        KeyInfo keyinfo = null;
        private void KeyInfoForm_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm wait2 = new WaitForm(LoadData);
                wait2.ShowDialog();

                if(lkinfo == null)
                {
                    throw new Exception("No data found");
                }
                if(keyinfo == null)
                {
                    throw new Exception("No key found");
                }
                tbDateExpire.Text = lkinfo.ExpiredDate;
                tbDaysLeft.Text = lkinfo.DaysLeft.ToString();
                tbKey.Text = keyinfo.Key;
                DateTime dtCreated = Convert.ToDateTime(lkinfo.ExpiredDate).AddDays(-lkinfo.DaysLeft);
                tbDateCreated.Text = dtCreated.ToShortDateString();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void LoadData()
        {
            try
            {
                macid = Lisence.GetMachineId();
                
                using (Context db = new Context())
                {
                    keyinfo = db.KeyInfo.Find(macid);
                }
                lkinfo = Lisence.GetLicenseKeyInfo(MsrCipher.Decrypt(keyinfo.Key));
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
                KeyForm form = new KeyForm();
                form.ShowDialog();
                if(form.IsDone)
                {
                    WaitForm wait2 = new WaitForm(LoadData);
                    wait2.ShowDialog();

                    if (lkinfo == null)
                    {
                        throw new Exception("No data found");
                    }
                    if (keyinfo == null)
                    {
                        throw new Exception("No key found");
                    }
                    tbDateExpire.Text = lkinfo.ExpiredDate;
                    tbDaysLeft.Text = lkinfo.DaysLeft.ToString();
                    tbKey.Text = keyinfo.Key;

                    DateTime dtCreated = Convert.ToDateTime(lkinfo.ExpiredDate).AddDays(-lkinfo.DaysLeft);
                    tbDateCreated.Text = dtCreated.ToShortDateString();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
