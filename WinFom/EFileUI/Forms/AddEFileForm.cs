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
using Model.EFiling.Model;
using System.IO;

namespace WinFom.RepairUI.Forms
{
    public partial class AddEFileForm : Form
    {
        private List<EFCategory> efCats = null;
        private EFCategory efCat = null;
        private AppSettings appSett = Helper.AppSet;
        private byte[] picData = null;
        public bool IsDone { get; set; }
        public AddEFileForm()
        {
            InitializeComponent();
        }

        private void LoadEFCats()
        {
            try
            {
                if(efCats != null)
                {
                    efCats.Clear();
                    efCats = null;
                }
                using (Context db = new Context())
                {
                    efCats = db.EFCategories.OrderBy(a => a.Title).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void BindCbCats()
        {
            cbCategories.DataSource = efCats;
            cbCategories.DisplayMember = "Title";
            cbCategories.ValueMember = "Id";
        }
        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadAndBindCbCats()
        {
            WaitForm wait = new WaitForm(LoadEFCats);
            wait.ShowDialog();
            BindCbCats();
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAndBindCbCats();
                dtpDateAdded.MinDate = appSett.StartDate;
                dtpDateAdded.MaxDate = appSett.EndDate;
                IsDone = false;
                tbDescription.Text = "N/A";
                Gujjar.TBOptional(tbDescription);
                Helper.IsOkApplied();
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

        private void cbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbCategories.SelectedIndex == -1)
            {
                efCat = null;
                return;
            }
            else
            {
                efCat = cbCategories.SelectedItem as EFCategory;
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            try
            {
                AddEFileCategoryForm form = new AddEFileCategoryForm();
                form.ShowDialog();

                int cid = form.CategoryId;
                if(cid != 0)
                {
                    LoadAndBindCbCats();
                    cbCategories.SelectedItem = cbCategories.Items.OfType<EFCategory>()
                        .FirstOrDefault(a => a.Id == cid);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
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
                if(efCat == null)
                {
                    throw new Exception("Please select category");
                }
                if(picData == null)
                {
                    throw new Exception("Please choose/add E File");
                }
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            string title = tbEFileTitle.Text;

                            var obj = db.EFiles.AsParallel().ToList()
                                .Where(a => a.DateAdded.Date == dtpDateAdded.Value.Date &&
                                a.Title.ToLower().Equals(title.ToLower()) && a.EFCategoryId == efCat.Id)
                                .FirstOrDefault();
                            if (obj != null)
                            {
                                throw new Exception("E File is already added in database");
                            }

                            EFile eFile = new EFile
                            {
                                DateAdded = dtpDateAdded.Value,
                                Category = null,
                                Description = tbDescription.Text,
                                EFCategoryId = efCat.Id,
                                Id = 0,

                                Title = title
                            };

                            eFile = db.EFiles.Add(eFile);
                            db.SaveChanges();

                            EFileImage fileImage = new EFileImage
                            {
                                DateAdded = dtpDateAdded.Value.Date,
                                EFile = null,
                                EFileId = eFile.Id,
                                Id = 0,
                                MimeType = ext,
                                PicData = picData,
                                Title = title
                            };
                            db.EFileImages.Add(fileImage);
                            db.SaveChanges();
                            trans.Commit();

                            Gujjar.InfoMsg("E File is added in database successfully");
                            IsDone = true;
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
            }
        }
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

                        int x1, y1;
                        if (img.Width > img.Height)
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
    }
}
