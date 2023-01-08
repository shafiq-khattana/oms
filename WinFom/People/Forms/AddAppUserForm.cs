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
    public partial class AddAppUserForm : Form
    {
        public bool IsDone { get; set; }
        public AddAppUserForm()
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
                Gujjar.TB4(pMain);
                IsDone = false;
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
                    var dbObj = db.AppUsers.ToList().FirstOrDefault(a => a.Id.ToLower() == userId.ToLower());
                    if (dbObj != null)
                    {
                        throw new Exception("User already added in database");
                    }

                    AppUser appUser = new AppUser
                    {
                        ArchiveSaleOrders = null,
                        CancelSaleOrders = null,
                        Contact = tbContactNo.Text,
                        Deals = null,
                        Id = userId,
                        IsActive = true,
                        Name = tbName.Text,
                        OilDeals = null,
                        OilDirtDeals = null,
                        Options = null,
                        Password = MsrCipher.Encrypt(tbPassword.Text),
                        ReadyDeals = null,
                        SaleOrders = null,
                        UserOptions = null
                    };
                    UserOps userOps = new UserOps();
                    userOps.UserId = userId;
                    string ops = Gujjar.ToXML(userOps);
                    appUser.Options = MsrCipher.Encrypt(ops);

                    db.AppUsers.Add(appUser);
                    db.SaveChanges();

                    IsDone = true;
                    Gujjar.InfoMsg("User is added in system successfully");
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
