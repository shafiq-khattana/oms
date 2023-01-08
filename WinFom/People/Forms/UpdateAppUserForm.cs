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
using Khattana.Secrecy;

namespace WinFom.People.Forms
{
    public partial class UpdateAppUserForm : Form
    {
        private string userId = "";
        private AppUser appUserLoaded = null;
        public bool IsDone { get; set; }
        public UpdateAppUserForm(string uid)
        {
            InitializeComponent();
            userId = uid;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadUser()
        {
            try
            {
                using (Context db = new Context())
                {
                    appUserLoaded = db.AppUsers.Find(userId);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                Gujjar.TB4(pMain);
                IsDone = false;

                WaitForm wait = new WaitForm(LoadUser);
                wait.ShowDialog();

                tbContactNo.Text = appUserLoaded.Contact;
                tbName.Text = appUserLoaded.Name;
                //tbPassword.Text = MsrCipher.Decrypt(appUserLoaded.Password);
                tbPassword.Text = appUserLoaded.Password;
                tbUserId.Text = appUserLoaded.Id;
                bswState.Value = appUserLoaded.IsActive;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void pMain_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }

                if (!Helper.ConfirmAdminPassword())
                {
                    return;
                }

                DialogResult res = Gujjar.ConfirmYesNo("Please confirm.. All information is correct");
                if (res == DialogResult.No)
                    return;

                string userId = tbUserId.Text;
                using (Context db = new Context())
                {
                    var userDb = db.AppUsers.Find(userId);

                    userDb.Name = tbName.Text;
                    userDb.Contact = tbContactNo.Text;
                    userDb.IsActive = bswState.Value;
                    //userDb.Password = MsrCipher.Encrypt(tbPassword.Text);
                    userDb.Password = tbPassword.Text.Trim();
                    db.Entry(userDb).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    int n = db.SaveChanges();
                    IsDone = true;

                    Gujjar.InfoMsg("User data is updated successfully");
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                tbPassword.UseSystemPasswordChar = false;
            }
            else
            {
                tbPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
