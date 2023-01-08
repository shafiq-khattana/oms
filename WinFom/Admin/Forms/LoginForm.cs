using Khattana.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.Entity;
using WinFom.Common.Forms;

using Model.Deal.Model;
using WinFom.Common.Model;
using WinFom.Admin.Database;
using Model.Retail.Model;
using WinFom.Client.Forms;
using Khattana.Secrecy;
using Khattana.Model;
using Model.Admin.Model;
using WinFom.Common.Reports.Model;

namespace WinFom.Admin.Forms
{
    public partial class LoginForm : Form
    {
        private string userId = "";
        private string password = "";
        private bool isValid = false;
        public AppUser appUser = null;
        private string dirName = @"C:\GBS";
        private string filePath = "";
        public List<Customer> AppCustomers = null;
        public List<Product> AppProducts = null;
        private string gbs = @"qynPDTYhAOWX4gdO2GI3lKmtm6NO7sR7CnPVyTZe1ibUCRWVDL+4mKTjbzEzwJk1Sxm4HOmX+eSvhgHYRgLX33/aZYesmi2JDuhjRDlvwzGU3tzNgzW06nNu9xEIn3rYy0LKoFfZwuO1PL5Lx45HI4BVcotieZKPxKQneumVcBvQ/9uv8igGQusjLXDyxZPOU9eI1y0Ip98OldMBPZc9FLvjFRpJZ2vCui+rIrQjmu7iYooCCeyF69tQcjX6UFHkEhTH8ZtASJIMJcI9zOUBuig8OQ5VEfoFGKzauYQxDT5k8E8j/ylPpBPY59AHo7biRrqSn+x2Lk5qyZChQdCWDfFq/WQw4BLextTw8ck8b2gHAFhW75WhrWd093Eke/sgARv1lcSeNxzLpTJov04kineQE3EAgsy6Q9L4Vz6ptg6hp0W/6AonE3s307uHseOjNs39+zUmtgzb6ldYnHFXAhl7zcBH4C4LFncEHTa9fCgAn1KLMc8BqsDZH9U2k4ZrSV4ahEOMDXUF4l4uW/bAkoWUOel7mxcLH3LuAMQ4WsxE6BRGPO9UB1fhVPBO6pNkJ8uVm8Hzj/jz38C1GsTK9a6whad+OL6wrJrjISJq7tcXl1xhw1d05N/01bbgvs7kwclkbqEdoLRHPhNn7GCSwViXQ/g9UM5GeuUmSsxcczycw2jP7632U+LFN3ikMBox55EgAeb6PGjrL3OTrXsBjJOZFyKGjZFVeBG3e0TRztY7GbRAsD4xYIHlI31IXDNI1AxAIU3gyOppYj1KFBktX0+XUUpc4Y41p2h7B0VaOw4YhMaU1ZvAnLa+NLYXkYUc14ZBaU8SqsdcCy6mPPyY/Or8DWl8ThmtDZuHGLVRpw/E2mAtFDjMTs8i66uQr4+/GmLOW3YaOVzkzWHhFriDYiE/L4G42eIJSgfUCq1+Nz9guJQw80uNIZuIFjk1fGANXikWJ/008HT6uAfxsmbpbnYQcmiFDUAlARqu9azAXQ9A3dSbg8/jHlQ9PXvC3D4gzO0FAucL95UoddDyuSXYbPoCuXiDqUK1ex+/CsTsz05SvQBYsJ99pD20RgKto2hZx1dJc4WCh3wbFSQ3oO4BzmdRyLn8zAJhAytl7kRvtrj115wkub9XxEOTKQHqq9iSVOT1B7rikG++qwGDIsdDroWBJEPf05eZIT8fHB85qevPBFQoZtlDoMYsUDBiR6EsyvXE2iVwme7nlXA6K081GcxJjWqMS5qGM7//fnxpM6Nsu4FnUug2Sn7uTpiQO7kFGk5hMCkzmSx7n27oiLRZ3l4UUGQ+a0suXZXaP84bv4LBNC25gk2fimtbVBQXQ77SkRiXsui+GxA5htZ4Rii000g1IsWLi2253K6ZaJ69neAalu6SdvlGh0rRV6VN/d5rrpqQ0BvU+Eq/fwoJULr4CsK0Bc3PPi2gEC8dUkqHqbA=";
        public bool RememberMeAdmin = false;
        public bool RememberMeUser = false;
        public UserOps UserOptions = null;
        public AppSettings AppSett = null;
        public FCompRVM FCompRVM = null;
        public LoginForm()
        {
            InitializeComponent();
            filePath = string.Format(@"{0}\gbss.txt", dirName);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void InitializedDB()
        {
            //try
            //{
            //    using (Context db = new Context())
            //    {
            //        var sett = db.AppUsers.First();
            //    }
            //}
            //catch(InvalidOperationException)
            //{
            //    using (Context db = new Context())
            //    {
            //        DbSeed.Seed(db);
            //    }
            //}
            //catch (Exception exp)
            //{
            //    Gujjar.ErrMsg(exp);
            //}
        }
        private void CheckCredentials()
        {
            try
            {

                using (Context db = new Context())
                {
                    appUser = db.AppUsers.FirstOrDefault(a => a.Id == userId);
                    if(appUser == null)
                    {
                        isValid = false;
                        throw new Exception("Invalid user id");
                    }
                    string dbPassword = appUser.Password;
                    if(dbPassword != password)
                    {
                        isValid = false;
                        throw new Exception("Invalid password");
                    }
                    if (appUser != null)
                    {
                        appUser.Options = MsrCipher.Decrypt(appUser.Options);
                        UserOptions = Gujjar.LoadFromXMLString<UserOps>(appUser.Options);
                        if(UserOptions.UserId != appUser.Id)
                        {
                            isValid = false;
                            throw new Exception("User options don't match with user id");
                        }
                        if(!appUser.IsActive)
                        {
                            isValid = false;
                            throw new Exception("User is disabled from admin");
                        }
                        isValid = true;
                        AppSett = db.AppSettings.First();
                        FCompRVM = new FCompRVM
                        {
                            Address = AppSett.Address,
                            LogoImg = AppSett.Logo,
                            Name = AppSett.Name,
                            Phone = AppSett.PhoneNo
                        };
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        public void LoadInitialData()
        {
            try
            {
                using (Context db = new Context())
                {
                    AppCustomers = db.Customers.Where(a => a.IsActive).AsParallel().ToList();

                    AppProducts = db.Products.Include(a => a.ProductSize).AsParallel().ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {

            try
            {
                userId = tbUserName.Text;
                password = tbPassword.Text;
                if (string.IsNullOrEmpty(userId))
                {
                    tbUserName.BackColor = Color.Pink;
                    tbUserName.Focus();
                    throw new Exception("Please enter user id");
                }
                if (string.IsNullOrEmpty(password))
                {
                    tbPassword.BackColor = Color.Pink;
                    tbPassword.Focus();
                    throw new Exception("Please enter password");
                }
                isValid = false;

                WaitForm w1 = new WaitForm(CheckCredentials);
                w1.ShowDialog();

                if (isValid)
                {
                    WaitForm wait4 = new WaitForm(LoadInitialData);
                    wait4.ShowDialog();
                    SingleTon.LoginForm.Hide();
                    

                    MainForm form = new MainForm();
                    form.ShowDialog();
                }
                else
                {
                    throw new Exception("Invalid user credentials or user is disabled.");
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private bool okRes = false;
        private void IsOk()
        {
            try
            {

                using (Context db = new Context())
                {
                    var setting = db.AppSettings.FirstOrDefault();
                    if(setting != null)
                    {
                        if(setting.bool1)
                        {
                            var obj = db.Anattakh.FirstOrDefault();
                            if (obj != null)
                            {
                                string dateEnd = MsrCipher.Decrypt(obj.ChalBasKerYar);
                                string dateSt = MsrCipher.Decrypt(obj.RabRakha);

                                DateTime dtEnd = Convert.ToDateTime(dateEnd);
                                DateTime lastLog = Convert.ToDateTime(dateSt).AddMinutes(-10);
                                DateTime noDt = DateTime.Now;

                                if (lastLog > noDt)
                                {
                                    throw new Exception(string.Format("Last login time ({0}), now login time ({1}). Please correct time", lastLog, noDt));
                                }

                                if (dtEnd.Date < noDt)
                                {
                                    setting.bool1 = false;
                                    db.Entry(setting).State = EntityState.Modified;
                                    db.SaveChanges();
                                    throw new Exception("Trial period has been ended");
                                }

                                var db2 = db.Anattakh.First();
                                db2.RabRakha = MsrCipher.Encrypt(noDt.ToString());
                                db.Entry(db2).State = EntityState.Modified;
                                db.SaveChanges();

                                okRes = true;
                                int rds = (dtEnd - noDt).Days;

                                if (rds < 10)
                                {
                                    Gujjar.InfoMsg(string.Format("Dear user, ({0}) days are remaining from trial period of this software.", rds));
                                }
                            }
                            else
                            {
                                
                            }
                        }
                        else
                        {
                            SecOptMod secMod = Helper.GetSecOpsObj();
                            string mid = Lisence.GetMachineId();
                            var obj4 = db.KeyInfo.Find(mid);

                            if (obj4 == null)
                            {
                                Gujjar.ErrMsg("There is no license information in your system.");
                                DialogResult res = Gujjar.ConfirmYesNo("Do you want to insert lisence key?");
                                if (res == DialogResult.No)
                                {
                                    throw new Exception("Thanks you");
                                }
                                KeyForm form = new KeyForm();
                                form.ShowDialog();
                                if (!form.IsDone)
                                {
                                    okRes = false;
                                    return;
                                }
                                else
                                {
                                    okRes = true;
                                    return;
                                }
                                
                            }

                            DateTime dtN = DateTime.Now;
                            DateTime lastLog = Convert.ToDateTime(MsrCipher.Decrypt(obj4.WahJi));

                            if (dtN < lastLog.AddMinutes(-10))
                            {
                                throw new Exception(string.Format("Last login time ({0}), now login time ({1}). Please correct time", lastLog, dtN));
                            }

                            string key = MsrCipher.Decrypt(obj4.Key);


                            if (!Lisence.IsValidKey(key, secMod.secretPhrase))
                            {
                                var obj6 = db.KeyInfo.Find(mid);
                                db.KeyInfo.Remove(obj6);
                                db.SaveChanges();

                                Gujjar.ErrMsg("Invalid Lisence Key");
                                DialogResult res = Gujjar.ConfirmYesNo("Do you want to insert lisence key?");
                                if (res == DialogResult.No)
                                {
                                    throw new Exception("Thanks you");
                                }
                                KeyForm form = new KeyForm();
                                form.ShowDialog();
                                if (!form.IsDone)
                                {
                                    okRes = false;
                                    return;
                                }
                                else
                                {
                                    okRes = true;
                                    return;
                                }
                            }
                            string isValid2 = MsrCipher.Decrypt(obj4.IsValid);
                            if (!isValid2.Equals(secMod.aaho))
                            {
                                throw new Exception("Error: 1185. Please contact to administrator");
                            }

                            var dbObj = db.KeyInfo.Find(mid);
                            dbObj.WahJi = MsrCipher.Encrypt(dtN.ToString());

                            db.Entry(dbObj).State = EntityState.Modified;
                            db.SaveChanges();
                            okRes = true;

                            LicenseKeyInfo linfo = Lisence.GetLicenseKeyInfo(key);
                            if (linfo.DaysLeft <= 10)
                            {
                                Gujjar.InfoMsg(string.Format("Dear user, ({0}) days are remaining to expire the existing lisence key", linfo.DaysLeft));
                            }
                        }
                    }
                    
                }
            }
            catch (Exception exp)
            {
                okRes = false;
                Gujjar.ErrMsg(exp);

            }
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                WaitForm w84 = new WaitForm(IsOk);
                w84.ShowDialog();

                if (!okRes)
                {
                    Application.Exit();
                }
                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, gbs);
                }
                //WaitForm w44 = new WaitForm(DoSecose);
                //w44.ShowDialog();
                WaitForm w3 = new WaitForm(Delay);
                w3.ShowDialog();
                WaitForm w4 = new WaitForm(InitializedDB);
                w4.ShowDialog();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void Delay()
        {
            //Thread.Sleep(2000);
        }

        private void DoSecose()
        {
            try
            {
                using (Context db = new Context())
                {
                    var sett = db.AppSettings.First();
                    DateTime endDate = new DateTime(2031, 06, 30);
                    
                    if (DateTime.Now.Date > endDate)
                    {
                        throw new Exception("Error code: 196. Application stop working. Please contact to administrator");
                    }
                }
            }
            catch (Exception ep)
            {
                Gujjar.ErrMsg(ep);
                Application.Exit();
            }
        }
        private void tbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(btnLogin, null);
                
            }
        }
    }
}
