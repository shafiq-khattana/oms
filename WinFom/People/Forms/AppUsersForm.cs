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
using Model.Admin.ViewModel;
using WinFom.Admin.Forms;
using Khattana.Secrecy;
using WinFom.People.Forms;

namespace WinFom.RepairUI.Forms
{
    public partial class AppUsersForm : Form
    {
        private string dgvbtnupdate = "dgvbtnupdate121";
        private string dgvbtnoptions = "btndgvoptions";
        private List<AppUser> appUsers = null;
        private AppUser appUser = SingleTon.LoginForm.appUser;
        public AppUsersForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadUsers()
        {
            try
            {
                if(appUsers != null)
                {
                    appUsers.Clear();
                    appUsers = null;
                }

                using (Context db = new Context())
                {
                    appUsers = db.AppUsers.Where(a => a.Id != "admin24").ToList();
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
                if (appUser.Id == "admin1" || appUser.Id == "admin24")
                {
                    Gujjar.AddDatagridviewButton(dgv, dgvbtnupdate, "Update", "Update", 80);
                    Gujjar.AddDatagridviewButton(dgv, dgvbtnoptions, "Options", "Options", 80);
                }
                WaitForm wait1 = new WaitForm(LoadUsers);
                wait1.ShowDialog();

                appUserVMBindingSource.List.Clear();
                foreach (var item in appUsers)
                {
                    AppUserVM vm = new AppUserVM
                    {
                        IsActive = item.IsActive,
                        Contact = item.Contact,
                        Id = item.Id,
                        Name = item.Name,
                        Options = item.Options,
                        Password = item.Password
                    };
                    appUserVMBindingSource.List.Add(vm);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                int ci = e.ColumnIndex;

                if (ri == -1 || ri == dgv.NewRowIndex)
                {
                    return;
                }
                if (appUser.Id == "admin1" || appUser.Id == "admin24")
                {
                    if (dgv.Columns[dgvbtnoptions].Index == ci)
                    {
                        string uid = dgv.Rows[ri].Cells[0].Value.ToString();
                        AppUserVM uvm = appUserVMBindingSource.List.OfType<AppUserVM>()
                            .FirstOrDefault(a => a.Id == uid);

                        UserOps userOps = null;
                        string ops = uvm.Options;
                        ops = MsrCipher.Decrypt(ops);
                        userOps = Gujjar.LoadFromXMLString<UserOps>(ops);
                        UserOpsForm form = new UserOpsForm(userOps);
                        form.ShowDialog();

                        if (form.IsDone)
                        {
                            string uid2 = dgv.Rows[ri].Cells[0].Value.ToString();
                            AppUserVM uvm2 = appUserVMBindingSource.List.OfType<AppUserVM>()
                                .FirstOrDefault(a => a.Id == uid2);
                            uvm2.Options = form.NewOptions;
                            dgv.Refresh();
                        }
                    }

                    if (dgv.Columns[dgvbtnupdate].Index == ci)
                    {
                        string uid = dgv.Rows[ri].Cells[0].Value.ToString();
                        DialogResult res = Gujjar.ConfirmYesNo("Are you sure to update user data");
                        UpdateAppUserForm form = new UpdateAppUserForm(uid);
                        form.ShowDialog();

                        if(form.IsDone)
                        {
                            WaitForm wait1 = new WaitForm(LoadUsers);
                            wait1.ShowDialog();

                            appUserVMBindingSource.List.Clear();
                            foreach (var item in appUsers)
                            {
                                AppUserVM vm = new AppUserVM
                                {
                                    IsActive = item.IsActive,
                                    Contact = item.Contact,
                                    Id = item.Id,
                                    Name = item.Name,
                                    Options = item.Options,
                                    Password = item.Password
                                };
                                appUserVMBindingSource.List.Add(vm);
                            }
                        }
                    }
                }
                
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
                AddAppUserForm form = new AddAppUserForm();
                form.ShowDialog();

                if(form.IsDone)
                {
                    WaitForm wait1 = new WaitForm(LoadUsers);
                    wait1.ShowDialog();

                    appUserVMBindingSource.List.Clear();
                    foreach (var item in appUsers)
                    {
                        AppUserVM vm = new AppUserVM
                        {
                            IsActive = item.IsActive,
                            Contact = item.Contact,
                            Id = item.Id,
                            Name = item.Name,
                            Options = item.Options,
                            Password = item.Password
                        };
                        appUserVMBindingSource.List.Add(vm);
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
