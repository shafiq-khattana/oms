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
using Khattana.BioMetric.SecuGenFingerPrintNonAuto;
using System.Configuration;

namespace WinFom.ReadyStuff.Forms
{
    public partial class AddReadyDriverForm2 : Form
    {
        WebCam webcam = null;
        public ReadyDriver driver = null;
        private byte[] data1 = null;
        private byte[] data2 = null;

        private int tn1 = 0;
        private int tn2 = 0;
        private string password = "";
        public AddReadyDriverForm2()
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
                password = ConfigurationManager.AppSettings["biopass"];
                bsPics.Value = pPics.Enabled = pThumbs.Enabled = bsThumbs.Value = false;
                Gujjar.TB4(pMain);
                webcam = new WebCam();
                webcam.InitializeWebCam(ref pictureBox1);
                bsPics_Click(null, null);
                bsThumbs_Click(null, null);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnCamStart_Click(object sender, EventArgs e)
        {
            webcam.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webcam.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = pictureBox1.Image;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dial = new OpenFileDialog();
                dial.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                if (dial.ShowDialog() == DialogResult.OK)
                {
                    pictureBox2.Image = Image.FromFile(dial.FileName);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btncap1_Click(object sender, EventArgs e)
        {
            try
            {
                BOps bops = new BOps(password);
                data1 = bops.CaptureImage(thumbp1, thumb1, ref tn1, password);
                if (data1 != null)
                {
                    Gujjar.InfoMsg(string.Format("Finger print image is captured with quality {0}", tn1));
                   
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btncap2_Click(object sender, EventArgs e)
        {
            try
            {
                BOps bops = new BOps(password);
                data2 = bops.CaptureImage(thumbp2, thumb2, ref tn2, password);
                if (data2 != null)
                {
                    Gujjar.InfoMsg(string.Format("Finger print image is captured with quality {0}", tn2));
                    
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void bsThumbs_Click(object sender, EventArgs e)
        {
            if(bsThumbs.Value)
            {
                pThumbs.Enabled = true;
                thumb1.Image = null;
                thumb2.Image = null;
            }
            else
            {
                thumb2.Image = thumb1.Image = Properties.Resources.fingerprint_reader;
                pThumbs.Enabled = false;
            }
        }

        private void bsPics_Click(object sender, EventArgs e)
        {
            if(bsPics.Value)
            {
                pictureBox2.Image = null;
                pPics.Enabled = true;
            }
            else
            {
                pPics.Enabled = false;
                pictureBox2.Image = Properties.Resources.userimage;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] thumbData = null;
                byte[] tpicByte = null;
                byte[] selectorPic = null;
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all textfields");
                }
                if(bsThumbs.Value)
                {
                    if(thumb1.Image == null || thumb2.Image == null)
                    {
                        throw new Exception("Please provide thumb impression through bio metric device");
                    }
                    BOps bops = new BOps(password);
                    int sn = 0;
                    if (!bops.IsMatched(data1, data2, ref sn, password))
                    {
                        throw new Exception("Your thumb impression doesn't match.");
                    }
                    thumbData = data1;
                    Image tpic = thumb1.Image;
                    if (tn2 > tn1)
                    {
                        thumbData = data2;
                        tpic = thumb2.Image;
                    }
                    tpicByte = Gujjar.GetByteArrayFromImage(tpic);
                }
                if(bsPics.Value)
                {
                    if(pictureBox2.Image == null)
                    {
                        throw new Exception("Please provide selector picture");
                    }
                    selectorPic = Gujjar.GetByteArrayFromImage(pictureBox2.Image);
                }

                ReadyDriver driver2 = new ReadyDriver
                {
                    Address = tbAddress.Text,
                    CNIC = tbCNIC.Text,
                    Contact = tbContact.Text,
                    DateAdded = DateTime.Now,
                    Extra = tbMisc.Text,
                    IsActive = true,
                    Schedules = null,
                    Id = 0,
                    IsAvailable = true,
                    Name = tbCompany.Text,
                    PicData = selectorPic,
                    Remarks = tbRemarks.Text,
                    ThumbData = thumbData,
                    ThumbPicData = tpicByte
                };
                using (Context db = new Context())
                {
                    var obj = db.ReadyDrivers.FirstOrDefault(a => a.CNIC.Equals(driver2.CNIC));
                    if(obj != null)
                    {
                        throw new Exception("Driver already added in database");
                    }

                    DialogResult res = Gujjar.ConfirmYesNo("Please confirm..!! All information is correct?");
                    if (res == DialogResult.No)
                        return;

                    driver = db.ReadyDrivers.Add(driver2);
                    db.SaveChanges();

                    Gujjar.InfoMsg("Driver is added in database successfully");
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
