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
using Khattana.BioMetric.SecuGenFingerPrintNonAuto;
using WinFom.Common.Model;
using Model.ReadyStuff.Model;
using WinFom.Common.Forms;
using System.Configuration;

namespace WinFom.ReadyStuff.Forms
{
    public partial class AddReadySelectorForm : Form
    {
        private string password = "";
        byte[] temp1 = null;
        int n1 = 0;
        private WebCam webcam = null;
        public ReadySelector selector = null;
        private List<ReadySelector> selectors = null;

        private byte[] data1 = null;
        private byte[] data2 = null;

        private int tn1 = 0;
        private int tn2 = 0;
        
        private bool IsCaptured1 = false;
        private bool IsCaptured2 = false;
        public AddReadySelectorForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            webcam.Stop();
            Close();
        }

        private void LoadDrivers()
        {
            try
            {
                using (Context db = new Context())
                {
                    selectors = db.ReadySelectors.ToList();
                    
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void BindCBSelectors()
        {
            cbSelectors.DataSource = selectors;
            cbSelectors.DisplayMember = "Name";
            cbSelectors.ValueMember = "Id";
            cbSelectors.SelectedItem = cbSelectors.Items.OfType<ReadySelector>().FirstOrDefault(a => a.Name == "N/A");
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                password = ConfigurationManager.AppSettings["biopass"];
                webcam = new WebCam();
                webcam.InitializeWebCam(ref pictureBox1);

                WaitForm wait = new WaitForm(LoadDrivers);
                wait.ShowDialog();
                //panel1.Enabled = false;
                //panel2.Enabled = false;

                Gujjar.TB4(panel2);
                BindCBSelectors();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void captureBtn1_Click(object sender, EventArgs e)
        {
            try
            {
                int n2 = 0;
                BOps bops = new BOps(password);
                temp1 = bops.CaptureImage(progressBar1, pic1, ref n1, password);
                int found = 0;
                if (temp1 != null)
                {
                    //Gujjar.InfoMsg(string.Format("Finger print image is captured with quality {0}", n1));

                    foreach (var item in selectors)
                    {
                        if (bops.IsMatched(item.ThumbData, temp1, ref n2, password))
                        {
                            selector = item;

                            tbAddress.Text = selector.Address;
                            tbContact.Text = selector.Contact;
                            tbName.Text = selector.Name;
                            tbRemarks.Text = selector.Remarks;
                            tbMisc.Text = selector.Extra;
                            tbCnic.Text = selector.CNIC;

                            pictureBox2.Image = Gujjar.GetImageFromByteArray(selector.PicData);
                            //panel1.Enabled = false;
                            //panel2.Enabled = false;
                            found = 1;
                            //isRegistered = true;
                            //btnAdd.Enabled = true;

                            thumb1.Image = thumb2.Image = Gujjar.GetImageFromByteArray(selector.ThumbPicData);
                            webcam.Stop();
                            break;
                        }
                    }
                    if (found == 0)
                    {
                        //panel1.Enabled = true;
                        //panel2.Enabled = true;
                        //btnAdd.Enabled = false;
                        //isRegistered = false;
                        throw new Exception("No driver found with this thumb impression. You need to add as a new driver");
                    }
                }
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

        private void btncap1_Click(object sender, EventArgs e)
        {
            try
            {
                BOps bops = new BOps(password);
                data1 = bops.CaptureImage(thumbp1, thumb1, ref tn1, password);
                if (data1 != null)
                {
                    Gujjar.InfoMsg(string.Format("Finger print image is captured with quality {0}", tn1));
                    IsCaptured1 = true;
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
                    IsCaptured2 = true;
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Gujjar.IsValidForm(panel2))
                {
                    throw new Exception("Please fill all text fields");
                }
                Image picData = pictureBox2.Image;
                if (picData == null)
                {
                    throw new Exception("Please capture driver's picture through web cam");
                }
                

                byte[] picByteData = null;
                byte[] thumbPicData = null;
                byte[] thumbData = null;
                picByteData = Gujjar.GetByteArrayFromImage(picData);
                if (IsCaptured1 && IsCaptured2)
                {
                    if (data1 == null || data2 == null)
                    {
                        throw new Exception("Please place thumb impression twice");
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
                    
                    thumbPicData = Gujjar.GetByteArrayFromImage(tpic);
                }
                else
                {
                    thumbData = null;
                    thumbPicData = Gujjar.GetByteArrayFromImage(Properties.Resources.Fingerprint_96px);
                    
                }
                selector = new ReadySelector
                {
                    Address = tbAddress.Text,
                    CNIC = tbCnic.Text,
                    Contact = tbContact.Text,
                    DateAdded = DateTime.Now,
                    Extra = tbMisc.Text,
                    IsActive = true,
                    IsAvailable = true,
                    Name = tbName.Text,
                    PicData = picByteData,
                    Remarks = tbRemarks.Text,
                    ThumbData = thumbData,
                    ThumbPicData = thumbPicData
                };

                using (Context db = new Context())
                {
                    db.ReadySelectors.Add(selector);
                    db.SaveChanges();
                    btnAdd_Click(null, null);
                    Gujjar.InfoMsg("Driver information is added in database successfully");
                    
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            webcam.Stop();
            Close();

        }

        private void cbDrivers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSelectors.SelectedIndex == -1)
                return;

            selector = cbSelectors.SelectedItem as ReadySelector;

            tbAddress.Text = selector.Address;
            tbContact.Text = selector.Contact;
            tbName.Text = selector.Name;
            tbRemarks.Text = selector.Remarks;
            tbMisc.Text = selector.Extra;
            tbCnic.Text = selector.CNIC;

            if(selector.Id == 1)
            {
                pictureBox2.Image = Properties.Resources.atomix_user31;
                thumb1.Image = thumb2.Image = Properties.Resources.Fingerprint_96px;
            }
            else
            {
                pictureBox2.Image = Gujjar.GetImageFromByteArray(selector.PicData);
                thumb1.Image = thumb2.Image = Gujjar.GetImageFromByteArray(selector.ThumbPicData);
            }
            
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

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
