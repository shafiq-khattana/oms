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
using System.Drawing.Imaging;
using Model.EFiling.Model;

namespace WinFom.EFileUI.Forms
{
    public partial class ViewFileForm : Form
    {
        List<EFileImage> images = null;
        string title = "";
        private int imageIndex = 0;
        public ViewFileForm(List<EFileImage> imageList, string title)
        {
            InitializeComponent();
            images = imageList;
            this.title = title;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetImage(int i)
        {
            var obj = images[i];
            pictureBox1.Image = Gujjar.GetImageFromByteArray(obj.PicData);
            label4.Text = obj.Title;
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                SetImage(imageIndex);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Gujjar.InfoMsg("In Progress");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = @"C:\EFiles";
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string compFile = string.Format(@"{0}\{1}", path, title);
            pictureBox1.Image.Save(compFile, ImageFormat.Jpeg);

            Gujjar.InfoMsg(string.Format("File is saved at {0}", compFile));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            imageIndex++;
            if(imageIndex == images.Count)
            {
                imageIndex = 0;
            }
            SetImage(imageIndex);
        }
    }
}
