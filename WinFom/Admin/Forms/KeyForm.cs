using Khattana.Common;
using Khattana.Model;
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
using WinFom.Common.Model;

namespace WinFom.Admin.Forms
{
    public partial class KeyForm : Form
    {
        string macid = "";
        public bool IsDone { get; set; }
        public KeyForm()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            IsDone = false;
            Close();            
        }

        private void KeyForm_Load(object sender, EventArgs e)
        {
            IsDone = false;
            try
            {
                WaitForm wait = new WaitForm(LoadMachineId);
                wait.ShowDialog();

                tbMachineId.Text = macid;

                Gujjar.TB4(panel2);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadMachineId()
        {
            try
            {
                macid = Lisence.GetMachineId();
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
                if(!Gujjar.IsValidForm(panel2))
                {
                    throw new Exception("Please fill all text fields");
                }

                SecOptMod mod = Helper.GetSecOpsObj();
                string keyEnc = tbKey.Text;
                string key2 = MsrCipher.Decrypt(keyEnc);
                if(!Lisence.IsValidKey(key2, mod.secretPhrase))
                {
                    throw new Exception("Invalid key");
                }

                DialogResult res = Gujjar.ConfirmYesNo("Confirm please...");
                if (res == DialogResult.No)
                    return;
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var tobj = db.Anattakh.FirstOrDefault();
                            if (tobj != null)
                            {
                                db.Anattakh.Remove(tobj);
                                db.SaveChanges();
                            }

                            var kobj = db.KeyInfo.Find(macid);
                            if (kobj != null)
                            {
                                db.KeyInfo.Remove(kobj);
                                db.SaveChanges();
                            }

                            KeyInfo keyInfo = new KeyInfo
                            {
                                Id = macid,
                                DateAdded = DateTime.Now,
                                IsValid = MsrCipher.Encrypt(mod.aaho),
                                Key = keyEnc,
                                WahJi = MsrCipher.Encrypt(DateTime.Now.ToString())
                            };

                            db.KeyInfo.Add(keyInfo);
                            db.SaveChanges();

                            trans.Commit();
                            IsDone = true;
                            Gujjar.InfoMsg("Lisence key is applied successfully");
                            Close();
                        }
                        catch (Exception exp2)
                        {
                            trans.Rollback();
                            throw exp2;
                        }
                        
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
                Application.Exit();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            IsDone = false;
            Close();
        }
    }
}
