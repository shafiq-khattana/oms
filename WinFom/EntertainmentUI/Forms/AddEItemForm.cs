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
using Model.Entertainment.Model;

namespace WinFom.EntertainmentUI.Forms
{
    public partial class AddEItemForm : Form
    {
        public bool IsDone { get; set; }
        public AddEItemForm()
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
                Gujjar.UrduFont(tbNameUrdu);
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
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        string nameEng = tbNameEng.Text;
                        string nameUrdu = tbNameUrdu.Text;

                        var eItemDb = db.EntItems.ToList().FirstOrDefault(a => a.Title.ToLower().Equals(nameEng.ToLower()));
                        if(eItemDb != null)
                        {
                            throw new Exception("Item already exists in database");
                        }

                        eItemDb = new EntItem
                        {
                            Id = 0,
                            NameUrdu = nameUrdu,
                            QtyConsumed = 0,
                            Title = nameEng
                        };

                        db.EntItems.Add(eItemDb);
                        db.SaveChanges();
                        trans.Commit();
                        IsDone = true;

                        Gujjar.InfoMsg("Item is added in database successfully");                        
                        Close();
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
