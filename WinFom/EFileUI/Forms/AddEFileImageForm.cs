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
using System.IO;
using Model.EFiling.Model;

namespace WinFom.EFileUI.Forms
{
    public partial class AddEFileImageForm : Form
    {
        private int eFileRecordId = 0;
        private EFile eFile = null;
        public bool IsDone { get; set; }
        public AddEFileImageForm(int id)
        {
            InitializeComponent();
            eFileRecordId = id;
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
                using (Context db = new Context())
                {
                    eFile = db.EFiles.Find(eFileRecordId);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        byte[] picData = null;
        string ext = "";
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dial = new OpenFileDialog())
            {
                try
                {
                    dial.Filter = "JPG Files (*.jpg) | *.jpg";
                    DialogResult result = dial.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        pictureBox1.Image = Image.FromFile(dial.FileName);
                        Image img = pictureBox1.Image;
                        picData = Gujjar.GetByteArrayFromImage(img);
                        ext = dial.DefaultExt;
                        tbEFileTitle.Text = dial.FileName.Substring(dial.FileName.LastIndexOf('\\') + 1);
                        int x1, y1;
                        if(img.Width > img.Height)
                        {
                            x1 = 1200;
                            y1 = 800;
                        }
                        else
                        {
                            x1 = 800;
                            y1 = 1200;
                        }
                        Size size = new Size(x1, y1);

                        using (MemoryStream inStream = new MemoryStream(picData))
                        {
                            using (MemoryStream outStream = new MemoryStream())
                            {
                                ImageProcessor.ImageFactory fact = new ImageProcessor.ImageFactory(preserveExifData: true);
                                fact.Load(inStream).Resize(new ImageProcessor.Imaging.ResizeLayer(size, ImageProcessor.Imaging.ResizeMode.Stretch))
                                    .Format(new ImageProcessor.Imaging.Formats.JpegFormat())
                                    .Save(outStream);
                                picData = null;
                                picData = outStream.ToArray();
                                Image img2 = Image.FromStream(outStream);
                                pictureBox1.Image = img2;

                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    Gujjar.ErrMsg(exp);
                }
            }
        }

        private void btnAddEFile_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }

                if(picData == null)
                {
                    throw new Exception("Please upload eFile image");
                }

                DialogResult res = Gujjar.ConfirmYesNo("Are you sured? Please confirm..!!");
                if (res == DialogResult.No)
                    return;

                using (Context db = new Context())
                {
                    EFileImage fileImage = new EFileImage
                    {
                        DateAdded = eFile.DateAdded,
                        EFile = null,
                        EFileId = eFileRecordId,
                        Id = 0,
                        MimeType = ext,
                        PicData = picData,
                        Title = tbEFileTitle.Text
                    };

                    db.EFileImages.Add(fileImage);
                    db.SaveChanges();

                    Gujjar.InfoMsg("EFile is added in database");
                    IsDone = true;
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
    }
}
